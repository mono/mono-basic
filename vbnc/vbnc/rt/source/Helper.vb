Public Class Helper
#If DEBUG Then
    Shared Function nextID() As Integer
        Static id As Integer
        id += 1
        Return id
    End Function
#End If

    ''' <summary>
    ''' Quotes an array of strings.
    ''' </summary>
    ''' <param name="Strings"></param>
    ''' <remarks></remarks>
    Shared Function QuoteStrings(ByVal Strings() As String) As String()
        For i As Integer = 0 To Strings.Length - 1
            If Strings(i).StartsWith("""") = False AndAlso Strings(i).EndsWith("""") = False AndAlso Strings(i).IndexOf(" "c) >= 0 Then
                Strings(i) = """" & Strings(i) & """"
            End If
        Next
        Return Strings
    End Function

    Shared Function NormalizePath(ByVal Path As String) As String
        Return Path.Replace("\", System.IO.Path.DirectorySeparatorChar)
    End Function

    Shared Function IsOnWindows() As Boolean
        Return Environment.OSVersion.Platform <> PlatformID.Unix AndAlso Environment.OSVersion.Platform <> 128
    End Function

    Shared Function IsOnMono() As Boolean
        Return GetType(Integer).GetType.Name = "MonoType"
    End Function

    ''' <summary>
    ''' This function takes a string as an argument and split it on the space character,
    ''' with the " as acceptable character.
    ''' </summary>
    Shared Function ParseLine(ByVal strLine As String) As String()
        Dim strs As New ArrayList
        Dim bInQuote As Boolean
        Dim iStart As Integer
        Dim builder As New System.Text.StringBuilder

        For i As Integer = 0 To strLine.Length - 1
            If strLine.Chars(i) = "\"c AndAlso i < strLine.Length - 1 AndAlso strLine.Chars(i + 1) = """"c Then
                builder.Append(""""c)
            ElseIf strLine.Chars(i) = """"c Then
                If strLine.Length - 1 >= i + 1 AndAlso strLine.Chars(i + 1) = """"c Then
                    builder.Append(""""c)
                Else
                    bInQuote = Not bInQuote
                End If
            ElseIf bInQuote = False AndAlso strLine.Chars(i) = " "c Then
                If builder.ToString.Trim() <> "" Then strs.Add(builder.ToString)
                builder.Length = 0
                iStart = i + 1
            Else
                builder.Append(strLine.Chars(i))
            End If
        Next
        If builder.Length > 0 Then strs.Add(builder.ToString)

        'Add the strings to the return value
        Dim stt(strs.Count - 1) As String
        For i As Integer = 0 To strs.Count - 1
            stt(i) = DirectCast(strs(i), String)
        Next

        Return stt
    End Function
End Class