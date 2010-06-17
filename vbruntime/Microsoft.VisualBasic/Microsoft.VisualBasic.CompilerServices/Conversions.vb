'
' Conversions.vb
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

Imports System
Imports System.Globalization

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class Conversions
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function ChangeType(ByVal Expression As Object, ByVal TargetType As Type) As Object
            If (TargetType.IsInstanceOfType(Expression)) Then
                Return Expression
            End If
            Return System.Convert.ChangeType(Expression, TargetType, CultureInfo.CurrentCulture)
        End Function

        Public Shared Function FromCharAndCount(ByVal Value As Char, ByVal Count As Integer) As String
            If (Count < 0) Then
                Throw New ArgumentException("Count")
            End If
            Dim cArr() As Char = New Char(Count) {}
            Dim i As Integer = 0
            While (i < Count)
                cArr(i) = Value
            End While
            Return FromCharArray(cArr)
        End Function

        Public Shared Function FromCharArray(ByVal Value As Char()) As String
            Return New String(Value)
        End Function
        Public Shared Function FromCharArraySubset(ByVal Value As Char(), ByVal StartIndex As Integer, ByVal Length As Integer) As String
            Return New String(Value, StartIndex, Length)
        End Function
        Public Shared Function ToBoolean(ByVal Value As Object) As Boolean
            Return BooleanType.FromObject(Value)
        End Function
        Public Shared Function ToBoolean(ByVal Value As String) As Boolean
            Return BooleanType.FromString(Value)
        End Function
        Public Shared Function ToByte(ByVal Value As Object) As Byte
            Return ByteType.FromObject(Value)
        End Function
        Public Shared Function ToByte(ByVal Value As String) As Byte
            Return ByteType.FromString(Value)
        End Function
        Public Shared Function ToChar(ByVal Value As Object) As Char
            Return CharType.FromObject(Value)
        End Function
        Public Shared Function ToChar(ByVal Value As String) As Char
            Return CharType.FromString(Value)
        End Function

        Public Shared Function ToCharArrayRankOne(ByVal Value As Object) As Char()
            If (Value Is Nothing) Then
                Return ToCharArrayRankOne("")
            End If

            If TypeOf Value Is Char() Then
                Return DirectCast(Value, Char())
            End If

            If TypeOf Value Is String Then
                Return ToCharArrayRankOne(DirectCast(Value, String))
            End If

            Throw New InvalidCastException("Conversion from type '" + Value.GetType().Name + "' to type 'Char()' is not valid.")
        End Function

        Public Shared Function ToCharArrayRankOne(ByVal Value As String) As Char()
            If (Value Is Nothing) Then
                Value = ""
            End If
            Return Value.ToCharArray()
        End Function
        Public Shared Function ToDate(ByVal Value As Object) As DateTime
            Return DateType.FromObject(Value)
        End Function
        Public Shared Function ToDate(ByVal Value As String) As DateTime
            Return DateType.FromString(Value)
        End Function
        Public Shared Function ToDecimal(ByVal Value As Boolean) As Decimal
            Return DecimalType.FromBoolean(Value)
        End Function
        Public Shared Function ToDecimal(ByVal Value As Object) As Decimal
            Return DecimalType.FromObject(Value)
        End Function
        Public Shared Function ToDecimal(ByVal Value As String) As Decimal
            Return DecimalType.FromString(Value)
        End Function
        Public Shared Function ToDouble(ByVal Value As Object) As Double
            Return DoubleType.FromObject(Value)
        End Function
        Public Shared Function ToDouble(ByVal Value As String) As Double
            Return DoubleType.FromString(Value)
        End Function
        Public Shared Function ToGenericParameter(Of T)(ByVal Value As Object) As T
            Return DirectCast(Value, T)
        End Function
        Public Shared Function ToInteger(ByVal Value As Object) As Integer
            Return IntegerType.FromObject(Value)
        End Function
        Public Shared Function ToInteger(ByVal Value As String) As Integer
            Return IntegerType.FromString(Value)
        End Function
        Public Shared Function ToLong(ByVal Value As Object) As Long
            Return LongType.FromObject(Value)
        End Function
        Public Shared Function ToLong(ByVal Value As String) As Long
            Return LongType.FromString(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToSByte(ByVal Value As Object) As SByte
            Return System.Convert.ToSByte(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToSByte(ByVal Value As String) As SByte
            Return System.Convert.ToSByte(Value)
        End Function
        Public Shared Function ToShort(ByVal Value As Object) As Short
            Return ShortType.FromObject(Value)
        End Function
        Public Shared Function ToShort(ByVal Value As String) As Short
            Return ShortType.FromString(Value)
        End Function
        Public Shared Function ToSingle(ByVal Value As Object) As Single
            Return SingleType.FromObject(Value)
        End Function
        Public Shared Function ToSingle(ByVal Value As String) As Single
            Return SingleType.FromString(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Boolean) As String
            Return StringType.FromBoolean(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Byte) As String
            Return StringType.FromByte(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Char) As String
            Return StringType.FromChar(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As DateTime) As String
            Return StringType.FromDate(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Decimal) As String
            Return StringType.FromDecimal(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Double) As String
            Return StringType.FromDouble(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Short) As String
            Return StringType.FromShort(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Integer) As String
            Return StringType.FromInteger(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Long) As String
            Return StringType.FromLong(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Object) As String
            Return StringType.FromObject(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Single) As String
            Return StringType.FromSingle(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Shadows Function ToString(ByVal Value As UInteger) As String
            Return System.Convert.ToString(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Shadows Function ToString(ByVal Value As ULong) As String
            Return System.Convert.ToString(Value)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Decimal, ByVal NumberFormat As NumberFormatInfo) As String
            Return System.Convert.ToString(Value, NumberFormat)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Double, ByVal NumberFormat As NumberFormatInfo) As String
            Return System.Convert.ToString(Value, NumberFormat)
        End Function
        Public Shared Shadows Function ToString(ByVal Value As Single, ByVal NumberFormat As NumberFormatInfo) As String
            Return System.Convert.ToString(Value, NumberFormat)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToUInteger(ByVal Value As Object) As UInteger
            Return System.Convert.ToUInt32(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToUInteger(ByVal Value As String) As UInteger
            Return System.Convert.ToUInt32(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToULong(ByVal Value As Object) As ULong
            Return System.Convert.ToUInt64(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToULong(ByVal Value As String) As ULong
            Return System.Convert.ToUInt64(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToUShort(ByVal Value As Object) As UShort
            Return System.Convert.ToUInt16(Value)
        End Function
        <CLSCompliant(False)> _
        Public Shared Function ToUShort(ByVal Value As String) As UShort
            Return System.Convert.ToUInt16(Value)
        End Function
    End Class
End Namespace