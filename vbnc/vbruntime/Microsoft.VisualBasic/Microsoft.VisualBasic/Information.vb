'
' Information.vb
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
Imports Microsoft.VisualBasic.CompilerServices
Imports System.ComponentModel

Namespace Microsoft.VisualBasic
    Public Module Information
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Erl() As Integer
            Return Err.Erl
        End Function
        Public Function Err() As Microsoft.VisualBasic.ErrObject
            ' VB Err keyword is compiled into ErrObject which is stored at ProjectData.
            ' ProjectData is a singelton for all VB library.
            Dim pd As CompilerServices.ProjectData
            pd = CompilerServices.ProjectData.Instance

            Return pd.ProjectError
        End Function
        Public Function IsArray(ByVal VarName As Object) As Boolean
            If VarName Is Nothing Then
                Return False
            End If
            If TypeOf VarName Is Array Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function IsDate(ByVal Expression As Object) As Boolean
            If Expression Is Nothing Then Return False
            If TypeOf Expression Is Date Then Return True
            If TypeOf Expression Is String Then
                Try
                    Convert.ToDateTime(Expression)
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            End If
        End Function
        Public Function IsDBNull(ByVal Expression As Object) As Boolean
            If Expression Is Nothing Then
                Return False
            End If
            If TypeOf Expression Is DBNull Then
                Return True
            Else
                Return False
            End If

        End Function
        Public Function IsError(ByVal Expression As Object) As Boolean
            If TypeOf Expression Is System.Exception Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function IsNothing(ByVal Expression As Object) As Boolean
            If Expression Is Nothing Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function IsNumeric(ByVal Expression As Object) As Boolean

            If Expression Is Nothing Then Return False

            Select Case Type.GetTypeCode(Expression.GetType)
                Case TypeCode.Byte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Double, TypeCode.Single, TypeCode.Decimal, TypeCode.Boolean
                    Return True
                Case TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.SByte
                    Return False
                Case TypeCode.DateTime
                    Return False
                Case TypeCode.String
                    Return Double.TryParse(DirectCast(Expression, String), Globalization.NumberStyles.Float Or Globalization.NumberStyles.AllowThousands, Nothing, 0)
                Case TypeCode.Char
                    Return Double.TryParse(DirectCast(Expression, Char), Globalization.NumberStyles.Float Or Globalization.NumberStyles.AllowThousands, Nothing, 0)
                Case Else
                    Return False
            End Select

            Return False
        End Function
        Public Function IsReference(ByVal Expression As Object) As Boolean
            If TypeOf Expression Is ValueType Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Function LBound(ByVal Array As System.Array, Optional ByVal Rank As Integer = 1) As Integer
            ' VB rank start at 1, but System.Array.Rank starts at 0
            Dim RealRank As Integer
            RealRank = Rank - 1
            If Array Is Nothing Then Throw New System.ArgumentException("Argument 'Array' is not a valid value")

            Return Array.GetLowerBound(RealRank)
        End Function
        Public Function QBColor(ByVal Color As Integer) As Integer
            If (Color < 0 Or Color > 15) Then Throw New System.ArgumentException("Argument 'Color' is not a valid value")
            Dim tmp_arr() As Integer = {0, 8388608, 32768, 8421376, 128, 8388736, 32896, _
                                        12632256, 8421504, 16711680, 65280, 16776960, 255, _
                                        16711935, 65535, 16777215}
            Return tmp_arr(Color)

        End Function
        Public Function RGB(ByVal Red As Integer, ByVal Green As Integer, ByVal Blue As Integer) As Integer
            Dim tempStr As String = ""
            Dim res As Integer

            If (Red < 0) Then Throw New System.ArgumentException("Argument 'Red' is not a valid value")
            If (Green < 0) Then Throw New System.ArgumentException("Argument 'Green' is not a valid value")
            If (Blue < 0) Then Throw New System.ArgumentException("Argument 'Blue' is not a valid value")

            If (Red > 255) Then Red = 255
            If (Green > 255) Then Green = 255
            If (Blue > 255) Then Blue = 255

            Dim strRed, strGreen, strBlue As String
            strRed = Conversion.Hex(Red)
            strGreen = Conversion.Hex(Green)
            strBlue = Conversion.Hex(Blue)

            If strRed.Length = 1 Then tempStr = tempStr + "0" + strRed Else tempStr = tempStr + strRed
            If strGreen.Length = 1 Then tempStr = tempStr + "0" + strGreen Else tempStr = tempStr + strGreen
            If strBlue.Length = 1 Then tempStr = tempStr + "0" + strBlue Else tempStr = tempStr + strBlue

            res = Convert.ToInt32(Conversion.Val("&H" + tempStr))

            Return res
        End Function
        Public Function SystemTypeName(ByVal VbName As String) As String

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

        Friend Function VBName(ByVal tp As Type) As String
            Select Case Type.GetTypeCode(tp)
                Case TypeCode.Boolean
                    Return "Boolean"
                Case TypeCode.Byte
                    Return "Byte"
                Case TypeCode.Char
                    Return "Char"
                Case TypeCode.DateTime
                    Return "Date"
                Case TypeCode.Decimal
                    Return "Decimal"
                Case TypeCode.Double
                    Return "Double"
                Case TypeCode.Int16
                    Return "Short"
                Case TypeCode.Int32
                    Return "Integer"
                Case TypeCode.Int64
                    Return "Long"
                Case TypeCode.Object
                    If tp Is GetType(Object) Then
                        Return "Object"
                    Else
                        Return tp.Name
                    End If
                Case TypeCode.SByte
                    Return "SByte"
                Case TypeCode.Single
                    Return "Single"
                Case TypeCode.String
                    Return "String"
