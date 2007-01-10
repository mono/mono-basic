Namespace NS
    Public Module M1
        Public a As Integer
        Public Const b As Integer = 10
        Class C1
        End Class
    End Module

    Friend Module MainModule
        Function Main() As Integer
            M1.a = 20
            Dim x As Integer = M1.b
            If (x <> 10) Then
                System.Console.WriteLine("#A1, Unexpected result") : Return 1
            End If

            x = NS.M1.b
            If x <> 10 Then
                System.Console.WriteLine("#A2, Unexpected result") : Return 1
            End If
        End Function
    End Module
End Namespace
