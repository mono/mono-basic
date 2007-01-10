'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ImpConversionIntegertoLongC
    Function Main() As Integer
        Dim a As Integer = 111
        Dim b As Long = 111 + a
        If b <> 222 Then
            System.Console.WriteLine("Addition of Integer& Long not working. Expected 222 but got " & b) : Return 1
        End If
    End Function
End Module
