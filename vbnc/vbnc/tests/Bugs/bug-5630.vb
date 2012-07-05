Module Module1

    Public Class Test

        Public Ambiguous As Integer

        Public Class NestedTest

            Public Function Impossible() As Integer

                Return Ambiguous
            End Function
        End Class
    End Class

    Sub Main()

        Dim instance As New Test.NestedTest
        Dim x As Integer = instance.Impossible
        'Runtime error:
        'Unhandled Exception: System.InvalidProgramException: Invalid IL code in ConsoleApplication1.Module1/Test/NestedTest:Impossible (): IL_0001: ldfld   0:      x04000001()
    End Sub
End Module