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

    Private m_ConstantValue As Object
    Private m_ParameterType As Type
    Private m_ParameterAttributes As ParameterAttributes = ParameterAttributes.None
    Private m_ParameterBuilder As ParameterBuilder

    ReadOnly Property Identifier() As ParameterIdentifier
        Get
            Return m_ParameterIdentifier
        End Get
    End Property

    Sub New(ByVal Parent As ParameterList)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParameterList, ByVal Name As String, ByVal ParameterType As Type)
        MyBase.New(Parent)
        m_ParameterIdentifier = New ParameterIdentifier(Me, Name)
        m_ParameterType = ParameterType
        m_Modifiers = New Modifiers()

        'Helper.Assert(vbnc.Modifiers.IsNothing(m_Modifiers) = False)
        Helper.Assert(m_ParameterIdentifier IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParameterList, ByVal Name As String, ByVal ParameterType As TypeName)
        MyBase.New(Parent)
        m_ParameterIdentifier = New ParameterIdentifier(Me, Name)
        m_TypeName = ParameterType
        m_Modifiers = New Modifiers()

        'Helper.Assert(vbnc.Modifiers.IsNothing(m_Modifiers) = False)
        Helper.Assert(m_ParameterIdentifier IsNot Nothing)
    End Sub

    Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal ParameterIdentifier As ParameterIdentifier, ByVal TypeName As TypeName, ByVal ConstantExpression As Expression)
        m_CustomAttributes = Attributes
        m_Modifiers = Modifiers
        m_ParameterIdentifier = ParameterIdentifier
        m_TypeName = TypeName
        m_ConstantExpression = ConstantExpression

        'Helper.Assert(vbnc.Modifiers.IsNothing(m_Modifiers) = False)
        Helper.Assert(m_ParameterIdentifier IsNot Nothing)
        '    Helper.Assert(m_TypeName IsNot Nothing)
    End Sub

    Function Clone(Optional ByVal NewParent As ParameterList = Nothing) As Parameter
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParameterList)
        Dim result As New Parameter(NewParent)
        result.m_CustomAttributes = m_CustomAttributes
        result.m_Modifiers = m_Modifiers
        result.m_ParameterIdentifier = m_ParameterIdentifier.Clone(result)
        If m_TypeName IsNot Nothing Then result.m_TypeName = m_TypeName.Clone(result)
        If m_ConstantExpression IsNot Nothing Then result.m_ConstantExpression = m_ConstantExpression.clone(result)
        Return result
    End Function

    ReadOnly Property HasConstantValue() As Boolean
        Get
            Return m_ConstantExpression IsNot Nothing
        End Get
    End Property

    ReadOnly Property ConstantValue() As Object
        Get
            Return m_ConstantValue
        End Get
    End Property

    ReadOnly Property CustomAttributes() As Attributes Implements IAttributableDeclaration.CustomAttributes
        Get
            Return m_CustomAttributes
        End Get
    End Property

    Property ParameterAttributes() As ParameterAttributes
        Get
            Return m_ParameterAttributes
        End Get
        Set(ByVal value As ParameterAttributes)
            m_ParameterAttributes = value
        End Set
    End Property


    ReadOnly Property Position() As Integer
        Get
            Return Me.FindFirstParent(Of ParameterList).List.IndexOf(Me) + 1
        End Get
    End Property

    ReadOnly Property ParameterType() As Type
        Get
            Helper.Assert(m_ParameterType IsNot Nothing)
            Return m_ParameterType
        End Get
    End Property

    ReadOnly Property ParameterBuilder() As ParameterBuilder
        Get
            Return m_ParameterBuilder
        End Get
    End Property

    Public Property Name() As String
        Get
            Return m_ParameterIdentifier.Name
        End Get
        Set(ByVal value As String)
            m_ParameterIdentifier = New ParameterIdentifier(Me, value)
        End Set
    End Property

    Public ReadOnly Property Modifiers() As Modifiers Implements IModifiable.Modifiers
        Get
            Return m_Modifiers
        End Get
    End Property

    Private ReadOnly Property ImplName() As String Implements INameable.Name
        Get
            Return Name
        End Get
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_TypeName
        End Get
    End Property

    <Obsolete("Call Define(Builder")> Public Overrides Function Define() As Boolean
        Throw New InternalException(Me)
    End Function

    Private Function DefineInternal() As Boolean
        Dim result As Boolean = True

        If m_ParameterBuilder.IsOptional Then
            If Helper.IsOnMS AndAlso ((Me.ParameterType.IsByRef AndAlso m_ConstantValue IsNot Nothing) OrElse (m_ParameterType.Equals(Compiler.TypeCache.System_Object) AndAlso m_ConstantValue IsNot Nothing AndAlso m_ConstantValue.GetType.Equals(Compiler.TypeCache.System_Object) = False)) Then
                'HACK (a really big one...)
                'The reflection.Emit is not able to set a constant value when the 
                'parameter type is a byref type. Do all this to bypass all checks.
                Dim mthd As MethodInfo
                mthd = GetType(TypeBuilder).GetMethod("InternalSetConstantValue", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static)

                Dim m As System.Reflection.Module = DirectCast(GetType(ParameterBuilder).GetField("m_methodBuilder", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance).GetValue(m_ParameterBuilder), MethodBuilder).GetModule
                Dim tk As Integer = DirectCast(GetType(ParameterBuilder).GetField("m_pdToken", BindingFlags.NonPublic Or BindingFlags.Instance).GetValue(m_ParameterBuilder), ParameterToken).Token
                Dim vt As Type = GetType(String).Assembly.GetType("System.Variant")
                Dim ctor As ConstructorInfo
                ctor = vt.GetConstructor(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.ExactBinding, Nothing, New Type() {Compiler.TypeCache.System_Object}, Nothing)

                Dim v As Object = Activator.CreateInstance(vt, New Object() {m_ConstantValue})
                mthd.Invoke(Nothing, New Object() {m, tk, v})
                'END HACK
            Else
                'Helper.Assert(m_ConstantValue Is Nothing OrElse (Type.GetTypeCode(m_ConstantValue.GetType) = Type.GetTypeCode(m_ParameterType)) OrElse m_ParameterType.IsEnum)
                m_ParameterBuilder.SetConstant(m_ConstantValue)
            End If
        End If

        If Me.Modifiers.Is(ModifierMasks.ParamArray) Then
            Dim cab As CustomAttributeBuilder
            cab = New CustomAttributeBuilder(Compiler.TypeCache.System_ParamArrayAttribute__ctor, New Object() {})
            m_ParameterBuilder.SetCustomAttribute(cab)
        End If


        Return result
    End Function


    Overloads Function Define(ByVal Builder As ConstructorBuilder) As Boolean
        m_ParameterBuilder = Builder.DefineParameter(Position, m_ParameterAttributes, Name)
        Return DefineInternal()
    End Function

    Overloads Function Define(ByVal Builder As MethodBuilder) As Boolean
        m_ParameterBuilder = Builder.DefineParameter(Position, m_ParameterAttributes, Name)
        Return DefineInternal()
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Me.CheckCodeNotResolved()

        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveCode(info) AndAlso result

        If m_ConstantExpression IsNot Nothing Then
            result = m_ConstantExpression.ResolveExpression(info) AndAlso result
        End If

        If Me.Modifiers.Is(ModifierMasks.Optional) Then
            m_ParameterAttributes = Reflection.ParameterAttributes.Optional
            If m_ConstantExpression Is Nothing Then
                Helper.AddError(Me, "Optional parameters must have a constant expression.")
                result = False
            ElseIf m_ConstantExpression.IsConstant = False Then
                Helper.AddError(Me, "Optional expressions must be constant.")
                result = False
            Else
                m_ConstantValue = m_ConstantExpression.ConstantValue
                result = TypeConverter.ConvertTo(Me, m_ConstantValue, m_ParameterType, m_ConstantValue) AndAlso result
                If m_ConstantValue Is DBNull.Value Then
                    m_ConstantValue = Nothing
                End If
            End If
        Else
            If m_ConstantExpression IsNot Nothing Then
                Helper.AddError(Me, "Non-optional parameters cannot have constant expressions.")
                result = False
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Me.CheckTypeReferencesNotResolved()

        result = Helper.ResolveTypeReferences(m_ConstantExpression, m_TypeName, m_ParameterIdentifier) AndAlso result
        If m_CustomAttributes IsNot Nothing Then result = m_CustomAttributes.ResolveTypeReferences AndAlso result

        If result = False Then Return result

        If m_ParameterType Is Nothing Then
            If m_TypeName IsNot Nothing Then
                m_ParameterType = m_TypeName.ResolvedType
                If m_ParameterIdentifier.ArrayNameModifier IsNot Nothing Then
                    If m_TypeName.IsArrayTypeName Then
                        Helper.AddError(Me)
                    Else
                        m_ParameterType = m_ParameterIdentifier.ArrayNameModifier.CreateArrayType(m_ParameterType)
                    End If
                End If
            ElseIf m_ParameterIdentifier.Identifier.HasTypeCharacter Then
                m_ParameterType = TypeCharacters.TypeCharacterToType(Compiler, m_ParameterIdentifier.Identifier.TypeCharacter)
            Else
                If Me.Location.File(Compiler).IsOptionStrictOn Then
                    Helper.AddError(Me, "Parameter type must be specified.")
                Else
                    Helper.AddWarning("Parameter type should be specified.")
                End If
                m_ParameterType = Compiler.TypeCache.System_Object
            End If
        End If
        Helper.Assert(m_ParameterType IsNot Nothing)
        If m_Modifiers.Is(ModifierMasks.ByRef) Then
            m_ParameterType = Compiler.TypeManager.MakeByRefType(Me, m_ParameterType)
        End If

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
