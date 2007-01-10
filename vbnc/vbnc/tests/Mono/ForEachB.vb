Imports System

Module ForEachB

    Class C1
        Public ReadOnly index As Integer = 0

        Sub New()
            Dim arr() As Integer = {1, 2, 3}
            For Each index In arr
            Next
        End Sub

    End Class

    Function Main() As Integer
        Dim c As New C1()
        If c.index <> 3 Then
            System.Console.WriteLine("#FEB1") : Return 1
        End If
    End Function

End Module
