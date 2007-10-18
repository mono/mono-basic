Option Strict Off
Module ImpConversionofDecimaltoBool
    Function Main() As Integer
        Dim a As Decimal = 123
        Dim b As Boolean = a
        If b <> True Then
            System.Console.WriteLine("Implicit Conversion of Decimal to Bool(Tru):return 1 has Failed. Expected True got " + b)
        End If
    End Function
End Module
