'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module Test
    Function Main() As Integer
        Dim ii As Integer = 0
        Try
            For i As Byte = 2 To 4 Step "hello"
                For j As Integer = 5 To 6
                Next
            Next
        Catch e As System.Exception
            ii = 1
        End Try
        If ii <> 1 Then
            System.Console.WriteLine("For loop not working properly") : Return 1
        End If
    End Function
End Module
