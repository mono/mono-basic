' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

Imports System.Reflection

Public Class AssemblyComparer
    Private m_File1 As String
    Private m_File2 As String

    Private m_Assembly1 As Assembly
    Private m_Assembly2 As Assembly

    Private m_Result As Boolean
    Private m_Errors As New Generic.List(Of String)
    Private m_Messages As New Generic.List(Of String)

    Private m_SkipMyTypes As Boolean = True
    Private m_SkipDiagnosticAttributes As Boolean = True

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

    ReadOnly Property Assembly1() As Assembly
        Get
            Return m_Assembly1
        End Get
    End Property

    ReadOnly Property Assembly2() As Assembly
        Get
            Return m_Assembly2
        End Get
    End Property

    Function ReflectionOnlyAssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly
        Return Assembly.ReflectionOnlyLoad(e.Name)
    End Function

    Function Compare() As Boolean
        'AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssembly
        AddHandler AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve, AddressOf ReflectionOnlyAssemblyResolve
        'AddHandler AppDomain.CurrentDomain.TypeResolve, AddressOf ResolveAssembly

        m_Assembly1 = Reflection.Assembly.ReflectionOnlyLoadFrom(m_File1)
        If m_Assembly1 Is Nothing Then Throw New Exception(String.Format("Could not load assembly '{0}'", m_File1))
        m_Assembly2 = Reflection.Assembly.ReflectionOnlyLoadFrom(m_File2)
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

        CompareTypes(m_Assembly1.GetTypes(), m_Assembly2.GetTypes())

    End Sub


    Private Function AttributeAsString(ByVal Info As CustomAttributeData) As String
        Return TypeAsString(Info.Constructor.DeclaringType) & ParametersToString(Info.Constructor.GetParameters)
    End Function

    Private Sub CompareAttribute(ByVal Attribute1 As CustomAttributeData, ByVal Attribute2 As CustomAttributeData)
        'If the are not equal, they are not equal, and the error message has already been shown.
    End Sub

    Private Function AreAttributesEqual(ByVal Attribute1 As CustomAttributeData, ByVal Attribute2 As CustomAttributeData) As Boolean
        Dim result As Boolean = True

        If AreSameTypes(Attribute1.Constructor.DeclaringType, Attribute2.Constructor.DeclaringType) = False Then
            Return False
        End If

        If String.CompareOrdinal(ParametersToString(Attribute1.Constructor.GetParameters), ParametersToString(Attribute2.Constructor.GetParameters)) <> 0 Then
            Return False
        End If

        If Attribute1.ConstructorArguments.Count <> Attribute2.ConstructorArguments.Count Then
            Return False
        End If

        For i As Integer = 0 To Attribute1.ConstructorArguments.Count - 1
            If AreSameTypes(Attribute1.ConstructorArguments(i).ArgumentType, Attribute2.ConstructorArguments(i).ArgumentType) = False Then
                Return False
            End If
            If Microsoft.VisualBasic.CompilerServices.Operators.CompareObject(Attribute1.ConstructorArguments(i).Value, Attribute2.ConstructorArguments(i).Value, False) <> 0 Then
                Return False
            End If
        Next

        If Attribute1.NamedArguments.Count <> Attribute2.NamedArguments.Count Then
            Return False
        End If

        For i As Integer = 0 To Attribute1.NamedArguments.Count - 1
            If AreSameTypes(Attribute1.NamedArguments(i).TypedValue.ArgumentType, Attribute2.NamedArguments(i).TypedValue.ArgumentType) = False Then
                Return False
            End If
            If Microsoft.VisualBasic.CompilerServices.Operators.CompareObject(Attribute1.NamedArguments(i).TypedValue.Value, Attribute2.NamedArguments(i).TypedValue.Value, False) <> 0 Then
                Return False
            End If
        Next

        Return result
    End Function

    Private Sub CompareAttributes(ByVal M1 As MemberInfo, ByVal M2 As MemberInfo, ByVal A1 As Generic.IList(Of CustomAttributeData), ByVal A2 As Generic.IList(Of CustomAttributeData))
        Dim lst1 As New Generic.List(Of CustomAttributeData)(A1)
        Dim lst2 As New Generic.List(Of CustomAttributeData)(A2)

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

        CompareAttributeList(lst1, lst2, AddressOf CompareAttribute, AddressOf AreAttributesEqual, "attribute", AddressOf AttributeAsString, M1, M2)
    End Sub

    Private Sub CompareAttributeList(Of T)(ByVal Lst1 As Generic.List(Of T), ByVal Lst2 As Generic.List(Of T), ByVal Comparer As ComparerMethod(Of T), ByVal EqualCheck As EqualChecker(Of T), ByVal Name As String, ByVal ItemToString As AsString(Of T), ByVal M1 As MemberInfo, ByVal M2 As MemberInfo)

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
                If TypeOf M1 Is Type Then
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
            If TypeOf M1 Is Type Then
                SaveMessage("Only '%a2%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type2), MemberAsString(M2))
            Else
                SaveMessage("Only '%a2%' has the {0} '{1}' on the member '{2}'.", Name, ItemToString(type2), TypeAsString(M2.DeclaringType) & "." & MemberAsString(M2))
            End If
        Next
    End Sub

    Private Sub CompareAttributes(ByVal Member1 As MemberInfo, ByVal Member2 As MemberInfo)
        If Member1.MemberType <> Member2.MemberType Then Throw New Exception("Wrong members!")
        CompareAttributes(Member1, Member2, GetAttributes(Member1), GetAttributes(Member2))
    End Sub

    Private Function GetAttributes(ByVal Member As MemberInfo) As Generic.IList(Of CustomAttributeData)
        Return CustomAttributeData.GetCustomAttributes(Member)
    End Function

    Private Sub CompareTypes(ByVal Types1() As Type, ByVal Types2() As Type)
        Dim lst1 As New Generic.List(Of Type)(Types1)
        Dim lst2 As New Generic.List(Of Type)(Types2)

        Do Until lst1.Count = 0
            Dim type1 As Type = lst1(0)
            Dim type2 As Type = Nothing
            For Each type As Type In lst2
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

        For Each type2 As Type In lst2
            If m_SkipMyTypes AndAlso type2.Namespace = "My" Then
                m_Messages.Add(String.Format("Skipped type '{0}'.", TypeAsString(type2)))
                Continue For
            End If
            SaveMessage("Only '%a2%' has the type '{0}'.", TypeAsString(type2))
        Next
    End Sub

    Private Function TypeAsString(ByVal Info As Type) As String
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

            If Info.ContainsGenericParameters Then
                Dim args As Type() = Info.GetGenericArguments
                If args.Length > 0 Then
                    result &= GenericParametersAsString(Info.GetGenericArguments)
                Else
                    result = "(Of " & result & ")"
                End If
            ElseIf Info.IsGenericType Then
                Dim tmp As New Generic.List(Of String)
                Dim tmpList As Type() = Info.GetGenericArguments
                For Each item As Type In tmpList
                    tmp.Add(TypeAsString(item))
                Next
                tmp.Sort()
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
    Private Function AreSameTypes(ByVal Type1 As Type, ByVal Type2 As Type) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(TypeAsString(Type1), TypeAsString(Type2)) = 0
    End Function

    Private Sub CompareType(ByVal Type1 As Type, ByVal Type2 As Type)

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

        Dim bindingAttr As BindingFlags = BindingFlags.Static Or BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly

        CompareMethods(Type1.GetMethods(bindingAttr), Type2.GetMethods(bindingAttr))

        CompareFields(Type1.GetFields(bindingAttr), Type2.GetFields(bindingAttr))

        CompareConstructors(Type1.GetConstructors(bindingAttr), Type2.GetConstructors(bindingAttr))

        CompareEvents(Type1.GetEvents(bindingAttr), Type2.GetEvents(bindingAttr))

        CompareProperties(Type1.GetProperties(bindingAttr), Type2.GetProperties(bindingAttr))

        CompareTypes(Type1.GetNestedTypes(bindingAttr), Type2.GetNestedTypes(bindingAttr))
    End Sub

    Private Function MethodAsString(ByVal Info As MethodInfo) As String
        Dim result As String

        result = Info.DeclaringType.FullName & "." & Info.Name
        If Info.IsGenericMethodDefinition Then
            result &= GenericParametersAsString(Info.GetGenericArguments)
        End If
        result &= ParametersToString(Info.GetParameters)


        Return result
    End Function

    Private Function GenericParametersAsString(ByVal Params As Type()) As String
        Dim result As String = ""
        Dim args() As Type
        Dim constraints() As Type
        Dim tmp As New Generic.List(Of String)
        Dim strTmp As String

        args = Params
        For Each item As Type In args
            strTmp = item.Name
            If item.IsGenericParameter Then
                constraints = item.GetGenericParameterConstraints()
                Dim tmpC As New Generic.List(Of String)
                For Each citem As Type In constraints
                    tmpC.Add(TypeAsString(citem))
                Next
                tmpC.Sort()
                If tmpC.Count = 1 Then
                    strTmp &= " As " & tmpC(0)
                ElseIf tmpC.Count > 1 Then
                    strTmp &= " As {" & Join(tmpC.ToArray, ", ") & "}"
                End If
            End If
            tmp.Add(strTmp)
        Next
        tmp.Sort()
        result &= "(Of " & Join(tmp.ToArray, ", ") & ")"
        Return result
    End Function

    Private Function AreSameMethod(ByVal Type1 As MethodInfo, ByVal Type2 As MethodInfo) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(MethodAsString(Type1), MethodAsString(Type2)) = 0
    End Function

    Private Sub CompareMethod(ByVal Method1 As MethodInfo, ByVal Method2 As MethodInfo)

        CompareAttributes(Method1, Method2)

        If Method1.Attributes <> Method2.Attributes Then
            SaveMessage("'(%a1%).{0}' has the attributes '{1}', while '(%a2%).{2}' has the attributes '{3}'", Method1, Method1.Attributes, Method2, Method2.Attributes)
        End If

        If Method1.GetMethodImplementationFlags <> Method2.GetMethodImplementationFlags Then
            SaveMessage("'(%a1%).{0}' has the implementation flags '{1}', while '(%a2%).{2}' has the implementation flags '{3}'", Method1, Method1.GetMethodImplementationFlags, Method2, Method2.GetMethodImplementationFlags)
        End If


        If Method1.CallingConvention <> Method2.CallingConvention Then
            SaveMessage("'(%a1%).{0}' has calling convention '{1}', while '(%a2%).{2}' has calling convention '{3}'", Method1, Method1.CallingConvention, Method2, Method2.CallingConvention)
        End If

        If AreSameTypes(Method1.DeclaringType, Method2.DeclaringType) = False Then
            SaveMessage("'(%a1%).{0}' has declaring type '{1}', while '(%a2%).{2}' has declaring type '{3}'", Method1, Method1.DeclaringType, Method2, Method2.DeclaringType)
        End If

        If AreSameTypes(Method1.ReturnType, Method2.ReturnType) = False Then
            SaveMessage("'(%a1%).{0}' has return type '{1}', while '(%a2%).{2}' has return type '{3}'", Method1, Method1.ReturnType, Method2, Method2.ReturnType)
        End If

    End Sub

    Private Sub CompareMethods(ByVal Methods1() As MethodInfo, ByVal Methods2() As MethodInfo)
        CompareList(New Generic.List(Of MethodInfo)(Methods1), New Generic.List(Of MethodInfo)(Methods2), AddressOf CompareMethod, AddressOf AreSameMethod, "Method", AddressOf MethodAsString)
    End Sub

    Private Function FieldAsString(ByVal Info As FieldInfo) As String
        Return TypeAsString(Info.DeclaringType) & "." & Info.Name
    End Function

    Private Function AreSameField(ByVal Type1 As FieldInfo, ByVal Type2 As FieldInfo) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(FieldAsString(Type1), FieldAsString(Type2)) = 0
    End Function

    Private Sub CompareField(ByVal Field1 As FieldInfo, ByVal Field2 As FieldInfo)
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

    Private Sub CompareFields(ByVal Fields1() As FieldInfo, ByVal Fields2() As FieldInfo)
        CompareList(New Generic.List(Of FieldInfo)(Fields1), New Generic.List(Of FieldInfo)(Fields2), AddressOf CompareField, AddressOf AreSameField, "Field", AddressOf FieldAsString)
    End Sub

    Private Function CtorAsString(ByVal Info As ConstructorInfo) As String
        If Info.IsStatic Then
            Return TypeAsString(Info.DeclaringType) & ".cctor" & ParametersToString(Info.GetParameters)
        Else
            Return TypeAsString(Info.DeclaringType) & ".ctor" & ParametersToString(Info.GetParameters)
        End If
    End Function

    Private Function AreSameCtor(ByVal Type1 As ConstructorInfo, ByVal Type2 As ConstructorInfo) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(CtorAsString(Type1), CtorAsString(Type2)) = 0
    End Function

    Private Sub CompareConstructor(ByVal Ctor1 As ConstructorInfo, ByVal Ctor2 As ConstructorInfo)

        CompareAttributes(Ctor1, Ctor2)

        If Ctor1.Attributes <> Ctor2.Attributes Then
            SaveMessage("'{0}' has the attributes '{1}', while '{2}' has the attributes '{3}'", Ctor1, Ctor1.Attributes, Ctor2, Ctor2.Attributes)
        End If

        If Ctor1.CallingConvention <> Ctor2.CallingConvention Then
            SaveMessage("'{0}' has calling convention '{1}', while '{2}' has calling convention '{3}'", Ctor1, Ctor1.CallingConvention, Ctor2, Ctor2.CallingConvention)
        End If

        If AreSameTypes(Ctor1.DeclaringType, Ctor2.DeclaringType) = False Then
            SaveMessage("'{0}' has declaring type '{1}', while '{2}' has declaring type '{3}'", Ctor1, Ctor1.DeclaringType, Ctor2, Ctor2.DeclaringType)
        End If

        If Ctor1.ContainsGenericParameters = False AndAlso Ctor2.ContainsGenericParameters = True Then
            SaveMessage("'{0}' does not contain generic parameters, while '{1}' does.", Ctor1, Ctor2)
        ElseIf Ctor1.ContainsGenericParameters = True AndAlso Ctor2.ContainsGenericParameters = False Then
            SaveMessage("'{0}' does not contain generic parameters, while '{1}' does.", Ctor2, Ctor1)
        ElseIf Ctor1.ContainsGenericParameters AndAlso Ctor2.ContainsGenericParameters Then
            SaveMessage("Comparison of generic parameters is not implemented.")
        End If

    End Sub

    Private Sub CompareConstructors(ByVal Ctors1() As ConstructorInfo, ByVal Ctors2() As ConstructorInfo)
        CompareList(New Generic.List(Of ConstructorInfo)(Ctors1), New Generic.List(Of ConstructorInfo)(Ctors2), AddressOf CompareConstructor, AddressOf AreSameCtor, "Constructor", AddressOf CtorAsString)
    End Sub

    Private Function EventAsString(ByVal Info As EventInfo) As String
        Return Info.Name & ParametersToString(Info.EventHandlerType.GetMethod("Invoke").GetParameters)
    End Function

    Private Function AreSameEvent(ByVal Type1 As EventInfo, ByVal Type2 As EventInfo) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(EventAsString(Type1), EventAsString(Type2)) = 0
    End Function

    Private Sub CompareEvent(ByVal Event1 As EventInfo, ByVal Event2 As EventInfo)
        CompareAttributes(Event1, Event2)

        If Event1.Attributes <> Event2.Attributes Then
            SaveMessage("'(%a1%).{0}' has the attributes '{1}', while '(%a2%).{2}' has the attributes '{3}'", Event1, Event1.Attributes, Event2, Event2.Attributes)
        End If

        If AreSameTypes(Event1.DeclaringType, Event2.DeclaringType) = False Then
            SaveMessage("'(%a1%).{0}' has declaring type '{1}', while '(%a2%).{2}' has declaring type '{3}'", Event1, Event1.DeclaringType, Event2, Event2.DeclaringType)
        End If

        If AreSameTypes(Event1.EventHandlerType, Event2.EventHandlerType) = False Then
            SaveMessage("'(%a1%).{0}' has event handler type '{1}', while '(%a2%).{2}' has event handler type '{3}'", Event1, Event1.EventHandlerType, Event2, Event2.EventHandlerType)
        End If

        Dim m1, m2 As String
        m1 = MethodAsString(Event1.GetAddMethod(True))
        m2 = MethodAsString(Event2.GetAddMethod(True))
        If String.CompareOrdinal(m1, m2) <> 0 Then
            SaveMessage("(%a1%).{0} contains add method '{1}', while (%a2%).{2} contains add method '{3}'.", Event1, m1, Event2, m2)
        End If

        Dim r1, r2 As String
        r1 = MethodAsString(Event1.GetRemoveMethod(True))
        r2 = MethodAsString(Event2.GetRemoveMethod(True))
        If String.CompareOrdinal(r1, r2) <> 0 Then
            SaveMessage("(%a1%).{0} contains remove method '{1}', while (%a2%).{2} contains remove method '{3}'.", Event1, r1, Event2, r2)
        End If

        If Event1.GetRaiseMethod(True) Is Nothing AndAlso Event2.GetRaiseMethod(True) IsNot Nothing Then
            SaveMessage("(%a2%).{0} has a raise method, but (%a1%).{1} does not.", Event1, Event2)
        ElseIf Event1.GetRaiseMethod(True) IsNot Nothing AndAlso Event2.GetRaiseMethod(True) Is Nothing Then
            SaveMessage("(%a1%).{0} has a raise method, but (%a2%).{1} does not.", Event2, Event1)
        ElseIf Event1.GetRaiseMethod(True) IsNot Nothing AndAlso Event2.GetRaiseMethod(True) IsNot Nothing Then
            r1 = MethodAsString(Event1.GetRemoveMethod(True))
            r2 = MethodAsString(Event2.GetRemoveMethod(True))
            If String.CompareOrdinal(r1, r2) <> 0 Then
                SaveMessage("(%a1%).{0} contains raise method '{1}', while (%a2%).{2} contains raise method '{3}'.", Event1, r1, Event2, r2)
            End If
        End If
    End Sub

    Private Sub CompareEvents(ByVal Events1() As EventInfo, ByVal Events2() As EventInfo)
        CompareList(New Generic.List(Of EventInfo)(Events1), New Generic.List(Of EventInfo)(Events2), AddressOf CompareEvent, AddressOf AreSameEvent, "Event", AddressOf EventAsString)
    End Sub

    Private Function PropertyAsString(ByVal Info As PropertyInfo) As String
        Return Info.Name & ParametersToString(Info.GetIndexParameters)
    End Function

    Private Function AreSameProperty(ByVal Type1 As PropertyInfo, ByVal Type2 As PropertyInfo) As Boolean
        If Type1 Is Nothing AndAlso Type2 IsNot Nothing Then Return False
        If Type2 Is Nothing AndAlso Type1 IsNot Nothing Then Return False
        If Type1 Is Nothing AndAlso Type2 Is Nothing Then Return True
        Return String.CompareOrdinal(PropertyAsString(Type1), PropertyAsString(Type2)) = 0
    End Function

    Private Sub CompareProperty(ByVal Prop1 As PropertyInfo, ByVal Prop2 As PropertyInfo)
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

        CompareMethods(Prop1.GetAccessors(True), Prop2.GetAccessors(True))
    End Sub

    Private Sub CompareProperties(ByVal Props1() As PropertyInfo, ByVal Props2() As PropertyInfo)
        CompareList(New Generic.List(Of PropertyInfo)(Props1), New Generic.List(Of PropertyInfo)(Props2), AddressOf CompareProperty, AddressOf AreSameProperty, "Property", AddressOf PropertyAsString)
    End Sub

    Private Sub CompareList(Of T As MemberInfo)(ByVal Lst1 As Generic.List(Of T), ByVal Lst2 As Generic.List(Of T), ByVal Comparer As ComparerMethod(Of T), ByVal EqualCheck As EqualChecker(Of T), ByVal Name As String, ByVal ItemToString As AsString(Of T))

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
                Lst1.Remove(type1)
                Lst2.Remove(type2)
            End If
        Loop

        For Each type2 As T In Lst2
            If IgnoreItem(type2) = False Then SaveMessage("Only '%a2%' has the {0} '{1}'.", Name, ItemToString(type2))
        Next
    End Sub

    Shared Function IgnoreItem(Of T As MemberInfo)(ByVal Value As T) As Boolean
        If Value.Name Is Nothing Then Return False
        If Value.Name.ToUpper.Contains("$STATIC$") Then Return True
        Return False
    End Function

    Delegate Sub ComparerMethod(Of T)(ByVal V1 As T, ByVal v2 As T)
    Delegate Function EqualChecker(Of T)(ByVal V1 As T, ByVal v2 As T) As Boolean
    Delegate Function AsString(Of T)(ByVal V As T) As String

    Private Function ParametersToString(ByVal Parameters() As ParameterInfo) As String
        Dim result As New System.Text.StringBuilder

        result.Append("(")
        For i As Integer = 0 To UBound(Parameters)
            result.Append(TypeAsString(Parameters(i).ParameterType))
            If i < UBound(Parameters) Then result.Append(", ")
        Next

        result.Append(")")

        Return result.ToString
    End Function

    Private Function MemberAsString(ByVal Member As MemberInfo) As String
        Select Case Member.MemberType
            Case MemberTypes.Constructor
                Return CtorAsString(DirectCast(Member, ConstructorInfo))
            Case MemberTypes.Event
                Return EventAsString(DirectCast(Member, EventInfo))
            Case MemberTypes.Field
                Return FieldAsString(DirectCast(Member, FieldInfo))
            Case MemberTypes.Method
                Return MethodAsString(DirectCast(Member, MethodInfo))
            Case MemberTypes.Property
                Return PropertyAsString(DirectCast(Member, PropertyInfo))
            Case MemberTypes.TypeInfo, MemberTypes.NestedType
                Return TypeAsString(DirectCast(Member, Type))
            Case Else
                SaveMessage("Not implemented AsString of member '" & Member.GetType.FullName & "'")
                Return ""
        End Select
    End Function

    Private Sub SaveMessage(ByVal Msg As String)
        Msg = Msg.Replace("%a1%", m_Assembly1.GetName.Name)
        Msg = Msg.Replace("%a2%", m_Assembly2.GetName.Name)
        m_Errors.Add(Msg)
    End Sub

    Private Sub SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As String)
        Msg = String.Format(Msg, parameters)
        Msg = Msg.Replace("%a1%", m_Assembly1.GetName.Name)
        Msg = Msg.Replace("%a2%", m_Assembly2.GetName.Name)
        m_Errors.Add(Msg)
    End Sub

    Private Sub SaveMessage(ByVal Msg As String, ByVal ParamArray parameters() As Object)
        Dim params(UBound(parameters)) As String
        For i As Integer = 0 To UBound(params)
            If parameters(i) Is Nothing Then
                params(i) = "Nothing"
            Else
                Dim paramMember As MemberInfo = TryCast(parameters(i), MemberInfo)
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
