Imports System
Imports System.Collections
Imports System.Reflection

Namespace SelectCase5
    Class Test
        Shared Function Main() As Integer
            Dim c As Char
            c = "0"c
            Select Case c
                Case "1"c
                    Return 1
                Case "2"c
                    Return 2
                Case Else
                    Return 0
            End Select
            Return -1
        End Function
    End Class
End Namespace