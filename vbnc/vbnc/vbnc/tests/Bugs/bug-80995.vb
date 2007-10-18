Imports VB = Microsoft.VisualBasic

Class bug_80995
    Shared Function Main() As Integer
        If VB.IsNumeric("0") = False Then
            System.Console.WriteLine("1")
            Return 1
        End If
        Return 0
    End Function
End Class
