Option Strict Off
Module ImpConversionofBooltoDoubleB
    Function Main() As Integer
        Dim b As Boolean = False
        Dim a As Double = b
        If a <> 0 Then
            System.Console.WriteLine("Implicit Conversion of Bool(Fals):return 1 to Double has Failed. Expected 0, but got " & a)
        End If
    End Function
End Module
