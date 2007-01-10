'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDoubletoByteC
    Function Main() As Integer
        Dim a As Double = 111.9
        Dim b As Byte = 111 + a
        If b <> 223 Then
            System.Console.WriteLine("Addition of Double & Byte not working. Expected 223 but got " & b) : Return 1
        End If
    End Function
End Module
