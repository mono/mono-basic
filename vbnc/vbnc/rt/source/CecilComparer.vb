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

Imports System.Collections.Generic
Imports Mono.Cecil

Public Class CecilComparer
    Private m_File1 As String
    Private m_File2 As String

    Private m_Assembly1 As AssemblyDefinition
    Private m_Assembly2 As AssemblyDefinition

    Private m_Result As Boolean
    Private m_Errors As New Generic.List(Of String)
    Private m_Messages As New Generic.List(Of String)

    Private m_SkipMyTypes As Boolean = True
    Private m_SkipDiagnosticAttributes As Boolean = True

    Private m_Search As Generic.List(Of String)
    Private m_Loaded As Dictionary(Of String, AssemblyDefinition)

    Property Search() As Generic.List(Of String)
        Get
            Return m_Search
        End Get
        Set(ByVal value As Generic.List(Of String))
            m_Search = value
        End Set
    End Property

    Sub New(ByVal File1 As String, ByVal File2 As String)
        m_File1 = File1
        m_File2 = File2
    End Sub

    ReadOnly Property Result() As Boolean
        Get
            Return m_Result
        End Get
    End Property

    ReadOnly Property Errors() As Generic.List(Of String)
        Get
            Return m_Errors
        End Get
    End Property

    ReadOnly Property Messages() As Generic.List(Of String)
        Get
            Return m_Messages
        End Get
    End Property

    ReadOnly Property Assembly1() As AssemblyDefinition
        Get
            Return m_Assembly1
        End Get
    End Property

    ReadOnly Property Assembly2() As AssemblyDefinition
        Get
            Return m_Assembly2
        End Get
    End Property

    Function Compare() As Boolean
        m_Assembly1 = AssemblyFactory.GetAssembly(m_File1, True)
        If m_Assembly1 Is Nothing Then Throw New Exception(String.Format("Could not load assembly '{0}'", m_File1))
        m_Assembly2 = AssemblyFactory.GetAssembly(m_File2, True)
        If m_Assembly2 Is Nothing Then Throw New Exception(String.Format("Coult not load assembly '{0}'", m_File2))

        CompareAssemblies()

        m_Result = m_Errors.Count = 0

        Return m_Result
    End Function

    Private Sub CompareAssemblies()

        If m_Assembly1.EntryPoint IsNot Nothing AndAlso m_Assembly2.EntryPoint Is Nothing Then
            SaveMessage("%a1% does not have an entry point, but %a2% does.")
        ElseIf m_Assembly2.EntryPoint IsNot Nothing AndAlso m_Assembly1.EntryPoint Is Nothing Then
            SaveMessage("%a2% does not have an entry point, but %a1% does.")
        End If

        'TODO: Compare all modules, not only MainModule
        CompareTypes(m_Assembly1.MainModule.Types, m_Assembly2.MainModule.Types)

    End Sub


    Private Function AttributeAsString(ByVal Info As CustomAttribute) As String
        Return TypeAsString(Info.Constructor.DeclaringType) & ParametersToString(Info.Constructor.Parameters)
    End Function

    Private Sub CompareAttribute(ByVal Attribute1 As CustomAttribute, ByVal Attribute2 As CustomAttribute)
        'If they are not equal, they are not equal, and the error message has already been shown.
    End Sub

    Private Function AreAttributesEqual(ByVal Attribute1 As CustomAttribute, ByVal Attribute2 As CustomAttribute) As Boolean
        Dim result As Boolean = True

        If AreSameTypes(Attribute1.Constructor.DeclaringType, Attribute2.Constructor.DeclaringType) = False Then
            Return False
        End If

        If String.CompareOrdinal(ParametersToString(Attribute1.Constructor.Parameters), ParametersToString(Attribute2.Constructor.Parameters)) <> 0 Then
            Return False
        End If

        If Attribute1.ConstructorParameters.Count <> Attribute2.ConstructorParameters.Count Then
            Return False
        End If

        For i As Integer = 0 To Attribute1.ConstructorParameters.Count - 1
            Dim v1 As Object = Attribute1.ConstructorParameters(i)
            Dim v2 As Object = Attribute2.ConstructorParameters(i)
            If v1 Is Nothing Xor v2 Is Nothing Then
                Return False
            ElseIf v1 Is Nothing AndAlso v2 Is Nothing Then
                Continue For
            ElseIf Not v1.GetType() Is v2.GetType Then
                Return False
            End If
            If Microsoft.VisualBasic.CompilerServices.Operators.CompareObject(v1, v2, False) <> 0 Then
                Return False
            End If
        Next

        If Attribute1.Fields.Count <> Attribute2.Fields.Count Then
            Return False
        End If

        If Attribute1.Properties.Count <> Attribute2.Properties.Count Then
            Return False
        End If

        For Each key As String In Attribute1.Fields.Keys
            If Not Attribute2.Fields.Contains(key) Then
                Return False
            End If

            Dim type1 As TypeReference = Attribute1.GetFieldType(key)
            Dim value1 As Object = Attribute1.Fields(key)
            Dim type2 As TypeReference = Attribute2.GetFieldType(key)
            Dim value2 As Object = Attribute2.Fields(key)

            If AreSameTypes(type1, type2) = False Then Return False
            If Microsoft.VisualBasic.CompilerServices.Operators.CompareObject(value1, value2, False) <> 0 Then Return False
        Next


        For Each key As String In Attribute1.Properties.Keys
            If Not Attribute2.Properties.Contains(key) Then
                Return False
            End If

            Dim type1 As TypeReference = Attribute1.GetPropertyType(key)
            Dim value1 As Object = Attribute1.Properties(key)
            Dim type2 As TypeReference = Attribute2.GetPropertyType(key)
            Dim value2 As Object = Attribute2.Properties(key)

            If AreSameTypes(type1, type2) = False Then Return False
            If Microsoft.VisualBasic.CompilerServices.Operators.CompareObject(value1, value2, False) <> 0 Then Return False
        Next

        Return result
    End Function

    Private Sub CompareAttributes(ByVal M1 As MemberReference, ByVal M2 As MemberReference, ByVal A1 As CustomAttributeCollection, ByVal A2 As CustomAttributeCollection)
        Dim lst1 As Generic.List(Of CustomAttribute) = CloneCollection(Of CustomAttribute)(A1)
        Dim lst2 As Generic.List(Of CustomAttribute) = CloneCollection(Of CustomAttribute)(A2)

        Dim i As Integer = 0
        Do Until i > lst1.Count - 1
            If lst1(i).Constructor.DeclaringType.Namespace IsNot Nothing AndAlso lst1(i).Constructor.DeclaringType.Namespace.Contains("System.Diagnostic") Then
                m_Messages.Add(String.Format("Skipped attribute of type '{0}'.", lst1(i).Constructor.DeclaringType.FullName))
                lst1.RemoveAt(i)
            Else
                i += 1
            End If
        Loop
        i = 0
        Do Until i > lst2.Count - 1
            If lst2(i).Constructor.DeclaringType.Namespace IsNot Nothing AndAlso lst2(i).Constructor.DeclaringType.Namespace.Contains("System.Diagnostic") Then
                m_Messages.Add(String.Format("Skipped attribute of type '{0}'.", lst2(i).Constructor.DeclaringType.FullName))
                lst2.RemoveAt(i)
            Else
                i += 1
            End If
        Loop

        CompareAttributeList(Of CustomAttribute)(lst1, lst2, New ComparerMethod(Of CustomAttribute)(AddressOf CompareAttribute), New EqualChecker(Of CustomAttribute)(AddressOf AreAttributesEqual), "attribute", New AsString(Of CustomAttribute)(AddressOf AttributeAsString), M1, M2)
    End Sub

    Private Sub CompareAttributeList(Of T)(ByVal Lst1 As Generic.List(Of T), ByVal Lst2 As Generic.List(Of T), ByVal Comparer As ComparerMethod(Of T), ByVal EqualCheck As EqualChecker(Of T), ByVal Name As String, ByVal ItemToString As AsString(Of T), ByVal M1 As MemberReference, ByVal M2 As MemberReference)

        Do Until Lst1.Count = 0
            Dim type1 As T = Lst1(0)
            Dim type2 As T = Nothing
            For Each type As T In Lst2
                If EqualCheck(type1, type) Then
                    type2 = type
                    Exit For
                End If
            Next
            If type2 Is Nothing Then
                If TypeOf M1 Is TypeDefinition Then
                    SaveMessage("Only '%a1%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type1), MemberAsString(M1))
                Else
                    SaveMessage("Only '%a1%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type1), TypeAsString(M1.DeclaringType) & "." & MemberAsString(M1))
                End If
                Lst1.Remove(type1)
            Else
                Comparer(type1, type2)
                Lst1.Remove(type1)
                Lst2.Remove(type2)
            End If
        Loop

        For Each type2 As T In Lst2
            If TypeOf M1 Is TypeDefinition Then
                SaveMessage("Only '%a2%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type2), MemberAsString(M2))
            Else
                SaveMessage("Only '%a2%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type2), TypeAsString(M2.DeclaringType) & "." & MemberAsString(M2))
            End If
        Next
    End Sub

    Private Sub CompareAttributes(ByVal Member1 As MemberReference, ByVal Member2 As MemberReference)
        CompareAttributes(Member1, Member2, GetAttributes(Member1), GetAttributes(Member2))
    End Sub

    Private Function GetAttributes(ByVal Member As MemberReference) As CustomAttributeCollection
        If TypeOf Member Is TypeDefinition Then
            Return DirectCast(Member, TypeDefinition).CustomAttributes
        ElseIf TypeOf Member Is MethodDefinition Then
            Return DirectCast(Member, MethodDefinition).CustomAttributes
        ElseIf TypeOf Member Is FieldDefinition Then
            Return DirectCast(Member, FieldDefinition).CustomAttributes
        ElseIf TypeOf Member Is PropertyDefinition Then
            Return DirectCast(Member, PropertyDefinition).CustomAttributes
        ElseIf TypeOf Member Is EventDefinition Then
            Return DirectCast(Member, EventDefinition).CustomAttributes
        ElseIf Member Is Nothing Then
            Return Nothing
        Else
            Throw New NotImplementedException
        End If
    End Function

    Private Sub CompareTypes(ByVal Types1 As NestedTypeCollection, ByVal Types2 As NestedTypeCollection)
        CompareTypes(CloneCollection(Of TypeDefinition)(Types1), CloneCollection(Of TypeDefinition)(Types2))
    End Sub

    Private Function CloneCollection(Of T)(ByVal e As IEnumerable) As Generic.List(Of T)
        Dim result As New Generic.List(Of T)
        For Each obj As T In e
            result.Add(obj)
        Next
        Return result
    End Function

    Private Sub CompareTypes(ByVal Types1 As TypeDefinitionCollection, ByVal Types2 As TypeDefinitionCollection)
        CompareTypes(CloneCollection(Of TypeDefinition)(Types1), CloneCollection(Of TypeDefinition)(Types2))
    End Sub

    Private Sub CompareTypes(ByVal Types1 As Generic.List(Of TypeDefinition), ByVal Types2 As Generic.List(Of TypeDefinition))
        Dim lst1 As New Generic.List(Of TypeDefinition)(Types1)
        Dim lst2 As New Generic.List(Of TypeDefinition)(Types2)

        Do Until lst1.Count = 0
            Dim type1 As TypeDefinition = lst1(0)
            Dim type2 As TypeDefinition = Nothing
            For Each type As TypeDefinition In lst2
                If AreSameTypes(type1, type) Then
                    type2 = type
                    Exit For
                End If
            Next
            If type2 Is Nothing Then
                If (m_SkipMyTypes AndAlso type1.Namespace = "My") = False Then
                    SaveMessage("Only '%a1%' has the type '{0}'.", TypeAsString(type1))
                Else
                    m_Messages.Add(String.Format("Skipped type '{0}'.", TypeAsString(type1)))
                End If
                lst1.Remove(type1)
            Else
                CompareType(type1, type2)
                lst1.Remove(type1)
                lst2.Remove(type2)
            End If
        Loop

        For Each type2 As TypeDefinition In lst2
            If m_SkipMyTypes AndAlso type2.Namespace = "My" Then
                m_Messages.Add(String.Format("Skipped type '{0}'.", TypeAsString(type2)))
                Continue For
            End If
            SaveMessage("Only '%a2%' has the type '{0}'.", TypeAsString(type2))
        Next
    End Sub

    Private Function TypeAsString(ByVal Info As TypeReference) As String
        Dim result As String
        If Info Is Nothing Then
            result = "Nothing"
        Else
            If Info.DeclaringType IsNot Nothing Then
                If Info.FullName IsNot Nothing Then
                    result = Info.FullName & "+" & Info.Name
                Else
                    result = Info.Name
                End If
            ElseIf Info.Namespace <> "" Then
                result = Info.Namespace & "." & Info.Name
            Else
                result = Info.Name
            End If

            If Info.GenericParameters.Count > 0 Then
                Dim args As GenericParameterCollection = Info.GenericParameters
                If args.Count > 0 Then
                    result &= GenericParametersAsString(Info.GenericParameters)
                Else
                    result = "(Of " & result & ")"
                End If
            ElseIf TypeOf Info Is GenericInstanceType Then
                Dim git As GenericInstanceType = DirectCast(Info, GenericInstanceType)
                Dim tmp As New Generic.List(Of String)
                For Each item As TypeReference In git.GenericArguments
                    tmp.Add(TypeAsString(item))
                Next
                result &= "[" & Join(tmp.ToArray, ", ") & "]"
            End If
        End If

        result = result.Replace("_vbc", "")

        Return result
    End Function

    ''' <summary>
    ''' Checks if two types are the same types.
    ''' (Same name and same namespace)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AreSameTypes(ByVal Type1 As TypeReference, ByVal Type2 As TypeReference) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(TypeAsString(Type1), TypeAsString(Type2)) = 0
    End Function

    Private Sub CompareType(ByVal Type1 As TypeDefinition, ByVal Type2 As TypeDefinition)

        CompareAttributes(Type1, Type2)

        If Type1.Attributes <> Type2.Attributes Then
            SaveMessage("'{0}' has the attributes '{1}', while '{2}' has the attributes '{3}'", Type1, Type1.Attributes, Type2, Type2.Attributes)
        End If

        If AreSameTypes(Type1.BaseType, Type2.BaseType) = False Then
            SaveMessage("'{0}' has base type '{1}', while '{2}' has base type '{3}'", Type1, Type1.BaseType, Type2, Type2.BaseType)
        End If

        If AreSameTypes(Type1.DeclaringType, Type2.DeclaringType) = False Then
            SaveMessage("'{0}' has declaring type '{1}', while '{2}' has declaring type '{3}'", Type1, Type1.DeclaringType, Type2, Type2.DeclaringType)
        End If

        CompareMethods(Type1.Methods, Type2.Methods)

        CompareFields(Type1.Fields, Type2.Fields)

        CompareConstructors(Type1.Constructors, Type2.Constructors)

        CompareEvents(Type1.Events, Type2.Events)

        CompareProperties(Type1.Properties, Type2.Properties)

        CompareTypes(Type1.NestedTypes, Type2.NestedTypes)

        CompareGenericParameters(Type1.GenericParameters, Type2.GenericParameters)
    End Sub

    Private Sub CompareGenericParameters(ByVal ListA As GenericParameterCollection, ByVal ListB As GenericParameterCollection)
        CompareList(Of GenericParameter)(CloneCollection(Of GenericParameter)(ListA), CloneCollection(Of GenericParameter)(ListB), New ComparerMethod(Of GenericParameter)(AddressOf CompareGenericParameters), New EqualChecker(Of GenericParameter)(AddressOf AreGenericParametersSame), "GenericParameter", New AsString(Of GenericParameter)(AddressOf GenericParameterAsString))
    End Sub

    Private Function GetFullName(ByVal owner As IGenericParameterProvider) As String
        Dim t As TypeReference = TryCast(owner, TypeReference)
        If t IsNot Nothing Then Return t.FullName
        Throw New NotImplementedException
    End Function

    Private Function CompareGenericParameters(ByVal P1 As GenericParameter, ByVal P2 As GenericParameter) As Boolean
        If P1.Name <> P2.Name Then
            SaveMessage("Generic parameter #{0} in {1} has the name '{2}', while generic parameter #{0} in {3} has the name '{4}'", P1.Position, GetFullName(P1.Owner), P1.Name, GetFullName(P2.Owner), P2.Name)
        End If

        If P1.Constraints.Count <> P2.Constraints.Count Then
            SaveMessage("Generic parameter #{0} in {1} has {2} constraints, while generic parameter #{0} in {3} has the {4} constraints.", P1.Position, GetFullName(P1.Owner), P1.Constraints.Count, P2.DeclaringType.FullName, P2.Constraints.Count)
        ElseIf P1.Constraints.Count > 0 Then
            For i As Integer = 0 To P1.Constraints.Count - 1
                If AreSameTypes(P1.Constraints(i), P2.Constraints(i)) = False Then
                    SaveMessage("Generic parameter #{0}'s constraint #{1} in {2} is '{3}', while generic parameter #{0}'s constraint #{1} in {4} is '{5}'", P1.Position, i, GetFullName(P1.Owner), P1.Constraints(i).FullName, GetFullName(P2.Owner), P2.Constraints(i).FullName)
                End If
            Next
        End If

        If P1.HasDefaultConstructorConstraint <> P2.HasDefaultConstructorConstraint Then
            SaveMessage("Generic parameter #{0} in {1} HasDefaultConstructorConstraint = {2}, while generic parameter #{0} in {3} HasDefaultConstructorConstraint = {4}", P1.Position, GetFullName(P1.Owner), P1.HasDefaultConstructorConstraint, GetFullName(P2.Owner), P2.HasDefaultConstructorConstraint)
        End If

        If P1.HasNotNullableValueTypeConstraint <> P2.HasNotNullableValueTypeConstraint Then
            SaveMessage("Generic parameter #{0} in {1} HasNotNullableValueTypeConstraint = {2}, while generic parameter #{0} in {3} HasNotNullableValueTypeConstraint = {4}", P1.Position, GetFullName(P1.Owner), P1.HasNotNullableValueTypeConstraint, GetFullName(P2.Owner), P2.HasNotNullableValueTypeConstraint)
        End If

        If P1.HasReferenceTypeConstraint <> P2.HasReferenceTypeConstraint Then
            SaveMessage("Generic parameter #{0} in {1} HasReferenceTypeConstraint = {2}, while generic parameter #{0} in {3} HasReferenceTypeConstraint = {4}", P1.Position, GetFullName(P1.Owner), P1.HasReferenceTypeConstraint, GetFullName(P2.Owner), P2.HasReferenceTypeConstraint)
        End If

        If P1.IsContravariant <> P2.IsContravariant Then
            SaveMessage("Generic parameter #{0} in {1} IsContravariant = {2}, while generic parameter #{0} in {3} IsContravariant = {4}", P1.Position, GetFullName(P1.Owner), P1.IsContravariant, GetFullName(P2.Owner), P2.IsContravariant)
        End If

        If P1.IsCovariant <> P2.IsCovariant Then
            SaveMessage("Generic parameter #{0} in {1} IsCovariant = {2}, while generic parameter #{0} in {3} IsCovariant = {4}", P1.Position, GetFullName(P1.Owner), P1.IsCovariant, GetFullName(P2.Owner), P2.IsCovariant)
        End If

        If P1.IsNonVariant <> P2.IsNonVariant Then
            SaveMessage("Generic parameter #{0} in {1} IsNonVariant = {2}, while generic parameter #{0} in {3} IsNonVariant = {4}", P1.Position, GetFullName(P1.Owner), P1.IsNonVariant, GetFullName(P2.Owner), P2.IsNonVariant)
        End If
    End Function

    Private Function AreGenericParametersSame(ByVal P1 As GenericParameter, ByVal P2 As GenericParameter) As Boolean
        Return P1.Position = P2.Position
    End Function

    Private Function GenericParameterAsString(ByVal Parameter As GenericParameter) As String
        Dim result As String

        result = Parameter.Name
        If Parameter.Constraints.Count > 0 Then
            result &= "{"
            For i As Integer = 0 To Parameter.Constraints.Count - 1
                If i > 0 Then result &= ", "
                result &= Parameter.Constraints(i).FullName
            Next
            result &= "}"
        End If

        Return result
    End Function

    Private Function MethodAsString(ByVal Info As MethodReference) As String
        Dim result As String

        result = Info.DeclaringType.FullName & "." & Info.Name
        result &= GenericParametersAsString(Info.GenericParameters)
        result &= ParametersToString(Info.Parameters)


        Return result
    End Function

    Private Function GenericParametersAsString(ByVal Params As GenericParameterCollection) As String
        Dim result As String = ""
        Dim args As GenericParameterCollection
        Dim constraints As ConstraintCollection
        Dim tmp As New Generic.List(Of String)
        Dim strTmp As String

        args = Params
        For Each item As GenericParameter In args
            strTmp = item.Name
            constraints = item.Constraints
            Dim tmpC As New Generic.List(Of String)
            For Each citem As TypeDefinition In constraints
                tmpC.Add(TypeAsString(citem))
            Next
            tmpC.Sort()
            If tmpC.Count = 1 Then
                strTmp &= " As " & tmpC(0)
            ElseIf tmpC.Count > 1 Then
                strTmp &= " As {" & Join(tmpC.ToArray, ", ") & "}"
            End If
            tmp.Add(strTmp)
        Next
        tmp.Sort()
        result &= "(Of " & Join(tmp.ToArray, ", ") & ")"
        Return result
    End Function

    Private Function AreSameMethod(ByVal Type1 As MethodDefinition, ByVal Type2 As MethodDefinition) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(MethodAsString(Type1), MethodAsString(Type2)) = 0
    End Function

    Private Sub CompareMethod(ByVal Method1 As MethodReference, ByVal Method2 As MethodReference)
        Dim mD1 As MethodDefinition = TryCast(Method1, MethodDefinition)
        Dim mD2 As MethodDefinition = TryCast(Method2, MethodDefinition)

        If Method1 Is Nothing AndAlso Method2 Is Nothing Then Return

        If mD1 IsNot Nothing AndAlso mD2 IsNot Nothing Then
            CompareMethod(mD1, mD2)
            Return
        End If

        Throw New NotImplementedException
    End Sub

    Private Sub CompareMethod(ByVal Method1 As MethodDefinition, ByVal Method2 As MethodDefinition)

        CompareAttributes(Method1, Method2)

        If Method1.Attributes <> Method2.Attributes Then
            SaveMessage("'(%a1%).{0}' has the attributes '{1}', while '(%a2%).{2}' has the attributes '{3}'", Method1, Method1.Attributes, Method2, Method2.Attributes)
        End If

        If Method1.ImplAttributes <> Method2.ImplAttributes Then
            SaveMessage("'(%a1%).{0}' has the implementation flags '{1}', while '(%a2%).{2}' has the implementation flags '{3}'", Method1, Method1.ImplAttributes, Method2, Method2.ImplAttributes)
        End If


        If Method1.CallingConvention <> Method2.CallingConvention Then
            SaveMessage("'(%a1%).{0}' has calling convention '{1}', while '(%a2%).{2}' has calling convention '{3}'", Method1, Method1.CallingConvention, Method2, Method2.CallingConvention)
        End If

        If AreSameTypes(Method1.DeclaringType, Method2.DeclaringType) = False Then
            SaveMessage("'(%a1%).{0}' has declaring type '{1}', while '(%a2%).{2}' has declaring type '{3}'", Method1, Method1.DeclaringType, Method2, Method2.DeclaringType)
        End If

        If AreSameTypes(Method1.ReturnType.ReturnType, Method2.ReturnType.ReturnType) = False Then
            SaveMessage("'(%a1%).{0}' has return type '{1}', while '(%a2%).{2}' has return type '{3}'", Method1, Method1.ReturnType, Method2, Method2.ReturnType)
        End If

        CompareGenericParameters(Method1.GenericParameters, Method2.GenericParameters)

    End Sub

    Private Sub CompareMethods(ByVal Methods1 As MethodDefinitionCollection, ByVal Methods2 As MethodDefinitionCollection)
        CompareList(Of MethodDefinition)(CloneCollection(Of MethodDefinition)(Methods1), CloneCollection(Of MethodDefinition)(Methods2), New ComparerMethod(Of MethodDefinition)(AddressOf CompareMethod), New EqualChecker(Of MethodDefinition)(AddressOf AreSameMethod), "Method", New AsString(Of MethodDefinition)(AddressOf MethodAsString))
    End Sub

    Private Function FieldAsString(ByVal Info As FieldReference) As String
        Return TypeAsString(Info.DeclaringType) & "." & Info.Name
    End Function

    Private Function AreSameField(ByVal Type1 As FieldDefinition, ByVal Type2 As FieldDefinition) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(FieldAsString(Type1), FieldAsString(Type2)) = 0
    End Function

    Private Sub CompareField(ByVal Field1 As FieldDefinition, ByVal Field2 As FieldDefinition)
        CompareAttributes(Field1, Field2)

        If Field1.Attributes <> Field2.Attributes Then
            If Field1.Name <> "value__" Then
                SaveMessage("'(%a1%.){0}' has the attributes '{1}', while '(%a2%.){2}' has the attributes '{3}'", Field1, Field1.Attributes, Field2, Field2.Attributes)
            End If
        End If

        If AreSameTypes(Field1.FieldType, Field2.FieldType) = False Then
            SaveMessage("'(%a1%.){0}' has field type '{1}', while '(%a2%.){2}' has field type '{3}'", Field1, Field1.FieldType, Field2, Field2.FieldType)
        End If

        If AreSameTypes(Field1.DeclaringType, Field2.DeclaringType) = False Then
            SaveMessage("'(%a1%.){0}' has declaring type '{1}', while '(%a2%.){2}' has declaring type '{3}'", Field1, Field1.DeclaringType, Field2, Field2.DeclaringType)
        End If

        If Field1.IsLiteral = True AndAlso Field2.IsLiteral = False Then
            SaveMessage("'(%a1%.){0}' has a literal value, while '(%a2%.){1}' does not", Field1, Field2)
        ElseIf Field1.IsLiteral = False AndAlso Field2.IsLiteral = True Then
            SaveMessage("'(%a2%.){0}' has a literal value, while '(%a1%.){1}' does not", Field2, Field1)
        End If

        If Field1.IsInitOnly = True AndAlso Field2.IsInitOnly = False Then
            SaveMessage("'(%a1%.){0}' is ReadOnly, while '(%a2%.){1}' is not", Field1, Field2)
        ElseIf Field1.IsInitOnly = False AndAlso Field2.IsInitOnly = True Then
            SaveMessage("'(%a2%.){0}' is ReadOnly, while '(%a1%.){1}' is not", Field2, Field1)
        End If

    End Sub

    Private Sub CompareFields(ByVal Fields1 As FieldDefinitionCollection, ByVal Fields2 As FieldDefinitionCollection)
        CompareList(Of FieldDefinition)(CloneCollection(Of FieldDefinition)(Fields1), CloneCollection(Of FieldDefinition)(Fields2), New ComparerMethod(Of FieldDefinition)(AddressOf CompareField), New EqualChecker(Of FieldDefinition)(AddressOf AreSameField), "Field", New AsString(Of FieldDefinition)(AddressOf FieldAsString))
    End Sub

    Private Function CtorAsString(ByVal Info As MethodReference) As String
        Return MethodAsString(Info)
    End Function

    Private Function AreSameCtor(ByVal Type1 As MethodReference, ByVal Type2 As MethodReference) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(CtorAsString(Type1), CtorAsString(Type2)) = 0
    End Function

    Private Sub CompareConstructors(ByVal Ctors1 As ConstructorCollection, ByVal Ctors2 As ConstructorCollection)
        CompareList(Of MethodDefinition)(CloneCollection(Of MethodDefinition)(Ctors1), CloneCollection(Of MethodDefinition)(Ctors2), New ComparerMethod(Of MethodDefinition)(AddressOf CompareMethod), New EqualChecker(Of MethodDefinition)(AddressOf AreSameCtor), "Constructor", New AsString(Of MethodDefinition)(AddressOf CtorAsString))
    End Sub

    Private Shared _assemblies As New Hashtable

    Private Class resolver
        Inherits BaseAssemblyResolver

    End Class

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

    Private Function EventAsString(ByVal Info As EventDefinition) As String
        Dim tD As TypeDefinition
        tD = FindDefinition(Info.EventType).Module.Types(Info.EventType.FullName)
        Return Info.Name & ParametersToString(tD.Methods.GetMethod("Invoke")(0).Parameters)
    End Function

    Private Function AreSameEvent(ByVal Type1 As EventDefinition, ByVal Type2 As EventDefinition) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(EventAsString(Type1), EventAsString(Type2)) = 0
    End Function

    Private Sub CompareEvent(ByVal Event1 As EventDefinition, ByVal Event2 As EventDefinition)
        CompareAttributes(Event1, Event2)

        If Event1.Attributes <> Event2.Attributes Then
            SaveMessage("'(%a1%).{0}' has the attributes '{1}', while '(%a2%).{2}' has the attributes '{3}'", Event1, Event1.Attributes, Event2, Event2.Attributes)
        End If

        If AreSameTypes(Event1.DeclaringType, Event2.DeclaringType) = False Then
            SaveMessage("'(%a1%).{0}' has declaring type '{1}', while '(%a2%).{2}' has declaring type '{3}'", Event1, Event1.DeclaringType, Event2, Event2.DeclaringType)
        End If

        If AreSameTypes(Event1.EventType, Event2.EventType) = False Then
            SaveMessage("'(%a1%).{0}' has event handler type '{1}', while '(%a2%).{2}' has event handler type '{3}'", Event1, Event1.EventType, Event2, Event2.EventType)
        End If

        Dim m1, m2 As String
        m1 = MethodAsString(Event1.AddMethod)
        m2 = MethodAsString(Event2.AddMethod)
        If String.CompareOrdinal(m1, m2) <> 0 Then
            SaveMessage("(%a1%).{0} contains add method '{1}', while (%a2%).{2} contains add method '{3}'.", Event1, m1, Event2, m2)
        End If

        Dim r1, r2 As String
        r1 = MethodAsString(Event1.RemoveMethod)
        r2 = MethodAsString(Event2.RemoveMethod)
        If String.CompareOrdinal(r1, r2) <> 0 Then
            SaveMessage("(%a1%).{0} contains remove method '{1}', while (%a2%).{2} contains remove method '{3}'.", Event1, r1, Event2, r2)
        End If

        If Event1.InvokeMethod Is Nothing AndAlso Event2.InvokeMethod IsNot Nothing Then
            SaveMessage("(%a2%).{0} has a raise method, but (%a1%).{1} does not.", Event1, Event2)
        ElseIf Event1.InvokeMethod IsNot Nothing AndAlso Event2.InvokeMethod Is Nothing Then
            SaveMessage("(%a1%).{0} has a raise method, but (%a2%).{1} does not.", Event2, Event1)
        ElseIf Event1.InvokeMethod IsNot Nothing AndAlso Event2.InvokeMethod IsNot Nothing Then
            r1 = MethodAsString(Event1.RemoveMethod)
            r2 = MethodAsString(Event2.RemoveMethod)
            If String.CompareOrdinal(r1, r2) <> 0 Then
                SaveMessage("(%a1%).{0} contains raise method '{1}', while (%a2%).{2} contains raise method '{3}'.", Event1, r1, Event2, r2)
            End If
        End If
    End Sub

    Private Sub CompareEvents(ByVal Events1 As EventDefinitionCollection, ByVal Events2 As EventDefinitionCollection)
        CompareList(Of EventDefinition)(CloneCollection(Of EventDefinition)(Events1), CloneCollection(Of EventDefinition)(Events2), New ComparerMethod(Of EventDefinition)(AddressOf CompareEvent), New EqualChecker(Of EventDefinition)(AddressOf AreSameEvent), "Event", New AsString(Of EventDefinition)(AddressOf EventAsString))
    End Sub

    Private Function PropertyAsString(ByVal Info As PropertyDefinition) As String
        Return Info.Name & ParametersToString(Info.Parameters)
    End Function

    Private Function AreSameProperty(ByVal Type1 As PropertyDefinition, ByVal Type2 As PropertyDefinition) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(PropertyAsString(Type1), PropertyAsString(Type2)) = 0
    End Function

    Private Sub CompareProperty(ByVal Prop1 As PropertyDefinition, ByVal Prop2 As PropertyDefinition)
        CompareAttributes(Prop1, Prop2)

        If Prop1.Attributes <> Prop2.Attributes Then
            SaveMessage("'(%a1%).{0}' has the attributes '{1}', while '(%a2%).{2}' has the attributes '{3}'", Prop1, Prop1.Attributes, Prop2, Prop2.Attributes)
        End If

        If AreSameTypes(Prop1.DeclaringType, Prop2.DeclaringType) = False Then
            SaveMessage("'(%a1%).{0}' has declaring type '{1}', while '(%a2%).{2}' has declaring type '{3}'", Prop1, Prop1.DeclaringType, Prop2, Prop2.DeclaringType)
        End If

        If AreSameTypes(Prop1.PropertyType, Prop2.PropertyType) = False Then
            SaveMessage("'(%a1%).{0}' has property type '{1}', while '(%a2%).{2}' has property type '{3}'", Prop1, Prop1.PropertyType, Prop2, Prop2.PropertyType)
        End If

        CompareMethod(Prop1.GetMethod, Prop2.GetMethod)
        CompareMethod(Prop1.SetMethod, Prop2.SetMethod)
    End Sub

    Private Sub CompareProperties(ByVal Props1 As PropertyDefinitionCollection, ByVal Props2 As PropertyDefinitionCollection)
        CompareList(Of PropertyDefinition)(CloneCollection(Of PropertyDefinition)(Props1), CloneCollection(Of PropertyDefinition)(Props2), New ComparerMethod(Of PropertyDefinition)(AddressOf CompareProperty), New EqualChecker(Of PropertyDefinition)(AddressOf AreSameProperty), "Property", New AsString(Of PropertyDefinition)(AddressOf PropertyAsString))
    End Sub

    Private Sub CompareList(Of T As MemberReference)(ByVal Lst1 As Generic.List(Of T), ByVal Lst2 As Generic.List(Of T), ByVal Comparer As ComparerMethod(Of T), ByVal EqualCheck As EqualChecker(Of T), ByVal Name As String, ByVal ItemToString As AsString(Of T))

        Do Until Lst1.Count = 0
            Dim type1 As T = Lst1(0)
            Dim type2 As T = Nothing
            For Each type As T In Lst2
                If EqualCheck(type1, type) Then
                    type2 = type
                    Exit For
                End If
            Next
            If type2 Is Nothing AndAlso IgnoreItem(type1) = False Then
                SaveMessage("Only '%a1%' has the {0} '{1}'.", Name, ItemToString(type1))

                Lst1.Remove(type1)
            Else
                If type1 IsNot Nothing AndAlso type2 IsNot Nothing Then
                    Comparer(type1, type2)
                End If
                Lst1.Remove(type1)
                Lst2.Remove(type2)
            End If
        Loop

        For Each type2 As T In Lst2
            If IgnoreItem(type2) = False Then SaveMessage("Only '%a2%' has the {0} '{1}'.", Name, ItemToString(type2))
        Next
    End Sub

    Shared Function IgnoreItem(Of T As MemberReference)(ByVal Value As T) As Boolean
        If Value.Name Is Nothing Then Return False
        If Value.Name.ToUpper.Contains("$STATIC$") Then Return True
        Return False
    End Function

    Delegate Sub ComparerMethod(Of T)(ByVal V1 As T, ByVal v2 As T)
    Delegate Function EqualChecker(Of T)(ByVal V1 As T, ByVal v2 As T) As Boolean
    Delegate Function AsString(Of T)(ByVal V As T) As String

    Private Function ParametersToString(ByVal Parameters As ParameterDefinitionCollection) As String
        Dim result As New System.Text.StringBuilder

        result.Append("(")
        For i As Integer = 0 To Parameters.Count - 1
            If i > 0 Then result.Append(", ")
            result.Append(TypeAsString(Parameters(i).ParameterType))
        Next
        result.Append(")")

        Return result.ToString
    End Function

    Private Function MemberAsString(ByVal Member As MemberReference) As String
        If TypeOf Member Is MethodDefinition Then
            Return MethodAsString(DirectCast(Member, MethodDefinition))
        ElseIf TypeOf Member Is EventDefinition Then
            Return EventAsString(DirectCast(Member, EventDefinition))
        ElseIf TypeOf Member Is TypeReference Then
            Return TypeAsString(DirectCast(Member, TypeReference))
        ElseIf TypeOf Member Is PropertyDefinition Then
            Return PropertyAsString(DirectCast(Member, PropertyDefinition))
        ElseIf TypeOf Member Is FieldReference Then
            Return FieldAsString(DirectCast(Member, FieldReference))
        Else
            Throw New NotImplementedException
        End If
    End Function

    Private Sub SaveMessage(ByVal Msg As String)
        Msg = Msg.Replace("%a1%", m_Assembly1.Name.Name)
        Msg = Msg.Replace("%a2%", m_Assembly2.Name.Name)
        m_Errors.Add(Msg)
    End Sub

    Private Sub SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As String)
        Msg = String.Format(Msg, parameters)
        Msg = Msg.Replace("%a1%", m_Assembly1.Name.Name)
        Msg = Msg.Replace("%a2%", m_Assembly2.Name.Name)
        m_Errors.Add(Msg)
    End Sub

    Private Sub SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As Object)
        Dim params(UBound(parameters)) As String
        For i As Integer = 0 To UBound(params)
            If parameters(i) Is Nothing Then
                params(i) = "Nothing"
            Else
                Dim paramMember As MemberReference = TryCast(parameters(i), MemberReference)
                If paramMember IsNot Nothing Then
                    params(i) = MemberAsString(paramMember)
                Else
                    params(i) = parameters(i).ToString
                End If
            End If
        Next
        SaveMessage(Msg, params)
    End Sub
End Class
