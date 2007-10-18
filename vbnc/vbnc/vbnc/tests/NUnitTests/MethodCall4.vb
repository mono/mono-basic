
''' <summary>
''' Methods with paramarrays.
''' </summary>
''' <remarks></remarks>
<TestFixture()> Public Class MethodCall4
    <Test()> Public Sub CallNoValues()
        Assert.AreEqual(Method0, 0, "Calling paramarray method with 0 parameters and no parenthesis, expected 0")
    End Sub

    <Test()> Public Sub CallNoValues2()
        Assert.AreEqual(Method0(), 0, "Calling paramarray method with 0 parameters and with parenthesis, expected 0")
    End Sub

    <Test()> Public Sub CallStringArray()
        Assert.AreEqual(Method1("Test1"), 1, "Calling method with 1 string paramarray value, expected 1")
    End Sub

    <Test()> Public Sub CallIntArray()
        Assert.AreEqual(Method2(1, 2, 3), 2, "Calling method with 3 integer paramarray values, expected 2")
    End Sub

    Function Method0(ByVal ParamArray value() As Integer) As Integer
        Return 0
    End Function

    Function Method1(ByVal ParamArray value() As String) As Integer
        Return 1
    End Function

    Function Method2(ByVal ParamArray value() As Integer) As Integer
        Return 2
    End Function

End Class
