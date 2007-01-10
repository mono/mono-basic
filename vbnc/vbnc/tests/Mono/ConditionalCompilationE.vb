Imports System
Module ConditionalCompilation
    Function Main() As Integer
        Dim value As Integer
        'Testing whitespaces between #If
        Try
#If True Then
                        	value=50
#End If
            If value <> 50 Then
                System.Console.WriteLine("#A1-Conditional Compilation:Failed ") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

    End Function


#Const A = True
#Const B = False
#If A Then
    Function Z() as object
    End Function
#Else
    Function W() As Object
    End Function
#End If
#If B Then
    Function X() as object
    End Function
#Else
    Function Y() As Object
    End Function
#End If

End Module

