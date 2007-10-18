'Namespace Conversions
'	<TestFixture()> Public Class BooleanConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CBool("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class ByteConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class SByteConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CSByte("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class CharConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CChar("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class UShortConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CUShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class ShortConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CShort("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class UIntegerConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CUInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class IntegerConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CInt("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class ULongConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CULng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class LongConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CLng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class DecimalConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CDec("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class SingleConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CSng("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class DoubleConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CDbl("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class ObjectConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CObj("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class DateConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CDate("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'	<TestFixture()> Public Class StringConversion
'		<Test()> Public Sub ToBoolean()
'			Dim value As Boolean
'			value = CBool(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToByte()
'			Dim value As Byte
'			value = CByte(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSByte()
'			Dim value As SByte
'			value = CSByte(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToChar()
'			Dim value As Char
'			value = CChar(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUShort()
'			Dim value As UShort
'			value = CUShort(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToShort()
'			Dim value As Short
'			value = CShort(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToUInteger()
'			Dim value As UInteger
'			value = CUInt(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToInteger()
'			Dim value As Integer
'			value = CInt(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToULong()
'			Dim value As ULong
'			value = CULng(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToLong()
'			Dim value As Long
'			value = CLng(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDecimal()
'			Dim value As Decimal
'			value = CDec(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToSingle()
'			Dim value As Single
'			value = CSng(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDouble()
'			Dim value As Double
'			value = CDbl(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToObject()
'			Dim value As Object
'			value = CObj(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToDate()
'			Dim value As Date
'			value = CDate(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'		<Test()> Public Sub ToString()
'			Dim value As String
'			value = CStr(CStr("20"))
'			Assert.AreEqual(value, 20, "value should be 20")
'		End Sub
'	End Class
'End Namespace
