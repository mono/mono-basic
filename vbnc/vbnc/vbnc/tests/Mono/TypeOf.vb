Imports System

Module M
    Class Test1
    End Class

    Class Test2
    End Class

    Function Main() As Integer
        Dim o As Object
        o = New Test1()
        If Not TypeOf o Is Test1 Then
            System.Console.WriteLine("#A1: TypeOf failed") : Return 1
        End If
        If TypeOf o Is Test2 Then
            System.Console.WriteLine("#A2: TypeOf failed") : Return 1
        End If
    End Function
End Module

