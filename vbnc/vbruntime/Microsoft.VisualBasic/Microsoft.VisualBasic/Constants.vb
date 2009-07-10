'
' Constants.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
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
Imports Microsoft.VisualBasic.CompilerServices

Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class Constants
        Public Const vbCrLf As String = ControlChars.Cr + ControlChars.Lf
        Public Const vbNewLine As String = vbCrLf
        Public Const vbCr As String = ControlChars.Cr
        Public Const vbLf As String = ControlChars.Lf
        Public Const vbBack As String = ControlChars.Back
        Public Const vbFormFeed As String = ControlChars.FormFeed
        Public Const vbTab As String = ControlChars.Tab
        Public Const vbVerticalTab As String = ControlChars.VerticalTab
        Public Const vbNullChar As String = ControlChars.NullChar
        Public Const vbNullString As String = Nothing

        Public Const vbGeneralDate As DateFormat = DateFormat.GeneralDate
        Public Const vbLongDate As DateFormat = DateFormat.LongDate
        Public Const vbLongTime As DateFormat = DateFormat.LongTime
        Public Const vbShortDate As DateFormat = DateFormat.ShortDate
        Public Const vbShortTime As DateFormat = DateFormat.ShortTime

        Public Const vbFriday As FirstDayOfWeek = FirstDayOfWeek.Friday
        Public Const vbMonday As FirstDayOfWeek = FirstDayOfWeek.Monday
        Public Const vbThursday As FirstDayOfWeek = FirstDayOfWeek.Thursday
        Public Const vbTuesday As FirstDayOfWeek = FirstDayOfWeek.Tuesday
        Public Const vbWednesday As FirstDayOfWeek = FirstDayOfWeek.Wednesday
        Public Const vbSaturday As FirstDayOfWeek = FirstDayOfWeek.Saturday
        Public Const vbSunday As FirstDayOfWeek = FirstDayOfWeek.Sunday
        Public Const vbUseSystemDayOfWeek As FirstDayOfWeek = FirstDayOfWeek.System

        Public Const vbMethod As CallType = CallType.Method
        Public Const vbGet As CallType = CallType.Get
        Public Const vbLet As CallType = CallType.Let
        Public Const vbSet As CallType = CallType.Set

        Public Const vbBinaryCompare As CompareMethod = CompareMethod.Binary
        Public Const vbTextCompare As CompareMethod = CompareMethod.Text

        Public Const vbUseSystem As FirstWeekOfYear = FirstWeekOfYear.System
        Public Const vbFirstJan1 As FirstWeekOfYear = FirstWeekOfYear.Jan1
        Public Const vbFirstFourDays As FirstWeekOfYear = FirstWeekOfYear.FirstFourDays
        Public Const vbFirstFullWeek As FirstWeekOfYear = FirstWeekOfYear.FirstFullWeek

#If Not MOONLIGHT Then
        Public Const vbUpperCase As VbStrConv = VbStrConv.Uppercase
        Public Const vbLowerCase As VbStrConv = VbStrConv.Lowercase
        Public Const vbProperCase As VbStrConv = VbStrConv.ProperCase
        Public Const vbWide As VbStrConv = VbStrConv.Wide
        Public Const vbNarrow As VbStrConv = VbStrConv.Narrow
        Public Const vbKatakana As VbStrConv = VbStrConv.Katakana
        Public Const vbHiragana As VbStrConv = VbStrConv.Hiragana
        Public Const vbSimplifiedChinese As VbStrConv = VbStrConv.SimplifiedChinese
        Public Const vbTraditionalChinese As VbStrConv = VbStrConv.TraditionalChinese
        Public Const vbLinguisticCasing As VbStrConv = VbStrConv.LinguisticCasing
#End If

        Public Const vbEmpty As VariantType = VariantType.Empty
        Public Const vbNull As VariantType = VariantType.Null
        Public Const vbInteger As VariantType = VariantType.Integer
        Public Const vbLong As VariantType = VariantType.Long
        Public Const vbSingle As VariantType = VariantType.Single
        Public Const vbDouble As VariantType = VariantType.Double
        Public Const vbCurrency As VariantType = VariantType.Currency
        Public Const vbDate As VariantType = VariantType.Date
        Public Const vbString As VariantType = VariantType.String
        Public Const vbObject As VariantType = VariantType.Object
        Public Const vbObjectError As Integer = -2147221504
        Public Const vbBoolean As VariantType = VariantType.Boolean
        Public Const vbVariant As VariantType = VariantType.Variant
        Public Const vbDecimal As VariantType = VariantType.Decimal
        Public Const vbByte As VariantType = VariantType.Byte
        Public Const vbUserDefinedType As VariantType = VariantType.UserDefinedType
        Public Const vbArray As VariantType = VariantType.Array

