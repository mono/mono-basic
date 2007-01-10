Option Strict Off
Module ImpConversionofShorttoBool
    Function Main() As Integer
        Dim a As Short = 0
        Dim b As Boolean = a
        If b <> False Then
            System.Console.WriteLine("Implicit Conversion of Short to Bool(Fals):return 1 has Failed. Expected False, but got " & b)
        End If
    End Function
End Module
