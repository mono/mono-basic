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
Public MustInherit Class VariableDeclaration
    Inherits MemberDeclaration

    Private m_VariableIdentifier As VariableIdentifier
    Private m_IsNew As Boolean
    Private m_NewExpression As DelegateOrObjectCreationExpression
    Private m_TypeName As TypeName
    Private m_VariableInitializer As VariableInitializer
    Private m_ArgumentList As ArgumentList
    Private m_Referenced As Boolean

    Private m_VariableType As Mono.Cecil.TypeReference

    Private m_HandledEvents As New Generic.List(Of Mono.Cecil.EventReference)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As VariableIdentifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent)
        MyBase.Init(Modifiers, VariableIdentifier.Name)
        m_VariableIdentifier = VariableIdentifier
        m_IsNew = IsNew
        m_TypeName = TypeName
        m_VariableInitializer = VariableInitializer
        m_ArgumentList = ArgumentList
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent)
        MyBase.Init(Modifiers, VariableIdentifier.Name)
        m_VariableIdentifier = New VariableIdentifier(Me, VariableIdentifier)
        m_IsNew = IsNew
        m_TypeName = TypeName
        m_VariableInitializer = VariableInitializer
        m_ArgumentList = ArgumentList
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Identifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As NonArrayTypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent)
        MyBase.Init(New Modifiers(), Identifier.Name)

        m_VariableIdentifier = New VariableIdentifier(Me, Identifier)
        m_IsNew = IsNew
        m_TypeName = New TypeName(Me, TypeName)
        m_VariableInitializer = VariableInitializer
        m_ArgumentList = ArgumentList
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As Mono.Cecil.TypeReference)
        MyBase.Init(Modifiers, Name)
        m_VariableType = VariableType
    End Sub

    ReadOnly Property DeclaringMethod() As MethodDeclaration
        Get
            Return Me.FindFirstParent(Of MethodDeclaration)()
        End Get
    End Property

    ReadOnly Property HasInitializer() As Boolean
        Get
            Return m_IsNew OrElse m_VariableInitializer IsNot Nothing OrElse (m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.HasArrayNameModifier AndAlso m_VariableIdentifier.ArrayNameModifier.IsArraySizeInitializationModifier)
        End Get
    End Property

    Public Property VariableType() As Mono.Cecil.TypeReference
        Get
            Return m_VariableType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            m_VariableType = value
        End Set
    End Property

    ReadOnly Property VariableTypeOrTypeBuilder() As Mono.Cecil.TypeReference
        Get
            Return Helper.GetTypeOrTypeBuilder(Compiler, VariableType)
        End Get
    End Property

    Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
        Set(ByVal value As TypeName)
            m_TypeName = value
        End Set
    End Property

    ReadOnly Property IsNew() As Boolean
        Get
            Return m_IsNew
        End Get
    End Property

    ReadOnly Property VariableInitializer() As VariableInitializer
        Get
            Return m_VariableInitializer
        End Get
    End Property

    ReadOnly Property ArgumentList() As ArgumentList
        Get
            Return m_ArgumentList
        End Get
    End Property

    Public Property IsReferenced() As Boolean
        Get
            Return m_Referenced
        End Get
        Set(ByVal value As Boolean)
            m_Referenced = value
        End Set
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        If m_VariableInitializer IsNot Nothing Then result = m_VariableInitializer.ResolveTypeReferences() AndAlso result

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        If m_VariableType Is Nothing Then 'the declaration might have been created with the type already.
            If m_TypeName IsNot Nothing Then
                m_VariableType = m_TypeName.ResolvedType

                If m_IsNew Then
                    If m_TypeName.IsNonArrayTypeName = False Then
                        result = Helper.AddError(Me) AndAlso result
                    End If
                    m_NewExpression = New DelegateOrObjectCreationExpression(Me, m_TypeName.AsNonArrayTypeName, m_ArgumentList)
                End If
            ElseIf m_VariableIdentifier Is Nothing Then
                'Do nothing, we've been created by an event that hasn't ResolveTypeReferences yet.
            ElseIf m_VariableIdentifier.Identifier.HasTypeCharacter Then
                m_VariableType = TypeCharacters.TypeCharacterToType(Compiler, m_VariableIdentifier.Identifier.TypeCharacter)
            Else
                If Me.Location.File(Compiler).IsOptionStrictOn Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30209, Me.Location) AndAlso result
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC42020, Me.Location) AndAlso result
                End If
                m_VariableType = Compiler.TypeCache.System_Object
            End If
        End If

        If m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.HasArrayNameModifier Then
            If CecilHelper.IsArray(m_VariableType) Then
                result = Compiler.Report.ShowMessage(Messages.VBNC31087, Location) AndAlso result
            Else
                If m_VariableIdentifier.ArrayNameModifier.IsArraySizeInitializationModifier Then
                    m_VariableType = m_VariableIdentifier.ArrayNameModifier.AsArraySizeInitializationModifier.CreateArrayType(m_VariableType)
                ElseIf m_VariableIdentifier.ArrayNameModifier.IsArrayTypeModifiers Then
                    m_VariableType = m_VariableIdentifier.ArrayNameModifier.AsArrayTypeModifiers.CreateArrayType(m_VariableType)
                Else
                    Throw New InternalException(Me)
                End If
            End If
        End If

        If m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.IsNullable Then
            result = CecilHelper.CreateNullableType(Me, m_VariableType, m_VariableType) AndAlso result
        End If

        If m_NewExpression IsNot Nothing Then result = m_NewExpression.ResolveTypeReferences AndAlso result

        'Helper.Assert(m_FieldType IsNot Nothing)

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Compiler.Report.Trace("{0}: VariableDeclaration.ResolveCode: {1}", Me.Location, Me.FullName)

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveCode(Info) AndAlso result

        result = MyBase.ResolveCode(ResolveInfo.Default(Info.Compiler)) AndAlso result
        If m_ArgumentList IsNot Nothing Then
            result = m_ArgumentList.ResolveCode(ResolveInfo.Default(Info.Compiler)) AndAlso result
            If result = False Then Return False
        End If

        If m_NewExpression IsNot Nothing Then
            result = m_NewExpression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
            If result = False Then Return False
        End If

        If m_VariableInitializer IsNot Nothing Then
            result = m_VariableInitializer.ResolveCode(New ExpressionResolveInfo(Compiler, VariableType)) AndAlso result
            If result = False Then Return False
        End If

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Helper.Assert(VariableType IsNot Nothing)

        If Me.GeneratedCode = False Then
            'If IsLocalVariable Then
            '    If m_LocalBuilder Is Nothing Then result = DefineLocalVariable(Info) AndAlso result
            '    Helper.Assert(m_LocalBuilder IsNot Nothing)
            '    result = EmitVariableInitializer(Info) AndAlso result
            'ElseIf Me.Modifiers.Is(ModifierMasks.Static) Then
            '    result = EmitStaticInitializer(Info) AndAlso result
            'Else
            '    'Field builder has been defined in DefineMember
            '    'EmitVariableInitializer will be called by the constructor declaration
            'End If

            result = MyBase.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

    Protected MustOverride Sub EmitThisIfNecessary(ByVal Info As EmitInfo)
    Protected MustOverride Sub EmitStore(ByVal Info As EmitInfo)

    Function EmitVariableInitializer(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim varType As Mono.Cecil.TypeReference = VariableType

        If m_VariableInitializer IsNot Nothing Then
            EmitThisIfNecessary(Info)
            result = m_VariableInitializer.GenerateCode(Info.Clone(Me, True, False, varType)) AndAlso result

            If m_VariableInitializer.InitializerExpression IsNot Nothing AndAlso Helper.CompareType(varType, Compiler.TypeCache.System_Object) AndAlso Helper.CompareType(m_VariableInitializer.ExpressionType, Compiler.TypeCache.System_Object) Then
                Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
            End If
            EmitStore(Info)
        ElseIf m_IsNew Then
            Helper.Assert(m_NewExpression IsNot Nothing)
            EmitThisIfNecessary(Info)
            result = m_NewExpression.GenerateCode(Info.Clone(Me, True, False, varType)) AndAlso result
            EmitStore(Info)
        End If

        If m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.ArrayNameModifier IsNot Nothing AndAlso m_VariableIdentifier.ArrayNameModifier.IsArraySizeInitializationModifier Then
            EmitThisIfNecessary(Info)
            ArrayCreationExpression.EmitArrayCreation(Me, Info, varType, m_VariableIdentifier.ArrayNameModifier.AsArraySizeInitializationModifier)
            EmitStore(Info)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Checks for this grammar:
    ''' VariableMemberDeclaration  ::=	[  Attributes  ]  VariableModifier+  VariableDeclarators  StatementTerminator
    ''' </summary>
    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.VariableModifiers)
            i += 1
        End While
        Return i > 0 AndAlso tm.PeekToken(i).IsIdentifier
    End Function
End Class

