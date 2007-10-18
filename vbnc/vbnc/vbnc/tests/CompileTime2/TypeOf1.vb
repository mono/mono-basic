Imports System
Imports System.Collections
Imports System.Reflection

Namespace TypeOf1
    Class Test
        Private m_Parent As Object

        Sub SetParent(ByVal obj As Object)
            m_Parent = obj
        End Sub

        Function Test() As Integer
            If TypeOf m_Parent Is Compiler Then
                Return 0
            ElseIf TypeOf Me Is Compiler Then
                Return 1
            Else
                If m_Parent Is Nothing Then Return -1
                Return -2
            End If
        End Function

        Shared Function Main() As Integer
            Dim c As New Compiler
            Dim t As New Test
            If c.Test <> 1 Then Return -1
            If t.Test <> -1 Then Return -2
            t.SetParent(t)
            If t.Test <> -2 Then Return -3
            t.SetParent(c)
            If t.Test <> 0 Then Return -4
            Return 0
        End Function
    End Class

    Class Compiler
        Inherits Test

    End Class
End Namespace