<TestFixture()> Public Class MethodOverload1
    <Test()> Public Sub Overload1Test1()
        Assert.AreEqual(1, Overload1(1, 1), "Overload 1 Test 1 failed, expected 1")
    End Sub
    <Test()> Public Sub Overload1Test2()
        Assert.AreEqual(2, Overload1(CLng(1), 1), "Overload 1 Test 2 failed, expected 2")
    End Sub
    Function Overload1(ByVal b As Object, ByVal c As Object) As Integer
        Return 0
    End Function
    Function Overload1(ByVal b As Integer, ByVal c As Integer) As Integer
        Return 1
    End Function
    Function Overload1(ByVal b As Long, ByVal c As Long) As Integer
        Return 2
    End Function
    Function Overload1(ByVal b As Byte, ByVal c As Byte) As Integer
        Return 3
    End Function
    Function Overload1(ByVal b As SByte, ByVal c As SByte) As Integer
        Return 4
    End Function
    Function Overload1(ByVal b As Short, ByVal c As Short) As Integer
        Return 5
    End Function
    Function Overload1(ByVal b As UShort, ByVal c As UShort) As Integer
        Return 6
    End Function
    Function Overload1(ByVal b As UInteger, ByVal c As UInteger) As Integer
        Return 7
    End Function
    Function Overload1(ByVal b As ULong, ByVal c As ULong) As Integer
        Return 8
    End Function
    Function Overload1(ByVal b As Single, ByVal c As Single) As Integer
        Return 9
    End Function
    Function Overload1(ByVal b As Double, ByVal c As Double) As Integer
        Return 10
    End Function
    Function Overload1(ByVal b As Decimal, ByVal c As Decimal) As Integer
        Return 11
    End Function
    Function Overload1(ByVal b As Date, ByVal c As Date) As Integer
        Return 12
    End Function
    Function Overload1(ByVal b As String, ByVal c As String) As Integer
        Return 13
    End Function
    Function Overload1(ByVal b As Char, ByVal d As Char) As Integer
        Return 14
    End Function
End Class