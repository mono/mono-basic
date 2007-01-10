'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Module ImpConversionBooleantoStringC
    Function Main() As Integer
        Dim a As Boolean = True
        Dim b As String = "111" + a
        If b <> "110" Then
            System.Console.WriteLine("Concat of Boolean & String not working. Expected 110 but got " & b) : Return 1
        End If
    End Function
End Module
