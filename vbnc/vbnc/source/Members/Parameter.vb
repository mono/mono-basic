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
''' Parameter            ::= [  Attributes  ]  ParameterModifier+  ParameterIdentifier  [  "As"  TypeName  ]  [  "="  ConstantExpression  ]
''' ParameterModifier    ::= "ByVal" | "ByRef" | "Optional" | "ParamArray"
''' ParameterIdentifier  ::= Identifier  [  ArrayNameModifier  ]
''' </summary>
''' <remarks></remarks>
Public Class Parameter
    Inherits ParsedObject
    Implements INameable, IModifiable, IAttributableDeclaration

    Private m_CustomAttributes As Attributes
    Private m_Modifiers As Modifiers
    Private m_ParameterIdentifier As ParameterIdentifier
    Private m_TypeName As TypeName
    Private m_ConstantExpression As Expression

    Private m_ParameterAttributes As Mono.Cecil.ParameterAttributes = Mono.Cecil.ParameterAttributes.None
    
    Private m_ParameterBuilderCecil As Mono.Cecil.ParameterDefinition
    Private m_ParamArrayAttribute As Mono.Cecil.CustomAttribute

    ReadOnly Property CecilBuilder() As Mono.Cecil.ParameterDefinition
        Get
            Return m_ParameterBuilderCecil
        End Get
    End Property

    Sub New(ByVal Parent As ParameterList)
        MyBase.New(Parent)
        UpdateDefinition()
    End Sub

    Sub New(ByVal Parent As ParameterList, ByVal Name As String, ByVal ParameterType As Mono.Cecil.TypeReference)
        MyBase.New(Parent)

        m_ParameterIdentifier = New ParameterIdentifier(Me, Name)
        Me.ParameterType = ParameterType

        UpdateDefinition()

        Helper.Assert(m_ParameterIdentifier IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParameterList, ByVal Name As String, ByVal ParameterType As TypeName)
        MyBase.New(Parent)

        m_ParameterIdentifier = New ParameterIdentifier(Me, Name)
        m_TypeName = ParameterType

        UpdateDefinition()

        Helper.Assert(m_ParameterIdentifier IsNot Nothing)
    End Sub

    ReadOnly Property ParameterIdentifier() As ParameterIdentifier
        Get
            Return m_ParameterIdentifier
        End Get
    End Property

    Shadows ReadOnly Property Parent() As ParameterList
        Get
            Return DirectCast(MyBase.Parent, ParameterList)
        End Get
    End Property

    Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal ParameterIdentifier As ParameterIdentifier, ByVal TypeName As TypeName, ByVal ConstantExpression As Expression)
        m_CustomAttributes = Attributes
        m_Modifiers = Modifiers
        m_ParameterIdentifier = ParameterIdentifier
        m_TypeName = TypeName
        m_ConstantExpression = ConstantExpression

        Helper.Assert(m_ParameterIdentifier IsNot Nothing)

        UpdateDefinition()
    End Sub

    Function Clone(Optional ByVal NewParent As ParameterList = Nothing) As Parameter
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParameterList)
        Dim result As New Parameter(NewParent)
        result.m_CustomAttributes = m_CustomAttributes
        result.m_Modifiers = m_Modifiers
        result.m_ParameterIdentifier = m_ParameterIdentifier.Clone(result)
        If m_TypeName IsNot Nothing Then result.m_TypeName = m_TypeName.Clone(result)
        If m_ConstantExpression IsNot Nothing Then result.m_ConstantExpression = m_ConstantExpression.Clone(result)
        result.UpdateDefinition()
        Return result
    End Function

    ReadOnly Property HasConstantValue() As Boolean
        Get
            Return m_ConstantExpression IsNot Nothing
        End Get
    End Property

    Property ConstantValue() As Object
        Get
            If m_ParameterBuilderCecil Is Nothing Then Return Nothing
            Return m_ParameterBuilderCecil.Constant
        End Get
        Set(ByVal value As Object)
            If TypeConverter.ConvertTo(Me, value, ParameterType, value) = False Then
                Throw New NotImplementedException
            End If
            If value Is DBNull.Value Then value = Nothing
            m_ParameterBuilderCecil.Constant = value
            UpdateDefinition()
        End Set
    End Property

    Property CustomAttributes() As Attributes Implements IAttributableDeclaration.CustomAttributes
        Get
            Return m_CustomAttributes
        End Get
        Set(ByVal value As Attributes)
            m_CustomAttributes = value
        End Set
    End Property

    Property ParameterAttributes() As Mono.Cecil.ParameterAttributes
        Get
            Return m_ParameterAttributes
        End Get
        Set(ByVal value As Mono.Cecil.ParameterAttributes)
            m_ParameterAttributes = value
            UpdateDefinition()
        End Set
    End Property

    ReadOnly Property Position() As Integer
        Get
            Return Me.FindFirstParent(Of ParameterList).List.IndexOf(Me) + 1
        End Get
    End Property

    Property ParameterType() As Mono.Cecil.TypeReference
        Get
            Return m_ParameterBuilderCecil.ParameterType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            If value.FullName.Contains("Void") Then Stop
            If m_ParameterBuilderCecil Is Nothing Then UpdateDefinition()
            m_ParameterBuilderCecil.ParameterType = Helper.GetTypeOrTypeReference(Compiler, value)
            UpdateDefinition()
        End Set
    End Property

    Public Property Name() As String Implements INameable.Name
        Get
            If m_ParameterIdentifier Is Nothing Then Return Nothing
            Return m_ParameterIdentifier.Name
        End Get
        Set(ByVal value As String)
            m_ParameterIdentifier = New ParameterIdentifier(Me, value)
            UpdateDefinition()
        End Set
    End Property

    Public Property Modifiers() As Modifiers Implements IModifiable.Modifiers
        Get
            Return m_Modifiers
        End Get
        Set(ByVal value As Modifiers)
            m_Modifiers = value
        End Set
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    <Obsolete("Call Define(Builder")> Public Overrides Function Define() As Boolean
        Throw New InternalException(Me)
    End Function

    Public Sub UpdateDefinition()
        Dim Parent As MethodBaseDeclaration = FindFirstParent(Of MethodBaseDeclaration)()
        Dim Builder As Mono.Cecil.MethodDefinition = Nothing

        If Parent IsNot Nothing Then
            Builder = Parent.CecilBuilder
        Else
            'Helper.StopIfDebugging()
        End If

        If m_ParameterBuilderCecil Is Nothing Then
            m_ParameterBuilderCecil = New Mono.Cecil.ParameterDefinition(Nothing)
            m_ParameterBuilderCecil.Sequence = -1
            m_ParameterBuilderCecil.Annotations.Add(Compiler, Me)
        End If

        m_ParameterBuilderCecil.Name = Name
        m_ParameterBuilderCecil.IsOptional = Modifiers.Is(ModifierMasks.Optional)

        If Builder IsNot Nothing AndAlso m_ParameterBuilderCecil.Sequence = -1 Then
            Builder.Parameters.Add(m_ParameterBuilderCecil)
        End If

        If Me.Modifiers.Is(ModifierMasks.ParamArray) AndAlso m_ParamArrayAttribute Is Nothing Then
            m_ParamArrayAttribute = New Mono.Cecil.CustomAttribute(Helper.GetMethodOrMethodReference(Compiler, Compiler.TypeCache.System_ParamArrayAttribute__ctor))
            m_ParameterBuilderCecil.CustomAttributes.Add(m_ParamArrayAttribute)
        End If

        If m_ParameterBuilderCecil.IsOptional Then
            'm_ParameterBuilderCecil.Constant = TypeConverter.ConvertTo(Compiler, m_ConstantValue, ParameterType)
            m_ParameterBuilderCecil.HasDefault = True
        End If
        If m_ParameterBuilderCecil.ParameterType Is Nothing Then
            m_ParameterBuilderCecil.ParameterType = Helper.GetTypeOrTypeReference(Compiler, Compiler.TypeCache.System_Void)
        End If
    End Sub

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveCode(Info) AndAlso result
        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveCode(Info) AndAlso result

        If m_ConstantExpression IsNot Nothing Then
            result = m_ConstantExpression.ResolveExpression(Info) AndAlso result
        End If

        If Me.Modifiers.Is(ModifierMasks.Optional) Then
            m_ParameterAttributes = Mono.Cecil.ParameterAttributes.Optional
            If m_ConstantExpression Is Nothing Then
                Helper.AddError(Me, "Optional parameters must have a constant expression.")
                result = False
            ElseIf m_ConstantExpression.IsConstant = False Then
                Helper.AddError(Me, "Optional expressions must be constant.")
                result = False
            Else
                Dim cv As Object = m_ConstantExpression.ConstantValue
                result = TypeConverter.ConvertTo(Me, cv, ParameterType, cv) AndAlso result
                If cv Is DBNull.Value Then
                    cv = Nothing
                End If
                ConstantValue = cv
            End If
        Else
            If m_ConstantExpression IsNot Nothing Then
                Helper.AddError(Me, "Non-optional parameters cannot have constant expressions.")
                result = False
            End If
        End If

        UpdateDefinition()

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_ConstantExpression, m_TypeName, m_ParameterIdentifier) AndAlso result
        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        'If ParameterType Is Nothing Then
        If m_TypeName IsNot Nothing Then
            ParameterType = m_TypeName.ResolvedType
            If m_ParameterIdentifier.ArrayNameModifier IsNot Nothing Then
                If m_TypeName.IsArrayTypeName Then
                    Helper.AddError(Me)
                Else
                    ParameterType = m_ParameterIdentifier.ArrayNameModifier.CreateArrayType(ParameterType)
                End If
            End If
        ElseIf m_ParameterIdentifier.Identifier.HasTypeCharacter Then
            ParameterType = TypeCharacters.TypeCharacterToType(Compiler, m_ParameterIdentifier.Identifier.TypeCharacter)
        ElseIf ParameterType Is Nothing OrElse Helper.CompareType(ParameterType, Compiler.TypeCache.System_Void) Then
            If Me.Location.File(Compiler).IsOptionStrictOn Then
                Helper.AddError(Me, "Parameter type must be specified.")
            Else
                Helper.AddWarning("Parameter type should be specified.")
            End If
            ParameterType = Compiler.TypeCache.System_Object
        End If
        'End If
        Helper.Assert(ParameterType IsNot Nothing)
        If m_Modifiers.Is(ModifierMasks.ByRef) Then
            ParameterType = Compiler.TypeManager.MakeByRefType(Me, ParameterType)
        End If

        UpdateDefinition()

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Me.CustomAttributes IsNot Nothing Then
            result = Me.CustomAttributes.GenerateCode(Info) AndAlso result
        End If

        Return result
    End Function

End Class
