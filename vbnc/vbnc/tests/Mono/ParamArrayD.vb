Option Strict Off
Imports System
Module Module1
    Class C
        Function F(ByVal ParamArray args() As Short)
            Console.WriteLine("Integer")
            Dim a As Integer
            a = args.Length
        End Function
        Function F(ByVal ParamArray args() As Long)
            System.Console.WriteLine("#A1, Unexcepted behavoiur of PARAM ARRAY") : Return 1
        End Function
    End Class

    Function Main() As Integer
        Dim obj As Object = New C()
        Dim a As Byte = 1
        obj.F(a, a, a, a)
    End Function
End Module
