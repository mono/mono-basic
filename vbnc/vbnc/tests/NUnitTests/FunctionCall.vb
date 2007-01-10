<TestFixture()> Public Class FunctionCall
	<Test()> Sub ReturnValue()
		Dim i As String
		i = "Somevalue"
        i = i.ToString()
		Assert.AreEqual(i, "Somevalue", "i should be 'Somevalue'")
	End Sub
End Class