#If Not MOONLIGHT Then
        Public Const vbArchive As FileAttribute = FileAttribute.Archive
        Public Const vbDirectory As FileAttribute = FileAttribute.Directory
        Public Const vbHidden As FileAttribute = FileAttribute.Hidden
        Public Const vbNormal As FileAttribute = FileAttribute.Normal
        Public Const vbReadOnly As FileAttribute = FileAttribute.ReadOnly
        Public Const vbSystem As FileAttribute = FileAttribute.System
        Public Const vbVolume As FileAttribute = FileAttribute.Volume

        Public Const vbAbort As MsgBoxResult = MsgBoxResult.Abort
        Public Const vbIgnore As MsgBoxResult = MsgBoxResult.Ignore
        Public Const vbOK As MsgBoxResult = MsgBoxResult.OK
        Public Const vbCancel As MsgBoxResult = MsgBoxResult.Cancel
        Public Const vbRetry As MsgBoxResult = MsgBoxResult.Retry
        Public Const vbYes As MsgBoxResult = MsgBoxResult.Yes
        Public Const vbNo As MsgBoxResult = MsgBoxResult.No

        Public Const vbAbortRetryIgnore As MsgBoxStyle = MsgBoxStyle.AbortRetryIgnore
        Public Const vbOKOnly As MsgBoxStyle = MsgBoxStyle.OKOnly
        Public Const vbOKCancel As MsgBoxStyle = MsgBoxStyle.OKCancel
        Public Const vbYesNoCancel As MsgBoxStyle = MsgBoxStyle.YesNoCancel
        Public Const vbYesNo As MsgBoxStyle = MsgBoxStyle.YesNo
        Public Const vbRetryCancel As MsgBoxStyle = MsgBoxStyle.RetryCancel
        Public Const vbCritical As MsgBoxStyle = MsgBoxStyle.Critical
        Public Const vbQuestion As MsgBoxStyle = MsgBoxStyle.Question
        Public Const vbExclamation As MsgBoxStyle = MsgBoxStyle.Exclamation
        Public Const vbInformation As MsgBoxStyle = MsgBoxStyle.Information
        Public Const vbDefaultButton1 As MsgBoxStyle = MsgBoxStyle.DefaultButton1
        Public Const vbDefaultButton2 As MsgBoxStyle = MsgBoxStyle.DefaultButton2
        Public Const vbDefaultButton3 As MsgBoxStyle = MsgBoxStyle.DefaultButton3
        Public Const vbApplicationModal As MsgBoxStyle = MsgBoxStyle.ApplicationModal
        Public Const vbSystemModal As MsgBoxStyle = MsgBoxStyle.SystemModal
        Public Const vbMsgBoxHelp As MsgBoxStyle = MsgBoxStyle.MsgBoxHelp
        Public Const vbMsgBoxRight As MsgBoxStyle = MsgBoxStyle.MsgBoxRight
        Public Const vbMsgBoxRtlReading As MsgBoxStyle = MsgBoxStyle.MsgBoxRtlReading
        Public Const vbMsgBoxSetForeground As MsgBoxStyle = MsgBoxStyle.MsgBoxSetForeground
#End If
        Public Const vbFalse As TriState = TriState.False
        Public Const vbTrue As TriState = TriState.True
        Public Const vbUseDefault As TriState = TriState.UseDefault

#If Not MOONLIGHT Then
        Public Const vbHide As AppWinStyle = AppWinStyle.Hide
        Public Const vbNormalFocus As AppWinStyle = AppWinStyle.NormalFocus
        Public Const vbMinimizedFocus As AppWinStyle = AppWinStyle.MinimizedFocus
        Public Const vbMaximizedFocus As AppWinStyle = AppWinStyle.MaximizedFocus
        Public Const vbNormalNoFocus As AppWinStyle = AppWinStyle.NormalNoFocus
        Public Const vbMinimizedNoFocus As AppWinStyle = AppWinStyle.MinimizedNoFocus
#End If
    End Class
End Namespace
