Option Strict Off

Module LateBinding
    Private field As Integer
    Private Const c As Long = 1234

    Function fun() As Byte
        Return 2
    End Function

    ReadOnly Property RO() As String
        Get
            Return "ro"
        End Get
    End Property

    Private _rw As String = "rw"
    Property RW() As String
        Get
            Return _rw
        End Get
        Set(ByVal value As String)
            _rw = value
        End Set
    End Property

    Function Main() As Integer
        Dim result As Integer

        result += LateCall
        result += LateIndexGet
        result += LateIndexSet
        result += LateGet
        result += LateSet

        Return result
    End Function

    Function LateCall() As Integer
        Dim o As Object = New LateCaller
        Dim tmp As Object

        o.a()
        o.a()

        o.aa(2)

        Dim local As Object = 23
        o.all(field, fun, RO, RW, local, c)
    End Function

    Function LateIndexGet() As Integer
        Dim o As Object
        Dim tmp As Object
        Dim local As Object = 23

        o = New String() {"a", "b"}
        tmp = o(1)

        o = New LateIndexer
        tmp = o(2)
        tmp = o(field, fun, RO, RW, local, c)

    End Function

    Function LateIndexSet() As Integer
        Dim o As Object
        Dim tmp As Object
        Dim local As Object = 23

        o = New String() {"a", "b"}
        o(1) = "c"

        o = New LateIndexer
        o(2) = 3
        o(field, fun, RO, RW, local, c) = 4

    End Function

    Function LateGet() As Integer
        Dim o As Object = New LateXetter
        Dim tmp As Object
        Dim local As Object = 23

        tmp = o.A
        tmp = o.B(2)

        o = New LateCaller
        tmp = o.b
        tmp = o.b()
        tmp = o.All(field, fun, RO, RW, local, c)
    End Function

    Function LateSet() As Integer
        Dim o As Object = New LateXetter
        Dim tmp As Object
        Dim local As Object = 23

        o.A = 2
        o.B(3) = True
        o.All(field, fun, RO, RW, local, c) = False
    End Function

    Class LateXetter
        Property A() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property

        Property B(ByVal i As Integer) As Boolean
            Get

            End Get
            Set(ByVal value As Boolean)

            End Set
        End Property

        Default Property All(ByVal a As Object, ByVal b As Object, ByVal c As Object, ByVal d As Object, ByVal e As Object, ByVal f As Object) As Integer
            Get
                Return 1
            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
    End Class

    Class LateCaller
        Sub a()

        End Sub
        Sub aa(ByVal p As Integer)

        End Sub

        Function b() As Integer

        End Function

        Sub all(ByVal ParamArray pa As Object())

        End Sub
    End Class

    Class LateIndexer
        Default Property a(ByVal index As Object) As Integer
            Get
                Return 1
            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
        Default Property a(ByVal _a As Object, ByVal b As Object, ByVal c As Object, ByVal d As Object, ByVal e As Object, ByVal f As Object) As Integer
            Get
                Return 1
            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
    End Class
End Module