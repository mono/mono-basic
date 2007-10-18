'
' ObjectTypeTests.vb
'
' Author:
'   Guy Cohen    (guyc@mainsoft.com)
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
Imports System.Reflection
Imports NUnit.Framework
Imports Microsoft.VisualBasic.CompilerServices


<TestFixture()> _
Public Class ObjectTypeTests


    <Test()> _
    Public Sub BitAndObj_1()
        ' pass same types to BitAndObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "123"
        Dim s2 As String = "123"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        obj1 = b1
        obj2 = b2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Byte", ObjTypeC.ToString(), "BO: Byte ")
        Assert.AreEqual(1, resObj, "BA: Byte(2)")

        obj1 = bool1
        obj2 = bool2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Boolean", ObjTypeC.ToString(), "BO: Boolean")
        Assert.AreEqual(True, resObj, "BA: Boolean(True)")

        obj1 = dbl1
        obj2 = dbl2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Double")
        Assert.AreEqual(3, resObj, "BA: Double(3)")

        obj1 = sn1
        obj2 = sn2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Single")
        Assert.AreEqual(1, resObj, "BA: Single(1)")

        obj1 = dec1
        obj2 = dec2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Decimal")
        Assert.AreEqual(0, resObj, "BA: Decimal(0)")

        obj1 = l1
        obj2 = l2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Long")
        Assert.AreEqual(1234560, resObj, "BA: Long(1234560)")

        obj1 = i1
        obj2 = i2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int32", ObjTypeC.ToString(), "BO: Integer")
        Assert.AreEqual(3, resObj, "BA: Integer(3)")

        obj1 = short1
        obj2 = short2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: Short")
        Assert.AreEqual(0, resObj, "BA: Short(0)")

        obj1 = s1
        obj2 = s2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: String")
        Assert.AreEqual(123, resObj, "BA: String(0)")
    End Sub


    <Test()> _
    Public Sub BitAndObj_2()
        ' pass different types to BitAndObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "112"
        Dim s2 As String = "112"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        ' Byte - Single
        obj1 = b1
        obj2 = sn2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Byte - Single")
        Assert.AreEqual(5, resObj, "BA: Single(5)")

        ' Single - Double
        obj1 = sn1
        obj2 = dbl2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Single - Double")
        Assert.AreEqual(1, resObj, "BA: Double(1)")

        ' short - bool
        obj1 = short1
        obj2 = bool2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: short - bool")
        Assert.AreEqual(2, resObj, "BA: Short(2)")

        ' short - string
        obj1 = s1
        obj2 = short2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: short - string")
        Assert.AreEqual(0, resObj, "BA: Single(1)")

        ' decimal - bool
        obj1 = dec1
        obj2 = bool2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: decimal - bool")
        Assert.AreEqual(3, resObj, "BA: decimal#1(0)")

        ' bool - decimal
        obj1 = bool1
        obj2 = dec1
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: bool - decimal")
        Assert.AreEqual(3, resObj, "BA: decimal#2(0)")

        ' Nothing - decimal
        obj1 = Nothing
        obj2 = dec2
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        '  Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Nothing - decimal")
        Assert.AreEqual(0, resObj, "BA: Nothing(0)")

        ' decimal - Nothing
        obj1 = dec1
        obj2 = Nothing
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: decimal - Nothing")
        Assert.AreEqual(0, resObj, "BA: Nothing(0)")

        ' short - Nothing
        obj1 = short1
        obj2 = Nothing
        resObj = ObjectType.BitAndObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: short - Nothing")
        Assert.AreEqual(0, resObj, "BA: Nothing(0)")

    End Sub

    <Test()> _
    Public Sub BitOrObj_1()

        ' pass same types to BitOrObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "123"
        Dim s2 As String = "123"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        obj1 = b1
        obj2 = b2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Byte", ObjTypeC.ToString(), "BO: Byte ")
        Assert.AreEqual(7, resObj, "BO: Byte(7)")

        obj1 = bool1
        obj2 = bool2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Boolean", ObjTypeC.ToString(), "BO: Boolean")
        Assert.AreEqual(True, resObj, "BO: Boolean(True)")

        obj1 = dbl1
        obj2 = dbl2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Double")
        Assert.AreEqual(3, resObj, "BO: Double(3)")

        obj1 = sn1
        obj2 = sn2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Single")
        Assert.AreEqual(5, resObj, "BO: Single(5)")

        obj1 = dec1
        obj2 = dec2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Decimal")
        Assert.AreEqual(7, resObj, "BO: Decimal(7)")

        obj1 = l1
        obj2 = l2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Long")
        Assert.AreEqual(1234575, resObj, "BO: Long(1234575)")

        obj1 = i1
        obj2 = i2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int32", ObjTypeC.ToString(), "BO: Integer")
        Assert.AreEqual(7, resObj, "BO: Integer(7)")

        obj1 = short1
        obj2 = short2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: Short")
        Assert.AreEqual(3, resObj, "BO: Short(3)")

        obj1 = s1
        obj2 = s2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: String")
        Assert.AreEqual(123, resObj, "BO: String(123)")
    End Sub

    <Test()> _
 Public Sub BitOrObj_2()
        ' pass different types to BitOrObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "112"
        Dim s2 As String = "112"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        ' Byte - Single
        obj1 = b1
        obj2 = sn2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Byte - Single")
        Assert.AreEqual(5, resObj, "BO: Single(5)")

        ' Single - Double
        obj1 = sn1
        obj2 = dbl2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Single - Double")
        Assert.AreEqual(3, resObj, "BO: Double(3)")

        ' short - bool
        obj1 = short1
        obj2 = bool2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: short - bool")
        Assert.AreEqual(-1, resObj, "BO: Short(-1)")

        ' short - string
        obj1 = s1
        obj2 = short2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: short - string")
        Assert.AreEqual(113, resObj, "BO: Double(113)")

        ' decimal - bool
        obj1 = dec1
        obj2 = bool2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: decimal - bool")
        Assert.AreEqual(-1, resObj, "BO: decimal#1(-1)")

        ' bool - decimal
        obj1 = bool1
        obj2 = dec1
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: bool - decimal")
        Assert.AreEqual(-1, resObj, "BO: decimal#2(-1)")

        ' Nothing - decimal
        obj1 = Nothing
        obj2 = dec2
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        '  Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: Nothing - decimal")
        Assert.AreEqual(4, resObj, "BO: decimal(4)")

        ' decimal - Nothing
        obj1 = dec1
        obj2 = Nothing
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int64", ObjTypeC.ToString(), "BO: decimal - Nothing")
        Assert.AreEqual(3, resObj, "BO: decimal(3)")

        ' short - Nothing
        obj1 = short1
        obj2 = Nothing
        resObj = ObjectType.BitOrObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int16", ObjTypeC.ToString(), "BO: short - Nothing")
        Assert.AreEqual(2, resObj, "BO: short(2)")

    End Sub




    <Test()> _
    Public Sub BitXorObj_1()

        ' pass same types to BitXorObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "123"
        Dim s2 As String = "123"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        obj1 = b1
        obj2 = b2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Byte", ObjTypeC.ToString(), "BX: Byte ")
        Assert.AreEqual(6, resObj, "BX: Byte(6)")

        obj1 = bool1
        obj2 = bool2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Boolean", ObjTypeC.ToString(), "BX: Boolean")
        Assert.AreEqual(False, resObj, "BX: Boolean(False)")

        obj1 = dbl1
        obj2 = dbl2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Double")
        Assert.AreEqual(0, resObj, "BX: Double(0)")

        obj1 = sn1
        obj2 = sn2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Single")
        Assert.AreEqual(4, resObj, "BX: Single(4)")

        obj1 = dec1
        obj2 = dec2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Decimal")
        Assert.AreEqual(7, resObj, "BX: Decimal(0)")

        obj1 = l1
        obj2 = l2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Long")
        Assert.AreEqual(15, resObj, "BX: Long(15)")

        obj1 = i1
        obj2 = i2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int32", ObjTypeC.ToString(), "BX: Integer")
        Assert.AreEqual(4, resObj, "BX: Integer(4)")

        obj1 = short1
        obj2 = short2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BX: Short")
        Assert.AreEqual(3, resObj, "BX: Short(3)")

        obj1 = s1
        obj2 = s2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: String")
        Assert.AreEqual(0, resObj, "BX: String(0)")
    End Sub

    <Test()> _
 Public Sub BitXorObj_2()
        ' pass different types to BitXorObj()
        Dim obj1 As Object
        Dim obj2 As Object

        Dim b1 As Byte = 5
        Dim b2 As Byte = 3

        Dim bool1 As Boolean = True
        Dim bool2 As Boolean = True

        Dim dbl1 As Double = 3D
        Dim dbl2 As Double = 3D

        Dim sn1 As Single = 1
        Dim sn2 As Single = 5

        Dim dec1 As Decimal = 3.4
        Dim dec2 As Decimal = 4.4

        Dim l1 As Long = 1234567
        Dim l2 As Long = 1234568

        Dim i1 As Integer = 7
        Dim i2 As Integer = 3

        Dim short1 As Short = 2
        Dim short2 As Short = 1

        Dim s1 As String = "112"
        Dim s2 As String = "112"

        Dim resObj As Object
        Dim ObjTypeC As TypeCode

        ' Byte - Single
        obj1 = b1
        obj2 = sn2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Byte - Single")
        Assert.AreEqual(0, resObj, "BX: Single(0)")

        ' Single - Double
        obj1 = sn1
        obj2 = dbl2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Single - Double")
        Assert.AreEqual(2, resObj, "BX: Double(2)")

        ' short - bool
        obj1 = short1
        obj2 = bool2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int16", ObjTypeC.ToString(), "BX: short - bool")
        Assert.AreEqual(-3, resObj, "BX: Short(-3)")

        ' short - string
        obj1 = s1
        obj2 = short2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: short - string")
        Assert.AreEqual(113, resObj, "BX: Double(113)")

        ' decimal - bool
        obj1 = dec1
        obj2 = bool2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: decimal - bool")
        Assert.AreEqual(-4, resObj, "BX: decimal#1(-4)")

        ' bool - decimal
        obj1 = bool1
        obj2 = dec1
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: bool - decimal")
        Assert.AreEqual(-4, resObj, "BX: decimal#2(-4)")

        ' Nothing - decimal
        obj1 = Nothing
        obj2 = dec2
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        '  Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: Nothing - decimal")
        Assert.AreEqual(4, resObj, "BX: decimal(4)")

        ' decimal - Nothing
        obj1 = dec1
        obj2 = Nothing
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int64", ObjTypeC.ToString(), "BX: decimal - Nothing")
        Assert.AreEqual(3, resObj, "BX: decimal(3)")

        ' short - Nothing
        obj1 = short1
        obj2 = Nothing
        resObj = ObjectType.BitXorObj(obj1, obj2)
        ObjTypeC = Type.GetTypeCode(resObj.GetType())
        ' Assert.AreEqual("Int16", ObjTypeC.ToString(), "BX: short - Nothing")
        Assert.AreEqual(2, resObj, "BX: short(2)")

    End Sub
End Class
