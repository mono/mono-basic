'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Optional Keyword:
'O.P-1.0.0: An Optional parameter must specify a constant expression to be used a replacement
'		value if no argument is specified.
'=============================================================================================
Option Strict Off

Imports System
Module OP1_0_0
    Function F(ByVal telephoneNo As Long, Optional ByVal code As Integer = 80)
        If (code <> 80) Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of OP1_0_0") : Return 1
        End If
    End Function

    Function Main() As Integer
        Dim telephoneNo As Long = 9886066432
        F(telephoneNo)
    End Function

End Module