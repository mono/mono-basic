'
' Conversion.vb
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
Imports System
Imports System.Reflection
Imports Microsoft.VisualBasic.CompilerServices

Namespace Microsoft.VisualBasic
    'MONOTODO: finish all the Constants
    <StandardModule()> _
    Public NotInheritable Class Conversion
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function ErrorToString() As String
            Return Information.Err.Description
        End Function
        Public Shared Function ErrorToString(ByVal ErrorNumber As Integer) As String
            Dim rm As New Resources.ResourceManager("strings", [Assembly].GetExecutingAssembly())

            Dim strDescription As String

#If TRACE Then
            Console.WriteLine("TRACE:Conversion.ErrorToString:input:" + ErrorNumber.ToString())
#End If

            'FIXMSDN: If (ErrorNumber < 0) Or (ErrorNumber >= 65535) Then
            If (ErrorNumber >= 65535) Then
                Throw New ArgumentException("Error number must be within the range 0 to 65535.")
            End If

            If (ErrorNumber = 0) Then
                Return ""
            End If

            strDescription = rm.GetString("ERR" + ErrorNumber.ToString())

            'Application-defined or object-defined error.
            If strDescription Is Nothing Then
                strDescription = rm.GetString("ERR95")
            End If

            Return strDescription
        End Function
        Public Shared Function Fix(ByVal Number As Decimal) As Decimal
            Return Number
        End Function
        Public Shared Function Fix(ByVal Number As Double) As Double
            Return Math.Sign(Number) * Conversion.Int(System.Math.Abs(Number))
        End Function
        Public Shared Function Fix(ByVal Number As Integer) As Integer
            Return Number
        End Function
        Public Shared Function Fix(ByVal Number As Long) As Long
            Return Number
        End Function
        Public Shared Function Fix(ByVal Number As Object) As Object
            'FIXME:ArgumentException 5 Number is not a numeric type. 
            If Number Is Nothing Then
                Throw New ArgumentNullException("Number", "Value can not be null.")
            End If

            If TypeOf Number Is Byte Then
                Return Conversion.Fix(Convert.ToByte(Number))
            ElseIf TypeOf Number Is Boolean Then
                Return Conversion.Fix(DoubleType.FromObject(Number))
            ElseIf TypeOf Number Is Long Then
                Return Conversion.Fix(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Decimal Then
                Return Conversion.Fix(Convert.ToDecimal(Number))
            ElseIf TypeOf Number Is Short Then
                Return Conversion.Fix(Convert.ToInt16(Number))
            ElseIf TypeOf Number Is Integer Then
                Return Conversion.Fix(Convert.ToInt32(Number))
            ElseIf TypeOf Number Is Double Then
                Return Conversion.Fix(Convert.ToDouble(Number))
            ElseIf TypeOf Number Is Single Then
                Return Conversion.Fix(Convert.ToSingle(Number))
            ElseIf TypeOf Number Is String Then
                Return Conversion.Fix(DoubleType.FromString(Number.ToString()))
            ElseIf TypeOf Number Is Char Then
                Return Conversion.Fix(DoubleType.FromString(Number.ToString()))
            Else 'Date, Object
                Throw New System.ArgumentException("Type of argument 'Number' is '" + Number.GetType.FullName + "', which is not numeric.")
            End If

        End Function
        Public Shared Function Fix(ByVal Number As Short) As Short
            Return Number
        End Function
        Public Shared Function Fix(ByVal Number As Single) As Single
            Return Math.Sign(Number) * Conversion.Int(System.Math.Abs(Number))
        End Function
        Public Shared Function Hex(ByVal Number As Byte) As String
            Return NumberToHex(Number)
        End Function
        Public Shared Function Hex(ByVal Number As Integer) As String
            Return NumberToHex(Number)
        End Function
        Public Shared Function Hex(ByVal Number As Long) As String
            Return NumberToHex(Number)
        End Function
        Public Shared Function Hex(ByVal Number As Object) As String

            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

#If TRACE Then
            Console.WriteLine("TRACE:Conversion.Hex:input:" + Number.ToString())
#End If

            If TypeOf Number Is Byte Then
                Return NumberToHex(Convert.ToByte(Number))
            ElseIf TypeOf Number Is Short Then
                Return NumberToHex(Convert.ToInt16(Number))
            ElseIf TypeOf Number Is Integer Then
                Return NumberToHex(Convert.ToInt32(Number))
            ElseIf TypeOf Number Is Long Then
                Return NumberToHex(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is String Then
                'support &H and &O
                Dim strNumber As String
                strNumber = Number.ToString
                If strNumber.StartsWith("&") Then
                    If strNumber.Substring(1, 1).ToUpper = "O" Then
                        Return NumberToHex(Convert.ToInt64(strNumber.Substring(2), 8))
                    ElseIf strNumber.Substring(1, 1).ToUpper = "H" Then
                        Return NumberToHex(Convert.ToInt64(strNumber.Substring(2), 16))
                    Else
                        'this probably will throw an exception
                        Return NumberToHex(Convert.ToInt64(Number))
                    End If
                Else
                    Return NumberToHex(Convert.ToInt64(Number))
                End If
            ElseIf TypeOf Number Is Double Then
                Return NumberToHex(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Decimal Then
                Return NumberToHex(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Single Then
                Return NumberToHex(Convert.ToInt32(Number))
            Else
                Throw New System.ArgumentException("Argument 'Number' cannot be converted to type '" + Number.GetType.FullName + "'.")
            End If
        End Function
        Public Shared Function Hex(ByVal Number As Short) As String
            Return NumberToHex(Number)
        End Function
        Private Shared Function NumberToHex(ByVal Number As Long) As String
#If TRACE Then
            Console.WriteLine("TRACE:Conversion.NumberToHex:input:" + Number.ToString())
#End If

            If Number >= 0 Then
                Return Convert.ToString(Number, 16).ToUpper
            Else
                'if the argument is negative - The unsigned value is the argument plus 2^32 
                Number = Number + Convert.ToInt64(Math.Pow(2, 32))
#If TRACE Then
                Console.WriteLine("TRACE:Conversion.NumberToHex:Negative Number:" + Number.ToString())
#End If
                Return Convert.ToString(Number, 16).ToUpper
            End If

        End Function
        Public Shared Function Int(ByVal Number As Decimal) As Decimal
            Return Number
        End Function
        Public Shared Function Int(ByVal Number As Double) As Double
            Return Math.Floor(Number)
        End Function
        Public Shared Function Int(ByVal Number As Integer) As Integer
            Return Number
        End Function
        Public Shared Function Int(ByVal Number As Long) As Long
            Return Number
        End Function
        Public Shared Function Int(ByVal Number As Object) As Object
            'FIXME:ArgumentException 5 Number is not a numeric type. 
            If Number Is Nothing Then
                Throw New ArgumentNullException("Number", "Value can not be null.")
            End If

            If TypeOf Number Is Byte Then
                Return Conversion.Int(Convert.ToByte(Number))
            ElseIf TypeOf Number Is Boolean Then
                Return Conversion.Int(Convert.ToDouble(Number))
            ElseIf TypeOf Number Is Long Then
                Return Conversion.Int(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Decimal Then
                Return Conversion.Int(Convert.ToDecimal(Number))
            ElseIf TypeOf Number Is Short Then
                Return Conversion.Int(Convert.ToInt16(Number))
            ElseIf TypeOf Number Is Integer Then
                Return Conversion.Int(Convert.ToInt32(Number))
            ElseIf TypeOf Number Is Double Then
                Return Conversion.Int(Convert.ToDouble(Number))
            ElseIf TypeOf Number Is Single Then
                Return Conversion.Int(Convert.ToSingle(Number))
            ElseIf TypeOf Number Is String Then
                Return Conversion.Int(Convert.ToString(Number))
            ElseIf TypeOf Number Is Char Then
                Return Conversion.Int(Convert.ToInt16(Number))
            Else 'Date, Object
                Throw New System.ArgumentException("Type of argument 'Number' is '" + Number.GetType.FullName + "', which is not numeric.")
            End If

        End Function
        Public Shared Function Int(ByVal Number As Short) As Short
            Return Number
        End Function
        Public Shared Function Int(ByVal Number As Single) As Single
            Return System.Convert.ToSingle(Math.Floor(Number))
        End Function
        Public Shared Function Oct(ByVal Number As Byte) As String
            Return NumberToOct(Number)
        End Function
        Public Shared Function Oct(ByVal Number As Integer) As String
            Return NumberToOct(Number)
        End Function
        Public Shared Function Oct(ByVal Number As Long) As String
            Return NumberToOct(Number)
        End Function
        Public Shared Function Oct(ByVal Number As Object) As String
            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

            If TypeOf Number Is Byte Then
                Return NumberToOct(Convert.ToByte(Number))
            ElseIf TypeOf Number Is Short Then
                Return NumberToOct(Convert.ToInt16(Number))
            ElseIf TypeOf Number Is Integer Then
                Return NumberToOct(Convert.ToInt32(Number))
            ElseIf TypeOf Number Is Long Then
                Return NumberToOct(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is String Then
                Return NumberToOct(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Double Then
                Return NumberToOct(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Decimal Then
                Return NumberToOct(Convert.ToInt64(Number))
            ElseIf TypeOf Number Is Single Then
                Return NumberToOct(Convert.ToInt32(Number))
            Else
                Throw New System.ArgumentException("Argument 'Number' cannot be converted to type '" + Number.GetType.FullName + "'.")
            End If
        End Function
        Public Shared Function Oct(ByVal Number As Short) As String
            Return NumberToOct(Number)
        End Function
        Private Shared Function NumberToOct(ByVal Number As Long) As String
#If TRACE Then
            Console.WriteLine("TRACE:Conversion.NumberToOct:input:" + Number.ToString())
#End If

            If Number >= 0 Then
                Return Convert.ToString(Number, 8).ToUpper
            Else
                'if the argument is negative - The unsigned value is the argument plus 2^32 
                Number = Number + Convert.ToInt64(Math.Pow(2, 32))
#If TRACE Then
                Console.WriteLine("TRACE:Conversion.NumberToOct:Negative Number:" + Number.ToString())
#End If
                Return Convert.ToString(Number, 8).ToUpper
            End If

        End Function
        Public Shared Function Str(ByVal Number As Object) As String
            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

            If TypeOf Number Is Byte Then
                If Convert.ToByte(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Short Then
                If Convert.ToInt16(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Integer Then
                If Convert.ToInt32(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Long Then
                If Convert.ToInt64(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Double Then
                If Convert.ToDouble(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Decimal Then
                If Convert.ToDecimal(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is Single Then
                If Convert.ToSingle(Number) > 0 Then
                    Return " " + Number.ToString
                Else
                    Return Number.ToString
                End If
            ElseIf TypeOf Number Is String Then
                Throw New System.NullReferenceException("Object reference not set to an instance of an object.")
            Else
                Throw New System.InvalidCastException("Argument 'Number' cannot be converted to a numeric value.")
            End If
        End Function
        Public Shared Function Val(ByVal Expression As Char) As Integer
            'only '0' - '9' are acceptable
            If Strings.Asc(Expression) >= Strings.Asc("0"c) And Strings.Asc(Expression) <= Strings.Asc("9"c) Then
                Return Strings.Asc(Expression) - Strings.Asc("0"c)
            Else
                'everything else is 0
                Return 0
            End If
        End Function
        Public Shared Function Val(ByVal Expression As Object) As Double
            If Expression Is Nothing Then
                Throw New System.ArgumentNullException("Expression", "Value cannot be null.")
            End If

            If TypeOf Expression Is Char Then
                Return Val(Convert.ToChar(Expression))
            ElseIf TypeOf Expression Is String Then
                Return Val(Convert.ToString(Expression))
            ElseIf TypeOf Expression Is Boolean Then
                Return Val(Convert.ToString((-1) * Convert.ToInt16(Expression)))
                'FIXME: add more types. Return Val(StringType.FromObject(Expression))
            Else
                Throw New System.ArgumentException("Argument 'Expression' cannot be converted to type '" + Expression.GetType.FullName + "'.")
            End If
        End Function
        Public Shared Function Val(ByVal InputStr As String) As Double

            If InputStr Is Nothing Then
                InputStr = ""
            End If

#If TRACE Then
            Console.WriteLine("TRACE:Conversion.Val:input:" + InputStr)
#End If

            '
            ' loop on InputStr chars
            Dim pos As Integer              ' char iterator
            Dim CurrentChar As Char         ' current char
            Dim CurrentCharAsc As Integer   ' current ascii value of char

            Dim NumericString As String = ""
            Dim PeriodCollected As Boolean ' did we already collected a Period ?

            ' Is it a positive/negative decimal or Hex or Oct
            ' start as negative. the first one who turns into True, wins.
            Dim IsNegative As Boolean = False
            Dim IsDecimal As Boolean = False
            Dim IsHex As Boolean = False
            Dim IsOct As Boolean = False

            '
            ' Loop on string and decide what is the base of the number.
            ' trim all left whitespace 
            '
            For pos = 0 To InputStr.Length - 1

                CurrentChar = Convert.ToChar(InputStr.Substring(pos, 1))

                If System.Char.IsWhiteSpace(CurrentChar) Then
                    'do nothing. continue
                ElseIf System.Char.IsDigit(CurrentChar) Then
                    'its decimal. exit loop
                    IsDecimal = True
                    pos = InputStr.Length
                ElseIf CurrentChar = "-" Then
                    IsNegative = True
                ElseIf CurrentChar = "&" Then
                    'if this is not the last char, 
                    'take the next char and see if radix is H or O
                    If pos < InputStr.Length - 1 Then
                        CurrentChar = Convert.ToChar(InputStr.Substring(pos + 1, 1))
                        If CurrentChar = "H" Or CurrentChar = "h" Then
                            'its Hex. exit loop
                            IsHex = True
                            pos = InputStr.Length
                        ElseIf CurrentChar = "O" Or CurrentChar = "o" Then
                            'its Oct. exit loop
                            IsOct = True
                            pos = InputStr.Length
                        Else
                            'its bad. return 0.
                            Return 0
                        End If
                    End If

                Else
                    'the string didn't start with a digit or &H or &O
                    'its bad. return 0.
                    Return 0
                End If
            Next


#If TRACE Then
            Console.WriteLine("TRACE:Conversion.Val:IsDecimal:" + IsDecimal.ToString() + "IsHex:" + IsHex.ToString() + "IsOct:" + IsOct.ToString())
#End If


            For pos = 0 To InputStr.Length - 1

                CurrentChar = Convert.ToChar(InputStr.Substring(pos, 1))
                CurrentCharAsc = Strings.Asc(CurrentChar)

                'collect numbers and one period, ignore whitespaces, stop on other
                If IsDecimal Then

                    If System.Char.IsWhiteSpace(CurrentChar) Then
                        'ignore this char
                    ElseIf CurrentChar = "-" Then
                        IsNegative = True
                    ElseIf System.Char.IsDigit(CurrentChar) Then
                        'collect this char
                        NumericString = NumericString + CurrentChar
                    ElseIf CurrentChar = "." Then
                        'The Val function recognizes only the period (.) as a valid decimal separator
                        If Not PeriodCollected Then
                            NumericString = NumericString + CurrentChar
                            PeriodCollected = True
                        Else
                            'period already collected. exit the loop.
                            pos = InputStr.Length
                        End If
                    Else
                        'exit the loop
                        pos = InputStr.Length
                    End If

                ElseIf IsHex Then

                    If System.Char.IsWhiteSpace(CurrentChar) Or CurrentChar = "&" Or CurrentChar = "H" Or CurrentChar = "h" Then
                        'ignore this char
                    ElseIf NumericString.Length = 16 Then
                        'max hex chars is 16. exit the loop.
                        pos = InputStr.Length
                    ElseIf System.Char.IsDigit(CurrentChar) Then
                        'collect this char
                        NumericString = NumericString + CurrentChar
                    ElseIf ((CurrentCharAsc >= Strings.Asc("A")) And (CurrentCharAsc <= Strings.Asc("F"))) Or _
                        ((CurrentCharAsc >= Strings.Asc("a")) And (CurrentCharAsc <= Strings.Asc("f"))) Then
                        'collect this char
                        NumericString = NumericString + CurrentChar
                    Else
                        'exit the loop.
                        pos = InputStr.Length
                    End If

                ElseIf IsOct Then

                    If System.Char.IsWhiteSpace(CurrentChar) Or CurrentChar = "&" Or CurrentChar = "O" Or CurrentChar = "o" Then
                        'ignore this char
                    ElseIf ((CurrentCharAsc >= Strings.Asc("0")) And (CurrentCharAsc <= Strings.Asc("7"))) Then
                        'collect this char
                        NumericString = NumericString + CurrentChar
                    Else
                        'exit the loop
                        pos = InputStr.Length
                    End If

                Else
                    Throw New NotSupportedException("FIXME")
                End If

            Next pos

#If TRACE Then
            Console.WriteLine("TRACE:Conversion.Val:collected string:" + NumericString)
#End If

            'convert the NumericString back to long
            'FIXME:the Val return double but it seems to cast to long. add a test and check that.
            Dim retVal As Double = 0
            If NumericString.Length > 0 Then
                If IsDecimal Then
                    retVal = DoubleType.FromString(NumericString)
                    If IsNegative Then retVal = (-1) * retVal
                ElseIf IsHex Then
                    NumericString = NumericString.ToUpper
                    Dim NumericStringLength As Integer = NumericString.Length
                    If (NumericStringLength = 4 Or NumericStringLength = 8 Or NumericStringLength >= 16) And NumericString.StartsWith("F") Then
                        'its negative
                        If NumericStringLength = 4 Then retVal = Convert.ToDouble(Convert.ToInt64(NumericString, 16)) - Math.Pow(2, 16)
                        If NumericStringLength = 8 Then retVal = Convert.ToDouble(Convert.ToInt64(NumericString, 16)) - Math.Pow(2, 32)
                        If NumericStringLength >= 16 Then retVal = Convert.ToDouble(Convert.ToInt64(NumericString, 16))
                    Else
                        retVal = Convert.ToDouble(Convert.ToInt64(NumericString, 16))
                    End If

                ElseIf IsOct Then
                    retVal = Convert.ToInt64(NumericString, 8)
                Else
                    'FIXME: what else ?
                End If
            End If
            Return retVal

            'exceptions
            'FIXME:OverflowException - InputStr is too large.
            'FIXME:InvalidCastException - Number is badly formed. 
            'FIXME:ArgumentException - Object type expression not convertible to String.

        End Function
#If NET_2_0 Then
        Public Shared Function Hex(ByVal Number As SByte) As String
            Return NumberToHex(CByte(Number))
        End Function
        Public Shared Function Hex(ByVal Number As UShort) As String
            Return NumberToHex(CShort(Number))
        End Function
        Public Shared Function Hex(ByVal Number As UInteger) As String
            Return NumberToHex(CInt(Number))
        End Function
        Public Shared Function Hex(ByVal Number As ULong) As String
            Return NumberToHex(CLng(Number))
        End Function
        Public Shared Function Oct(ByVal Number As SByte) As String
            Return NumberToOct(CByte(Number))
        End Function
        Public Shared Function Oct(ByVal Number As UShort) As String
            Return NumberToOct(CShort(Number))
        End Function
        Public Shared Function Oct(ByVal Number As UInteger) As String
            Return NumberToOct(CInt(Number))
        End Function
        Public Shared Function Oct(ByVal Number As ULong) As String
            Return NumberToOct(CLng(Number))
        End Function
#End If
    End Class
End Namespace
