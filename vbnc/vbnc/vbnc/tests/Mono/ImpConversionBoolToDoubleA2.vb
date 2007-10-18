Option Strict Off
Module ImpConversionofBooltoDoubleB
    Function Main() As Integer
        Dim b As Boolean = True
        Dim a As Double = b
        If a <> -1 Then
            System.Console.WriteLine("Implicit Conversion of Bool(Tru):return 1 to Double has Failed. Expected -1 got " & a)
        End If
    End Function
End Module
