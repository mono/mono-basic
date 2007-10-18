'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Invoking properties with Named Arguments
Option Strict Off

Imports System
Module M
    Dim a As Integer() = {1, 2, 3}
    Property Prop(ByVal j As Long) As Integer
        Get
            Return a(j)
        End Get
        Set(ByVal value As Integer)
            Throw New exception("Should not come here")
            a(j) = Value
        End Set
    End Property
    Property Prop(ByVal i As Integer) As Integer
        Get
            Throw New exception("Should not come here")
            Return a(i)
        End Get
        Set(ByVal value As Integer)
            a(i) = Value
        End Set
    End Property
    Function Main() As Integer
        Prop(i:=2) = 10
        If (Prop(j:=2) <> 10) Then
            Throw New Exception("A#1 Properties not working with arguments. Expected 10 but got " & Prop(2))
        End If
    End Function
End Module
