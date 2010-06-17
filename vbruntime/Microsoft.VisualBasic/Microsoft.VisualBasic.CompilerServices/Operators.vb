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

Imports System
Imports System.Reflection
Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class Operators

        Enum CompareResult
            Less
            Equal
            Greater
            NotResolved
        End Enum

        Private Shared Function CompareBoolean(ByVal Left As Boolean, ByVal Right As Boolean) As CompareResult
            If Left = Right Then Return CompareResult.Equal
            If Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareByte(ByVal Left As Byte, ByVal Right As Byte) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareChar(ByVal Left As Char, ByVal Right As Char) As CompareResult
            If Left = Right Then Return CompareResult.Equal
            If Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareDate(ByVal Left As Date, ByVal Right As Date) As CompareResult
            Return IntToCompareResult(DateTime.Compare(Left, Right))
        End Function

        Private Shared Function IntToCompareResult(ByVal val As Integer) As CompareResult
            If (val = 0) Then
                Return CompareResult.Equal
            ElseIf (val > 0) Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareDecimal(ByVal Left As Decimal, ByVal Right As Decimal) As CompareResult
            Return IntToCompareResult(Decimal.Compare(Left, Right))
        End Function

        Private Shared Function CompareDouble(ByVal Left As Double, ByVal Right As Double) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareInt16(ByVal Left As Int16, ByVal Right As Int16) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareInt32(ByVal Left As Int32, ByVal Right As Int32) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareInt64(ByVal Left As Int64, ByVal Right As Int64) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareSByte(ByVal Left As SByte, ByVal Right As SByte) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareSingle(ByVal Left As Single, ByVal Right As Single) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareUInt16(ByVal Left As UInt16, ByVal Right As UInt16) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareUInt32(ByVal Left As UInt32, ByVal Right As UInt32) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Private Shared Function CompareUInt64(ByVal Left As UInt64, ByVal Right As UInt64) As CompareResult
            If Left = Right Then
                Return CompareResult.Equal
            ElseIf Left > Right Then
                Return CompareResult.Greater
            Else
                Return CompareResult.Less
            End If
        End Function

        Friend Shared Function GetTypeCode(ByVal obj As Object) As TypeCode
            If (obj Is Nothing) Then
                Return TypeCode.Empty
            ElseIf (TypeOf obj Is IConvertible) Then
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

        Shared DEST_TYPECODE_SUBTRACT As TypeCode(,) = { _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Double, TypeCode.Single, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.DateTime, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double} _
}

        Shared DEST_TYPECODE_DIVIDE As TypeCode(,) = { _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Double, TypeCode.Single, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double} _
 }
        Shared DEST_TYPECODE_INTDIVIDE As TypeCode(,) = { _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64} _
}

        Shared DEST_TYPECODE_MULTIPLY As TypeCode(,) = { _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Double, TypeCode.Single, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
{TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double} _
}

        Shared DEST_TYPECODE_BITWISE_OP As TypeCode(,) = { _
 {TypeCode.Int32, TypeCode.Object, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
  {TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Boolean, TypeCode.Object, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Boolean, TypeCode.Boolean}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.SByte, TypeCode.Object, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.SByte, TypeCode.Int64}, _
 {TypeCode.Byte, TypeCode.Object, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Byte, TypeCode.Int64}, _
 {TypeCode.Int16, TypeCode.Object, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int16, TypeCode.Int64}, _
 {TypeCode.UInt16, TypeCode.Object, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt16, TypeCode.Int64}, _
 {TypeCode.Int32, TypeCode.Object, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
 {TypeCode.UInt32, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt32, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.UInt64, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.UInt64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64}, _
 {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
 {TypeCode.Int32, TypeCode.Object, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int32, TypeCode.Int64}, _
 {TypeCode.Int64, TypeCode.Object, TypeCode.Empty, TypeCode.Boolean, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64} _
 }

        Shared DEST_TYPECODE_MOD As TypeCode(,) = { _
  {TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
  {TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.SByte, TypeCode.Empty, TypeCode.SByte, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int16, TypeCode.Empty, TypeCode.Int16, TypeCode.Int16, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int32, TypeCode.Empty, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Int64, TypeCode.Empty, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Decimal, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Single, TypeCode.Empty, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Single, TypeCode.Double, TypeCode.Single, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Decimal, TypeCode.Empty, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Empty, TypeCode.Empty, TypeCode.Double}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty}, _
  {TypeCode.Empty, TypeCode.Object, TypeCode.Empty, TypeCode.Double, TypeCode.Empty, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Empty, TypeCode.Empty, TypeCode.Double} _
  }

        Shared DEST_TYPECODE_COMPARE As TypeCode(,) = { _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
{TypeCode.Object, TypeCode.Object, TypeCode.DBNull, TypeCode.Object, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Object, TypeCode.Object}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Boolean, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Object, TypeCode.Boolean}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Char, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Object, TypeCode.String}, _
{TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.SByte, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int32, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int32, TypeCode.Int32}, _
{TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Byte, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int32, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int32, TypeCode.Int32}, _
{TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int16, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int32, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int32, TypeCode.Int32}, _
{TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.UInt16, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int32, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int32, TypeCode.Int32}, _
{TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Int32, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int32, TypeCode.Int32}, _
{TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.UInt32, TypeCode.Int64, TypeCode.Int64, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int64, TypeCode.Int64}, _
{TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Int64, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Int64, TypeCode.Int64}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.UInt64, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Object, TypeCode.Object}, _
{TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Double, TypeCode.Double}, _
{TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Double, TypeCode.Double}, _
{TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Decimal, TypeCode.Decimal}, _
{TypeCode.Object, TypeCode.Object, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.DateTime, TypeCode.Object, TypeCode.DateTime}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.Object, TypeCode.Object, TypeCode.Object}, _
{TypeCode.Object, TypeCode.Object, TypeCode.Object, TypeCode.Boolean, TypeCode.String, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int32, TypeCode.Int64, TypeCode.Int64, TypeCode.Object, TypeCode.Double, TypeCode.Double, TypeCode.Decimal, TypeCode.DateTime, TypeCode.Object, TypeCode.String} _
}

        'Returns the expected return TypeCode of operation between these two objects or TypeCode.Empty if operation is not possible.
        'Notice: The expected TypeCode may not be the actual TypeCode of the return type. The actual type return is "black box"ed 
        'by the operation implementation. For example in the case of Integer and Short the expected return TypeCode is of Integer 
        'but the actual return type may be Long (in the case of overflow)
        Private Shared Function DestTypeCodeOpAdd(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_ADD(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpSubtract(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_SUBTRACT(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpDivide(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_DIVIDE(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpIntDivide(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_INTDIVIDE(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpMultiply(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_MULTIPLY(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpMod(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_MOD(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeBitwiseOp(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_BITWISE_OP(GetTypeCode(obj1), GetTypeCode(obj2))
        End Function

        Private Shared Function DestTypeCodeOpCompare(ByVal obj1 As Object, ByVal obj2 As Object) As TypeCode
            Return DEST_TYPECODE_COMPARE(GetTypeCode(obj1), GetTypeCode(obj2))
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
            Dim ret As Object = Nothing
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

        Public Shared Function AddObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Return 0
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Right = CreateNullObjectType(Left)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpAdd(Left, Right)
            Try
                Select Case destTc
                    'Case TypeCode.Empty -> break
                    Case TypeCode.Boolean
                        Return AddBooleans(VBConvert.ToBoolean(Left), VBConvert.ToBoolean(Right))
                    Case TypeCode.Byte
                        Return AddBytes(VBConvert.ToByte(Left), VBConvert.ToByte(Right))
                    Case TypeCode.Char
                        Return AddChars(VBConvert.ToChar(Left), VBConvert.ToChar(Right))
                    Case TypeCode.DateTime
                        Return AddDateTimes(VBConvert.ToDateTime(Left), VBConvert.ToDateTime(Right))
                    Case TypeCode.Decimal
                        Return AddDecimals(VBConvert.ToDecimal(Left), VBConvert.ToDecimal(Right))
                    Case TypeCode.Double
                        Return AddDoubles(VBConvert.ToDouble(Left), VBConvert.ToDouble(Right))
                    Case TypeCode.Int16
                        Return AddInt16s(VBConvert.ToInt16(Left), VBConvert.ToInt16(Right))
                    Case TypeCode.Int32
                        Return AddInt32s(VBConvert.ToInt32(Left), VBConvert.ToInt32(Right))
                    Case TypeCode.Int64
                        Return AddInt64s(VBConvert.ToInt64(Left), VBConvert.ToInt64(Right))
                    Case TypeCode.SByte
                        Return AddSBytes(VBConvert.ToSByte(Left), VBConvert.ToSByte(Right))
                    Case TypeCode.Single
                        Return AddSingles(VBConvert.ToSingle(Left), VBConvert.ToSingle(Right))
                    Case TypeCode.String
                        Return AddStrings(VBConvert.ToString(Left), VBConvert.ToString(Right))
                    Case TypeCode.UInt16
                        Return AddUInt16s(VBConvert.ToUInt16(Left), VBConvert.ToUInt16(Right))
                    Case TypeCode.UInt32
                        Return AddUInt32s(VBConvert.ToUInt32(Left), VBConvert.ToUInt32(Right))
                    Case TypeCode.UInt64
                        Return AddUInt64s(VBConvert.ToUInt64(Left), VBConvert.ToUInt64(Right))

                End Select
                Return AddObjects(Left, Right)
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '+' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
        End Function



        'creates a real type of a Nothing object 
        Private Shared Function CreateNullObjectType(ByVal otype As Object) As Object

            If TypeOf otype Is Byte Then
                Return VBConvert.ToByte(0)
            ElseIf TypeOf otype Is Boolean Then
                Return VBConvert.ToBoolean(False)
            ElseIf TypeOf otype Is Long Then
                Return VBConvert.ToInt64(0)
            ElseIf TypeOf otype Is Decimal Then
                Return VBConvert.ToDecimal(0)
            ElseIf TypeOf otype Is Short Then
                Return VBConvert.ToInt16(0)
            ElseIf TypeOf otype Is Integer Then
                Return VBConvert.ToInt32(0)
            ElseIf TypeOf otype Is Double Then
                Return VBConvert.ToDouble(0)
            ElseIf TypeOf otype Is Single Then
                Return VBConvert.ToSingle(0)
            ElseIf TypeOf otype Is String Then
                Return VBConvert.ToString("")
            ElseIf TypeOf otype Is Char Then
                Return VBConvert.ToChar(0)
            ElseIf TypeOf otype Is Date Then
                Return Nothing
            Else
                Throw New NotImplementedException("Implement me: " + otype.GetType.Name)
            End If

        End Function

        Public Shared Function AndObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return BitWiseOpObject(Left, Right, New AndHandler())
        End Function


        Friend Class AndHandler
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
                Return DoBitWiseOp(CType(VBConvert.ToDouble(o1), Long), CType(VBConvert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "And"
            End Function
        End Class

        Public Shared Function CompareObject(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Integer
            Dim res As Integer = CompareObjectInternal(Left, Right, TextCompare)
            If (res = CompareResult.Equal) Then
                Return 0
            ElseIf (res = CompareResult.Greater) Then
                Return 1
            ElseIf (res = CompareResult.Less) Then
                Return -1
            End If
            Throw New InvalidOperationException
        End Function

        Private Shared Function CompareObjectInternal(ByVal o1 As Object, ByVal o2 As Object, ByVal TextCompare As Boolean) As CompareResult
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return CompareResult.Equal
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpCompare(o1, o2)

            Select Case destTc
                Case TypeCode.Boolean
                    Return CompareBoolean(VBConvert.ToBoolean(o1), VBConvert.ToBoolean(o2))
                Case TypeCode.Byte
                    Return CompareByte(VBConvert.ToByte(o1), VBConvert.ToByte(o2))
                Case TypeCode.Char
                    Return CompareChar(VBConvert.ToChar(o1), VBConvert.ToChar(o2))
                Case TypeCode.DateTime
                    Return CompareDate(VBConvert.ToDateTime(o1), VBConvert.ToDateTime(o2))
                Case TypeCode.Decimal
                    Return CompareDecimal(VBConvert.ToDecimal(o1), VBConvert.ToDecimal(o2))
                Case TypeCode.Double
                    Return CompareDouble(VBConvert.ToDouble(o1), VBConvert.ToDouble(o2))
                Case TypeCode.Int16
                    Return CompareInt16(VBConvert.ToInt16(o1), VBConvert.ToInt16(o2))
                Case TypeCode.Int32
                    Return CompareInt32(VBConvert.ToInt32(o1), VBConvert.ToInt32(o2))
                Case TypeCode.Int64
                    Return CompareInt64(VBConvert.ToInt64(o1), VBConvert.ToInt64(o2))
                Case TypeCode.SByte
                    Return CompareSByte(VBConvert.ToSByte(o1), VBConvert.ToSByte(o2))
                Case TypeCode.Single
                    Return CompareSingle(VBConvert.ToSingle(o1), VBConvert.ToSingle(o2))
                Case TypeCode.String
                    Return IntToCompareResult(CompareString(VBConvert.ToString(o1), VBConvert.ToString(o2), TextCompare))
                Case TypeCode.UInt16
                    Return CompareUInt16(VBConvert.ToUInt16(o1), VBConvert.ToUInt16(o2))
                Case TypeCode.UInt32
                    Return CompareUInt32(VBConvert.ToUInt32(o1), VBConvert.ToUInt32(o2))
                Case TypeCode.UInt64
                    Return CompareUInt64(VBConvert.ToUInt64(o1), VBConvert.ToUInt64(o2))
                Case TypeCode.DBNull
                    Throw New InvalidOperationException
                Case TypeCode.Empty
                    Throw New InvalidOperationException
                Case TypeCode.Object
                    Return CompareResult.NotResolved
            End Select
        End Function

        Public Shared Function CompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_Equality", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return result = CompareResult.Equal
            Catch ex As FormatException
                Throw New InvalidCastException(ex.Message)
            Catch ex As Exception
                Throw New InvalidCastException("Operator '=' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try
        End Function

        Public Shared Function CompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_GreaterThan", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return result = CompareResult.Greater
            Catch ex As Exception
                Throw New InvalidCastException("Operator '>' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try
        End Function

        Public Shared Function CompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_GreaterThanOrEqual", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return result = CompareResult.Greater Or result = CompareResult.Equal
            Catch ex As Exception
                Throw New InvalidCastException("Operator '>=' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'. (")
            End Try
        End Function

        Public Shared Function CompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_LessThan", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return result = CompareResult.Less
            Catch ex As Exception
                Throw New InvalidCastException("Operator '<' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try
        End Function

        Public Shared Function CompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_LessThanOrEqual", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return result = CompareResult.Less Or result = CompareResult.Equal
            Catch ex As Exception
                Throw New InvalidCastException("Operator '<=' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try
        End Function

        Public Shared Function CompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
            Dim result As CompareResult
            Try
                result = CompareObjectInternal(Left, Right, TextCompare)
                If (result = CompareResult.NotResolved) Then
                    Dim obj As Object = Nothing
                    If (InvokeBinaryOperator(Left, Right, "op_Inequality", obj)) Then
                        Return obj
                    Else
                        Throw New InvalidOperationException
                    End If
                End If
                Return Not result = CompareResult.Equal
            Catch ex As Exception
                Throw New InvalidCastException("Operator '<>' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try
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

        Public Shared Function ConcatenateObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) Then
                Left = ""
            Else
                Dim tc1 As TypeCode = GetTypeCode(Left)
                If (tc1.Equals(TypeCode.DBNull) Or tc1.Equals(TypeCode.Empty)) Then
                    Left = ""
                End If
            End If

            If (Right Is Nothing) Or (TypeOf Left Is DBNull) Then
                Right = ""
            Else
                Dim tc2 As TypeCode = GetTypeCode(Right)
                If (tc2.Equals(TypeCode.DBNull) Or tc2.Equals(TypeCode.Empty)) Then
                    Right = ""
                End If
            End If

            Dim ret As Object = Nothing
            Try
                If (InvokeBinaryOperator(Left, Right, "op_Concatenate", ret)) Then
                    Return ret
                End If
            Catch ex As Exception
                Throw New InvalidCastException("Operator '+' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
            End Try

            Return String.Concat(VBConvert.ToString(Left), VBConvert.ToString(Right))

        End Function

        Friend Shared Function InvokeBinaryOperator(ByVal left As Object, ByVal right As Object, ByVal operation As String, ByRef ret As Object) As Boolean
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
            Return Conversions.ToBoolean(CompareObjectEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return Conversions.ToBoolean(CompareObjectGreater(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return Conversions.ToBoolean(CompareObjectGreaterEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return Conversions.ToBoolean(CompareObjectLess(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return Conversions.ToBoolean(CompareObjectLessEqual(Left, Right, TextCompare))
        End Function

        Public Shared Function ConditionalCompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return Conversions.ToBoolean(CompareObjectNotEqual(Left, Right, TextCompare))
        End Function

        Private Shared Function DivideObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_Division", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function IntDivideObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_IntegerDivision", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function MultiplyObjects(ByVal Left As Object, ByVal Right As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(Left, Right, "op_Multiply", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function ModObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_Modulus", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Public Shared Function DivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Return Double.NaN
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Right = CreateNullObjectType(Left)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpDivide(Left, Right)
            Try
                Select Case destTc
                    Case TypeCode.Decimal
                        Return VBConvert.ToDecimal(Left) / VBConvert.ToDecimal(Right)
                    Case TypeCode.Double
                        Return VBConvert.ToDouble(Left) / VBConvert.ToDouble(Right)
                    Case TypeCode.Single
                        Return VBConvert.ToSingle(Left) / VBConvert.ToSingle(Right)
                End Select
                Return DivideObjects(Left, Right)
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '/' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
        End Function

        Public Shared Function ExponentObject(ByVal Left As Object, ByVal Right As Object) As Object
            Dim exp As Double = VBConvert.ToDouble(PlusObject(Right))
            Dim base As Double = VBConvert.ToDouble(PlusObject(Left))

            Return Math.Pow(base, exp)
        End Function

        Private Shared Function IsUnsignNum(ByVal obj As Object) As Boolean
            Dim tc As TypeCode = GetTypeCode(obj)
            Select Case tc
                Case TypeCode.Byte
                    Return True
                Case TypeCode.UInt16
                    Return True
                Case TypeCode.UInt32
                    Return True
                Case TypeCode.UInt64
                    Return True
            End Select
            Return False
        End Function


        Public Shared Function IntDivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Return Double.NaN
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Right = CreateNullObjectType(Left)
            End If

            Dim resUnsign As Boolean = IsUnsignNum(Left) Or IsUnsignNum(Right)
            Dim tc As TypeCode = GetTypeCode(Left)
            If (tc = TypeCode.Boolean) Then
                If (VBConvert.ToBoolean(Left)) Then
                    Left = -1
                Else
                    Left = 0
                End If
            End If

            Dim destTc As TypeCode = DestTypeCodeOpIntDivide(Left, Right)

            Try
                Select Case destTc
                    Case TypeCode.Byte
                        Return VBConvert.ToByte(Left) \ VBConvert.ToByte(Right)
                    Case TypeCode.Int16
                        Return VBConvert.ToInt16(Left) \ VBConvert.ToInt16(Right)
                    Case TypeCode.Int32
                        Return VBConvert.ToInt32(Left) \ VBConvert.ToInt32(Right)
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Left) \ VBConvert.ToInt64(Right)
                    Case TypeCode.SByte
                        Return VBConvert.ToSByte(Left) \ VBConvert.ToSByte(Right)
                    Case TypeCode.UInt16
                        Return VBConvert.ToUInt16(Left) \ VBConvert.ToUInt16(Right)
                    Case TypeCode.UInt32
                        Return VBConvert.ToUInt32(Left) \ VBConvert.ToUInt32(Right)
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Left) \ VBConvert.ToUInt64(Right)
                End Select
                Return IntDivideObjects(Left, Right)
            Catch ex As Exception
                If (TypeOf ex Is DivideByZeroException) Then
                    Throw ex
                End If
            End Try
            Throw New InvalidCastException("Operator '\' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")

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
                        If (VBConvert.ToBoolean(Operand)) Then
                            Return -1S << intAmount
                        End If
                        Return 0
                    Case TypeCode.Byte
                        Return VBConvert.ToByte(Operand) << intAmount
                    Case TypeCode.Decimal
                        Return VBConvert.ToInt64(VBConvert.ToDecimal(Operand)) << intAmount
                    Case TypeCode.Double
                        Return VBConvert.ToInt64(VBConvert.ToDouble(Operand)) << intAmount
                    Case TypeCode.Int16
                        Return VBConvert.ToInt16(Operand) << intAmount
                    Case TypeCode.Int32
                        Return VBConvert.ToInt32(Operand) << intAmount
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Operand) << intAmount
                    Case TypeCode.SByte
                        Return VBConvert.ToSByte(Operand) << intAmount
                    Case TypeCode.String
                        Return VBConvert.ToInt64(VBConvert.ToString(Operand)) << intAmount
                    Case TypeCode.Single
                        Return VBConvert.ToInt64(VBConvert.ToSingle(Operand)) << intAmount
                    Case TypeCode.UInt16
                        Return VBConvert.ToUInt16(Operand) << intAmount
                    Case TypeCode.UInt32
                        Return VBConvert.ToUInt32(Operand) << intAmount
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Operand) << intAmount

                End Select

            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '<<' is not defined for type '" + tcOperand.ToString() + "'.")
        End Function

        Public Shared Function LikeObject(ByVal Source As Object, ByVal Pattern As Object, ByVal CompareOption As CompareMethod) As Object
            Try
                Select Case GetTypeCode(Source)
                    Case TypeCode.String
                        Return LikeString(Source.ToString(), Pattern.ToString(), CompareOption)
                    Case Else
                        Dim ret As Object = Nothing
                        If (InvokeBinaryOperator(Source, Pattern, "op_Like", ret)) Then
                            Return ret
                        End If
                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator 'Like' is not defined for type '" + Source.GetType().ToString() + "' and type '" + Pattern.GetType().ToString() + "'.")
        End Function

        Public Shared Function LikeString(ByVal Source As String, ByVal Pattern As String, ByVal CompareOption As CompareMethod) As Boolean
            Return StringType.StrLike(Source, Pattern, CompareOption)
        End Function

        Public Shared Function ModObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Throw New DivideByZeroException()
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Throw New DivideByZeroException()
            End If

            Dim destTc As TypeCode = DestTypeCodeOpDivide(Left, Right)
            Try
                Select Case destTc
                    Case TypeCode.Byte
                        Return VBConvert.ToByte(Left) Mod VBConvert.ToByte(Right)
                    Case TypeCode.Int16
                        Return VBConvert.ToInt16(Left) Mod VBConvert.ToInt16(Right)
                    Case TypeCode.Int32
                        Return VBConvert.ToInt32(Left) Mod VBConvert.ToInt32(Right)
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Left) Mod VBConvert.ToInt64(Right)
                    Case TypeCode.SByte
                        Return VBConvert.ToSByte(Left) Mod VBConvert.ToSByte(Right)
                    Case TypeCode.UInt16
                        Return VBConvert.ToUInt16(Left) Mod VBConvert.ToUInt16(Right)
                    Case TypeCode.UInt32
                        Return VBConvert.ToUInt32(Left) Mod VBConvert.ToUInt32(Right)
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Left) Mod VBConvert.ToUInt64(Right)
                    Case TypeCode.Decimal
                        Return VBConvert.ToDecimal(Left) Mod VBConvert.ToDecimal(Right)
                    Case TypeCode.Double
                        Return VBConvert.ToDouble(Left) Mod VBConvert.ToDouble(Right)
                    Case TypeCode.Single
                        Return VBConvert.ToSingle(Left) Mod VBConvert.ToSingle(Right)
                End Select
                Return ModObjects(Left, Right)
            Catch ex As Exception
                If (TypeOf ex Is DivideByZeroException) Then
                    Throw ex
                End If
            End Try
            Throw New InvalidCastException("Operator 'Mod' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")
        End Function

        Private Shared Function SizeDown(ByVal num As Long, ByVal minTC As TypeCode) As Object
            While (True)
                Select Case minTC

                    Case TypeCode.Byte
                        If (num > Byte.MaxValue Or num < 0) Then
                            minTC = TypeCode.Int16
                        Else
                            Return CType(num, Byte)
                        End If

                    Case TypeCode.Int16
                        If (num > Int16.MaxValue Or num < Int16.MinValue) Then
                            minTC = TypeCode.Int32
                        Else
                            Return CType(num, Int16)
                        End If

                    Case TypeCode.Int32
                        If (num > Int32.MaxValue Or num < Int32.MinValue) Then
                            minTC = TypeCode.Int64
                        Else
                            Return CType(num, Int32)
                        End If

                    Case TypeCode.Int64
                        Return num

                    Case TypeCode.SByte
                        If (num > SByte.MaxValue Or num < SByte.MinValue) Then
                            minTC = TypeCode.Int16
                        Else
                            Return CType(num, SByte)
                        End If

                    Case TypeCode.UInt16
                        If (num > UInt16.MaxValue Or num < 0) Then
                            minTC = TypeCode.Int32
                        Else
                            Return CType(num, UInt16)
                        End If

                    Case TypeCode.UInt32
                        If (num > UInt32.MaxValue Or num < 0) Then
                            minTC = TypeCode.Int64
                        Else
                            Return CType(num, UInt32)
                        End If

                    Case Else
                        Throw New ArgumentException("minTC")

                End Select
            End While
            Return num 'will never get here
        End Function

        Friend Shared Function MultiplyAndSize(ByVal o1 As Long, ByVal o2 As Long, ByVal tc As TypeCode) As Object
            Return SizeDown(o1 * o2, tc)
        End Function
        Public Shared Function MultiplyObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Return 0
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Right = CreateNullObjectType(Left)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpMultiply(Left, Right)
            Try
                Select Case destTc
                    Case TypeCode.Byte
                        Return MultiplyAndSize(VBConvert.ToByte(Left), VBConvert.ToByte(Right), destTc)
                    Case TypeCode.Int16
                        Return MultiplyAndSize(VBConvert.ToInt16(Left), VBConvert.ToInt16(Right), destTc)
                    Case TypeCode.Int32
                        Return MultiplyAndSize(VBConvert.ToInt32(Left), VBConvert.ToInt32(Right), destTc)
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Left) * VBConvert.ToInt64(Right)
                    Case TypeCode.SByte
                        Return MultiplyAndSize(VBConvert.ToSByte(Left), VBConvert.ToSByte(Right), destTc)
                    Case TypeCode.UInt16
                        Return MultiplyAndSize(VBConvert.ToUInt16(Left), VBConvert.ToUInt16(Right), destTc)
                    Case TypeCode.UInt32
                        Return MultiplyAndSize(VBConvert.ToUInt32(Left), VBConvert.ToUInt32(Right), destTc)
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Left) * VBConvert.ToUInt64(Right)
                    Case TypeCode.Decimal
                        Return VBConvert.ToDecimal(Left) * VBConvert.ToDecimal(Right)
                    Case TypeCode.Double
                        Return VBConvert.ToDouble(Left) * VBConvert.ToDouble(Right)
                    Case TypeCode.Single
                        Return VBConvert.ToSingle(Left) * VBConvert.ToSingle(Right)
                End Select
                Return MultiplyObjects(Left, Right)
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '*' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")

        End Function

        Friend Shared Function PlusBoolean(ByVal Operand As Boolean) As Object
            If (Operand) Then
                Return -1S
            End If
            Return 0S
        End Function

        Friend Shared Function PlusString(ByVal Operand As String) As Object
            Return Double.Parse(Operand)
        End Function

        Friend Shared Function NegateBoolean(ByVal Operand As Boolean) As Object
            If (Operand) Then
                Return 1S
            End If
            Return 0S
        End Function

        Friend Shared Function NegateByte(ByVal Operand As Byte) As Object
            Return -1S * Operand
        End Function

        Friend Shared Function NegateUInt16(ByVal Operand As UShort) As Object
            Return -1 * Operand
        End Function

        Friend Shared Function NegateUInt32(ByVal Operand As UInteger) As Object
            Return -1L * Operand
        End Function

        Friend Shared Function NegateUInt64(ByVal Operand As ULong) As Object
            Return -1 * CType(Operand, Decimal)
        End Function

        Friend Shared Function NegateSByte(ByVal Operand As SByte) As Object
            If (Operand = SByte.MinValue) Then
                Return 1S + SByte.MaxValue
            End If
            Return -Operand
        End Function

        Friend Shared Function NegateDecimal(ByVal Operand As Decimal) As Object
            Return -Operand
        End Function

        Friend Shared Function NegateDouble(ByVal Operand As Double) As Object
            Return -Operand
        End Function

        Friend Shared Function NegateSingle(ByVal Operand As Single) As Object
            Return -Operand
        End Function

        Friend Shared Function NegateInt16(ByVal Operand As Int16) As Object
            If (Operand = Int16.MinValue) Then
                Return 1 + Int16.MaxValue
            End If
            Return -Operand
        End Function

        Friend Shared Function NegateInt32(ByVal Operand As Int32) As Object
            If (Operand = Int32.MinValue) Then
                Return 1L + Int32.MaxValue
            End If
            Return -Operand
        End Function

        Friend Shared Function NegateInt64(ByVal Operand As Int64) As Object
            If (Operand = Int64.MinValue) Then
                Return 1 + CType(Int64.MaxValue, ULong)
            End If
            Return -Operand
        End Function

        Friend Shared Function NegateString(ByVal Operand As String) As Object
            Dim d As Double = Double.Parse(Operand)
            Return NegateDouble(d)
        End Function

        Friend Shared Function NegateObject_(ByVal Operand As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeUnaryOperator(Operand, "op_UnaryNegation", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Friend Shared Function PlusObject_(ByVal Operand As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeUnaryOperator(Operand, "op_UnaryPlus", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Friend Shared Function RightShiftObject_(ByVal Operand As Object, ByVal Amount As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(Operand, Amount, "op_RightShift", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Friend Shared Function LeftShiftObject_(ByVal Operand As Object, ByVal Amount As Object) As Object
            Dim ret As Object = Nothing
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
                        Return NegateBoolean(VBConvert.ToBoolean(Operand))
                    Case TypeCode.Byte
                        Return NegateByte(VBConvert.ToByte(Operand))
                    Case TypeCode.Decimal
                        Return NegateDecimal(VBConvert.ToDecimal(Operand))
                    Case TypeCode.Double
                        Return NegateDouble(VBConvert.ToDouble(Operand))
                    Case TypeCode.Int16
                        Return NegateInt16(VBConvert.ToInt16(Operand))
                    Case TypeCode.Int32
                        Return NegateInt32(VBConvert.ToInt32(Operand))
                    Case TypeCode.Int64
                        Return NegateInt64(VBConvert.ToInt64(Operand))
                    Case TypeCode.SByte
                        Return NegateSByte(VBConvert.ToSByte(Operand))
                    Case TypeCode.Single
                        Return NegateSingle(VBConvert.ToSingle(Operand))
                    Case TypeCode.String
                        Return NegateString(VBConvert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return NegateUInt16(VBConvert.ToUInt16(Operand))
                    Case TypeCode.UInt32
                        Return NegateUInt32(VBConvert.ToUInt32(Operand))
                    Case TypeCode.UInt64
                        Return NegateUInt64(VBConvert.ToUInt64(Operand))
                    Case TypeCode.Object
                        Return NegateObject_(Operand)

                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '-' is not defined for type '" + tc.ToString() + "'.")
        End Function

        Friend Shared Function NotObject_(ByVal Operand As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeUnaryOperator(Operand, "op_OnesComplement", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Friend Shared Function NotString(ByVal Operand As String) As Object
            Return Not (CType((VBConvert.ToDouble(Operand)), Long))
        End Function


        Public Shared Function NotObject(ByVal Operand As Object) As Object
            If (Operand Is Nothing) Then
                Return -1
            End If

            Dim tc As TypeCode = GetTypeCode(Operand)
            Try
                Select Case tc
                    Case TypeCode.Boolean
                        Return Not VBConvert.ToBoolean(Operand)
                    Case TypeCode.Byte
                        Return Not VBConvert.ToByte(Operand)
                    Case TypeCode.Decimal
                        Return Not (CType((VBConvert.ToDecimal(Operand)), Long))
                    Case TypeCode.Double
                        Return Not (CType((VBConvert.ToDouble(Operand)), Long))
                    Case TypeCode.Int16
                        Return Not VBConvert.ToInt16(Operand)
                    Case TypeCode.Int32
                        Return Not VBConvert.ToInt32(Operand)
                    Case TypeCode.Int64
                        Return Not VBConvert.ToInt64(Operand)
                    Case TypeCode.SByte
                        Return Not VBConvert.ToSByte(Operand)
                    Case TypeCode.Single
                        Return Not (CType((VBConvert.ToSingle(Operand)), Long))
                    Case TypeCode.String
                        Return NotString(VBConvert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return Not VBConvert.ToUInt16(Operand)
                    Case TypeCode.UInt32
                        Return Not VBConvert.ToUInt32(Operand)
                    Case TypeCode.UInt64
                        Return Not VBConvert.ToUInt64(Operand)
                    Case TypeCode.Object
                        Return NotObject_(Operand)

                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator 'Not' is not defined for type '" + tc.ToString() + "'.")
        End Function

        Private Shared Function OrObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_BitwiseOr", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function XorObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_ExclusiveOr", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function AndObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_BitwiseAnd", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function GetAsLong(ByVal obj As Object) As Long
            Dim tc As TypeCode = GetTypeCode(obj)
            Select Case tc
                Case TypeCode.Boolean
                    If (VBConvert.ToBoolean(obj)) Then
                        Return -1L
                    End If
                    Return 0L
                Case TypeCode.Byte
                    Return CType(VBConvert.ToByte(obj), Long)
                Case TypeCode.Int16
                    Return CType(VBConvert.ToInt16(obj), Long)
                Case TypeCode.Int32
                    Return CType(VBConvert.ToInt32(obj), Long)
                Case TypeCode.Int64
                    Return VBConvert.ToInt64(obj)
                Case TypeCode.SByte
                    Return CType(VBConvert.ToSByte(obj), Long)
                Case TypeCode.String
                    Return CType(Math.Round(VBConvert.ToDouble(VBConvert.ToString(obj))), Long)
                Case TypeCode.Double
                    Return CType(Math.Round(VBConvert.ToDouble(obj)), Long)
                Case TypeCode.Decimal
                    Return CType(Math.Round(VBConvert.ToDecimal(obj)), Long)
                Case TypeCode.Single
                    Return CType(Math.Round(VBConvert.ToSingle(obj)), Long)
                Case TypeCode.UInt16
                    Return CType(VBConvert.ToUInt16(obj), Long)
                Case TypeCode.UInt32
                    Return CType(VBConvert.ToUInt32(obj), Long)
                Case TypeCode.UInt64
                    Return CType(VBConvert.ToUInt64(obj), Long)
            End Select
        End Function

        Public Shared Function OrObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return BitWiseOpObject(Left, Right, New OrHandler())
        End Function

        Friend Class OrHandler
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
                Return DoBitWiseOp(CType(VBConvert.ToDouble(o1), Long), CType(VBConvert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "Or"
            End Function
        End Class

        Friend Interface BitWiseOpHandler
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

        Friend Shared Function BitWiseOpObject(ByVal o1 As Object, ByVal o2 As Object, ByVal opHandler As BitWiseOpHandler) As Object
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
                        Return opHandler.DoBitWiseOp(VBConvert.ToBoolean(o1), VBConvert.ToBoolean(o2))
                    Case TypeCode.Byte
                        Return opHandler.DoBitWiseOp(VBConvert.ToByte(o1), VBConvert.ToByte(o2))
                    Case TypeCode.Int16
                        Return opHandler.DoBitWiseOp(VBConvert.ToInt16(o1), VBConvert.ToInt16(o2))
                    Case TypeCode.Int32
                        Return opHandler.DoBitWiseOp(VBConvert.ToInt32(o1), VBConvert.ToInt32(o2))
                    Case TypeCode.Int64
                        Return opHandler.DoBitWiseOp(GetAsLong(o1), GetAsLong(o2))
                    Case TypeCode.SByte
                        Return opHandler.DoBitWiseOp(VBConvert.ToSByte(o1), VBConvert.ToSByte(o2))
                    Case TypeCode.String
                        Return opHandler.DoBitWiseOp(VBConvert.ToString(o1), VBConvert.ToString(o2))
                    Case TypeCode.UInt16
                        Return opHandler.DoBitWiseOp(VBConvert.ToUInt16(o1), VBConvert.ToUInt16(o2))
                    Case TypeCode.UInt32
                        Return opHandler.DoBitWiseOp(VBConvert.ToUInt32(o1), VBConvert.ToUInt32(o2))
                    Case TypeCode.UInt64
                        Return opHandler.DoBitWiseOp(VBConvert.ToUInt64(o1), VBConvert.ToUInt64(o2))

                End Select
                Return opHandler.DoBitWiseOp(o1, o2)
            Catch ex As Exception
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
                        Return PlusBoolean(VBConvert.ToBoolean(Operand))
                    Case TypeCode.Byte
                        Return VBConvert.ToByte(Operand)
                    Case TypeCode.Decimal
                        Return VBConvert.ToDecimal(Operand)
                    Case TypeCode.Double
                        Return VBConvert.ToDouble(Operand)
                    Case TypeCode.Int16
                        Return VBConvert.ToInt16(Operand)
                    Case TypeCode.Int32
                        Return VBConvert.ToInt32(Operand)
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Operand)
                    Case TypeCode.SByte
                        Return VBConvert.ToSByte(Operand)
                    Case TypeCode.Single
                        Return VBConvert.ToSingle(Operand)
                    Case TypeCode.String
                        Return PlusString(VBConvert.ToString(Operand))
                    Case TypeCode.UInt16
                        Return VBConvert.ToUInt16(Operand)
                    Case TypeCode.UInt32
                        Return VBConvert.ToUInt32(Operand)
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Operand)
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
                    Return VBConvert.ToInt32(VBConvert.ToByte(Amount))
                Case TypeCode.Decimal
                    Return VBConvert.ToInt32(VBConvert.ToDecimal(Amount))
                Case TypeCode.Double
                    Return VBConvert.ToInt32(VBConvert.ToDouble(Amount))
                Case TypeCode.Int16
                    Return VBConvert.ToInt32(VBConvert.ToInt16(Amount))
                Case TypeCode.Int32
                    Return VBConvert.ToInt32(VBConvert.ToInt32(Amount))
                Case TypeCode.Int64
                    Return VBConvert.ToInt32(VBConvert.ToInt64(Amount))
                Case TypeCode.SByte
                    Return VBConvert.ToInt32(VBConvert.ToSByte(Amount))
                Case TypeCode.String
                    Return VBConvert.ToInt32(VBConvert.ToString(Amount))
                Case TypeCode.Single
                    Return VBConvert.ToInt32(VBConvert.ToSingle(Amount))
                Case TypeCode.UInt16
                    Return VBConvert.ToInt32(VBConvert.ToUInt16(Amount))
                Case TypeCode.UInt32
                    Return VBConvert.ToInt32(VBConvert.ToUInt32(Amount))
                Case TypeCode.UInt64
                    Return VBConvert.ToInt32(VBConvert.ToUInt64(Amount))

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
                        If (VBConvert.ToBoolean(Operand)) Then
                            Return -1S >> intAmount
                        End If
                        Return 0
                    Case TypeCode.Byte
                        Return VBConvert.ToByte(Operand) >> intAmount
                    Case TypeCode.Decimal
                        Return VBConvert.ToInt64(VBConvert.ToDecimal(Operand)) >> intAmount
                    Case TypeCode.Double
                        Return VBConvert.ToInt64(VBConvert.ToDouble(Operand)) >> intAmount
                    Case TypeCode.Int16
                        Return VBConvert.ToInt16(Operand) >> intAmount
                    Case TypeCode.Int32
                        Return VBConvert.ToInt32(Operand) >> intAmount
                    Case TypeCode.Int64
                        Return VBConvert.ToInt64(Operand) >> intAmount
                    Case TypeCode.SByte
                        Return VBConvert.ToSByte(Operand) >> intAmount
                    Case TypeCode.String
                        Return VBConvert.ToInt64(VBConvert.ToString(Operand)) >> intAmount
                    Case TypeCode.Single
                        Return VBConvert.ToInt64(VBConvert.ToSingle(Operand)) >> intAmount
                    Case TypeCode.UInt16
                        Return VBConvert.ToUInt16(Operand) >> intAmount
                    Case TypeCode.UInt32
                        Return VBConvert.ToUInt32(Operand) >> intAmount
                    Case TypeCode.UInt64
                        Return VBConvert.ToUInt64(Operand) >> intAmount

                End Select

            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '>>' is not defined for type '" + tcOperand.ToString() + "'.")
        End Function

        Private Shared Function SubtractBooleans(ByVal o1 As Boolean, ByVal o2 As Boolean) As Object
            Dim ret As Short = 0
            If (o1) Then
                ret = -1S
            End If
            If (o2) Then
                ret = ret + 1S
            End If
            Return ret
        End Function

        Private Shared Function SubtractBytes(ByVal o1 As Byte, ByVal o2 As Byte) As Object
            Dim s As Short = CType(o1, Short) - o2
            If (s < 0) Then
                Return s
            End If
            Return CType(s, Byte)
        End Function

        Private Shared Function SubtractDecimals(ByVal o1 As Decimal, ByVal o2 As Decimal) As Object
            Return o1 - o2
        End Function

        Private Shared Function SubtractDoubles(ByVal o1 As Double, ByVal o2 As Double) As Object
            Return o1 - o2
        End Function

        Private Shared Function SubtractInt16s(ByVal o1 As Short, ByVal o2 As Short) As Object
            Dim int As Integer = CType(o1, Integer) - o2
            If (int > Short.MaxValue) Or (int < Short.MinValue) Then
                Return int
            End If
            Return CType(int, Short)
        End Function

        Private Shared Function SubtractInt32s(ByVal o1 As Integer, ByVal o2 As Integer) As Object
            Dim l As Long = CType(o1, Long) - o2
            If (l > Integer.MaxValue) Or (l < Integer.MinValue) Then
                Return l
            End If
            Return CType(l, Integer)
        End Function

        Private Shared Function SubtractInt64s(ByVal o1 As Long, ByVal o2 As Long) As Object
            Return o1 - o2
        End Function

        Private Shared Function SubtractObjects(ByVal o1 As Object, ByVal o2 As Object) As Object
            Dim ret As Object = Nothing
            If Not (InvokeBinaryOperator(o1, o2, "op_Subtraction", ret)) Then
                Throw New InvalidOperationException()
            End If
            Return ret
        End Function

        Private Shared Function SubtractSBytes(ByVal o1 As SByte, ByVal o2 As SByte) As Object
            Dim s As Short = CType(o1, Short) - o2
            If (s > SByte.MaxValue) Or (s < SByte.MinValue) Then
                Return s
            End If
            Return CType(s, SByte)
        End Function

        Private Shared Function SubtractSingles(ByVal o1 As Single, ByVal o2 As Single) As Object
            Return o1 - o2
        End Function

        Private Shared Function SubtractUInt16s(ByVal o1 As UShort, ByVal o2 As UShort) As Object
            Dim int As Integer = CType(o1, Integer) - o2
            If (int < 0) Then
                Return int
            End If
            Return CType(int, UShort)
        End Function

        Private Shared Function SubtractUInt32s(ByVal o1 As UInteger, ByVal o2 As UInteger) As Object
            Dim l As Long = CType(o1, Long) - o2
            If (l < 0) Then
                Return l
            End If
            Return CType(l, UInteger)
        End Function

        Private Shared Function SubtractUInt64s(ByVal o1 As ULong, ByVal o2 As ULong) As Object
            Return o1 - o2
        End Function

        Private Shared Function SubtractDateTime(ByVal o1 As DateTime, ByVal o2 As DateTime) As Object
            Return New TimeSpan(CType(o1, DateTime).Ticks - CType(o2, DateTime).Ticks)
        End Function

        Public Shared Function SubtractObject(ByVal Left As Object, ByVal Right As Object) As Object
            If (Left Is Nothing) And (Right Is Nothing) Then
                Return 0
            End If
            If (Left Is Nothing) Then
                Left = CreateNullObjectType(Right)
            End If
            If (Right Is Nothing) Then
                Right = CreateNullObjectType(Left)
            End If

            Dim destTc As TypeCode = DestTypeCodeOpSubtract(Left, Right)
            Try
                Select Case destTc
                    Case TypeCode.Boolean
                        Return SubtractBooleans(VBConvert.ToBoolean(Left), VBConvert.ToBoolean(Right))
                    Case TypeCode.Byte
                        Return SubtractBytes(VBConvert.ToByte(Left), VBConvert.ToByte(Right))
                    Case TypeCode.Decimal
                        Return SubtractDecimals(VBConvert.ToDecimal(Left), VBConvert.ToDecimal(Right))
                    Case TypeCode.Double
                        Return SubtractDoubles(VBConvert.ToDouble(Left), VBConvert.ToDouble(Right))
                    Case TypeCode.Int16
                        Return SubtractInt16s(VBConvert.ToInt16(Left), VBConvert.ToInt16(Right))
                    Case TypeCode.Int32
                        Return SubtractInt32s(VBConvert.ToInt32(Left), VBConvert.ToInt32(Right))
                    Case TypeCode.Int64
                        Return SubtractInt64s(VBConvert.ToInt64(Left), VBConvert.ToInt64(Right))
                    Case TypeCode.SByte
                        Return SubtractSBytes(VBConvert.ToSByte(Left), VBConvert.ToSByte(Right))
                    Case TypeCode.Single
                        Return SubtractSingles(VBConvert.ToSingle(Left), VBConvert.ToSingle(Right))
                    Case TypeCode.UInt16
                        Return SubtractUInt16s(VBConvert.ToUInt16(Left), VBConvert.ToUInt16(Right))
                    Case TypeCode.UInt32
                        Return SubtractUInt32s(VBConvert.ToUInt32(Left), VBConvert.ToUInt32(Right))
                    Case TypeCode.UInt64
                        Return SubtractUInt64s(VBConvert.ToUInt64(Left), VBConvert.ToUInt64(Right))
                    Case TypeCode.DateTime
                        Return SubtractDateTime(VBConvert.ToDateTime(Left), VBConvert.ToDateTime(Right))

                End Select
                Return SubtractObjects(Left, Right)
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Operator '-' is not defined for type '" + GetTypeCode(Left).ToString() + "' and type '" + GetTypeCode(Right).ToString() + "'.")

        End Function

        Public Shared Function XorObject(ByVal Left As Object, ByVal Right As Object) As Object
            Return BitWiseOpObject(Left, Right, New XorHandler())
        End Function

        Friend Class XorHandler
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
                Return DoBitWiseOp(CType(VBConvert.ToDouble(o1), Long), CType(VBConvert.ToDouble(o2), Long))
            End Function

            Public Function GetOpName() As String Implements BitWiseOpHandler.GetOpName
                Return "Xor"
            End Function
        End Class

        Friend Class VBConvert
            Public Shared Function ToBoolean(ByVal obj As Object) As Boolean
                Return Convert.ToBoolean(obj)
            End Function

            Public Shared Function ToByte(ByVal obj As Object) As Byte
                Return Convert.ToByte(obj)
            End Function

            Public Shared Function ToSByte(ByVal obj As Object) As SByte
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToSByte(obj)
            End Function

            Public Shared Function ToInt16(ByVal obj As Object) As Short
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToInt16(obj)
            End Function

            Public Shared Function ToUInt16(ByVal obj As Object) As UShort
                Return Convert.ToUInt16(obj)
            End Function

            Public Shared Function ToInt32(ByVal obj As Object) As Integer
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToInt32(obj)
            End Function

            Public Shared Function ToUInt32(ByVal obj As Object) As UInteger
                Return Convert.ToUInt32(obj)
            End Function

            Public Shared Function ToInt64(ByVal obj As Object) As Long
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToInt64(obj)
            End Function

            Public Shared Function ToUInt64(ByVal obj As Object) As ULong
                Return Convert.ToUInt64(obj)
            End Function

            Public Shared Function ToSingle(ByVal obj As Object) As Single
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToSingle(obj)
            End Function

            Public Shared Function ToDouble(ByVal obj As Object) As Double
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToDouble(obj)
            End Function

            Public Shared Function ToDecimal(ByVal obj As Object) As Decimal
                If (GetTypeCode(obj) = TypeCode.Boolean) Then
                    If (Conversions.ToBoolean(obj)) Then
                        Return -1
                    End If
                    Return 0
                End If
                Return Convert.ToDecimal(obj)
            End Function

            Public Shared Function ToChar(ByVal obj As Object) As Char
                Return Convert.ToChar(obj)
            End Function

            Public Shared Shadows Function ToString(ByVal obj As Object) As String
                Return Convert.ToString(obj)
            End Function

            Public Shared Function ToDateTime(ByVal obj As Object) As DateTime
                Return Convert.ToDateTime(obj)
            End Function
        End Class

        Private Sub New()

        End Sub
    End Class
End Namespace
