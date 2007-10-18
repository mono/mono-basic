'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDoubletoLongA
    Function Main() As Integer
        Dim a As Double = 123.5
        Dim b As Long
        b = a
        If b <> 124 Then
            System.Console.WriteLine("Double to Long Conversion is not working properly. Expected 124 but got " & b) : Return 1
        End If
    End Function
End Module
