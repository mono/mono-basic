'
' StringType.vb
'
' Author:
'   Miguel de Icaza (miguel@novell.com)
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Boris Kirzner (borisk@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


'
' Based on the requirements to run the tests in mbas/Test/tests/expressions
'
'

Imports System
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class StringType

        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromBoolean(ByVal Value As Boolean) As String
            If Value Then
                Return "True"
            Else
                Return "False"
            End If
        End Function

        Public Shared Function FromChar(ByVal Value As Char) As String
            Return Value.ToString
        End Function

        Public Shared Function FromInteger(ByVal Value As Integer) As String
            Return Value.ToString()
        End Function

        Public Shared Function FromDecimal(ByVal Value As Decimal) As String
            Return Value.ToString()
        End Function

        Public Shared Function FromDecimal(ByVal Value As Decimal, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As String
            Return Value.ToString(NumberFormat)
        End Function

        Public Shared Function FromObject(ByVal Value As Object) As String
            If Value Is Nothing Then
                Return Nothing
            End If

            Dim type1 As Type = Value.GetType()
            Select Case Type.GetTypeCode(type1)
                Case TypeCode.Boolean
                    Return Convert.ToString(DirectCast(Value, Boolean))
                Case TypeCode.Byte
                    Return Convert.ToString(DirectCast(Value, Byte))
                Case TypeCode.Char
                    Return Convert.ToString(DirectCast(Value, Char))
                Case TypeCode.DateTime
                    ' Return StringType.FromDate(DirectCast(Value, Date))
                    Return StringType.FromDate(DateType.FromObject(Value))
                Case TypeCode.Double
                    Return Convert.ToString(DirectCast(Value, Double))
                Case TypeCode.Decimal
                    Return Convert.ToString(DirectCast(Value, Decimal))
                Case TypeCode.Int32
                    Return Convert.ToString(DirectCast(Value, Integer))
                Case TypeCode.Int16
                    Return Convert.ToString(DirectCast(Value, Short))
                Case TypeCode.Int64
                    Return Convert.ToString(DirectCast(Value, Long))
                Case TypeCode.Single
                    Return Convert.ToString(DirectCast(Value, Single))
                Case TypeCode.String
                    ' do nothing.
                    Return Value.ToString()
#If TARGET_JVM Then 'These cases are always relevant, however because of a bug in the vbc it is only compiled for jvm
                Case TypeCode.SByte
                    Return Convert.ToString(DirectCast(Value, SByte))
                Case TypeCode.UInt32
                    Return Convert.ToString(DirectCast(Value, UInteger))
                Case TypeCode.UInt16
                    Return Convert.ToString(DirectCast(Value, UShort))
                Case TypeCode.UInt64
                    Return Convert.ToString(DirectCast(Value, ULong))
                Case TypeCode.DBNull
                    Return Convert.ToString(DirectCast(Value, DBNull))
#End If
                Case Else 'TypeCode.Object and other
                    Throw New InvalidCastException
            End Select
        End Function

        Public Shared Function FromDouble(ByVal value As Double) As String
            Return value.ToString()
        End Function

        Public Shared Function FromDouble(ByVal Value As Double, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As String
            Return Value.ToString(NumberFormat)
        End Function

        Public Shared Function FromByte(ByVal value As Byte) As String
            Return value.ToString()
        End Function

        Public Shared Function FromSingle(ByVal value As Single) As String
            Return value.ToString()
        End Function

        Public Shared Function FromSingle(ByVal Value As Single, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As String
            Return Value.ToString(NumberFormat)
        End Function

        Public Shared Function FromLong(ByVal value As Long) As String
            Return value.ToString()
        End Function

        Public Shared Function FromShort(ByVal value As Short) As String
            Return value.ToString()
        End Function

        Public Shared Function StrCmp(ByVal sLeft As String, ByVal sRight As String, ByVal textCompare As Boolean) As Integer
#If TRACE Then
            System.Console.WriteLine("TRACE:StringType.StrCmp: {0} {1}", sLeft, sRight)
#End If

            If sLeft Is Nothing Then
                sLeft = ""
            End If

            If sRight Is Nothing Then
                sRight = ""
            End If

            If textCompare Then
                Return sLeft.CompareTo(sRight)
            Else
                Return String.CompareOrdinal(sLeft, sRight)
            End If
        End Function

        Public Shared Function FromDate(ByVal value As DateTime) As String
#If TRACE Then
            System.Console.WriteLine("TRACE:StringType.FromDate:input:" + value.ToString())
            System.Console.WriteLine("TRACE:StringType.FromDate:output:" + Convert.ToString(value))
#End If
            ' Convert.ToString(value) return a Date and Time
            ' If the input DateTime value contains just date or just time,
            ' the returned string should contain just date or just time.
            ' 
#If TRACE Then
            System.Console.WriteLine("TRACE:StringType.FromDate:value.Hour:" + value.Hour.ToString())
#End If
            'value is just a date
            If ((value.Hour = 0) And (value.Minute = 0) And (value.Millisecond = 0)) Then
                Return value.ToShortDateString
            End If

            'value is just a time
            If ((value.Year = 0) And (value.Month = 0) And (value.Day = 0)) Then
                Return value.ToShortTimeString
            End If

            'this is a date and a time
            Return value.ToString()

        End Function
        Public Shared Sub MidStmtStr(ByRef sDest As String, ByVal StartPosition As Integer, ByVal MaxInsertLength As Integer, ByVal sInsert As String)
            Dim destLen As Integer = sDest.Length
            Dim LenToInsert As Integer

            If MaxInsertLength > sInsert.Length Then
                LenToInsert = sInsert.Length
            ElseIf MaxInsertLength > (destLen - StartPosition) Then
                LenToInsert = ((destLen - StartPosition) + 1)
            Else
                LenToInsert = MaxInsertLength
            End If

            sDest = sDest.Remove(StartPosition - 1, LenToInsert)
            sDest = sDest.Insert(StartPosition - 1, sInsert.Substring(0, LenToInsert))

        End Sub
        Public Shared Function StrLike(ByVal Source As String, ByVal Pattern As String, ByVal CompareOption As Microsoft.VisualBasic.CompareMethod) As Boolean

            If (Source Is Nothing OrElse Source.Length = 0) AndAlso (Pattern Is Nothing OrElse Pattern.Length = 0) Then
                Return True
                ' LAMESPEC : MSDN states "if either string or pattern is an empty string, the result is False."
                ' but "" Like "[]" returns True
            ElseIf ((Source Is Nothing OrElse Source.Length = 0) OrElse (Pattern Is Nothing OrElse Pattern.Length = 0)) AndAlso String.Compare(Pattern, "[]") <> 0 Then
                Return False
            End If

            Dim options As RegexOptions = RegexOptions.Singleline
            If CompareOption = CompareMethod.Text Then
                options = options Or RegexOptions.CultureInvariant Or RegexOptions.IgnoreCase
            End If

            Dim regexString As String = ConvertLikeExpression(Pattern)
            Dim regexpr As Regex = New Regex(regexString, options)

            'Console.WriteLine("{0} --> {1}", Pattern, regexString)

            Return regexpr.IsMatch(Source)

        End Function

        Private Shared Function ConvertLikeExpression(ByVal expression As String) As String
            Dim sb As StringBuilder = New StringBuilder
            Dim bDigit As Boolean = False '' need it in order to clode the string pattern

            For pos As Integer = 0 To expression.Length - 1
                Select Case expression(pos)
                    Case "?"c
                        sb.Append("."c)
                    Case "*"c
                        sb.Append("."c).Append("*"c)
                    Case "#"c  '' only one digit and only once ->  "^\d{1}$"
                        If bDigit Then
                            sb.Append("\d{1}")
                        Else
                            sb.Append("^\d{1}")
                            bDigit = True
                        End If
                    Case "["c
                        Dim gsb As StringBuilder = ConvertGroupSubexpression(expression, pos)
                        ' skip groups of form [], i.e. empty strings
                        If gsb.Length > 2 Then
                            sb.Append(gsb)
                        End If
                    Case Else
                        sb.Append(Regex.Escape(expression(pos).ToString()))
                End Select
            Next
            If bDigit Then sb.Append("$"c)

            Return sb.ToString()
        End Function

        Private Shared Function ConvertGroupSubexpression(ByVal carr As String, ByRef pos As Integer) As StringBuilder
            Dim sb As StringBuilder = New StringBuilder
            Dim negate As Boolean = False

            While Not carr(pos) = "]"c
                If negate Then
                    sb.Append("^"c)
                    negate = False
                End If
                If carr(pos) = "!"c Then
                    sb.Remove(1, sb.Length - 1)
                    negate = True
                Else
                    sb.Append(carr(pos))
                End If
                pos = pos + 1
            End While
            sb.Append("]"c)

            Return sb
        End Function

        Public Shared Function StrLikeBinary(ByVal Source As String, ByVal Pattern As String) As Boolean
            Return StrLike(Source, Pattern, CompareMethod.Binary)
        End Function
        Public Shared Function StrLikeText(ByVal Source As String, ByVal Pattern As String) As Boolean
            Return StrLike(Source, Pattern, CompareMethod.Text)
        End Function
    End Class
End Namespace
