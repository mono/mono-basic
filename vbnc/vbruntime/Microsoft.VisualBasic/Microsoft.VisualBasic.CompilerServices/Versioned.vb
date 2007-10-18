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
#If NET_VER >= 2.0 Then
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
                    Return Double.TryParse(DirectCast(Expression, Char), 0)
                Case Else
                    Return False
            End Select
        End Function
        Public Shared Function SystemTypeName(ByVal VbName As String) As String

            Select Case VbName.ToLower()
                Case "boolean"
                    Return "System.Boolean"
                Case "byte"
                    Return "System.Byte"
                Case "sbyte"
                    Return "System.SByte"
                Case "char"
                    Return "System.Char"
                Case "date"
                    Return "System.DateTime"
                Case "decimal"
                    Return "System.Decimal"
                Case "double"
                    Return "System.Double"
                Case "integer"
                    Return "System.Int32"
                Case "uinteger"
                    Return "System.UInt32"
                Case "long"
                    Return "System.Int64"
                Case "ulong"
                    Return "System.UInt64"
                Case "object"
                    Return "System.Object"
                Case "short"
                    Return "System.Int16"
                Case "ushort"
                    Return "System.UInt16"
                Case "single"
                    Return "System.Single"
                Case "string"
                    Return "System.String"
                Case Else
                    Return Nothing
            End Select

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

            Select Case TmpObjType
                Case "string"
                    RetObjType = "String"
                Case "int32"
                    RetObjType = "Integer"
                Case "uint32"
                    RetObjType = "UInteger"
                Case "int16"
                    RetObjType = "Short"
                Case "uint16"
                    RetObjType = "UShort"
                Case "int64"
                    RetObjType = "Long"
                Case "uint64"
                    RetObjType = "ULong"
                Case "byte"
                    RetObjType = "Byte"
                Case "sbyte"
                    RetObjType = "SByte"
                Case "boolean"
                    RetObjType = "Boolean"
                Case "char"
                    RetObjType = "Char"
                Case "datetime"
                    RetObjType = "Date"
                Case "single"
                    RetObjType = "Single"
                Case "double"
                    RetObjType = "Double"
                Case "decimal"
                    RetObjType = "Decimal"
                Case "object"
                    RetObjType = "Object"
                Case Else
                    RetObjType = TmpObjType
            End Select

            Return (RetObjType + ArrCh)

        End Function
        Public Shared Function VbTypeName(ByVal SystemName As String) As String

            Dim tmpStr As String
            Dim RetObjType As String


            tmpStr = SystemName.ToLower
            If SystemName.ToLower.StartsWith("system.") Then tmpStr = SystemName.ToLower.Substring(7)

            Select Case tmpStr
                Case "string"
                    RetObjType = "String"
                Case "int32"
                    RetObjType = "Integer"
                Case "uint32"
                    RetObjType = "UInteger"
                Case "int16"
                    RetObjType = "Short"
                Case "uint16"
                    RetObjType = "UShort"
                Case "int64"
                    RetObjType = "Long"
                Case "uint64"
                    RetObjType = "ULong"
                Case "byte"
                    RetObjType = "Byte"
                Case "sbyte"
                    RetObjType = "SByte"
                Case "boolean"
                    RetObjType = "Boolean"
                Case "char"
                    RetObjType = "Char"
                Case "datetime"
                    RetObjType = "Date"
                Case "single"
                    RetObjType = "Single"
                Case "double"
                    RetObjType = "Double"
                Case "decimal"
                    RetObjType = "Decimal"
                Case "object"
                    RetObjType = "Object"
                Case Else
                    RetObjType = tmpStr
            End Select

            Return RetObjType

        End Function
    End Class
End Namespace
#End If