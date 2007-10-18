Imports System

Module IntegerLiteralTest
    Function Main() As Integer
        Dim i As Integer
        Dim l As Long
        Dim s As Short

        i = 20
        i = System.Int32.MaxValue
        i = System.Int32.MinValue

        l = (System.Int32.MaxValue)
        l = l + 100
        l = System.Int64.MaxValue
        l = System.Int64.MinValue
    End Function
End Module
