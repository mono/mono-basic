Imports Microsoft.VisualBasic.Strings

Class ExternalModuleFunction1
    Shared Function Main() As Integer
        Dim str As String = "abc"
        Dim i As Integer
        i = instr(str, "a") - 1
        Return i
    End Function
End Class