'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionInttoSingleA
    Function Main() As Integer
        Dim a As Integer = 124
        Dim b As Single = CSng(a)
        If b <> 124.0 Then
            System.Console.WriteLine("Explicit Conversion of Long to Single has Failed. Expected 124 but got " & b) : Return 1
        End If
    End Function
End Module
