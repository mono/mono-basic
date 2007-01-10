Imports System
Module ConditionalConstant
    Function Main() As Integer
        Dim value As Integer
        'Testing the scope of the conditional constant
#Const a = True
#If a Then
		   #Const a=False 
		   value=10
#End If
#If a Then
		value=20
#End If
        If value <> 10 Then
            System.Console.WriteLine("ConditionalConstantA:Failed-conditional constant should have global scope") : Return 1
        End If

        'Redefining a conditional constant
#Const a = 5
    End Function
End Module
