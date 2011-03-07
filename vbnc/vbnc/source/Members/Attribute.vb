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

    Private m_ResolvedType As Mono.Cecil.TypeReference
    Private m_ResolvedTypeConstructor As Mono.Cecil.MethodReference

    Private m_Arguments As Object()
    Private m_Fields As Generic.List(Of Mono.Cecil.FieldReference)
    Private m_FieldValues As Generic.List(Of Object)
    Private m_Properties As Generic.List(Of Mono.Cecil.PropertyReference)
    Private m_PropertyValues As Generic.List(Of Object)
    Private m_IsResolved As Boolean

    Private m_Instance As System.Attribute
    Private m_CecilBuilder As Mono.Cecil.CustomAttribute

    ReadOnly Property CecilBuilder() As Mono.Cecil.CustomAttribute
        Get
            Return m_CecilBuilder
        End Get
    End Property

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

    Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
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

    Public Sub New(ByVal Parent As ParsedObject, ByVal Type As Mono.Cecil.TypeReference, ByVal ParamArray Arguments() As Object)
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
        If m_SimpleTypeName IsNot Nothing Then result.m_SimpleTypeName = m_SimpleTypeName
        If m_AttributeArguments IsNot Nothing Then result.m_AttributeArguments = m_AttributeArguments
        result.m_ResolvedType = m_ResolvedType
        result.m_ResolvedTypeConstructor = m_ResolvedTypeConstructor

        Return result
    End Function

    Private Function ConvertArgument(ByVal constant As Object, ByVal Type As Mono.Cecil.TypeReference) As Object
        If Helper.IsEnum(Compiler, Type) = False Then Return constant

        Dim enumCecilAssembly As Mono.Cecil.AssemblyNameReference = CecilHelper.GetAssemblyRef(Type)
        Dim enumAssembly As Assembly = System.Reflection.Assembly.Load(enumCecilAssembly.FullName)
        Dim enumType As Type = enumAssembly.GetType(Type.FullName)

        If enumType.IsEnum = False Then Return constant

        Return System.Enum.ToObject(enumType, constant)
    End Function

    Function Instantiate(ByVal errorNumber As Messages, ByRef instantiation As Object) As Boolean
        Dim attribModule As Mono.Cecil.ModuleDefinition = TryCast(ResolvedType.Scope, Mono.Cecil.ModuleDefinition)
        Dim attribAssembly As Assembly
        Dim attribType As Type
        Dim attribInstance As Object

        If attribModule Is Nothing Then
            Return Compiler.Report.ShowMessage(errorNumber, Location, "The attribute isn't defined in an assembly.")
        End If

        attribAssembly = System.Reflection.Assembly.Load(attribModule.Assembly.Name.FullName)

        If attribAssembly Is Nothing Then
            Return Compiler.Report.ShowMessage(errorNumber, Location, String.Format("Could not load the assembly '{0}' where this attribute is stored.", attribModule.Assembly.Name.FullName))
        End If

        attribType = attribAssembly.GetType(ResolvedType.FullName, False, False)

        If attribType Is Nothing Then
            Return Compiler.Report.ShowMessage(errorNumber, Location, String.Format("Could not load the type '{0}' from the assembly '{1}'.", ResolvedType.FullName, attribAssembly.FullName))
        End If

        Dim args As Object()
        ReDim args(Me.m_Arguments.Length)
        ReDim args(Me.m_Arguments.Length - 1)
        For i As Integer = 0 To m_Arguments.Length - 1
            args(i) = ConvertArgument(m_Arguments(i), m_ResolvedTypeConstructor.Parameters(i).ParameterType)
        Next
        attribInstance = Activator.CreateInstance(attribType, args)

        For i As Integer = 0 To m_Fields.Count - 1
            Dim fieldInfo As FieldInfo
            fieldInfo = attribType.GetField(m_Fields(i).Name, BindingFlags.Instance Or BindingFlags.Public)
            If fieldInfo Is Nothing Then
                Return Compiler.Report.ShowMessage(errorNumber, Location, String.Format("Could not find the field '{0}' on the type '{1}'.", m_Fields(i).Name, attribType.FullName))
            End If

            fieldInfo.SetValue(attribInstance, m_FieldValues(i))
        Next

        For i As Integer = 0 To m_Properties.Count - 1
            Dim propInfo As PropertyInfo
            propInfo = attribType.GetProperty(m_Properties(i).Name, Type.EmptyTypes)
            If propInfo Is Nothing Then
                Return Compiler.Report.ShowMessage(errorNumber, Location, String.Format("Could not find the property '{0}' on the type '{1}'.", m_Properties(i).Name, attribType.FullName))
            End If
            If propInfo.CanWrite = False Then
                Return Compiler.Report.ShowMessage(errorNumber, Location, String.Format("The property '{0}' on the type '{1}' is ReadOnly.", propInfo.Name, attribType.FullName))
            End If
            propInfo.SetValue(attribInstance, m_PropertyValues(i), Nothing)
        Next

        instantiation = attribInstance
        Return True
    End Function

    ReadOnly Property AttributeType() As Mono.Cecil.TypeReference
        Get
            ' If m_ResolvedType Is Nothing Then Throw New InternalException(Me)
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
                    Dim member As Mono.Cecil.MemberReference
                    Dim members As Mono.Collections.Generic.Collection(Of Mono.Cecil.MemberReference)
                    Dim constant As Object = Nothing

                    name = item.Identifier
                    members = cache.LookupFlattenedMembers(name)
                    members = Helper.FilterExternalInaccessible(Info.Compiler, members)
                    If members.Count <> 1 Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    End If
                    member = members(0)

                    If Not item.AttributeArgumentExpression.Expression.GetConstant(constant, True) Then Return False

                    If TypeOf member Is Mono.Cecil.FieldReference Then
                        Dim field As Mono.Cecil.FieldReference
                        field = DirectCast(member, Mono.Cecil.FieldReference)

                        If m_Fields Is Nothing Then m_Fields = New Generic.List(Of Mono.Cecil.FieldReference)
                        If m_FieldValues Is Nothing Then m_FieldValues = New Generic.List(Of Object)
                        m_Fields.Add(field)
                        m_FieldValues.Add(constant)
                    ElseIf TypeOf member Is Mono.Cecil.PropertyReference Then
                        Dim prop As Mono.Cecil.PropertyReference
                        prop = DirectCast(member, Mono.Cecil.PropertyReference)
                        If m_Properties Is Nothing Then m_Properties = New Generic.List(Of Mono.Cecil.PropertyReference)
                        If m_PropertyValues Is Nothing Then m_PropertyValues = New Generic.List(Of Object)
                        m_Properties.Add(prop)
                        m_PropertyValues.Add(constant)
                        'm_PropertyValues.add(item.
                    Else
                        Helper.AddError(Me, "Invalid member type for attribute value.")
                    End If
                Next
            End If

        ElseIf m_Arguments IsNot Nothing Then
            argList = New ArgumentList(Me)
            For i As Integer = 0 To m_Arguments.Length - 1
                argList.Arguments.Add(New PositionalArgument(argList, argList.Count, New ConstantExpression(argList, m_Arguments(i), CecilHelper.GetType(Compiler, m_Arguments(i)))))
            Next
        Else
            argList = New ArgumentList(Me)
        End If

        If m_Arguments Is Nothing Then m_Arguments = New Object() {}
        If m_Fields Is Nothing Then m_Fields = New Generic.List(Of Mono.Cecil.FieldReference)
        If m_FieldValues Is Nothing Then m_FieldValues = New Generic.List(Of Object)
        If m_Properties Is Nothing Then m_Properties = New Generic.List(Of Mono.Cecil.PropertyReference)
        If m_PropertyValues Is Nothing Then m_PropertyValues = New Generic.List(Of Object)

        Dim ctors As Mono.Collections.Generic.Collection(Of MethodReference)
        Dim parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        ctors = CecilHelper.GetConstructors(m_ResolvedType)

        Dim groupClassification As New MethodGroupClassification(Me, Nothing, Nothing, Nothing, ctors)
        result = groupClassification.ResolveGroup(argList) AndAlso result
        If result = False Then
            groupClassification.ResolveGroup(argList, True)
            Return result
        End If
        m_ResolvedTypeConstructor = groupClassification.ResolvedConstructor
        result = m_ResolvedTypeConstructor IsNot Nothing AndAlso result
        result = argList.FillWithOptionalParameters(m_ResolvedTypeConstructor) AndAlso result

        parameters = Helper.GetParameters(Me, m_ResolvedTypeConstructor)

        ReDim m_Arguments(argList.Count - 1)
        For i As Integer = 0 To m_Arguments.Length - 1
            Dim constant As Object = Nothing
            If argList(i).Expression.GetConstant(constant, True) = False Then Return False
            m_Arguments(i) = constant
            If TypeOf m_Arguments(i) Is DBNull Then
                m_Arguments(i) = Nothing
            End If
        Next

        m_IsResolved = result

        If result Then
            For i As Integer = 0 To m_Arguments.Length - 1
                Dim value As Object = Nothing
                If TypeOf m_Arguments(i) Is TypeReference Then Continue For
                result = TypeConverter.ConvertTo(Me, m_Arguments(i), parameters(i).ParameterType, value, True)
                If result Then m_Arguments(i) = value
            Next
            For i As Integer = 0 To m_FieldValues.Count - 1
                Dim value As Object = Nothing
                'TypeConverter.ConvertTo will report any errors
                result = TypeConverter.ConvertTo(Me, m_FieldValues(i), m_Fields(i).FieldType, value, True)
                If result Then m_FieldValues(i) = value
            Next
            For i As Integer = 0 To m_PropertyValues.Count - 1
                Dim value As Object = Nothing
                'TypeConverter.ConvertTo will report any errors
                result = TypeConverter.ConvertTo(Me, m_PropertyValues(i), m_Properties(i).PropertyType, value, True)
                If result Then m_PropertyValues(i) = value
            Next
        End If

        m_CecilBuilder = GetAttributeBuilderCecil()

        If m_IsAssembly Then
            Me.Compiler.AssemblyBuilderCecil.CustomAttributes.Add(CecilBuilder)
        ElseIf m_IsModule Then
            Me.Compiler.ModuleBuilderCecil.CustomAttributes.Add(CecilBuilder)
        Else
            Dim memberparent As IAttributableDeclaration = Me.FindFirstParent(Of IAttributableDeclaration)()
            If memberparent IsNot Nothing Then
                Dim tp As TypeDeclaration = TryCast(memberparent, TypeDeclaration)
                Dim mthd As IMethod = TryCast(memberparent, IMethod)
                Dim ctro As IConstructorMember = TryCast(memberparent, IConstructorMember)
                Dim fld As IFieldMember = TryCast(memberparent, IFieldMember)
                Dim prop As PropertyDeclaration = TryCast(memberparent, PropertyDeclaration)
                Dim param As Parameter = TryCast(memberparent, Parameter)
                Dim evt As EventDeclaration = TryCast(memberparent, EventDeclaration)

                If ctro IsNot Nothing Then mthd = Nothing
                Helper.Assert(tp IsNot Nothing Xor mthd IsNot Nothing Xor ctro IsNot Nothing Xor fld IsNot Nothing Xor prop IsNot Nothing Xor param IsNot Nothing OrElse evt IsNot Nothing)

                If tp IsNot Nothing Then
                    If Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_SerializableAttribute) Then
                        tp.Serializable = True
                    ElseIf Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_StructLayoutAttribute) Then
                        Dim layout As System.Runtime.InteropServices.LayoutKind
                        layout = CType(CecilBuilder.ConstructorArguments(0).Value, System.Runtime.InteropServices.LayoutKind)
                        Select Case layout
                            Case Runtime.InteropServices.LayoutKind.Auto
                                tp.CecilType.IsAutoLayout = True
                            Case Runtime.InteropServices.LayoutKind.Explicit
                                tp.CecilType.IsExplicitLayout = True
                            Case Runtime.InteropServices.LayoutKind.Sequential
                                tp.CecilType.IsSequentialLayout = True
                            Case Else
                                Compiler.Report.ShowMessage(Messages.VBNC30127, Me.Location, CecilBuilder.Constructor.DeclaringType.FullName, "Invalid argument.")
                                Return False
                        End Select
                    Else
                        tp.CecilType.CustomAttributes.Add(CecilBuilder)
                    End If
                ElseIf mthd IsNot Nothing Then
                    If Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_DllImportAttribute) Then
                        Dim values As Mono.Collections.Generic.Collection(Of Mono.Cecil.CustomAttributeNamedArgument) = CecilBuilder.Fields
                        Dim modRef As Mono.Cecil.ModuleReference = Nothing
                        Dim modRefName As String = DirectCast(CecilBuilder.ConstructorArguments(0).Value, String)
                        Dim entry As String = Nothing
                        Dim charSetField As Nullable(Of Mono.Cecil.CustomAttributeNamedArgument) = Nothing
                        Dim entryPointField As Nullable(Of Mono.Cecil.CustomAttributeNamedArgument) = Nothing
                        Dim callingConventionField As Nullable(Of Mono.Cecil.CustomAttributeNamedArgument) = Nothing
                        Dim setLastErrorField As Nullable(Of Mono.Cecil.CustomAttributeNamedArgument) = Nothing

                        For i As Integer = 0 To values.Count - 1
                            If Helper.CompareNameOrdinal(values(i).Name, "CharSet") Then
                                charSetField = values(i)
                            ElseIf Helper.CompareNameOrdinal(values(i).Name, "EntryPoint") Then
                                entryPointField = values(i)
                                entry = DirectCast(entryPointField.Value.Argument.Value, String)
                            ElseIf Helper.CompareNameOrdinal(values(i).Name, "CallingConvention") Then
                                callingConventionField = values(i)
                            ElseIf Helper.CompareNameOrdinal(values(i).Name, "SetLastError") Then
                                setLastErrorField = values(i)
                            End If
                        Next

                        If entry = String.Empty Then entry = mthd.Name
                        For i As Integer = 0 To Compiler.AssemblyBuilderCecil.MainModule.ModuleReferences.Count - 1
                            If Helper.CompareNameOrdinal(Compiler.AssemblyBuilderCecil.MainModule.ModuleReferences(i).Name, modRefName) Then
                                modRef = Compiler.AssemblyBuilderCecil.MainModule.ModuleReferences(i)
                                Exit For
                            End If
                        Next
                        If modRef Is Nothing Then
                            modRef = New Mono.Cecil.ModuleReference(modRefName)
                            Compiler.AssemblyBuilderCecil.MainModule.ModuleReferences.Add(modRef)
                        End If
                        mthd.CecilBuilder.PInvokeInfo = New Mono.Cecil.PInvokeInfo(0, entry, modRef)

                        Dim charset As System.Runtime.InteropServices.CharSet
                        If charSetField.HasValue Then
                            charset = DirectCast(charSetField.Value.Argument.Value, System.Runtime.InteropServices.CharSet)
                        Else
                            charset = Runtime.InteropServices.CharSet.Auto
                        End If
                        Select Case charset
                            Case Runtime.InteropServices.CharSet.Ansi
                                mthd.CecilBuilder.PInvokeInfo.IsCharSetAnsi = True
                            Case Runtime.InteropServices.CharSet.Auto
                                mthd.CecilBuilder.PInvokeInfo.IsCharSetAuto = True
                            Case Runtime.InteropServices.CharSet.None
                                mthd.CecilBuilder.PInvokeInfo.IsCharSetNotSpec = True
                            Case Runtime.InteropServices.CharSet.Unicode
                                mthd.CecilBuilder.PInvokeInfo.IsCharSetUnicode = True
                            Case Else
                                result = Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, "Invalid charset: " & charset.ToString()) AndAlso result
                        End Select

                        Dim callingconv As System.Runtime.InteropServices.CallingConvention
                        If callingConventionField.HasValue Then
                            callingconv = DirectCast(callingConventionField.Value.Argument.Value, System.Runtime.InteropServices.CallingConvention)
                        Else
                            callingconv = Runtime.InteropServices.CallingConvention.Winapi
                        End If
                        Select Case callingconv
                            Case Runtime.InteropServices.CallingConvention.Cdecl
                                mthd.CecilBuilder.PInvokeInfo.IsCallConvCdecl = True
                            Case Runtime.InteropServices.CallingConvention.FastCall
                                mthd.CecilBuilder.PInvokeInfo.IsCallConvFastcall = True
                            Case Runtime.InteropServices.CallingConvention.StdCall
                                mthd.CecilBuilder.PInvokeInfo.IsCallConvStdCall = True
                            Case Runtime.InteropServices.CallingConvention.ThisCall
                                mthd.CecilBuilder.PInvokeInfo.IsCallConvThiscall = True
                            Case Runtime.InteropServices.CallingConvention.Winapi
                                mthd.CecilBuilder.PInvokeInfo.IsCallConvWinapi = True
                            Case Else
                                result = Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, "Invalid calling convention: " & callingconv.ToString()) AndAlso result
                        End Select

                        Dim setlasterror As Boolean = True
                        If setLastErrorField.HasValue Then
                            setlasterror = DirectCast(setLastErrorField.Value.Argument.Value, Boolean)
                        End If
                        mthd.CecilBuilder.PInvokeInfo.SupportsLastError = setlasterror
                        mthd.CecilBuilder.PInvokeInfo.IsNoMangle = True
                    Else
                        mthd.CecilBuilder.CustomAttributes.Add(CecilBuilder)
                    End If
                ElseIf ctro IsNot Nothing Then
                    ctro.CecilBuilder.CustomAttributes.Add(CecilBuilder)
                ElseIf fld IsNot Nothing Then
                    If Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_MarshalAsAttribute) Then
                        fld.FieldBuilder.MarshalInfo = New MarshalInfo(CType(CecilBuilder.ConstructorArguments(0).Value, Mono.Cecil.NativeType))
                        fld.FieldBuilder.Attributes = fld.FieldBuilder.Attributes Or Mono.Cecil.FieldAttributes.HasFieldMarshal
                    ElseIf Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_FieldOffsetAttribute) Then
                        fld.FieldBuilder.Offset = CType(CecilBuilder.ConstructorArguments(0).Value, Integer)
                    Else
                        fld.FieldBuilder.CustomAttributes.Add(CecilBuilder)
                    End If
                ElseIf prop IsNot Nothing Then
                    prop.CecilBuilder.CustomAttributes.Add(CecilBuilder)
                ElseIf param IsNot Nothing Then
                    If Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_OutAttribute) Then
                        param.CecilBuilder.IsOut = True
                    ElseIf Helper.CompareType(CecilBuilder.Constructor.DeclaringType, Compiler.TypeCache.System_Runtime_InteropServices_MarshalAsAttribute) Then
                        param.CecilBuilder.MarshalInfo = New MarshalInfo(CType(CecilBuilder.ConstructorArguments(0).Value, Mono.Cecil.NativeType))
                        param.CecilBuilder.Attributes = param.CecilBuilder.Attributes Or Mono.Cecil.ParameterAttributes.HasFieldMarshal
                    Else
                        param.CecilBuilder.CustomAttributes.Add(CecilBuilder)
                    End If
                ElseIf evt IsNot Nothing Then
                    evt.CecilBuilder.CustomAttributes.Add(CecilBuilder)
                Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                End If
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            End If
        End If

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return True
    End Function

    Public Function GetSecurityAttribute() As Mono.Cecil.SecurityAttribute
        Return DirectCast(GetAttribute(True), SecurityAttribute)
    End Function

    Private Function GetAttributeBuilderCecil() As Mono.Cecil.CustomAttribute
        Return DirectCast(GetAttribute(False), CustomAttribute)
    End Function

    Private Function GetAttribute(ByVal security As Boolean) As Mono.Cecil.ICustomAttribute
        Dim result As ICustomAttribute
        Dim customAttribute As CustomAttribute = Nothing
        Dim securityAttribute As SecurityAttribute = Nothing
        Dim parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition) = Helper.GetParameters(Compiler, m_ResolvedTypeConstructor)

        Helper.Assert(m_ResolvedTypeConstructor IsNot Nothing)
        Helper.Assert(m_Arguments IsNot Nothing)
        Helper.Assert(parameters.Count = m_Arguments.Length)
        Helper.Assert(m_Properties IsNot Nothing AndAlso m_PropertyValues IsNot Nothing AndAlso m_Properties.Count = m_PropertyValues.Count)
        Helper.Assert(m_Fields IsNot Nothing AndAlso m_FieldValues IsNot Nothing AndAlso m_Fields.Count = m_FieldValues.Count)

        m_ResolvedTypeConstructor = Helper.GetMethodOrMethodReference(Compiler, m_ResolvedTypeConstructor)

        Dim cecilArguments As Object()
        ReDim cecilArguments(m_Arguments.Length - 1)
        Array.Copy(m_Arguments, cecilArguments, m_Arguments.Length)
        For i As Integer = 0 To cecilArguments.Length - 1
            Dim type As Mono.Cecil.TypeReference
            type = TryCast(cecilArguments(i), Mono.Cecil.TypeReference)
            If type IsNot Nothing Then
                cecilArguments(i) = Helper.GetTypeOrTypeReference(Compiler, type)
            End If
        Next

        Try
            If security Then
                securityAttribute = New SecurityAttribute(Me.AttributeType)
                result = securityAttribute
            Else
                customAttribute = New Mono.Cecil.CustomAttribute(Helper.GetMethodOrMethodReference(Compiler, m_ResolvedTypeConstructor))
                result = customAttribute
            End If

            For i As Integer = 0 To m_Fields.Count - 1
                result.Fields.Add(New Mono.Cecil.CustomAttributeNamedArgument(m_Fields(i).Name, New CustomAttributeArgument(Helper.GetTypeOrTypeReference(Compiler, m_Fields(i).FieldType), m_FieldValues(i))))
            Next
            For i As Integer = 0 To m_Properties.Count - 1
                result.Properties.Add(New Mono.Cecil.CustomAttributeNamedArgument(m_Properties(i).Name, New CustomAttributeArgument(Helper.GetTypeOrTypeReference(Compiler, m_Properties(i).PropertyType), m_PropertyValues(i))))
            Next
            If customAttribute IsNot Nothing Then
                For i As Integer = 0 To cecilArguments.Length - 1
                    customAttribute.ConstructorArguments.Add(New CustomAttributeArgument(Helper.GetTypeOrTypeReference(Compiler, parameters(i).ParameterType), cecilArguments(i)))
                Next
            End If
        Catch ex As Exception
            Throw
        End Try

        Return result
    End Function
End Class

