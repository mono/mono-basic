'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionShorttoStringA
    Function Main() As Integer
        Dim a As Short = 123
        Dim b As String = a
        If b <> "123" Then
            System.Console.WriteLine("Conversion of Short to String not working. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
