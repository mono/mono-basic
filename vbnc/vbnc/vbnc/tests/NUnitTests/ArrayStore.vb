''' <summary>
''' Array store.
''' </summary>
''' <remarks></remarks>
<TestFixture()> Public Class ArrayStore
    <Test()> Public Sub ByteTest()
        Dim i As Byte()
        i = New Byte() {0}
        i(0) = 1
        Assert.AreEqual(i(0), 1, "One dimensional byte array store, expected 1.")
    End Sub
    <Test()> Public Sub UShortTest()
        Dim i As UShort()
        i = New UShort() {0, 1}
        i(1) = 2
        Assert.AreEqual(i(1), 2, "One dimensional ushort array store, expected 2.")
    End Sub
    <Test()> Public Sub UIntegerTest()
        Dim i As UInteger()
        i = New UInteger() {0, 1, 2, 3}
        i(2) = 3
        Assert.AreEqual(i(2), 3, "One dimensional uinteger array store, expected 3.")
    End Sub
    <Test()> Public Sub ULongTest()
        Dim i As ULong()
        i = New ULong() {0, 1, 2, 3, 4}
        i(3) = 4
        Assert.AreEqual(i(3), 4, "One dimensional ulong array store, expected 4.")
    End Sub

    <Test()> Public Sub SByteTest()
        Dim i As SByte()
        i = New SByte() {0, 1}
        i(0) = 1
        Assert.AreEqual(i(0), 1, "One dimensional sbyte array store, expected 1.")
    End Sub
    <Test()> Public Sub ShortTest()
        Dim i As Short()
        i = New Short() {0, 1, 2}
        i(1) = 2
        Assert.AreEqual(i(1), 2, "One dimensional short array store, expected 2.")
    End Sub
    <Test()> Public Sub IntegerTest()
        Dim i As Integer()
        i = New Integer() {0, 1, 2, 3}
        i(2) = 3
        Assert.AreEqual(i(2), 3, "One dimensional integer array store, expected 3.")
    End Sub
    <Test()> Public Sub LongTest()
        Dim i As Long()
        i = New Long() {0, 1, 2, 3, 4}
        i(3) = 4
        Assert.AreEqual(i(3), 4, "One dimensional long array store, expected 4.")
    End Sub

    <Test()> Public Sub SingleTest()
        Dim i As Single()
        i = New Single() {0, 1, 2}
        i(1) = 2
        Assert.AreEqual(i(1), 2, "One dimensional single array store, expected 2.")
    End Sub
    <Test()> Public Sub DoubleTest()
        Dim i As Double()
        i = New Double() {0, 1, 2, 3}
        i(2) = 3
        Assert.AreEqual(i(2), 3, "One dimensional double array store, expected 3.")
    End Sub
    <Test()> Public Sub DecimalTest()
        Dim i As Decimal()
        i = New Decimal() {0}
        i(0) = 4
        Assert.AreEqual(i(0), 4, "One dimensional decimal array store, expected 4.")
    End Sub


    <Test()> Public Sub CharTest()
        Dim i As Char()
        i = New Char() {"0"c, "1"c}
        i(0) = "1"c
        Assert.AreEqual(i(0), "1"c, "One dimensional char array store, expected ""1""c.")
    End Sub
    <Test()> Public Sub StringTest()
        Dim i As String()
        i = New String() {"0", "1", "2"}
        i(1) = "2"
        Assert.AreEqual(i(1), "2", "One dimensional string array store, expected ""2"".")
    End Sub
    <Test()> Public Sub DateTest()
        Dim i As Date()
        i = New Date() {#9/9/2009#}
        i(0) = #3/3/2003#
        Assert.AreEqual(i(0), #3/3/2003#, "One dimensional date array store, expected #3/3/2003#.")
    End Sub
    <Test()> Public Sub BooleanTest()
        Dim i As Boolean()
        i = New Boolean() {False, True, False, True, False}
        i(3) = False
        Assert.AreEqual(i(3), False, "One dimensional boolean array store, expected False.")
    End Sub
End Class
