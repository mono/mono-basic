'
' Operators.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Rolf Bjarne Kvinge (RKvinge@novell.com)

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

#If NET_2_0 Then
Imports System
Imports System.Reflection
Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class Operators
        Class CompareResult
            Public Value As Integer
            Public Type As CompareResultType

            Sub New(ByVal Type As CompareResultType, ByVal Value As Integer)
                Me.Value = Value
                Me.Type = Type
            End Sub
        End Class

        Enum CompareResultType
            Success
            UserDefined
            Fail
        End Enum

        Private Shared Function CompareBoolean(ByVal Left As Boolean, ByVal Right As Boolean) As Integer
            If Left = Right Then Return 0
            If Left < Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareByte(ByVal Left As Byte, ByVal Right As Byte) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareChar(ByVal Left As Char, ByVal Right As Char) As Integer
            If Left = Right Then Return 0
            If Left < Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareDate(ByVal Left As Date, ByVal Right As Date) As Integer
            Return DateTime.Compare(Left, Right)
        End Function

        Private Shared Function CompareDecimal(ByVal Left As Decimal, ByVal Right As Decimal) As Integer
            Return Decimal.Compare(Left, Right)
        End Function

        Private Shared Function CompareDouble(ByVal Left As Double, ByVal Right As Double) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareInt16(ByVal Left As Int16, ByVal Right As Int16) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareInt32(ByVal Left As Int32, ByVal Right As Int32) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareInt64(ByVal Left As Int64, ByVal Right As Int64) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareSByte(ByVal Left As SByte, ByVal Right As SByte) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareSingle(ByVal Left As Single, ByVal Right As Single) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareUInt16(ByVal Left As UInt16, ByVal Right As UInt16) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareUInt32(ByVal Left As UInt32, ByVal Right As UInt32) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function CompareUInt64(ByVal Left As UInt64, ByVal Right As UInt64) As Integer
            If Left = Right Then
                Return 0
            ElseIf Left > Right Then
                Return 1
            Else
                Return -1
            End If
        End Function

        Private Shared Function GetTypeCode(ByVal obj As Object) As TypeCode
            If (TypeOf obj Is IConvertible) Then
                Return CType(obj, IConvertible).GetTypeCode()
            End If
            Return Type.GetTypeCode(obj.GetType())
        End Function

        Shared DEST_TYPECODE_ADD As TypeCode(,) = { _
{TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.String, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.String, TypeCode.Int32, TypeCode.String}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.String}, _
{TypeCode.Int16, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Int16, TypeCode.Double}, _
{TypeCode.String, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.String, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.String, TypeCode.String}, _
{TypeCode.SByte, TypeCode.Empty, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.SByte, TypeCode.Double}, _
{TypeCode.Byte, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Byte, TypeCode.Double}, _
{TypeCode.Int16, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Int16, TypeCode.Double}, _
{TypeCode.UInt16, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.UInt16, TypeCode.Double}, _
{TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Int32, TypeCode.Double}, _
{TypeCode.UInt32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.UInt32, TypeCode.Double}, _
{TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Int64, TypeCode.Double}, _
{TypeCode.UInt64, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.UInt64, TypeCode.Double}, _
{TypeCode.Single, TypeCode.Empty, TypeCode.Empty, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Double, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Double}, _
{TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double}, _
{TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Double}, _
{TypeCode.String, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.String, TypeCode.String, TypeCode.String}, _
{TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.String, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.String, TypeCode.Int32, TypeCode.String}, _
{TypeCode.String, TypeCode.Empty, TypeCode.String, TypeCode.Double, TypeCode.String, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.String, TypeCode.String, TypeCode.String} _
}

        Shared DEST_TYPECODE_BITWISE_OP As TypeCode(,) = { _
 {TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
 {TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Boolean, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Boolean, TypeCode.Boolean}, _
 {TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.SByte, TypeCode.Empty, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.SByte, TypeCode.Int64}, _
 {TypeCode.Byte, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Byte, TypeCode.Int64}, _
 {TypeCode.Int16, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int16, TypeCode.Int64}, _
 {TypeCode.UInt16, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt16, TypeCode.Int64}, _
 {TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
 {TypeCode.UInt32, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt32, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.UInt64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Int32, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64} _
 }
       

        'Returns the expected return TypeCode of operation between these two objects or TypeCode.Empty if operation is not possible.
        'Notice: The expected TypeCode may not be the actual TypeCode of the return type. The actual type return is "black box"ed 
        'by the operation implementation. For example in the case of Integer and Short the expected return TypeCode is of Integer 
        'but the actual return type may be Long (in the case of overflow)
        Private Shared Function DestTypeCodeOpAdd(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_ADD(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        'Notice: unlike DestTypeCodeOpAdd this is the actual return type
        Private Shared Function DestTypeCodeBitwiseOp(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_BITWISE_OP(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function AddBooleans(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object
            Dim ret As Short = 0
            If (o1) Then
                ret = ret - 1S
            End If
            If (o2) Then
                ret = ret - 1S
            End If
            Return ret
        End Function

        Private Shared Function AddBytes(ByVal o1 As Byte, ByVal o2 As Byte) As Object
            Dim s As Short = CType(o1, Short) + o2
            If (s > Byte.MaxValue) Then
                Return s
            End If
            Return CType(s, Byte)
        End Function

        Private Shared Function AddChars(ByVal o1 As Char, ByVal o2 As Char) As Object
            Return AddStrings(o1.ToString(), o2.ToString())
        End Function

        Private Shared Function AddDateTimes(ByVal o1 As DateTime, ByVal o2 As DateTime) As Object
            Return AddStrings(o1.ToString(), o2.ToString())
        End Function

        Private Shared Function AddDecimals(ByVal o1 As Decimal, ByVal o2 As Decimal) As Object
            Return o1 + o2
        End Function

        Private Shared Function AddDoubles(ByVal o1 As Double, ByVal o2 As Double) As Object
            Return o1 + o2
        End Function

        Private Shared Function AddInt16s(ByVal o1 As Short, ByVal o2 As Short) As Object
            Dim int As Integer = CType(o1, Integer) + o2
            If (int > Short.MaxValue) Or (int < Short.MinValue) Then
                Return int
            End If
            Return CType(int, Short)
        End Function

        Private Shared Function AddInt32s(ByVal o1 As Integer, ByVal o2 As Integer) As Object
            Dim l As Long = CType(o1, Long) + o2
            If (l > Integer.MaxValue) Or (l < Integer.MinValue) Then
                Return l
            End If
            Return CType(l, Integer)
        End Function

        Private Shared Function AddInt64s(ByVal o1 As Long, ByVal o2 As Long) As Object
            Return o1 + o2
        End Function

        Private Shared Function AddObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(o1, o2, "op_Addition", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function AddSBytes(ByVal o1 As SByte, ByVal o2 As SByte) As Object
            Dim s As Short = CType(o1, Short) + o2
            If (s > SByte.MaxValue) Or (s < SByte.MinValue) Then
                Return s
            End If
            Return CType(s, SByte)
        End Function

        Private Shared Function AddSingles(ByVal o1 As Single, ByVal o2 As Single) As Object
            Return o1 + o2
        End Function

        Private Shared Function AddStrings(ByVal o1 As String, ByVal o2 As String) As Object
            Return o1 + o2
        End Function

        Private Shared Function AddUInt16s(ByVal o1 As UShort, ByVal o2 As UShort) As Object
            Dim int As Integer = CType(o1, Integer) + o2
            If (int > UShort.MaxValue) Then
                Return int
            End If
            Return CType(int, UShort)
        End Function

        Private Shared Function AddUInt32s(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object
            Dim l As Long = CType(o1, Long) + o2
            If (l > UInteger.MaxValue) Then
                Return l
            End If
            Return CType(l, UInteger)
        End Function

        Private Shared Function AddUInt64s(ByVal o1 As ULong, ByVal o2 As ULong) As Object
            Return o1 + o2
        End Function

        Public Shared Function AddObject(ByVal o1 As Object, ByVal o2 As Object) As Object
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpAdd(o1, o2)
            Try
                Select Case destTc
                    'Case TypeCode.Empty -> break
                    Case TypeCode.Boolean
                        Return AddBooleans(Convert.ToBoolean(o1), Convert.ToBoolean(o2))
                    Case TypeCode.Byte
                        Return AddBytes(Convert.ToByte(o1), Convert.ToByte(o2))
                    Case TypeCode.Char
                        Return AddChars(Convert.ToChar(o1), Convert.ToChar(o2))
                    Case TypeCode.DateTime
                        Return AddDateTimes(Convert.ToDateTime(o1), Convert.ToDateTime(o2))
                    Case TypeCode.Decimal
                        Return AddDecimals(Convert.ToDecimal(o1), Convert.ToDecimal(o2))
                    Case TypeCode.Double
                        Return AddDoubles(Convert.ToDouble(o1), Convert.ToDouble(o2))
                    Case TypeCode.Int16
                        Return AddInt16s(Convert.ToInt16(o1), Convert.ToInt16(o2))
                    Case TypeCode.Int32
                        Return AddInt32s(Convert.ToInt32(o1), Convert.ToInt32(o2))
                    Case TypeCode.Int64
                        Return AddInt64s(Convert.ToInt64(o1), Convert.ToInt64(o2))
                    Case TypeCode.SByte
                        Return AddSBytes(Convert.ToSByte(o1), Convert.ToSByte(o2))
                    Case TypeCode.Single
                        Return AddSingles(Convert.ToSingle(o1), Convert.ToSingle(o2))
                    Case TypeCode.String
                        Return AddStrings(Convert.ToString(o1), Convert.ToString(o2))
                    Case TypeCode.UInt16
                        Return AddUInt16s(Convert.ToUInt16(o1), Convert.ToUInt16(o2))
                    Case TypeCode.UInt32
                        Return AddUInt32s(Convert.ToUInt32(o1), Convert.ToUInt32(o2))
                    Case TypeCode.UInt64
                        Return AddUInt64s(Convert.ToUInt64(o1), Convert.ToUInt64(o2))

                End Select
                Return AddObjects(o1, o2)
            Catch ex As Exception
                If (TypeOf ex Is NotImplementedException) Then
                    Throw ex
                End If
            End Try
            Throw New InvalidCastException("Operator '+' is not defined for type '" + GetTypeCode(o1).ToString() + "' and type '" + GetTypeCode(o2).ToString() + "'.")
        End Function



        'creates a real type of a Nothing object 
        Private Shared Function CreateNullObjectType(ByVal otype As Object) As Object

            If TypeOf otype Is Byte Then
                Return Convert.ToByte(0)
            ElseIf TypeOf otype Is Boolean Then
                Return Convert.ToBoolean(False)
            ElseIf TypeOf otype Is Long Then
                Return Convert.ToInt64(0)
            ElseIf TypeOf otype Is Decimal Then
                Return Convert.ToDecimal(0)
            ElseIf TypeOf otype Is Short Then
                Return Convert.ToInt16(0)
            ElseIf TypeOf otype Is Integer Then
                Return Convert.ToInt32(0)
            ElseIf TypeOf otype Is Double Then
                Return Convert.ToDouble(0)
            ElseIf TypeOf otype Is Single Then
                Return Convert.ToSingle(0)
            ElseIf TypeOf otype Is String Then
                Return Convert.ToString("")
            ElseIf TypeOf otype Is Char Then
                Return Convert.ToChar(0)
            ElseIf TypeOf otype Is Date Then
                Return Nothing
            Else
                Throw New NotImplementedException("Implement me: " + otype.GetType.Name)
            End If

        End Function

        Public Shared Function AndObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return BitWiseOpObject(Left, Right, New AndHandler())
        End Function


        Class AndHandler
            Implements BitWiseOpHandler

            Public Function DoBitWiseOp(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Byte, ByVal o2 As Byte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Integer, ByVal o2 As Integer) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Long, ByVal o2 As Long) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As SByte, ByVal o2 As SByte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Short, ByVal o2 As Short) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As ULong, ByVal o2 As ULong) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UShort, ByVal o2 As UShort) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 And o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Object, ByVal o2 As Object) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return AndObjects(o1, o2)
            End Function

            Public Function DoBitWiseOp(ByVal o1 As String, ByVal o2 As String) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return DoBitWiseOp(CType(Convert.ToDouble(o1), Long), CType(Convert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "And"
            End Function
        End Class

        Public Shared Function CompareObject(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Integer
            Throw New NotImplementedException
        End Function

        Private Shared Function CompareObjectInternal(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As CompareResult
            Dim codeLeft, codeRight As TypeCode
            Const codeNothing As TypeCode = TypeCode.Empty

            If Left Is Nothing Then
                codeLeft = codeNothing
            Else
                codeLeft = Type.GetTypeCode(Left.GetType)
            End If
            If Right Is Nothing Then
                codeRight = codeNothing
            Else
                codeRight = Type.GetTypeCode(Right.GetType)
            End If

            If codeRight = codeNothing AndAlso codeLeft = codeNothing Then Return New CompareResult(CompareResultType.Success, 0)

            If codeRight = codeLeft OrElse codeRight = codeNothing OrElse codeLeft = codeNothing Then
                Dim codeToCompare As TypeCode = codeLeft
                If codeLeft = codeNothing Then codeToCompare = codeRight
                Select Case codeToCompare
                    Case TypeCode.Boolean
                        Return New CompareResult(CompareResultType.Success, CompareBoolean(CBool(Left), CBool(Right)))
                    Case TypeCode.Byte
                        Return New CompareResult(CompareResultType.Success, CompareByte(CByte(Left), CByte(Right)))
                    Case TypeCode.Char
                        Return New CompareResult(CompareResultType.Success, CompareChar(CChar(Left), CChar(Right)))
                    Case TypeCode.DateTime
                        Return New CompareResult(CompareResultType.Success, CompareDate(CDate(Left), CDate(Right)))
                    Case TypeCode.Decimal
                        Return New CompareResult(CompareResultType.Success, CompareDecimal(CDec(Left), CDec(Right)))
                    Case TypeCode.Double
                        Return New CompareResult(CompareResultType.Success, CompareDouble(CDbl(Left), CDbl(Right)))
                    Case TypeCode.Int16
                        Return New CompareResult(CompareResultType.Success, CompareInt16(CShort(Left), CShort(Right)))
                    Case TypeCode.Int32
                        Return New CompareResult(CompareResultType.Success, CompareInt32(CInt(Left), CInt(Right)))
                    Case TypeCode.Int64
                        Return New CompareResult(CompareResultType.Success, CompareInt64(CLng(Left), CLng(Right)))
                    Case TypeCode.SByte
                        Return New CompareResult(CompareResultType.Success, CompareSByte(CSByte(Left), CSByte(Right)))
                    Case TypeCode.Single
                        Return New CompareResult(CompareResultType.Success, CompareSingle(CSng(Left), CSng(Right)))
                    Case TypeCode.String
                        Return New CompareResult(CompareResultType.Success, CompareString(CStr(Left), CStr(Right), TextCompare))
                    Case TypeCode.UInt16
                        Return New CompareResult(CompareResultType.Success, CompareUInt16(CUShort(Left), CUShort(Right)))
                    Case TypeCode.UInt32
                        Return New CompareResult(CompareResultType.Success, CompareUInt32(CUInt(Left), CUInt(Right)))
                    Case TypeCode.UInt64
                        Return New CompareResult(CompareResultType.Success, CompareUInt64(CULng(Left), CULng(Right)))
                    Case TypeCode.Object
                        Throw New NotImplementedException
                    Case TypeCode.DBNull
                        Throw New NotImplementedException
                End Select
            End If

            Select Case CType(codeLeft << TypeCombinations.SHIFT Or codeRight, TypeCombinations)
                Case TypeCombinations.String_Double, TypeCombinations.Double_String
                    Return New CompareResult(CompareResultType.Success, CompareDouble(Conversions.ToDouble(Left), Conversions.ToDouble(Right)))
                Case TypeCombinations.Boolean_String, TypeCombinations.String_Boolean
                    Return New CompareResult(CompareResultType.Success, CompareBoolean(Conversions.ToBoolean(Left), Conversions.ToBoolean(Right)))
                Case Else
                    Throw New NotImplementedException("Not implemented comparison between '" & codeLeft.ToString() & "' and '" & codeRight.ToString() & "'")
            End Select

        End Function

        Public Shared Function CompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value = 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value > 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value >= 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value < 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value <= 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            result = CompareObjectInternal(Left, Right, TextCompare)
            Select Case result.Type
                Case CompareResultType.Fail
                    Throw New NotImplementedException
                Case CompareResultType.Success
                    Return result.Value <> 0
                Case CompareResultType.UserDefined
                    Throw New NotImplementedException
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Shared Function CompareString(ByVal Left As String, ByVal Right As String, ByVal TextCompare As Boolean) As Integer
            If DirectCast(Left, Object) Is Nothing Then
                Left = ""
            End If
            If DirectCast(Right, Object) Is Nothing Then
                Right = ""
            End If
            If TextCompare Then
                Return Left.CompareTo(Right)
            Else
                Return String.CompareOrdinal(Left, Right)
            End If
        End Function

        Public Shared Function ConcatenateObject(ByVal o1 As Object, ByVal o2 As Object) As Object
            If (o1 Is Nothing) Then
                o1 = ""
            Else
                Dim tc1 As TypeCode = GetTypeCode(o1)
                If (tc1.Equals(TypeCode.DBNull) Or tc1.Equals(TypeCode.Empty)) Then
                    o1 = ""
                End If
            End If

            If (o2 Is Nothing) Or (TypeOf o1 Is DBNull) Then
                o2 = ""
            Else
                Dim tc2 As TypeCode = GetTypeCode(o2)
                If (tc2.Equals(TypeCode.DBNull) Or tc2.Equals(TypeCode.Empty)) Then
                    o2 = ""
                End If
            End If

            Dim ret As Object
            Try
                If (InvokeBinaryOperator(o1, o2, "op_Concatenate", ret)) Then
                    Return ret
                End If
            Catch ex As Exception
                If (TypeOf ex Is NotImplementedException) Then
                    Throw ex
                End If
                Throw New InvalidCastException("Operator '+' is not defined for type '" + GetTypeCode(o1).ToString() + "' and type '" + GetTypeCode(o2).ToString() + "'.")
            End Try
            
            Return String.Concat(Convert.ToString(o1), Convert.ToString(o2))

        End Function

        Private Shared Function InvokeBinaryOperator(ByVal left As Object, ByVal right As Object, ByVal operation As String, ByRef ret As Object) As Boolean
            Dim tleft As Type = left.GetType()
            Dim tright As Type = right.GetType()
            Dim types() As Type = {tleft, tright}
            Dim parameters() As Object = {left, right}

            Dim methodL As MethodInfo = tleft.GetMethod(operation, BindingFlags.Static Or BindingFlags.Public, Nothing, types, Nothing)
            If (methodL IsNot Nothing) Then
                ret = methodL.Invoke(Nothing, parameters)
                Return True
            End If

            Dim methodR As MethodInfo = tright.GetMethod(operation, BindingFlags.Static Or BindingFlags.Public)
            If (methodR IsNot Nothing) Then
                ret = methodR.Invoke(Nothing, parameters)
                Return True
            End If

            ret = Nothing
            Return False
        End Function

        Private Shared Function InvokeUnaryOperator(ByVal operand As Object, ByVal operation As String, ByRef ret As Object) As Boolean
            Dim operandType As Type = operand.GetType()
            Dim types() As Type = {operandType}
            Dim parameters() As Object = {operand}

            Dim method As MethodInfo = operandType.GetMethod(operation, BindingFlags.Static Or BindingFlags.Public, Nothing, types, Nothing)
            If (method IsNot Nothing) Then
                ret = method.Invoke(Nothing, parameters)
                Return True
            End If

            ret = Nothing
            Return False
        End Function

        Public Shared Function ConditionalCompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectEqual(Left, right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectGreater(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectGreaterEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectLess(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectLessEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectNotEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function DivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Public Shared Function ExponentObject(ByVal Left As Object, ByVal Right As Object) As Object
            Dim exp As Double = Convert.ToDouble(PlusObject(Right))
            Dim base As Double = Convert.ToDouble(PlusObject(Left))

            Return Math.Pow(base, exp)
        End Function

        Public Shared Function IntDivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Public Shared Function LeftShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            If (Operand Is Nothing) Then
                Return 0
            End If

            Dim tcOperand As TypeCode = GetTypeCode(Operand)

            Try
                If (tcOperand = TypeCode.Object) Then
                    Return LeftShiftObject_(Operand, Amount)
                End If

                Dim intAmount As Integer = 0

                If (Amount IsNot Nothing) Then
                    intAmount = GetAmountAsInteger(Amount)
                End If


                Select Case tcOperand
                    Case TypeCode.Boolean
                        If (Convert.ToBoolean(Operand)) Then
                            Return -1S << intAmount
                        End If
                        Return 0
                    Case TypeCode.Byte
                        Return Convert.ToByte(Operand) << intAmount
                    Case TypeCode.Decimal
                        Return Convert.ToInt64(Convert.ToDecimal(Operand)) << intAmount
                    Case TypeCode.Double
                        Return Convert.ToInt64(Convert.ToDouble(Operand)) << intAmount
                    Case TypeCode.Int16
                        Return Convert.ToInt16(Operand) << intAmount
                    Case TypeCode.Int32
                        Return Convert.ToInt32(Operand) << intAmount
                    Case TypeCode.Int64
                        Return Convert.ToInt64(Operand) << intAmount
                    Case TypeCode.SByte
                        Return Convert.ToSByte(Operand) << intAmount
                    Case TypeCode.String
                        Return Convert.ToInt64(Convert.ToString(Operand)) << intAmount
                    Case TypeCode.Single
                        Return Convert.ToInt64(Convert.ToSingle(Operand)) << intAmount
                    Case TypeCode.UInt16
                        Return Convert.ToUInt16(Operand) << intAmount
                    Case TypeCode.UInt32
                        Return Convert.ToUInt32(Operand) << intAmount
                    Case TypeCode.UInt64
                        Return Convert.ToUInt64(Operand) << intAmount

                End Select

            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '<<' is not defined for type '" + tcOperand.ToString() + "'.")
        End Function

        Public Shared Function LikeObject(ByVal Source As Object, ByVal Pattern As Object, ByVal CompareOption As CompareMethod) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LikeString(ByVal Source As String, ByVal Pattern As String, ByVal CompareOption As CompareMethod) As Boolean
            Throw New NotImplementedException
        End Function
        Public Shared Function ModObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function MultiplyObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Public Shared Function PlusBoolean(ByVal Operand As Boolean) As Object
            If (Operand) Then
                Return -1S
            End If
            Return 0S
        End Function

        Public Shared Function PlusString(ByVal Operand As String) As Object
            Return Double.Parse(Operand)
        End Function

        Public Shared Function NegateBoolean(ByVal Operand As Boolean) As Object
            If (Operand) Then
                Return 1S
            End If
            Return 0S
        End Function

        Public Shared Function NegateByte(ByVal Operand As Byte) As Object
            Return -1S * Operand
        End Function

        Public Shared Function NegateUInt16(ByVal Operand As UShort) As Object
            Return -1 * Operand
        End Function

        Public Shared Function NegateUInt32(ByVal Operand As UInteger) As Object
            Return -1L * Operand
        End Function

        Public Shared Function NegateUInt64(ByVal Operand As ULong) As Object
            Return -1 * CType(Operand, Decimal)
        End Function

        Public Shared Function NegateSByte(ByVal Operand As SByte) As Object
            If (Operand = SByte.MinValue) Then
                Return 1S + SByte.MaxValue
            End If
            Return -Operand
        End Function

        Public Shared Function NegateDecimal(ByVal Operand As Decimal) As Object
            Return -Operand
        End Function

        Public Shared Function NegateDouble(ByVal Operand As Double) As Object
            Return -Operand
        End Function

        Public Shared Function NegateSingle(ByVal Operand As Single) As Object
            Return -Operand
        End Function

        Public Shared Function NegateInt16(ByVal Operand As Int16) As Object
            If (Operand = Int16.MinValue) Then
                Return 1 + Int16.MaxValue
            End If
            Return -Operand
        End Function

        Public Shared Function NegateInt32(ByVal Operand As Int32) As Object
            If (Operand = Int32.MinValue) Then
                Return 1L + Int32.MaxValue
            End If
            Return -Operand
        End Function

        Public Shared Function NegateInt64(ByVal Operand As Int64) As Object
            If (Operand = Int64.MinValue) Then
                Return 1 + CType(Int64.MaxValue, ULong)
            End If
            Return -Operand
        End Function

        Public Shared Function NegateString(ByVal Operand As String) As Object
            Dim d As Double = Double.Parse(Operand)
            Return NegateDouble(d)
        End Function

        Public Shared Function NegateObject_(ByVal Operand As Object) As Object
            Dim ret As Object
            If Not (InvokeUnaryOperator(Operand, "op_UnaryNegation", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function PlusObject_(ByVal Operand As Object) As Object
            Dim ret As Object
            If Not (InvokeUnaryOperator(Operand, "op_UnaryPlus", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function RightShiftObject_(ByVal Operand As Object, ByVal Amount As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(Operand, Amount, "op_RightShift", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function LeftShiftObject_(ByVal Operand As Object, ByVal Amount As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(Operand, Amount, "op_LeftShift", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function NegateObject(ByVal Operand As Object) As Object

            If (Operand Is Nothing) Then
                Return 0
            End If

            Dim tc As TypeCode = GetTypeCode(Operand)
            Try
                Select Case tc
                    Case TypeCode.Boolean
                        Return NegateBoolean(Convert.ToBoolean(Operand))
                    Case TypeCode.Byte
                        Return NegateByte(Convert.ToByte(Operand))
                    Case TypeCode.Decimal
                        Return NegateDecimal(Convert.ToDecimal(Operand))
                    Case TypeCode.Double
                        Return NegateDouble(Convert.ToDouble(Operand))
                    Case TypeCode.Int16
                        Return NegateInt16(Convert.ToInt16(Operand))
                    Case TypeCode.Int32
                        Return NegateInt32(Convert.ToInt32(Operand))
                    Case TypeCode.Int64
                        Return NegateInt64(Convert.ToInt64(Operand))
                    Case TypeCode.SByte
                        Return NegateSByte(Convert.ToSByte(Operand))
                    Case TypeCode.Single
                        Return NegateSingle(Convert.ToSingle(Operand))
                    Case TypeCode.String
                        Return NegateString(Convert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return NegateUInt16(Convert.ToUInt16(Operand))
                    Case TypeCode.UInt32
                        Return NegateUInt32(Convert.ToUInt32(Operand))
                    Case TypeCode.UInt64
                        Return NegateUInt64(Convert.ToUInt64(Operand))
                    Case TypeCode.Object
                        Return NegateObject_(Operand)

                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '-' is not defined for type '" + tc.ToString() + "'.")
        End Function

        Public Shared Function NotObject_(ByVal Operand As Object) As Object
            Dim ret As Object
            If Not (InvokeUnaryOperator(Operand, "op_OnesComplement", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function NotString(ByVal Operand As String) As Object
            Return Not (CType((Convert.ToDouble(Operand)), Long))
        End Function


        Public Shared Function NotObject(ByVal Operand As Object) As Object
            If (Operand Is Nothing) Then
                Return -1
            End If

            Dim tc As TypeCode = GetTypeCode(Operand)
            Try
                Select Case tc
                    Case TypeCode.Boolean
                        Return Not Convert.ToBoolean(Operand)
                    Case TypeCode.Byte
                        Return Not Convert.ToByte(Operand)
                    Case TypeCode.Decimal
                        Return Not (CType((Convert.ToDecimal(Operand)), Long))
                    Case TypeCode.Double
                        Return Not (CType((Convert.ToDouble(Operand)), Long))
                    Case TypeCode.Int16
                        Return Not Convert.ToInt16(Operand)
                    Case TypeCode.Int32
                        Return Not Convert.ToInt32(Operand)
                    Case TypeCode.Int64
                        Return Not Convert.ToInt64(Operand)
                    Case TypeCode.SByte
                        Return Not Convert.ToSByte(Operand)
                    Case TypeCode.Single
                        Return Not (CType((Convert.ToSingle(Operand)), Long))
                    Case TypeCode.String
                        Return NotString(Convert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return Not Convert.ToUInt16(Operand)
                    Case TypeCode.UInt32
                        Return Not Convert.ToUInt32(Operand)
                    Case TypeCode.UInt64
                        Return Not Convert.ToUInt64(Operand)
                    Case TypeCode.Object
                        Return NotObject_(Operand)

                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator 'Not' is not defined for type '" + tc.ToString() + "'.")
        End Function

        Private Shared Function OrObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(o1, o2, "op_BitwiseOr", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function XorObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(o1, o2, "op_ExclusiveOr", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function AndObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object
            If Not (InvokeBinaryOperator(o1, o2, "op_BitwiseAnd", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function GetAsLong(ByVal obj As Object) As Long
            Dim tc As TypeCode = GetTypeCode(obj)
            Select Case tc
                Case TypeCode.Boolean
                    If (Convert.ToBoolean(obj)) Then
                        Return -1L
                    End If
                    Return 0L
                Case TypeCode.Byte
                    Return CType(Convert.ToByte(obj), Long)
                Case TypeCode.Int16
                    Return CType(Convert.ToInt16(obj), Long)
                Case TypeCode.Int32
                    Return CType(Convert.ToInt32(obj), Long)
                Case TypeCode.Int64
                    Return Convert.ToInt64(obj)
                Case TypeCode.SByte
                    Return CType(Convert.ToSByte(obj), Long)
                Case TypeCode.String
                    Return CType(Math.Round(Convert.ToDouble(Convert.ToString(obj))), Long)
                Case TypeCode.Double
                    Return CType(Math.Round(Convert.ToDouble(obj)), Long)
                Case TypeCode.Decimal
                    Return CType(Math.Round(Convert.ToDecimal(obj)), Long)
                Case TypeCode.Single
                    Return CType(Math.Round(Convert.ToSingle(obj)), Long)
                Case TypeCode.UInt16
                    Return CType(Convert.ToUInt16(obj), Long)
                Case TypeCode.UInt32
                    Return CType(Convert.ToUInt32(obj), Long)
                Case TypeCode.UInt64
                    Return CType(Convert.ToUInt64(obj), Long)
            End Select
            Throw New NotImplementedException()
        End Function

        Public Shared Function OrObject(ByVal o1 As Object, ByVal o2 As Object) As Object
            Return BitWiseOpObject(o1, o2, New OrHandler())
        End Function

        Class OrHandler
            Implements BitWiseOpHandler

            Public Function DoBitWiseOp(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Byte, ByVal o2 As Byte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Integer, ByVal o2 As Integer) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Long, ByVal o2 As Long) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As SByte, ByVal o2 As SByte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Short, ByVal o2 As Short) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As ULong, ByVal o2 As ULong) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UShort, ByVal o2 As UShort) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Or o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Object, ByVal o2 As Object) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return OrObjects(o1, o2)
            End Function

            Public Function DoBitWiseOp(ByVal o1 As String, ByVal o2 As String) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return DoBitWiseOp(CType(Convert.ToDouble(o1), Long), CType(Convert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "Or"
            End Function
        End Class

        Interface BitWiseOpHandler
            Function DoBitWiseOp(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object
            Function DoBitWiseOp(ByVal o1 As Byte, ByVal o2 As Byte) As Object
            Function DoBitWiseOp(ByVal o1 As Short, ByVal o2 As Short) As Object
            Function DoBitWiseOp(ByVal o1 As Integer, ByVal o2 As Integer) As Object
            Function DoBitWiseOp(ByVal o1 As Long, ByVal o2 As Long) As Object
            Function DoBitWiseOp(ByVal o1 As SByte, ByVal o2 As SByte) As Object
            Function DoBitWiseOp(ByVal o1 As UShort, ByVal o2 As UShort) As Object
            Function DoBitWiseOp(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object
            Function DoBitWiseOp(ByVal o1 As ULong, ByVal o2 As ULong) As Object
            Function DoBitWiseOp(ByVal o1 As String, ByVal o2 As String) As Object
            Function DoBitWiseOp(ByVal o1 As Object, ByVal o2 As Object) As Object
            Function GetOpName() As String
        End Interface

        Public Shared Function BitWiseOpObject(ByVal o1 As Object, ByVal o2 As Object, ByVal opHandler As BitWiseOpHandler) As Object
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            Dim destTc As TypeCode = DestTypeCodeBitwiseOp(o1, o2)
            Try
                Select Case destTc
                    Case TypeCode.Boolean
                        Return opHandler.DoBitWiseOp(Convert.ToBoolean(o1), Convert.ToBoolean(o2))
                    Case TypeCode.Byte
                        Return opHandler.DoBitWiseOp(Convert.ToByte(o1), Convert.ToByte(o2))
                    Case TypeCode.Int16
                        Return opHandler.DoBitWiseOp(Convert.ToInt16(o1), Convert.ToInt16(o2))
                    Case TypeCode.Int32
                        Return opHandler.DoBitWiseOp(Convert.ToInt32(o1), Convert.ToInt32(o2))
                    Case TypeCode.Int64
                        Return opHandler.DoBitWiseOp(GetAsLong(o1), GetAsLong(o2))
                    Case TypeCode.SByte
                        Return opHandler.DoBitWiseOp(Convert.ToSByte(o1), Convert.ToSByte(o2))
                    Case TypeCode.String
                        Return opHandler.DoBitWiseOp(Convert.ToString(o1), Convert.ToString(o2))
                    Case TypeCode.UInt16
                        Return opHandler.DoBitWiseOp(Convert.ToUInt16(o1), Convert.ToUInt16(o2))
                    Case TypeCode.UInt32
                        Return opHandler.DoBitWiseOp(Convert.ToUInt32(o1), Convert.ToUInt32(o2))
                    Case TypeCode.UInt64
                        Return opHandler.DoBitWiseOp(Convert.ToUInt64(o1), Convert.ToUInt64(o2))

                End Select
                Return opHandler.DoBitWiseOp(o1, o2)
            Catch ex As Exception
                If (TypeOf ex Is NotImplementedException) Then
                    Throw ex
                End If
            End Try
            Throw New InvalidCastException("Operator '" + opHandler.GetOpName() + "' is not defined for type '" + GetTypeCode(o1).ToString() + "' and type '" + GetTypeCode(o2).ToString() + "'.")

        End Function

        Public Shared Function PlusObject(ByVal Operand As Object) As Object
            If (Operand Is Nothing) Then
                Return 0
            End If

            Dim tc As TypeCode = GetTypeCode(Operand)
            Try
                Select Case tc
                    Case TypeCode.Boolean
                        Return PlusBoolean(Convert.ToBoolean(Operand))
                    Case TypeCode.Byte
                        Return Convert.ToByte(Operand)
                    Case TypeCode.Decimal
                        Return Convert.ToDecimal(Operand)
                    Case TypeCode.Double
                        Return Convert.ToDouble(Operand)
                    Case TypeCode.Int16
                        Return Convert.ToInt16(Operand)
                    Case TypeCode.Int32
                        Return Convert.ToInt32(Operand)
                    Case TypeCode.Int64
                        Return Convert.ToInt64(Operand)
                    Case TypeCode.SByte
                        Return Convert.ToSByte(Operand)
                    Case TypeCode.Single
                        Return Convert.ToSingle(Operand)
                    Case TypeCode.String
                        Return PlusString(Convert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return Convert.ToUInt16(Operand)
                    Case TypeCode.UInt32
                        Return Convert.ToUInt32(Operand)
                    Case TypeCode.UInt64
                        Return Convert.ToUInt64(Operand)
                    Case TypeCode.Object
                        Return PlusObject_(Operand)

                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '+' is not defined for type '" + tc.ToString() + "'.")
        End Function

        Private Shared Function GetAmountAsInteger(ByVal Amount As Object) As Integer
            Dim tcAmount As TypeCode = GetTypeCode(Amount)
            Select Case tcAmount
                Case TypeCode.Byte
                    Return Convert.ToInt32(Convert.ToByte(Amount))
                Case TypeCode.Decimal
                    Return Convert.ToInt32(Convert.ToDecimal(Amount))
                Case TypeCode.Double
                    Return Convert.ToInt32(Convert.ToDouble(Amount))
                Case TypeCode.Int16
                    Return Convert.ToInt32(Convert.ToInt16(Amount))
                Case TypeCode.Int32
                    Return Convert.ToInt32(Convert.ToInt32(Amount))
                Case TypeCode.Int64
                    Return Convert.ToInt32(Convert.ToInt64(Amount))
                Case TypeCode.SByte
                    Return Convert.ToInt32(Convert.ToSByte(Amount))
                Case TypeCode.String
                    Return Convert.ToInt32(Convert.ToString(Amount))
                Case TypeCode.Single
                    Return Convert.ToInt32(Convert.ToSingle(Amount))
                Case TypeCode.UInt16
                    Return Convert.ToInt32(Convert.ToUInt16(Amount))
                Case TypeCode.UInt32
                    Return Convert.ToInt32(Convert.ToUInt32(Amount))
                Case TypeCode.UInt64
                    Return Convert.ToInt32(Convert.ToUInt64(Amount))

            End Select
            Throw New InvalidCastException("Amount")
        End Function

        Public Shared Function RightShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            If (Operand Is Nothing) Then
                Return 0
            End If

            Dim tcOperand As TypeCode = GetTypeCode(Operand)

            Try
                If (tcOperand = TypeCode.Object) Then
                    Return RightShiftObject_(Operand, Amount)
                End If

                Dim intAmount As Integer = 0

                If (Amount IsNot Nothing) Then
                    intAmount = GetAmountAsInteger(Amount)
                End If


                Select Case tcOperand
                    Case TypeCode.Boolean
                        If (Convert.ToBoolean(Operand)) Then
                            Return -1S >> intAmount
                        End If
                        Return 0
                    Case TypeCode.Byte
                        Return Convert.ToByte(Operand) >> intAmount
                    Case TypeCode.Decimal
                        Return Convert.ToInt64(Convert.ToDecimal(Operand)) >> intAmount
                    Case TypeCode.Double
                        Return Convert.ToInt64(Convert.ToDouble(Operand)) >> intAmount
                    Case TypeCode.Int16
                        Return Convert.ToInt16(Operand) >> intAmount
                    Case TypeCode.Int32
                        Return Convert.ToInt32(Operand) >> intAmount
                    Case TypeCode.Int64
                        Return Convert.ToInt64(Operand) >> intAmount
                    Case TypeCode.SByte
                        Return Convert.ToSByte(Operand) >> intAmount
                    Case TypeCode.String
                        Return Convert.ToInt64(Convert.ToString(Operand)) >> intAmount
                    Case TypeCode.Single
                        Return Convert.ToInt64(Convert.ToSingle(Operand)) >> intAmount
                    Case TypeCode.UInt16
                        Return Convert.ToUInt16(Operand) >> intAmount
                    Case TypeCode.UInt32
                        Return Convert.ToUInt32(Operand) >> intAmount
                    Case TypeCode.UInt64
                        Return Convert.ToUInt64(Operand) >> intAmount

                End Select

            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '>>' is not defined for type '" + tcOperand.ToString() + "'.")
        End Function

        Public Shared Function SubtractObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return AddObject(Left, NegateObject(Right))
        End Function

        Public Shared Function XorObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return BitWiseOpObject(Left, Right, New XorHandler())
        End Function

        Class XorHandler
            Implements BitWiseOpHandler

            Public Function DoBitWiseOp(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Byte, ByVal o2 As Byte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Integer, ByVal o2 As Integer) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Long, ByVal o2 As Long) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As SByte, ByVal o2 As SByte) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Short, ByVal o2 As Short) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As ULong, ByVal o2 As ULong) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As UShort, ByVal o2 As UShort) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return o1 Xor o2
            End Function

            Public Function DoBitWiseOp(ByVal o1 As Object, ByVal o2 As Object) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return XorObjects(o1, o2)
            End Function

            Public Function DoBitWiseOp(ByVal o1 As String, ByVal o2 As String) As Object Implements BitWiseOpHandler.DoBitWiseOp
                Return DoBitWiseOp(CType(Convert.ToDouble(o1), Long), CType(Convert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "Xor"
            End Function
        End Class

        Private Sub New()

        End Sub
    End Class
End Namespace
#End If