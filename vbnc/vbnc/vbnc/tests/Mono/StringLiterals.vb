Module StringLiterals
    Function Main() As Integer
        Dim s As String
        s = "x"

        Dim a As String = "xyz"
        a = "xyz1"

        If a = "xyz1" Then
        End If

        'Escaped " mark
        ' each "" represents a single " in a string
        a = ("a""b""")

        Dim x As String = "hi"
        Dim y As String = "hi"

        If Not x Is y Then
            System.Console.WriteLine("x and y are different instances")
            Return 1
        End If
    End Function
End Module
