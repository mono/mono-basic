class DirectCastExpression1
	sub Test
		Dim i as DirectCastExpression1
		dim o as object
		o = i
		i = DIrectCast(o, DirectCastExpression1)
	end sub
End Class