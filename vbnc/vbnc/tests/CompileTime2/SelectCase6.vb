Imports System
Imports System.Collections
Imports System.Reflection

Namespace SelectCase6
    Class Test
        Shared Function Main() As Integer
            Dim i As Integer = 0
            Select Case i
                Case 1 To 3
                    Return 1
                Case 0
                    Return 0
                Case 5
                    Return 5
                Case Else
                    Return -1
            End Select
        End Function
    End Class
End Namespace