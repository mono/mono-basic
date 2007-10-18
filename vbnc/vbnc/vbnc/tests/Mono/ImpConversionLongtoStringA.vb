Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ImpConversionLongtoStringA
    Function Main() As Integer
        Dim a As Long = 123
        Dim b As String = a
        If b <> "123" Then
            System.Console.WriteLine("Conversion of Long to String not working. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
