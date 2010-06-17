'
' Conversion.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Guy Cohen (guyc@mainsoft.com)

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
Imports System.Globalization


Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class Conversion
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
            Return Math.Sign(Number) * Conversion.Int(System.Math.Abs(Number))
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
                If (Convert.ToBoolean(Number)) Then
                    Return 1
                End If
                Return 0
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
            Return Convert.ToString(Number, 16).ToUpper
        End Function

        Public Shared Function Hex(ByVal Number As Integer) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function

        Public Shared Function Hex(ByVal Number As Long) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function

        Public Shared Function Hex(ByVal Number As Short) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function

        Public Shared Function Hex(ByVal Number As Object) As String

            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

            If (TypeOf Number Is IConvertible) Then
                Dim tc As TypeCode = CType(Number, IConvertible).GetTypeCode()

                Select Case tc
                    Case TypeCode.Byte
                        Return Hex(Convert.ToByte(Number))
                    Case TypeCode.Decimal
                        Return Hex(SizeDown(Convert.ToInt64(Number)))
                    Case TypeCode.Double
                        Return Hex(SizeDown(Convert.ToInt64(Number)))
                    Case TypeCode.Int16
                        Return Hex(Convert.ToInt16(Number))
                    Case TypeCode.Int32
                        Return Hex(Convert.ToInt32(Number))
                    Case TypeCode.Int64
                        Return Hex(Convert.ToInt64(Number))
                    Case TypeCode.Single
                        Return Hex(SizeDown(Convert.ToInt32(Number)))
                    Case TypeCode.String
                        Dim strNumber As String
                        strNumber = Number.ToString
                        If strNumber.StartsWith("&") Then
                            If Char.ToUpper(strNumber.Chars(1)) = "O"c Then
                                Return Hex(SizeDown(Convert.ToInt64(strNumber.Substring(2), 8)))
                            ElseIf Char.ToUpper(strNumber.Chars(1)) = "H"c Then
                                Return Hex(SizeDown(Convert.ToInt64(strNumber.Substring(2), 16)))
                            Else
                                Return Hex(SizeDown(Convert.ToInt64(Number)))
                            End If
                        Else
                            Return Hex(SizeDown(Convert.ToInt64(Number)))
                        End If
                    Case TypeCode.SByte
                        Return Hex(Convert.ToSByte(Number))
                    Case TypeCode.UInt16
                        Return Hex(Convert.ToUInt16(Number))
                    Case TypeCode.UInt32
                        Return Hex(Convert.ToUInt32(Number))
                    Case TypeCode.UInt64
                        Return Hex(Convert.ToUInt64(Number))
                    Case Else
                        Throw New System.ArgumentException("Argument 'Number' cannot be converted to type '" + Number.GetType.FullName + "'.")

                End Select
            Else
                Throw New System.ArgumentException("Argument 'Number' is not a number.")
            End If
        End Function

        Private Shared Function SizeDown(ByVal num As Long) As Object
            'If (num <= Byte.MaxValue And num >= 0) Then
            '    Return CType(num, Byte)
            'End If

            'If (num <= SByte.MaxValue And num >= SByte.MinValue) Then
            '    Return CType(num, SByte)
            'End If

            'If (num <= Int16.MaxValue And num >= Int16.MinValue) Then
            '    Return CType(num, Int16)
            'End If

            'If (num <= UInt16.MaxValue And num >= 0) Then
            '    Return CType(num, UInt16)
            'End If

            If (num <= Int32.MaxValue And num >= Int32.MinValue) Then
                Return CType(num, Int32)
            End If
            If (num <= UInt32.MaxValue And num >= 0) Then
                Return CType(num, UInt32)
            End If
            Return num
        End Function

        Public Shared Function Int(ByVal Number As Decimal) As Decimal
            Return Decimal.Floor(Number)
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
                Return Conversion.Int(Convert.ToDouble(Number))
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
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        Public Shared Function Oct(ByVal Number As Integer) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        Public Shared Function Oct(ByVal Number As Long) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        Public Shared Function Oct(ByVal Number As Object) As String
            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

            If (TypeOf Number Is IConvertible) Then
                Dim tc As TypeCode = CType(Number, IConvertible).GetTypeCode()

                Select Case tc
                    Case TypeCode.Byte
                        Return Oct(Convert.ToByte(Number))
                    Case TypeCode.Decimal
                        Return Oct(SizeDown(Convert.ToInt64(Number)))
                    Case TypeCode.Double
                        Return Oct(SizeDown(Convert.ToInt64(Number)))
                    Case TypeCode.Int16
                        Return Oct(Convert.ToInt16(Number))
                    Case TypeCode.Int32
                        Return Oct(Convert.ToInt32(Number))
                    Case TypeCode.Int64
                        Return Oct(Convert.ToInt64(Number))
                    Case TypeCode.Single
                        Return Oct(SizeDown(Convert.ToInt32(Number)))
                    Case TypeCode.String
                        Dim strNumber As String
                        strNumber = Number.ToString
                        If strNumber.StartsWith("&") Then
                            If Char.ToUpper(strNumber.Chars(1)) = "O"c Then
                                Return Oct(SizeDown(Convert.ToInt64(strNumber.Substring(2), 8)))
                            ElseIf Char.ToUpper(strNumber.Chars(1)) = "H"c Then
                                Return Oct(SizeDown(Convert.ToInt64(strNumber.Substring(2), 16)))
                            Else
                                Return Oct(SizeDown(Convert.ToInt64(Number)))
                            End If
                        Else
                            Return Oct(SizeDown(Convert.ToInt64(Number)))
                        End If
                    Case TypeCode.SByte
                        Return Oct(Convert.ToSByte(Number))
                    Case TypeCode.UInt16
                        Return Oct(Convert.ToUInt16(Number))
                    Case TypeCode.UInt32
                        Return Oct(Convert.ToUInt32(Number))
                    Case TypeCode.UInt64
                        Return Oct(Convert.ToUInt64(Number))
                    Case Else
                        Throw New System.ArgumentException("Argument 'Number' cannot be converted to type '" + Number.GetType.FullName + "'.")

                End Select
            Else
                Throw New System.ArgumentException("Argument 'Number' is not a number.")
            End If
        End Function

        Public Shared Function Oct(ByVal Number As Short) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function

        Public Shared Function Str(ByVal Number As Object) As String
            If Number Is Nothing Then
                Throw New System.ArgumentNullException("Number", "Value cannot be null.")
            End If

            If TypeOf Number Is Byte Then
                If Convert.ToByte(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Short Then
                If Convert.ToInt16(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Integer Then
                If Convert.ToInt32(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Long Then
                If Convert.ToInt64(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Double Then
                If Convert.ToDouble(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Decimal Then
                If Convert.ToDecimal(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
                End If
            ElseIf TypeOf Number Is Single Then
                If Convert.ToSingle(Number) > 0 Then
                    Return " " + Convert.ToString(Number, CultureInfo.InvariantCulture)
                Else
                    Return Convert.ToString(Number, CultureInfo.InvariantCulture)
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
                Return Val("")
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
            Dim IsE As Boolean

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
                ElseIf CurrentChar = "-"c Then
                    IsNegative = True
                ElseIf CurrentChar = "&"c Then
                    'if this is not the last char, 
                    'take the next char and see if radix is H or O
                    If pos < InputStr.Length - 1 Then
                        CurrentChar = Convert.ToChar(InputStr.Substring(pos + 1, 1))
                        If CurrentChar = "H"c Or CurrentChar = "h"c Then
                            'its Hex. exit loop
                            IsHex = True
                            pos = InputStr.Length
                        ElseIf CurrentChar = "O"c Or CurrentChar = "o"c Then
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
                    ElseIf CurrentChar = "-"c Then
                        IsNegative = True
                    ElseIf System.Char.IsDigit(CurrentChar) Then
                        'collect this char
                        NumericString = NumericString & CurrentChar.ToString()
                    ElseIf CurrentChar = "."c Then
                        'The Val function recognizes only the period (.) as a valid decimal separator
                        If Not PeriodCollected Then
                            NumericString = NumericString & CurrentChar.ToString()
                            PeriodCollected = True
                        Else
                            'period already collected. exit the loop.
                            pos = InputStr.Length
                        End If
                    ElseIf IsE = False AndAlso (CurrentChar = "E"c OrElse CurrentChar = "e"c) Then
                        IsE = True
                        NumericString &= CurrentChar.ToString()
                    ElseIf IsE AndAlso CurrentChar = "+"c Then
                        'ignore this
                    Else
                        'exit the loop
                        pos = InputStr.Length
                    End If

                ElseIf IsHex Then

                    If System.Char.IsWhiteSpace(CurrentChar) Or CurrentChar = "&"c Or CurrentChar = "H"c Or CurrentChar = "h"c Then
                        'ignore this char
                    ElseIf NumericString.Length = 16 Then
                        'max hex chars is 16. exit the loop.
                        pos = InputStr.Length
                    ElseIf System.Char.IsDigit(CurrentChar) Then
                        'collect this char
                        NumericString = NumericString & CurrentChar.ToString()
                    ElseIf ((CurrentCharAsc >= Strings.Asc("A")) And (CurrentCharAsc <= Strings.Asc("F"))) Or _
                        ((CurrentCharAsc >= Strings.Asc("a")) And (CurrentCharAsc <= Strings.Asc("f"))) Then
                        'collect this char
                        NumericString = NumericString & CurrentChar.ToString()
                    Else
                        'exit the loop.
                        pos = InputStr.Length
                    End If

                ElseIf IsOct Then

                    If System.Char.IsWhiteSpace(CurrentChar) Or CurrentChar = "&"c Or CurrentChar = "O"c Or CurrentChar = "o"c Then
                        'ignore this char
                    ElseIf ((CurrentCharAsc >= Strings.Asc("0")) And (CurrentCharAsc <= Strings.Asc("7"))) Then
                        'collect this char
                        NumericString = NumericString & CurrentChar.ToString()
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
                    retVal = Convert.ToDouble(NumericString, New CultureInfo("en-US"))
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

        <CLSCompliant(False)> _
        Public Shared Function Hex(ByVal Number As SByte) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Hex(ByVal Number As UShort) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Hex(ByVal Number As UInteger) As String
            Return Convert.ToString(Number, 16).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Hex(ByVal Number As ULong) As String
            Return Convert.ToString(CLng(Number), 16).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Oct(ByVal Number As SByte) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Oct(ByVal Number As UShort) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Oct(ByVal Number As UInteger) As String
            Return Convert.ToString(Number, 8).ToUpper
        End Function
        <CLSCompliant(False)> _
        Public Shared Function Oct(ByVal Number As ULong) As String
            Return Convert.ToString(CLng(Number), 8).ToUpper
        End Function
    End Class
End Namespace
