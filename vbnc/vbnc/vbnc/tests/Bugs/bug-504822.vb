Option Strict On

Namespace Bug_504822
	Public Class Foo1
	  Private b As Bar

	  Private Class Bar
	    Public Function Testing() As Integer
	          Return 12
	    End Function
	  End Class

	  Private Class Baz
	     Private parent As Foo1

	     Public Function Test(ByVal i As Integer) As Integer
	         Return parent.b.Testing() + i
	     End Function

	  End Class

	End Class

	Public Class Foo2
	   Public Class Bar
	      Private Structure Baz
	        Public Member As Integer
	      End Structure

	      Public Sub Test()
	         Dim b As New Baz
	         b.Member = 12
	      End Sub
	   End Class
	End Class
End Namespace
