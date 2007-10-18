Imports System
Imports System.Collections
Imports System.Reflection

Namespace StringHandling1
    Class Test
        ''' <summary>
        ''' This function takes a string as an argument and split it on the space character,
        ''' with the " as acceptable character.
        ''' </summary>
        Shared Function ParseLine(ByVal strLine As String) As String()
            Dim strs As New ArrayList

            strs.Add("a")

            'Add the strings to the return value
            Dim stt(strs.Count - 1) As String
            For i As Integer = 0 To strs.Count - 1
                stt(i) = DirectCast(strs(i), String)
            Next

            Return stt
        End Function

        Shared Function Main(ByVal cmds() As String) As Integer
            Dim args() As String
            Try
                args = Parseline("a")
                Return 0
            Catch ex As exception
                console.writeline(Ex.message)
                console.writeline(ex.stacktrace)
                Return 1
            End Try
        End Function
    End Class
End Namespace