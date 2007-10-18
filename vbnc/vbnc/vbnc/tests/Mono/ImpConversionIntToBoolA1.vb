Option Strict Off
Module ImpConversionofInttoBool
    Function Main() As Integer
        Dim a As Integer = 0
        Dim b As Boolean = a
        If b <> False Then
            System.Console.WriteLine("Implicit Conversion of Int to Bool(Fals):return 1 has Failed. Expected False, but got " & b) : Return 1
        End If
    End Function
End Module