#If NET_VER >= 2.0 Then
                Case TypeCode.UInt16
                    Return "UShort"
                Case TypeCode.UInt32
                    Return "UInteger"
                Case TypeCode.UInt64
                    Return "ULong"
#End If
                Case Else
                    Return tp.Name
            End Select
        End Function

        Public Function TypeName(ByVal VarName As Object) As String

            Dim TmpObjType1, TmpObjType2, tmpstr As String
            Dim RetObjType As String
            Dim ArrCh As String

            If VarName Is Nothing Then Return "Nothing"
            If TypeOf VarName Is DBNull Then Return "DBNull"

            TmpObjType1 = VarName.GetType().Name.ToLower

            If VarName.GetType.IsArray Then
                Dim lastch As Integer = TmpObjType1.LastIndexOf("]") - 1
                Dim firstch As Integer = TmpObjType1.IndexOf("[") - 1
                TmpObjType2 = TmpObjType1.Remove(firstch + 1, (lastch - firstch + 1))
            Else
                TmpObjType2 = TmpObjType1
            End If
            Select Case TmpObjType2
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
                Case "decimal"
                    RetObjType = "Decimal"
                Case "double"
                    RetObjType = "Double"
                Case Else
                    RetObjType = TmpObjType1
            End Select

            If VarName.GetType.IsArray Then
                ArrCh = "()"

                tmpstr = RetObjType.Replace(")", "]")
                tmpstr = RetObjType.Replace("(", "[")
                RetObjType = tmpstr
            Else
                ArrCh = ""
            End If

            Return (RetObjType + ArrCh)

        End Function
        Public Function UBound(ByVal Array As System.Array, Optional ByVal Rank As Integer = 1) As Integer

            ' VB rank start at 1, but System.Array.Rank starts at 0
            Dim RealRank As Integer
            RealRank = Rank - 1
            If Array Is Nothing Then Throw New System.ArgumentException("Argument 'Array' is not a valid value")

            Return Array.GetUpperBound(RealRank)
        End Function
        Public Function VarType(ByVal VarName As Object) As Microsoft.VisualBasic.VariantType

            Dim tmpVar As VariantType = VariantType.Empty
            Dim TmpObjType, TmpStr, TmpObjType2 As String

            If VarName Is Nothing Then Return VariantType.Object
            If TypeOf VarName Is System.Exception Then Return VariantType.Error

            TmpObjType = VarName.GetType.Name.ToLower

            If VarName.GetType.IsEnum Then
                TmpStr = System.Enum.GetUnderlyingType(VarName.GetType).ToString
                '' remove the "System." from the type we get
                TmpObjType = TmpStr.ToLower.Substring(7)
            End If
            If VarName.GetType.IsArray Then
                Dim lastch As Integer = TmpObjType.LastIndexOf("]") - 1
                Dim firstch As Integer = TmpObjType.IndexOf("[") - 1
                TmpObjType2 = TmpObjType.Remove(firstch + 1, (lastch - firstch + 1))
            Else
                TmpObjType2 = TmpObjType
            End If
            Select Case TmpObjType2
                Case "string"
                    tmpVar = VariantType.String
                Case "dbnull"
                    tmpVar = VariantType.Null
                Case "boolean"
                    tmpVar = VariantType.Boolean
                Case "int16"
                    tmpVar = VariantType.Short
                Case "int32"
                    tmpVar = VariantType.Integer
                Case "int64"
                    tmpVar = VariantType.Long
                Case "decimal"
                    tmpVar = VariantType.Decimal
                Case "char"
                    tmpVar = VariantType.Char
                Case "byte"
                    tmpVar = VariantType.Byte
                Case "double"
                    tmpVar = VariantType.Double
                Case "single"
                    tmpVar = VariantType.Single
                Case "datetime"
                    tmpVar = VariantType.Date
                Case Else
                    'class(Ref) or struct(Val)
                    If VarName.GetType.IsValueType Then
                        tmpVar = VariantType.UserDefinedType
                    Else '' probably class
                        tmpVar = VariantType.Object
                    End If
            End Select


            '' Check If got Array Of Arrays then should return VariantType.Array | VariantType.Object 
            If (VarName.GetType.IsArray) AndAlso VarName.GetType.GetElementType Is GetType(System.Array) Then
                Return (VariantType.Array Or VariantType.Object)
            End If

            If VarName.GetType.IsArray Then
                Return (VariantType.Array Or tmpVar)
            End If

            Return tmpVar

        End Function
        Public Function VbTypeName(ByVal UrtName As String) As String

            Dim tmpStr As String
            Dim RetObjType As String


            tmpStr = UrtName.ToLower
            If UrtName.ToLower.StartsWith("system.") Then tmpStr = UrtName.ToLower.Substring(7)

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
                Case "double"
                    RetObjType = "Double"
                Case "char"
                    RetObjType = "Char"
                Case "datetime"
                    RetObjType = "Date"
                Case "single"
                    RetObjType = "Single"
                Case "object"
                    RetObjType = "Object"
                Case "decimal"
                    RetObjType = "Decimal"
                Case Else
                    RetObjType = Nothing
            End Select

            Return RetObjType
        End Function
    End Module
End Namespace
