'
' Strings.vb
'
' Author:
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
Imports System
Imports System.Collections
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices

Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class Strings

        Private Shared PredefinedNumericFormats As Hashtable
        Private Shared PredefinedDateTimeFormats As Hashtable
        Private Shared PredefinedNumbersAfterDigitalSign() As String = {".00", _
                                                    ".", _
                                                    ".0", _
                                                    ".00", _
                                                    ".000", _
                                                    ".0000", _
                                                    ".00000", _
                                                    ".000000", _
                                                    ".0000000", _
                                                    ".00000000", _
                                                    ".000000000", _
                                                    ".0000000000"}

        Shared Sub New()
            PredefinedNumericFormats = New Hashtable
            PredefinedNumericFormats.Add("General Number", "{0:G}")
            PredefinedNumericFormats.Add("Currency", "{0:C}")
            PredefinedNumericFormats.Add("Fixed", "{0:F}")
            PredefinedNumericFormats.Add("Standard", "{0:N}")
            PredefinedNumericFormats.Add("Percent", "{0:0.00%}")
            PredefinedNumericFormats.Add("percent", "{0:0.00%}")
            PredefinedNumericFormats.Add("Scientific", "{0:0.00E+00}")

            PredefinedDateTimeFormats = New Hashtable
            PredefinedDateTimeFormats.Add("General Date", "{0:G}")
            PredefinedDateTimeFormats.Add("Long Date", "{0:D}")
            'FIXME : check more of Medium Date
            PredefinedDateTimeFormats.Add("Medium Date", "{0:D}")
            PredefinedDateTimeFormats.Add("Short Date", "{0:d}")
            PredefinedDateTimeFormats.Add("Long Time", "{0:T}")
            'FIXME : check more of Medium Time
            PredefinedDateTimeFormats.Add("Medium Time", "{0:T}")
            PredefinedDateTimeFormats.Add("Short Time", "{0:t}")

        End Sub

#If Moonlight = False Then
        Public Shared Function Asc(ByVal [String] As Char) As Integer
            ' Convert.ToUInt16 with ldarg.0 and ret is a good candidate for
            ' inlining and unsigned comparison will be performed later.
            Dim charCode As UShort = Convert.ToUInt16([String])

            ' Fast path for ASCII that also makes Asc incompatible with
            ' non-ASCII-compatible encodings.
            If charCode <= CByte(127) Then
                Return charCode
            End If

            Dim enc As Encoding = Encoding.Default
            Dim chars As Char() = New Char() {[String]}
            Dim bytes As Byte() = New Byte(1) {}

            ' ArgumentException is thrown when more than two bytes are required.
            If enc.GetBytes(chars, 0, 1, bytes, 0) = 1 Then
                Return bytes(0)
            End If

            ' Zero is returned when no characters were encoded.
            Return (CInt(bytes(0)) << 8) Or bytes(1)
        End Function
#End If

        Public Shared Function AscW(ByVal [String] As Char) As Integer
            ' Compiled as if it were "Return CInt([String])" when /novbruntimeref is used;
            ' No AscW or other method is called.
            Return AscW([String])
        End Function

        '#If Moonlight = False Then
        Public Shared Function Asc(ByVal [String] As String) As Integer
            If [String] Is Nothing OrElse [String].Length = 0 Then
                Throw New ArgumentException("Length of argument 'String' must be greater than zero.")
            End If

            Return Asc([String].Chars(0))
        End Function
        '#End If

        Public Shared Function AscW(ByVal [String] As String) As Integer
            If [String] Is Nothing OrElse [String].Length = 0 Then
                Throw New ArgumentException("Length of argument 'String' must be greater than zero.")
            End If

            Return AscW([String].Chars(0))
        End Function

#If Moonlight = False Then
        Public Shared Function Chr(ByVal CharCode As Integer) As Char
            ' Fast path for ASCII that also makes Chr incompatible with
            ' non-ASCII-compatible encodings.
            If CharCode >= 0 AndAlso CharCode <= 127 Then
                ' Convert.ToChar with ldarg.0 and ret is a good candidate for inlining.
                Return Convert.ToChar(CUShort(CharCode))
            End If

            If CharCode < -32768 OrElse CharCode > 65535 Then
                Throw New ArgumentException("Argument 'CharCode' must be within the range of -32768 to 65535.")
            End If

            ' Unlike Asc, Chr is using ANSICodePage that makes them incompatible.
            Dim enc As Encoding = Encoding.GetEncoding(Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage)
            Dim bytes As Byte()
            Dim byteCount As Integer

            If CharCode >= 0 AndAlso CharCode <= 255 Then
                bytes = New Byte() {CByte(CharCode)}
                byteCount = 1
            Else
                ' GetMaxByteCount includes possible fallback characters from EncoderFallback
                If enc.IsSingleByte Then
                    Throw New ArgumentException("Procedure call or argument is not valid.")
                End If

                ' Two bytes are allowed for multi-byte encodings even when
                ' the first byte represents a character by itself.
                bytes = New Byte() {CByte((CharCode And &HFF00) >> 8), CByte(CharCode And &HFF)}
                byteCount = 2
            End If

            Dim chars As Char() = New Char(1) {}

            ' No execption is thrown by Decoders on left over bytes.
            ' ArgumentException is thrown when more than two characters are required.
            enc.GetDecoder().GetChars(bytes, 0, byteCount, chars, 0)

            ' The second character (if present) is ignored.
            ' Zero is returned when no characters were decoded.
            Return chars(0)
        End Function
