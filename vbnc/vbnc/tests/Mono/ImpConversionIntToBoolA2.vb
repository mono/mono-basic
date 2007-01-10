Option Strict Off
Module ImpConversionofInttoBool
    Function Main() As Integer
        Dim a As Integer = 123
        Dim b As Boolean = a
        If b <> True Then
            System.Console.WriteLine("Implicit Conversion of Int to Bool(Tru):return 1 has Failed. Expected True got " + b)
        End If
    End Function
End Module
