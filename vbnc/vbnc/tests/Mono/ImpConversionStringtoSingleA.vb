'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionStringtoSingleA
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As Single
        Dim b As String = "123.5"
        a = b
        If a <> 123.5 Then
            System.Console.WriteLine("Conversion of String to Single not working. Expected 123.5 but got " & a) : Return 1
        End If
    End Function
End Module
