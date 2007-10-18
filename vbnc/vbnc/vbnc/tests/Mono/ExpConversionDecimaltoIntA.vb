'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDecimaltoIntA
    Function Main() As Integer
        Dim i As Boolean = False
        Try
            Dim a As Integer
            Dim b As Decimal = 3000000000
            a = CInt(b)
        Catch e As System.Exception
            System.Console.WriteLine(" Arithmetic operation resulted in an overflow.")
            i = True
        End Try
        If i = False Then
            System.Console.WriteLine("Decimal to Integer Conversion is not working properly.")
        End If
    End Function
End Module
