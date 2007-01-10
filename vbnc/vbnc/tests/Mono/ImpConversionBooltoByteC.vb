'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module ImpConversionBooleantoByteC
    Function Main() As Integer
        Dim a As Boolean = True
        Dim b As Byte = 111 + a
        If b <> 110 Then
            System.Console.WriteLine("Addition of Boolean & Byte not working. Expected 110 but got " & b) : Return 1
        End If
    End Function
End Module

