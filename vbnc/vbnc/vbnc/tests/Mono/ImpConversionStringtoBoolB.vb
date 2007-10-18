'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionStringtoBooleanA
    Function Main() As Integer
        Dim a As Boolean
        Dim b As String = "False"
        a = b
        If a <> False Then
            System.Console.WriteLine("Conversion of String to Boolean not working. Expected False but got " & a) : Return 1
        End If
    End Function
End Module
