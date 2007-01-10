'============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: ParamArray:
'APR-1.0.0: ParamArray can be used only on the last argument of argument list. it allows us to 'pass an arbitrary list. It allows us to pass an arbitrary number of argument to the procedure 
'=============================================================================================
Imports System
Module PA_1_0_0
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
'=============================================================================================