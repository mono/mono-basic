'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionSingletoStringC
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As Single = 123.9
        Dim b As String = a + "123"
        If b <> "246.900001525879" Then
            System.Console.WriteLine("Concat of Single & String not working. Expected 246.900001525879 but got " & b) : Return 1
        End If
    End Function
End Module



