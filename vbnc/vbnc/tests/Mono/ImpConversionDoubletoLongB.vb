'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDoubletoLongC
    Function Main() As Integer
        Dim a As Double = 111.9
        Dim b As Long = 111 + a
        If b <> 223 Then
            System.Console.WriteLine("Addition of Double & Long not working. Expected 223 but got " & b) : Return 1
        End If
    End Function
End Module