#Else
        Public Shared Function Chr(CharCode As Integer) As Char
            'Silverlight doesn't include Chr, but vbnc crashes without it
            'This is a dummy implementation until vbnc is fixed
            Return ChrW (CharCode) 
        End Function
#End If

        Public Shared Function ChrW(ByVal CharCode As Integer) As Char
            If CharCode < -32768 OrElse CharCode > 65535 Then
                Throw New ArgumentException("Argument 'CharCode' must be within the range of -32768 to 65535.")
            End If

            ' Convert.ToChar with ldarg.0 and ret is a good candidate for inlining.
            Return Convert.ToChar(CUShort(CharCode And &HFFFF))
        End Function

        Public Shared Function Filter(ByVal Source() As Object, ByVal Match As String, Optional ByVal Include As Boolean = True, _
                        <OptionCompare()> Optional ByVal Compare As CompareMethod = CompareMethod.Binary) As String()
            Dim Temp(Source.Length - 1) As String

            If Compare = CompareMethod.Text Then
                Match = Match.ToLower
            End If

            Dim j As Integer = 0
            For i As Integer = 0 To Source.Length - 1
                Dim s As String = Conversions.ToString(Source(i))

                If Compare = CompareMethod.Text Then
                    s = s.ToLower
                End If

                Dim comparisonResult As Boolean = (s.IndexOf(Match) >= 0)

                If comparisonResult = Include Then
                    Temp(j) = Conversions.ToString(Source(i))
                    j = j + 1
                End If
            Next

            Temp = DirectCast (Utils.CopyArray (Temp, New String (j - 1) {}), String ())

            Return Temp
        End Function

        Public Shared Function Filter(ByVal Source() As String, ByVal Match As String, Optional ByVal Include As Boolean = True, _
                            <OptionCompare()> Optional ByVal Compare As CompareMethod = CompareMethod.Binary) As String()
            Dim oarr() As Object = Source
            Return Filter(oarr, Match, Include, Compare)
        End Function


        Public Shared Function Format(ByVal Expression As Object, Optional ByVal Style As String = "") As String

            If Expression Is Nothing Then
                Return String.Empty
            End If

            If Style Is Nothing Then
                Style = String.Empty
            End If

            Select Case (Type.GetTypeCode(Expression.GetType()))
                'Case TypeCode.Boolean

                Case TypeCode.Byte, TypeCode.Decimal, TypeCode.Double, TypeCode.Int16, _
                    TypeCode.Int32, TypeCode.Int64, TypeCode.SByte, TypeCode.Single, _
                    TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                    Return FormatNumeric(Expression, Style)
                    'Case TypeCode.Char
                Case TypeCode.DateTime
                    Return FormatDateTime(Expression, Style)
                    'Case TypeCode.DBNull
                    'Case TypeCode.String
                    'Case Else
            End Select

            Return String.Empty

        End Function

        Private Shared Function FormatDateTime(ByVal Expression As Object, ByVal Style As String) As String
            Dim PredefinedStyle As Object = PredefinedDateTimeFormats(Style)

            If Not PredefinedStyle Is Nothing Then
                Return String.Format(PredefinedStyle.ToString(), Expression)
            End If
            If Style Is Nothing OrElse Style.Length = 0 Then
                Return String.Format("{0}", Expression)
            Else
                Return String.Format("{0:" + Style + "}", Expression)
            End If
        End Function


        Private Shared Function FormatNumeric(ByVal Expression As Object, ByVal Style As String) As String
            Dim PredefinedStyle As Object = Nothing

            If PredefinedNumericFormats.ContainsKey(Style) Then
                PredefinedStyle = PredefinedNumericFormats(Style)
            End If

            If Not PredefinedStyle Is Nothing Then
                Return String.Format(PredefinedStyle.ToString(), Expression)
            End If

            If String_Compare(Style, "Yes/No", True) = 0 Then
                If Expression.Equals(0) Then
                    Return "No"
                Else
                    Return "Yes"
                End If
            End If

            If String_Compare(Style, "True/False", True) = 0 Then
                If Expression.Equals(0) Then
                    Return "False"
                Else
                    Return "True"
                End If
            End If

            If String_Compare(Style, "On/Off", True) = 0 Then
                If Expression.Equals(0) Then
                    Return "Off"
                Else
                    Return "On"
                End If
            End If

            Return String.Format("{0:" + Style + "}", Expression)

        End Function

        Public Shared Function FormatCurrency(ByVal Expression As Object, Optional ByVal NumDigitsAfterDecimal As Integer = -1, _
                                            Optional ByVal IncludeLeadingDigit As TriState = TriState.UseDefault, _
                                            Optional ByVal UseParensForNegativeNumbers As TriState = TriState.UseDefault, _
                                            Optional ByVal GroupDigits As TriState = TriState.UseDefault) As String

            If (NumDigitsAfterDecimal > 99) Then
                Throw New ArgumentException(" Argument 'NumDigitsAfterDecimal' must be within the range 0 to 99.")
            End If

            Try
                If TypeOf Expression Is String Then
                    Dim tmpstr1 As String
                    Dim tmpstr2 As String = Conversions.ToString(Expression)
                    If ((tmpstr2.StartsWith("(")) And (tmpstr2.EndsWith(")"))) Then
                        tmpstr1 = tmpstr2.Substring(1, tmpstr2.Length - 1)
                        tmpstr2 = tmpstr1.Substring(0, tmpstr1.Length - 2)

                        Dim obj As CultureInfo = System.Globalization.CultureInfo.CurrentCulture()
                        Dim currSym As String = obj.NumberFormat.CurrencySymbol()
                        Dim ch1 As Char = tmpstr2.Chars(0)

                        If Not Char.IsDigit(ch1) Then
                            tmpstr2.TrimStart(currSym.Chars(0))
                        End If
                    End If
                    Convert.ToDouble(tmpstr2)
                End If
            Catch ex As Exception
                Throw New InvalidCastException(" Cast from String to type 'Double' is not valid.")
            End Try

            If Not ((TypeOf Expression Is Short) Or (TypeOf Expression Is Integer) Or (TypeOf Expression Is Long) _
                Or (TypeOf Expression Is Decimal) Or (TypeOf Expression Is Single) Or (TypeOf Expression Is Double) _
                Or (TypeOf Expression Is Byte)) Then
                Throw New InvalidCastException(" Cast to type 'Currency' is not valid.")
            End If


            If NumDigitsAfterDecimal = -1 And IncludeLeadingDigit = TriState.UseDefault _
                And UseParensForNegativeNumbers = TriState.UseDefault And GroupDigits = TriState.UseDefault Then
                Return String.Format("{0:C}", Expression)
            End If

            ' FIXME
            If UseParensForNegativeNumbers = TriState.UseDefault Then
                UseParensForNegativeNumbers = TriState.True
            End If

            Dim sb As StringBuilder = New StringBuilder

            sb.Append("{"c).Append("0"c).Append(":"c)

            FormatCurrency(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)

            If (UseParensForNegativeNumbers = TriState.True) Then
                sb.Append(";"c).Append("("c)
                FormatCurrency(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)
                sb.Append(")"c)
            End If

            sb.Append("}"c)

            Return String.Format(sb.ToString, Expression)

        End Function

        Private Shared Sub FormatCurrency(ByVal sb As StringBuilder, ByVal Expression As Object, ByVal NumDigitsAfterDecimal As Integer, _
                                            ByVal IncludeLeadingDigit As TriState, ByVal UseParensForNegativeNumbers As TriState, _
                                            ByVal GroupDigits As TriState)

            'FIXME : use NumberFormatInfo.CurrencyNegativePattern and NumberFormatInfo.CurrencyPositivePattern

            Dim currencyChar As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol
            sb.Append(currencyChar)
            FormatNumber(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)
            
        End Sub

        Public Shared Function FormatDateTime(ByVal Expression As DateTime, Optional ByVal NamedFormat As DateFormat = DateFormat.GeneralDate) As String

            Dim format As String
            Select Case NamedFormat
                Case DateFormat.GeneralDate
                    If Expression.Year <= 1 Then
                        format = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
                    ElseIf (Expression.Hour = 0 And Expression.Minute = 0 _
                        And Expression.Second = 0 And Expression.Millisecond = 0) Then
                        format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
                    Else
                        format = "G"
                    End If
                Case DateFormat.LongDate
                    format = CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern
                Case DateFormat.ShortDate
                    format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
                Case DateFormat.LongTime
                    format = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
                Case DateFormat.ShortTime
                    format = "HH:mm" 'MSDN states "24-hour format (hh:mm)"
                    'for some reason its not CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern
                Case Else
                    Throw New ArgumentException("Procedure call or argument is not valid.")
            End Select

            Return String.Format("{0:" + format + "}", Expression)
        End Function

        Public Shared Function FormatNumber(ByVal Expression As Object, _
                                Optional ByVal NumDigitsAfterDecimal As Integer = -1, _
                                Optional ByVal IncludeLeadingDigit As TriState = TriState.UseDefault, _
                                Optional ByVal UseParensForNegativeNumbers As TriState = TriState.UseDefault, _
                                Optional ByVal GroupDigits As TriState = TriState.UseDefault) As String

            If (NumDigitsAfterDecimal > 99) Then
                Throw New ArgumentException("Argument 'NumDigitsAfterDecimal' must be within the range 0 to 99")
            End If

            Try
                If TypeOf Expression Is String Then
                    Dim tmpstr2 As String = Conversions.ToString(Expression)
                    Convert.ToDouble(tmpstr2)
                End If

            Catch ex As Exception
                Throw New InvalidCastException(" Cast from String to type 'Double' is not valid.")
            End Try

            If Not ((TypeOf Expression Is Short) Or (TypeOf Expression Is Integer) Or (TypeOf Expression Is Long) _
                Or (TypeOf Expression Is Decimal) Or (TypeOf Expression Is Single) Or (TypeOf Expression Is Double) _
                Or (TypeOf Expression Is Byte)) Then
                Throw New InvalidCastException(" Cast to type 'Currency' is not valid.")
            End If


            ' FIXME : what affects default values
            If UseParensForNegativeNumbers = TriState.UseDefault Then
                UseParensForNegativeNumbers = TriState.False
            End If

            If GroupDigits = TriState.UseDefault Then
                GroupDigits = TriState.True
            End If

            If IncludeLeadingDigit = TriState.UseDefault Then
                IncludeLeadingDigit = TriState.True
            End If

            Dim sb As StringBuilder = New StringBuilder

            sb.Append("{"c).Append("0"c).Append(":"c)

            FormatNumber(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)

            If (UseParensForNegativeNumbers = TriState.True) Then
                sb.Append(";"c).Append("("c)
                FormatNumber(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)
                sb.Append(")"c)
            End If

            sb.Append("}"c)

            Return String.Format(sb.ToString, Expression)

        End Function

        Private Shared Sub FormatNumber(ByVal sb As StringBuilder, ByVal Expression As Object, _
                                Optional ByVal NumDigitsAfterDecimal As Integer = -1, _
                                Optional ByVal IncludeLeadingDigit As TriState = TriState.UseDefault, _
                                Optional ByVal UseParensForNegativeNumbers As TriState = TriState.UseDefault, _
                                Optional ByVal GroupDigits As TriState = TriState.UseDefault)

            'FIXME : perform more elegant type checking
            'Expression = DirectCast(Expression, Double)

            If (GroupDigits = TriState.True) Then
                sb.Append("#"c).Append(","c).Append("#"c).Append("#"c).Append("#"c)
            End If

            If (IncludeLeadingDigit = TriState.True) Then
                sb.Append("0"c)
            End If

            sb.Append("."c)
            If NumDigitsAfterDecimal = -1 Then
                sb.Append("0"c).Append("0"c)
            Else
                For i As Integer = 1 To NumDigitsAfterDecimal
                    sb.Append("0"c)
                Next
            End If
        End Sub

        Public Shared Function FormatPercent(ByVal Expression As Object, Optional ByVal NumDigitsAfterDecimal As Integer = -1, _
                                            Optional ByVal IncludeLeadingDigit As TriState = TriState.UseDefault, _
                                            Optional ByVal UseParensForNegativeNumbers As TriState = TriState.UseDefault, _
                                            Optional ByVal GroupDigits As TriState = TriState.UseDefault) As String

            'FIXME : should throw IllegalCastException
            'Expression = CDbl(Expression)
            If (NumDigitsAfterDecimal < -1) Then
                Throw New ArgumentException("Argument 'NumDigitsAfterDecimal' is Invalid")
            End If

            Try
                If TypeOf Expression Is String Then
                    Dim tmpstr2 As String = Conversions.ToString(Expression)
                    Convert.ToDouble(tmpstr2)
                End If

            Catch ex As Exception
                Throw New InvalidCastException(" Cast from String to type 'Double' is not valid.")
            End Try

            If Not ((TypeOf Expression Is Short) Or (TypeOf Expression Is Integer) Or (TypeOf Expression Is Long) _
                Or (TypeOf Expression Is Decimal) Or (TypeOf Expression Is Single) Or (TypeOf Expression Is Double) _
                Or (TypeOf Expression Is Byte)) Then
                Throw New InvalidCastException(" Cast to type 'Currency' is not valid.")
            End If
            ' FIXME : what affects default values
            If UseParensForNegativeNumbers = TriState.UseDefault Then
                UseParensForNegativeNumbers = TriState.False
            End If

            If GroupDigits = TriState.UseDefault Then
                GroupDigits = TriState.True
            End If

            If IncludeLeadingDigit = TriState.UseDefault Then
                IncludeLeadingDigit = TriState.True
            End If

            Dim sb As StringBuilder = New StringBuilder

            sb.Append("{"c).Append("0"c).Append(":"c)

            FormatPercent(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)

            If (UseParensForNegativeNumbers = TriState.True) Then
                sb.Append(";"c).Append("("c)
                FormatPercent(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)
                sb.Append(")"c)
            End If

            sb.Append("}"c)

            Return String.Format(sb.ToString, Expression)

        End Function

        Private Shared Sub FormatPercent(ByVal sb As StringBuilder, ByVal Expression As Object, ByVal NumDigitsAfterDecimal As Integer, _
                                            ByVal IncludeLeadingDigit As TriState, ByVal UseParensForNegativeNumbers As TriState, _
                                            ByVal GroupDigits As TriState)

            'FIXME : use NumberFormatInfo.PercentNegativePattern and NumberFormatInfo.PercentPositivePattern

            Dim percentChar As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol
            FormatNumber(sb, Expression, NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits)
            sb.Append(percentChar)

        End Sub

        Public Shared Function GetChar(ByVal str As String, ByVal Index As Integer) As Char
            If str Is Nothing Then
                Throw New ArgumentException("Length of argument 'String' must be greater than zero.")
            End If

            If Index < 1 Then
                Throw New ArgumentException("Argument 'Index' must be greater than or equal to 1.")
            End If

            If Index > str.Length Then
                Throw New ArgumentException("Argument 'Index' must be less than or equal to the length of argument 'String'.")
            End If

            Return str.Chars(Index - 1)

        End Function


        Public Shared Function InStr(ByVal Start As Integer, ByVal String1 As String, ByVal String2 As String, _
                                <OptionCompare()> Optional ByVal Compare As Microsoft.VisualBasic.CompareMethod = 0) As Integer

            If Start < 1 Then
                Throw New ArgumentException("Argument 'Start' must be greater or equal to zero.")
            End If

            If String1 Is Nothing Then
                Return 0
            End If
            If String1.Length = 0 Then
                Return 0
            End If

            If String2 Is Nothing Then
                Return Start
            End If
            If String2.Length = 0 Then
                Return Start
            End If

            If Start > String1.Length Then
                Return 0
            End If

            If Compare = CompareMethod.Text Then
                String1 = String1.ToLower()
                String2 = String2.ToLower()
            End If

            Dim i As Integer = String1.IndexOf(String2, Start - 1)

            Return i + 1

        End Function

        Public Shared Function InStr(ByVal String1 As String, ByVal String2 As String, _
                               <OptionCompare()> Optional ByVal Compare As Microsoft.VisualBasic.CompareMethod = 0) As Integer
            Return InStr(1, String1, String2, Compare)
        End Function

        Public Shared Function InStrRev(ByVal StringCheck As String, ByVal StringMatch As String, _
                                        Optional ByVal Start As Integer = -1, _
                                        <OptionCompare()> Optional ByVal Compare As Microsoft.VisualBasic.CompareMethod = 0) As Integer
            If Start = 0 Or Start < -1 Then
                Throw New ArgumentException("Argument 'Start' must be greater than 0 or equal to -1.")
            End If

            If StringCheck Is Nothing Then
                Return 0
            End If
            If StringCheck.Length = 0 Then
                Return 0
            End If

            If StringMatch Is Nothing Then
                Return Start
            End If
            If StringMatch.Length = 0 Then
                Return Start
            End If

            If Start > StringCheck.Length Then
                Return 0
            End If

            If Start = -1 Then
                Start = StringCheck.Length
            End If

            If Compare = CompareMethod.Text Then
                StringMatch = StringMatch.ToLower()
                StringCheck = StringCheck.ToLower()
            End If

            Dim i As Integer = StringCheck.LastIndexOf(StringMatch, Start - 1, Start)

            Return i + 1

        End Function

        Public Shared Function Join(ByVal SourceArray() As Object, Optional ByVal Delimiter As String = " ") As String
            Dim i As Integer
            Dim sb As StringBuilder = New StringBuilder

            If (SourceArray Is Nothing) Then
                Return Nothing
            End If
            If (SourceArray.Length = 0) Then
                Return Nothing
            End If


            If TypeOf SourceArray(0) Is Array Then
                Throw New ArgumentException("Procedure call or argument is not valid")
            End If

            If SourceArray.Rank > 1 Then
                Throw New ArgumentException("Argument 'SourceArray' cannot be converted to type 'String'")
            End If

            For i = 0 To SourceArray.Length - 2
                sb.Append(Conversions.ToString(SourceArray(i)))
                If Not Delimiter Is Nothing Then
                    sb.Append(Delimiter)
                End If
            Next

            sb.Append((SourceArray(i)))

            Return sb.ToString()

        End Function

        Public Shared Function Join(ByVal SourceArray() As String, Optional ByVal Delimiter As String = " ") As String
            Dim oarr() As Object
            oarr = SourceArray
            Return Join(oarr, Delimiter)
        End Function

        Public Shared Function LCase(ByVal Value As Char) As Char
            Return Char.ToLower(Value)
        End Function

        Public Shared Function LCase(ByVal Value As String) As String
            If Value Is Nothing Then
                Return Nothing
            End If

            Return Value.ToLower()
        End Function

        Public Shared Function Left(ByVal str As String, ByVal Length As Integer) As String

            If Length < 0 Then
                Throw New ArgumentException("Argument 'Length' must be greater or equal to zero.")
            End If

            If str Is Nothing Or Length = 0 Then
                Return String.Empty
            End If

            If Length > str.Length Then
                Length = str.Length
            End If

            Return str.Substring(0, Length)

        End Function

        Public Shared Function Len(ByVal Expression As Boolean) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Byte) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Char) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Double) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Integer) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Long) As Integer
            Return GetSize(Expression)
        End Function

