class T
    Shared Function Main() As Integer
        Dim bt As Byte = 2
        B(bt)
    End Function

    Shared Sub B(ByRef c As Byte)
        System.Console.WriteLine("{0}", c)
    End Sub
End Class
