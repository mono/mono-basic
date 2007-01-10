'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionDecimaltoString
    Function fun(ByVal i As String)
        If i <> "10.5" Then
            System.Console.WriteLine("Implicit Conversion of Decimal to String not working. Expected 10.5 but got " & i) : Return 1
        End If
    End Function
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim i As Decimal = 10.5
        fun(i)

    End Function
End Module
