Module ExplicitConversion1
    Function ExplicitConversionSByteToByte1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = CSByte(20)
        value2 = CByte(value1)
        const2 = CByte(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 30S
        value2 = CByte(value1)
        const2 = CByte(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToByte1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 40US
        value2 = CByte(value1)
        const2 = CByte(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 50I
        value2 = CByte(value1)
        const2 = CByte(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToByte1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 60UI
        value2 = CByte(value1)
        const2 = CByte(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 70L
        value2 = CByte(value1)
        const2 = CByte(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToByte1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 80UL
        value2 = CByte(value1)
        const2 = CByte(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 90.09D
        value2 = CByte(value1)
        const2 = CByte(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 100.001!
        value2 = CByte(value1)
        const2 = CByte(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 110.011
        value2 = CByte(value1)
        const2 = CByte(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToByte1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "testvalue"
        value2 = CByte(value1)
        const2 = CByte("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = True
        value2 = CByte(value1)
        const2 = CByte(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "C"c
        value2 = CByte(value1)
        const2 = CByte("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = #01/01/2000 12:34#
        value2 = CByte(value1)
        const2 = CByte(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = Nothing
        value2 = CByte(value1)
        const2 = CByte(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = CByte(10)
        value2 = CSByte(value1)
        const2 = CSByte(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 30S
        value2 = CSByte(value1)
        const2 = CSByte(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 40US
        value2 = CSByte(value1)
        const2 = CSByte(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 50I
        value2 = CSByte(value1)
        const2 = CSByte(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 60UI
        value2 = CSByte(value1)
        const2 = CSByte(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 70L
        value2 = CSByte(value1)
        const2 = CSByte(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 80UL
        value2 = CSByte(value1)
        const2 = CSByte(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 90.09D
        value2 = CSByte(value1)
        const2 = CSByte(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 100.001!
        value2 = CSByte(value1)
        const2 = CSByte(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 110.011
        value2 = CSByte(value1)
        const2 = CSByte(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "testvalue"
        value2 = CSByte(value1)
        const2 = CSByte("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = True
        value2 = CSByte(value1)
        const2 = CSByte(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "C"c
        value2 = CSByte(value1)
        const2 = CSByte("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = #01/01/2000 12:34#
        value2 = CSByte(value1)
        const2 = CSByte(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = Nothing
        value2 = CSByte(value1)
        const2 = CSByte(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToSByte1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Short
        Dim const2 As Short

        value1 = CByte(10)
        value2 = CShort(value1)
        const2 = CShort(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToShort1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Short
        Dim const2 As Short

        value1 = CSByte(20)
        value2 = CShort(value1)
        const2 = CShort(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToShort1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Short
        Dim const2 As Short

        value1 = 40US
        value2 = CShort(value1)
        const2 = CShort(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Short
        Dim const2 As Short

        value1 = 50I
        value2 = CShort(value1)
        const2 = CShort(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToShort1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Short
        Dim const2 As Short

        value1 = 60UI
        value2 = CShort(value1)
        const2 = CShort(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Short
        Dim const2 As Short

        value1 = 70L
        value2 = CShort(value1)
        const2 = CShort(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToShort1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Short
        Dim const2 As Short

        value1 = 80UL
        value2 = CShort(value1)
        const2 = CShort(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Short
        Dim const2 As Short

        value1 = 90.09D
        value2 = CShort(value1)
        const2 = CShort(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Short
        Dim const2 As Short

        value1 = 100.001!
        value2 = CShort(value1)
        const2 = CShort(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Short
        Dim const2 As Short

        value1 = 110.011
        value2 = CShort(value1)
        const2 = CShort(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToShort1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Short
        Dim const2 As Short

        value1 = "testvalue"
        value2 = CShort(value1)
        const2 = CShort("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Short
        Dim const2 As Short

        value1 = True
        value2 = CShort(value1)
        const2 = CShort(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Short
        Dim const2 As Short

        value1 = "C"c
        value2 = CShort(value1)
        const2 = CShort("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Short
        Dim const2 As Short

        value1 = #01/01/2000 12:34#
        value2 = CShort(value1)
        const2 = CShort(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Short
        Dim const2 As Short

        value1 = Nothing
        value2 = CShort(value1)
        const2 = CShort(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = CByte(10)
        value2 = CUShort(value1)
        const2 = CUShort(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = CSByte(20)
        value2 = CUShort(value1)
        const2 = CUShort(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 30S
        value2 = CUShort(value1)
        const2 = CUShort(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 50I
        value2 = CUShort(value1)
        const2 = CUShort(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 60UI
        value2 = CUShort(value1)
        const2 = CUShort(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 70L
        value2 = CUShort(value1)
        const2 = CUShort(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 80UL
        value2 = CUShort(value1)
        const2 = CUShort(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 90.09D
        value2 = CUShort(value1)
        const2 = CUShort(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 100.001!
        value2 = CUShort(value1)
        const2 = CUShort(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 110.011
        value2 = CUShort(value1)
        const2 = CUShort(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = "testvalue"
        value2 = CUShort(value1)
        const2 = CUShort("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = True
        value2 = CUShort(value1)
        const2 = CUShort(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = "C"c
        value2 = CUShort(value1)
        const2 = CUShort("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = #01/01/2000 12:34#
        value2 = CUShort(value1)
        const2 = CUShort(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = Nothing
        value2 = CUShort(value1)
        const2 = CUShort(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToUShort1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = CByte(10)
        value2 = CInt(value1)
        const2 = CInt(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = CSByte(20)
        value2 = CInt(value1)
        const2 = CInt(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 30S
        value2 = CInt(value1)
        const2 = CInt(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 40US
        value2 = CInt(value1)
        const2 = CInt(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 60UI
        value2 = CInt(value1)
        const2 = CInt(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 70L
        value2 = CInt(value1)
        const2 = CInt(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 80UL
        value2 = CInt(value1)
        const2 = CInt(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 90.09D
        value2 = CInt(value1)
        const2 = CInt(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 100.001!
        value2 = CInt(value1)
        const2 = CInt(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 110.011
        value2 = CInt(value1)
        const2 = CInt(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "testvalue"
        value2 = CInt(value1)
        const2 = CInt("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = True
        value2 = CInt(value1)
        const2 = CInt(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "C"c
        value2 = CInt(value1)
        const2 = CInt("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = #01/01/2000 12:34#
        value2 = CInt(value1)
        const2 = CInt(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = Nothing
        value2 = CInt(value1)
        const2 = CInt(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CByte(10)
        value2 = CUInt(value1)
        const2 = CUInt(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CSByte(20)
        value2 = CUInt(value1)
        const2 = CUInt(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 30S
        value2 = CUInt(value1)
        const2 = CUInt(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 40US
        value2 = CUInt(value1)
        const2 = CUInt(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 50I
        value2 = CUInt(value1)
        const2 = CUInt(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 70L
        value2 = CUInt(value1)
        const2 = CUInt(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 80UL
        value2 = CUInt(value1)
        const2 = CUInt(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 90.09D
        value2 = CUInt(value1)
        const2 = CUInt(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 100.001!
        value2 = CUInt(value1)
        const2 = CUInt(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 110.011
        value2 = CUInt(value1)
        const2 = CUInt(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = "testvalue"
        value2 = CUInt(value1)
        const2 = CUInt("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = True
        value2 = CUInt(value1)
        const2 = CUInt(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = "C"c
        value2 = CUInt(value1)
        const2 = CUInt("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = #01/01/2000 12:34#
        value2 = CUInt(value1)
        const2 = CUInt(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = Nothing
        value2 = CUInt(value1)
        const2 = CUInt(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToUInteger1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Long
        Dim const2 As Long

        value1 = CByte(10)
        value2 = CLng(value1)
        const2 = CLng(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToLong1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Long
        Dim const2 As Long

        value1 = CSByte(20)
        value2 = CLng(value1)
        const2 = CLng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Long
        Dim const2 As Long

        value1 = 30S
        value2 = CLng(value1)
        const2 = CLng(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToLong1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Long
        Dim const2 As Long

        value1 = 40US
        value2 = CLng(value1)
        const2 = CLng(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Long
        Dim const2 As Long

        value1 = 50I
        value2 = CLng(value1)
        const2 = CLng(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToLong1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Long
        Dim const2 As Long

        value1 = 60UI
        value2 = CLng(value1)
        const2 = CLng(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToLong1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Long
        Dim const2 As Long

        value1 = 80UL
        value2 = CLng(value1)
        const2 = CLng(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Long
        Dim const2 As Long

        value1 = 90.09D
        value2 = CLng(value1)
        const2 = CLng(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Long
        Dim const2 As Long

        value1 = 100.001!
        value2 = CLng(value1)
        const2 = CLng(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Long
        Dim const2 As Long

        value1 = 110.011
        value2 = CLng(value1)
        const2 = CLng(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToLong1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Long
        Dim const2 As Long

        value1 = "testvalue"
        value2 = CLng(value1)
        const2 = CLng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Long
        Dim const2 As Long

        value1 = True
        value2 = CLng(value1)
        const2 = CLng(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Long
        Dim const2 As Long

        value1 = "C"c
        value2 = CLng(value1)
        const2 = CLng("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Long
        Dim const2 As Long

        value1 = #01/01/2000 12:34#
        value2 = CLng(value1)
        const2 = CLng(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Long
        Dim const2 As Long

        value1 = Nothing
        value2 = CLng(value1)
        const2 = CLng(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToLong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CByte(10)
        value2 = CULng(value1)
        const2 = CULng(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToULong1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CSByte(20)
        value2 = CULng(value1)
        const2 = CULng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 30S
        value2 = CULng(value1)
        const2 = CULng(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToULong1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 40US
        value2 = CULng(value1)
        const2 = CULng(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 50I
        value2 = CULng(value1)
        const2 = CULng(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToULong1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 60UI
        value2 = CULng(value1)
        const2 = CULng(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 70L
        value2 = CULng(value1)
        const2 = CULng(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 90.09D
        value2 = CULng(value1)
        const2 = CULng(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 100.001!
        value2 = CULng(value1)
        const2 = CULng(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 110.011
        value2 = CULng(value1)
        const2 = CULng(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToULong1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = "testvalue"
        value2 = CULng(value1)
        const2 = CULng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = True
        value2 = CULng(value1)
        const2 = CULng(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = "C"c
        value2 = CULng(value1)
        const2 = CULng("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = #01/01/2000 12:34#
        value2 = CULng(value1)
        const2 = CULng(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = Nothing
        value2 = CULng(value1)
        const2 = CULng(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToULong1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = CByte(10)
        value2 = CDec(value1)
        const2 = CDec(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = CSByte(20)
        value2 = CDec(value1)
        const2 = CDec(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 30S
        value2 = CDec(value1)
        const2 = CDec(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 40US
        value2 = CDec(value1)
        const2 = CDec(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 50I
        value2 = CDec(value1)
        const2 = CDec(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 60UI
        value2 = CDec(value1)
        const2 = CDec(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 70L
        value2 = CDec(value1)
        const2 = CDec(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 80UL
        value2 = CDec(value1)
        const2 = CDec(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 100.001!
        value2 = CDec(value1)
        const2 = CDec(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 110.011
        value2 = CDec(value1)
        const2 = CDec(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = "testvalue"
        value2 = CDec(value1)
        const2 = CDec("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = True
        value2 = CDec(value1)
        const2 = CDec(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = "C"c
        value2 = CDec(value1)
        const2 = CDec("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = #01/01/2000 12:34#
        value2 = CDec(value1)
        const2 = CDec(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = Nothing
        value2 = CDec(value1)
        const2 = CDec(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToDecimal1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CByte(10)
        value2 = CSng(value1)
        const2 = CSng(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CSByte(20)
        value2 = CSng(value1)
        const2 = CSng(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Single
        Dim const2 As Single

        value1 = 30S
        value2 = CSng(value1)
        const2 = CSng(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Single
        Dim const2 As Single

        value1 = 40US
        value2 = CSng(value1)
        const2 = CSng(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Single
        Dim const2 As Single

        value1 = 50I
        value2 = CSng(value1)
        const2 = CSng(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Single
        Dim const2 As Single

        value1 = 60UI
        value2 = CSng(value1)
        const2 = CSng(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Single
        Dim const2 As Single

        value1 = 70L
        value2 = CSng(value1)
        const2 = CSng(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Single
        Dim const2 As Single

        value1 = 80UL
        value2 = CSng(value1)
        const2 = CSng(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Single
        Dim const2 As Single

        value1 = 90.09D
        value2 = CSng(value1)
        const2 = CSng(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Single
        Dim const2 As Single

        value1 = 110.011
        value2 = CSng(value1)
        const2 = CSng(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Single
        Dim const2 As Single

        value1 = "testvalue"
        value2 = CSng(value1)
        const2 = CSng("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Single
        Dim const2 As Single

        value1 = True
        value2 = CSng(value1)
        const2 = CSng(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Single
        Dim const2 As Single

        value1 = "C"c
        value2 = CSng(value1)
        const2 = CSng("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Single
        Dim const2 As Single

        value1 = #01/01/2000 12:34#
        value2 = CSng(value1)
        const2 = CSng(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Single
        Dim const2 As Single

        value1 = Nothing
        value2 = CSng(value1)
        const2 = CSng(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToSingle1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CByte(10)
        value2 = CDbl(value1)
        const2 = CDbl(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CSByte(20)
        value2 = CDbl(value1)
        const2 = CDbl(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Double
        Dim const2 As Double

        value1 = 30S
        value2 = CDbl(value1)
        const2 = CDbl(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Double
        Dim const2 As Double

        value1 = 40US
        value2 = CDbl(value1)
        const2 = CDbl(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Double
        Dim const2 As Double

        value1 = 50I
        value2 = CDbl(value1)
        const2 = CDbl(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Double
        Dim const2 As Double

        value1 = 60UI
        value2 = CDbl(value1)
        const2 = CDbl(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Double
        Dim const2 As Double

        value1 = 70L
        value2 = CDbl(value1)
        const2 = CDbl(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Double
        Dim const2 As Double

        value1 = 80UL
        value2 = CDbl(value1)
        const2 = CDbl(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Double
        Dim const2 As Double

        value1 = 90.09D
        value2 = CDbl(value1)
        const2 = CDbl(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Double
        Dim const2 As Double

        value1 = 100.001!
        value2 = CDbl(value1)
        const2 = CDbl(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Double
        Dim const2 As Double

        value1 = "testvalue"
        value2 = CDbl(value1)
        const2 = CDbl("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Double
        Dim const2 As Double

        value1 = True
        value2 = CDbl(value1)
        const2 = CDbl(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Double
        Dim const2 As Double

        value1 = "C"c
        value2 = CDbl(value1)
        const2 = CDbl("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Double
        Dim const2 As Double

        value1 = #01/01/2000 12:34#
        value2 = CDbl(value1)
        const2 = CDbl(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Double
        Dim const2 As Double

        value1 = Nothing
        value2 = CDbl(value1)
        const2 = CDbl(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToDouble1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToString1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As String
        Dim const2 As String

        value1 = CByte(10)
        value2 = CStr(value1)
        const2 = CStr(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToString1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As String
        Dim const2 As String

        value1 = CSByte(20)
        value2 = CStr(value1)
        const2 = CStr(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToString1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As String
        Dim const2 As String

        value1 = 30S
        value2 = CStr(value1)
        const2 = CStr(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToString1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As String
        Dim const2 As String

        value1 = 40US
        value2 = CStr(value1)
        const2 = CStr(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToString1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As String
        Dim const2 As String

        value1 = 50I
        value2 = CStr(value1)
        const2 = CStr(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToString1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As String
        Dim const2 As String

        value1 = 60UI
        value2 = CStr(value1)
        const2 = CStr(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToString1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As String
        Dim const2 As String

        value1 = 70L
        value2 = CStr(value1)
        const2 = CStr(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToString1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As String
        Dim const2 As String

        value1 = 80UL
        value2 = CStr(value1)
        const2 = CStr(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToString1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As String
        Dim const2 As String

        value1 = 90.09D
        value2 = CStr(value1)
        const2 = CStr(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToString1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As String
        Dim const2 As String

        value1 = 100.001!
        value2 = CStr(value1)
        const2 = CStr(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToString1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As String
        Dim const2 As String

        value1 = 110.011
        value2 = CStr(value1)
        const2 = CStr(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToString1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As String
        Dim const2 As String

        value1 = True
        value2 = CStr(value1)
        const2 = CStr(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToString1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As String
        Dim const2 As String

        value1 = "C"c
        value2 = CStr(value1)
        const2 = CStr("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToString1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As String
        Dim const2 As String

        value1 = #01/01/2000 12:34#
        value2 = CStr(value1)
        const2 = CStr(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToString1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As String
        Dim const2 As String

        value1 = Nothing
        value2 = CStr(value1)
        const2 = CStr(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToString1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = CByte(10)
        value2 = CBool(value1)
        const2 = CBool(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = CSByte(20)
        value2 = CBool(value1)
        const2 = CBool(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 30S
        value2 = CBool(value1)
        const2 = CBool(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 40US
        value2 = CBool(value1)
        const2 = CBool(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 50I
        value2 = CBool(value1)
        const2 = CBool(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 60UI
        value2 = CBool(value1)
        const2 = CBool(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 70L
        value2 = CBool(value1)
        const2 = CBool(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 80UL
        value2 = CBool(value1)
        const2 = CBool(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 90.09D
        value2 = CBool(value1)
        const2 = CBool(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 100.001!
        value2 = CBool(value1)
        const2 = CBool(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 110.011
        value2 = CBool(value1)
        const2 = CBool(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = "testvalue"
        value2 = CBool(value1)
        const2 = CBool("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = "C"c
        value2 = CBool(value1)
        const2 = CBool("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = #01/01/2000 12:34#
        value2 = CBool(value1)
        const2 = CBool(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = Nothing
        value2 = CBool(value1)
        const2 = CBool(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToBoolean1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Char
        Dim const2 As Char

        value1 = CByte(10)
        value2 = CChar(value1)
        const2 = CChar(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToChar1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Char
        Dim const2 As Char

        value1 = CSByte(20)
        value2 = CChar(value1)
        const2 = CChar(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Char
        Dim const2 As Char

        value1 = 30S
        value2 = CChar(value1)
        const2 = CChar(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToChar1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Char
        Dim const2 As Char

        value1 = 40US
        value2 = CChar(value1)
        const2 = CChar(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Char
        Dim const2 As Char

        value1 = 50I
        value2 = CChar(value1)
        const2 = CChar(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToChar1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Char
        Dim const2 As Char

        value1 = 60UI
        value2 = CChar(value1)
        const2 = CChar(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Char
        Dim const2 As Char

        value1 = 70L
        value2 = CChar(value1)
        const2 = CChar(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToChar1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Char
        Dim const2 As Char

        value1 = 80UL
        value2 = CChar(value1)
        const2 = CChar(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Char
        Dim const2 As Char

        value1 = 90.09D
        value2 = CChar(value1)
        const2 = CChar(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Char
        Dim const2 As Char

        value1 = 100.001!
        value2 = CChar(value1)
        const2 = CChar(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Char
        Dim const2 As Char

        value1 = 110.011
        value2 = CChar(value1)
        const2 = CChar(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToChar1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Char
        Dim const2 As Char

        value1 = "testvalue"
        value2 = CChar(value1)
        const2 = CChar("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Char
        Dim const2 As Char

        value1 = True
        value2 = CChar(value1)
        const2 = CChar(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Char
        Dim const2 As Char

        value1 = #01/01/2000 12:34#
        value2 = CChar(value1)
        const2 = CChar(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Char
        Dim const2 As Char

        value1 = Nothing
        value2 = CChar(value1)
        const2 = CChar(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToChar1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CByte(10)
        value2 = CDate(value1)
        const2 = CDate(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToDate1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CSByte(20)
        value2 = CDate(value1)
        const2 = CDate(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Date
        Dim const2 As Date

        value1 = 30S
        value2 = CDate(value1)
        const2 = CDate(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToDate1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Date
        Dim const2 As Date

        value1 = 40US
        value2 = CDate(value1)
        const2 = CDate(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Date
        Dim const2 As Date

        value1 = 50I
        value2 = CDate(value1)
        const2 = CDate(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToDate1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Date
        Dim const2 As Date

        value1 = 60UI
        value2 = CDate(value1)
        const2 = CDate(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Date
        Dim const2 As Date

        value1 = 70L
        value2 = CDate(value1)
        const2 = CDate(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToDate1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Date
        Dim const2 As Date

        value1 = 80UL
        value2 = CDate(value1)
        const2 = CDate(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Date
        Dim const2 As Date

        value1 = 90.09D
        value2 = CDate(value1)
        const2 = CDate(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Date
        Dim const2 As Date

        value1 = 100.001!
        value2 = CDate(value1)
        const2 = CDate(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Date
        Dim const2 As Date

        value1 = 110.011
        value2 = CDate(value1)
        const2 = CDate(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToDate1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Date
        Dim const2 As Date

        value1 = "testvalue"
        value2 = CDate(value1)
        const2 = CDate("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Date
        Dim const2 As Date

        value1 = True
        value2 = CDate(value1)
        const2 = CDate(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Date
        Dim const2 As Date

        value1 = "C"c
        value2 = CDate(value1)
        const2 = CDate("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionObjectToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Date
        Dim const2 As Date

        value1 = Nothing
        value2 = CDate(value1)
        const2 = CDate(Nothing)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionObjectToDate1")
            Return 1
        End If
    End Function
    Function ExplicitConversionByteToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CByte(10)
        value2 = CObj(value1)
        const2 = CObj(CByte(10))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionByteToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSByteToObject1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CSByte(20)
        value2 = CObj(value1)
        const2 = CObj(CSByte(20))

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSByteToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionShortToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Object
        Dim const2 As Object

        value1 = 30S
        value2 = CObj(value1)
        const2 = CObj(30S)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionShortToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUShortToObject1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Object
        Dim const2 As Object

        value1 = 40US
        value2 = CObj(value1)
        const2 = CObj(40US)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUShortToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionIntegerToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Object
        Dim const2 As Object

        value1 = 50I
        value2 = CObj(value1)
        const2 = CObj(50I)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionIntegerToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionUIntegerToObject1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Object
        Dim const2 As Object

        value1 = 60UI
        value2 = CObj(value1)
        const2 = CObj(60UI)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionUIntegerToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionLongToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Object
        Dim const2 As Object

        value1 = 70L
        value2 = CObj(value1)
        const2 = CObj(70L)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionLongToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionULongToObject1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Object
        Dim const2 As Object

        value1 = 80UL
        value2 = CObj(value1)
        const2 = CObj(80UL)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionULongToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDecimalToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Object
        Dim const2 As Object

        value1 = 90.09D
        value2 = CObj(value1)
        const2 = CObj(90.09D)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDecimalToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionSingleToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Object
        Dim const2 As Object

        value1 = 100.001!
        value2 = CObj(value1)
        const2 = CObj(100.001!)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionSingleToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDoubleToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Object
        Dim const2 As Object

        value1 = 110.011
        value2 = CObj(value1)
        const2 = CObj(110.011)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDoubleToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionStringToObject1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Object
        Dim const2 As Object

        value1 = "testvalue"
        value2 = CObj(value1)
        const2 = CObj("testvalue")

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionStringToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionBooleanToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Object
        Dim const2 As Object

        value1 = True
        value2 = CObj(value1)
        const2 = CObj(True)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionBooleanToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionCharToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Object
        Dim const2 As Object

        value1 = "C"c
        value2 = CObj(value1)
        const2 = CObj("C"c)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionCharToObject1")
            Return 1
        End If
    End Function
    Function ExplicitConversionDateToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Object
        Dim const2 As Object

        value1 = #01/01/2000 12:34#
        value2 = CObj(value1)
        const2 = CObj(#01/01/2000 12:34#)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ExplicitConversionDateToObject1")
            Return 1
        End If
    End Function
End Module

