Namespace Assignments
    <TestFixture()> Public Class SByteAssignment
        Dim i As SByte
        Shared j As SByte
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 20
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 20
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 20
            Assert.AreEqual(i, 20, "i should be 20.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 20
            Assert.AreEqual(j, 20, "j should be 20.")
        End Sub
    End Class
    <TestFixture()> Public Class Int16Assignment
        Dim i As Short
        Shared j As Short
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 20000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 20000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 20000
            Assert.AreEqual(i, 20000, "i should be 20000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 20000
            Assert.AreEqual(j, 20000, "j should be 20000.")
        End Sub
    End Class
    <TestFixture()> Public Class Int32Assignment
        Dim i As Integer
        Shared j As Integer
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000
            Assert.AreEqual(i, 200000, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000
            Assert.AreEqual(j, 200000, "j should be 200000.")
        End Sub
    End Class
    <TestFixture()> Public Class Int64Assignment
        Dim i As Long
        Shared j As Long
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000
            Assert.AreEqual(i, 200000, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000
            Assert.AreEqual(j, 200000, "j should be 200000.")
        End Sub
    End Class

    <TestFixture()> Public Class ByteAssignment
        Dim i As Byte
        Shared j As Byte
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 20
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 20
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 20
            Assert.AreEqual(i, 20, "i should be 20.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 20
            Assert.AreEqual(j, 20, "j should be 20.")
        End Sub
    End Class
    <TestFixture()> Public Class UInt16Assignment
        Dim i As UShort
        Shared j As UShort
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 20000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 20000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 20000
            Assert.AreEqual(i, 20000, "i should be 20000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 20000
            Assert.AreEqual(j, 20000, "j should be 20000.")
        End Sub
    End Class
    <TestFixture()> Public Class UInt32Assignment
        Dim i As UInteger
        Shared j As UInteger
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000
            Assert.AreEqual(i, 200000, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000
            Assert.AreEqual(j, 200000, "j should be 200000.")
        End Sub
    End Class
    <TestFixture()> Public Class UInt64Assignment
        Dim i As ULong
        Shared j As ULong
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000
            Assert.AreEqual(i, 200000, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000
            Assert.AreEqual(j, 200000, "j should be 200000.")
        End Sub
    End Class

    <TestFixture()> Public Class DoubleAssignment
        Dim i As Double
        Shared j As Double
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000.0
            j = i
            Assert.AreEqual(i, j, 0.0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000.0
            i = j
            Assert.AreEqual(i, j, 0.0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000.0
            Assert.AreEqual(i, 200000.0, 0.0, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000.0
            Assert.AreEqual(j, 200000.0, 0.0, "j should be 200000.")
        End Sub
    End Class
    <TestFixture()> Public Class SingleAssignment
        Dim i As Single
        Shared j As Single
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000.0
            j = i
            Assert.AreEqual(i, j, 0.0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000.0
            i = j
            Assert.AreEqual(i, j, 0.0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000.0
            Assert.AreEqual(i, 200000.0, 0.0, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000.0
            Assert.AreEqual(j, 200000.0, 0.0, "j should be 200000.")
        End Sub
    End Class
    <TestFixture()> Public Class DecimalAssignment
        Dim i As Decimal
        Shared j As Decimal
        <Test()> Public Sub InstanceToSharedAssignment()
            i = 200000
            j = i
            Assert.AreEqual(i, j, 0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = 200000
            i = j
            Assert.AreEqual(i, j, 0, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = 200000
            Assert.AreEqual(i, 200000, 0, "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = 200000
            Assert.AreEqual(j, 200000, 0, "j should be 200000.")
        End Sub
    End Class

    <TestFixture()> Public Class StringAssignment
        Dim i As String
        Shared j As String
        <Test()> Public Sub InstanceToSharedAssignment()
            i = "200000"
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = "200000"
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = "200000"
            Assert.AreEqual(i, "200000", "i should be 200000.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = "200000"
            Assert.AreEqual(j, "200000", "j should be 200000.")
        End Sub
    End Class
    <TestFixture()> Public Class DateTimeAssignment
        Dim i As Date
        Dim j As Date
        <Test()> Public Sub InstanceToSharedAssignment()
            i = #1/1/2005#
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = #1/1/2005#
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = #1/1/2005#
            Assert.AreEqual(i, #1/1/2005#, "i should be #1/1/2005#.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = #1/1/2005#
            Assert.AreEqual(j, #1/1/2005#, "j should be #1/1/2005#.")
        End Sub
    End Class
    <TestFixture()> Public Class BooleanAssignment
        Dim i As Boolean
        Shared j As Boolean
        <Test()> Public Sub InstanceToSharedAssignment()
            i = True
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = True
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = True
            Assert.AreEqual(i, True, "i should be True.")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = True
            Assert.AreEqual(j, True, "j should be True.")
        End Sub
    End Class
    <TestFixture()> Public Class CharAssignment
        Dim i As Char
        Shared j As Char
        <Test()> Public Sub InstanceToSharedAssignment()
            i = "i"c
            j = i
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub SharedToInstanceAssignment()
            j = "i"c
            i = j
            Assert.AreEqual(i, j, "i and j should be equal.")
        End Sub
        <Test()> Public Sub InstanceConstantAssignment()
            i = "i"c
            Assert.AreEqual(i, "i"c, "i should be ""c"".")
        End Sub
        <Test()> Public Sub SharedConstantAssignment()
            j = "i"c
            Assert.AreEqual(j, "i"c, "j should be ""c"".")
        End Sub
    End Class
End Namespace