'MemberAccessExpression: E.I
'If "E" is classified as a variable or value, the type of which is "T", ant "I" is the name
'of an accessible member of "E", then "E.I" is evaluated and classified as follows:
Class MemberAccessExpression5


    'If "I" is the keyword "New" and "E" is an instance expression ("Me", "MyBase", "MyClass"=,
    'then the result is a method group representing the instance constructors of the type
    'of "E" with an associated instance expression of "E" and no type argument list.
    Sub New(ByVal test1 As Integer)
        MyBase.New()
    End Sub

    'If "I" identifies one or more methods, then the result is a method group with 
    'the associated type argument list and an associated instance expression of "E"
    Sub Test2()
        Dim o As Object
        o = o.Tostring
    End Sub

    'If "I" identifies one or more properties, then the result is a property group
    'with an associated instance expression of "E".
    Sub Test3()
        Dim d As Date
        Dim o As Integer
        o = d.day
    End Sub

    'If "I" identifies a shared variable or an instance variable, and if the variable is read-only, and the
    'reference occurs outside the constructor of the class in which the variable is declared appropiate for the kind
    'of variable (shared or instance), then the result is the value of the variable "I" in the object referenced by "E". 
    'If "T" is a reference type, then the result is the variable "I" in the object referenced by "E". Otherwise, if
    '"T" is a value type and the expression "E" is classified as a variable, the result is variable; 
    'otherwise the result is a value.
    Sub Test4()
        Dim n As nested
        Dim o As Object
        o = n.readonlySharedVariable
        o = n.ReadOnlyinstanceVariable
    End Sub

    'If "I" identifies a event, the result is an event access with an associated 
    'instance expression of "E"
    Sub Test5()
        Dim o As nested
        AddHandler o.Someevent, AddressOf test5
    End Sub

    'If "I" identifies a constant, then the result is the value of that constant.
    Sub Test6()
        Dim n As nested
        Dim o As Object
        o = n.constant
    End Sub

    'If "I" identifies an enumeration member, then the result is the value of that
    'enumeration member.
    Sub Test7()
        Dim o As Object
        Dim e As system.dayofweek
        o = e.Monday
    End Sub

    'If "T" is "Object" then the result is a late-bound member lookup classified
    'as a late-bound acces with an associated instance expression of "E"
    Sub Test8()
        'TODO: Implement late bound semantics.
    End Sub

    Class Nested
        Public Const Constant As Integer = 1
        Event SomeEvent()
        Public ReadOnly ReadOnlyInstanceVariable As Integer
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