Imports System
'tests interface members
Public Interface Interface1
    Sub a()
    Function b() As String
    Sub c(ByVal param1 As Byte, ByRef param2 As SByte, ByVal param3 As UShort)
    Function d(ByRef param1 As Short, ByVal param2 As UInteger, ByVal param3 As Integer) As ULong
    Delegate Sub e()
    Delegate Function f() As Long
    Event g()
    Event h(ByVal sender As Object, ByVal e As eventargs)
    ReadOnly Property i() As Double
    WriteOnly Property j() As Single
    Property k(ByVal index As Decimal) As Object

End Interface