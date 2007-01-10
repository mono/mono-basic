'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Value:
'APV-1.1.0: If variable elements is of value type, i.e. it contains only a value then procedure '		cannot change the variable or any of its members
'=============================================================================================

Imports System
Module APV1_1_0
    Function F(ByVal p As String) As Object
        p = "Sinha"
    End Function

    Function Main() As Integer
        Dim a As String = "Manish"
        F(a)
        If a <> "Manish" Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of APV1_1_0") : Return 1
        End If
    End Function
End Module
'=============================================================================================