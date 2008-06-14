' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

    'Private m_VariableIdentifier As VariableIdentifier
    'Private m_IsNew As Boolean
    'Private m_NewExpression As DelegateOrObjectCreationExpression
    'Private m_TypeName As TypeName
    'Private m_VariableInitializer As VariableInitializer
    'Private m_ArgumentList As ArgumentList

    'Private m_Name As String

    Private m_LocalBuilder As Mono.Cecil.Cil.VariableDefinition
    Private m_FieldBuilderStaticCecil As Mono.Cecil.FieldDefinition
    'Private m_FieldType As Mono.Cecil.TypeReference 'TODO: Rename to m_VariableType

    'Private m_WithEventsRedirect As PropertyDeclaration
    'Private m_HandledEvents As New Generic.List(Of EventInfo)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As VariableIdentifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Attributes, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal VariableIdentifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As TypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Attributes, Modifiers, VariableIdentifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal Identifier As Identifier, _
    ByVal IsNew As Boolean, ByVal TypeName As NonArrayTypeName, ByVal VariableInitializer As VariableInitializer, ByVal ArgumentList As ArgumentList)
        MyBase.New(Parent, Attributes, Identifier, IsNew, TypeName, VariableInitializer, ArgumentList)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As Mono.Cecil.TypeReference)
        MyBase.Init(Attributes, Modifiers, Name, VariableType)
        CreateMember()
        'm_Name = Name
        'FieldType = VariableType

        'Helper.Assert(m_Name <> "")
        'Helper.Assert(FieldType IsNot Nothing)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Name As String, ByVal VariableType As TypeName)
        MyBase.Init(Attributes, Modifiers, Name, VariableType)
        CreateMember()
        'm_Name = Name
        'm_TypeName = VariableType

        'Helper.Assert(m_Name <> "")
        'Helper.Assert(m_TypeName IsNot Nothing)
    End Sub

    ReadOnly Property IsStatic() As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.Static)
        End Get
    End Property

    ReadOnly Property LocalBuilder() As Mono.Cecil.Cil.VariableDefinition
        Get
            Return m_LocalBuilder
        End Get
    End Property

    ReadOnly Property StaticInitBuilder() As Mono.Cecil.FieldDefinition
        Get
            Return m_FieldBuilderStaticCecil
        End Get
    End Property

    'ReadOnly Property FieldBuilderCecil() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldBuilderCecil
    '    Get
    '        Return m_FieldBuilderCecil
    '    End Get
    'End Property

    'ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldBuilder
    '    Get
    '        Return m_FieldBuilderCecil
    '    End Get
    'End Property

    'Overrides ReadOnly Property VariableType() As Mono.Cecil.TypeReference
    '    Get
    '        Return m_FieldType
    '    End Get
    'End Property

    'ReadOnly Property VariableTypeOrTypeBuilder() As Mono.Cecil.TypeReference
    '    Get
    '        Return Helper.GetTypeOrTypeBuilder(Compiler, VariableType)
    '    End Get
    'End Property

    ReadOnly Property IsLocalVariable() As Boolean
        Get
            Return True 'Me.Modifiers.Is(ModifierMasks.Static) = False 'AndAlso Me.FindFirstParent(Of CodeBlock)() IsNot Nothing
        End Get
    End Property

    'ReadOnly Property IsFieldVariable() As Boolean
    '    Get
    '        Return Not IsLocalVariable
    '    End Get
    'End Property

    ReadOnly Property IsStaticVariable() As Boolean
        Get
            Return Me.Modifiers.Is(ModifierMasks.Static)
        End Get
    End Property

    'Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
    '    Get
    '        Return m_FieldBuilderCecil
    '    End Get
    'End Property

    'Property FieldType() As Mono.Cecil.TypeReference
    '    Get
    '        Return m_FieldBuilderCecil.FieldType
    '    End Get
    '    Set(ByVal value As Mono.Cecil.TypeReference)
    '        m_FieldBuilderCecil.FieldType = value
    '    End Set
    'End Property

    'Private ReadOnly Property FieldType2() As Mono.Cecil.TypeReference Implements IFieldMember.FieldType
    '    Get
    '        Return m_FieldBuilderCecil.FieldType
    '    End Get
    'End Property

    'ReadOnly Property TypeName() As TypeName
    '    Get
    '        Return m_TypeName
    '    End Get
    'End Property

    'ReadOnly Property IsNew() As Boolean
    '    Get
    '        Return m_IsNew
    '    End Get
    'End Property

    'ReadOnly Property VariableInitializer() As VariableInitializer
    '    Get
    '        Return m_VariableInitializer
    '    End Get
    'End Property

    'ReadOnly Property ArgumentList() As ArgumentList
    '    Get
    '        Return m_ArgumentList
    '    End Get
    'End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        'If m_VariableInitializer IsNot Nothing Then result = m_VariableInitializer.ResolveTypeReferences() AndAlso result

        'If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        'If m_FieldType Is Nothing Then 'the declaration might have been created with the type already.
        '    If m_TypeName IsNot Nothing Then
        '        m_FieldType = m_TypeName.ResolvedType

        '        If m_IsNew Then
        '            If m_TypeName.IsNonArrayTypeName = False Then
        '                result = Helper.AddError(Me) AndAlso result
        '            End If
        '            m_NewExpression = New DelegateOrObjectCreationExpression(Me, m_TypeName.AsNonArrayTypeName, m_ArgumentList)
        '        End If
        '    ElseIf m_VariableIdentifier.Identifier.HasTypeCharacter Then
        '        m_FieldType = TypeCharacters.TypeCharacterToType(Compiler, m_VariableIdentifier.Identifier.TypeCharacter)
        '    Else
        '        If Me.Location.File(Compiler).IsOptionStrictOn Then
        '            result = Compiler.Report.ShowMessage(Messages.VBNC30209, Me.Location) AndAlso result
        '        Else
        '            Helper.AddWarning("Variable type should be specified.")
        '        End If
        '        m_FieldType = Compiler.TypeCache.System_Object
        '    End If
        'End If

        'If m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.HasArrayNameModifier Then
        '    If CecilHelper.IsArray(m_FieldType) Then
        '        result = Helper.AddError(Me, "Cannot specify array modifier on both type name and on variable name.") AndAlso result
        '    Else
        '        If m_VariableIdentifier.ArrayNameModifier.IsArraySizeInitializationModifier Then
        '            m_FieldType = m_VariableIdentifier.ArrayNameModifier.AsArraySizeInitializationModifier.CreateArrayType(m_FieldType)
        '        ElseIf m_VariableIdentifier.ArrayNameModifier.IsArrayTypeModifiers Then
        '            m_FieldType = m_VariableIdentifier.ArrayNameModifier.AsArrayTypeModifiers.CreateArrayType(m_FieldType)
        '        Else
        '            Throw New InternalException(Me)
        '        End If
        '    End If
        'End If

        'If m_NewExpression IsNot Nothing Then result = m_NewExpression.ResolveTypeReferences AndAlso result

        'Helper.Assert(m_FieldType IsNot Nothing)

        result = CreateMember() AndAlso result

        Return result
    End Function

    'Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
    '    Dim result As Boolean = True

    '    Helper.Assert(m_FieldType IsNot Nothing)

    '    Return result
    'End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result
        'If m_ArgumentList IsNot Nothing Then result = m_ArgumentList.ResolveCode(ResolveInfo.Default(Info.Compiler)) AndAlso result

        'If m_NewExpression IsNot Nothing Then
        '    result = m_NewExpression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
        'End If

        'If m_VariableInitializer IsNot Nothing Then
        '    result = m_VariableInitializer.ResolveCode(New ExpressionResolveInfo(Compiler, m_FieldType)) AndAlso result
        'End If

        Return result
    End Function

    Private Function CreateMember() As Boolean
        Dim result As Boolean = True

        If IsLocalVariable Then
            'Local builder will be defined in GenerateCode
        Else
            'If m_FieldBuilderCecil IsNot Nothing Then
            '    m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(Name, Helper.GetTypeOrTypeReference(Compiler, FieldType), Helper.GetAttributes(Compiler, Me))
            '    DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)
            'End If
        End If

        Return result
    End Function

    'Public Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
    '    Dim result As Boolean = True

    '    Return result
    'End Function

    Friend Function DefineStaticMember() As Boolean
        Dim result As Boolean = True

        'If FieldBuilder Is Nothing Then

        'm_FieldBuilder = Me.DeclaringType.TypeBuilder.DefineField(staticName, VariableTypeOrTypeBuilder, m_Descriptor.Attributes)
        'Compiler.TypeManager.RegisterReflectionMember(m_FieldBuilder, Me.FieldDescriptor)
        If Me.HasInitializer Then
            Dim staticName As String
            staticName = "$STATIC$" & Me.FindFirstParent(Of INameable).Name & "$" & Me.ObjectID.ToString & "$" & Me.Name & "$Init"
            'm_StaticInitBuilder = Me.DeclaringType.TypeBuilder.DefineField(m_FieldBuilder.Name & "$Init", Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag, m_FieldBuilder.Attributes)
            '#If ENABLECECIL Then
            Dim attr As Mono.Cecil.FieldAttributes = Mono.Cecil.FieldAttributes.Private
            If DeclaringMethod.IsShared Then attr = attr Or Mono.Cecil.FieldAttributes.Static

            m_FieldBuilderStaticCecil = New Mono.Cecil.FieldDefinition(staticName, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag), attr)
            DeclaringType.CecilType.Fields.Add(m_FieldBuilderStaticCecil)
            '#End If
        End If
        '#If ENABLECECIL Then
        '        m_FieldBuilderCecil = New Mono.Cecil.FieldDefinition(staticName, Helper.GetTypeOrTypeReference(Compiler, FieldType), Helper.GetAttributes(Compiler, Me))
        '        DeclaringType.CecilType.Fields.Add(m_FieldBuilderCecil)
        '#End If
        'End If

        Return result
    End Function

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
                result = EmitStaticInitializer(Info) AndAlso result
            Else
                'Field builder has been defined in DefineMember
                'EmitVariableInitializer will be called by the constructor declaration
            End If

            result = MyBase.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

    Private ReadOnly Property FieldBuilder() As Mono.Cecil.FieldDefinition
        Get
            Return m_FieldBuilderStaticCecil
        End Get
    End Property

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
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Enter_Object)
        'Try
        Dim exBlock As Label
        exBlock = Emitter.EmitBeginExceptionBlock(Info)
        '   If initvar.State = 0 Then
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
        Emitter.EmitLoadVariable(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        Emitter.EmitLoadI4Value(Info, 0I)
        Info.ILGen.Emit(Mono.Cecil.Cil.OpCodes.Bne_Un_S, elseIfLabel)
        '       initvar.State = 2
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
        Emitter.EmitLoadI4Value(Info, 2I)
        Emitter.EmitStoreField(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        '       (initalization)
        result = EmitVariableInitializer(Info) AndAlso result
        Emitter.EmitBranch(Info, endIfLabel)
        '   ElseIf initvar.State = 2 Then
        Emitter.MarkLabel(Info, elseIfLabel)
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
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
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
        Emitter.EmitLoadI4Value(Info, 1I)
        Emitter.EmitStoreField(Info, Compiler.TypeCache.MS_VB_CS_StaticLocalInitFlag__State)
        '   Monitor.Exit(initvar)
        If Not m_FieldBuilderStaticCecil.IsStatic Then Emitter.EmitLoadMe(Info, Me.DeclaringType.CecilType)
        Emitter.EmitLoadVariable(Info, m_FieldBuilderStaticCecil)
        Emitter.EmitCall(Info, Compiler.TypeCache.System_Threading_Monitor__Exit_Object)
        'End Try
        Info.ILGen.EndExceptionBlock()
        Return result
    End Function

    'Function EmitVariableInitializer(ByVal Info As EmitInfo) As Boolean
    '    Dim result As Boolean = True

    '    Dim varType As Mono.Cecil.TypeReference = VariableType

    '    If m_VariableInitializer IsNot Nothing Then
    '        EmitThisIfNecessary(Info)
    '        result = m_VariableInitializer.GenerateCode(Info.Clone(Me, True, False, varType)) AndAlso result

    '        If m_VariableInitializer.InitializerExpression IsNot Nothing AndAlso Helper.CompareType(varType, Compiler.TypeCache.System_Object) AndAlso Helper.CompareType(m_VariableInitializer.ExpressionType, Compiler.TypeCache.System_Object) Then
    '            Emitter.EmitCall(Info, Compiler.TypeCache.System_Runtime_CompilerServices_RuntimeHelpers__GetObjectValue_Object)
    '        End If
    '        EmitStore(Info)
    '    ElseIf m_IsNew Then
    '        Helper.Assert(m_NewExpression IsNot Nothing)
    '        EmitThisIfNecessary(Info)
    '        result = m_NewExpression.GenerateCode(Info.Clone(Me, True, False, varType)) AndAlso result
    '        EmitStore(Info)
    '    End If

    '    If m_VariableIdentifier IsNot Nothing AndAlso m_VariableIdentifier.ArrayNameModifier IsNot Nothing AndAlso m_VariableIdentifier.ArrayNameModifier.IsArraySizeInitializationModifier Then
    '        EmitThisIfNecessary(Info)
    '        ArrayCreationExpression.EmitArrayCreation(Me, Info, varType, m_VariableIdentifier.ArrayNameModifier.AsArraySizeInitializationModifier)
    '        EmitStore(Info)
    '    End If

    '    Return result
    'End Function

    '''' <summary>
    '''' Checks for this grammar:
    '''' VariableMemberDeclaration  ::=	[  Attributes  ]  VariableModifier+  VariableDeclarators  StatementTerminator
    '''' </summary>
    'Shared Function IsMe(ByVal tm As tm) As Boolean
    '    Dim i As Integer
    '    While tm.PeekToken(i).Equals(ModifierMasks.VariableModifiers)
    '        i += 1
    '    End While
    '    Return i > 0 AndAlso tm.PeekToken(i).IsIdentifier
    'End Function

    'Public ReadOnly Property FieldDescriptor() As Mono.Cecil.FieldDefinition Implements IFieldMember.FieldDescriptor
    '    Get
    '        Return FieldBuilderCecil
    '    End Get
    'End Property

    'Public Function CreateImplicitMembers() As Boolean Implements IHasImplicitMembers.CreateImplicitMembers
    '    Dim result As Boolean = True

    '    If Me.Modifiers.Is(ModifierMasks.WithEvents) = False Then Return result

    '    Dim parentType As TypeDeclaration = Me.FindFirstParent(Of TypeDeclaration)()
    '    Dim propertyAccessor As New PropertyDeclaration(parentType)
    '    Dim modifiers As New Modifiers(ModifierMasks.Private)

    '    If Me.Modifiers.Is(ModifierMasks.Shared) Then
    '        modifiers.AddModifiers(ModifierMasks.Shared)
    '    End If
    '    modifiers.AddModifiers(Me.Modifiers.Mask And ModifierMasks.AccessModifiers)

    '    propertyAccessor.Init(New Attributes(propertyAccessor), modifiers, Name, m_TypeName)
    '    result = propertyAccessor.ResolveTypeReferences() AndAlso result
    '    propertyAccessor.HandlesField = Me

    '    Rename("_" & Name)

    '    parentType.Members.Add(propertyAccessor)

    '    Return result
    'End Function

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Throw New InternalException
        End Get
    End Property
End Class
