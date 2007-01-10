'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module ImpConversionDoubletoSingleA
    Function Main() As Integer
        Dim a As Single
        Dim b As Double = -4.94065645841247E-324
        a = b
        If a <> -0 Then
            System.Console.WriteLine("Double to Single Conversion is not working properly.") : Return 1
        End If
    End Function
End Module
