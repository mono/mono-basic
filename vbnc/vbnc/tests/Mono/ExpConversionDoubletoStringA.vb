'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System.Threading
Imports System.Globalization

Module ExpConversionDoubletoStringA
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As Double = 123.052
        Dim b As String = a.toString()
        If b <> "123.052" Then
            System.Console.WriteLine("Conversion of Double to String not working. Expected 123.052 but got " & b) : Return 1
        End If
    End Function
End Module
