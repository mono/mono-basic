'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Expected to call double
Option Strict Off

Module ImpConversionSingletoSingle
    Function fun(ByVal i As Decimal)
        Return 1
    End Function
    Function fun(ByVal i As Double)
        Return 2
    End Function
    Function Main() As Integer
        Dim j As Integer
        Dim i As Single = 10
        j = fun(i)
        If j <> 2 Then
            System.Console.WriteLine("Implicit Conversion not working. Expected 1 but got " & j) : Return 1
        End If

    End Function
End Module
