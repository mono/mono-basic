Imports System

Class StructureMembers1
    Private Shared result As Integer
    Private Shared hole As Object

    Class Tmp
	Sub New (S As S)
	End Sub
    End Class

    Shared Sub Report(ByVal Msg As String)
        Console.WriteLine(Msg)
        result += 1
    End Sub

    Shared Sub Method1(ByRef S As S)
        Dim S2 As S
        S = S2
    End Sub
    Shared Sub Method2(ByRef S As S)
        S = New S
    End Sub

    Shared Sub Dummy()

    End Sub

    Shared Sub TestStructureMembers()
        Dim s As S = New S

        s.Int = 2
        If s.Int <> 2 Then report("TestStructureMembers #1, expected 2, got " & s.Int)

        s.Obj = "abc"
        If CStr(s.obj) <> "abc" Then report("TestStructureMembers #2, expected 'abc', got '" & CStr(s.obj) & "'")

        s.Nil = Nothing
        If s.Nil.HasValue Then Report("TestStructureMembers #3, expected no value")
        'If s.Nil.Value <> 0 = False Then Report("TestStructureMembers #4, expected 0, got " & s.Nil.Value)

        s.Nil = 2
        If s.Nil.HasValue = False Then Report("TestStructureMembers #5, expected some value")
        If s.Nil.Value <> 2 Then Report("TestStructureMembers #6, expected 2, got " & s.Nil.Value)

        s.S = New S1

        s.I = New IC
        If s.I Is Nothing Then Report("TestStructureMembers #7, expected something")

        s.C = New C1
        If s.C Is Nothing Then Report("TestStructureMembers #8, expected something")

        s.D = New D1(AddressOf Dummy)
        If s.D Is Nothing Then Report("TestStructureMembers #9, expected something")

        If s.E <> 0 Then Report("TestStructureMembers #10, expected 0, got " & s.E)
        s.E = E1.A
        If s.E <> E1.A Then Report("TestStructureMembers #11, expected 0, got " & s.E)

        If s.En Is Nothing = False Then Report("TestStructureMembers #12, expected nothing")
        Dim en As System.Enum = E1.A
        s.En = en
        If s.En.Equals(E1.A) = False Then Report("TestStructureMembers #13, expected 1, got " & s.En.ToString)
        s.En = E1.A
        If s.En.Equals(E1.A) = False Then Report("TestStructureMembers #14, expected 1, got " & s.En.ToString)

    End Sub

    Shared Sub TestStructureMembers_NoExecute()
        Dim s As S = New S
        hole = s.C.S.I.S.E
        hole = s.C.S.I.S.D
        hole = s.C.S.E.ToString()
        hole = s.C.S.E.GetType()
        hole = s.C.S.D.ToString()
        hole = s.C.S.ToString()
        hole = s.C.S.FunctionMethod()
    End Sub

    Shared Function Main() As Integer

        TestStructureMembers()

        Return result
    End Function

    Structure S
        Public Int As Integer
        Public Obj As Object
        Public En As System.Enum
        Public Nil As Nullable(Of Integer)
        Public S As S1
        Public I As I1
        Public C As C1
        Public D As D1
        Public E As E1

        Sub New(ByVal I As Integer)
            Int = I
        End Sub

        Sub M ()
	    Dim e As New Tmp (Me)
        End Sub

        Public Overrides Function ToString() As String
            Return ""
        End Function

        Public Sub SubMethod()
            Me.int = 2
        End Sub

        Public Sub A(ByVal Var As Integer)
            A(Me.Int)
            A(Int)
        End Sub

        Function B() As Boolean
            If Me.P(int) Then Return True
            Return P(Int)
            Return P(Me.Int)
        End Function
        Public Function FunctionMethod() As Object
            Return "abc"
        End Function

        ReadOnly Property P(ByVal Index As Integer) As Boolean
            Get
                Return False
            End Get
        End Property
    End Structure


    Structure S1
        Public I As I1
        Public C As C1
        Public D As D1
        Public E As E1

        Public Overrides Function ToString() As String
            Return ""
        End Function

        Public Sub SubMethod()

        End Sub

        Public Function FunctionMethod() As Object
            Return "abc"
        End Function
    End Structure

    Interface I1
        Sub Su()
        'Event Ev()
        Function I() As I1
        Function S() As S1
        Function C() As C1
        Function D() As D1
        Function E() As E1
        Function ET() As System.Enum
        Function VT() As ValueType
        Function Nil() As Nullable(Of Integer)
        Function Int() As Integer
        Function Obj() As Object
    End Interface

    Class C1
        Public Int As Integer
        Public Obj As Object
        Public Nil As Nullable(Of Integer)
        Public S As S1
        Public I As I1
        Public C As C1
        Public D As D1
        Public E As E1
        Public VT As ValueType
        Public ET As System.Enum
    End Class

    Delegate Sub D1()

    Enum E1
        A = 1
    End Enum

    Class IC
        Implements I1

        Public Function C() As C1 Implements I1.C
            Return Nothing
        End Function

        Public Function D() As D1 Implements I1.D
            Return Nothing
        End Function

        Public Function E() As E1 Implements I1.E
            Return Nothing
        End Function

        Public Function ET() As System.Enum Implements I1.ET
            Return Nothing
        End Function

        Public Event Ev() 'Implements I1.Ev

        Public Function I() As I1 Implements I1.I
            Return Nothing
        End Function

        Public Function Int() As Integer Implements I1.Int

        End Function

        Public Function Nil() As System.Nullable(Of Integer) Implements I1.Nil

        End Function

        Public Function Obj() As Object Implements I1.Obj
            Return Nothing
        End Function

        Public Function S() As S1 Implements I1.S
            Return Nothing
        End Function

        Public Sub Su() Implements I1.Su

        End Sub

        Public Function VT() As System.ValueType Implements I1.VT
            Return Nothing
        End Function
    End Class
End Class
