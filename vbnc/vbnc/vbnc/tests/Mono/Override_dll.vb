REM Target: library

Option Strict Off

Imports System

NameSpace NSOverride
	Public Class B1
		Public Overridable readonly Property SS(i as integer, y as string) as Integer
			get
			End Get
		End Property
	
		Public Overridable writeonly Property SS1(i as integer, y as string) as Integer
			set (Value As Integer)
			End Set
		End Property

		Public Overridable Property SS2(i as integer, y as string) as Integer
			get
			End Get
			set (Value As Integer)
			End Set
		End Property
		
        Public Overridable Function S2(ByVal i As Integer, ByVal j As String)
        End Function
	End Class
	

	Public Class B2
		Inherits B1
		
        Public Overridable Function S1(ByVal i As Integer)
        End Function
	End Class
End NameSpace
