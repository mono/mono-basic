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

Imports Mono.Cecil
Imports System.Diagnostics

Public Class GenericConstructedMethod
    Inherits MethodReference

    Private m_OriginalMethod As MethodReference

    Sub New(ByVal OriginalMethod As MethodReference, ByVal Type As TypeReference)
        MyBase.New(OriginalMethod.Name, Type, OriginalMethod.DeclaringType, OriginalMethod.HasThis, OriginalMethod.HasThis, OriginalMethod.CallingConvention)
        m_OriginalMethod = OriginalMethod
    End Sub

    Public Overrides Function GetOriginalMethod() As Mono.Cecil.MethodReference
        Return m_OriginalMethod
    End Function
End Class

Public Class CecilHelper
    Private Shared _assemblies As New Hashtable

#If ENABLECECIL And Debug Then
    Public Shared Sub Test()
        For Each Type As TypeDefinition In a.MainModule.Types
            System.Diagnostics.Debug.WriteLine(Type.FullName)
            For Each field As FieldDefinition In Type.Fields
                System.Diagnostics.Debug.WriteLine(field.Name)
            Next
            For Each method As MethodDefinition In Type.Methods
                System.Diagnostics.Debug.WriteLine(method.Name)
            Next
        Next
    End Sub

    Shared m As ModuleDefinition
    Shared tested As New Generic.List(Of Object)

    Private Shared Sub TestMember(ByVal Member As MemberReference)
        Try
            If tested.Contains(Member) Then Return
            tested.Add(Member)

            If TypeOf Member Is TypeSpecification Then
                Dim ts As TypeSpecification = DirectCast(Member, TypeSpecification)
                If Not TypeOf ts Is Mono.Cecil.GenericInstanceType Then
                    Dim str As String = ts.ToString()
                    If tested.Contains(str) Then Return
                    tested.Add(str)
                End If
            End If

            If (TypeOf Member Is MethodReference) Then
                Dim m1 As MethodReference = DirectCast(Member, MethodReference)
                m.Import(m1)
                For Each t1 As TypeReference In m1.GenericParameters
                    TestMember(t1)
                Next
                For Each p1 As ParameterDefinition In m1.Parameters
                    TestMember(p1.ParameterType)
                Next
            ElseIf TypeOf Member Is FieldReference Then
                Dim f1 As FieldReference = DirectCast(Member, FieldReference)
                m.Import(f1)
                TestMember(f1.FieldType)
            ElseIf TypeOf Member Is TypeReference Then
                Dim t1 As TypeReference = DirectCast(Member, TypeReference)
                Dim tD As TypeDefinition = TryCast(Member, TypeDefinition)
                m.Import(t1)
                For Each t2 As TypeReference In t1.GenericParameters
                    TestMember(t2)
                Next
                If tD IsNot Nothing Then
                    For Each t2 As TypeDefinition In tD.NestedTypes
                        TestMember(t2)
                    Next
                    For Each m1 As MethodDefinition In tD.Methods
                        TestMember(m1)
                    Next
                    For Each m1 As MethodDefinition In tD.Constructors
                        TestMember(m1)
                    Next
                    For Each f1 As FieldDefinition In tD.Fields
                        TestMember(f1)
                    Next
                End If
            Else
                Throw New NotImplementedException
            End If
        Catch ex As Exception
            If TypeOf Member Is TypeReference Then
                Debug.WriteLine(String.Format("{2} {0}: {1}", DirectCast(Member, TypeReference).FullName, ex.Message, Member.GetType().FullName))
            Else
                Debug.WriteLine(String.Format("{3} {0}.{1}: {2}", Member.DeclaringType.FullName, Member.Name, ex.Message, Member.GetType().FullName))
            End If
        End Try
    End Sub

    Public Shared Sub TestCecil()
        Dim a As Mono.Cecil.AssemblyDefinition
        Dim corlib As Mono.Cecil.AssemblyDefinition = AssemblyFactory.GetAssembly(GetType(Integer).Assembly.Location)

        System.Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("en-US")
        System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

        a = Mono.Cecil.AssemblyFactory.DefineAssembly("test", AssemblyKind.Dll)
        m = a.MainModule

        For Each t1 As Mono.Cecil.TypeDefinition In corlib.MainModule.Types
            'Debug.WriteLine(t1.FullName)
            TestMember(t1)
        Next
    End Sub
