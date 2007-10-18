''' <summary>
''' "Method calls with no parameters and with parenthesis."
''' </summary>
''' <remarks></remarks>
<TestFixture()> Public Class MethodCall3
    Private m_Var5 As Integer

    Public Sub New()
        '
    End Sub

    Public Sub New(ByVal Var5 As Integer)
        m_Var5 = Var5
    End Sub

    <Test()> Public Sub Test1()
        Assert.AreEqual(Method1().Method2(), 2, "Shared function call through return value of shared function call failed. (Expected 2).")
    End Sub

    <Test()> Public Sub Test2()
        Assert.AreEqual(Method2(), 2, "Shared function call failed. (Expected 2).")
    End Sub

    <Test()> Public Sub Test3()
        Assert.AreEqual(Method3(), 3, "Instance function call without Me failed. (Expected 3).")
    End Sub

    <Test()> Public Sub Test4()
        Assert.AreEqual(Me.Method4(), 4, "Instance function call with Me failed. (Expected 4).")
    End Sub

    <Test()> Public Sub Test5()
        Dim var As MethodCall3
        var = New MethodCall3(5)
        Assert.AreEqual(var.Method5(), 5, "Instance function call with local variable failed. (Expeced 5).")
    End Sub

    <Test()> Public Sub Test6()
        Assert.AreEqual(Method6().Method3(), 3, "Instance function call through return value of a function call failed. (Expected 3).")
    End Sub

    <Test()> Public Sub Test7()
        Assert.AreEqual(Method6().Method2(), 2, "Shared function call through return value of instance function call failed. (Expected 2).")
    End Sub

    <Test()> Public Sub Test8()
        Assert.AreEqual(Me.Method2(), 2, "Shared function call through Me failed. (Expected 2).")
    End Sub

    Shared Function Method1() As MethodCall3
        Return New MethodCall3
    End Function

    Shared Function Method2() As Integer
        Return 2
    End Function

    Function Method3() As Integer
        Return 3
    End Function

    Function Method4() As Integer
        Return 4
    End Function

    Function Method5() As Integer
        Return m_Var5
    End Function

    Function Method6() As MethodCall3
        Return Me
    End Function
End Class