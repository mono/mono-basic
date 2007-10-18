#If True Then
Imports System
Module ConditionalCompilation
	Function Main() As Integer
		Dim value As Integer
		Try
		'Testing nested #If,#Elseif, #Else and #End If block 

		#If False          
			System.Console.WriteLine("#C01-Conditional Compilation :Failed"):return 1

        	        #If False
                	          System.Console.WriteLine("#C02-Conditional Compilation :Failed"):return 1
	                #ElseIf True
        	                value=10
                	#Else
                        	  System.Console.WriteLine("#C03-Conditional Compilation :Failed"):return 1
	                #End If

			System.Console.WriteLine("#C04-Conditional Compilation :Failed"):return 1
		#ElseIf True
        	        #If True
				value=20
	                #ElseIf True
				System.Console.WriteLine("#C05-Conditional Compilation :Failed"):return 1
                	#Else
                        	  System.Console.WriteLine("#C06-Conditional Compilation :Failed"):return 1
	                #End If

        	        If value<>20 Then
                	        System.Console.WriteLine("#C07-Conditional Compilation:Failed "):return 1
	                End If

		#ElseIf True
        	        #If False
				System.Console.WriteLine("#C08-Conditional Compilation :Failed"):return 1
	                #ElseIf False
				System.Console.WriteLine("#C09-Conditional Compilation :Failed"):return 1
                	#Else
				value=30
	                #End If

        	        If value<>30 Then
                	        System.Console.WriteLine("#C10-Conditional Compilation:Failed "):return 1
	                End If
		#Else
        	        #If False
				System.Console.WriteLine("#C11-Conditional Compilation :Failed"):return 1
	                #ElseIf True
				System.Console.WriteLine("#C12-Conditional Compilation :Failed"):return 1
                	#ElseIf False
				System.Console.WriteLine("#C13-Conditional Compilation :Failed"):return 1
	                #End If

               	        System.Console.WriteLine("#C14-Conditional Compilation:Failed "):return 1
		#End If
		Catch e As Exception
			Console.WriteLine(e.Message)
		End Try                                                                                                      
	End Function
End Module
#End If