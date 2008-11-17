Class bug_445479

    Shared Function Main() As Integer
        Dim result As Boolean = True
        result = UInt64 AndAlso result
        result = UInt32 AndAlso result
        result = UInt16 AndAlso result
        result = UInt8 AndAlso result

        If result = False Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Shared Function UInt64() As Boolean
        Dim result As Boolean = True
        Dim a As ULong = ULong.maxvalue
        Dim b As ULong = 0

        If a > b = False Then
            System.Console.WriteLine("Failed UInt64 A")
            result = False
        End If

        If a >= b = False Then
            System.Console.WriteLine("Failed UInt64 B")
            result = False
        End If

        If b < a = False Then
            System.Console.WriteLine("Failed UInt64 C")
            result = False
        End If

        If b <= a = False Then
            System.Console.WriteLine("Failed UInt64 D")
            result = False
        End If

        Return result
    End Function


    Shared Function UInt32() As Boolean
        Dim result As Boolean = True
        Dim a As UInteger = UInteger.maxvalue
        Dim b As UInteger = 0

        If a > b = False Then
            System.Console.WriteLine("Failed UInt32 A")
            result = False
        End If

        If a >= b = False Then
            System.Console.WriteLine("Failed UInt32 B")
            result = False
        End If

        If b < a = False Then
            System.Console.WriteLine("Failed UInt32 C")
            result = False
        End If

        If b <= a = False Then
            System.Console.WriteLine("Failed UInt32 D")
            result = False
        End If

        Return result
    End Function

    Shared Function UInt16() As Boolean
        Dim result As Boolean = True
        Dim a As UShort = UShort.MaxValue
        Dim b As UShort = 0

        If a > b = False Then
            System.Console.WriteLine("Failed UInt16 A")
            result = False
        End If

        If a >= b = False Then
            System.Console.WriteLine("Failed UInt16 B")
            result = False
        End If

        If b < a = False Then
            System.Console.WriteLine("Failed UInt16 C")
            result = False
        End If

        If b <= a = False Then
            System.Console.WriteLine("Failed UInt16 D")
            result = False
        End If

        Return result
    End Function

    Shared Function UInt8() As Boolean
        Dim result As Boolean = True
        Dim a As Byte = Byte.MaxValue
        Dim b As Byte = 0

        If a > b = False Then
            System.Console.WriteLine("Failed UInt8 A")
            result = False
        End If

        If a >= b = False Then
            System.Console.WriteLine("Failed UInt8 B")
            result = False
        End If

        If b < a = False Then
            System.Console.WriteLine("Failed UInt8 C")
            result = False
        End If

        If b <= a = False Then
            System.Console.WriteLine("Failed UInt8 D")
            result = False
        End If

        Return result
    End Function
End Class
