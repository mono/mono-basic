'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Two overloaded and applicable properties 
Option Strict Off

Imports System
Module M
    Class C
        Dim a() As Integer = {7, 8, 9}
        Property Prop() As Integer()
            Get
                Throw New Exception("Should not come here")
            End Get
            Set(ByVal value As Integer())
                Throw New Exception("Should not come here")
            End Set
        End Property
        Property Prop(ByVal i As Integer) As Integer
            Get
                Return a(i)
            End Get
            Set(ByVal value As Integer)
                a(i) = Value
            End Set
        End Property
    End Class

    Function Main() As Integer
        Dim c1 As New C()
        c1.Prop(2) = 1
        If (c1.Prop(2) <> 1) Then
            Throw New Exception("A#1 Properties not working with arguments.")
        End If
    End Function
End Module
