'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDoubletoSingleD
    Function Main() As Integer
        Dim c As Boolean = False
        Dim a As Single
        Dim b As Double = Double.NaN
        a = CSng(b)
        c = Single.IsNan(a)
        If (c = False) Then
            System.Console.WriteLine("Double to Single Conversion is not working properly. Expected NaN but got " & c) : Return 1
        End If
    End Function
End Module
