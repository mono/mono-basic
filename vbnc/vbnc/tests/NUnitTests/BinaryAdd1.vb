<TestFixture()> Public Class BinaryAdd
    <Test()> Sub Add_Integer_Integer()
        Dim i, j As Integer
        i = 1
        j = 1
        Assert.AreEqual(2, i + j, "1 + 1 should be 2")
    End Sub
End Class
