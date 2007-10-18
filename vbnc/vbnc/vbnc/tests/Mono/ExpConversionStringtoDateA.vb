'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module ExpConversionStringtoDateA
    Function Main() As Integer
        Dim a As Date
        Dim b As String = "1/1/0001 12:00:00 AM"
        a = CDate(b)
        If a <> "1/1/0001 12:00:00 AM" Then
            System.Console.WriteLine("Conversion of String to Date not working. Expected 1/1/0001 12:00:00 AM but got " & a) : Return 1
        End If
    End Function
End Module
