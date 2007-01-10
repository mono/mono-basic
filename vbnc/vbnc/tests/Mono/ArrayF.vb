Imports System

Module ArrayF

    Function Main() As Integer
        Dim arr As Integer(,) = {{1, 2, 3}, {3, 4, 7}}
        ReDim arr(1, 1)
        If arr(0, 0) = 1 Then
            System.Console.WriteLine("#AF1 - ReDim Statement failed") : Return 1
        End If

        arr(0, 0) = 1
        arr(0, 1) = 2
        If arr(0, 0) <> 1 Then
            System.Console.WriteLine("#AF2 - ReDim Statement failed") : Return 1
        End If

        Try
            Erase arr
            Console.WriteLine(arr(0, 0))
        Catch e As Exception
            If e.GetType.ToString <> "System.NullReferenceException" Then
                System.Console.WriteLine("#AF3 - Erase Statement failed") : Return 1
            End If
        End Try

    End Function

End Module
