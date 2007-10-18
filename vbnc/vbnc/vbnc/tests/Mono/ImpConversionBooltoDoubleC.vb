'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionBooleantoDoubleC
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As Boolean = True
        Dim b As Double = 111.9 + a
        If b <> "110.9" Then
            System.Console.WriteLine("Addition of Boolean & Double not working. Expected 110 but got " & b) : Return 1
        End If
    End Function
End Module
