'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Properties taking/returning types containing default properties
Option Strict Off

Imports System
Module M
    Class C
        Dim a As Integer() = {1, 2, 3, 4, 5}
        Default Property Item(ByVal i As Integer)
            Get
                Return a(i)
            End Get
            Set(ByVal value)
                a(i) = Value
            End Set
        End Property
    End Class
    Class C1
        Dim inst As New C()
        Public Sub New()
            inst = New C()
        End Sub
        Public Property Prop() As C
            Get
                Return inst
            End Get
            Set(ByVal value As C)
                inst = Value
            End Set
        End Property
    End Class
    Function Main() As Integer
        Dim a As New C1()
        a.Prop(1) = 3
        If (a.Prop(1) <> 3) Then
            Throw New Exception("A#1 Property not working")
        End If
    End Function
End Module
