'This test requires one-char newlines.
Namespace CC1
    Class A
        Shared Sub Main()
#If False Then
            Next
#End If
            Return
        End Sub
    End Class
End Namespace