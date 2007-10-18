'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDecimaltoByteA
    Function Main() As Integer
        Dim i As Boolean = False
        Try
            Dim a As Byte
            Dim b As Decimal = 3000000000
            a = CByte(b)
        Catch e As System.Exception
            System.Console.WriteLine(" Arithmetic operation resulted in an overflow.")
            i = True
        End Try
        If i = False Then
            System.Console.WriteLine("Decimal to Byte Conversion is not working properly.")
        End If
    End Function
End Module
