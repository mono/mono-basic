Imports System
Module OP1_0_0
    Function F(ByVal telephoneNo As Long, Optional ByVal code As Integer = 80, Optional ByVal code1 As Integer = 91, Optional ByRef name As String = "Sinha") As Boolean
        If (code <> 80 And code1 <> 91 And name <> "Manish") Then
            Return False
        Else
            name = "Sinha"
            Return True
        End If
    End Function

    Function foo() As Integer
        Return 0
    End Function

    Function Main() As Integer
        Dim telephoneNo As Long = 9886066432
        Dim name As String = "Manish"
        Dim status As Boolean

        status = F(telephoneNo, , , name)
        If (status = False Or name <> "Sinha") Then
            System.Console.WriteLine("#A1, Unexcepted behaviour in string of OP1_0_1") : Return 1
        End If
    End Function

End Module
