''' <summary>
''' Method calls with default values
''' </summary>
''' <remarks></remarks>
<TestFixture()> Public Class MethodCall2

    <Test()> Public Sub Test1()
        Assert.AreEqual(Method1(), 1, "Function call without specifying default parameter failed.")
    End Sub

    <Test()> Public Sub Test2()
        Assert.AreEqual(Method2(2), 2, "Function call specifying default parameter failed.")
    End Sub

    <Test()> Public Sub Test3()
        Assert.AreEqual(Method3(1, 2), 3, "Function call specifying one default parameter (of two) failed.")
    End Sub

    <Test()> Public Sub Test4()
        Assert.AreEqual(Method4(, 2), 4, "Function call specifying middle default parameter (of three) failed.")
    End Sub

    Function Method1(Optional ByVal value As String = Nothing) As Integer
        Return 1
    End Function

    Function Method2(Optional ByVal value As Integer = 2) As Integer
        Return value
    End Function

    Function Method3(ByVal value1 As Integer, Optional ByVal value2 As Integer = 2, Optional ByVal value3 As Integer = 3) As Integer
        Return 3
    End Function

    Function Method4(Optional ByVal value1 As Integer = 1, Optional ByVal value2 As Integer = 2, Optional ByVal value3 As Integer = 3) As Integer
        Return 4
    End Function
End Class