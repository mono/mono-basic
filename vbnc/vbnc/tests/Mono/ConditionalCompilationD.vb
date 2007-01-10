Imports System
Module ConditionalCompilation
    Function Main() As Integer
        'Testing line continuation within conditional compilation statement
        Dim value As Integer
#If _
                True Then
			value=10
#Else
		      _

        System.Console.WriteLine("#D11-Conditional Compilation: Failed") : Return 1
#End If

        If value <> 10 Then
            System.Console.WriteLine("#D12-Conditional Compilation: Failed") : Return 1
        End If
    End Function
End Module

