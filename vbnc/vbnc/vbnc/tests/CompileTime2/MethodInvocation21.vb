Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation21
    Class Result

    End Class
    Class List
        Default ReadOnly Property Item(ByVal index As String) As Result
            Get

            End Get
        End Property
    End Class
    Class Tester

        ReadOnly Property T3() As list
            Get

            End Get
        End Property

        Sub Test()
            Dim t As tester
            t = New tester(t3(""))
        End Sub

        Sub New(ByVal v As Result)

        End Sub
    End Class
    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace