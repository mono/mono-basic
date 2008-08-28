' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public MustInherit Class MethodBaseDeclaration
    Inherits MemberDeclaration
    Implements IMethod

    Private m_Signature As SubSignature
    Private m_Code As CodeBlock

    Private m_DefaultReturnVariable As Mono.Cecil.Cil.VariableDefinition

    Private m_MethodOverrides As Mono.Cecil.MethodReference
    Private m_CecilBuilder As Mono.Cecil.MethodDefinition

    Protected Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
        UpdateDefinition()
    End Sub

    Protected Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.new(Parent)
        UpdateDefinition()
    End Sub

    Protected Sub New(ByVal Parent As EventDeclaration)
        MyBase.new(Parent)
        UpdateDefinition()
    End Sub

    Public Overrides Sub Initialize(ByVal Parent As BaseObject)
        MyBase.Initialize(Parent)

        If m_Signature IsNot Nothing Then m_Signature.Initialize(Me)
        If m_Code IsNot Nothing Then m_Code.Initialize(Me)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Signature As SubSignature)
        MyBase.Init(Attributes, Modifiers, Signature.Name)
        m_Signature = Signature
        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Code As CodeBlock)
        Me.Init(Attributes, Modifiers, Signature)
        m_Code = Code
        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    Overrides Sub UpdateDefinition()
        MyBase.UpdateDefinition()

        If m_CecilBuilder Is Nothing Then
            m_CecilBuilder = New Mono.Cecil.MethodDefinition(Name, 0, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void))
            m_CecilBuilder.Annotations.Add(Compiler, Me)
        End If
        m_CecilBuilder.Name = Name
        m_CecilBuilder.HasThis = Not Me.IsShared

        If Signature IsNot Nothing AndAlso Signature.Parameters IsNot Nothing Then
            ReturnType = m_Signature.ReturnType
            If Signature.Parameters IsNot Nothing Then
                For i As Integer = 0 To Signature.Parameters.Count - 1
                    Signature.Parameters(i).UpdateDefinition()
                Next
            End If
        End If

        MethodAttributes = Helper.GetAttributes(Me)
        MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL
    End Sub

    Property ReturnType() As Mono.Cecil.TypeReference
        Get
            Return m_CecilBuilder.ReturnType.ReturnType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            m_CecilBuilder.ReturnType.ReturnType = Helper.GetTypeOrTypeReference(Compiler, value)
        End Set
    End Property

    Property MethodAttributes() As Mono.Cecil.MethodAttributes
        Get
            Return m_CecilBuilder.Attributes
        End Get
        Set(ByVal value As Mono.Cecil.MethodAttributes)
            If (value And Mono.Cecil.MethodAttributes.MemberAccessMask) = 0 Then
                m_CecilBuilder.Attributes = value Or m_CecilBuilder.Attributes
            Else
                m_CecilBuilder.Attributes = value Or (m_CecilBuilder.Attributes And Not Mono.Cecil.MethodAttributes.MemberAccessMask)
            End If
        End Set
    End Property

    Property MethodImplAttributes() As Mono.Cecil.MethodImplAttributes Implements IMethod.MethodImplementationFlags
        Get
            Return m_CecilBuilder.ImplAttributes
        End Get
        Set(ByVal value As Mono.Cecil.MethodImplAttributes)
            m_CecilBuilder.ImplAttributes = value Or m_CecilBuilder.ImplAttributes
        End Set
    End Property

    Public Overrides ReadOnly Property MemberDescriptor() As Mono.Cecil.MemberReference
        Get
            Return Me.CecilBuilder
        End Get
    End Property

    Property Code() As CodeBlock
        Get
            Return m_Code
        End Get
        Set(ByVal value As CodeBlock)
            m_Code = value
        End Set
    End Property

    Public ReadOnly Property DefaultReturnVariable() As Mono.Cecil.Cil.VariableDefinition Implements IMethod.DefaultReturnVariable
        Get
            Return m_DefaultReturnVariable
        End Get
    End Property

    Public ReadOnly Property GetParameters() As Mono.Cecil.ParameterDefinition() Implements IMethod.GetParameters
        Get
            Helper.Assert(m_Signature IsNot Nothing)
            Helper.Assert(m_Signature.Parameters IsNot Nothing)
            Return m_Signature.Parameters.AsParameterInfo
        End Get
    End Property

    Public Overridable ReadOnly Property HandlesOrImplements() As HandlesOrImplements Implements IMethod.HandlesOrImplements
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property HasReturnValue() As Boolean Implements IMethod.HasReturnValue
        Get
            Return TypeOf m_Signature Is FunctionSignature
        End Get
    End Property

    'Public MustOverride ReadOnly Property ILGenerator() As System.Reflection.Emit.ILGenerator Implements IMethod.ILGenerator
    'Public ReadOnly Property MethodBuilder() As Mono.Cecil.MethodDefinition Implements IMethod.MethodBuilder
    '    Get
    '        Return m_CecilBuilder
    '    End Get
    'End Property

    Public ReadOnly Property CecilBuilder() As Mono.Cecil.MethodDefinition Implements IMethod.CecilBuilder
        Get
            Return m_CecilBuilder
        End Get
    End Property

    ReadOnly Property HasMethodBody() As Boolean
        Get
            Dim result As Boolean

            If TypeOf Me Is ExternalSubDeclaration Then Return False

            result = CBool(MethodAttributes And Mono.Cecil.MethodAttributes.Abstract) = False AndAlso CBool(MethodImplAttributes And Mono.Cecil.MethodImplAttributes.Runtime) = False

            If result Then
                If Me.CustomAttributes IsNot Nothing AndAlso Me.CustomAttributes.IsDefined(Compiler.TypeCache.System_Runtime_InteropServices_DllImportAttribute) Then
                    result = False
                End If
            End If

            Return result
        End Get
    End Property

    'Public ReadOnly Property MethodDescriptor() As Mono.Cecil.MethodDefinition Implements IMethod.MethodDescriptor
    '    Get
    '        Return m_CecilBuilder
    '    End Get
    'End Property

    Public ReadOnly Property Signature() As SubSignature Implements IMethod.Signature
        Get
            Return m_Signature
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        result = m_Signature.ResolveTypeReferences AndAlso result
        If result = False Then Return result
        'm_ParameterTypes = m_Signature.Parameters.ToTypeArray
        ReturnType = m_Signature.ReturnType

        If m_Code IsNot Nothing Then result = m_Code.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overridable Function ResolveMember(ByVal Info As ResolveInfo) As Boolean Implements INonTypeMember.ResolveMember
        Dim result As Boolean = True

        result = m_Signature.Parameters.ResolveParameters(Info, True) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result
        result = ResolveOverrides() AndAlso result
        result = m_Signature.ResolveCode(Info) AndAlso result

        If m_Code IsNot Nothing Then
            result = m_Code.ResolveCode(Info) AndAlso result
        End If

        Return result
    End Function

    Public Overridable Function DefineMember() As Boolean Implements IDefinableMember.DefineMember
        Dim result As Boolean = True

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = DefineOverrides() AndAlso result
        result = MyBase.GenerateCode(Info) AndAlso result

        If Me.IsPropertyHandlesHandler Then
            result = GeneratePropertyHandlers() AndAlso result
        ElseIf Me.HasMethodBody Then
            Helper.Assert(m_Code IsNot Nothing)

            'Create the default return variable
            If Me.HasReturnValue Then
                m_DefaultReturnVariable = Emitter.DeclareLocal(Info, ReturnType)
            End If

            result = m_Code.GenerateCode(Me) AndAlso result
        End If

        Return result
    End Function

    ReadOnly Property MethodOverride() As Mono.Cecil.MethodReference
        Get
            Return m_MethodOverrides
        End Get
    End Property

    Overridable Function ResolveOverrides() As Boolean
        Dim result As Boolean = True

        If Me.Modifiers.Is(ModifierMasks.Overrides) = False Then Return result

        Return result

        'Dim cache As MemberCacheEntry
        'Dim members As Generic.List(Of MemberInfo)
        'Dim member As MemberInfo
        'Dim Name As String
        'Dim params As ParameterInfo()

        'Dim phd As PropertyHandlerDeclaration = TryCast(Me, PropertyHandlerDeclaration)
        'If phd IsNot Nothing Then
        '    Name = phd.Parent.Name
        '    params = phd.Parent.Signature.Parameters.AsParameterInfo
        'Else
        '    params = Me.GetParameters
        '    Name = Me.Name
        'End If

        'cache = Compiler.TypeManager.GetCache(Compiler.TypeManager.GetRegisteredType(DeclaringType.BaseType)).LookupFlattened(Name)
        'If cache Is Nothing Then
        '    result = Compiler.Report.ShowMessage(Messages.VBNC30284, Me.Location, Me.Name) AndAlso result
        '    If result = False Then Return result
        'End If

        'members = cache.Members
        'member = Helper.ResolveGroupExact(Compiler, members, Helper.GetTypes(params))

        'If member Is Nothing Then
        '    result = Compiler.Report.ShowMessage(Messages.VBNC30284, Me.Location, Me.Name) AndAlso result
        '    If result = False Then Return result
        'End If

        'If member Is Nothing Then Return result

        'Dim methodI As MethodInfo
        'methodI = TryCast(member, MethodInfo)
        'If methodI IsNot Nothing Then
        '    If CBool(methodI.Attributes And Reflection.MethodAttributes.Abstract) Then
        '        m_MethodOverrides = methodI
        '    End If
        '    Return result
        'End If

        'Dim propI As PropertyInfo
        'propI = TryCast(member, PropertyInfo)
        'If propI IsNot Nothing Then
        '    If CBool(Helper.GetPropertyAttributes(propI) And Reflection.MethodAttributes.Abstract) Then
        '        If TypeOf Me Is PropertyGetDeclaration Then
        '            m_MethodOverrides = propI.GetGetMethod(True)
        '        ElseIf TypeOf Me Is PropertySetDeclaration Then
        '            m_MethodOverrides = propI.GetSetMethod(True)
        '        Else
        '            Throw New InternalException("?")
        '        End If
        '    End If
        '    Return result
        'End If

        'Helper.NotImplemented()

        'Return result
    End Function

    Overridable Function DefineOverrides() As Boolean
        Dim result As Boolean = True

        If m_MethodOverrides IsNot Nothing Then
            Throw New NotImplementedException
            'm_MethodOverrides = Helper.GetMethodOrMethodBuilder(Compiler, m_MethodOverrides)
            'DeclaringType.TypeBuilder.DefineMethodOverride(MethodBuilder, m_MethodOverrides)
        End If

        Return result
    End Function

    Private Function IsPropertyHandlesHandler() As Boolean
        Dim propD As PropertyDeclaration

        propD = TryCast(Parent, PropertyDeclaration)
        If propD Is Nothing Then Return False
        Return propD.HandlesField IsNot Nothing
    End Function

    Private Function GeneratePropertyHandlers() As Boolean
        Dim result As Boolean = True
        Dim propD As PropertyDeclaration

        propD = DirectCast(Parent, PropertyDeclaration)

        Dim isGet As Boolean
        Dim isSet As Boolean
        isGet = TypeOf Me Is PropertyGetDeclaration
        isSet = TypeOf Me Is PropertySetDeclaration

        Helper.Assert(isGet Xor isSet)

        Dim info As New EmitInfo(Me)
        Dim meType As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeBuilder(Compiler, Me.FindFirstParent(Of TypeDeclaration).CecilType)
        If isGet Then
            If Me.IsShared = False Then
                Emitter.EmitLoadMe(info, meType)
            End If
            Emitter.EmitLoadVariable(info, propD.HandlesField.FieldBuilder)
            Emitter.EmitRet(info)
        Else
            Dim endRemoveLabel As Label
            Dim endAddLabel As Label

            endRemoveLabel = Emitter.DefineLabel(info)
            endAddLabel = Emitter.DefineLabel(info)

            'Remove old handlers
            If Me.IsShared = False Then
                Emitter.EmitLoadMe(info, meType)
            End If
            Emitter.EmitLoadVariable(info, propD.HandlesField.FieldBuilder)
            Emitter.EmitBranchIfFalse(info, endRemoveLabel)
            For Each item As AddOrRemoveHandlerStatement In propD.Handlers
                result = item.GenerateCode(info, False) AndAlso result
            Next
            Emitter.MarkLabel(info, endRemoveLabel)


            'Store the variable
            If Me.IsShared = False Then
                Emitter.EmitLoadMe(info, meType)
            End If
            Emitter.EmitLoadParameter(info, Me.GetParameters()(0))
            Emitter.EmitStoreField(info, propD.HandlesField.FieldBuilder)

            'Add new handlers
            If Me.IsShared = False Then
                Emitter.EmitLoadMe(info, meType)
            End If
            Emitter.EmitLoadVariable(info, propD.HandlesField.FieldBuilder)
            Emitter.EmitBranchIfFalse(info, endAddLabel)
            For Each item As AddOrRemoveHandlerStatement In propD.Handlers
                result = item.GenerateCode(info, True) AndAlso result
            Next
            Emitter.MarkLabel(info, endAddLabel)

            Emitter.EmitRet(info)
        End If

        Return result
    End Function

End Class
