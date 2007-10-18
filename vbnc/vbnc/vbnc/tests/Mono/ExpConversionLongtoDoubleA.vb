'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionLongtoDoubleA
    Function Main() As Integer
        Dim a As Long = 124
        Dim b As Double = CLng(a)
        If b <> 124 Then
            System.Console.WriteLine("Explicit Conversion of Long to Double has Failed. Expected 124 but got " & b) : Return 1
        End If
    End Function
End Module
