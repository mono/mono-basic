'============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Param Array:
'APR-1.2.1: If ParamArray modifier is precied by ByVal modifier the it produces doesn't
'		produces compiler error
'============================================================================================
Option Strict Off
Imports System
Module PA_1_2_1
    Function F(ByVal ParamArray args() As Integer)
        Dim a As Integer
        a = args.Length
        If a = 0 Then
            System.Console.WriteLine("#A1, Unexcepted behavoiur of PARAM ARRAY") : Return 1
        End If

    End Function
    Function Main() As Integer
        Dim a As Integer() = {1, 2, 3}
        F(a)
        F(10, 20, 30, 40)
    End Function
End Module

'=================================================================================