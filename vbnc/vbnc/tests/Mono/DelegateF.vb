'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Delegate Function DoubleFunc(ByVal x As Double) As Double
Delegate Function DoubleFunc1(ByVal x As Double) As Double
Class A
    Private f1 As New DoubleFunc1(AddressOf Square)
    Private f As New DoubleFunc(AddressOf Square)
    Overloads Shared Function Square(ByVal x As Single) As Single
        Return x * x
    End Function
    Overloads Shared Function Square(ByVal x As Double) As Double
        Return x * x
    End Function
End Class
Module M
    Function Main() As Integer
    End Function
End Module
