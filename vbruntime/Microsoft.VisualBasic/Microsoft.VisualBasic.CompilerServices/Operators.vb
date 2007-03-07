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

        Public Shared Function AddObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Public Shared Function AndObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

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

        Public Shared Function ConcatenateObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Public Shared Function ConditionalCompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
            Return CBool(CompareObjectEqual(Left, Right, TextCompare))
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
            Throw New NotImplementedException
        End Function
        Public Shared Function IntDivideObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LeftShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            Throw New NotImplementedException
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
        Public Shared Function NegateObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function NotObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function OrObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function PlusObject(ByVal Operand As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function RightShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function SubtractObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function XorObject(ByVal Left As Object, ByVal Right As Object) As Object
            Throw New NotImplementedException
        End Function

        Private Sub New()

        End Sub
    End Class
End Namespace
#End If