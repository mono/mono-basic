Imports System
Module ConditionalCompilation
    Function Main() As Integer
        Dim value As Integer
        Try
            'Testing #If and #End If Block - Variation 1

#If True Then
			       value=10 
#End If
            If value <> 10 Then
                System.Console.WriteLine("#A11-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If and #End If Block - Variation 2
#If False Then
				System.Console.WriteLine("#A21-Conditional Compilation:Failed"):return 1
#End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If, #Else and #End If Block - Variation 3
#If True Then
				value=30
#Else
            System.Console.WriteLine("#A31-Conditional Compilation:Failed") : Return 1
#End If

            If value <> 30 Then
                System.Console.WriteLine("#A32-Conditional Compilation:Failed") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            'Testing #If, #Else and #End If Block - Variation 4
#If False Then
				System.Console.WriteLine("#A41-Conditional Compilation:Failed"):return 1
#Else
            value = 40
#End If

            If value <> 40 Then
                System.Console.WriteLine("#A42-Conditional Compilation:Failed") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

    End Function
End Module
