Module CurlyBraces
    Class ExampleStr
        Public S As String
        Public I As Integer
    End Class
    Public Function test()
        Dim q = New ExampleStr With {
            .S = "string",
            .I = 0
            }
        Return 1
    End Function
End Module
