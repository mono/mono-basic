

Public Class ErrorInfo
    Private m_Line As Integer
    Private m_Number As Integer
    Private m_Message As String

    Sub New()

    End Sub

    Sub New(ByVal Line As Integer, ByVal Number As Integer, ByVal Message As String)
        m_Line = Line
        m_Number = Number
        m_Message = Message
    End Sub

    Public Property Line As Integer
        Get
            Return m_Line
        End Get
        Set(ByVal value As Integer)
            m_Line = value
        End Set
    End Property

    Public Property Number As Integer
        Get
            Return m_Number
        End Get
        Set(ByVal value As Integer)
            m_Number = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return m_Message
        End Get
        Set(ByVal value As String)
            m_Message = value
        End Set
    End Property

    Shared Function Compare(ByVal expected As ErrorInfo, ByVal actual As ErrorInfo, ByRef result As String) As Boolean
        Dim res As Boolean = True

        result = String.Empty

        If String.IsNullOrEmpty(expected.Message) = False AndAlso String.IsNullOrEmpty(actual.Message) = False AndAlso Not String.Equals(expected.Message, actual.Message) Then
            result += String.Format("Messages differ: Expected: '{0}' vs Actual: '{1}'{2}", expected.Message, actual.Message, Environment.NewLine)
            res = False
        End If

        If expected.Line <> 0 AndAlso actual.Line <> 0 AndAlso expected.Line <> actual.Line Then
            result += String.Format("Line number differ: Expected: '{0}' vs Actual: '{1}'{2}", expected.Line, actual.Line, Environment.NewLine)
            res = False
        End If

        If expected.Number <> actual.Number Then
            result += String.Format("Error number differ: Expected: '{0}' vs Actual: '{1}'{2}", expected.Number, actual.Number, Environment.NewLine)
            res = False
        End If

        Return res
    End Function

    Shared Function ParseLine(ByVal line As String) As ErrorInfo
        Dim idx As Integer
        Dim errline As Integer
        Dim errnumber As Integer
        Dim errmessage As String
        Dim foundSize As Integer = 9

        idx = line.IndexOf(" : error ")
        If idx = -1 Then
            idx = line.IndexOf(" : Command line error ")
            foundSize = 22
        End If
        If idx = -1 Then
            idx = line.IndexOf(" : warning ")
            foundSize = 11
        End If
        If idx = -1 Then Return Nothing

        If idx > 2 AndAlso line(idx - 1) = ")"c Then
            Dim pari As Integer = line.LastIndexOf("("c, idx)
            Dim strline As String = line.Substring(pari + 1, idx - pari - 2)
            If strline.IndexOf(","c) >= 0 Then
                strline = strline.Substring(0, strline.IndexOf(","c))
            End If
            errline = Integer.Parse(strline)
        End If

        line = line.Substring(idx + foundSize)
        If line.StartsWith("BC") Then
            line = line.Substring(2)
        Else
            line = line.Substring(4) 'VBNC
        End If

        idx = line.IndexOf(":"c)
        errnumber = Integer.Parse(line.Substring(0, idx), Globalization.NumberStyles.AllowTrailingWhite Or Globalization.NumberStyles.AllowLeadingWhite)
        errmessage = line.Substring(idx + 1).Trim()

        Return New ErrorInfo(errline, errnumber, errmessage)
    End Function
End Class
