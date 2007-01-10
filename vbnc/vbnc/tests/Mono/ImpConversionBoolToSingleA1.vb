Option Strict Off
Module ImpConversionofBooltoSingle
    Function Main() As Integer
        Dim b As Boolean = False
        Dim a As Single = b
        If a <> 0 Then
            System.Console.WriteLine("Implicit Conversion of Bool(Fals):return 1 to Single has Failed. Expected 0, but got " & a)
        End If
    End Function
End Module
