Imports System

Module ForB

    Class C1
        Public index As Integer = 0

        Function x() As Object
            For index = 0 To 2
                Console.WriteLine(index)
            Next
        End Function

    End Class

    Function Main() As Integer
        Dim c As New C1()
        c.x()
        If c.index <> 3 Then
            System.Console.WriteLine("#ForB1") : Return 1
        End If
    End Function

End Module
