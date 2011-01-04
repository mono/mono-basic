Module IsandIsNotOperators
    Public Function test6()
        If TypeOf ("test") Is
            String Then
            Return 1
        End If
        Return 0
    End Function
End Module
