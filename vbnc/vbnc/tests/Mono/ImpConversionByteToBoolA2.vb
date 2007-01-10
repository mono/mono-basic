Option Strict Off
Module ImpConversionofBytetoBool
    Function Main() As Integer
        Dim a As Byte = 123
        Dim b As Boolean = a
        If b <> True Then
            System.Console.WriteLine("Implicit Conversion of Byte to Bool(Tru):return 1 has Failed. Expected True got " & b)
        End If
    End Function
End Module
