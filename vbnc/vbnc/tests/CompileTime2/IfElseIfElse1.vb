Imports System
Imports System.Collections
Imports System.Reflection

Namespace IfElseIfElse1
    Class Test
        Private Shared Value As Integer
        Shared Sub Test(ByVal p As Integer)
            value = p
        End Sub

        Shared Function Main() As Integer
            Dim i As Integer
            main = 245
            i = 20
            If i = 10 OrElse i < 10 Then
                test(1)
            ElseIf i = 20 OrElse i < 20 Then
                test(2)
            Else
                test(3)
            End If
            Return value - 2
        End Function
    End Class
End Namespace