#If Not MOONLIGHT Then
        Public Shared Function Len(ByVal Expression As Object) As Integer
            Return GetSize(Expression)
        End Function
#End If

        Public Shared Function Len(ByVal Expression As Short) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Single) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As String) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As DateTime) As Integer
            Return GetSize(Expression)
        End Function

        Public Shared Function Len(ByVal Expression As Decimal) As Integer
            Return GetSize(Expression)
        End Function

        Private Shared Function GetSize(ByVal Expression As Object) As Integer
            If Expression Is Nothing Then
                Return 0
            End If

            Dim Tcode As TypeCode = Type.GetTypeCode(Expression.GetType())

            Select Case Tcode
                Case TypeCode.Boolean
                    Return 2
                Case TypeCode.Byte
                    Return 1
                Case TypeCode.Char
                    Return 2
                Case TypeCode.DateTime
                    Return 8
                Case TypeCode.DBNull
                    Throw New InvalidCastException
                Case TypeCode.Decimal
                    Return 8
                Case TypeCode.Double
                    Return 8
                Case TypeCode.Empty
                    Return 0
                Case TypeCode.Int16
                    Return 2
                Case TypeCode.Int32
                    Return 4
                Case TypeCode.Int64
                    Return 8
                Case TypeCode.Object
                    Throw New InvalidCastException
                Case TypeCode.SByte
                    Return 1
                Case TypeCode.Single
                    Return 4
                Case TypeCode.String
                    Return Conversions.ToString(Expression).Length
                Case TypeCode.UInt16
                    Return 2
                Case TypeCode.UInt32
                    Return 4
                Case TypeCode.UInt64
                    Return 8
            End Select

        End Function

        Public Shared Function LSet(ByVal Source As String, ByVal Length As Integer) As String
            If Source Is Nothing Then
                Source = String.Empty
            End If

            If Source.Length >= Length Then
                Return Source.Substring(0, Length)
            Else
                Return Source.PadRight(Length)
            End If
        End Function

        Public Shared Function LTrim(ByVal str As String) As String
            If str Is Nothing Then
                Return String.Empty
            End If

            Dim carr() As Char = {" "c}
            Return str.TrimStart(carr)
        End Function

        Public Shared Function Mid(ByVal str As String, ByVal Start As Integer, ByVal Length As Integer) As String

            If Start <= 0 Then
                Throw New ArgumentException("Argument 'Start' is not a valid value.")
            End If

            If Length < 0 Then
                Throw New ArgumentException("Argument 'Length' is not a valid value.")
            End If

            If str Is Nothing OrElse str.Length = 0 OrElse Length = 0 Then
                Return String.Empty
            End If

            If Start > str.Length Then
                Return String.Empty
            End If

            If Start + Length > str.Length Then
                Return str.Substring(Start - 1)
            Else
                Return str.Substring(Start - 1, Length)
            End If

        End Function

        Public Shared Function Mid(ByVal str As String, ByVal Start As Integer) As String
            If str Is Nothing Then
                Return Nothing
            End If

            Return Mid(str, Start, str.Length)

        End Function

        Public Shared Function Replace(ByVal Expression As String, ByVal Find As String, ByVal Replacement As String, _
                                Optional ByVal Start As Integer = 1, Optional ByVal Count As Integer = -1, _
                                <OptionCompare()> Optional ByVal Compare As CompareMethod = CompareMethod.Binary) As String
            If Count < -1 Then
                Throw New ArgumentException("Argument 'Count' must be greater than or equal to -1.")
            End If

            If Start <= 0 Then
                Throw New ArgumentException("Argument 'Start' must be greater than zero.")
            End If

            If Expression Is Nothing OrElse Expression.Length = 0 Then
                Return Nothing
            End If

            If Start > Expression.Length Then
                Return Nothing
            End If

            If Find Is Nothing OrElse Find.Length = 0 Then
                Return Expression
            End If

            Expression = Expression.Substring(Start - 1)

            Dim IgnoreCase As Boolean = False
            If Compare = CompareMethod.Text Then
                IgnoreCase = True
            End If

            Return Replace(Expression, Find, Replacement, Start, Count, IgnoreCase)

        End Function

        Private Shared Function Replace(ByVal Expression As String, ByVal Find As String, ByVal Replacement As String, _
                                ByVal Start As Integer, ByVal Count As Integer, ByVal IgnoreCase As Boolean) As String

            Dim replaced As Integer = 0
            Dim current As Integer = 0
            Dim carr() As Char = Expression.ToCharArray()
            Dim sb As StringBuilder = New StringBuilder(Expression.Length)

            While (replaced < Count Or Count = -1) And current < Expression.Length
                Dim res As Integer = String_Compare(Expression, current, Find, 0, Find.Length, IgnoreCase)
                If res = 0 Then
                    sb.Append(Replacement)
                    current = current + Find.Length
                    replaced = replaced + 1
                Else
                    sb.Append(carr(current))
                    current = current + 1
                End If
            End While

            sb.Append(Expression.Substring(current))

            Return sb.ToString()
        End Function


        Public Shared Function Right(ByVal str As String, ByVal Length As Integer) As String

            If Length < 0 Then
                Throw New ArgumentException("Argument 'Length' must be greater or equal to zero")
            End If

            If str Is Nothing OrElse str.Length = 0 Then
                Return String.Empty
            End If

            If Length >= str.Length Then
                Return str
            End If

            Return str.Substring(str.Length - Length)

        End Function

        Public Shared Function RSet(ByVal Source As String, ByVal Length As Integer) As String
            If Source Is Nothing Then
                Source = String.Empty
            End If

            If Source.Length >= Length Then
                Return Source.Substring(0, Length)
            Else
                Return Source.PadLeft(Length)
            End If
        End Function

        Public Shared Function RTrim(ByVal str As String) As String
            If str Is Nothing Then
                Return String.Empty
            End If

            Dim carr() As Char = {" "c}
            Return str.TrimEnd(carr)
        End Function

        Public Shared Function Space(ByVal Number As Integer) As String
            If Number < 0 Then
                Throw New ArgumentException("Argument 'Number' must be greater or equal to zero.")
            End If

            Select Case Number
                Case 0
                    Return String.Empty
                Case 1
                    Return " "
                Case 2
                    Return "  "
                Case 3
                    Return "   "
                Case 4
                    Return "    "
                Case 5
                    Return "     "
                Case 6
                    Return "      "
                Case 7
                    Return "       "
                Case 8
                    Return "        "
                Case 9
                    Return "         "
                Case 10
                    Return "          "
                Case Else
                    Dim carr(Number - 1) As Char
                    For i As Integer = 0 To Number - 1
                        carr(i) = " "c
                    Next
                    Return New String(carr)
            End Select
        End Function

        Public Shared Function Split(ByVal Expression As String, Optional ByVal Delimiter As String = " ", _
                Optional ByVal Limit As Integer = -1, _
                <OptionCompare()> Optional ByVal Compare As CompareMethod = CompareMethod.Binary) As String()

            If Expression Is Nothing OrElse Expression.Length = 0 Then
                Dim r(0) As String
                r(0) = String.Empty
                Return r
            End If

            If Delimiter Is Nothing OrElse Expression.Length = 0 Then
                Dim r(0) As String
                r(0) = Expression
                Return r
            End If

            Dim carr() As Char = Expression.ToCharArray()
            Dim current As Integer = 0
            Dim IgnoreCase As Boolean = False
            If Compare = CompareMethod.Text Then
                IgnoreCase = True
            End If

            If Limit = -1 Then
                Limit = 0
                While current < Expression.Length
                    Dim res As Integer = String_Compare(Expression, current, Delimiter, 0, Delimiter.Length, IgnoreCase)
                    If res = 0 Then
                        current = current + Delimiter.Length
                        Limit = Limit + 1
                    Else
                        current = current + 1
                    End If
                End While
                Limit = Limit + 1
            End If

            Dim sarr(Limit - 1) As String
            Dim count As Integer = 0
            Dim previous As Integer = 0
            current = 0
            While current < Expression.Length And count < Limit - 1
                Dim res As Integer = String_Compare(Expression, current, Delimiter, 0, Delimiter.Length, IgnoreCase)
                If res = 0 Then
                    sarr(count) = Expression.Substring(previous, current - previous)
                    current = current + Delimiter.Length
                    previous = current
                    count = count + 1
                Else
                    current = current + 1
                End If
            End While

            sarr(count) = Expression.Substring(previous)

            sarr = DirectCast (Utils.CopyArray (sarr, New String (count) {}), String ())
            Return sarr

        End Function

        Friend Shared Function String_Compare(ByVal strA As String, ByVal strB As String, ByVal ignoreCase As Boolean) As Integer
            Return String.Compare(strA, strB, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase)
        End Function

        Friend Shared Function String_Compare(ByVal strA As String, ByVal indexA As Integer, ByVal strB As String, ByVal indexB As Integer, ByVal length As Integer, ByVal ignoreCase As Boolean) As Integer
            Return String.Compare(strA, indexA, strB, indexB, length, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase)
        End Function

        Public Shared Function StrComp(ByVal String1 As String, ByVal String2 As String, _
                        <OptionCompare()> Optional ByVal Compare As CompareMethod = 0) As Integer

            If String1 Is Nothing Then
                String1 = String.Empty
            End If

            If String2 Is Nothing Then
                String2 = String.Empty
            End If

            Dim res As Integer
            If Compare = CompareMethod.Binary Then
                res = String.CompareOrdinal(String1, String2)
            Else
                res = String_Compare(String1, String2, True)
            End If

            If res > 0 Then
                Return 1
            ElseIf res < 0 Then
                Return -1
            Else
                Return 0
            End If

        End Function

