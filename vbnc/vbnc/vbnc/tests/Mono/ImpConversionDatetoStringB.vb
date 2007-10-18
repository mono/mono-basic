'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System.Threading
Imports System.Globalization

Module ImpConversionDatetoStringB
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As Date = "1/1/0001 12:00:00 AM"
        Dim b As String = "hello" + a
        If b <> "hello12:00:00 AM" Then
            System.Console.WriteLine("Concat of Date & String not working. Expected helloa but got " & b) : Return 1
        End If
    End Function
End Module



