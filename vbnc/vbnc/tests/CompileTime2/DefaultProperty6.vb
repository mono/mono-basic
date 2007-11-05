Imports System.Collections.Generic
Imports System.Collections
Imports System

Class Test

	Shared ReadOnly Property col As Generic.Dictionary (Of Integer, Type)
		Get
			Dim c As New Generic.dictionary(of integer, type)
			c.add (1, gettype (integer))
			return c
		end get
	end property

	Shared Function Main () As Integer
		Dim t As Type

		t = col (1)
		
		Return M (col (1))
	End Function

	Shared Function M (t As Type) As Integer
		If TypeOf t Is Type Then 
			Return 0
		Else
			Return 1
		End If
	End Function
End Class
