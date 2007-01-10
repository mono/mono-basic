'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionShorttoSingleA
    Function Main() As Integer
        Dim a As Short = 123
        Dim b As Single
        b = CSng(a)
        If b <> 123 Then
            System.Console.WriteLine("Short to Single Conversion is not working properly. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
