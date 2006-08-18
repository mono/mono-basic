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
#If NET_2_0 Then
Imports System
'StackOverflow WARNING: 
' When vbc2.0 compiles Interaction.CallByName, Information.IsNumeric, Information.SystemTypeName, 
' Information.TypeName and Information.VbTypeName.
' It compiles them into coaresponding Versioned.foo functions.
Namespace Microsoft.VisualBasic.CompilerServices
    Public Class Versioned
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function CallByName(ByVal Instance As Object, ByVal MethodName As String, ByVal UseCallType As CallType, ByVal Arguments As Object()) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function IsNumeric(ByVal Expression As Object) As Boolean

            If (TypeOf Expression Is Short) Or (TypeOf Expression Is Integer) Or (TypeOf Expression Is Long) _
                Or (TypeOf Expression Is Decimal) Or (TypeOf Expression Is Single) Or (TypeOf Expression Is Double) _
                Or (TypeOf Expression Is Boolean) Then Return True

            Try
                Dim tempStr As String
                tempStr = CStr(Expression)
                Convert.ToDouble(tempStr)
            Catch ex As Exception
                Return False
            End Try

        End Function
        Public Shared Function SystemTypeName(ByVal VbName As String) As String

            'FIXME: do we need to support the new Data types? (for example unsigned data types)

            Select Case VbName.ToLower()
                Case "boolean"
                    Return "System.Boolean"
                Case "byte"
                    Return "System.Byte"
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
                Case "long"
                    Return "System.Int64"
                Case "object"
                    Return "System.Object"
                Case "short"
                    Return "System.Int16"
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

            'FIXME: do we need to support the new Data types? (for example unsigned data types)

            Select Case TmpObjType
                Case "string"
                    RetObjType = "String"
                Case "int32"
                    RetObjType = "Integer"
                Case "int16"
                    RetObjType = "Short"
                Case "int64"
                    RetObjType = "Long"
                Case "byte"
                    RetObjType = "Byte"
                Case "boolean"
                    RetObjType = "Boolean"
                Case "char"
                    RetObjType = "Char"
                Case "datetime"
                    RetObjType = "Date"
                Case "single"
                    RetObjType = "Single"
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

            'FIXME: do we need to support the new Data types? (for example unsigned data types)

            Select Case tmpStr
                Case "string"
                    RetObjType = "String"
                Case "int32"
                    RetObjType = "Integer"
                Case "int16"
                    RetObjType = "Short"
                Case "int64"
                    RetObjType = "Long"
                Case "byte"
                    RetObjType = "Byte"
                Case "boolean"
                    RetObjType = "Boolean"
                Case "char"
                    RetObjType = "Char"
                Case "datetime"
                    RetObjType = "Date"
                Case "single"
                    RetObjType = "Single"
                Case "object"
                    RetObjType = "Object"
                Case Else
                    RetObjType = Nothing
            End Select

            Return RetObjType

        End Function
    End Class
End Namespace
#End If