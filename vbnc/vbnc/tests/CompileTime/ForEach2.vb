Imports System.Collections

Class ForEach2
    Shared Sub Main()
        Dim i() As Object
        Dim o As Object = i
        Dim c As New ArrayList
        For Each i(2) In c

        Next i
        For Each i(2) In c

        Next i(3)
    End Sub
End Class