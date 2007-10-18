Imports System
Imports System.Collections
Imports System.Reflection

Namespace SelectCase3
    Class Test
        Shared Function Main() As Integer
            Dim i As Integer
            i = 2
            Select Case i
                Case 1
                    Return -1
                Case 2, 3
                    Return 0
                Case Else
                    Return -2
            End Select
        End Function
    End Class
End Namespace