Imports System
Imports System.Collections
Imports System.Reflection

Namespace SelectCase1
    Class Test
        Shared Function Main() As Integer
            Dim i As Integer
            Dim result As Integer
            i = 2
            Select Case i
                Case 0
                    result += i
                Case 1
                    result += i
                Case 2
                    result += i
                Case 3
                    result += i
                Case Else
                    result += i
            End Select
            Return result - 2
        End Function
    End Class
End Namespace