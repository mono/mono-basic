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
End Class