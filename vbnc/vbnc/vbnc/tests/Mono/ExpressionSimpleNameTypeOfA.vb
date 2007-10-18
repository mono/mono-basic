'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'TypeOf
Option Strict Off

Imports System

Class NewClass
    Public i As Integer
    Public Overridable Function MyMethod(ByVal i)
    End Function
End Class

Class NewClass2
    Public i As Integer
    Public Overridable Function MyMethod(ByVal i)
    End Function
End Class


Module Test
    Function Main() As Integer
        Dim TestObj2 As NewClass2 = New NewClass2()
        Dim TestObj As NewClass = New NewClass()
        If TypeOf Testobj Is NewClass Then
        Else
            System.Console.WriteLine("Unexpected Behavior S should be 10 ") : Return 1
        End If
    End Function
End Module
