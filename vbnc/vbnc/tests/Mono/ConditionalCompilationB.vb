Imports System
Module ConditionalCompilation
    Function Main() As Integer
        Dim value As Integer
        Try
            'Testing #If,#Elseif, #Else and #End If block - Variation 1

#If False Then
                	          System.Console.WriteLine("#B11-Conditional Compilation :Failed"):return 1
#ElseIf True Then
        	                value=10
#Else
            System.Console.WriteLine("#B12-Conditional Compilation :Failed") : Return 1
#End If

            If value <> 10 Then
                System.Console.WriteLine("#B13-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If,#Elseif, #Else and #End If block - Variation 2

#If True Then
				value=20
#ElseIf True Then
				System.Console.WriteLine("#B21-Conditional Compilation :Failed"):return 1
#Else
            System.Console.WriteLine("#B22-Conditional Compilation :Failed") : Return 1
#End If

            If value <> 20 Then
                System.Console.WriteLine("#B23-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If,#Elseif, #Else and #End If block - Variation 3

#If False Then
				System.Console.WriteLine("#B31-Conditional Compilation :Failed"):return 1
#ElseIf False Then
				System.Console.WriteLine("#B32-Conditional Compilation :Failed"):return 1
#Else
            value = 30
#End If

            If value <> 30 Then
                System.Console.WriteLine("#B33-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If,#Elseif, #Else and #End If block - Variation 4

            value = 40
#If False Then
				System.Console.WriteLine("#B41-Conditional Compilation :Failed"):return 1
#ElseIf False Then
				System.Console.WriteLine("#B42-Conditional Compilation :Failed"):return 1
#ElseIf True Then
				value=40
#Else
            System.Console.WriteLine("#B42-Conditional Compilation :Failed") : Return 1
#End If

            If value <> 40 Then
                System.Console.WriteLine("#B33-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try


        Try
            'Testing #If,#Elseif and #End If block - Variation 5

            value = 50

#If False Then
				System.Console.WriteLine("#B51-Conditional Compilation :Failed"):return 1
#ElseIf False Then
				System.Console.WriteLine("#B52-Conditional Compilation :Failed"):return 1
#ElseIf False Then
				System.Console.WriteLine("#B53-Conditional Compilation :Failed"):return 1
#End If

            If value <> 50 Then
                System.Console.WriteLine("#B54-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If,#Elseif and #End If block - Variation 6

#If False Then
				System.Console.WriteLine("#B61-Conditional Compilation :Failed"):return 1
#ElseIf True Then
				value=60
#ElseIf False Then
				System.Console.WriteLine("#B63-Conditional Compilation :Failed"):return 1
#End If

            If value <> 60 Then
                System.Console.WriteLine("#B64-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Function
End Module
