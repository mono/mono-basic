'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Properties as function arguments

Imports System
Module M
    Function F(ByRef p As Integer) As Integer
        p += 1
        Return p
    End Function

    Dim a As Integer = 9
    Property Prop() As Integer
        Get
            Return a
        End Get
        Set(ByVal value As Integer)
            a = value
        End Set
    End Property

    Function Main() As Integer
        F(Prop)
        If (Prop <> 10) Then
            System.Console.WriteLine("#A1, Unexcepted Behaviour in Arguments_ByReferenceA.vb") : Return 1
        End If
    End Function
End Module

