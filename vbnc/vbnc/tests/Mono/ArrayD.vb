Imports System
Imports Microsoft.VisualBasic

Module VariableC
    Dim a() As Integer = {1, 2, 3, 4, 5}

    Function Main() As Integer
        Dim c As Integer

        c = UBound(a, 1)
        c = LBound(a, 1)
        'c = UBound(a)
        'c = LBound(a)

    End Function
End Module
