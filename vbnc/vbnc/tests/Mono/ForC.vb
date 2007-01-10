Imports System

Module ForB
    Class C1
        Function x() As Object
            For index As Integer = 0 To 2
                Console.WriteLine(index)
            Next

        End Function

    End Class

    Function Main() As Integer
        Dim c As New C1()
        c.x()
    End Function

End Module
