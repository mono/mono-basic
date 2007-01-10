'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module EnumConversion
    Enum e As Integer
        A = 9.9
        B
    End Enum
    Function Main() As Integer
        Dim i As Double = e.B
        If i <> 11 Then
            System.Console.WriteLine("Enum Conversion is not working properly. Expected 11 but got " & i) : Return 1
        End If
    End Function
End Module
