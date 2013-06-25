Imports A

Namespace Other
    Class Foo
        Shared Function Main() As Integer
            F = 2
            If P <> 1 Then Return 1
            P = 3
            If P <> 1 Then Return 2
            p(1) = 2
            If p(1) <> 2 Then Return 3
            M()
            M(2)
            MF()
            If MF() <> 3 Then Return 4
            MF(2)
            If MF(2) <> 4 Then Return 5
            AddHandler Ev, AddressOf EvHandler

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
    Module B
        Public F As Integer
        Public Property P As Integer
            Get
                Return 1
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Property P(i As Integer) As Integer
            Get
                Return 2
            End Get
            Set(value As Integer)

            End Set
        End Property
        Public Sub M()

        End Sub
        Public Sub M(i As Integer)

        End Sub
        Public Function MF() As Integer
            Return 3
        End Function
        Public Function MF(i As Integer) As Integer
            Return 4
        End Function
        Public Event Ev()

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
    End Module
End Namespace