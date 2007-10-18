'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Option Strict Off
Imports System
Imports System.Reflection

<Assembly: AssemblyVersionAttribute("1.0"), Assembly: AssemblyCulture("")> 

<AttributeUsage(AttributeTargets.All)> _
Public Class AuthorAttribute
    Inherits Attribute
    Public Name As Object
    Public Sub New1(ByVal Name As String)
        Me.Name = Name
        If Me.Name <> "a" Then
            Throw New Exception("Expected Me.Name to be a but got, ", Me.Name)
        End If
    End Sub
End Class

Module Test
    Function Main() As Integer
        Dim a As AuthorAttribute = New AuthorAttribute()
        a.New1("a")
    End Function
End Module
