'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Module EnumToInt

    Enum Days
        Sunday
        Monday
        Tuesday
        Wednesday
        Thursday
        Friday
        Saturday
        NotValid = -1
    End Enum

    Function Main() As Integer
        Dim i As Integer = -1
        If Days.NotValid < i Then
            console.writeline("#Can not Convert from Enum to Integer")
            Return 1
        End If
    End Function
End Module
