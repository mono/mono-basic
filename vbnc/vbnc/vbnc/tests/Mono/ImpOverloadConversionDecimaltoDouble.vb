'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionDecimaltoDouble
    Function fun(ByVal i As Double)
        If i <> 10.5 Then
            System.Console.WriteLine("Implicit Conversion of Decimal to Double not working. Expected 10.5 but got " & i) : Return 1
        End If
    End Function
    Function Main() As Integer
        Dim i As Decimal = 10.5
        fun(i)

    End Function
End Module
