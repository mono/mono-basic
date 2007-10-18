'==========================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Reference:
'APR-1.0.0: Argument Passing by Reference, which means the procedure can modify the variable
' 		itself.
'===========================================================================================

Imports System
Module APR_1_0_0
    Function F(ByRef p As Integer) As Integer
        p += 1
        Return p
    End Function

    Function Main() As Integer
        Dim a As Integer = 1
        Dim b As Integer = 0
        b = F(a)
        If (b <> a) Then
            System.Console.WriteLine("#A1, Unexcepted Behaviour in Arguments_ByReferenceA.vb") : Return 1
        End If
    End Function
End Module

'============================================================================================