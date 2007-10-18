Imports System

Class A
    Public Shared x As Integer = 10

    Shared Sub New()
        Console.WriteLine("Shared ctor")
    End Sub

    Public Sub New()
        Console.WriteLine("ctor")
    End Sub

End Class

Class B
    Inherits A

    Public Shared y As Integer = 20
    Public z As Integer = 30

    Shared Sub New()
        Console.WriteLine("Shared ctor in derived class")
    End Sub

    Public Sub New()
        Console.WriteLine("ctor in derived class")
    End Sub

    Shared Function f() As Object
        Console.WriteLine("f")
    End Function


End Class

Module M
    Function Main() As Integer
        B.y = 25
        If B.y <> 25 Then
            System.Console.WriteLine("#A1 Constructor not working") : Return 1
        End If
        If A.x <> 10 Then
            System.Console.WriteLine("#A2 Constructor not working") : Return 1
        End If
        Dim c As New B()
        If C.z <> 30 Then
            System.Console.WriteLine("#A3 Constructor not working") : Return 1
        End If
    End Function
End Module
