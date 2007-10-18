'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Properties with types as arrays
Option Strict Off

Imports System
Module M
    Class C
        Dim a()() As Integer = {New Integer() {1, 2, 3}, New Integer() {4, 5, 6}, New Integer() {7, 8, 9}}
        Property Prop(ByVal i As Integer) As Integer()
            Get
                Return a(i)
            End Get
            Set(ByVal value As Integer())
                a(i) = Value
            End Set
        End Property
    End Class

    Function Main() As Integer
        Dim c1 As New C()
        c1.Prop(2) = New Integer() {1, 2}
        If (c1.Prop(2).Length <> 2) Then
            Throw New Exception("A#1 Properties not working with arguments.")
        End If
        c1.Prop(1)(0) = 7
        If (c1.Prop(1)("0") <> 7) Then
            Throw New Exception("A#2 Properties not working with arguments.")
        End If
    End Function
End Module
