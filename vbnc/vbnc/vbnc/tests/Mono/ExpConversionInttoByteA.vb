'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionInttoByteA
    Function Main() As Integer
        Dim a As Byte
        Dim b As Integer = 123
        a = CByte(b)
        If a <> 123 Then
            System.Console.WriteLine("Byte to Int Conversion is not working properly. Expected 123 but got " & a) : Return 1
        End If
    End Function
End Module
