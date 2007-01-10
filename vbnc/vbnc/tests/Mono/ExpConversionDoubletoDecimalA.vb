'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDoubletoDecimalA
    Function Main() As Integer
        Dim a As Decimal
        Dim b As Double = -4.94065645841247E-324
        a = CDec(b)
        If a <> -0 Then
            System.Console.WriteLine("Double to Decimal Conversion is not working properly. Expected 0 but got " & b) : Return 1
        End If
    End Function
End Module
