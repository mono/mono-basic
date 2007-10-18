'==========================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Value:
'APV-1.0.0: Argument Passing by value, which means the procedure cannot modify the variable
' 		itself.
'==========================================================================================
Imports System
Module APV1_0
    Function F(ByVal p As Integer) As Object
        p += 1
    End Function

    Function Main() As Integer
        Dim a As Integer = 1
        F(a)
        If a <> 1 Then
            System.Console.WriteLine("#A1, Unexcepted behaviour") : Return 1
        End If
    End Function
End Module
'============================================================================================