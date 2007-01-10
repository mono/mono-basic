'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Delegate Function DoubleFunc(ByVal x As Double) As Double

Class A
    Public f As New DoubleFunc(AddressOf Square)
    Function Square(ByVal x As Double) As Double
        Return x * x
    End Function
End Class

Class AA
    Inherits A
    Function Square(ByVal x As Double) As Double
        Return x * x
    End Function
End Class

Module M
    Function Main() As Integer

    End Function
End Module
