Class NameResolution2_Class
    Dim j As String
    Sub CodeGen()
        Dim i As String
        i = NameResolution2_Namespace.teste.b.c.tostring
    End Sub
    Function test1() As Double

    End Function
    Function test1(ByVal i As Integer) As Double

    End Function
    'Shared Function ist() As Double
    '    Return math.pi
    'End Function
    'Function istt() As Double
    '    Return math.pi
    'End Function
End Class
Namespace NameResolution2_Namespace
    Class testE
        Enum b
            c = 2
        End Enum
        Function a() As String

        End Function
    End Class
End Namespace