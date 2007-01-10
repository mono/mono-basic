Imports System
Imports System.Collections
Imports System.Reflection

Namespace SelectCase2
    Class Test
        Shared Function Main() As Integer
            Dim i As Integer
            i = 5
            Select Case i
                Case 2 To 4
                    Return 1
                Case Else
                    Return 0
            End Select
            Return -1
        End Function
    End Class
End Namespace