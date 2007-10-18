Class BuiltInType1
	Sub test()
		Dim a As Byte = Byte.maxvalue
		Dim b As SByte
		Dim c As Short = Short.maxvalue
		Dim d As UShort
		Dim e As Integer = Integer.maxvalue
		Dim f As UInteger
		Dim g As Long = Long.maxvalue
		Dim h As ULong
		Dim i As String = String.Empty
		Dim j As Char
		Dim k As Object = Object.Equals(Nothing, Nothing)
		Dim l As Boolean
		Dim m As Date = Date.Now
		Dim n As Single
		Dim o As Double = Double.PositiveInfinity

		b = SByte.MinValue
		d = UShort.MinValue
		f = UInteger.MinValue
		h = ULong.MaxValue
		j = Char.MaxValue
		l = Boolean.TryParse("Boolean", l)
		n = Single.NegativeInfinity
	End Sub
End Class