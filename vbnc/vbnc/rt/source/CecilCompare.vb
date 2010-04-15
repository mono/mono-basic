Public Class CecilCompare
    Inherits VerificationBase

    Sub New(ByVal Test As Test)
        MyBase.New(Test)
    End Sub

    Protected Overrides Function RunVerification() As Boolean
        Try
            Dim cc As New CecilComparer(Me.Test.OutputVBCAssemblyFull, Me.Test.OutputAssemblyFull)
            Dim result As Boolean = cc.Compare

            For Each Str As String In cc.Errors
                MyBase.DescriptiveMessage &= Str & vbNewLine
            Next

            For Each Str As String In cc.Messages
                MyBase.DescriptiveMessage &= Str & vbNewLine
            Next

            Return result
        Catch ex As Exception
            MyBase.DescriptiveMessage = ex.ToString()
            Return False
        End Try
    End Function
End Class
