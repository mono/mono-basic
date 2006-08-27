'
' ObjectType.vb
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
Namespace Microsoft.VisualBasic.CompilerServices
    Public Class ObjectType

        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function ObjTst(ByVal o1 As System.Object, ByVal o2 As System.Object, ByVal TextCompare As System.Boolean) As System.Int32

            Dim b1 As Byte
            Dim b2 As Byte

            Dim bool1 As Boolean
            Dim bool2 As Boolean

            Dim dbl1 As Double
            Dim dbl2 As Double

            Dim f1 As Single
            Dim f2 As Single

            Dim dec1 As Decimal
            Dim dec2 As Decimal

            Dim l1 As Long
            Dim l2 As Long

            Dim i1 As Integer
            Dim i2 As Integer

            Dim short1 As Short
            Dim short2 As Short

            Dim s1 As String
            Dim s2 As String

            Dim c1 As Char
            Dim c2 As Char

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            'FIXME: Add ElseIf implementation for all types
            If (TypeOf o1 Is Double) Or (TypeOf o2 Is Double) Then
                dbl1 = Convert.ToDouble(o1)
                dbl2 = Convert.ToDouble(o2)

                If dbl1 < dbl2 Then
                    Return -1
                End If

                If dbl1 > dbl2 Then
                    Return 1
                End If

                'If dbl1 = dbl2
                Return 0
            ElseIf (TypeOf o1 Is Single) Or (TypeOf o2 Is Single) Then
                f1 = Convert.ToSingle(o1)
                f2 = Convert.ToSingle(o2)

                If f1 < f2 Then
                    Return -1
                End If

                If f1 > f2 Then
                    Return 1
                End If

                'If f1 = f2
                Return 0

            ElseIf (TypeOf o1 Is Decimal) Or (TypeOf o2 Is Decimal) Then
                dec1 = Convert.ToDecimal(o1)
                dec2 = Convert.ToDecimal(o2)

                If dec1 < dec2 Then
                    Return -1
                End If

                If dec1 > dec2 Then
                    Return 1
                End If

                'If dec1 = dec2
                Return 0

            ElseIf (TypeOf o1 Is Long) Or (TypeOf o2 Is Long) Then
                l1 = Convert.ToInt64(o1)
                l2 = Convert.ToInt64(o2)

                If l1 < l2 Then
                    Return -1
                End If

                If l1 > l2 Then
                    Return 1
                End If

                'If l1 = l2
                Return 0
            ElseIf (TypeOf o1 Is Integer) Or (TypeOf o2 Is Integer) Then
                i1 = Convert.ToInt32(o1)
                i2 = Convert.ToInt32(o2)

                If i1 < i2 Then
                    Return -1
                End If

                If i1 > i2 Then
                    Return 1
                End If

                'If i1 = i2
                Return 0
            ElseIf (TypeOf o1 Is Short) Or (TypeOf o2 Is Short) Then
                short1 = Convert.ToInt16(o1)
                short2 = Convert.ToInt16(o2)

                If short1 < short2 Then
                    Return -1
                End If

                If short1 > short2 Then
                    Return 1
                End If

                'If short1 = short2
                Return 0
            ElseIf (TypeOf o1 Is String) Or (TypeOf o2 Is String) Then
                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                Return s1.CompareTo(s2)

            ElseIf (TypeOf o1 Is Char) Or (TypeOf o2 Is Char) Then
                c1 = Convert.ToChar(o1)
                c2 = Convert.ToChar(o2)

                Return c1.CompareTo(c2)

            ElseIf (TypeOf o1 Is Byte) Or (TypeOf o2 Is Byte) Then
                b1 = Convert.ToByte(o1)
                b2 = Convert.ToByte(o2)

                Return b1.CompareTo(b2)

            ElseIf (TypeOf o1 Is Boolean) Or (TypeOf o2 Is Boolean) Then
                bool1 = Convert.ToBoolean(o1)
                bool2 = Convert.ToBoolean(o2)

                Return bool1.CompareTo(bool2)

            Else ' Not implemented case
                Throw New NotImplementedException("Implement me: " + o1.GetType.Name + " " + o2.GetType.Name)
            End If

        End Function
        Public Shared Function PlusObj(ByVal obj As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function NegObj(ByVal obj As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function NotObj(ByVal obj As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function BitAndObj(ByVal obj1 As System.Object, ByVal obj2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function BitOrObj(ByVal obj1 As System.Object, ByVal obj2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function BitXorObj(ByVal obj1 As System.Object, ByVal obj2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function

        Public Shared Function AddObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object

            Dim b1 As Byte
            Dim b2 As Byte

            Dim bool1 As Boolean
            Dim bool2 As Boolean

            Dim dbl1 As Double
            Dim dbl2 As Double

            Dim f1 As Single
            Dim f2 As Single

            Dim dec1 As Decimal
            Dim dec2 As Decimal

            Dim l1 As Long
            Dim l2 As Long

            Dim i1 As Integer
            Dim i2 As Integer

            Dim short1 As Short
            Dim short2 As Short

            Dim s1 As String
            Dim s2 As String

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            'FIXME: Add defense for checking overflow.
            'FIXME: Add support for Date
            If (TypeOf o1 Is Double) Or (TypeOf o2 Is Double) Then
                dbl1 = Convert.ToDouble(o1)
                dbl2 = Convert.ToDouble(o2)

                Return dbl1 + dbl2
            ElseIf (TypeOf o1 Is Single) Or (TypeOf o2 Is Single) Then
                f1 = Convert.ToSingle(o1)
                f2 = Convert.ToSingle(o2)

                Return f1 + f2

            ElseIf (TypeOf o1 Is Decimal) Or (TypeOf o2 Is Decimal) Then
                dec1 = Convert.ToDecimal(o1)
                dec2 = Convert.ToDecimal(o2)

                Return dec1 + dec2

            ElseIf (TypeOf o1 Is Long) Or (TypeOf o2 Is Long) Then
                l1 = Convert.ToInt64(o1)
                l2 = Convert.ToInt64(o2)

                Return l1 + l2
            ElseIf (TypeOf o1 Is Integer) Or (TypeOf o2 Is Integer) Then
                i1 = Convert.ToInt32(o1)
                i2 = Convert.ToInt32(o2)

                Return i1 + i2

            ElseIf (TypeOf o1 Is Short) Or (TypeOf o2 Is Short) Then
                short1 = Convert.ToInt16(o1)
                short2 = Convert.ToInt16(o2)

                Return short1 + short2

            ElseIf (TypeOf o1 Is Byte) Or (TypeOf o2 Is Byte) Then
                b1 = Convert.ToByte(o1)
                b2 = Convert.ToByte(o2)

                Return b1 + b2

            ElseIf (TypeOf o1 Is Boolean) Or (TypeOf o2 Is Boolean) Then
                bool1 = Convert.ToBoolean(o1)
                bool2 = Convert.ToBoolean(o2)

                Return Convert.ToInt16(bool1) + Convert.ToInt16(bool2)

            ElseIf ((TypeOf o1 Is String) And (TypeOf o2 Is String)) Or _
                    ((TypeOf o1 Is Char) And (TypeOf o2 Is Char)) Or _
                    ((TypeOf o1 Is String) And (TypeOf o2 Is Char)) Or _
                    ((TypeOf o1 Is Char) And (TypeOf o2 Is String)) Then
                ' both are String, its a Concat
                ' both are Char, its a Concat
                ' one is String and one is Char, its a Concat
                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                Return s1 + s2

            ElseIf (TypeOf o1 Is String) Or (TypeOf o2 Is String) Then
                ' one is String, its a numeric Add

                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                If s1 = "" Then s1 = "0"
                If s2 = "" Then s2 = "0"

                dbl1 = Convert.ToDouble(s1)
                dbl2 = Convert.ToDouble(s2)

                Return dbl1 + dbl2
            Else ' Not implemented case
                Throw New NotImplementedException("Implement me: " + o1.GetType.Name + " " + o2.GetType.Name)
            End If
        End Function

        Public Shared Function SubObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object

            Dim b1 As Byte
            Dim b2 As Byte

            Dim bool1 As Boolean
            Dim bool2 As Boolean

            Dim dbl1 As Double
            Dim dbl2 As Double

            Dim f1 As Single
            Dim f2 As Single

            Dim dec1 As Decimal
            Dim dec2 As Decimal

            Dim l1 As Long
            Dim l2 As Long

            Dim i1 As Integer
            Dim i2 As Integer

            Dim short1 As Short
            Dim short2 As Short

            Dim s1 As String
            Dim s2 As String

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            'FIXME: Add defense for checking overflow.
            If (TypeOf o1 Is Double) Or (TypeOf o2 Is Double) Then
                dbl1 = Convert.ToDouble(o1)
                dbl2 = Convert.ToDouble(o2)

                Return dbl1 - dbl2
            ElseIf (TypeOf o1 Is Single) Or (TypeOf o2 Is Single) Then
                f1 = Convert.ToSingle(o1)
                f2 = Convert.ToSingle(o2)

                Return f1 - f2
            ElseIf (TypeOf o1 Is Decimal) Or (TypeOf o2 Is Decimal) Then
                dec1 = Convert.ToDecimal(o1)
                dec2 = Convert.ToDecimal(o2)

                Return dec1 - dec2

            ElseIf (TypeOf o1 Is Long) Or (TypeOf o2 Is Long) Then
                l1 = Convert.ToInt64(o1)
                l2 = Convert.ToInt64(o2)

                Return l1 - l2
            ElseIf (TypeOf o1 Is Integer) Or (TypeOf o2 Is Integer) Then
                i1 = Convert.ToInt32(o1)
                i2 = Convert.ToInt32(o2)

                Return i1 - i2
            ElseIf (TypeOf o1 Is Short) Or (TypeOf o2 Is Short) Then
                short1 = Convert.ToInt16(o1)
                short2 = Convert.ToInt16(o2)

                Return short1 - short2
            ElseIf (TypeOf o1 Is Byte) Or (TypeOf o2 Is Byte) Then
                b1 = Convert.ToByte(o1)
                b2 = Convert.ToByte(o2)

                Return b1 - b2
            ElseIf (TypeOf o1 Is Boolean) Or (TypeOf o2 Is Boolean) Then
                bool1 = Convert.ToBoolean(o1)
                bool2 = Convert.ToBoolean(o2)

                Return Convert.ToInt16(bool1) - Convert.ToInt16(bool2)

            ElseIf (TypeOf o1 Is String) Or (TypeOf o2 Is String) Then
                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                If s1 = "" Then s1 = "0"
                If s2 = "" Then s2 = "0"

                dbl1 = Convert.ToDouble(s1)
                dbl2 = Convert.ToDouble(s2)

                Return dbl1 - dbl2
            Else ' Not implemented case
                Throw New NotImplementedException("Implement me: " + o1.GetType.Name + " " + o2.GetType.Name)
            End If
        End Function

        Public Shared Function MulObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object

            Dim b1 As Byte
            Dim b2 As Byte

            Dim bool1 As Boolean
            Dim bool2 As Boolean

            Dim dbl1 As Double
            Dim dbl2 As Double

            Dim f1 As Single
            Dim f2 As Single

            Dim dec1 As Decimal
            Dim dec2 As Decimal

            Dim l1 As Long
            Dim l2 As Long

            Dim i1 As Integer
            Dim i2 As Integer

            Dim short1 As Short
            Dim short2 As Short

            Dim s1 As String
            Dim s2 As String

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If

            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
                ' multiple of zero and x is zero
                Return o1
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
                ' multiple of x and zero is zero
                Return o2
            End If

            'FIXME: Add ElseIf implementation for all types.
            'FIXME: Add defense for checking overflow.
            If (TypeOf o1 Is Double) Or (TypeOf o2 Is Double) Then
                dbl1 = Convert.ToDouble(o1)
                dbl2 = Convert.ToDouble(o2)

                Return dbl1 * dbl2
            ElseIf (TypeOf o1 Is Single) Or (TypeOf o2 Is Single) Then
                f1 = Convert.ToSingle(o1)
                f2 = Convert.ToSingle(o2)

                Return f1 * f2

            ElseIf (TypeOf o1 Is Decimal) Or (TypeOf o2 Is Decimal) Then
                dec1 = Convert.ToDecimal(o1)
                dec2 = Convert.ToDecimal(o2)

                Return dec1 * dec2

            ElseIf (TypeOf o1 Is Long) Or (TypeOf o2 Is Long) Then
                l1 = Convert.ToInt64(o1)
                l2 = Convert.ToInt64(o2)

                Return l1 * l2
            ElseIf (TypeOf o1 Is Integer) Or (TypeOf o2 Is Integer) Then
                i1 = Convert.ToInt32(o1)
                i2 = Convert.ToInt32(o2)

                Return i1 * i2
            ElseIf (TypeOf o1 Is Short) Or (TypeOf o2 Is Short) Then
                short1 = Convert.ToInt16(o1)
                short2 = Convert.ToInt16(o2)

                Return short1 * short2
            ElseIf (TypeOf o1 Is Byte) Or (TypeOf o2 Is Byte) Then
                b1 = Convert.ToByte(o1)
                b2 = Convert.ToByte(o2)

                Return b1 * b2

            ElseIf (TypeOf o1 Is Boolean) Or (TypeOf o2 Is Boolean) Then
                bool1 = Convert.ToBoolean(o1)
                bool2 = Convert.ToBoolean(o2)

                Return Convert.ToInt16(bool1) * Convert.ToInt16(bool2)

            ElseIf (TypeOf o1 Is String) Or (TypeOf o2 Is String) Then
                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                If s1 = "" Then s1 = "0"
                If s2 = "" Then s2 = "0"

                dbl1 = Convert.ToDouble(s1)
                dbl2 = Convert.ToDouble(s2)

                Return dbl1 * dbl2

            Else ' Not implemented case
                Throw New NotImplementedException("Implement me: " + o1.GetType.Name + " " + o2.GetType.Name)
            End If
        End Function

        Public Shared Function DivObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object
            Dim b1 As Byte
            Dim b2 As Byte

            Dim bool1 As Boolean
            Dim bool2 As Boolean

            Dim dbl1 As Double
            Dim dbl2 As Double

            Dim f1 As Single
            Dim f2 As Single

            Dim dec1 As Decimal
            Dim dec2 As Decimal

            Dim l1 As Long
            Dim l2 As Long

            Dim i1 As Integer
            Dim i2 As Integer

            Dim short1 As Short
            Dim short2 As Short

            Dim s1 As String
            Dim s2 As String

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If

            'FIXME: Add checks for Nothing
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
                ' divide of zero and x is infinite
                Throw New NotImplementedException("implement me")
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
                ' divide of x and zero is exception
                Throw New DivideByZeroException
            End If

            'FIXME: Add ElseIf implementation for all types.
            'FIXME: Add defense for checking overflow.
            If (TypeOf o1 Is Double) Or (TypeOf o2 Is Double) Then
                dbl1 = Convert.ToDouble(o1)
                dbl2 = Convert.ToDouble(o2)

                Return dbl1 / dbl2
            ElseIf (TypeOf o1 Is Single) Or (TypeOf o2 Is Single) Then
                f1 = Convert.ToSingle(o1)
                f2 = Convert.ToSingle(o2)

                Return f1 / f2

            ElseIf (TypeOf o1 Is Decimal) Or (TypeOf o2 Is Decimal) Then
                dec1 = Convert.ToDecimal(o1)
                dec2 = Convert.ToDecimal(o2)

                Return dec1 / dec2

            ElseIf (TypeOf o1 Is Long) Or (TypeOf o2 Is Long) Then
                l1 = Convert.ToInt64(o1)
                l2 = Convert.ToInt64(o2)

                Return l1 / l2
            ElseIf (TypeOf o1 Is Integer) Or (TypeOf o2 Is Integer) Then
                i1 = Convert.ToInt32(o1)
                i2 = Convert.ToInt32(o2)

                Return i1 / i2
            ElseIf (TypeOf o1 Is Short) Or (TypeOf o2 Is Short) Then
                short1 = Convert.ToInt16(o1)
                short2 = Convert.ToInt16(o2)

                Return short1 / short2
            ElseIf (TypeOf o1 Is Byte) Or (TypeOf o2 Is Byte) Then
                b1 = Convert.ToByte(o1)
                b2 = Convert.ToByte(o2)

                Return b1 / b2

            ElseIf (TypeOf o1 Is Boolean) Or (TypeOf o2 Is Boolean) Then
                bool1 = Convert.ToBoolean(o1)
                bool2 = Convert.ToBoolean(o2)

                Return Convert.ToInt16(bool1) / Convert.ToInt16(bool2)

            ElseIf (TypeOf o1 Is String) Or (TypeOf o2 Is String) Then
                s1 = Convert.ToString(o1)
                s2 = Convert.ToString(o2)

                If s1 = "" Then s1 = "0"
                If s2 = "" Then s2 = "0"

                dbl1 = Convert.ToDouble(s1)
                dbl2 = Convert.ToDouble(s2)

                Return dbl1 / dbl2

            Else ' Not implemented case
                Throw New NotImplementedException("Implement me: " + o1.GetType.Name + " " + o2.GetType.Name)
            End If
        End Function

        Public Shared Function PowObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object
            Dim dbl1 As Double
            Dim dbl2 As Double

            ' comparing null objects
            ' if both are null, return 0
            ' if one is a type, convert the other to its 'null value'
            If (o1 Is Nothing) And (o2 Is Nothing) Then
                Return 0
            End If
            If (o1 Is Nothing) Then
                o1 = CreateNullObjectType(o2)
            End If
            If (o2 Is Nothing) Then
                o2 = CreateNullObjectType(o1)
            End If

            'FIXME: Add defense for checking overflow.
            dbl1 = Convert.ToDouble(o1)
            dbl2 = Convert.ToDouble(o2)
            Return Math.Pow(dbl1, dbl2)
        End Function
        Public Shared Function ShiftLeftObj(ByVal o1 As Object, ByVal amount As Integer) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function ShiftRightObj(ByVal o1 As Object, ByVal amount As Integer) As Object
            Throw New NotImplementedException
        End Function
        Public Shared Function ModObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function
        Public Shared Function IDivObj(ByVal o1 As System.Object, ByVal o2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function
        Public Shared Function XorObj(ByVal obj1 As System.Object, ByVal obj2 As System.Object) As System.Object
            Throw New NotImplementedException
        End Function
        Public Shared Function LikeObj(ByVal vLeft As System.Object, ByVal vRight As System.Object, ByVal CompareOption As Microsoft.VisualBasic.CompareMethod) As System.Boolean

            Dim strLeft As String = StringType.FromObject(vLeft)
            Dim strRight As String = StringType.FromObject(vRight)

            Return StringType.StrLike(strLeft, strRight, CompareOption)
        End Function
        Public Shared Function StrCatObj(ByVal vLeft As System.Object, ByVal vRight As System.Object) As System.Object

            Dim strLeft As String = StringType.FromObject(vLeft)
            Dim strRight As String = StringType.FromObject(vRight)

            Return strLeft & strRight
        End Function

        Public Shared Function GetObjectValuePrimitive(ByVal o As System.Object) As System.Object
            Throw New NotImplementedException
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

    End Class
End Namespace
