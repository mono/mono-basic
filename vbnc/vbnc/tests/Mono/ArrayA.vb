Imports System

Module VariableC
    Dim a() As Integer = {1, 2, 3, 4, 5}
    Dim b(3) As Integer
    Dim c(2), d(4) As Long
    Dim e As Integer() = {1, 2, 3}
    Dim f(2, 3) As Integer
    Dim g As Integer(,) = {{1, 1}, {2, 2}, {3, 3}}
    Dim h() As Integer
    Dim i As Integer(,)
    Dim j As Integer()
    Dim k(4) As System.String

    Function Main() As Integer

        If a(2) <> 3 Then
            System.Console.WriteLine("#A1, value mismatch") : Return 1
        End If

        b(0) = 0
        b(1) = 5
        b(2) = 10
        If b(1) <> 5 Then
            System.Console.WriteLine("#A2, value mismatch") : Return 1
        End If

        If e(1) <> 2 Then
            System.Console.WriteLine("#A3, value mismatch") : Return 1
        End If

        If g(2, 1) <> 3 Then
            System.Console.WriteLine("#A4, value mismatch") : Return 1
        End If


        'Console.WriteLine(e(1))
        'Console.WriteLine(g(2,1))

        h = New Integer() {0, 1, 2}
        i = New Integer(2, 1) {{1, 1}, {2, 2}, {3, 3}}

        j = New Integer(2) {}
    End Function
End Module
