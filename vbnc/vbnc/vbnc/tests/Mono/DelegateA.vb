Imports System

Module M
    Delegate Function SD() As Object
    Function f() As Object
        System.Console.WriteLine("s called")
    End Function

    Public Delegate Function SF(ByVal a As Integer) As Integer
    Public Function f1(ByVal a As Integer) As Integer
        System.Console.WriteLine("f1 called")
        Return a
    End Function

    Public Function TD(ByVal d As SD) As Integer
        d.Invoke()
    End Function

    Function Main() As Integer
        Dim d1 As SD
        d1 = New SD(AddressOf f)
        d1.Invoke()

        Dim d2 As SD
        d2 = AddressOf f
        d2.Invoke()

        Dim d3 As New SD(AddressOf f)
        d3.Invoke()

        Dim d4 As SF
        d4 = New SF(AddressOf f1)
        Dim i As Integer = d4.Invoke(10)
        If i <> 10 Then
            System.Console.WriteLine("#A1, Unexpected result") : Return 1
        End If

        TD(AddressOf f)

    End Function
End Module
