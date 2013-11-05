' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' Represents one single variable of a VariableDeclarator.
''' 
''' VariableDeclarator  ::=
'''  	VariableIdentifiers  [  As  [  New  ]  TypeName  [  (  ArgumentList  )  ]  ]  |
'''     VariableIdentifier   [  As  TypeName  ]  [  =  VariableInitializer  ]
''' </summary>
''' <remarks></remarks>
Public Class LocalVariableDeclaration
    Inherits VariableDeclaration

    Private m_LocalBuilder As Mono.Cecil.Cil.VariableDefinition
    Private m_FieldBuilderStatic As Mono.Cecil.FieldDefinition
    Private m_FieldBuilderStaticInit As Mono.Cecil.FieldDefinition

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As VariableIdentifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As NonArrayTypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Identifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As Mono.Cecil.TypeReference)
        MyBase.Init(Modifiers, Name, VariableType)
    End Sub

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition() AndAlso result

        If IsStatic Then
            If m_FieldBuilderStaticInit Is Nothing AndAlso HasInitializer Then
                Dim staticName As String
                Dim attr As Mono.Cecil.FieldAttributes = Mono.Cecil.FieldAttributes.Private

                staticName = "$STATIC$" & Me.FindFirstParent(Of INameable).Name & "$" & Me.ObjectID.ToString & "$" & Me.Name & "$Init"
                If DeclaringMethod.IsShared Then attr = attr Or Mono.Cecil.FieldAttributes.Static

                m_FieldBuilderStaticInit = New Mono.Cecil.FieldDefinition(staticName, attr, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag))
                DeclaringType.CecilType.Fields.Add(m_FieldBuilderStaticInit)
            End If
        Else
            'TODO: Create local builder here too
        End If

        Return result
    End Function

    Private Function DefineField() As Boolean
        Dim result As Boolean = True

        'result = MyBase.Define AndAlso result

        If IsStatic AndAlso m_FieldBuilderStatic Is Nothing Then
            Dim staticName As String
            Dim attr As Mono.Cecil.FieldAttributes = Mono.Cecil.FieldAttributes.Private

            staticName = "$STATIC$" & Me.FindFirstParent(Of INameable).Name & "$" & Me.ObjectID.ToString & "$" & Me.Name
            If DeclaringMethod.IsShared Then attr = attr Or Mono.Cecil.FieldAttributes.Static

            m_FieldBuilderStatic = New Mono.Cecil.FieldDefinition(staticName, attr, Helper.GetTypeOrTypeReference(Compiler, Me.VariableType))
            DeclaringType.CecilType.Fields.Add(m_FieldBuilderStatic)
        End If

        Return result
    End Function

    ReadOnly Property IsStatic() As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.Static)
        End Get
    End Property

    ReadOnly Property IsConst As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.Const)
        End Get
    End Property

    ReadOnly Property LocalBuilder() As Mono.Cecil.Cil.VariableDefinition
        Get
            Return m_LocalBuilder
        End Get
    End Property

    ReadOnly Property StaticInitBuilder() As Mono.Cecil.FieldDefinition
        Get
            Return m_FieldBuilderStaticInit
        End Get
    End Property

    ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition
        Get
            Return m_FieldBuilderStatic
        End Get
    End Property

    ReadOnly Property IsLocalVariable() As Boolean
        Get
            Return Me.Modifiers.Is(ModifierMasks.Static) = False
        End Get
    End Property

    ReadOnly Property IsStaticVariable() As Boolean
        Get
            Return Me.Modifiers.Is(ModifierMasks.Static)
        End Get
    End Property

    Friend Function DefineLocalVariable(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Me.IsStaticVariable Then Return result

        Helper.Assert(IsLocalVariable)

        If m_LocalBuilder Is Nothing Then
            m_LocalBuilder = Emitter.DeclareLocal(Info, VariableTypeOrTypeBuilder, Me.Name)
        End If

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Helper.Assert(VariableType IsNot Nothing)

        If Me.GeneratedCode = False Then
            If IsLocalVariable Then
                If m_LocalBuilder Is Nothing Then result = DefineLocalVariable(Info) AndAlso result
                Helper.Assert(m_LocalBuilder IsNot Nothing)
                result = EmitVariableInitializer(Info) AndAlso result
            ElseIf Me.Modifiers.Is(ModifierMasks.Static) Then
                result = DefineField() AndAlso result
                result = EmitStaticInitializer(Info) AndAlso result
            End If

            result = MyBase.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

    Protected Overrides Sub EmitStore(ByVal Info As EmitInfo)
        If m_LocalBuilder IsNot Nothing Then
            Emitter.EmitStoreVariable(Info, m_LocalBuilder)
        ElseIf FieldBuilder IsNot Nothing Then
            Emitter.EmitStoreField(Info, FieldBuilder)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End If
    End Sub

    Protected Overrides Sub EmitThisIfNecessary(ByVal Info As EmitInfo)
        If FieldBuilder IsNot Nothing AndAlso FieldBuilder.IsStatic = False Then
            Emitter.EmitLoadMe(Info, FieldBuilder.DeclaringType)
        End If
    End Sub

    Private Function EmitStaticInitializer(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim elseIfLabel As Label
        Dim endIfLabel As Label

        If Me.HasInitializer = False Then Return result

        elseIfLabel = Emitter.DefineLabel(Info)
        endIfLabel = Emitter.DefineLabel(Info)

        'Monitor.Enter(initvar)
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Enter_Object)
        'Try
        Dim exBlock As Label
        exBlock = Emitter.EmitBeginExceptionBlock(Info)
        '   If initvar.State = 0 Then
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitLoadVariable(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        Emitter.EmitLoadI4Value(Info, 0I)
        Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Bne_Un_S, elseIfLabel)
        '       initvar.State = 2
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitLoadI4Value(Info, 2I)
        Emitter.EmitStoreField(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        '       (initalization)
        result = EmitVariableInitializer(Info) AndAlso result
        Emitter.EmitBranch(Info, endIfLabel)
        '   ElseIf initvar.State = 2 Then
        Emitter.MarkLabel(Info, elseIfLabel)
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitLoadVariable(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        Emitter.EmitLoadI4Value(Info, 2I)
        Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Bne_Un_S, endIfLabel)
        '       Throw New IncompleteInitializationException
        Emitter.EmitNew(Info, Compiler.TypeCache.MS_VB_CS_IncompleteInitialization__ctor)
        Emitter.EmitThrow(Info)
        '   End If
        Emitter.MarkLabel(Info, endIfLabel)
        Emitter.EmitLeave(Info, exBlock)
        'Finally
        Info.ILGen.BeginFinallyBlock()
        '   initvar.State = 1
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitLoadI4Value(Info, 1I)
        Emitter.EmitStoreField(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        '   Monitor.Exit(initvar)
        If Not m_FieldBuilderStaticInit.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticInit)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Exit_Object)
        'End Try
        Info.ILGen.EndExceptionBlock()
        Return result
    End Function

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Throw New InternalException
        End Get
    End Property
End Class
