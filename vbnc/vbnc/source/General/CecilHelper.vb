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

' Why custom collection instead of list<>?
' No way to get an inflated propertydefinition/propertyreference
' don't add nested types in the module types list, though nestedtype.module isn't set when adding it to type.nestedtypes

Imports Mono.Cecil
Imports System.Diagnostics

Public Class CecilHelper
#If DEBUG Then
    Public Shared Sub Test(ByVal file As String)
        Dim ass As AssemblyDefinition = AssemblyDefinition.ReadAssembly(file, New ReaderParameters(ReadingMode.Immediate))
        Console.WriteLine(ass.ToString())
    End Sub
#End If

    Public Shared Sub GetConstructors(ByVal Type As Mono.Cecil.TypeReference, ByVal result As Mono.Collections.Generic.Collection(Of MethodReference))
        Dim tD As Mono.Cecil.TypeDefinition = FindDefinition(Type)
        For Each item As Mono.Cecil.MethodDefinition In tD.Methods
            If Not item.IsConstructor Then Continue For
            If item.IsStatic Then Continue For
            If Helper.CompareType(item.DeclaringType, Type) = False Then
                result.Add(GetCorrectMember(item, Type))
            Else
                result.Add(item)
            End If
        Next
    End Sub

    Public Shared Function GetConstructors(ByVal Type As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of MethodReference)
        Dim result As New Mono.Collections.Generic.Collection(Of MethodReference)
        GetConstructors(Type, result)
        Return result
    End Function

    Private Shared Sub AddRange(ByVal C As Mono.Collections.Generic.Collection(Of MemberReference), ByVal C2 As Mono.Collections.Generic.Collection(Of MemberReference))
        For i As Integer = 0 To C2.Count - 1
            C.Add(C2(i))
        Next
    End Sub

    Public Shared Function GetMembers(ByVal Type As Mono.Cecil.GenericParameter) As Mono.Collections.Generic.Collection(Of MemberReference)
        Dim result As New Mono.Collections.Generic.Collection(Of MemberReference)()

        For i As Integer = 0 To Type.Constraints.Count - 1
            AddRange(result, GetMembers(Type.Constraints(i)))
        Next
        AddRange(result, GetMembers(BaseObject.m_Compiler.TypeCache.System_Object))
        Return result
    End Function

    Public Shared Function GetMembers(ByVal Type As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of MemberReference)
        Dim tD As Mono.Cecil.TypeDefinition
        Dim result As Mono.Collections.Generic.Collection(Of MemberReference)

        Dim tG As Mono.Cecil.GenericParameter = TryCast(Type, GenericParameter)
        If tG IsNot Nothing Then Return GetMembers(tG)

        Dim arr As Mono.Cecil.ArrayType = TryCast(Type, ArrayType)
        If arr IsNot Nothing Then
            result = New Mono.Collections.Generic.Collection(Of MemberReference)()
            For Each member As MemberReference In GetMembers(BaseObject.m_Compiler.TypeCache.System_Array)
                'result.Add(GetCorrectMember(member, Type))
                result.Add(member)
            Next
            Return result
        End If

        tD = FindDefinition(Type)

        result = New Mono.Collections.Generic.Collection(Of MemberReference)(tD.Events.Count + tD.Methods.Count + tD.Properties.Count + tD.NestedTypes.Count + tD.Fields.Count)

        For i As Integer = 0 To tD.Events.Count - 1
            Dim item As EventDefinition = tD.Events(i)
            'I don't think events need to call GetCorrectMember
            result.Add(item)
        Next

        For i As Integer = 0 To tD.Methods.Count - 1
            Dim item As MethodReference = tD.Methods(i)
            If Helper.CompareType(item.DeclaringType, Type) = False Then item = GetCorrectMember(item, Type)
            result.Add(item)
        Next

        For i As Integer = 0 To tD.Properties.Count - 1
            Dim pd As PropertyDefinition = tD.Properties(i)
            Dim item As PropertyReference = pd
            If Helper.CompareType(item.DeclaringType, Type) = False Then item = GetCorrectMember(pd, Type)
            result.Add(item)
        Next

        For i As Integer = 0 To tD.NestedTypes.Count - 1
            Dim item As TypeReference = tD.NestedTypes(i)
            If Helper.CompareType(item.DeclaringType, Type) = False Then item = GetCorrectMember(item, Type)
            result.Add(item)
        Next

        For i As Integer = 0 To tD.Fields.Count - 1
            Dim fd As FieldDefinition = tD.Fields(i)
            Dim item As FieldReference = fd
            If Helper.CompareType(item.DeclaringType, Type) = False Then item = GetCorrectMember(fd, Type)
            result.Add(item)
        Next

        Return result
    End Function

    Public Shared Function IsValidType(ByVal type As TypeReference) As Boolean
        Dim arrayType As ArrayType

        If type Is Nothing Then Return True
        If TypeOf type Is PointerType Then Return False

        arrayType = TryCast(type, ArrayType)
        If arrayType IsNot Nothing AndAlso IsValidType(arrayType.ElementType) = False Then Return False

        Return True
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

    Public Shared Function GetCorrectMember(ByVal Member As Mono.Cecil.TypeDefinition, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.GenericInstanceType = Nothing
        Dim args As New Generic.List(Of Mono.Cecil.TypeReference)
        Dim any_change As Boolean
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)

        If genericType Is Nothing Then Return Member

        result = New Mono.Cecil.GenericInstanceType(Member)
        ' result.DeclaringType = FindDefinition(Type)

        Dim tGI As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        If Member.DeclaringType IsNot Nothing AndAlso tGI IsNot Nothing AndAlso Helper.CompareType(Member.DeclaringType, tGI.ElementType) Then
            'Nested generic type
            For i As Integer = 0 To tGI.GenericArguments.Count - 1
                result.GenericArguments.Add(Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, tGI.GenericArguments(i)))
            Next
            Return result
        End If

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
        result = New Mono.Cecil.PropertyDefinition(Member.Name, Member.Attributes, propertyType)
        result.DeclaringType = genericType
        result.SetMethod = setMethod
        result.GetMethod = getMethod
        result.Annotations.Add("OriginalProperty", Member)
        Return result
    End Function

    Public Shared Sub GetGenericArgsAndParams(ByVal Type As TypeReference, ByRef GenericParameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByRef GenericArguments As Mono.Collections.Generic.Collection(Of TypeReference))
        Dim declType As TypeReference = Nothing
        Dim genericType As GenericInstanceType
        Dim genericTypeDefinition As TypeDefinition
        Dim cloned As Boolean

        Do
            If declType Is Nothing Then
                declType = Type
            Else
                declType = declType.DeclaringType
            End If

            genericType = TryCast(declType, Mono.Cecil.GenericInstanceType)

            If genericType IsNot Nothing Then
                genericTypeDefinition = CecilHelper.FindDefinition(genericType)

                Helper.Assert(genericType.GenericArguments.Count = genericTypeDefinition.GenericParameters.Count)

                If GenericArguments Is Nothing Then
                    GenericArguments = genericType.GenericArguments
                    GenericParameters = genericTypeDefinition.GenericParameters
                Else
                    If cloned = False Then
                        Dim tmp1 As New Mono.Collections.Generic.Collection(Of TypeReference)
                        Dim tmp2 As New Mono.Collections.Generic.Collection(Of GenericParameter)
                        For i As Integer = 0 To GenericArguments.Count - 1
                            tmp1.Add(GenericArguments(i))
                            tmp2.Add(GenericParameters(i))
                        Next
                        GenericArguments = tmp1
                        GenericParameters = tmp2
                        cloned = True
                    End If

                    For i As Integer = 0 To genericType.GenericArguments.Count - 1
                        GenericArguments.Insert(i, genericType.GenericArguments(i))
                        GenericParameters.Insert(i, genericTypeDefinition.GenericParameters(i))
                    Next
                End If
            End If
        Loop While declType.IsNested
    End Sub

    Public Shared Function InflateType(ByVal original As TypeReference, ByVal container As TypeReference) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ByReferenceType = TryCast(original, ByReferenceType)
        Dim genericType As GenericInstanceType = TryCast(original, GenericInstanceType)
        Dim originalDef As TypeDefinition = TryCast(original, TypeDefinition)
        Dim parameters As Mono.Collections.Generic.Collection(Of GenericParameter)
        Dim arguments As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim genericCollection As GenericInstanceType = TryCast(container, GenericInstanceType)
        Dim containerDef As TypeDefinition

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

        containerDef = CecilHelper.FindDefinition(container)

        If containerDef IsNot Nothing Then
            parameters = containerDef.GenericParameters
        Else
            parameters = genericCollection.ElementType.GenericParameters
        End If
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
            'originalDef = CecilHelper.FindDefinition(original)
            'For i As Integer = 0 To originalDef.GenericParameters.Count - 1
            '    For j As Integer = 0 To parameters.Count - 1
            '        If parameters(j) Is originalDef.GenericParameters(i) Then
            '            result.GenericArguments.Add(InflateType(originalDef.GenericParameters(i), container))
            '            Exit For
            '        End If
            '    Next
            'Next
            For i As Integer = 0 To genericType.GenericArguments.Count - 1
                result.GenericArguments.Add(InflateType(genericType.GenericArguments(i), container))
            Next
            'Helper.Assert(result.GenericArguments.Count = parameters.Count)
            Return result
        End If

        If spec IsNot Nothing Then
            Dim resolved As TypeReference = InflateType(spec.ElementType, container)

            If resolved Is spec.ElementType Then
                Return spec
            End If


            If array IsNot Nothing Then
                Return New ArrayType(resolved, array.Dimensions.Count)
            ElseIf reference IsNot Nothing Then
                Return New ByReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        Else
            Return original
        End If
    End Function

    Public Shared Function InflateType(ByVal original As TypeReference, ByVal parameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal arguments As Mono.Collections.Generic.Collection(Of TypeReference)) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ByReferenceType = TryCast(original, ByReferenceType)
        Dim genericType As GenericInstanceType = TryCast(original, GenericInstanceType)
        Dim originalDef As TypeDefinition = TryCast(original, TypeDefinition)

        If parameters Is Nothing AndAlso arguments Is Nothing Then Return original

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
            Dim result As New GenericInstanceType(CecilHelper.FindDefinition(genericType.ElementType))
            For i As Integer = 0 To result.ElementType.GenericParameters.Count - 1
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
                Return New ArrayType(resolved, array.Dimensions.Count)
            ElseIf reference IsNot Nothing Then
                Return New ByReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        Else
            Return original
        End If
    End Function

    Public Shared Function ResolveType(ByVal original As TypeReference, ByVal parameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal arguments As Mono.Collections.Generic.Collection(Of TypeReference)) As TypeReference
        Dim spec As TypeSpecification = TryCast(original, TypeSpecification)
        Dim array As ArrayType = TryCast(original, ArrayType)
        Dim reference As ByReferenceType = TryCast(original, ByReferenceType)
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
                Return New ArrayType(resolved, array.Dimensions.Count)
            ElseIf (reference IsNot Nothing) Then
                Return New ByReferenceType(resolved)
            Else
                Throw New System.NotImplementedException()
            End If
        End If

        For i As Integer = 0 To parameters.Count - 1
            If parameters(i) Is original Then
                Return arguments(i)
            End If
        Next

        For i As Integer = 0 To arguments.Count - 1
            If arguments(i) Is original Then Return arguments(i)
        Next

        If original.IsNested Then
            Dim parentType As TypeReference = InflateType(original.DeclaringType, parameters, arguments)
            If parentType IsNot original Then
                Return Compiler.CurrentCompiler.ModuleBuilderCecil.Import(FindDefinition(original))
            End If
        End If

        Return original
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As FieldDefinition, ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.FieldReference
        Dim result As Mono.Cecil.FieldReference
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim elementType As Mono.Cecil.TypeDefinition
        Dim fieldType As Mono.Cecil.TypeReference

        If genericType Is Nothing Then
            Return Member
        End If

        elementType = CecilHelper.FindDefinition(genericType.ElementType)
        fieldType = CecilHelper.ResolveType(Member.FieldType, elementType.GenericParameters, genericType.GenericArguments)

        'If fieldType IsNot Member.FieldType Then
        fieldType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, fieldType)

        result = New FieldReference(Member.Name, fieldType, Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, Member.DeclaringType))
        result.Annotations.Add("MemberInReflection", New FieldReference(Member.Name, Member.FieldType, genericType))
        Return result
        'Else
        '    Return Member
        'End If

    End Function


    '
    'Emittable: to get a member as cecil/cil wants it
    ' - 1Declarations/GenericProperty1 shows one case where vbnc and cecil/cil wants different things (method return type shouldn't be inflated for cecil/cil)
    '
    '

    Public Shared Function GetCorrectMember(ByVal Member As MethodReference, ByVal Type As TypeReference, Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim mD As MethodDefinition = TryCast(Member, MethodDefinition)

        If mD IsNot Nothing Then Return GetCorrectMember(mD, Type, Emittable)

        mD = FindDefinition(Member)
        If mD IsNot Nothing Then Return GetCorrectMember(mD, Type, Emittable)

        Throw New NotImplementedException
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MethodReference, ByVal Arguments As Mono.Collections.Generic.Collection(Of TypeReference), Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim mD As MethodDefinition = TryCast(Member, MethodDefinition)

        If mD IsNot Nothing Then Return GetCorrectMember(mD, Arguments, Emittable)
        If Member.OriginalMethod IsNot Nothing Then
            mD = TryCast(Member.OriginalMethod, MethodDefinition)
            If mD IsNot Nothing Then Return GetCorrectMember(mD, Arguments, Emittable)
        End If

        Throw New NotImplementedException
    End Function

    Shared Function CloneGenericParameter(gp As GenericParameter) As GenericParameter
        Dim rv As New GenericParameter(gp.Name, gp.Owner)
        rv.Attributes = gp.Attributes
        If gp.HasConstraints Then
            For i As Integer = 0 To gp.Constraints.Count - 1
                rv.Constraints.Add(gp.Constraints(i))
            Next
        End If
        rv.HasDefaultConstructorConstraint = gp.HasDefaultConstructorConstraint
        rv.HasNotNullableValueTypeConstraint = gp.HasNotNullableValueTypeConstraint
        rv.HasReferenceTypeConstraint = gp.HasReferenceTypeConstraint
        rv.IsContravariant = gp.IsContravariant
        rv.IsCovariant = gp.IsCovariant
        rv.IsNonVariant = gp.IsNonVariant

        Return rv
    End Function

    Public Shared Function GetCorrectMember(ByVal Member As MethodDefinition, ByVal Arguments As Mono.Collections.Generic.Collection(Of TypeReference), Optional ByVal Emittable As Boolean = False) As Mono.Cecil.MethodReference
        Dim result As Mono.Cecil.MethodReference
        Dim parameters As Mono.Collections.Generic.Collection(Of GenericParameter) = Member.GenericParameters
        Dim returnType As Mono.Cecil.TypeReference
        Dim reflectableMember As Mono.Cecil.MethodReference

        If Member.GenericParameters.Count = 0 Then Return Member

        returnType = CecilHelper.ResolveType(Member.ReturnType, parameters, Arguments)
        returnType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, returnType)
        result = New Mono.Cecil.MethodReference(Member.Name, Member.DeclaringType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember = New Mono.Cecil.MethodReference(Member.Name, Member.DeclaringType, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember.OriginalMethod = Member
        result.OriginalMethod = Member

        For i As Integer = 0 To Member.GenericParameters.Count - 1
            result.GenericParameters.Add(CloneGenericParameter(Member.GenericParameters(i)))
            reflectableMember.GenericParameters.Add(CloneGenericParameter(Member.GenericParameters(i)))
        Next

        For i As Integer = 0 To Member.Parameters.Count - 1
            Dim pD As Mono.Cecil.ParameterDefinition = Member.Parameters(i)
            Dim pDType As Mono.Cecil.TypeReference
            pDType = InflateType(pD.ParameterType, parameters, Arguments)
            If pDType IsNot pD.ParameterType Then
                Dim newPD As Mono.Cecil.ParameterDefinition
                Dim pd2 As Mono.Cecil.TypeReference
                pDType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pDType)
                newPD = New ParameterDefinition(pD.Name, pD.Attributes, pDType)
                result.Parameters.Add(newPD)
                pd2 = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pD.ParameterType)
                reflectableMember.Parameters.Add(New ParameterDefinition(pD.Name, pD.Attributes, pd2))
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
        Dim genericArguments As Mono.Collections.Generic.Collection(Of TypeReference) = Nothing
        Dim genericParameters As Mono.Collections.Generic.Collection(Of GenericParameter) = Nothing
        Dim returnType As Mono.Cecil.TypeReference
        Dim reflectableMember As Mono.Cecil.MethodReference
        Dim declType As TypeReference = Nothing

        GetGenericArgsAndParams(Type, genericParameters, genericArguments)

        'If genericType Is Nothing Then
        '    Dim declType As TypeReference = Type
        '    While declType.IsNested
        '        Dim genType As GenericInstanceType = TryCast(declType, Mono.Cecil.GenericInstanceType)
        '        If genType IsNot Nothing Then
        '            If genericArguments Is Nothing Then genericArguments = New GenericArgumentCollection(Nothing)
        '            For Each arg As TypeReference In genType.GenericArguments
        '                genericArguments.Add(arg)
        '            Next
        '        End If
        '    End While
        '    Return Member
        'Else
        '    genericArguments = genericType.GenericArguments
        '    genericParameters = tD.GenericParameters
        'End If

        If genericParameters Is Nothing AndAlso genericArguments Is Nothing AndAlso tD Is Type AndAlso tD.Module Is Compiler.CurrentCompiler.ModuleBuilderCecil Then
            Return Member
        End If

        If Emittable Then
            returnType = Member.ReturnType
        Else
            returnType = CecilHelper.InflateType(Member.ReturnType, genericParameters, genericArguments)
        End If
        result = New Mono.Cecil.MethodReference(Member.Name, Type, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember = New Mono.Cecil.MethodReference(Member.Name, Type, returnType, Member.HasThis, Member.ExplicitThis, Member.CallingConvention)
        reflectableMember.OriginalMethod = Member
        result.OriginalMethod = Member

        'If Member.DeclaringType.GenericParameters.Count > 0 AndAlso Not False Then
        '    Dim tmp As New GenericInstanceType(Member.DeclaringType)
        '    For i As Integer = 0 To Member.DeclaringType.GenericParameters.Count - 1
        '        tmp.GenericArguments.Add(Member.DeclaringType.GenericParameters(i))
        '    Next
        '    result.DeclaringType = tmp
        'End If

        For i As Integer = 0 To Member.Parameters.Count - 1
            Dim pD As Mono.Cecil.ParameterDefinition = Member.Parameters(i)
            Dim pDType As Mono.Cecil.TypeReference
            pDType = InflateType(pD.ParameterType, genericParameters, genericArguments)
            If pDType IsNot pD.ParameterType Then
                Dim newPD As Mono.Cecil.ParameterDefinition
                Dim pd2 As Mono.Cecil.TypeReference
                pDType = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pDType)
                newPD = New ParameterDefinition(pD.Name, pD.Attributes, pDType)
                result.Parameters.Add(newPD)
                pd2 = Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, pD.ParameterType)
                reflectableMember.Parameters.Add(New ParameterDefinition(pD.Name, pD.Attributes, pd2))
            Else
                result.Parameters.Add(pD)
                reflectableMember.Parameters.Add(pD)
            End If
        Next

        result.Annotations.Add("MemberInReflection", reflectableMember)

        Return result
    End Function

    Public Shared Function MakeEmittable(ByVal Method As MethodReference) As MethodReference
        Dim result As MethodReference
        Dim genM As GenericInstanceMethod = TryCast(Method, GenericInstanceMethod)
        Dim tG As GenericInstanceType = TryCast(Method.DeclaringType, GenericInstanceType)

        If genM Is Nothing AndAlso tG Is Nothing Then
            If Method.DeclaringType.GenericParameters.Count > 0 Then
                tG = New GenericInstanceType(Method.DeclaringType)
                For i As Integer = 0 To Method.DeclaringType.GenericParameters.Count - 1
                    tG.GenericArguments.Add(Method.DeclaringType.GenericParameters(i))
                Next

                Dim mR As New Mono.Cecil.MethodReference(Method.Name, tG, Method.ReturnType, Method.HasThis, Method.ExplicitThis, Method.CallingConvention)
                For i As Integer = 0 To Method.Parameters.Count - 1
                    Dim param As Mono.Cecil.ParameterDefinition
                    param = New Mono.Cecil.ParameterDefinition(Method.Parameters(i).ParameterType)
                    mR.Parameters.Add(param)
                Next
                Return mR
            End If
        End If

        Dim mD As MethodDefinition = FindDefinition(Method)

        If mD Is Nothing Then
            If TypeOf Method.DeclaringType Is ArrayType Then
                Dim arrayType As TypeReference
                arrayType = Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, Method.DeclaringType)
                result = New MethodReference(Method.Name, Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, Method.DeclaringType), Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, Method.ReturnType), Method.HasThis, Method.ExplicitThis, Method.CallingConvention)
                For i As Integer = 0 To Method.Parameters.Count - 1
                    Dim pType As Mono.Cecil.TypeReference
                    pType = Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, Method.Parameters(i).ParameterType)
                    If pType Is Method.Parameters(i).ParameterType Then
                        result.Parameters.Add(Method.Parameters(i))
                    Else
                        result.Parameters.Add(New ParameterDefinition(pType))
                    End If
                Next
                Return result
            End If
            Helper.Assert(mD IsNot Nothing)
            Return Nothing
        End If

        If mD Is Method AndAlso mD.DeclaringType.Module Is Compiler.CurrentCompiler.ModuleBuilderCecil Then
            Return mD
        End If

        If genM IsNot Nothing Then
            Dim gimResult As New GenericInstanceMethod(Helper.GetMethodOrMethodReference(BaseObject.m_Compiler, mD))
            gimResult.OriginalMethod = mD
            gimResult.ReturnType = Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, mD.ReturnType)
            For i As Integer = 0 To genM.GenericArguments.Count - 1
                gimResult.GenericArguments.Add(Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, genM.GenericArguments(i)))
            Next
            Return gimResult
        End If

        result = New MethodReference(Method.Name, Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, Method.DeclaringType), Helper.GetTypeOrTypeReference(BaseObject.m_Compiler, mD.ReturnType), Method.HasThis, Method.ExplicitThis, Method.CallingConvention)
        For i As Integer = 0 To mD.Parameters.Count - 1
            Dim pType As Mono.Cecil.TypeReference
            pType = Helper.GetTypeOrTypeReference(Compiler.CurrentCompiler, mD.Parameters(i).ParameterType)
            If pType Is mD.Parameters(i).ParameterType Then
                result.Parameters.Add(mD.Parameters(i))
            Else
                result.Parameters.Add(New ParameterDefinition(pType))
            End If
        Next
        If mD.GenericParameters IsNot Nothing AndAlso mD.GenericParameters.Count > 0 Then
            For i As Integer = 0 To mD.GenericParameters.Count - 1
                result.GenericParameters.Add(mD.GenericParameters(i))
            Next
        End If
        Return result
    End Function

    Public Shared Function GetAssemblyRef(ByVal Type As Mono.Cecil.TypeReference) As Mono.Cecil.AssemblyNameReference
        Dim modDef As ModuleDefinition = TryCast(Type.Scope, ModuleDefinition)
        If modDef IsNot Nothing Then Return modDef.Assembly.Name

        Dim assemblyRef As Mono.Cecil.AssemblyNameReference = TryCast(Type.Scope, AssemblyNameReference)
        If assemblyRef IsNot Nothing Then Return assemblyRef

        Throw New NotImplementedException
    End Function

    Public Shared Function MakeByRefType(ByVal Type As Mono.Cecil.TypeReference) As ByReferenceType
        Return New ByReferenceType(Type)
    End Function

    Public Shared Function MakeGenericMethod(ByVal Method As MethodReference, ByVal Types() As Mono.Cecil.TypeReference) As Mono.Cecil.GenericInstanceMethod
        Dim result As New Mono.Cecil.GenericInstanceMethod(Method)
        For i As Integer = 0 To Types.Length - 1
            result.GenericArguments.Add(Types(i))
        Next
        Return result
    End Function

    Public Shared Function GetNestedType(ByVal Type As TypeReference, ByVal Name As String) As TypeReference
        Dim tD As TypeDefinition = FindDefinition(Type)
        For i As Integer = 0 To tD.NestedTypes.Count - 1
            If Helper.CompareName(tD.NestedTypes(i).Name, Name) Then Return tD.NestedTypes(i)
        Next
        If tD.BaseType Is Nothing Then Return Nothing
        Return GetNestedType(tD.BaseType, Name)
    End Function

    Public Shared Function GetNestedTypes(ByVal Type As TypeReference) As Mono.Collections.Generic.Collection(Of TypeDefinition)
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

    Public Shared Function IsStatic(ByVal [Property] As PropertyReference) As Boolean
        Dim pd As PropertyDefinition = FindDefinition([Property])

        If pd.GetMethod IsNot Nothing Then Return IsStatic(pd.GetMethod)
        If pd.SetMethod IsNot Nothing Then Return IsStatic(pd.SetMethod)

        Return False
    End Function

    Public Shared Function MakeArrayType(ByVal Type As Mono.Cecil.TypeReference, Optional ByVal Ranks As Integer = 1) As Mono.Cecil.ArrayType
        Dim result As ArrayType
        result = New Mono.Cecil.ArrayType(Type, Ranks)
        Return result
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

    Public Shared Function GetCustomAttributes(ByVal Type As Mono.Cecil.TypeDefinition, ByVal AttributeType As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of CustomAttribute)
        Return GetCustomAttributes(Type.CustomAttributes, AttributeType)
    End Function

    Public Shared Function GetCustomAttributes(ByVal Attributes As Mono.Collections.Generic.Collection(Of CustomAttribute), ByVal AttributeType As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of CustomAttribute)
        Dim result As Mono.Collections.Generic.Collection(Of CustomAttribute) = Nothing

        For i As Integer = 0 To Attributes.Count - 1
            Dim attrib As CustomAttribute = Attributes(i)
            If Helper.CompareType(AttributeType, attrib.Constructor.DeclaringType) Then
                If result Is Nothing Then result = New Mono.Collections.Generic.Collection(Of CustomAttribute)()
                result.Add(attrib)
            End If
        Next

        Return result
    End Function

    Public Shared Function GetAttributeCtorString(ByVal Attrib As CustomAttribute, ByVal index As Integer) As String
        Dim result As String
        If Attrib.ConstructorArguments.Count - 1 < index Then Return Nothing
        result = TryCast(Attrib.ConstructorArguments(index).Value, String)
        Return result
    End Function

    Public Shared Function IsDefined(ByVal CustomAttributes As Mono.Collections.Generic.Collection(Of CustomAttribute), ByVal Type As TypeReference) As Boolean
        For i As Integer = 0 To CustomAttributes.Count - 1
            Dim Attribute As CustomAttribute = CustomAttributes(i)
            If Helper.CompareType(Attribute.Constructor.DeclaringType, Type) Then Return True
        Next
        Return False
    End Function

    Public Shared Function GetAttributes(ByVal CustomAttributes As Mono.Collections.Generic.Collection(Of CustomAttribute), ByVal Type As TypeReference) As Mono.Collections.Generic.Collection(Of CustomAttribute)
        Dim result As Mono.Collections.Generic.Collection(Of CustomAttribute) = Nothing
        For i As Integer = 0 To CustomAttributes.Count - 1
            Dim Attribute As CustomAttribute = CustomAttributes(i)
            If Helper.CompareType(Attribute.Constructor.DeclaringType, Type) Then
                If result Is Nothing Then result = New Mono.Collections.Generic.Collection(Of CustomAttribute)()
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

    Public Shared Function GetTypes(ByVal Params As Mono.Collections.Generic.Collection(Of GenericParameter)) As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim result As New Mono.Collections.Generic.Collection(Of TypeReference)(Params.Count)
        For i As Integer = 0 To Params.Count - 1
            result.Add(Params(i))
        Next
        Return result
    End Function

    Public Shared Function GetTypes(ByVal Params As Mono.Collections.Generic.Collection(Of TypeReference)) As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim result As New Mono.Collections.Generic.Collection(Of TypeReference)(Params.Count)
        For i As Integer = 0 To Params.Count - 1
            result.Add(Params(i))
        Next
        Return result
    End Function

    Public Shared Function GetGenericArguments(ByVal Type As Mono.Cecil.TypeReference) As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim tR As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        If tR Is Nothing Then
            Return GetTypes(Type.GenericParameters)
        Else
            Return GetTypes(tR.GenericArguments)
        End If
    End Function

    Public Shared Function GetGenericArguments(ByVal Method As Mono.Cecil.MethodReference) As Mono.Collections.Generic.Collection(Of TypeReference)
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
        If TypeOf Type Is ByReferenceType Then Return False
        Return Type.IsValueType
    End Function

    Public Shared Function CreateNullableType(ByVal Context As BaseObject, ByVal Type As TypeReference, ByRef result As TypeReference) As Boolean
        If CecilHelper.IsValueType(Type) = False Then
            Dim gp As GenericParameter = TryCast(Type, GenericParameter)
            If gp Is Nothing OrElse gp.HasNotNullableValueTypeConstraint = False Then
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC33101, Context.Location, Helper.ToString(Context.Compiler, Type))
            End If
        End If

        Dim git As New GenericInstanceType(Context.Compiler.TypeCache.System_Nullable1)
        git.GenericArguments.Add(Type)
        result = git
        Return True
    End Function

    Public Shared Function IsNullable(ByVal Type As TypeReference) As Boolean
        Dim git As GenericInstanceType

        If Type Is Nothing Then Return False
        If Not Type.IsGenericInstance Then Return False

        If Helper.CompareNameOrdinal(Type.Name, "Nullable`1") = False Then Return False

        git = TryCast(Type, GenericInstanceType)
        If git Is Nothing Then Return False

        Return Helper.CompareType(Compiler.CurrentCompiler.TypeCache.System_Nullable1, git.ElementType)
    End Function

    Public Shared Function GetNulledType(ByVal Type As TypeReference) As TypeReference
        'No checking done, caller must call IsNullable first
        Dim git As GenericInstanceType = DirectCast(Type, GenericInstanceType)
        Return git.GenericArguments()(0)
    End Function

    Public Shared Function IsByRef(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is ByReferenceType
    End Function

    Public Shared Function IsArray(ByVal Type As Mono.Cecil.TypeReference) As Boolean
        Return TypeOf Type Is Mono.Cecil.ArrayType
    End Function

    Public Shared Function GetInterfaces(ByVal Type As Mono.Cecil.TypeReference, ByVal checkBase As Boolean) As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim genericType As Mono.Cecil.GenericInstanceType = TryCast(Type, Mono.Cecil.GenericInstanceType)
        Dim result As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim tmp As Mono.Cecil.TypeReference
        Dim tD As Mono.Cecil.TypeDefinition
        Dim tG As Mono.Cecil.GenericParameter = TryCast(Type, Mono.Cecil.GenericParameter)

        If tG IsNot Nothing Then
            If tG.Constraints.Count = 0 Then Return Nothing
            result = New Mono.Collections.Generic.Collection(Of TypeReference)
            For i As Integer = 0 To tG.Constraints.Count - 1
                For Each t As TypeReference In GetInterfaces(tG.Constraints(i), checkBase)
                    result.Add(t)
                Next
            Next
            Return result
        End If

        Dim arrD As Mono.Cecil.ArrayType = TryCast(Type, Mono.Cecil.ArrayType)
        If arrD IsNot Nothing Then
            result = New Mono.Collections.Generic.Collection(Of TypeReference)
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

        result = New Mono.Collections.Generic.Collection(Of TypeReference)
        For i As Integer = 0 To tD.Interfaces.Count - 1
            result.Add(InflateType(tD.Interfaces(i), Type))
        Next

        If genericType IsNot Nothing Then
            For i As Integer = 0 To result.Count - 1
                tmp = CecilHelper.ResolveType(result(i), CecilHelper.FindDefinition(genericType).GenericParameters, genericType.GenericArguments)
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
        Dim rT As ByReferenceType = TryCast(Type, ByReferenceType)
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
        Dim tG As Mono.Cecil.GenericParameter

        If IsValueType(Type) Then Return False
        If TypeOf Type Is Mono.Cecil.ArrayType Then Return True

        tG = TryCast(Type, Mono.Cecil.GenericParameter)
        If tG IsNot Nothing Then
            If tG.HasReferenceTypeConstraint Then Return True
            If tG.HasNotNullableValueTypeConstraint Then Return False
            Return True
        End If

        tD = FindDefinition(Type)

        Return tD.IsClass
    End Function

    Public Shared Function IsReferenceTypeOrGenericReferenceTypeParameter(ByVal Type As TypeReference) As Boolean
        Dim tg As GenericParameter = TryCast(Type, GenericParameter)
        Dim td As TypeDefinition

        If tg IsNot Nothing Then
            If tg.HasReferenceTypeConstraint Then Return True
            Return False
        End If

        td = FindDefinition(Type)
        Return (td.IsInterface OrElse td.IsClass) AndAlso td.IsValueType = False
    End Function

    Public Shared Function GetGenericParameterAttributes(ByVal Type As TypeReference) As GenericParameterAttributes
        Dim gt As Mono.Cecil.GenericParameter

        gt = DirectCast(Type, Mono.Cecil.GenericParameter)

        Return gt.Attributes
    End Function

    Public Shared Function IsInterface(ByVal Type As TypeReference) As Boolean
        If TypeOf Type Is ArrayType Then Return False
        If TypeOf Type Is GenericParameter Then Return False
        Return FindDefinition(Type).IsInterface
    End Function

    Public Shared Function FindDefinition(ByVal name As AssemblyNameReference) As AssemblyDefinition
        Return Compiler.CurrentCompiler.AssemblyResolver.Resolve(name)
    End Function

    Public Shared Function GetBaseType(ByVal Type As TypeReference) As TypeReference
        Dim result As Mono.Cecil.TypeReference
        Dim tD As Mono.Cecil.TypeDefinition

        tD = CecilHelper.FindDefinition(Type)

        If tD Is Nothing Then Return Nothing

        result = tD.BaseType

        If result Is Nothing Then Return Nothing

        result = CecilHelper.InflateType(result, Type)

        Return result
    End Function

    Public Shared Function FindField(ByVal fields As Mono.Collections.Generic.Collection(Of FieldDefinition), ByVal name As String) As FieldDefinition
        For i As Integer = 0 To fields.Count - 1
            If Helper.CompareNameOrdinal(fields(i).Name, name) Then Return fields(i)
        Next
        Return Nothing
    End Function

    Public Shared Function FindProperties(ByVal properties As Mono.Collections.Generic.Collection(Of PropertyDefinition), ByVal name As String) As Mono.Collections.Generic.Collection(Of PropertyDefinition)
        Dim result As Mono.Collections.Generic.Collection(Of PropertyDefinition) = Nothing
        For i As Integer = 0 To properties.Count - 1
            If Helper.CompareNameOrdinal(properties(i).Name, name) Then
                If result Is Nothing Then result = New Mono.Collections.Generic.Collection(Of PropertyDefinition)
                result.Add(properties(i))
            End If
        Next
        Return result
    End Function

    Public Shared Function FindConstructor(ByVal Methods As Mono.Collections.Generic.Collection(Of MethodDefinition), ByVal [Shared] As Boolean, ByVal parameters() As TypeReference) As MethodReference
        For i As Integer = 0 To Methods.Count - 1
            Dim mr As MethodDefinition = Methods(i)

            If mr.IsConstructor = False Then Continue For
            If mr.IsStatic Then
                If [Shared] Then Return mr
                Continue For
            End If
            If Helper.CompareTypes(Helper.GetParameterTypes(Compiler.CurrentCompiler, mr), parameters) Then Return mr
        Next
        Return Nothing
    End Function

    Public Shared Function FindDefinition(ByVal type As TypeReference) As TypeDefinition
        If type Is Nothing Then Return Nothing
        Dim tD As TypeDefinition = TryCast(type, TypeDefinition)
        If tD IsNot Nothing Then Return tD
        type = type.GetElementType
        If TypeOf type Is TypeDefinition Then
            Return DirectCast(type, TypeDefinition)
        End If
        Dim reference As AssemblyNameReference = TryCast(type.Scope, AssemblyNameReference)
        If reference IsNot Nothing Then
            Dim assembly As AssemblyDefinition = FindDefinition(reference)
            If type.IsNested Then
                Return assembly.MainModule.GetType(type.FullName)
            Else
                Return assembly.MainModule.GetType(type.Namespace, type.Name)
            End If
        End If
        Dim moduledef As ModuleDefinition = TryCast(type.Scope, ModuleDefinition)
        If moduledef IsNot Nothing Then
            Dim fn As String
            If type.IsNested Then
                fn = FindDefinition(type.DeclaringType).FullName + "/" + type.Name
                Return moduledef.GetType(fn)
            Else
                Return moduledef.GetType(type.Namespace, type.Name)
            End If
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
        Dim type As TypeDefinition

        If TypeOf method Is MethodDefinition Then Return DirectCast(method, MethodDefinition)

        'If TypeOf method.DeclaringType Is ArrayType Then
        '    type = Compiler.CurrentCompiler.TypeCache.System_Array
        'Else
        type = FindDefinition(method.DeclaringType)
        'End If
        If method.OriginalMethod IsNot Nothing Then
            method = method.OriginalMethod
        Else
            method = method.GetElementMethod
        End If

        If TypeOf method Is MethodDefinition Then Return DirectCast(method, MethodDefinition)

        Return GetMethod(type, method)
    End Function

    Public Shared Function FindDefinition(ByVal method As PropertyReference) As PropertyDefinition
        If method Is Nothing Then Return Nothing
        Dim pD As PropertyDefinition

        pD = TryCast(method, PropertyDefinition)
        If pD IsNot Nothing Then Return pD

        If method.Annotations.Contains("OriginalProperty") Then
            pD = DirectCast(method.Annotations("OriginalProperty"), PropertyDefinition)
            Return pD
        End If

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

    Public Shared Function AreSame(ByVal a As Mono.Collections.Generic.Collection(Of ParameterDefinition), ByVal b As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Boolean
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