#If Not MOONLIGHT Then
        Public Shared Function StrConv(ByVal str As String, _
                                        ByVal Conversion As VbStrConv, _
                                        Optional ByVal LocaleID As Integer = 0) As String

            If str Is Nothing Then
                Throw New ArgumentNullException("Value can not be null.")
            End If

            Select Case Conversion
                Case VbStrConv.None
                    Return str
                Case VbStrConv.Uppercase
                    Return str.ToUpper()
                Case VbStrConv.Lowercase
                    Return str.ToLower()
                Case VbStrConv.ProperCase
                    Dim carr() As Char = str.ToCharArray()
                    Dim inWord As Boolean = False
                    For i As Integer = 0 To carr.Length - 1
                        If (Char.IsLetter(carr(i)) Or carr(i) = "'"c Or Char.IsDigit(carr(i))) Then

                            If Not inWord Then
                                carr(i) = Char.ToUpper(carr(i))
                            Else
                                carr(i) = Char.ToLower(carr(i))
                            End If
                            inWord = True
                        Else
                            inWord = False
                        End If
                    Next
                    Return New String(carr)
                Case VbStrConv.LinguisticCasing
                    Throw New ArgumentException("'StrConv.LinguisticCasing' requires 'StrConv.LowerCase' or 'StrConv.UpperCase'.")
            End Select

            Throw New NotSupportedException(String.Format("Conversion {0} is not supported yet", Conversion))

        End Function
