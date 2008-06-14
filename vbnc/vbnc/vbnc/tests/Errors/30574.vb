'Option Strict without On, no compilation errors should occur
Option Strict

Class T
	Shared function Main as integer
		Dim o As Object = new T ()
		return o.Test ()
	End Function
	Function Test  As INteger
	End Function
End Class
