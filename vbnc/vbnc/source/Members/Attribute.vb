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
''' Attribute          ::= [  AttributeModifier  ":"  ]  SimpleTypeName  [  "("  [  AttributeArguments  ]  ")"  ]
''' AttributeModifier  ::=  "Assembly" | "Module"
''' </summary>
''' <remarks></remarks>
Public Class Attribute
    Inherits ParsedObject

    Private m_IsAssembly As Boolean
    Private m_IsModule As Boolean
    Private m_SimpleTypeName As SimpleTypeName
    Private m_AttributeArguments As AttributeArguments

    Private m_ResolvedType As Type
    Private m_ResolvedTypeConstructor As ConstructorInfo

    Private m_Arguments As Object()
    Private m_Fields As Generic.List(Of FieldInfo)
    Private m_FieldValues As Generic.List(Of Object)
    Private m_Properties As Generic.List(Of PropertyInfo)
    Private m_PropertyValues As Generic.List(Of Object)
    Private m_IsResolved As Boolean

    Private m_Instance As System.Attribute

    ''' <summary>
    ''' Returns the specified argument, or nothing if index is out of range
    ''' </summary>
    ''' <param name="Index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetArgument(ByVal Index As Integer) As Object
        If m_Arguments IsNot Nothing AndAlso Index < m_Arguments.Length Then
            Return m_Arguments(Index)
        Else
            Return Nothing
        End If
    End Function

    ReadOnly Property Arguments() As Object()
        Get
            Return m_Arguments
        End Get
    End Property

    ReadOnly Property IsAssembly() As Boolean
        Get
            Return m_IsAssembly
        End Get
    End Property

    ReadOnly Property IsModule() As Boolean
        Get
            Return m_IsModule
        End Get
    End Property

    ReadOnly Property SimpleTypeName() As SimpleTypeName
        Get
            Return m_SimpleTypeName
        End Get
    End Property

    Property ResolvedType() As Type
        Get
            Return m_ResolvedType
        End Get
        Set(ByVal value As Type)
            m_ResolvedType = value
        End Set
    End Property

    ReadOnly Property AttributeArguments() As AttributeArguments
        Get
            If m_AttributeArguments Is Nothing Then
                m_AttributeArguments = New AttributeArguments(Me)
            End If
            Return m_AttributeArguments
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Public Sub New(ByVal Parent As ParsedObject, ByVal Type As Type, ByVal ParamArray Arguments() As Object)
        MyBase.New(Parent)
        m_ResolvedType = Type
        m_Arguments = Arguments
    End Sub

    Sub Init(ByVal IsAssembly As Boolean, ByVal IsModule As Boolean, ByVal SimpleTypeName As SimpleTypeName, ByVal AttributeArguments As AttributeArguments)
        m_IsAssembly = IsAssembly
        m_IsModule = IsModule
        m_SimpleTypeName = SimpleTypeName
        m_AttributeArguments = AttributeArguments
    End Sub

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Attribute
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParsedObject)
        Dim result As New Attribute(NewParent)
        result.m_IsAssembly = m_IsAssembly
        result.m_IsModule = m_IsModule
        If m_SimpleTypeName IsNot Nothing Then result.m_SimpleTypeName = m_SimpleTypeName.Clone(result)
        If m_AttributeArguments IsNot Nothing Then result.m_AttributeArguments = m_AttributeArguments.Clone(result)
        result.m_ResolvedType = m_ResolvedType
        result.m_ResolvedTypeConstructor = m_ResolvedTypeConstructor

        Return result
    End Function

    ReadOnly Property AttributeInstance() As System.Attribute
        Get
            If m_Instance Is Nothing Then
                m_Instance = CType(Activator.CreateInstance(m_ResolvedType, BindingFlags.CreateInstance, Nothing, m_Arguments, Nothing), System.Attribute)
            End If
            Return m_Instance
        End Get
    End Property

    ReadOnly Property AttributeType() As Type
        Get
            If m_ResolvedType Is Nothing Then Throw New InternalException(Me)
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ResolvedType Is Nothing Then
            Helper.Assert(m_SimpleTypeName IsNot Nothing, "SimpleTypeName Is Nothing: " & Me.Location.ToString(Compiler))
            result = m_SimpleTypeName.ResolveTypeReferences(True) AndAlso result
            m_ResolvedType = m_SimpleTypeName.ResolvedType
        End If
        result = m_ResolvedType IsNot Nothing AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim argList As ArgumentList

        If m_IsResolved Then Return result

        If m_AttributeArguments IsNot Nothing Then
            Helper.Assert(m_Arguments Is Nothing)
            Helper.Assert(m_Fields Is Nothing)
            Helper.Assert(m_FieldValues Is Nothing)
            Helper.Assert(m_Properties Is Nothing)
            Helper.Assert(m_PropertyValues Is Nothing)

            result = m_AttributeArguments.ResolveCode(Info) AndAlso result

            If m_AttributeArguments.PositionalArgumentList IsNot Nothing Then
                argList = New ArgumentList(Me, m_AttributeArguments.PositionalArgumentList.AsExpressions)
            Else
                argList = New ArgumentList(Me)
            End If

            If m_AttributeArguments.VariablePropertyInitializerList IsNot Nothing Then
                Dim cache As MemberCache

                cache = Info.Compiler.TypeManager.GetCache(m_ResolvedType)

                For Each item As VariablePropertyInitializer In m_AttributeArguments.VariablePropertyInitializerList
                    Dim name As String
                    Dim member As MemberInfo
                    Dim members As Generic.List(Of MemberInfo)

                    name = item.Identifier
                    members = cache.LookupFlattenedMembers(name)
                    members = Helper.FilterExternalInaccessible(Info.Compiler, members)
                    If members.Count <> 1 Then
                        If members(0) Is members(1) Then
                            Console.WriteLine("They are the same!")
                        End If
                        For Each m As MemberInfo In members
                            Console.WriteLine(m.DeclaringType.FullName & ":" & m.Name)
                        Next
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                        '                        Helper.NotImplemented(String.Format("Property resolution for attribute arguments ({0} members named '{1}' in {2})" & Me.Location.AsString, members.Count, name, m_ResolvedType.FullName))
                    End If
                    member = members(0)
                    Select Case member.MemberType
                        Case MemberTypes.Field
                            Dim field As FieldInfo
                            field = DirectCast(member, FieldInfo)

                            If m_Fields Is Nothing Then m_Fields = New Generic.List(Of FieldInfo)
                            If m_FieldValues Is Nothing Then m_FieldValues = New Generic.List(Of Object)
                            m_Fields.Add(field)
                            m_FieldValues.Add(item.AttributeArgumentExpression.Expression.ConstantValue)
                        Case MemberTypes.Property
                            Dim prop As PropertyInfo
                            prop = DirectCast(member, PropertyInfo)
                            If m_Properties Is Nothing Then m_Properties = New Generic.List(Of PropertyInfo)
                            If m_PropertyValues Is Nothing Then m_PropertyValues = New Generic.List(Of Object)
                            m_Properties.Add(prop)
                            m_PropertyValues.Add(item.AttributeArgumentExpression.Expression.ConstantValue)
                            'm_PropertyValues.add(item.
                        Case Else
                            Helper.AddError(Me, "Invalid member type for attribute value: " & member.MemberType.ToString)
                    End Select

                Next
            End If

        ElseIf m_Arguments IsNot Nothing Then
            argList = New ArgumentList(Me)
            For i As Integer = 0 To m_Arguments.Length - 1
                argList.Arguments.Add(New PositionalArgument(argList, argList.Count, New ConstantExpression(argList, m_Arguments(i), m_Arguments(i).GetType)))
            Next
        Else
            argList = New ArgumentList(Me)
        End If

        If m_Arguments Is Nothing Then m_Arguments = New Object() {}
        If m_Fields Is Nothing Then m_Fields = New Generic.List(Of FieldInfo)
        If m_FieldValues Is Nothing Then m_FieldValues = New Generic.List(Of Object)
        If m_Properties Is Nothing Then m_Properties = New Generic.List(Of PropertyInfo)
        If m_PropertyValues Is Nothing Then m_PropertyValues = New Generic.List(Of Object)

        Dim ctors As ConstructorInfo()
        ctors = Compiler.Helper.GetConstructors(m_ResolvedType)

        Dim groupClassification As New MethodGroupClassification(Me, Nothing, Nothing, ctors)
        result = groupClassification.ResolveGroup(argList, Nothing) AndAlso result
        m_ResolvedTypeConstructor = groupClassification.ResolvedConstructor
        result = m_ResolvedTypeConstructor IsNot Nothing AndAlso result
        result = argList.FillWithOptionalParameters(m_ResolvedTypeConstructor) AndAlso result

        ReDim m_Arguments(argList.Count - 1)
        For i As Integer = 0 To m_Arguments.Length - 1
            m_Arguments(i) = argList(i).Expression.ConstantValue
            If TypeOf m_Arguments(i) Is DBNull Then
                m_Arguments(i) = Nothing
            End If
        Next

        m_IsResolved = result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_IsAssembly Then
            Dim builder As CustomAttributeBuilder
            builder = GetAttributeBuilder()
            Me.Compiler.AssemblyBuilder.SetCustomAttribute(builder)
        ElseIf m_IsModule Then
            Dim builder As CustomAttributeBuilder
            builder = GetAttributeBuilder()
            Me.Compiler.ModuleBuilder.SetCustomAttribute(builder)
        Else
            Dim memberparent As IAttributableDeclaration = Me.FindFirstParent(Of IAttributableDeclaration)()
            If memberparent IsNot Nothing Then
                Dim tp As TypeDeclaration = TryCast(memberparent, TypeDeclaration)
                Dim mthd As IMethod = TryCast(memberparent, IMethod)
                Dim ctro As IConstructorMember = TryCast(memberparent, IConstructorMember)
                Dim fld As IFieldMember = TryCast(memberparent, IFieldMember)
                Dim prop As PropertyDeclaration = TryCast(memberparent, PropertyDeclaration)
                Dim param As Parameter = TryCast(memberparent, Parameter)

                If ctro IsNot Nothing Then mthd = Nothing
                Helper.Assert(tp IsNot Nothing Xor mthd IsNot Nothing Xor ctro IsNot Nothing Xor fld IsNot Nothing Xor prop IsNot Nothing Xor param IsNot Nothing)

                If tp IsNot Nothing Then
                    If tp.TypeBuilder Is Nothing Then
                        tp.EnumBuilder.SetCustomAttribute(GetAttributeBuilder)
                    Else
                        tp.TypeBuilder.SetCustomAttribute(GetAttributeBuilder)
                    End If
                ElseIf mthd IsNot Nothing Then
                    mthd.MethodBuilder.SetCustomAttribute(GetAttributeBuilder)
                ElseIf ctro IsNot Nothing Then
                    ctro.ConstructorBuilder.SetCustomAttribute(GetAttributeBuilder)
                ElseIf fld IsNot Nothing Then
                    fld.FieldBuilder.SetCustomAttribute(GetAttributeBuilder)
                ElseIf prop IsNot Nothing Then
                    prop.PropertyBuilder.SetCustomAttribute(GetAttributeBuilder)
                ElseIf param IsNot Nothing Then
                    param.ParameterBuilder.SetCustomAttribute(GetAttributeBuilder)
                Else
                    Throw New InternalException(Me)
                End If
            Else
                Throw New InternalException(Me)
            End If
        End If

        Return result
    End Function

    Private Function GetAttributeBuilder() As CustomAttributeBuilder
        Dim result As CustomAttributeBuilder

        Helper.Assert(m_ResolvedTypeConstructor IsNot Nothing)
        Helper.Assert(m_Arguments IsNot Nothing)
        Helper.Assert(Helper.GetParameters(Me, m_ResolvedTypeConstructor).Length = m_Arguments.Length)
        Helper.Assert(m_Properties IsNot Nothing AndAlso m_PropertyValues IsNot Nothing AndAlso m_Properties.Count = m_PropertyValues.Count)
        Helper.Assert(m_Fields IsNot Nothing AndAlso m_FieldValues IsNot Nothing AndAlso m_Fields.Count = m_FieldValues.Count)

        m_ResolvedTypeConstructor = Helper.GetCtorOrCtorBuilder(m_ResolvedTypeConstructor)
        Helper.GetFieldOrFieldBuilder(m_Fields)
        Helper.GetPropertyOrPropertyBuilder(m_Properties)

        For i As Integer = 0 To m_Arguments.Length - 1
            Dim type As Type
            type = TryCast(m_Arguments(i), Type)
            If type IsNot Nothing Then
                m_Arguments(i) = Helper.GetTypeOrTypeBuilder(type)
            End If
        Next

        Try
            result = New CustomAttributeBuilder(m_ResolvedTypeConstructor, m_Arguments, m_Properties.ToArray, m_PropertyValues.ToArray, m_Fields.ToArray, m_FieldValues.ToArray)
        Catch ex As Exception
            Throw
        End Try

        Return result
    End Function

End Class
