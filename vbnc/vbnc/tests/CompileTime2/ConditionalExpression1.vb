Option Strict On
Imports System

Module ConditionalExpression1

    Private failures As Integer
    Private evaluated As Integer

    Private Const constvalue As Integer = If(True, 1, 2)

    Function Main() As Integer
        Dim x As Nullable(Of Integer) = New Nullable(Of Integer)(2)
        Dim a As Nullable(Of Long) = New Nullable(Of Long)(3)

        Assert(TypeCode.Int32, tc(If(True, 1, 2)), "1")
        Assert(TypeCode.Int32, tc(If(True, CSByte(1), 2)), "2")

        Assert(TypeCode.SByte, tc(If(True, CSByte(1), CSByte(2))), "3")
        Assert(TypeCode.Byte, tc(If(True, CByte(1), CByte(2))), "4")
        Assert(TypeCode.Int16, tc(If(True, CShort(1), CShort(2))), "5")
        Assert(TypeCode.UInt16, tc(If(False, CUShort(1), CUShort(3))), "6")
        Assert(TypeCode.Int32, tc(If("you" = "me", 1I, 2I)), "7")
        Assert(TypeCode.UInt32, tc(If("me" = "you", 2UI, 3UI)), "8")
        Assert(TypeCode.Int64, tc(If("he" = "she", 9L, 8L)), "9")
        Assert(TypeCode.UInt64, tc(If("she" = "me", 7UL, 6UL)), "10")
        Assert(TypeCode.Single, tc(If("a" = "b", 1.1!, 2.2!)), "11.0!")
        Assert(TypeCode.Double, tc(If("b" = "c", 2.2, 3.3)), "12.0")
        Assert(TypeCode.Decimal, tc(If("c" = "c", 2D, 3D)), "13")
        'Assert(TypeCode.Decimal, tc(If(False, 1L, 2UL)), "14")
        'Assert(TypeCode.Object, tc(If(True, Nothing, Nothing)), "15")
        Assert(TypeCode.Byte, tc(If(True, CByte(1), Nothing)), "16")
        Assert(TypeCode.Byte, tc(If(True, Nothing, CByte(2))), "17")
        Assert(True, If(Nothing, Nothing) Is Nothing, "18")
        Assert("Int64", If(True, x, a).GetType().Name, "19")
        Assert(True, If(True, x, a).HasValue, "20")

        evaluated = 0
        Assert("foo", If(evaluate("foo"), "bar"), "b1")
        Assert(1, evaluated, "e1")

        evaluated = 0
        Assert("bar", If(evaluate(Nothing), "bar"), "b2")
        Assert(1, evaluated, "e2")

        Assert("Int64", If(x, a).GetType().Name, "n1")
        Assert("Int32", If(x, 0).GetType().Name, "n2")
        Assert("String", If(evaluate(Nothing), "foo").GetType().Name, "n4")
        Assert("2", If(x, a).ToString(), "n5")

        Return failures
    End Function

    Function ByrefNullable(ByRef a As Nullable(Of Integer)) As Nullable(Of Integer)
        Return a
    End Function

    Function Evaluate(ByVal result As Object) As Object
        evaluated += 1
        Return result
    End Function

    Sub Assert(ByVal expected As Object, ByVal actual As Object, ByVal msg As String)
        If Object.Equals(expected, actual) = False Then
            Console.WriteLine("Expected {0} got {1}: {2}", expected, actual, msg)
            failures += 1
        End If
    End Sub

    Sub Assert(ByVal expected As Integer, ByVal actual As Integer, ByVal msg As String)
        If expected <> actual Then
            Console.WriteLine("Expected {0} got {1}: {2}", expected, actual, msg)
            failures += 1
        End If
    End Sub

    Sub Assert(ByVal expected As TypeCode, ByVal actual As TypeCode, ByVal msg As String)
        If expected <> actual Then
            Console.WriteLine("Expected {0} got {1}: {2}", expected, actual, msg)
            failures += 1
        End If
    End Sub
    Function TC(ByVal p As SByte) As TypeCode
        Return TypeCode.SByte
    End Function
    Function TC(ByVal p As Short) As TypeCode
        Return TypeCode.Int16
    End Function
    Function TC(ByVal p As Integer) As TypeCode
        Return TypeCode.Int32
    End Function
    Function TC(ByVal p As Long) As TypeCode
        Return TypeCode.Int64
    End Function
    Function TC(ByVal p As Byte) As TypeCode
        Return TypeCode.Byte
    End Function
    Function TC(ByVal p As UShort) As TypeCode
        Return TypeCode.UInt16
    End Function
    Function TC(ByVal p As UInteger) As TypeCode
        Return TypeCode.UInt32
    End Function
    Function TC(ByVal p As ULong) As TypeCode
        Return TypeCode.UInt64
    End Function
    Function TC(ByVal p As Single) As TypeCode
        Return TypeCode.Single
    End Function
    Function TC(ByVal p As Double) As TypeCode
        Return TypeCode.Double
    End Function
    Function TC(ByVal p As Decimal) As TypeCode
        Return TypeCode.Decimal
    End Function
    Function TC(ByVal p As String) As TypeCode
        Return TypeCode.String
    End Function
    Function TC(ByVal p As Date) As TypeCode
        Return TypeCode.DateTime
    End Function
    Function TC(ByVal p As Object) As TypeCode
        Return TypeCode.Object
    End Function
    Function TC(ByVal p As Boolean) As TypeCode
        Return TypeCode.Boolean
    End Function
    Function TC(ByVal p As Char) As TypeCode
        Return TypeCode.Char
    End Function

End Module

