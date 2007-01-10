Class ParamArray1
	Sub Test(ByVal ParamArray v As String())

	End Sub
	Sub Test2(ByVal v As String, ByVal ParamArray v2 As Integer())

	End Sub
	sub Test3(v as string())
	end sub
	'Can a paramarray be an array of an array of strings?
	sub Test4(paramarray v as string()()) 
	end sub
End Class