'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionSingletoByteC
    Function Main() As Integer
        Dim a As Single = 111
        Dim b As Byte = 111 + a
        If b <> 222 Then
            System.Console.WriteLine("Addition of Single & Byte not working. Expected 222 but got " & b) : Return 1
        End If
    End Function
End Module
