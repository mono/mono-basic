Imports System
Imports System.Collections
Imports System.Reflection

Namespace Select1
    Class Test
        Shared Function Main() As Integer
            Select Case ""
                Case "a"
                    Return 1
                Case Else
                    Return 0
            End Select
        End Function
    End Class
End Namespace