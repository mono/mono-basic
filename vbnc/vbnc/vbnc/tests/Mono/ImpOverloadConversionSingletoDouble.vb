'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionSingletoDouble
    Function fun(ByVal i As Double)
        If i <> 10.5 Then
            System.Console.WriteLine("Implicit Conversion of Single to Double not working. Expected 10.5 but got " & i) : Return 1
        End If
    End Function
    Function Main() As Integer
        Dim i As Single = 10.5
        fun(i)

    End Function
End Module
