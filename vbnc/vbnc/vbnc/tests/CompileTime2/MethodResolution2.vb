Imports System
Imports System.Collections

Class MethodResolution2
    Public Shared Value As Integer

    Class IndentedTextWriter

    End Class

    Class TheList
        Inherits Generic.List(Of IndentedTextWriter)

    End Class
    Shared Sub DumpCollection(ByVal obj As IList, ByVal Dumper As IndentedTextWriter, Optional ByVal Delimiter As String = "")
        value = 0 'This is the correct method.
    End Sub

    Shared Sub DumpCollection(ByVal obj As IList, ByVal Dumper As IndentedTextWriter, ByVal Prefix As String, ByVal Postfix As String)
        value = 1
    End Sub

    Shared Sub DumpCollection(ByVal obj As IEnumerable, ByVal Dumper As IndentedTextWriter, Optional ByVal Delimiter As String = "")
        value = 2
    End Sub

    Shared Function Main() As Integer
        Dim List As New TheList
        Dim Dumper As New IndentedTextWriter

        DumpCollection(List, Dumper)

        Return value
    End Function
End Class