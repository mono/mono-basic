Public Class ArrayTypeC6
	Sub a(ByVal arr() As Integer)
	End Sub
	Function b(ByRef a() As String, ByVal c()() As Byte, ByRef d(,,,) As Long, ByVal e(,)(,,) As Integer) As Object()()()
	End Function
	Property c(ByVal val As Integer()()) As String(,)
		Get

		End Get
		Set(ByVal value As String(,))

		End Set
	End Property
	Function d(ByRef a As String(), ByVal c As Byte()(), ByRef b As Long(,,,), ByVal e As Integer(,)(,,)) As Object()()()
	End Function
End Class
Structure ArrayTypeS6
	Public var()(,) As ULong
End Structure
Interface ArrayTypeI6
	Sub a(ByVal arr() As Integer)
	Function b(ByRef a() As String, ByVal c()() As Byte, ByRef d(,,,) As Long, ByVal e(,)(,,) As Integer) As Object()()()
	Property c(ByVal val As Integer()()) As String(,)
End Interface