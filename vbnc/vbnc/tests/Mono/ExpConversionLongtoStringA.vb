'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionLongtoStringA
    Function Main() As Integer
        Dim a As Long = 123
        Dim b As String = a.toString()
        If b <> "123" Then
            System.Console.WriteLine("Conversion of Long to String not working. Expected 123 but got " & b) : Return 1
        End If
    End Function
End Module
