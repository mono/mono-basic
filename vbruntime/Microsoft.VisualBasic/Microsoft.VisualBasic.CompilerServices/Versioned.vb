'
' Versioned.vb (needed by vbc 2.0)
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
'StackOverflow WARNING: 
' When vbc2.0 compiles Interaction.CallByName, Information.IsNumeric, Information.SystemTypeName, 
' Information.TypeName and Information.VbTypeName.
' It compiles them into corresponding Versioned.foo functions.
Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class Versioned
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function CallByName(ByVal Instance As Object, ByVal MethodName As String, ByVal UseCallType As CallType, ByVal ParamArray Arguments As Object()) As Object
            Return Interaction.CallByName(Instance, MethodName, UseCallType, Arguments)
        End Function
        Public Shared Function IsNumeric(ByVal Expression As Object) As Boolean

            If Expression Is Nothing Then Return False

            Select Case Type.GetTypeCode(Expression.GetType)
                Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Double, TypeCode.Single, TypeCode.Decimal, TypeCode.Boolean
                    Return True
                Case TypeCode.DateTime
                    Return False
                Case TypeCode.String
                    Return Double.TryParse(DirectCast(Expression, String), 0)
                Case TypeCode.Char
                    Return Double.TryParse(DirectCast(Expression, Char).ToString(), 0)
                Case Else
                    Return False
            End Select
        End Function
        Public Shared Function SystemTypeName(ByVal VbName As String) As String
            Dim lower As String = VbName.ToLower()
            If String.Equals(lower, "boolean") Then
                Return "System.Boolean"
            ElseIf String.Equals(lower, "byte") Then
                Return "System.Byte"
            ElseIf String.Equals(lower, "sbyte") Then
                Return "System.SByte"
            ElseIf String.Equals(lower, "char") Then
                Return "System.Char"
            ElseIf String.Equals(lower, "date") Then
                Return "System.DateTime"
            ElseIf String.Equals(lower, "decimal") Then
                Return "System.Decimal"
            ElseIf String.Equals(lower, "double") Then
                Return "System.Double"
            ElseIf String.Equals(lower, "integer") Then
                Return "System.Int32"
            ElseIf String.Equals(lower, "uinteger") Then
                Return "System.UInt32"
            ElseIf String.Equals(lower, "long") Then
                Return "System.Int64"
            ElseIf String.Equals(lower, "ulong") Then
                Return "System.UInt64"
            ElseIf String.Equals(lower, "object") Then
                Return "System.Object"
            ElseIf String.Equals(lower, "short") Then
                Return "System.Int16"
            ElseIf String.Equals(lower, "ushort") Then
                Return "System.UInt16"
            ElseIf String.Equals(lower, "single") Then
                Return "System.Single"
            ElseIf String.Equals(lower, "string") Then
                Return "System.String"
            Else
                Return Nothing
            End If
        End Function
        Public Shared Function TypeName(ByVal Expression As Object) As String

            Dim TmpObjType As String
            Dim RetObjType As String
            Dim ArrCh As String

            If Expression Is Nothing Then Return "Nothing"
            If TypeOf Expression Is DBNull Then Return "DBNull"

            If Expression.GetType.IsArray Then
                ArrCh = "()"
            Else
                ArrCh = ""
            End If
            TmpObjType = Expression.GetType().Name.ToLower

            If String.Equals(TmpObjType, "string") Then
                RetObjType = "String"
            ElseIf String.Equals(TmpObjType, "int32") Then
                RetObjType = "Integer"
            ElseIf String.Equals(TmpObjType, "uint32") Then
                RetObjType = "UInteger"
            ElseIf String.Equals(TmpObjType, "int16") Then
                RetObjType = "Short"
            ElseIf String.Equals(TmpObjType, "uint16") Then
                RetObjType = "UShort"
            ElseIf String.Equals(TmpObjType, "int64") Then
                RetObjType = "Long"
            ElseIf String.Equals(TmpObjType, "uint64") Then
                RetObjType = "ULong"
            ElseIf String.Equals(TmpObjType, "byte") Then
                RetObjType = "Byte"
            ElseIf String.Equals(TmpObjType, "sbyte") Then
                RetObjType = "SByte"
            ElseIf String.Equals(TmpObjType, "boolean") Then
                RetObjType = "Boolean"
            ElseIf String.Equals(TmpObjType, "char") Then
                RetObjType = "Char"
            ElseIf String.Equals(TmpObjType, "datetime") Then
                RetObjType = "Date"
            ElseIf String.Equals(TmpObjType, "single") Then
                RetObjType = "Single"
            ElseIf String.Equals(TmpObjType, "double") Then
                RetObjType = "Double"
            ElseIf String.Equals(TmpObjType, "decimal") Then
                RetObjType = "Decimal"
            ElseIf String.Equals(TmpObjType, "object") Then
                RetObjType = "Object"
            Else
                RetObjType = TmpObjType
            End If

            Return (RetObjType + ArrCh)

        End Function
        Public Shared Function VbTypeName(ByVal SystemName As String) As String

            Dim tmpStr As String
            Dim RetObjType As String


            tmpStr = SystemName.ToLower
            If SystemName.ToLower.StartsWith("system.") Then tmpStr = SystemName.ToLower.Substring(7)

            If String.Equals(tmpStr, "string") Then
                RetObjType = "String"
            ElseIf String.Equals(tmpStr, "int32") Then
                RetObjType = "Integer"
            ElseIf String.Equals(tmpStr, "uint32") Then
                RetObjType = "UInteger"
            ElseIf String.Equals(tmpStr, "int16") Then
                RetObjType = "Short"
            ElseIf String.Equals(tmpStr, "uint16") Then
                RetObjType = "UShort"
            ElseIf String.Equals(tmpStr, "int64") Then
                RetObjType = "Long"
            ElseIf String.Equals(tmpStr, "uint64") Then
                RetObjType = "ULong"
            ElseIf String.Equals(tmpStr, "byte") Then
                RetObjType = "Byte"
            ElseIf String.Equals(tmpStr, "sbyte") Then
                RetObjType = "SByte"
            ElseIf String.Equals(tmpStr, "boolean") Then
                RetObjType = "Boolean"
            ElseIf String.Equals(tmpStr, "char") Then
                RetObjType = "Char"
            ElseIf String.Equals(tmpStr, "datetime") Then
                RetObjType = "Date"
            ElseIf String.Equals(tmpStr, "single") Then
                RetObjType = "Single"
            ElseIf String.Equals(tmpStr, "double") Then
                RetObjType = "Double"
            ElseIf String.Equals(tmpStr, "decimal") Then
                RetObjType = "Decimal"
            ElseIf String.Equals(tmpStr, "object") Then
                RetObjType = "Object"
            Else
                RetObjType = tmpStr
            End If

            Return RetObjType

        End Function
    End Class
End Namespace
