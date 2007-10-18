Option Strict Off
Module PA_1_0_0
    Class C
        Function F(ByVal ParamArray args() As Long)
            Dim a As Integer
            a = args.Length
            If a <> 3 Then
                System.Console.WriteLine("#A1, Unexcepted behavoiur of PARAM ARRAY") : Return 1
            End If
        End Function
        Function F(ByVal ParamArray args() As Integer)
            Dim a As Integer
            a = args.Length
            If a <> 4 Then
                System.Console.WriteLine("#A1, Unexcepted behavoiur of PARAM ARRAY") : Return 1
            End If
        End Function
    End Class

    Function Main() As Integer
        Dim obj As Object = New C()
        Dim a As Long() = {1, 2, 3}
        obj.F(a)
        obj.F(10, 20, 30, 40)
    End Function
End Module
