Namespace Conversions
	Namespace Implicit
		<TestFixture()> Public Class SByteConversions
			<Test()> Public Sub ToSByte()
				Dim i As SByte
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToInt16()
				Dim i As Short
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToInt32()
				Dim i As Integer
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToInt64()
				Dim i As Long
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToSingle()
				Dim i As Single
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToDouble()
				Dim i As Double
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToDecimal()
				Dim i As Decimal
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToObject()
				Dim i As Object
				i = CSByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
		End Class
        <TestFixture()> Public Class Int16Conversions
            <Test()> Public Sub ToInt16()
                Dim i As Short
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToInt32()
                Dim i As Integer
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToInt64()
                Dim i As Long
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class
        <TestFixture()> Public Class Int32Conversions
            <Test()> Public Sub ToInt32()
                Dim i As Integer
                i = 200000
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToInt64()
                Dim i As Long
                i = 200000
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 200000
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 200000
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 200000
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000I
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class
        <TestFixture()> Public Class Int64Conversions
            <Test()> Public Sub ToInt64()
                Dim i As Long
                i = 200000L
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 200000L
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 200000L
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 200000L
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000L
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class

		<TestFixture()> Public Class ByteConversions
			<Test()> Public Sub ToInt16()
				Dim i As Short
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToUInt16()
				Dim i As UShort
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToInt32()
				Dim i As Integer
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToUInt32()
				Dim i As UInteger
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToInt64()
				Dim i As Long
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToUInt64()
				Dim i As ULong
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToSingle()
				Dim i As Single
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToDouble()
				Dim i As Double
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToDecimal()
				Dim i As Decimal
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
			<Test()> Public Sub ToObject()
				Dim i As Object
				i = CByte(20)
				Assert.AreEqual(i, 20, "i should be 20")
			End Sub
		End Class
        <TestFixture()> Public Class UInt16Conversions
            <Test()> Public Sub ToUInt16()
                Dim i As UShort
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToUInt32()
                Dim i As UInteger
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToInt32()
                Dim i As Integer
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToUInt64()
                Dim i As ULong
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToInt64()
                Dim i As Long
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 20000S
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000US
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class
        <TestFixture()> Public Class UInt32Conversions
            <Test()> Public Sub ToUInt32()
                Dim i As UInteger
                i = 20000UI
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToUInt64()
                Dim i As ULong
                i = 20000UI
                Assert.AreEqual(i, 20000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToInt64()
                Dim i As Long
                i = 200000UI
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 200000UI
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 200000UI
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 200000UI
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000UI
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class
        <TestFixture()> Public Class UInt64Conversions
            <Test()> Public Sub ToUInt64()
                Dim i As ULong
                i = 200000UL
                Assert.AreEqual(i, 200000, "i should be 20000")
            End Sub
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 200000UL
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 200000UL
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 200000UL
                Assert.AreEqual(i, 200000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000UL
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class

		<TestFixture()> Public Class SingleConversions
			<Test()> Public Sub ToSingle()
				Dim i As Single
				i = 20000.0!
				Assert.AreEqual(i, 20000, "i should be 200000")
			End Sub
			<Test()> Public Sub ToDouble()
				Dim i As Double
				i = 20000.0!
				Assert.AreEqual(i, 20000, "i should be 200000")
			End Sub
			<Test()> Public Sub ToObject()
				Dim i As Object
				i = 20000.0!
				Assert.AreEqual(i, 20000, "i should be 200000")
			End Sub

		End Class
        <TestFixture()> Public Class DoubleConversions
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 20000.0#
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000.0#
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub

        End Class
        <TestFixture()> Public Class DecimalConversions
            <Test()> Public Sub ToSingle()
                Dim i As Single
                i = 20000@
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDouble()
                Dim i As Double
                i = 20000@
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToDecimal()
                Dim i As Decimal
                i = 20000@
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub
            <Test()> Public Sub ToObject()
                Dim i As Object
                i = 20000@
                Assert.AreEqual(i, 20000, "i should be 200000")
            End Sub
        End Class
	End Namespace
End Namespace
