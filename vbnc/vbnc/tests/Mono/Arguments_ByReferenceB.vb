'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Reference:
'APR-1.4.0: If procedure is define by passing argument by reference and while calling the
'		 procedure it is passes by giving parentheses around the variable then it protect
'		 it from change 
'=============================================================================================

Imports System
Module APR_1_4_0
    Function F(ByRef p As Integer) As Object
        p += 1
    End Function

    Function Main() As Integer
        Dim a As Integer = 1
        F((a))
        If (a <> 1) Then
            System.Console.WriteLine("#A1, Unexpected behavior in Arguments_ByReferenceB.vb") : Return 1
        End If
    End Function
End Module

'==============================================================================================