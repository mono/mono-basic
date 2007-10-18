'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Optional Keyword:
'O.P-1.0.1: An Optional parameter must specify a constant expression to be used a replacement
'		value if no argument is specified.
'=============================================================================================

Imports System
Module OP1_0_1
    Function F(ByVal telephoneNo As Long, Optional ByVal code As Integer = 80) As Boolean
        If (code = 80) Then
            Return False
        Else
            Return True
        End If
    End Function

    Function Main() As Integer
        Dim telephoneNo As Long = 9886066432
        Dim code As Integer = 81
        Dim status As Boolean
        status = F(telephoneNo, code)
        If (status = False) Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of OP1_0_1") : Return 1
        End If
    End Function

End Module