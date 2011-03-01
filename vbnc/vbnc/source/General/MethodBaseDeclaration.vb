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

Imports System.Security
Imports System.Security.Permissions

Public MustInherit Class MethodBaseDeclaration
    Inherits MemberDeclaration
    Implements IMethod

    Private m_Signature As SubSignature
    Private m_Code As CodeBlock

    Private m_DefaultReturnVariable As Mono.Cecil.Cil.VariableDefinition

    Private m_MethodOverrides As Mono.Cecil.MethodReference
    Private m_CecilBuilder As Mono.Cecil.MethodDefinition

    Private m_HasSecurityCustomAttribute As Nullable(Of Boolean)
    Private m_DefinedSecurityDeclarations As Boolean

    Protected Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As EventDeclaration)
        MyBase.new(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature)
        MyBase.Init(Modifiers, Signature.Name)
        m_Signature = Signature
        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Code As CodeBlock)
        Me.Init(Modifiers, Signature)
        m_Code = Code
        Helper.Assert(m_Signature IsNot Nothing)
    End Sub

    Function DefineOptionalParameters() As Boolean
        Dim result As Boolean = True

        result = m_Signature.Parameters.DefineOptionalParameters AndAlso result

        Return result
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        Helper.Assert(m_CecilBuilder Is Nothing)
        m_CecilBuilder = New Mono.Cecil.MethodDefinition(Name, 0, Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void))
        m_CecilBuilder.Annotations.Add(Compiler, Me)
        m_CecilBuilder.Name = Name
        m_CecilBuilder.HasThis = Not Me.IsShared
        m_CecilBuilder.DeclaringType = DeclaringType.CecilType

        DeclaringType.CecilType.Methods.Add(CecilBuilder)

        If m_Signature IsNot Nothing Then result = m_Signature.CreateDefinition AndAlso result

        MethodAttributes = Helper.GetAttributes(Me)

        Return result
    End Function

    Public ReadOnly Property IsExternalDeclaration() As Boolean
        Get
            If TypeOf Me Is ExternalSubDeclaration Then Return True
            If CustomAttributes Is Nothing Then Return False
            For i As Integer = 0 To CustomAttributes.Count - 1
                If CustomAttributes(i).ResolvedType Is Nothing Then Continue For
                If Helper.CompareType(CustomAttributes(i).ResolvedType, Compiler.TypeCache.System_Runtime_InteropServices_DllImportAttribute) Then Return True
            Next
            Return False
        End Get
    End Property

    Public Function DefineSecurityDeclarations() As Boolean
        Dim result As Boolean = True
        Dim checkedAll As Boolean = True

        If m_DefinedSecurityDeclarations Then Return True

        If CustomAttributes Is Nothing Then Return True

        For i As Integer = 0 To CustomAttributes.Count - 1
            If CustomAttributes(i).ResolvedType Is Nothing Then
                checkedAll = False
                Exit For
            End If
        Next

        If Not checkedAll Then Return True

        For i As Integer = CustomAttributes.Count - 1 To 0 Step -1
            Dim attrib As Attribute = CustomAttributes(i)

            If Not Helper.IsSubclassOf(Compiler.TypeCache.System_Security_Permissions_SecurityAttribute, attrib.ResolvedType) Then Continue For

            Try
                Dim sec As Mono.Cecil.SecurityDeclaration
                Dim secAtt As Mono.Cecil.SecurityAttribute
                Dim attribInstantiation As Object = Nothing
                Dim attribInstance As SecurityAttribute
                Dim attribAction As Mono.Cecil.SecurityAction
                Dim attribPermissionSetAttribute As PermissionSetAttribute

                If attrib.Instantiate(Messages.VBNC30128, attribInstantiation) = False Then
                    'Attribute.Instantiate prints an error message
                    result = False
                    Continue For
                End If

                attribInstance = TryCast(attribInstantiation, SecurityAttribute)
                If attribInstance Is Nothing Then
                    Compiler.Report.ShowMessage(Messages.VBNC30128, attrib.Location, "Security attribute does not inherit from System.Security.Permissions.SecurityAttribute")
                    result = False
                    Continue For
                End If

                attribAction = CType(attribInstance.Action, Mono.Cecil.SecurityAction)
                attribPermissionSetAttribute = TryCast(attribInstance, PermissionSetAttribute)

                sec = New Mono.Cecil.SecurityDeclaration(attribAction)
                secAtt = attrib.GetSecurityAttribute()
                sec.SecurityAttributes.Add(secAtt)
                CecilBuilder.SecurityDeclarations.Add(sec)
                CustomAttributes.Remove(attrib)
                CecilBuilder.CustomAttributes.Remove(attrib.CecilBuilder)

                Me.MethodAttributes = Mono.Cecil.MethodAttributes.HasSecurity
            Catch ex As Exception
                Compiler.Report.ShowMessage(Messages.VBNC30128, attrib.Location, attrib.AttributeType.Name, ex.Message)
                result = False
            End Try

        Next

        m_DefinedSecurityDeclarations = True

        Return result
    End Function

    ReadOnly Property HasSecurityCustomAttribute() As Boolean
        Get
            Dim checkedAll As Boolean = True

            If CustomAttributes Is Nothing Then Return False

            If m_HasSecurityCustomAttribute.HasValue Then Return m_HasSecurityCustomAttribute.Value

            For i As Integer = 0 To CustomAttributes.Count - 1
                If CustomAttributes(i).ResolvedType Is Nothing Then
                    checkedAll = False
                ElseIf Helper.IsSubclassOf(Compiler.TypeCache.System_Security_Permissions_SecurityAttribute, CustomAttributes(i).ResolvedType) Then
                    m_HasSecurityCustomAttribute = True
                    Return True
                End If
            Next

            If checkedAll Then m_HasSecurityCustomAttribute = False

            Return False
        End Get
    End Property

    Property ReturnType() As Mono.Cecil.TypeReference
        Get
            Return m_CecilBuilder.ReturnType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            m_CecilBuilder.ReturnType = Helper.GetTypeOrTypeReference(Compiler, value)
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

        ReturnType = m_Signature.ReturnType

        If m_Code IsNot Nothing Then result = m_Code.ResolveTypeReferences AndAlso result

        If IsExternalDeclaration Then
            MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL Or Mono.Cecil.MethodImplAttributes.PreserveSig
        Else
            MethodImplAttributes = Mono.Cecil.MethodImplAttributes.IL
        End If

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

        Return result
    End Function

    Overridable Function DefineOverrides() As Boolean
        Dim result As Boolean = True

        If m_MethodOverrides IsNot Nothing Then
            Throw New NotImplementedException
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
