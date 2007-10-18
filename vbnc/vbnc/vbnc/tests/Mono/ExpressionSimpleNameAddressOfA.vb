'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'AddressOf
Option Strict Off

Imports System

Class NewClass
    Public i As Integer
    Public Overridable Function MyMethod(ByVal i)
    End Function
    Public Function AMethod(ByVal s As Integer)
        If s <> 10 Then
            Throw New Exception("Unexpected Behavior S should be 10 but got s=" & s)
        End If
    End Function
End Class

Module Test
    Delegate Function ADelegate(ByVal s As Integer)
    Function Main() As Integer
        Dim TestObj As NewClass = New NewClass()
        Dim delg As ADelegate
        delg = New ADelegate(AddressOf TestObj.AMethod)
        delg.Invoke(10)
    End Function
End Module
