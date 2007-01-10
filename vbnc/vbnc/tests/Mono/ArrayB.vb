Option Strict Off
Imports System

Module ArrayF

    Function Main() As Integer
        Dim arr As Integer(,) = {{1, 2, 3}, {3, 4, 7}}
        ReDim arr(1, 1)
        Dim obj As Object = arr
        If obj(0, 0) = 1 Then
            System.Console.WriteLine("#AF1 - ReDim Statement failed") : Return 1
        End If

        obj(0, 0) = 1
        obj(0, 1) = 2
        If obj(0, 0) <> 1 Then
            System.Console.WriteLine("#AF2 - ReDim Statement failed") : Return 1
        End If

        Try
            Erase arr
            obj = arr
            Console.WriteLine(obj(0, 0))
        Catch e As Exception
            If e.GetType.ToString <> "System.NullReferenceException" Then
                System.Console.WriteLine("#AF3 - Erase Statement failed") : Return 1
            End If
        End Try

    End Function

End Module
