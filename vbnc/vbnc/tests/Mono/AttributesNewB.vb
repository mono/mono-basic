'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell Inc. (http://www.novell.com)

Imports System

<AttributeUsage(AttributeTargets.Class)> _
Public Class NewAttribute
    Inherits Attribute

    Public Sub New(ByVal A As String)
        Me.A = A
    End Sub

    Public B As String
    Private A As String

    Public ReadOnly Property A1() As String
        Get
            Return A
        End Get
    End Property
End Class

Public Class Class1
    Shared Function Main() As Integer
        Dim type As Type = GetType(Class1)
        Dim arr() As Object = _
                      type.GetCustomAttributes(GetType(NewAttribute), True)
        If arr.Length <> 0 Then
            Throw New Exception("Class1 should not get any Attribute. Lenght of the array should be 0 but got " & arr.Length)
        End If
    End Function

End Class
