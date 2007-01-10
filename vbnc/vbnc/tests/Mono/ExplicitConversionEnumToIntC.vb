'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module EnumToInt

    Enum Days As Byte
        Sunday
        Monday
        Tuesday
        Wednesday
        Thursday
        Friday
        Saturday
    End Enum

    Function Main() As Integer
        Dim i As Integer
        Dim d As Days
        d = Days.Thursday
        i = CInt(d)
        If i <> 4 Then
            System.Console.WriteLine("#Can not Convert from Enum to Integer") : Return 1
        End If
    End Function
End Module

