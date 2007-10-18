Option Strict Off
Module ImpConversionofDoubletoBool
    Function Main() As Integer
        Dim a As Double = 0
        Dim b As Boolean = a
        If b <> False Then
            System.Console.WriteLine("Implicit Conversion of Double to Bool(Fals):return 1 has Failed. Expected False, but got " & b)
        End If
    End Function
End Module
