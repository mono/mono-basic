'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System

Module SomeGetType
    Function Main() As Integer
        Dim x As Integer
        Dim y As System.Int32
        If x.GetType() Is y.GetType() Then
        Else
            System.Console.WriteLine("Unexpected Behavior of GetType.expected was Int32 ") : Return 1
        End If
    End Function
End Module