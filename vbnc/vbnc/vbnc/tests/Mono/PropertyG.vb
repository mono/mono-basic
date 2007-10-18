'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
'
' Property taking 'ParamArray' parameter, overloading of properties

Imports System
Module M
    Dim a As Integer() = {1, 2, 3}
    Property Prop(ByVal ParamArray i As Integer()) As Integer
        Get
            Return i.Length
        End Get
        Set(ByVal value As Integer)
            a(0) = Value
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

    Function Main() As Integer
        If Prop(2, 3, 4, 5) <> 4 Then
        End If
        Prop(2, 3, 4, 5) = 6
        If (Prop(0) <> 6) Then
            Throw New exception("A#1 - Overloaded properties not working properly")
        End If
        Prop(1) = 9
        If (Prop(1) <> 9) Then
            Throw New exception("A#2 - Overloaded properties not working properly")
        End If
    End Function
End Module
