'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionBytetoStringB
    Function Main() As Integer
        Dim a As Byte = 255
        Dim b As String = "111" + a
        If b <> "366" Then
            System.Console.WriteLine("Concat of Byte & String not working. Expected 366 but got " & b) : Return 1
        End If
    End Function
End Module

