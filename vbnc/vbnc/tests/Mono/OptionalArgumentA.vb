Option Strict Off
Imports System
Class C
    Function F(ByVal telephoneNo As Long, Optional ByVal code As Integer = 80, Optional ByVal code1 As Integer = 91, Optional ByRef name As String = "Sinha")
        If (code <> 80 And code1 <> 91 And name = "Sinha") Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of OP1_0_0") : Return 1
        End If
    End Function
End Class
Module OP1_0_0
    Function Main() As Integer
        Dim o As Object = New C()
        Dim telephoneNo As Long = 9886066432
        Dim name As String = "Manish"
        o.F(telephoneNo, , , name)
    End Function

End Module