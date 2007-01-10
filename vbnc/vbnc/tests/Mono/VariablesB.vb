Option Strict Off
Class AA
    Public Shared i As Integer
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New AA()
        o.i = o.i + 1
        fun()
    End Function
    Function fun()
        AA.i = AA.i + 1
        If AA.i <> 2 Then
            System.Console.WriteLine("Shared variable not working") : Return 1
        End If
    End Function
End Module
