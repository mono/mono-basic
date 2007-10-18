'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionShorttoDoubleA
    Function Main() As Integer
        Dim a As Short = 123
        Dim b As Double
        b = CDbl(a)
        If b <> 123 Then
            System.Console.WriteLine("Short to Double Conversion is not working properly. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
