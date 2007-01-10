Option Strict Off
Module ImpConversionofDoubletoBool
    Function Main() As Integer
        Dim a As Double = 123
        Dim b As Boolean = a
        If b <> True Then
            System.Console.WriteLine("Implicit Conversion of Double to Bool(Tru):return 1 has Failed.Expected True got " + b)
        End If
    End Function
End Module
