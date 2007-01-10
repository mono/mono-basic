'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Module EnumConversion
    Enum e As Integer
        A = -9.9
        B
        C = B
        D
    End Enum
    Function Main() As Integer
        Dim i As Double = e.D
        If i <> -8 Then
            System.Console.WriteLine("Enum Conversion is not working properly. Expected -8 but got " & i) : Return 1
        End If
    End Function
End Module
