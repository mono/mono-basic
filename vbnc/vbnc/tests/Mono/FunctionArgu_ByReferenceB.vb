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
    Function F(ByRef p As Integer) As Integer
        p += 1
        Return p
    End Function

    Function Main() As Integer
        Dim a As Integer = 1
        Dim b As Integer = 0
        b = F((a))
        If (b = a) Then
            Throw New System.Exception("#A1, Unexpected behavior in Arguments_ByReferenceB.vb")
        End If
    End Function
End Module
'=============================================================================================