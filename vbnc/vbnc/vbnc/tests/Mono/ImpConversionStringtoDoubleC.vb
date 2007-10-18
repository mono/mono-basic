'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionStringtoDoubleD
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As String = "12.9"
        Dim b As Double = a + 123
        If b <> 135.9 Then
            System.Console.WriteLine("Concat of String & Double not working. Expected  135.9 but got " & b) : Return 1
        End If
    End Function
End Module