#End If

        Public Shared Function StrDup(ByVal Number As Integer, ByVal Character As Char) As String
            If Character = Nothing Then
                Throw New ArgumentException("Length of argument 'Character' must be greater than zero.")
            End If

            If Number < 0 Then
                Throw New ArgumentException("Argument 'Number' must be greater or equal to zero.")
            End If

            Dim carr(Number - 1) As Char
            For i As Integer = 0 To carr.Length - 1
                carr(i) = Character
            Next

            Return New String(carr)
        End Function

        Public Shared Function StrDup(ByVal Number As Integer, ByVal Character As String) As String
            If Character Is Nothing OrElse Character.Length = 0 Then
                Throw New ArgumentException("Length of argument 'Character' must be greater than zero.")
            End If

            Return StrDup(Number, Character.Chars(0))
        End Function

        Public Shared Function StrDup(ByVal Number As Integer, ByVal Character As Object) As Object
            Dim c As Char = "c"c
            Try
                c = DirectCast(Character, Char)
                Return StrDup(Number, c)
            Catch ex As InvalidCastException
            End Try

            Dim s As String = "c"
            Try
                s = DirectCast(Character, String)
                Return StrDup(Number, s)
            Catch ex As InvalidCastException
            End Try

            Throw New ArgumentException("Argument 'Character' is not a valid value.")

        End Function

        Public Shared Function StrReverse(ByVal Expression As String) As String
            If Expression Is Nothing OrElse Expression.Length = 0 Then
                Return String.Empty
            End If

            Dim carr() As Char = Expression.ToCharArray()
            Array.Reverse(carr)
            Return New String(carr)
        End Function

        Public Shared Function Trim(ByVal str As String) As String
            If str Is Nothing Then
                Return String.Empty
            End If

            Dim carr() As Char = {" "c}
            Return str.Trim(carr)
        End Function

        Public Shared Function UCase(ByVal Value As Char) As Char
            Return Char.ToUpper(Value)
        End Function

        Public Shared Function UCase(ByVal Value As String) As String
            If Value Is Nothing Then
                Return String.Empty
            End If

            Return Value.ToUpper()
        End Function

        <CLSCompliant(False)> _
        Public Shared Function Len(ByVal Expression As SByte) As Integer
            Return GetSize(Expression)
        End Function

        <CLSCompliant(False)> _
        Public Shared Function Len(ByVal Expression As UInteger) As Integer
            Return GetSize(Expression)
        End Function

        <CLSCompliant(False)> _
        Public Shared Function Len(ByVal Expression As ULong) As Integer
            Return GetSize(Expression)
        End Function

        <CLSCompliant(False)> _
        Public Shared Function Len(ByVal Expression As UShort) As Integer
            Return GetSize(Expression)
        End Function
    End Class
End Namespace
