''' <summary>
''' Array index expressions.
''' </summary>
''' <remarks></remarks>
<TestFixture()> Public Class ArrayIndexExpressions
    <Test()> Public Sub ByteTest()
        Dim i As Byte()
        i = New Byte() {1}
        Assert.AreEqual(i(0), 1, "One dimensional byte array access, expected 1.")
    End Sub
    <Test()> Public Sub UShortTest()
        Dim i As UShort()
        i = New UShort() {1, 2}
        Assert.AreEqual(i(1), 2, "One dimensional ushort array access, expected 2.")
    End Sub
    <Test()> Public Sub UIntegerTest()
        Dim i As UInteger()
        i = New UInteger() {1, 2, 3}
        Assert.AreEqual(i(2), 3, "One dimensional uinteger array access, expected 3.")
    End Sub
    <Test()> Public Sub ULongTest()
        Dim i As ULong()
        i = New ULong() {1, 2, 3, 4}
        Assert.AreEqual(i(3), 4, "One dimensional ulong array access, expected 4.")
    End Sub

    <Test()> Public Sub SByteTest()
        Dim i As SByte()
        i = New SByte() {1}
        Assert.AreEqual(i(0), 1, "One dimensional sbyte array access, expected 1.")
    End Sub
    <Test()> Public Sub ShortTest()
        Dim i As Short()
        i = New Short() {1, 2}
        Assert.AreEqual(i(1), 2, "One dimensional short array access, expected 2.")
    End Sub
    <Test()> Public Sub IntegerTest()
        Dim i As Integer()
        i = New Integer() {1, 2, 3}
        Assert.AreEqual(i(2), 3, "One dimensional integer array access, expected 3.")
    End Sub
    <Test()> Public Sub LongTest()
        Dim i As Long()
        i = New Long() {1, 2, 3, 4}
        Assert.AreEqual(i(3), 4, "One dimensional long array access, expected 4.")
    End Sub

    <Test()> Public Sub SingleTest()
        Dim i As Single()
        i = New Single() {1, 2}
        Assert.AreEqual(i(1), 2, "One dimensional single array access, expected 2.")
    End Sub
    <Test()> Public Sub DoubleTest()
        Dim i As Double()
        i = New Double() {1, 2, 3}
        Assert.AreEqual(i(2), 3, "One dimensional double array access, expected 3.")
    End Sub
    <Test()> Public Sub DecimalTest()
        Dim i As Decimal()
        i = New Decimal() {1, 2, 3, 4}
        Assert.AreEqual(i(3), 4, "One dimensional decimal array access, expected 4.")
    End Sub


    <Test()> Public Sub CharTest()
        Dim i As Char()
        i = New Char() {"1"c}
        Assert.AreEqual(i(0), "1"c, "One dimensional char array access, expected ""1""c.")
    End Sub
    <Test()> Public Sub StringTest()
        Dim i As String()
        i = New String() {"1", "2"}
        Assert.AreEqual(i(1), "2", "One dimensional string array access, expected ""2"".")
    End Sub
    <Test()> Public Sub DateTest()
        Dim i As Date()
        i = New Date() {#1/1/2001#, #2/2/2002#, #3/3/2003#}
        Assert.AreEqual(i(2), #3/3/2003#, "One dimensional date array access, expected #3/3/2003#.")
    End Sub
    <Test()> Public Sub BooleanTest()
        Dim i As Boolean()
        i = New Boolean() {True, False, True, False}
        Assert.AreEqual(i(3), False, "One dimensional boolean array access, expected False.")
    End Sub
End Class
