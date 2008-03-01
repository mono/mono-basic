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
#If ENABLECECIL Then

Imports Mono.Cecil

Public Class CecilHelper
    Private Shared _assemblies As New Hashtable

    Private Class resolver
        Inherits BaseAssemblyResolver

    End Class

    Public Shared Function GetMembers(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.MemberReferenceCollection
        Dim tD As Mono.Cecil.TypeDefinition = FindDefinition(Type)
        Dim result As New Mono.Cecil.MemberReferenceCollection(Type.Module)
        For Each list As System.Collections.IList In New System.Collections.IList() {tD.Events, tD.Methods, tD.Properties, tD.NestedTypes, tD.Fields, tD.Constructors}
            For Each item As Mono.Cecil.MemberReference In list
                result.Add(item)
            Next
        Next
        Return result
    End Function

    Public Shared Function MakeByRefType(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.ReferenceType
        Return New Mono.Cecil.ReferenceType(Type)
    End Function

    Public Shared Function MakeGenericMethod(ByVal Method As MethodReference, ByVal Types() As Mono.Cecil.TypeReference) As Mono.Cecil.GenericInstanceMethod
        Throw New NotImplementedException
    End Function

    Public Shared Function GetNestedType(ByVal Type As TypeReference, ByVal Name As String) As TypeReference
        Dim tD As TypeDefinition = FindDefinition(Type)
        For i As Integer = 0 To tD.NestedTypes.Count - 1
            If Helper.CompareName(tD.NestedTypes(i).Name, Name) Then Return tD.NestedTypes(i)
        Next
        Return Nothing
    End Function

    Public Shared Function GetNestedTypes(ByVal Type As TypeReference) As NestedTypeCollection
        Return FindDefinition(Type).NestedTypes
    End Function

    Public Shared Function GetMemberType(ByVal member As Mono.Cecil.MemberReference) As MemberTypes
        If TypeOf member Is FieldReference Then Return MemberTypes.Field
        If TypeOf member Is TypeReference Then Return MemberTypes.TypeInfo
        If TypeOf member Is MethodReference Then
            If Helper.CompareNameOrdinal(member.Name, ".ctor") OrElse Helper.CompareNameOrdinal(member.Name, ".cctor") Then
                Return MemberTypes.Constructor
            Else
                Return MemberTypes.Method
            End If
        End If
        If TypeOf member Is EventReference Then Return MemberTypes.Event
        If TypeOf member Is PropertyReference Then Return MemberTypes.Property
        Throw New NotImplementedException
    End Function

    Public Shared Function IsStatic(ByVal Field As Mono.Cecil.FieldReference) As Boolean
        Return FindDefinition(Field).IsStatic
    End Function

    Public Shared Function IsStatic(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Return FindDefinition(Method).IsStatic
    End Function

    Public Shared Function MakeArrayType(ByVal Type As Mono.Cecil.TypeReference, Optional ByVal Ranks As Integer = 1) As Mono.Cecil.ArrayType
        Return New Mono.Cecil.ArrayType(Type, Ranks)
    End Function

    Public Overloads Shared Function [GetType](ByVal Compiler As Compiler, ByVal value As Object) As Mono.Cecil.TypeReference
        If value Is Nothing Then Throw New InternalException("'Nothing' doesn't have a type")
        Select Case Type.GetTypeCode(value.GetType)
            Case TypeCode.Boolean
                Return Compiler.TypeCache.System_Boolean
            Case TypeCode.Byte
                Return Compiler.TypeCache.System_Byte
            Case TypeCode.Char
                Return Compiler.TypeCache.System_Char
            Case TypeCode.DateTime
                Return Compiler.TypeCache.System_DateTime
            Case TypeCode.DBNull
                Return Compiler.TypeCache.System_DBNull
            Case TypeCode.Decimal
                Return Compiler.TypeCache.System_Decimal
            Case TypeCode.Double
                Return Compiler.TypeCache.System_Double
            Case TypeCode.Int16
                Return Compiler.TypeCache.System_Int16
            Case TypeCode.Int32
                Return Compiler.TypeCache.System_Int32
            Case TypeCode.Int64
                Return Compiler.TypeCache.System_Int64
            Case TypeCode.SByte
                Return Compiler.TypeCache.System_SByte
            Case TypeCode.Single
                Return Compiler.TypeCache.System_Single
            Case TypeCode.String
                Return Compiler.TypeCache.System_String
            Case TypeCode.UInt16
                Return Compiler.TypeCache.System_UInt16
            Case TypeCode.UInt32
                Return Compiler.TypeCache.System_UInt32
            Case TypeCode.UInt64
                Return Compiler.TypeCache.System_UInt64
            Case Else
                Throw New InternalException(String.Format("No constant value can be of the type '{0}'", value.GetType.FullName))
        End Select
    End Function

    Public Shared Function GetCustomAttributes(ByVal Type As Mono.Cecil.TypeDefinition, ByVal AttributeType As Mono.Cecil.TypeReference) As Mono.Cecil.CustomAttributeCollection
        Return GetCustomAttributes(Type.CustomAttributes, AttributeType)
    End Function

    Public Shared Function GetCustomAttributes(ByVal Attributes As Mono.Cecil.CustomAttributeCollection, ByVal AttributeType As Mono.Cecil.TypeReference) As Mono.Cecil.CustomAttributeCollection
        Dim result As Mono.Cecil.CustomAttributeCollection = Nothing

        For Each attrib As Mono.Cecil.CustomAttribute In Attributes
            If Helper.CompareType(AttributeType, attrib.Constructor.DeclaringType) Then
                If result Is Nothing Then result = New Mono.Cecil.CustomAttributeCollection(Attributes.Container)
                result.Add(attrib)
            End If
        Next

        Return result
    End Function

    Public Shared Function IsDefault(ByVal Compiler As Compiler, ByVal Prop As Mono.Cecil.PropertyReference) As Boolean
        Return FindDefinition(Prop).CustomAttributes.IsDefined(Compiler.TypeCache.System_Reflection_DefaultMemberAttribute)
    End Function

    Public Shared Function IsGenericParameter(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.GenericParameter
    End Function

    Public Shared Function IsGenericType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        If Type.GenericParameters Is Nothing OrElse Type.GenericParameters.Count = 0 Then Return False
        Throw New NotImplementedException
    End Function

    Public Shared Function IsGenericTypeDefinition(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Dim tD As Mono.Cecil.TypeDefinition = TryCast(Type, Mono.Cecil.TypeDefinition)
        Return tD IsNot Nothing AndAlso tD.GenericParameters.Count > 0
    End Function

    Public Shared Function ContainsGenericParameters(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Throw New NotImplementedException
    End Function

    Public Shared Function GetTypes(ByVal Params As Mono.Cecil.GenericParameterCollection) As Mono.Cecil.TypeReference()
        Dim result(Params.Count - 1) As Mono.Cecil.TypeReference
        For i As Integer = 0 To Params.Count - 1
            result(i) = Params(i)
        Next
        Return result
    End Function

    Public Shared Function GetTypes(ByVal Params As Mono.Cecil.GenericArgumentCollection) As Mono.Cecil.TypeReference()
        Dim result(Params.Count - 1) As Mono.Cecil.TypeReference
        For i As Integer = 0 To Params.Count - 1
            result(i) = Params(i)
        Next
        Return result
    End Function

    Public Shared Function GetGenericArguments(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference()
        Dim tR As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        If tR Is Nothing Then
            Return GetTypes(Type.GenericParameters)
        Else
            Throw New NotImplementedException
        End If
    End Function

    Public Shared Function GetGenericArguments(ByVal Method As Mono.Cecil.MethodReference) As Mono.Cecil.TypeReference()
        Dim mR As Mono.Cecil.GenericInstanceMethod = TryCast(Method, Mono.Cecil.GenericInstanceMethod)
        If mR Is Nothing Then
            Return GetTypes(Method.GenericParameters)
        Else
            Return GetTypes(mR.GenericArguments)
        End If
    End Function

    Public Shared Function GetGenericTypeDefinition(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeDefinition
        Return FindDefinition(Type)
    End Function

    Public Shared Function GetGenericMethod(ByVal Method As Mono.Cecil.MethodReference) As Mono.Cecil.MethodReference
        Throw New NotImplementedException
    End Function

    Public Shared Function IsGenericMethod(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        If TypeOf Method Is Mono.Cecil.GenericInstanceMethod Then Return True
        If Method.GenericParameters.Count > 0 Then Return True
        Return False
        Throw New NotImplementedException
    End Function

    Public Shared Function IsGenericMethodDefinition(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Throw New NotImplementedException
    End Function


    Public Shared Function IsByRef(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.ReferenceType
    End Function

    Public Shared Function IsArray(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.ArrayType
    End Function

    Public Shared Function GetInterfaces(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.InterfaceCollection ' Mono.Cecil.TypeReference()
        '        Dim tp As Mono.Cecil.TypeDefinition = FindDefinition(Type)
        Return FindDefinition(Type).Interfaces
    End Function

    Public Shared Function GetElementType(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Return Type.GetOriginalType
    End Function

    Public Shared Function IsPrimitive(ByVal Compiler As Compiler, ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Select Case Helper.GetTypeCode(Compiler, Type)
            Case TypeCode.Byte, TypeCode.SByte, TypeCode.Boolean, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Char, TypeCode.Double, TypeCode.Single
                Return True
            Case Else
                If Helper.CompareType(Type, Compiler.TypeCache.System_IntPtr) Then
                    Return True
                Else
                    Return False
                End If
        End Select
    End Function

    Public Shared Function GetArrayRank(ByVal Type As Mono.Cecil.TypeReference) As Integer
        Dim aT As ArrayType = TryCast(Type, ArrayType)
        If aT Is Nothing Then Return 0
        Return aT.Rank
    End Function

    Public Shared Function GetGetMethod(ByVal Prop As Mono.Cecil.PropertyReference) As Mono.Cecil.MethodReference
        Return FindDefinition(Prop).GetMethod
    End Function

    Public Shared Function GetSetMethod(ByVal Prop As PropertyReference) As MethodReference
        Return FindDefinition(Prop).SetMethod
    End Function

    Public Shared Function IsClass(ByVal Type As TypeReference) As Boolean
        Return FindDefinition(Type).IsClass
    End Function

    Public Shared Function GetGenericParameterAttributes(ByVal Type As TypeReference) As GenericParameterAttributes
        Throw New NotImplementedException
    End Function

    Public Shared Function IsInterface(ByVal Type As TypeReference) As Boolean
        Return FindDefinition(Type).IsInterface
    End Function

    Public Shared ReadOnly Property AssemblyCache() As IDictionary
        Get
            Return _assemblies
        End Get
    End Property

    Public Shared Function FindDefinition(ByVal name As AssemblyNameReference) As AssemblyDefinition
        Dim asm As AssemblyDefinition = TryCast(_assemblies(name.Name), AssemblyDefinition)
        If asm Is Nothing Then
            Dim base As New resolver

            asm = base.Resolve(name)
            asm.Resolver = base
            _assemblies(name.Name) = asm
        End If

        Return asm
    End Function

    Public Shared Function FindDefinition(ByVal type As TypeReference) As TypeDefinition
        If type Is Nothing Then Return Nothing
        Dim tD As TypeDefinition = TryCast(type, TypeDefinition)
        If tD IsNot Nothing Then Return tD
        type = type.GetOriginalType
        If TypeOf type Is TypeDefinition Then
            Return DirectCast(type, TypeDefinition)
        End If
        Dim reference As AssemblyNameReference = TryCast(type.Scope, AssemblyNameReference)
        If reference IsNot Nothing Then
            Dim assembly As AssemblyDefinition = FindDefinition(reference)
            Return assembly.MainModule.Types(type.FullName)
        End If
        Dim moduledef As ModuleDefinition = TryCast(type.Scope, ModuleDefinition)
        If moduledef IsNot Nothing Then
            Return moduledef.Types(type.FullName)
        End If
        Throw New NotImplementedException
    End Function

    Public Shared Function FindDefinition(ByVal field As FieldReference) As FieldDefinition
        If field Is Nothing Then Return Nothing
        Dim fD As FieldDefinition = TryCast(field, FieldDefinition)
        If fD IsNot Nothing Then Return fD
        Dim type As TypeDefinition = FindDefinition(field.DeclaringType)
        Return GetField(Type.Fields, field)
    End Function

    Public Shared Function FindDefinition(ByVal param As ParameterReference) As ParameterDefinition
        If param Is Nothing Then Return Nothing
        Dim pD As ParameterDefinition = TryCast(param, ParameterDefinition)
        If pD IsNot Nothing Then Return pD
        Throw New NotImplementedException
    End Function

    Public Shared Function GetField(ByVal collection As ICollection, ByVal reference As FieldReference) As FieldDefinition
        For Each field As FieldDefinition In collection
            If Not Helper.CompareNameOrdinal(field.Name, reference.Name) Then
                Continue For
            End If
            If Not AreSame(field.FieldType, reference.FieldType) Then
                Continue For
            End If
            Return field
        Next
        Return Nothing
    End Function

    Public Shared Function FindDefinition(ByVal method As MethodReference) As MethodDefinition
        If method Is Nothing Then Return Nothing
        Dim type As TypeDefinition = FindDefinition(method.DeclaringType)
        method = method.GetOriginalMethod
        If Helper.CompareNameOrdinal(method.Name, MethodDefinition.Cctor) OrElse Helper.CompareNameOrdinal(method.Name, MethodDefinition.Ctor) Then
            Return GetMethod(type.Constructors, method)
        Else
            Return GetMethod(type, method)
        End If
    End Function

    Public Shared Function FindDefinition(ByVal method As PropertyReference) As PropertyDefinition
        If method Is Nothing Then Return Nothing
        Dim type As TypeDefinition = FindDefinition(method.DeclaringType)
        'method = method.GetOriginalMethod
        'If Helper.CompareNameOrdinal(method.Name, MethodDefinition.Cctor) OrElse Helper.CompareNameOrdinal(method.Name, MethodDefinition.Ctor) Then
        '    Return GetMethod(type.Constructors, method)
        'Else
        Return GetProperty(type.Properties, method)
        'End If
    End Function

    Public Shared Function FindDefinition(ByVal method As EventReference) As EventDefinition
        If method Is Nothing Then Return Nothing
        Dim type As TypeDefinition = FindDefinition(method.DeclaringType)
        'method = method.GetOriginalMethod
        'If Helper.CompareNameOrdinal(method.Name, MethodDefinition.Cctor) OrElse Helper.CompareNameOrdinal(method.Name, MethodDefinition.Ctor) Then
        '    Return GetMethod(type.Constructors, method)
        'Else
        Return GetEvent(type.Events, method)
        'End If
    End Function

    Public Shared Function GetEvent(ByVal collection As ICollection, ByVal reference As EventReference) As EventDefinition
        For Each meth As EventDefinition In collection
            If Helper.CompareNameOrdinal(meth.Name, reference.Name) = False Then
                Continue For
            End If
            Return meth
        Next
        Return Nothing
    End Function

    Public Shared Function GetMethod(ByVal type As ICollection, ByVal reference As MethodReference) As MethodDefinition
        For Each item As Mono.Cecil.MethodDefinition In type
            If CecilHelper.AreSame(reference, item) Then Return item
        Next
        Return Nothing
        'While type IsNot Nothing
        '    Dim method As MethodDefinition = GetMethod(type.Methods, reference)
        '    If method Is Nothing Then
        '        type = FindDefinition(type.BaseType)
        '    Else
        '        Return method
        '    End If
        'End While
        'Return Nothing
    End Function

    Public Shared Function GetMethod(ByVal type As TypeDefinition, ByVal reference As MethodReference) As MethodDefinition
        While type IsNot Nothing
            Dim method As MethodDefinition = GetMethod(type.Methods, reference)
            If method Is Nothing Then
                type = FindDefinition(type.BaseType)
            Else
                Return method
            End If
        End While
        Return Nothing
    End Function

    Public Shared Function GetProperty(ByVal collection As ICollection, ByVal reference As PropertyReference) As PropertyDefinition
        For Each meth As PropertyDefinition In collection
            If Helper.CompareNameOrdinal(meth.Name, reference.Name) = False Then
                Continue For
            End If
            If Not AreSame(meth.PropertyType, reference.PropertyType) Then
                Continue For
            End If
            If Not AreSame(meth.Parameters, reference.Parameters) Then
                Continue For
            End If
            Return meth
        Next
        Return Nothing
    End Function

    Public Shared Function AreSame(ByVal a As MethodReference, ByVal b As MethodReference) As Boolean
        If Helper.CompareNameOrdinal(a.Name, b.Name) = False Then Return False
        Return AreSame(a.Parameters, b.Parameters)
    End Function

    Public Shared Function AreSame(ByVal a As ParameterDefinitionCollection, ByVal b As ParameterDefinitionCollection) As Boolean
        If a.Count <> b.Count Then Return False
        If a.Count = 0 Then Return True
        For i As Integer = 0 To a.Count - 1
            If Not AreSame(a(i).ParameterType, b(i).ParameterType) Then Return False
        Next
        Return True
    End Function

    Public Shared Function AreSame(ByVal a As TypeReference, ByVal b As TypeReference) As Boolean
        While TypeOf a Is TypeSpecification OrElse TypeOf b Is TypeSpecification
            If a.GetType IsNot b.GetType Then
                Return False
            End If
            a = DirectCast(a, TypeSpecification).ElementType
            b = DirectCast(b, TypeSpecification).ElementType
        End While
        If TypeOf a Is GenericParameter OrElse TypeOf b Is GenericParameter Then
            If a.GetType IsNot b.GetType Then
                Return False
            End If
            Dim pa As GenericParameter = DirectCast(a, GenericParameter)
            Dim pb As GenericParameter = DirectCast(b, GenericParameter)

            Return pa.Position = pb.Position
        End If
        Return Helper.CompareNameOrdinal(a.FullName, b.FullName)
    End Function

End Class

#End If