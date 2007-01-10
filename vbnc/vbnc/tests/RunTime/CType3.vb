Class CType3
    Sub Main()
        Dim fromvalue As Object
        Dim tovalue As ValueType2
        tovalue = CType(fromvalue, ValueType2)
    End Sub
    Structure ValueType2
        Public i As Integer
    End Structure
End Class