Imports System
Imports System.Collections
Imports System.Reflection

Class Box1
    Shared Function Main() As Integer
        Dim o As Object
        o = 1
        Return CInt(o) - 1
    End Function
End Class
