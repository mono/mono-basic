Option Strict Off
Module ImpConversionofBooltoDecimal
    Function Main() As Integer
        Dim b As Boolean = False
        Dim a As Decimal = b
        If a <> 0 Then
            System.Console.WriteLine("Implicit Conversion of Bool(Fals):return 1 to Decimal has Failed. Expected 0, but got " & a)
        End If
    End Function
End Module
