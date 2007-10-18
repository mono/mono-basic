'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionStringtoIntegerA
    Function Main() As Integer
        Dim a As Integer
        Dim b As String = "1234"
        a = CInt(b)
        If a <> 1234 Then
            System.Console.WriteLine("Conversion of String to Integer not working. Expected 1234 but got " & a) : Return 1
        End If
    End Function
End Module
