REM LineNo: 17
REM ExpectedError: BC30637
REM ErrorMessage: Assembly or Module attribute statements must precede any declaration in a file

Imports System
Imports System.Reflection

<AttributeUsage(AttributeTargets.All)> _
Public Class AuthorAttribute 
     Inherits Attribute
	Public Name
	Public Sub New(ByVal Name As String)
		Me.Name=Name
	End Sub	
End Class

<Assembly:AssemblyVersion("1.0")>

Module Test
	Sub Main()

	End Sub
End Module
