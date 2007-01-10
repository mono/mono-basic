<TestFixture()> Public Class ArrayCreation1
   
    <Test()> Public Sub Create1()
        Dim i As Integer()
        i = New Integer() {}
        Assert.AreEqual(i.Length, 0, "Array creation failed, expected Length = 0")
    End Sub

    <Test()> Public Sub Create2()
        Dim i As Integer()
        i = New Integer() {1}
        Assert.AreEqual(i.Length, 1, "Array creation failed, expected Length = 1")
        Assert.AreEqual(i(0), 1, "Array creation failed, expected first item to be 1")
    End Sub

    <Test()> Public Sub Create3()
        Dim i As Integer()
        i = New Integer() {-255, -254, -129, -128, -127, -5, -4, -3, -2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 127, 128, 254, 255, 256}
        Assert.AreEqual(i.Length, 24, "Array creation failed, expected Length = 24")
        Assert.AreEqual(i(0), -255, "Array creation failed, expected first item to be -255")
        Assert.AreEqual(i(23), 256, "Array creation failed, expected last item to be 256")
    End Sub

    <Test()> Public Sub Create4()
        Dim b As Boolean()
        b = New Boolean() {True, False, True, False}
        Assert.AreEqual(b.GetUpperBound(0), 3, "Array creation failed, expected Ubound = 3")
        Assert.AreEqual(b.Rank, 1, "Array creation failed, expected Rank = 1")
        Assert.AreEqual(True, b(0), "Array creation failed, expected first item to be True")
        Assert.AreEqual(False, b(1), "Array creation failed, expected second item to be False")
        Assert.AreEqual(True, b(2), "Array creation failed, expected third item to be True")
        Assert.AreEqual(False, b(3), "Array creation failed, expected fourth item to be False")
    End Sub

    <Test()> Public Sub CreateSByte()
        Dim c As SByte()
        c = New SByte() {1}
        Assert.AreEqual(c(0), 1, "SByte array creation failed, expected 1")
    End Sub
    <Test()> Public Sub CreateShort()
        Dim s As Short()
        s = New Short() {3}
        Assert.AreEqual(s(0), 3, "Short array creation failed, expected 3")
    End Sub
    <Test()> Public Sub CreateInteger()
        Dim i As Integer()
        i = New Integer() {1}
        Assert.AreEqual(i(0), 1, "Integer array creation failed, expected 1")
    End Sub
    <Test()> Public Sub CreateLong()
        Dim i As Long()
        i = New Long() {1}
        Assert.AreEqual(i(0), 1, "Long array creation failed, expected 1")
    End Sub

    <Test()> Public Sub CreateByte()
        Dim b As Byte()
        b = New Byte() {2}
        Assert.AreEqual(b(0), 2, "Byte array creation failed, expected 2")
    End Sub
    <Test()> Public Sub CreateUShort()
        Dim s As UShort()
        s = New UShort() {4}
        Assert.AreEqual(s(0), 4, "UShort array creation failed, expected 4")
    End Sub
    <Test()> Public Sub CreateUInteger()
        Dim i As UInteger()
        i = New UInteger() {1}
        Assert.AreEqual(i(0), 1, "UInteger array creation failed, expected 1")
    End Sub
    <Test()> Public Sub CreateULong()
        Dim i As ULong()
        i = New ULong() {1}
        Assert.AreEqual(i(0), 1, "ULong array creation failed, expected 1")
    End Sub

    <Test()> Public Sub CreateSingle()
        Dim i As Single()
        i = New Single() {1.1, 1}
        Assert.AreEqual(i(0), 1.1, "Single array creation failed, expected 1.1")
    End Sub
    <Test()> Public Sub CreateDouble()
        Dim i As Double()
        i = New Double() {2.2, 2}
        Assert.AreEqual(i(0), 2.2, "Double array creation failed, expected 2.2")
    End Sub
    <Test()> Public Sub CreateDecimal()
        Dim i As Decimal()
        i = New Decimal() {1}
        Assert.AreEqual(i(0), 1, "Decimal array creation failed, expected 1")
    End Sub

    <Test()> Public Sub CreateDate()
        Dim i As Date()
        i = New Date() {#1/1/2005#}
        Assert.AreEqual(i(0), #1/1/2005#, "Date array creation failed, expected #1/1/2005#")
    End Sub
    <Test()> Public Sub CreateString()
        Dim i As String()
        i = New String() {"Test"}
        Assert.AreEqual(i(0), "Test", "SByte array creation failed, expected ""Test""")
    End Sub
    <Test()> Public Sub CreateBoolean()
        Dim b As Boolean()
        b = New Boolean() {True}
        Assert.AreEqual(b(0), True, "SByte array creation failed, expected True")
    End Sub
    <Test()> Public Sub CreateChar()
        Dim i As Char()
        i = New Char() {"1"c}
        Assert.AreEqual(i(0), "1"c, "SByte array creation failed, expected ""1""c")
    End Sub

End Class
