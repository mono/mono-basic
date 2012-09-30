Imports System
Imports Microsoft.VisualBasic.CompilerServices
Imports NUnit.Framework
<TestFixture()> _
Public Class OperatorsTests
    Sub New()
        Helper.SetThreadCulture()
    End Sub

    <Test()> _
    Sub TestOperatorsCompareEqual1()
        Assert.IsTrue(Operators.CompareObjectEqual("True", True, False), """True"" = True")
        Assert.IsTrue(Operators.CompareObjectEqual(True, "True", False), "True = ""True""")
        Assert.IsTrue(Operators.CompareObjectEqual("False", False, False), """False"" = False")
        Assert.IsTrue(Operators.CompareObjectEqual(False, "False", False), "False = ""False""")

        Assert.IsFalse(Operators.CompareObjectEqual("True", False, False), """True"" = False")
        Assert.IsFalse(Operators.CompareObjectEqual(False, "True", False), "False = ""True""")
        Assert.IsFalse(Operators.CompareObjectEqual("False", True, False), """False"" = True")
        Assert.IsFalse(Operators.CompareObjectEqual(True, "False", False), "True = ""False""")
    End Sub

    <Test(), ExpectedException(GetType(InvalidCastException))> _
    Sub TestOperatorsCompareEqual2()
        Assert.IsTrue(Operators.CompareObjectEqual("Truez", True, False), """Truez"" = True")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "ab")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "d1")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "11")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate4()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = 1
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "11")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate5()
        Dim o1, o2, o3 As Object
        o1 = 1.1
        o2 = 1
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "1.11")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, String.Concat(o1.ToString(), o2.ToString()))
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate7()
        Dim o1, o2, o3 As Object
        o1 = "a"c
        o2 = "b"c
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "ab")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate8()
        Dim o1, o2, o3, o4, o5, o6 As Object
        o1 = "b"c
        o2 = Nothing
        o3 = Operators.ConcatenateObject(o1, o2)
        o4 = "b"c
        o5 = DBNull.Value
        o6 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, o6)
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 1
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "1")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate11()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "abc"
        o3 = Operators.ConcatenateObject(o1, o2)
        Assert.AreEqual(o3, "ok&")
    End Sub

    <Test()> _
    Sub TestOperatorsConcatenate12()
        Dim o1, o2, o3 As Object
        o1 = "abc"
        o2 = New OperatorsImplementer()
        Try
            o3 = Operators.ConcatenateObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNegate1()
        Dim o1, o2 As Object
        o1 = True
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Short) Then
            Assert.AreEqual(CType(o2, Short), 1S)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate2()
        Dim o1, o2 As Object
        o1 = Nothing
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Integer) Then
            Assert.AreEqual(CType(o2, Integer), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate3()
        Dim o1, o2 As Object
        o1 = Nothing
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Integer) Then
            Assert.AreEqual(CType(o2, Integer), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate4()
        Dim o1, o2 As Object
        o1 = CType(2, Byte)
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Short) Then
            Assert.AreEqual(CType(o2, Short), -2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate5()
        Dim o1, o2 As Object
        o1 = CType(0, Byte)
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Short) Then
            Assert.AreEqual(CType(o2, Short), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate6()
        Dim o1, o2 As Object
        o1 = "1"c
        Try
            o2 = Operators.NegateObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNegate7()
        Dim o1 As DateTime = DateTime.Now
        Dim o2 As Object
        Try
            o2 = Operators.NegateObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNegate8()
        Dim o1, o2 As Object
        o1 = CType(-1, Decimal)
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Decimal) Then
            Assert.AreEqual(CType(o2, Decimal), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate9()
        Dim o1, o2 As Object
        o1 = CType(-1, Double)
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Double) Then
            Assert.AreEqual(CType(o2, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate10()
        Dim o1, o2 As Object
        o1 = CType(Int16.MinValue, Int16)
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Int32) Then
            Assert.AreEqual(CType(o2, Int32), Int16.MaxValue + 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate11()
        Dim o1, o2 As Object
        o1 = "1L"
        Try
            o2 = Operators.NegateObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNegate12()
        Dim o1, o2 As Object
        o1 = "1"
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Double) Then
            Assert.AreEqual(CType(o2, Double), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate13()
        Dim o1, o2 As Object
        o1 = 3US
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Integer) Then
            Assert.AreEqual(CType(o2, Integer), -3)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNegate14()
        Dim o1, o2 As Object
        o1 = New OperatorsImplementer()
        o2 = Operators.NegateObject(o1)
        Assert.AreEqual(o2, "-ok")
    End Sub

    <Test()> _
    Sub TestOperatorsNegate15()
        Dim o1, o2 As Object
        o1 = 3UL
        o2 = Operators.NegateObject(o1)
        If (TypeOf o2 Is Decimal) Then
            Assert.AreEqual(CType(o2, Decimal), -3)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus1()
        Dim o1, o2 As Object
        o1 = True
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Short) Then
            Assert.AreEqual(CType(o2, Short), -1S)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus2()
        Dim o1, o2 As Object
        o1 = Nothing
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Integer) Then
            Assert.AreEqual(CType(o2, Integer), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus3()
        Dim o1, o2 As Object
        o1 = DBNull.Value
        Try
            o2 = Operators.PlusObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsPlus4()
        Dim o1, o2 As Object
        o1 = CType(2, Byte)
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Byte) Then
            Assert.AreEqual(CType(o2, Byte), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus5()
        Dim o1, o2 As Object
        o1 = CType(0, Byte)
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Byte) Then
            Assert.AreEqual(CType(o2, Byte), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus6()
        Dim o1, o2 As Object
        o1 = "1"c
        Try
            o2 = Operators.PlusObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsPlus7()
        Dim o2 As Object
        Dim o1 As DateTime = DateTime.Now
        Try
            o2 = Operators.PlusObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsPlus8()
        Dim o1, o2 As Object
        o1 = CType(-1, Decimal)
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Decimal) Then
            Assert.AreEqual(CType(o2, Decimal), -1)
        Else
            Assert.Fail()
        End If
    End Sub


    <Test()> _
    Sub TestOperatorsPlus9()
        Dim o1, o2 As Object
        o1 = CType(-1, Double)
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Double) Then
            Assert.AreEqual(CType(o2, Double), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus10()
        Dim o1, o2 As Object
        o1 = CType(Int16.MinValue, Int16)
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Short) Then
            Assert.AreEqual(CType(o2, Int32), Int16.MinValue)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus11()
        Dim o1, o2 As Object
        o1 = "1L"

        Try
            o2 = Operators.PlusObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsPlus12()
        Dim o1, o2 As Object
        o1 = "1"
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is Double) Then
            Assert.AreEqual(CType(o2, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus13()
        Dim o1, o2 As Object
        o1 = 3US
        o2 = Operators.PlusObject(o1)
        If (TypeOf o2 Is UShort) Then
            Assert.AreEqual(CType(o2, Integer), 3)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsPlus14()
        Dim o1, o2 As Object
        o1 = New OperatorsImplementer()
        o2 = Operators.PlusObject(o1)
        Assert.AreEqual(o2, "+ok")
    End Sub

    <Test()> _
    Sub TestOperatorsNot1()
        Dim o1, o2 As Object
        o1 = True
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Boolean) Then
            Assert.AreEqual(CType(o2, Boolean), False)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot2()
        Dim o1, o2 As Object
        o1 = Nothing
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Integer) Then
            Assert.AreEqual(CType(o2, Integer), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot3()
        Dim o1, o2 As Object
        o1 = DBNull.Value
        Try
            o2 = Operators.NotObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNot4()
        Dim o1, o2 As Object
        o1 = CType(2, Byte)
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Byte) Then
            Assert.AreEqual(CType(o2, Byte), (Not CType(o1, Byte)))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot5()
        Dim o1, o2 As Object
        o1 = CType(0, Byte)
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Byte) Then
            Assert.AreEqual(CType(o2, Byte), (Not CType(o1, Byte)))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot6()
        Dim o1, o2 As Object
        o1 = "1"c
        Try
            o2 = Operators.NotObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNot7()
        Dim o2 As Object
        Dim o1 As DateTime = DateTime.Now
        Try
            o2 = Operators.NotObject(o1)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsNot8()
        Dim o1, o2 As Object
        o1 = "1.1"
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Long) Then
            Assert.AreEqual(o2, -2L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot9()
        Dim o1, o2 As Object
        o1 = 1.1
        o2 = Operators.NotObject(o1)
        If (TypeOf o2 Is Long) Then
            Assert.AreEqual(o2, -2L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsNot10()
        Dim o1, o2 As Object
        o1 = New OperatorsImplementer()
        o2 = Operators.NotObject(o1)
        Assert.AreEqual(o2, "!ok")
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject1()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = -1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> -1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject2()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = Nothing
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject3()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject4()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 >> 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject5()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = False
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 >> 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject6()
        Dim o1, o2, o3 As Object
        o1 = DBNull.Value
        o2 = 1
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject7()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DBNull.Value
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DateTime.Now
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject9()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = 1
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject10()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DateTime.Now
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject11()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = 1
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject12()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 1
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject13()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = "1"c
        Try
            o3 = Operators.RightShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject14()
        Dim o1, o2, o3 As Object
        o1 = "4"
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject15()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = "1"
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject16()
        Dim o1, o2, o3 As Object
        o1 = True
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), (-1S >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject17()
        Dim o1, o2, o3 As Object
        o1 = 4D
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (4L >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject18()
        Dim o1, o2, o3 As Object
        o1 = 4.4D
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (4L >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject19()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject20()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = Int32.MinValue + 1
        o3 = Operators.RightShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 >> (Int32.MinValue + 1)))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsRightShiftObject21()
        Dim o1, o3 As Object
        o1 = New OperatorsImplementer()
        o3 = Operators.RightShiftObject(o1, 1)
        Assert.AreEqual(o3, "ok>>")
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject1()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = -1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 << -1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject2()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = Nothing
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 << 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject3()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject4()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 << 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject5()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = False
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (0 << 0))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject6()
        Dim o1, o2, o3 As Object
        o1 = DBNull.Value
        o2 = 1
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject7()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DBNull.Value
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DateTime.Now
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject9()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = 1
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject10()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = DateTime.Now
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject11()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = 1
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject12()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 1
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject13()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = "1"c
        Try
            o3 = Operators.LeftShiftObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject14()
        Dim o1, o2, o3 As Object
        o1 = "4"
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Integer), (4 << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject15()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = "1"
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject16()
        Dim o1, o2, o3 As Object
        o1 = True
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), (-1S << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject17()
        Dim o1, o2, o3 As Object
        o1 = 4D
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (4L << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject18()
        Dim o1, o2, o3 As Object
        o1 = 4.4D
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (4L << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject19()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 << 1))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject20()
        Dim o1, o2, o3 As Object
        o1 = 4
        o2 = Int32.MinValue + 1
        o3 = Operators.LeftShiftObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (4 << (Int32.MinValue + 1)))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsLeftShiftObject21()
        Dim o1, o3 As Object
        o1 = New OperatorsImplementer()
        o3 = Operators.LeftShiftObject(o1, 1)
        Assert.AreEqual(o3, "ok<<")
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        Try
            o3 = Operators.SubtractObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.SubtractObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 0D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract4()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract5()
        Dim o1, o2, o3 As Object
        o1 = 1.1
        o2 = 1.1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 0D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is TimeSpan) Then
            Assert.AreEqual(CType(o3, TimeSpan).Ticks, (CType(o1, DateTime).Ticks - CType(o2, DateTime).Ticks))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract7()
        Dim o1, o2, o3 As Object
        o1 = "a"c
        o2 = "b"c
        Try
            o3 = Operators.SubtractObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = Nothing
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(o3, 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(o3, -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.SubtractObject(o1, o2)
        Assert.AreEqual(o3, 0)
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract11()
        Dim o3 As Object
        Dim o1, o2 As Integer
        o1 = Integer.MaxValue
        o2 = Integer.MinValue
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(o3, CType(Integer.MaxValue, Long) - CType(Integer.MinValue, Long))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract12()
        Dim o3 As Object
        Dim o1, o2 As Short
        o1 = 1
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract13()
        Dim o3 As Object
        Dim o1, o2 As Short
        o1 = 1
        o2 = -1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract14()
        Dim o3 As Object
        Dim o1, o2 As UShort
        o1 = 1
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is UShort) Then
            Assert.AreEqual(CType(o3, UShort), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract15()
        Dim o3 As Object
        Dim o1 As Short
        Dim o2 As Long
        o1 = -2
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), -3L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract16()
        Dim o1, o2 As Long
        Dim o3 As Object
        o1 = 1L
        o2 = 1L
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 0L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract17()
        Dim o1, o2 As Boolean
        Dim o3 As Object
        o1 = True
        o2 = False
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract18()
        Dim o3 As Object
        Try
            o3 = Operators.SubtractObject(DBNull.Value, DBNull.Value)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract19()
        Dim o1, o2 As Byte
        Dim o3 As Object
        o1 = 0
        o2 = 1
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract20()
        Dim o1, o2 As Byte
        Dim o3 As Object
        o1 = Byte.MaxValue
        o2 = 0
        o3 = Operators.SubtractObject(o1, o2)
        If (TypeOf o3 Is Byte) Then
            Assert.AreEqual(CType(o3, Byte), Byte.MaxValue)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract21()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "abc"
        o3 = Operators.SubtractObject(o1, o2)
        Assert.AreEqual(o3, "ok-")
    End Sub

    <Test()> _
    Sub TestOperatorsSubtract22()
        Dim o1, o2, o3 As Object
        o1 = "abc"
        o2 = New OperatorsImplementer()
        Try
            o3 = Operators.SubtractObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAdd1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, "ab")
    End Sub

    <Test()> _
    Sub TestOperatorsAdd2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.AddObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAdd3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 2D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd4()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(o3, 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd5()
        Dim o1, o2, o3 As Object
        o1 = 1.1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(o3, 2.1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, String.Concat(o1.ToString(), o2.ToString()))
    End Sub

    <Test()> _
    Sub TestOperatorsAdd7()
        Dim o1, o2, o3 As Object
        o1 = "a"c
        o2 = "b"c
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, "ab")
    End Sub

    <Test()> _
    Sub TestOperatorsAdd8()
        Dim o1, o2, o3, o4, o5, o6 As Object
        o1 = "b"c
        o2 = Nothing
        o3 = Operators.AddObject(o1, o2)
        o4 = "b"c
        o5 = DBNull.Value
        o6 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, o6)
    End Sub

    <Test()> _
    Sub TestOperatorsAdd9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, 1)
    End Sub

    <Test()> _
    Sub TestOperatorsAdd10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, 0)
    End Sub

    <Test()> _
    Sub TestOperatorsAdd11()
        Dim o3 As Object
        Dim o1, o2 As Integer
        o1 = Integer.MaxValue
        o2 = Integer.MaxValue
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), Integer.MaxValue * 2L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd12()
        Dim o3 As Object
        Dim o1, o2 As Short
        o1 = 1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd13()
        Dim o3 As Object
        Dim o1, o2 As Short
        o1 = 1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd14()
        Dim o3 As Object
        Dim o1, o2 As UShort
        o1 = 1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is UShort) Then
            Assert.AreEqual(CType(o3, UShort), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd15()
        Dim o3 As Object
        Dim o1 As Short
        Dim o2 As Long
        o1 = -2
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), -1L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd16()
        Dim o1, o2 As Long
        Dim o3 As Object
        o1 = 1L
        o2 = 1L
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 2L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd17()
        Dim o1, o2 As Boolean
        Dim o3 As Object
        o1 = True
        o2 = False
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), -1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd18()
        Dim o3 As Object
        Try
            o3 = Operators.AddObject(DBNull.Value, DBNull.Value)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAdd19()
        Dim o1, o2 As Byte
        Dim o3 As Object
        o1 = Byte.MaxValue
        o2 = Byte.MaxValue
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), Byte.MaxValue * 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd20()
        Dim o1, o2 As Byte
        Dim o3 As Object
        o1 = Byte.MaxValue
        o2 = 0
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Byte) Then
            Assert.AreEqual(CType(o3, Byte), Byte.MaxValue)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd21()
        Dim o1, o2 As Double
        Dim o3 As Object
        o1 = Double.MaxValue
        o2 = Double.MaxValue
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), Double.MaxValue * 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd22()
        Dim o1, o2 As UShort
        Dim o3 As Object
        o1 = UShort.MaxValue
        o2 = UShort.MaxValue
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), UShort.MaxValue * 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd23()
        Dim o1 As Double
        Dim o2 As Decimal
        Dim o3 As Object
        o1 = 1
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 2)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd24()
        Dim o1 As Boolean
        Dim o2 As String
        Dim o3 As Object
        o1 = False
        o2 = "1"
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd25()
        Dim o1 As Boolean
        Dim o2 As Integer
        Dim o3 As Object
        o1 = False
        o2 = 1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd26()
        Dim o1 As ULong
        Dim o2 As SByte
        Dim o3 As Object
        o1 = UInteger.MaxValue
        o2 = -1
        o3 = Operators.AddObject(o1, o2)
        If (TypeOf o3 Is Decimal) Then
            Assert.AreEqual(CType(o3, Decimal), UInteger.MaxValue - 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAdd27()
        Dim o1 As Char
        Dim o2 As Integer
        Dim o3 As Object
        o1 = "1"c
        o2 = -1
        Try
            o3 = Operators.AddObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAdd28()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "abc"
        o3 = Operators.AddObject(o1, o2)
        Assert.AreEqual(o3, "ok+")
    End Sub

    <Test()> _
    Sub TestOperatorsAdd29()
        Dim o1, o2, o3 As Object
        o1 = "abc"
        o2 = New OperatorsImplementer()
        Try
            o3 = Operators.AddObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsOr1()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = "1"
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L Or 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.OrObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsOr3()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = 1
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L Or 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr4()
        Dim o1, o2, o3 As Object
        o1 = 52
        o2 = 14
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (52 Or 14))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr5()
        Dim o1, o2, o3 As Object
        o1 = 14134.3124
        o2 = 14.59
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (14134L Or 15L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.OrObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsOr7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = "2"c
        Try
            o3 = Operators.OrObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsOr9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 134S
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), (134S))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.OrObject(o1, o2)
        Assert.AreEqual(o3, 0)
    End Sub

    <Test()> _
    Sub TestOperatorsOr14()
        Dim o3 As Object
        Dim o1, o2 As UShort
        o1 = 643US
        o2 = 24US
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is UShort) Then
            Assert.AreEqual(CType(o3, UShort), (24US Or 643US))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr15()
        Dim o3 As Object
        Dim o1 As Short
        Dim o2 As Long
        o1 = -2
        o2 = 1
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), -1L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr17()
        Dim o1, o2 As Object
        Dim o3 As Object
        o1 = True
        o2 = False
        o3 = Operators.OrObject(o1, o2)
        If (TypeOf o3 Is Boolean) Then
            Assert.AreEqual(CType(o3, Boolean), True)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsOr18()
        Dim o3 As Object
        Try
            o3 = Operators.OrObject(DBNull.Value, DBNull.Value)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsOr28()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = 1
        o3 = Operators.OrObject(o1, o2)
        Assert.AreEqual(o3, "ok||")
    End Sub

    <Test()> _
    Sub TestOperatorsXor1()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = "1"
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L Xor 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.XorObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsXor3()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = 1
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L Xor 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor4()
        Dim o1, o2, o3 As Object
        o1 = 52
        o2 = 14
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (52 Xor 14))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor5()
        Dim o1, o2, o3 As Object
        o1 = 14134.3124
        o2 = 14.59
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (14134L Xor 15L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.XorObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsXor7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = "2"c
        Try
            o3 = Operators.XorObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsXor9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 134S
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), (134S))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.XorObject(o1, o2)
        Assert.AreEqual(o3, 0)
    End Sub

    <Test()> _
    Sub TestOperatorsXor14()
        Dim o3 As Object
        Dim o1, o2 As UShort
        o1 = 643US
        o2 = 24US
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is UShort) Then
            Assert.AreEqual(CType(o3, UShort), (24US Xor 643US))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor15()
        Dim o3 As Object
        Dim o1 As Short
        Dim o2 As Long
        o1 = -2
        o2 = 1
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), -1L)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor17()
        Dim o1, o2 As Object
        Dim o3 As Object
        o1 = True
        o2 = False
        o3 = Operators.XorObject(o1, o2)
        If (TypeOf o3 Is Boolean) Then
            Assert.AreEqual(CType(o3, Boolean), True)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsXor18()
        Dim o3 As Object
        Try
            o3 = Operators.XorObject(DBNull.Value, DBNull.Value)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsXor28()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = 1
        o3 = Operators.XorObject(o1, o2)
        Assert.AreEqual(o3, "okXor")
    End Sub

    <Test()> _
    Sub TestOperatorsAnd1()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = "1"
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L And 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.AndObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAnd3()
        Dim o1, o2, o3 As Object
        o1 = "1.1"
        o2 = 1
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (1L And 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd4()
        Dim o1, o2, o3 As Object
        o1 = 52
        o2 = 14
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), (52 And 14))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd5()
        Dim o1, o2, o3 As Object
        o1 = 14134.3124
        o2 = 14.59
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (14134L And 15L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.AndObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAnd7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = "2"c
        Try
            o3 = Operators.AndObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAnd9()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = 134S
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 0S)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd10()
        Dim o1, o2, o3 As Object
        o1 = Nothing
        o2 = Nothing
        o3 = Operators.AndObject(o1, o2)
        Assert.AreEqual(o3, 0)
    End Sub

    <Test()> _
    Sub TestOperatorsAnd15()
        Dim o3 As Object
        Dim o1 As Short
        Dim o2 As Long
        o1 = -2
        o2 = 1
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), (-2L And 1L))
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd17()
        Dim o1, o2 As Object
        Dim o3 As Object
        o1 = True
        o2 = False
        o3 = Operators.AndObject(o1, o2)
        If (TypeOf o3 Is Boolean) Then
            Assert.AreEqual(CType(o3, Boolean), False)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsAnd18()
        Dim o3 As Object
        Try
            o3 = Operators.AndObject(DBNull.Value, DBNull.Value)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsAnd28()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = 1
        o3 = Operators.AndObject(o1, o2)
        Assert.AreEqual(o3, "okAnd")
    End Sub

    <Test()> _
    Sub TestOperatorsDivide1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        Try
            o3 = Operators.DivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsDivide2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.DivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsDivide3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.DivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsDivide7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 1
        Try
            o3 = Operators.DivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsDivide8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = Nothing
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), Double.PositiveInfinity)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide11()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1.0
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide12()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Decimal) Then
            Assert.AreEqual(CType(o3, Decimal), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide13()
        Dim o1, o2, o3 As Object
        o1 = 1.1F
        o2 = 1.1F
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide14()
        Dim o1, o2, o3 As Object
        o1 = False
        o2 = 1.0F
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide15()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = "1"
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide16()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = 1
        o3 = Operators.DivideObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsDivide18()
        Dim o3 As Object
        Try
            o3 = Operators.DivideObject(DBNull.Value, 1.1D)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsDivide19()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "abc"
        o3 = Operators.DivideObject(o1, o2)
        Assert.AreEqual(o3, "ok/")
    End Sub

    <Test()> _
    Sub TestOperatorsMod1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        Try
            o3 = Operators.ModObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMod2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.ModObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMod3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.ModObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 0D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMod6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = 2
        Try
            o3 = Operators.ModObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMod7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 2
        Try
            o3 = Operators.ModObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMod8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = Nothing
        Try
            o3 = Operators.ModObject(o1, o2)
        Catch ex As DivideByZeroException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMod11()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(3)
        o2 = 2.0F
        o3 = Operators.ModObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMod12()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(3)
        o2 = -2.5F
        o3 = Operators.ModObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0.5F)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMod13()
        Dim o1, o2, o3 As Object
        o1 = -1.1F
        o2 = 1.1D
        o3 = Operators.ModObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMod14()
        Dim o1, o2, o3 As Object
        o1 = False
        o2 = 1.0F
        o3 = Operators.ModObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMod16()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "2"
        o3 = Operators.ModObject(o1, o2)
        Assert.AreEqual(o3, "okMod")
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        Try
            o3 = Operators.MultiplyObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.MultiplyObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1D)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.MultiplyObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 1
        Try
            o3 = Operators.MultiplyObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply8()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = Nothing
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0.0F)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply11()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1.0
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply12()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Decimal) Then
            Assert.AreEqual(CType(o3, Decimal), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply13()
        Dim o1, o2, o3 As Object
        o1 = 1US
        o2 = 1.1F
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 1.1F)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply14()
        Dim o1, o2, o3 As Object
        o1 = False
        o2 = 1.0F
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply15()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = "1"
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Double) Then
            Assert.AreEqual(CType(o3, Double), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsMultiply16()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = 1
        o3 = Operators.MultiplyObject(o1, o2)
        If (TypeOf o3 Is Single) Then
            Assert.AreEqual(CType(o3, Single), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide1()
        Dim o1, o2, o3 As Object
        o1 = "a"
        o2 = "b"
        Try
            o3 = Operators.IntDivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide2()
        Dim o1, o2, o3 As Object
        o1 = "d"
        o2 = 1
        Try
            o3 = Operators.IntDivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide3()
        Dim o1, o2, o3 As Object
        o1 = "1"
        o2 = 1
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide6()
        Dim o1, o2, o3 As Object
        o1 = DateTime.Now
        o2 = DateTime.Now
        Try
            o3 = Operators.IntDivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide7()
        Dim o1, o2, o3 As Object
        o1 = "1"c
        o2 = 1
        Try
            o3 = Operators.IntDivideObject(o1, o2)
        Catch ex As InvalidCastException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide8()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = Nothing
        Try
            o3 = Operators.IntDivideObject(o1, o2)
        Catch ex As DivideByZeroException
            Return
        End Try
        Assert.Fail()
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide11()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1.0
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide12()
        Dim o1, o2, o3 As Object
        o1 = New Decimal(1)
        o2 = 1
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide13()
        Dim o1, o2, o3 As Object
        o1 = 1.1F
        o2 = 1.1F
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide14()
        Dim o1, o2, o3 As Object
        o1 = False
        o2 = 1.0F
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 0)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide15()
        Dim o1, o2, o3 As Object
        o1 = 1.0F
        o2 = "1"
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Long) Then
            Assert.AreEqual(CType(o3, Long), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide16()
        Dim o1, o2, o3 As Object
        o1 = CType(1, Byte)
        o2 = CType(1, Byte)
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Byte) Then
            Assert.AreEqual(CType(o3, Byte), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide17()
        Dim o1, o2, o3 As Object
        o1 = 1
        o2 = CType(1, Byte)
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Integer) Then
            Assert.AreEqual(CType(o3, Integer), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide18()
        Dim o1, o2, o3 As Object
        o1 = CType(1, SByte)
        o2 = CType(1, Byte)
        o3 = Operators.IntDivideObject(o1, o2)
        If (TypeOf o3 Is Short) Then
            Assert.AreEqual(CType(o3, Short), 1)
        Else
            Assert.Fail()
        End If
    End Sub

    <Test()> _
    Sub TestOperatorsIntDivide19()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer()
        o2 = "abc"
        o3 = Operators.IntDivideObject(o1, o2)
        Assert.AreEqual(o3, "ok\")
    End Sub

    <Test()> _
   Sub TestOperatorsLikeObject()
        Dim o1, o2, o3 As Object
        o1 = New OperatorsImplementer
        o2 = New Object
        o3 = New Object

        Assert.IsTrue(o1 Like o2)
        Try
            Dim b1 As Boolean = o2 Like o1
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

        Try
            Dim b2 As Boolean = o3 Like o2
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try
    End Sub

    <Test()> _
    Sub TestOperatorsLikeString()
        Assert.IsTrue(LikeOperator.LikeString("-1d", "-#*", CompareMethod.Binary), "1")
        Assert.IsTrue(LikeOperator.LikeString("1", "#", CompareMethod.Binary), "2")
        Assert.IsFalse(LikeOperator.LikeString("12", "#", CompareMethod.Binary), "3")
        Assert.IsFalse(LikeOperator.LikeString("aa", "?", CompareMethod.Binary), "4")
        Assert.IsTrue(LikeOperator.LikeString("F", "F", CompareMethod.Binary), "5")
        Assert.IsTrue(LikeOperator.LikeString("F", "F", CompareMethod.Text), "6")
        Assert.IsFalse(LikeOperator.LikeString("F", "f", CompareMethod.Binary), "7")
        Assert.IsTrue(LikeOperator.LikeString("F", "f", CompareMethod.Text), "8")
        Assert.IsFalse(LikeOperator.LikeString("F", "FFF", CompareMethod.Binary), "9")
        Assert.IsTrue(LikeOperator.LikeString("aBBBa", "a*a", CompareMethod.Binary), "10")
        Assert.IsTrue(LikeOperator.LikeString("F", "[A-Z]", CompareMethod.Binary), "11")
        Assert.IsFalse(LikeOperator.LikeString("F", "[!A-Z]", CompareMethod.Binary), "12")
        Assert.IsTrue(LikeOperator.LikeString("a2a", "a#a", CompareMethod.Binary), "13")
        Assert.IsTrue(LikeOperator.LikeString("aM5b", "a[L-P]#[!c-e]", CompareMethod.Binary), "14")
        Assert.IsTrue(LikeOperator.LikeString("BAT123khg", "B?T*", CompareMethod.Binary), "15")
        Assert.IsFalse(LikeOperator.LikeString("CAT123khg", "B?T*", CompareMethod.Binary), "16")
    End Sub

    <Test()> _
    Sub TestOperatorsCompare()
        Dim _false As Object = False
        Dim _true As Object = True
        Dim _int1 As Object = -4
        Dim _double1 As Object = -4
        Dim _byte1 As Object = 0
        Dim _date As Object = DateTime.Now
        Dim _str As Object = "word"
        Dim _str2 As Object = "aaaa"
        Dim _strNum As Object = "1"
        Dim _nothing As Object = Nothing
        Dim _nothing2 As Object = Nothing
        Dim _dbnull As Object = DBNull.Value
        Dim _dbnull2 As Object = DBNull.Value
        Dim _a As Object = New A
        Dim _operatorsImplementer As Object = New OperatorsImplementer

        Assert.IsTrue(1 > _nothing)

        Assert.IsTrue(_nothing2 = _nothing)

        Assert.IsTrue(_nothing < 1)

        Assert.IsTrue(_nothing > -1)

        Assert.IsTrue(_false > _true)

        Assert.IsTrue(_operatorsImplementer > _date)

        Assert.IsTrue(_str > _str2)

        Assert.IsTrue("a"c < _str)

        Assert.IsTrue(2 > _strNum)

        Assert.IsTrue(_int1 <= _byte1)

        Assert.IsTrue(_double1 = _int1)

        Try
            Dim tmp = _a > _nothing
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

        Try
            Dim tmp = 1 = _a
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

        Try
            Dim tmp = _str > _date
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

        Try
            Dim tmp = 1 > _date
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

        Try
            Dim tmp = _dbnull >= _dbnull2
            Assert.Fail()
        Catch ex As InvalidCastException
        End Try

    End Sub

End Class

Class A
End Class

Class OperatorsImplementer
    Public Shared Operator =(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator <>(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator >(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator <(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator >=(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator <=(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "True"
    End Operator

    Public Shared Operator +(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok+"
    End Operator

    Public Shared Operator *(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok*"
    End Operator

    Public Shared Operator /(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok/"
    End Operator

    Public Shared Operator \(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok\"
    End Operator

    Public Shared Operator &(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok&"
    End Operator

    Public Shared Operator -(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok-"
    End Operator

    Public Shared Operator Or(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "ok||"
    End Operator

    Public Shared Operator Xor(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "okXor"
    End Operator

    Public Shared Operator Mod(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "okMod"
    End Operator

    Public Shared Operator And(ByVal a As OperatorsImplementer, ByVal b As Object) As Object
        Return "okAnd"
    End Operator

    Public Shared Operator >>(ByVal a As OperatorsImplementer, ByVal b As Integer) As Object
        Return "ok>>"
    End Operator

    Public Shared Operator <<(ByVal a As OperatorsImplementer, ByVal b As Integer) As Object
        Return "ok<<"
    End Operator

    Public Shared Operator -(ByVal a As OperatorsImplementer) As Object
        Return "-ok"
    End Operator

    Public Shared Operator Not(ByVal a As OperatorsImplementer) As Object
        Return "!ok"
    End Operator

    Public Shared Operator +(ByVal a As OperatorsImplementer) As Object
        Return "+ok"
    End Operator

    Public Shared Operator Like(ByVal a As OperatorsImplementer, ByVal b As Object) As Boolean
        Return True
    End Operator

End Class
