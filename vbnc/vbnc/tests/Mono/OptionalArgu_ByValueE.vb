'=============================================================================================
'Name:Manish Kumar Sinha 
'Email Address: manishkumarsinha@sify.com
'Test Case Name: Argument passing by Optional Keyword:
'O.P-1.0.0: An Optional parameter must specify a constant expression to be used a replacement
'		value if no argument is specified.When you omit one or more optional arguments in
'		 the argument list, you use successive commas to mark their positions. 
'		The following example call supplies the first and fourth arguments but not the
'		 second or third:
'=============================================================================================
Option Strict Off

Imports System
Module OP1_0_0
    Function F(ByVal telephoneNo As Long, Optional ByVal code As Integer = 80, Optional ByVal code1 As Integer = 91, Optional ByRef name As String = "Sinha")
        If (code <> 80 And code1 <> 91 And name = "Sinha") Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of OP1_0_0") : Return 1
        End If
    End Function

    Function Main() As Integer
        Dim telephoneNo As Long = 9886066432
        Dim name As String = "Manish"
        F(telephoneNo, , , name)
    End Function

End Module