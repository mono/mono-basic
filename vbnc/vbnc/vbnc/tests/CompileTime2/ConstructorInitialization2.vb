Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization2
    Class Test
        Public Shared var1 As New test
        Public Shared var2 As New test()
        Public Shared var3 As New test(2)
        Public value As Integer

        Sub New()
            value = 1
        End Sub
        Sub New(ByVal int As Integer)
            value = int
        End Sub

        Shared Function Main() As Integer
            Return var1.value - 1 + var2.value - 1 + var3.value - 2
        End Function
    End Class
End Namespace