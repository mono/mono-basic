'============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: ParamArray:
'APR-1.0.0: ParamArray can be used only on the last argument of argument list. it allows us to 'pass an arbitrary list. It allows us to pass an arbitrary number of argument to the procedure 
'=============================================================================================
Imports System
Module PA_1_0_0
    Function F(ByVal b As Integer, ByVal ParamArray args() As Integer) As Boolean
        Dim a As Integer
        a = args.Length
        If a = b Then
            Return True
        Else
            Return False
        End If
    End Function
    Function Main() As Integer
        Dim a As Integer() = {1, 2, 3}
        Dim c As Integer
        c = a.Length
        Dim b As Boolean
        b = F(c, a)
        If b <> True Then
            System.Console.WriteLine("#A1, Unexcepted Behaviour in F(a)") : Return 1
        End If
    End Function
End Module
'=============================================================================================