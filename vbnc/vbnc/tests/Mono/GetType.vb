Imports System

Module Test
    Function Main() As Integer
        Dim s As String = GetType(String).ToString()
        If s <> "System.String" Then
            System.Console.WriteLine("#A1: wrong type returned") : Return 1
        End If
        Dim t As Type = GetType(String)
        If Not t Is s.GetType() Then
            System.Console.WriteLine("#A2: wrong type returned") : Return 1
        End If
    End Function
End Module


