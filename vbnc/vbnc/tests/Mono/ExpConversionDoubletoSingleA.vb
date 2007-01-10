'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDoubletoSingleA
    Function Main() As Integer
        Dim a As Single
        Dim b As Double = -4.94065645841247E-324
        a = CSng(b)
        If a <> -0 Then
            System.Console.WriteLine("Double to Single Conversion is not working properly.") : Return 1
        End If
    End Function
End Module
