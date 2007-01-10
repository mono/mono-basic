'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionDecimaltoStringA
    Function Main() As Integer
        Dim a As Decimal = 123052
        Dim b As String = a.toString()
        If b <> "123052" Then
            System.Console.WriteLine("Conversion of Decimal to String not working. Expected 123052 but got " & b) : Return 1
        End If
    End Function
End Module
