Imports System.Collections.Generic

Class bug
	Shared Sub Main ()
		Dim o As Object
		Dim i As IList(Of Object)
		Dim c As New List(Of Object)
		i = c
		o = i.GetEnumerator ()
				
	End Sub

End Class
