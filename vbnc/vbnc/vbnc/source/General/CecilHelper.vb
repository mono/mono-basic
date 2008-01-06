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
        Dim type As TypeDefinition = FindDefinition(field.DeclaringType)
        Return GetField(type.Fields, field)
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
        Dim type As TypeDefinition = FindDefinition(method.DeclaringType)
        method = method.GetOriginalMethod
        If Helper.CompareNameOrdinal(method.Name, MethodDefinition.Cctor) OrElse Helper.CompareNameOrdinal(method.Name, MethodDefinition.Ctor) Then
            Return GetMethod(type.Constructors, method)
        Else
            Return GetMethod(type, method)
        End If
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

    Public Shared Function GetMethod(ByVal collection As ICollection, ByVal reference As MethodReference) As MethodDefinition
        For Each meth As MethodDefinition In collection
            If Helper.CompareNameOrdinal(meth.Name, reference.Name) = False Then
                Continue For
            End If
            If Not AreSame(meth.ReturnType.ReturnType, reference.ReturnType.ReturnType) Then
                Continue For
            End If
            If Not AreSame(meth.Parameters, reference.Parameters) Then
                Continue For
            End If
            Return meth
        Next
        Return Nothing
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