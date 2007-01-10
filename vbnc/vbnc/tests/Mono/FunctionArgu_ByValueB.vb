'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Value:
'APV-1.4.0: By Default VB pass arguments by values, i.e if procedure is pass without mentioning
'		type then it take as ByVal type
'=============================================================================================
Imports System
Module APV1_4_0
    Function F(ByVal p As Integer) As Integer
        p += 1
        Return p
    End Function

    Function Main() As Integer
        Dim a As Integer = 1
        Dim b As Integer = 0
        b = F(a)
        If b = a Then
            System.Console.WriteLine("#A1, uncexcepted behaviour of Default VB pass arguments") : Return 1
        End If
    End Function
End Module
'============================================================================================