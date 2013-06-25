Imports A.A
Imports A.B

Namespace Other
    Class Foo

        Shared Function Main() As Integer
            Dim z1 As D
            Dim z2 As I
            Dim z3 As C
            Dim z4 As E
            Dim z5 As S

            Return 0
        End Function

        Shared Sub EvHandler()

        End Sub
    End Class
End Namespace

Namespace A
    Class A
        Public Shared F As Integer
        Public Shared Property P As Integer
            Get
                Return 1
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Shared Property P(i As Integer) As Integer
            Get
                Return 2
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Shared Sub M()

        End Sub
        Public Shared Sub M(i As Integer)

        End Sub
        Public Shared Function MF() As Integer
            Return 3
        End Function
        Public Shared Function MF(i As Integer) As Integer
            Return 4
        End Function
        Public Shared Event Ev()

        'Nested types
        Public Delegate Sub D()
        Public Interface I
        End Interface
        Public Class C
        End Class
        Public Enum E
            a
        End Enum
        Public Structure S
            Public i As Integer
        End Structure
    End Class

    Class B
        Public Shared F As Integer
        Public Shared Property P As Integer
            Get
                Return 1
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Shared Property P(i As Integer) As Integer
            Get
                Return 2
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Shared Sub M()

        End Sub
        Public Shared Sub M(i As Integer)

        End Sub
        Public Shared Function MF() As Integer
            Return 3
        End Function
        Public Shared Function MF(i As Integer) As Integer
            Return 4
        End Function
        Public Shared Event Ev()

        'Nested types
        Public Delegate Sub D()
        Public Interface I
        End Interface
        Public Class C
        End Class
        Public Enum E
            a
        End Enum
        Public Structure S
            Public i As Integer
        End Structure
    End Class
End Namespace