'MemberAccessExpression: E.I
'If "E" is a built-in type of an expression classified as a type, and "I" is the name
'of an accessible member of "E", then "E.I" is evaluated and classified as follows:
Class MemberAccessExpression4

    'If "I" identifies a type, then the result is that type.
    Sub Test1()
        Dim o As MemberAccessExpression4.Nested
    End Sub

    'If "I" identifies one or more methods, then the result is a method group
    'with the associated type argument list and no associated instance expression.
    Sub Test2()
        Nested.SharedSub()
    End Sub

    'If "I" identifies one or more properties, then the result is a property group
    'with no associated instance expression.
    Sub Test3()
        Dim o As Object
        o = nested.sharedproperty
    End Sub

    'If "I" identifies a shared variable, and if the variable is read-only, and the
    'reference occurs outside the shared constructor of the type in which the variable
    'is declared, then the result is the value of the shared variable "I" in "E". Otherwise 
    'the result is the shared variable "I" in "E"
    Sub Test4()
        Dim o As Object
        o = Nested.SharedVariable
        o = Nested.ReadOnlySharedVariable
    End Sub

    'If "I" identifies a shared event, the result is an event access with no associated 
    'instance expression
    Sub Test5()
        Dim o As Object
        AddHandler nested.sharedevent, AddressOf test5
    End Sub

    'If "I" identifies a constant, then the result is the value of that constant.
    Sub Test6()
        Dim o As Object
        o = nested.constant
    End Sub

    'If "I" identifies an enumeration member, then the result is the value of that
    'enumeration member.
    Sub Test7()
        Dim o As Object
        o = System.DayOfWeek.Monday
    End Sub

    Class Nested
        Public Const Constant As Integer = 1
        Shared Event SharedEvent()
        Public Shared SharedVariable As String
        Public Shared ReadOnly ReadOnlySharedVariable As String
        Shared Sub SharedSub()

        End Sub
        Shared Property SharedProperty() As String
            Get

            End Get
            Set(ByVal value As String)

            End Set
        End Property
    End Class
End Class