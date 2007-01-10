Imports System
Imports System.Collections
Imports System.Reflection

Namespace ByRef11
    Class Test
        Sub Test(ByRef T As Test)

        End Sub
        Sub Tester()
            Test(Me)
        End Sub
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace