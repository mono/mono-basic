Imports System.Collections.Generic
Imports System.Collections
Imports System

Class Test
	Shared Function Main () As Integer
	
		Dim col As New Generic.Dictionary(Of Integer, Type)
'		Dim e As Generic.Dictionary(Of Integer, Type).ValueCollection
		Dim t As Type

		col.Add (1, GetType (Integer))

'		e = col.Values
				
		t = col (1)

	End Function
End Class