#End If

    Private Class resolver
        Inherits BaseAssemblyResolver

    End Class

    Public Shared Sub GetConstructors(ByVal Type As Mono.Cecil.TypeReference, ByVal result As Mono.Cecil.MemberReferenceCollection)
        Dim tD As Mono.Cecil.TypeDefinition = FindDefinition(Type)
        For Each item As Mono.Cecil.MethodDefinition In tD.Constructors
            If item.IsStatic Then Continue For
            If Helper.CompareType(item.DeclaringType, Type) = False Then
                result.Add(GetCorrectMember(item, Type))
            Else
                result.Add(item)
            End If
        Next
    End Sub

    Public Shared Function GetConstructors(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.MemberReferenceCollection
        Dim result As New Mono.Cecil.MemberReferenceCollection(Type.Module)
        GetConstructors(Type, result)
        Return result
    End Function

    Private Shared Sub AddRange(ByVal C As Mono.Cecil.MemberReferenceCollection, ByVal C2 As Mono.Cecil.MemberReferenceCollection)
        For i As Integer = 0 To C2.Count - 1
            C.Add(C2(i))
        Next
    End Sub

    Public Shared Function GetMembers(ByVal Type As Mono.Cecil.GenericParameter) As Mono.Cecil.MemberReferenceCollection
        Dim result As New Mono.Cecil.MemberReferenceCollection(Type.Module)

        For i As Integer = 0 To Type.Constraints.Count - 1
            AddRange(result, GetMembers(Type.Constraints(i)))
        Next
        If Type.HasReferenceTypeConstraint Then
            AddRange(result, GetMembers(BaseObject.m_Compiler.TypeCache.System_Object))
        End If

        Return result
    End Function

    Public Shared Function GetMembers(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.MemberReferenceCollection
        Dim tD As Mono.Cecil.TypeDefinition
        Dim result As Mono.Cecil.MemberReferenceCollection

        Dim tG As Mono.Cecil.GenericParameter = TryCast(Type, GenericParameter)
        If tG IsNot Nothing Then Return GetMembers(tG)

        Dim arr As Mono.Cecil.ArrayType = TryCast(Type, ArrayType)
        If arr IsNot Nothing Then
            result = New Mono.Cecil.MemberReferenceCollection(Type.Module)
            For Each member As MemberReference In GetMembers(BaseObject.m_Compiler.TypeCache.System_Array)
                result.Add(GetCorrectMember(member, Type))
            Next
            Return result
        End If

        tD = FindDefinition(Type)

        result = New Mono.Cecil.MemberReferenceCollection(Type.Module)

        For Each list As System.Collections.IList In New System.Collections.IList() {tD.Events, tD.Methods, tD.Properties, tD.NestedTypes, tD.Fields, tD.Constructors}
            For Each item As Mono.Cecil.MemberReference In list
                If Helper.CompareType(item.DeclaringType, Type) = False Then
                    item = GetCorrectMember(item, Type)
                End If
                result.Add(item)
            Next
        Next

        Return result
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As TypeReference, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim tD As Mono.Cecil.TypeDefinition = TryCast(Member, Mono.Cecil.TypeDefinition)

        If tD IsNot Nothing Then Return GetCorrectMember(tD, Type)

        Dim tG As Mono.Cecil.GenericInstanceType = TryCast(Member, Mono.Cecil.GenericInstanceType)
        If tG IsNot Nothing Then
            tD = TryCast(tG.ElementType, Mono.Cecil.TypeDefinition)
            If tD IsNot Nothing Then
                Helper.Assert(tG.GenericParameters.Count = 0)
                Return GetCorrectMember(tD, Type)
            End If
        End If

        tD = FindDefinition(Member)
        Return GetCorrectMember(tD, Type)

        Throw New NotImplementedException
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MemberReference, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.MemberReference
        Dim method As MethodDefinition = TryCast(Member, MethodDefinition)
        Dim field As FieldDefinition = TryCast(Member, FieldDefinition)
        Dim prop As PropertyDefinition = TryCast(Member, PropertyDefinition)
        Dim t As TypeDefinition = TryCast(Member, TypeDefinition)

        If method IsNot Nothing Then
            Return GetCorrectMember(method, Type)
        ElseIf field IsNot Nothing Then
            Return GetCorrectMember(field, Type)
        ElseIf prop IsNot Nothing Then
            Return GetCorrectMember(prop, Type)
        ElseIf t IsNot Nothing Then
            Return GetCorrectMember(t, Type)
        Else
            Throw New NotImplementedException
        End If

    End Function

    Public Shared Function GetCorrectMember(ByVal Member As Mono.Cecil.TypeDefinition, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.GenericInstanceType = Nothing
        Dim args As New Generic.List(Of Mono.Cecil.TypeReference)
        Dim any_change As Boolean
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)

        If genericType Is Nothing Then Return Member

        result = New Mono.Cecil.GenericInstanceType(Member)
        result.DeclaringType = FindDefinition(Type)

        For i As Integer = 0 To Member.GenericParameters.Count - 1
            Dim found As Boolean = False
            For j As Integer = 0 To genericType.ElementType.GenericParameters.Count - 1
                If genericType.ElementType.GenericParameters(j).Name = Member.GenericParameters(i).Name Then
                    result.GenericArguments.Add(Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, genericType.GenericArguments(j)))
                    found = True
                    any_change = True
                    Exit For
                End If
            Next
            If Not found Then Throw New NotImplementedException
        Next

        Return result
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As PropertyDefinition, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.PropertyReference
        Dim result As Mono.Cecil.PropertyDefinition
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim propertyType As Mono.Cecil.TypeReference
        Dim getMethod As Mono.Cecil.MethodReference = Nothing
        Dim setMethod As Mono.Cecil.MethodReference = Nothing

        If genericType Is Nothing Then Return Member

        propertyType = CecilHelper.ResolveType(Member.PropertyType, Member.DeclaringType.GenericParameters, genericType.GenericArguments)
        propertyType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, propertyType)

        If Member.GetMethod IsNot Nothing Then
            getMethod = GetCorrectMember(Member.GetMethod, Type)
        End If

        If Member.SetMethod IsNot Nothing Then
            setMethod = GetCorrectMember(Member.SetMethod, Type)
        End If

        'If propertyType Is Member.PropertyType AndAlso (getMethod Is Nothing OrElse Member.GetMethod Is getMethod) AndAlso (setMethod Is Nothing OrElse Member.SetMethod Is setMethod) Then
        'Return Member
        'End If

        result = New Mono.Cecil.PropertyDefinition(Member.Name, propertyType, Member.Attributes)
        result.DeclaringType = genericType
        result.SetMethod = setMethod
        result.GetMethod = getMethod
        result.Annotations.Add("OriginalProperty", Member)
        Return result
    End Function

    Public Shared Function InflateType(ByVal original As TypeReference, ByVal container As TypeReference) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ReferenceType = TryCast(original, ReferenceType)
        Dim genericType As GenericInstanceType = TryCast(original, GenericInstanceType)
        Dim originalDef As TypeDefinition = TryCast(original, TypeDefinition)
        Dim parameters As GenericParameterCollection
        Dim arguments As GenericArgumentCollection
        Dim genericCollection As GenericInstanceType = TryCast(container, GenericInstanceType)

        If genericCollection Is Nothing Then
            Return original
            Throw New ArgumentException("The type to inflate with isn't generic.")
        End If

        If originalDef IsNot Nothing Then
            If originalDef.GenericParameters.Count = 0 Then Return original
            Dim result As New GenericInstanceType(originalDef)
            For i As Integer = 0 To originalDef.GenericParameters.Count - 1
                Dim tG As GenericParameter = originalDef.GenericParameters(i)
                If tG.Owner Is originalDef Then
                    result.GenericArguments.Add(InflateType(originalDef.GenericParameters(i), container))
                Else
                    result.GenericArguments.Add(originalDef.GenericParameters(i))
                End If
            Next
            Return result
        End If

        parameters = genericCollection.ElementType.GenericParameters
        arguments = genericCollection.GenericArguments

        If parameters.Count <> arguments.Count Then
            Throw New System.ArgumentException("Parameters and Arguments must have the same number of elements.")
        End If

        Dim genParam As GenericParameter = TryCast(original, GenericParameter)
        If genParam IsNot Nothing Then
            If Not TypeOf genParam.Owner Is TypeReference Then Return genParam
            Helper.Assert(genParam.Position < arguments.Count)
            Return arguments.Item(genParam.Position)
        End If

        If genericType IsNot Nothing Then
            Dim result As New GenericInstanceType(genericType.ElementType)
            For i As Integer = 0 To genericType.ElementType.GenericParameters.Count - 1
                result.GenericArguments.Add(InflateType(genericType.GenericArguments(i), container))
            Next
            Return result
        End If

        If spec IsNot Nothing Then
            Dim resolved As TypeReference = InflateType(spec.ElementType, container)

            If resolved Is spec.ElementType Then
                Return spec
            End If


            If array IsNot Nothing Then
                Return New ArrayType(resolved, array.Dimensions)
            ElseIf reference IsNot Nothing Then
                Return New ReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        Else
            Return original
        End If
    End Function

    Public Shared Function InflateType(ByVal original As TypeReference, ByVal parameters As GenericParameterCollection, ByVal arguments As GenericArgumentCollection) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ReferenceType = TryCast(original, ReferenceType)
        Dim genericType As GenericInstanceType = TryCast(original, GenericInstanceType)
        Dim originalDef As TypeDefinition = TryCast(original, TypeDefinition)

        If originalDef IsNot Nothing Then
            If originalDef.GenericParameters.Count = 0 Then Return original
            Dim result As New GenericInstanceType(originalDef)
            For i As Integer = 0 To originalDef.GenericParameters.Count - 1
                Dim tG As GenericParameter = originalDef.GenericParameters(i)
                If tG.Owner Is originalDef Then
                    result.GenericArguments.Add(InflateType(originalDef.GenericParameters(i), parameters, arguments))
                Else
                    result.GenericArguments.Add(originalDef.GenericParameters(i))
                End If
            Next
            Return result
        End If

        If parameters.Count <> arguments.Count Then
            Throw New System.ArgumentException("Parameters and Arguments must have the same number of elements.")
        End If

        Dim genParam As GenericParameter = TryCast(original, GenericParameter)
        If genParam IsNot Nothing Then
            If Not TypeOf genParam.Owner Is TypeReference Then
                For i As Integer = 0 To parameters.Count - 1
                    If parameters(i).Owner Is genParam.Owner AndAlso parameters(i).Position = genParam.Position Then
                        Return arguments(i)
                    End If
                Next
                Return genParam
            End If
            Helper.Assert(genParam.Position < arguments.Count)
            Return arguments.Item(genParam.Position)
        End If

        If genericType IsNot Nothing Then
            Dim result As New GenericInstanceType(genericType.ElementType)
            For i As Integer = 0 To genericType.ElementType.GenericParameters.Count - 1
                result.GenericArguments.Add(InflateType(genericType.GenericArguments(i), parameters, arguments))
            Next
            Return result
        End If

        If spec IsNot Nothing Then
            Dim resolved As TypeReference = InflateType(spec.ElementType, parameters, arguments)

            If resolved Is spec.ElementType Then
                Return spec
            End If


            If array IsNot Nothing Then
                Return New ArrayType(resolved, array.Dimensions)
            ElseIf reference IsNot Nothing Then
                Return New ReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        Else
            Return original
        End If
    End Function

    Public Shared Function ResolveType(ByVal original As TypeReference, ByVal parameters As GenericParameterCollection, ByVal arguments As GenericArgumentCollection) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ReferenceType = TryCast(original, ReferenceType)
        Dim genericType As GenericInstanceType = TryCast(original, GenericInstanceType)

        If parameters.Count <> arguments.Count Then
            Throw New System.ArgumentException("Parameters and Arguments must have the same number of elements.")
        End If

        If spec IsNot Nothing Then
            Dim resolved As TypeReference = ResolveType(spec.ElementType, parameters, arguments)

            If genericType IsNot Nothing Then
                Dim result As GenericInstanceType = New GenericInstanceType(genericType.ElementType)
                For i As Integer = 0 To genericType.GenericArguments.Count - 1
                    Dim tg As Mono.Cecil.TypeReference = ResolveType(genericType.GenericArguments(i), parameters, arguments)
                    result.GenericArguments.Add(tg)
                Next
                Return result
            End If

            If resolved Is spec.ElementType Then
                Return spec
            End If


            If array IsNot Nothing Then
                Return New ArrayType(resolved, array.Dimensions)
            ElseIf (reference IsNot Nothing) Then
                Return New ReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        Else
            For i As Integer = 0 To parameters.Count - 1
                If parameters(i) Is original Then
                    Return arguments(i)
                End If
            Next
            Return original
        End If
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As FieldDefinition, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.FieldReference
        Dim result As Mono.Cecil.FieldReference
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim fieldType As Mono.Cecil.TypeReference

        If genericType Is Nothing Then
            Return Member
        End If

        fieldType = CecilHelper.ResolveType(Member.FieldType, genericType.ElementType.GenericParameters, genericType.GenericArguments)

        If fieldType IsNot Member.FieldType Then
            fieldType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, fieldType)

            result = New FieldReference(Member.Name, Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, Member.DeclaringType), fieldType)
            result.Annotations.Add("MemberInReflection", New FieldReference(Member.Name, genericType, Member.FieldType))
            Return result
        Else
            Return Member
        End If

    End Function


    '
    'Emittable: to get a member as cecil/cil wants it
    ' - 1Declarations/GenericProperty1 shows one case where vbnc and cecil/cil wants different things (method return type shouldn't be inflated for cecil/cil)
    '
    '

    Public Shared Function GetCorrectMember(ByVal Member As MethodReference, ByVal Type As TypeReference, Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim mD As MethodDefinition = TryCast(Member, MethodDefinition)

        If mD IsNot Nothing Then Return GetCorrectMember(mD, Type, Emittable)

        Throw New NotImplementedException
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MethodReference, ByVal Arguments As GenericArgumentCollection, Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim mD As MethodDefinition = TryCast(Member, MethodDefinition)

        If mD IsNot Nothing Then Return GetCorrectMember(mD, Arguments, Emittable)
        If Member.OriginalMethod IsNot Nothing Then
            mD = TryCast(Member.OriginalMethod, MethodDefinition)
            If mD IsNot Nothing Then Return GetCorrectMember(mD, Arguments, Emittable)
        End If

        Throw New NotImplementedException
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MethodDefinition, ByVal Arguments As GenericArgumentCollection, Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim result As Mono.Cecil.MethodReference
        Dim parameters As Mono.Cecil.GenericParameterCollection = Member.GenericParameters
        Dim returnType As Mono.Cecil.TypeReference
        Dim reflectableMember As Mono.Cecil.MethodReference

        If Member.GenericParameters.Count = 0 Then Return Member

        returnType = CecilHelper.ResolveType(Member.ReturnType.ReturnType, parameters, Arguments)
        returnType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, returnType)
        result = New Mono.Cecil.MethodReference(Member.Name, Member.DeclaringType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember = New Mono.Cecil.MethodReference(Member.Name, Member.DeclaringType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember.OriginalMethod = Member
        result.OriginalMethod = Member

        For i As Integer = 0 To Member.GenericParameters.Count - 1
            result.GenericParameters.Add(Member.GenericParameters(i))
            reflectableMember.GenericParameters.Add(Member.GenericParameters(i))
        Next

        For i As Integer = 0 To Member.Parameters.Count - 1
            Dim pD As Mono.Cecil.ParameterDefinition = Member.Parameters(i)
            Dim pDType As Mono.Cecil.TypeReference
            pDType = InflateType(pD.ParameterType, parameters, Arguments)
            If pDType IsNot pD.ParameterType Then
                Dim newPD As Mono.Cecil.ParameterDefinition
                Dim pd2 As Mono.Cecil.TypeReference
                pDType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pDType)
                newPD = New ParameterDefinition(pD.Name, pD.Sequence, pD.Attributes, pDType)
                result.Parameters.Add(newPD)
                pd2 = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pD.ParameterType)
                reflectableMember.Parameters.Add(New ParameterDefinition(pD.Name, pD.Sequence, pD.Attributes, pd2))
            Else
                result.Parameters.Add(pD)
                reflectableMember.Parameters.Add(pD)
            End If
        Next

        result.Annotations.Add("MemberInReflection", reflectableMember)

        Return result
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MethodDefinition, ByVal Type As Mono.Cecil.TypeReference, Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim result As Mono.Cecil.MethodReference
        Dim tD As Mono.Cecil.TypeDefinition = CecilHelper.FindDefinition(Type)
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim returnType As Mono.Cecil.TypeReference
        Dim reflectableMember As Mono.Cecil.MethodReference

        If genericType Is Nothing Then Return Member

        If Emittable Then
            returnType = Member.ReturnType.ReturnType
        Else
            returnType = CecilHelper.ResolveType(Member.ReturnType.ReturnType, tD.GenericParameters, genericType.GenericArguments)
        End If
        returnType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, returnType)
        result = New Mono.Cecil.MethodReference(Member.Name, genericType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember = New Mono.Cecil.MethodReference(Member.Name, genericType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember.OriginalMethod = Member
        result.OriginalMethod = Member

        'This is weird, but there is a bug in the lazy reflection reader which 
        'causes parameter types to not be read. Reading the method body
        'will cause the parameter types to be read.
        'TODO: Fix the lazy reflection reader bug.
        'This shows up when getting System.Collection.Generic.Queue(of String)'s constructors, the third ctor
        '(taking a IEnumerable(of string)) shows up as IEnumerable() (no GenericParameters in the inflated type)
        'Test case: Cecil/LazyBug1.vb
        Dim cilbody As Mono.Cecil.Cil.MethodBody = Member.Body

        For i As Integer = 0 To Member.Parameters.Count - 1
            Dim pD As Mono.Cecil.ParameterDefinition = Member.Parameters(i)
            Dim pDType As Mono.Cecil.TypeReference
            pDType = InflateType(pD.ParameterType, Type)
            If pDType IsNot pD.ParameterType Then
                Dim newPD As Mono.Cecil.ParameterDefinition
                Dim pd2 As Mono.Cecil.TypeReference
                pDType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pDType)
                newPD = New ParameterDefinition(pD.Name, pD.Sequence, pD.Attributes, pDType)
                result.Parameters.Add(newPD)
                pd2 = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pD.ParameterType)
                reflectableMember.Parameters.Add(New ParameterDefinition(pD.Name, pD.Sequence, pD.Attributes, pd2))
            Else
                result.Parameters.Add(pD)
                reflectableMember.Parameters.Add(pD)
            End If
        Next

        result.Annotations.Add("MemberInReflection", reflectableMember)

        Return result
    End Function

    Public Shared Function MakeEmittable(ByVal Method As MethodReference) As MethodReference
        Dim genM As GenericInstanceMethod = TryCast(Method, GenericInstanceMethod)
        Dim tG As GenericInstanceType = TryCast(Method.DeclaringType, GenericInstanceType)

        If genM Is Nothing AndAlso tG Is Nothing Then
            Return Method
        End If

        Dim mD As MethodDefinition = FindDefinition(Method)
        If mD Is Nothing Then Return Method

        If genM IsNot Nothing Then
            Dim result As New GenericInstanceMethod(Helper.GetMethodOrMethodReference(BaseObject.m_Compiler, mD))
            result.OriginalMethod = mD
            For i As Integer = 0 To genM.GenericArguments.Count - 1
                result.GenericArguments.Add(Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, genM.GenericArguments(i)))
            Next
            Return result
        Else
            Dim result As MethodReference
            Dim ret As Mono.Cecil.TypeReference
            Dim args As New Mono.Cecil.GenericArgumentCollection(Nothing)
            For i As Integer = 0 To mD.DeclaringType.GenericParameters.Count - 1
                args.Add(mD.DeclaringType.GenericParameters(i))
            Next
            ret = CecilHelper.InflateType(mD.ReturnType.ReturnType, mD.DeclaringType.GenericParameters, args)
            ret = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, ret)
            result = New MethodReference(Method.Name, Method.DeclaringType, ret, Method.HasThis, Method.ExplicitThis, Method.CallingConvention)
            For i As Integer = 0 To mD.Parameters.Count - 1
                Dim pType As Mono.Cecil.TypeReference
                pType = CecilHelper.InflateType(mD.Parameters(i).ParameterType, mD.DeclaringType.GenericParameters, args)
                pType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pType)
                result.Parameters.Add(New ParameterDefinition(pType))
            Next
            Return result
        End If
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

        For i As Integer = 0 To Attributes.Count - 1
            Dim attrib As CustomAttribute = Attributes(i)
            If Helper.CompareType(AttributeType, attrib.Constructor.DeclaringType) Then
                If result Is Nothing Then result = New Mono.Cecil.CustomAttributeCollection(Attributes.Container)
                result.Add(attrib)
            End If
        Next

        Return result
    End Function

    Public Shared Function GetAttributeCtorString(ByVal Attrib As CustomAttribute, ByVal index As Integer) As String
        Dim result As String
        If Attrib.ConstructorParameters.Count - 1 < index Then Return Nothing
        result = TryCast(Attrib.ConstructorParameters(index), String)
        Return result
    End Function

    Public Shared Function IsDefined(ByVal CustomAttributes As CustomAttributeCollection, ByVal Type As TypeReference) As Boolean
        For i As Integer = 0 To CustomAttributes.Count - 1
            Dim Attribute As CustomAttribute = CustomAttributes(i)
            If Helper.CompareType(Attribute.Constructor.DeclaringType, Type) Then Return True
        Next
        Return False
    End Function

    Public Shared Function GetAttributes(ByVal CustomAttributes As CustomAttributeCollection, ByVal Type As TypeReference) As CustomAttributeCollection
        Dim result As CustomAttributeCollection = Nothing
        For i As Integer = 0 To CustomAttributes.Count - 1
            Dim Attribute As CustomAttribute = CustomAttributes(i)
            If Helper.CompareType(Attribute.Constructor.DeclaringType, Type) Then
                If result Is Nothing Then result = New CustomAttributeCollection(CustomAttributes.Container)
                result.Add(Attribute)
            End If
        Next
        Return result
    End Function

    Public Shared Function IsGenericParameter(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.GenericParameter
    End Function

    Public Shared Function IsGenericType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, GenericInstanceType)
        If genericType IsNot Nothing Then Return True
        If Type.GenericParameters Is Nothing OrElse Type.GenericParameters.Count = 0 Then Return False
        If Type.GenericParameters IsNot Nothing AndAlso Type.GenericParameters.Count > 0 Then Return True
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
            Return GetTypes(tR.GenericArguments)
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
        Return Method.GenericParameters.Count > 0
        Throw New NotImplementedException
    End Function

    'Cecil's ValueType property returns true for arrays of value types
    Public Shared Function IsValueType(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        If TypeOf Type Is ArrayType Then Return False
        If TypeOf Type Is ReferenceType Then Return False
        Return Type.IsValueType
    End Function

    Public Shared Function IsByRef(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.ReferenceType
    End Function

    Public Shared Function IsArray(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.ArrayType
    End Function

    Public Shared Function GetInterfaces(ByVal Type As Mono.Cecil.TypeReference, ByVal checkBase As Boolean) As Mono.Cecil.InterfaceCollection
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim result As Mono.Cecil.InterfaceCollection
        Dim tmp As Mono.Cecil.TypeReference
        Dim tD As Mono.Cecil.TypeDefinition
        Dim tG As Mono.Cecil.GenericParameter = TryCast(Type, Mono.Cecil.GenericParameter)

        If tG IsNot Nothing Then
            If tG.Constraints.Count = 0 Then Return Nothing
            result = New Mono.Cecil.InterfaceCollection(Nothing)
            For i As Integer = 0 To tG.Constraints.Count - 1
                For Each t As TypeReference In GetInterfaces(tG.Constraints(i), checkBase)
                    result.Add(t)
                Next
            Next
            Return result
        End If

        Dim arrD As Mono.Cecil.ArrayType = TryCast(Type, Mono.Cecil.ArrayType)
        If arrD IsNot Nothing Then
            result = New Mono.Cecil.InterfaceCollection(Nothing)
            For Each tp As TypeReference In GetInterfaces(BaseObject.m_Compiler.TypeCache.System_Array, False)
                result.Add(tp)
            Next
            For Each tp As TypeDefinition In New TypeDefinition() {BaseObject.m_Compiler.TypeCache.System_Collections_Generic_ICollection1, BaseObject.m_Compiler.TypeCache.System_Collections_Generic_IEnumerable1, BaseObject.m_Compiler.TypeCache.System_Collections_Generic_IList1}
                Dim newTP As New GenericInstanceType(tp)
                newTP.GenericArguments.Add(arrD.ElementType)
                result.Add(newTP)
            Next
            Return result
        End If

        tD = FindDefinition(Type)

        result = New Mono.Cecil.InterfaceCollection(tD)
        For i As Integer = 0 To tD.Interfaces.Count - 1
            result.Add(InflateType(tD.Interfaces(i), Type))
        Next

        If genericType IsNot Nothing Then
            For i As Integer = 0 To result.Count - 1
                tmp = CecilHelper.ResolveType(result(i), genericType.ElementType.GenericParameters, genericType.GenericArguments)
                result.Item(i) = tmp
            Next
        End If

        If checkBase Then
            Dim bT As Mono.Cecil.TypeReference

            bT = tD.BaseType
            If bT IsNot Nothing Then
                If genericType IsNot Nothing Then
                    bT = CecilHelper.GetCorrectMember(bT, genericType)
                End If

                For Each t As Mono.Cecil.TypeReference In GetInterfaces(bT, checkBase)
                    result.Add(t)
                Next
            End If
        End If

        Return result
    End Function

    Public Shared Function GetElementType(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim rT As Mono.Cecil.ReferenceType = TryCast(Type, Mono.Cecil.ReferenceType)
        If rT IsNot Nothing Then Return rT.ElementType
        Dim aT As Mono.Cecil.ArrayType = TryCast(Type, Mono.Cecil.ArrayType)
        If aT IsNot Nothing Then Return aT.ElementType
        Throw New InternalException
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
        Dim pD As Mono.Cecil.PropertyDefinition = FindDefinition(Prop)
        Dim result As MethodReference = pD.GetMethod
        result = GetCorrectMember(result, Prop.DeclaringType)
        Return result
    End Function

    Public Shared Function GetSetMethod(ByVal Prop As PropertyReference) As MethodReference
        Dim pD As Mono.Cecil.PropertyDefinition = FindDefinition(Prop)
        Dim result As MethodReference = pD.SetMethod
        result = GetCorrectMember(result, Prop.DeclaringType)
        Return result
    End Function

    Public Shared Function IsClass(ByVal Type As TypeReference) As Boolean
        Dim tD As Mono.Cecil.TypeDefinition
        If IsValueType(Type) Then Return False
        If TypeOf Type Is Mono.Cecil.ArrayType Then Return True
        tD = FindDefinition(Type)
        Return tD.IsClass
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
            For i As Integer = 0 To BaseObject.m_Compiler.TypeManager.CecilAssemblies.Count - 1
                asm = BaseObject.m_Compiler.TypeManager.CecilAssemblies(i)
                If Helper.CompareNameOrdinal(asm.Name.FullName, name.FullName) Then
                    _assemblies(name.Name) = asm
                    Return asm
                End If
            Next
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
        Return GetField(type.Fields, field)
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
        Dim pD As PropertyDefinition

        If method.Annotations.Contains("OriginalProperty") Then
            pD = DirectCast(method.Annotations("OriginalProperty"), PropertyDefinition)
            Return pD
        End If

        pD = TryCast(method, PropertyDefinition)

        If pD IsNot Nothing Then Return pD

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
        If a.GenericParameters.Count <> b.GenericParameters.Count Then Return False
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
