'============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Param Array:
'APR-1.2.1: If ParamArray modifier is precied by ByVal modifier the it produces doesn't
'		produces compiler error
'============================================================================================
Imports System
Module PA_1_2_1
    Function F(ByVal ParamArray args() As Integer) As Integer
        Dim a As Integer
        a = args.Length
        Return a
    End Function
    Function Main() As Integer
        Dim a As Integer() = {1, 2, 3}
        Dim b As Integer
        b = F(a)
        If b <> 3 Then
            System.Console.WriteLine("#A1, Unexcepted Behaviour in F(a)") : Return 1
        End If

        b = F(10, 20, 30, 40)
        If b <> 4 Then
            System.Console.WriteLine("#A2, Unexcepted Behaviour in F(10,20,30,40)") : Return 1
        End If
        b = F()
        If b <> 0 Then
            System.Console.WriteLine("#A3, Unexcepted Behaviour in F()") : Return 1
        End If
    End Function
End Module

'============================================================================================