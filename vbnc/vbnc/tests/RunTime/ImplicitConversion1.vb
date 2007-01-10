Module ImplicitConversion1
    Function ImplicitConversionSByteToByte1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToByte1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToByte1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToByte1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToByte1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToByte1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Byte
        Dim const2 As Byte

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToSByte1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As SByte
        Dim const2 As SByte

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToSByte1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Short
        Dim const2 As Short

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToShort1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Short
        Dim const2 As Short

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToShort1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Short
        Dim const2 As Short

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Short
        Dim const2 As Short

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToShort1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Short
        Dim const2 As Short

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Short
        Dim const2 As Short

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToShort1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Short
        Dim const2 As Short

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Short
        Dim const2 As Short

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Short
        Dim const2 As Short

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Short
        Dim const2 As Short

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToShort1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Short
        Dim const2 As Short

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Short
        Dim const2 As Short

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Short
        Dim const2 As Short

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Short
        Dim const2 As Short

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToShort1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Short
        Dim const2 As Short

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToUShort1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As UShort
        Dim const2 As UShort

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToUShort1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Integer
        Dim const2 As Integer

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToUInteger1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As UInteger
        Dim const2 As UInteger

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToUInteger1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Long
        Dim const2 As Long

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToLong1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Long
        Dim const2 As Long

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Long
        Dim const2 As Long

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToLong1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Long
        Dim const2 As Long

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Long
        Dim const2 As Long

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToLong1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Long
        Dim const2 As Long

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToLong1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Long
        Dim const2 As Long

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Long
        Dim const2 As Long

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Long
        Dim const2 As Long

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Long
        Dim const2 As Long

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToLong1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Long
        Dim const2 As Long

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Long
        Dim const2 As Long

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Long
        Dim const2 As Long

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Long
        Dim const2 As Long

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToLong1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Long
        Dim const2 As Long

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToLong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToULong1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToULong1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToULong1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToULong1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToULong1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As ULong
        Dim const2 As ULong

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToULong1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToDecimal1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Decimal
        Dim const2 As Decimal

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToDecimal1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Single
        Dim const2 As Single

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Single
        Dim const2 As Single

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Single
        Dim const2 As Single

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Single
        Dim const2 As Single

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Single
        Dim const2 As Single

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Single
        Dim const2 As Single

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Single
        Dim const2 As Single

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Single
        Dim const2 As Single

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Single
        Dim const2 As Single

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Single
        Dim const2 As Single

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Single
        Dim const2 As Single

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Single
        Dim const2 As Single

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Single
        Dim const2 As Single

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToSingle1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Single
        Dim const2 As Single

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToSingle1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Double
        Dim const2 As Double

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Double
        Dim const2 As Double

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Double
        Dim const2 As Double

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Double
        Dim const2 As Double

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Double
        Dim const2 As Double

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Double
        Dim const2 As Double

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Double
        Dim const2 As Double

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Double
        Dim const2 As Double

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Double
        Dim const2 As Double

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Double
        Dim const2 As Double

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Double
        Dim const2 As Double

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Double
        Dim const2 As Double

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Double
        Dim const2 As Double

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToDouble1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Double
        Dim const2 As Double

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToDouble1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToString1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As String
        Dim const2 As String

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToString1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As String
        Dim const2 As String

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToString1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As String
        Dim const2 As String

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToString1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As String
        Dim const2 As String

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToString1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As String
        Dim const2 As String

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToString1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As String
        Dim const2 As String

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToString1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As String
        Dim const2 As String

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToString1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As String
        Dim const2 As String

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToString1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As String
        Dim const2 As String

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToString1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As String
        Dim const2 As String

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToString1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As String
        Dim const2 As String

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToString1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As String
        Dim const2 As String

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToString1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As String
        Dim const2 As String

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToString1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As String
        Dim const2 As String

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToString1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As String
        Dim const2 As String

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToString1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToBoolean1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Boolean
        Dim const2 As Boolean

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToBoolean1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Char
        Dim const2 As Char

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToChar1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Char
        Dim const2 As Char

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Char
        Dim const2 As Char

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToChar1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Char
        Dim const2 As Char

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Char
        Dim const2 As Char

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToChar1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Char
        Dim const2 As Char

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Char
        Dim const2 As Char

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToChar1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Char
        Dim const2 As Char

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Char
        Dim const2 As Char

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Char
        Dim const2 As Char

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Char
        Dim const2 As Char

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToChar1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Char
        Dim const2 As Char

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Char
        Dim const2 As Char

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Char
        Dim const2 As Char

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToChar1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Char
        Dim const2 As Char

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToChar1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToDate1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Date
        Dim const2 As Date

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Date
        Dim const2 As Date

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToDate1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Date
        Dim const2 As Date

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Date
        Dim const2 As Date

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToDate1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Date
        Dim const2 As Date

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Date
        Dim const2 As Date

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToDate1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Date
        Dim const2 As Date

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Date
        Dim const2 As Date

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Date
        Dim const2 As Date

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Date
        Dim const2 As Date

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToDate1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Date
        Dim const2 As Date

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Date
        Dim const2 As Date

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Date
        Dim const2 As Date

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionObjectToDate1() As Integer
        Dim result As Boolean
        Dim value1 As Object
        Dim value2 As Date
        Dim const2 As Date

        value1 = Nothing
        value2 = value1
        const2 = Nothing

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionObjectToDate1")
            Return 1
        End If
    End Function
    Function ImplicitConversionByteToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Byte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CByte(10)
        value2 = value1
        const2 = CByte(10)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionByteToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSByteToObject1() As Integer
        Dim result As Boolean
        Dim value1 As SByte
        Dim value2 As Object
        Dim const2 As Object

        value1 = CSByte(20)
        value2 = value1
        const2 = CSByte(20)

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSByteToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionShortToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Short
        Dim value2 As Object
        Dim const2 As Object

        value1 = 30S
        value2 = value1
        const2 = 30S

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionShortToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUShortToObject1() As Integer
        Dim result As Boolean
        Dim value1 As UShort
        Dim value2 As Object
        Dim const2 As Object

        value1 = 40US
        value2 = value1
        const2 = 40US

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUShortToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionIntegerToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Integer
        Dim value2 As Object
        Dim const2 As Object

        value1 = 50I
        value2 = value1
        const2 = 50I

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionIntegerToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionUIntegerToObject1() As Integer
        Dim result As Boolean
        Dim value1 As UInteger
        Dim value2 As Object
        Dim const2 As Object

        value1 = 60UI
        value2 = value1
        const2 = 60UI

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionUIntegerToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionLongToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Long
        Dim value2 As Object
        Dim const2 As Object

        value1 = 70L
        value2 = value1
        const2 = 70L

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionLongToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionULongToObject1() As Integer
        Dim result As Boolean
        Dim value1 As ULong
        Dim value2 As Object
        Dim const2 As Object

        value1 = 80UL
        value2 = value1
        const2 = 80UL

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionULongToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDecimalToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Decimal
        Dim value2 As Object
        Dim const2 As Object

        value1 = 90.09D
        value2 = value1
        const2 = 90.09D

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDecimalToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionSingleToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Single
        Dim value2 As Object
        Dim const2 As Object

        value1 = 100.001!
        value2 = value1
        const2 = 100.001!

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionSingleToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDoubleToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Double
        Dim value2 As Object
        Dim const2 As Object

        value1 = 110.011
        value2 = value1
        const2 = 110.011

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDoubleToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionStringToObject1() As Integer
        Dim result As Boolean
        Dim value1 As String
        Dim value2 As Object
        Dim const2 As Object

        value1 = "testvalue"
        value2 = value1
        const2 = "testvalue"

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionStringToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionBooleanToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Boolean
        Dim value2 As Object
        Dim const2 As Object

        value1 = True
        value2 = value1
        const2 = True

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionBooleanToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionCharToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Char
        Dim value2 As Object
        Dim const2 As Object

        value1 = "C"c
        value2 = value1
        const2 = "C"c

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionCharToObject1")
            Return 1
        End If
    End Function
    Function ImplicitConversionDateToObject1() As Integer
        Dim result As Boolean
        Dim value1 As Date
        Dim value2 As Object
        Dim const2 As Object

        value1 = #01/01/2000 12:34#
        value2 = value1
        const2 = #01/01/2000 12:34#

        result = value2 = const2

        If result = False Then
            System.Console.WriteLine("FAIL ImplicitConversionDateToObject1")
            Return 1
        End If
    End Function
End Module

