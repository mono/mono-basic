Imports System

Module ArrayG

    Function Main() As Integer
        Dim arr As Integer(,) = {{1, 2, 3}, {3, 4, 7}}
        ReDim arr(-1, -1)
        If arr.Length <> 0 Then
            System.Console.WriteLine("#AG1 - ReDim Statement failed") : Return 1
        End If

        If arr Is Nothing Then
            System.Console.WriteLine("#AG2 - ReDim Statement failed") : Return 1
        End If

        Erase arr
        If Not arr Is Nothing Then
            System.Console.WriteLine("#AG3 - Erase Statement failed") : Return 1
        End If
    End Function

End Module
