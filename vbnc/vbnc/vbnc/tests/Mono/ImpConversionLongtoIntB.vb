Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ImpConversionLongtoIntegerC
    Function Main() As Integer
        Dim a As Long = 111
        Dim b As Integer = 111 + a
        If b <> 222 Then
            System.Console.WriteLine("Addition of Long & Integer not working. Expected 222 but got " & b) : Return 1
        End If
    End Function
End Module


