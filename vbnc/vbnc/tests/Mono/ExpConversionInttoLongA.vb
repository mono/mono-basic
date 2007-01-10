'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionofInttoLongA
    Function Main() As Integer
        Dim a As Integer = 123
        Dim b As Long = CLng(a)
        If b <> 123 Then
            System.Console.WriteLine("Explicit Conversion of Int to Long has Failed") : Return 1
        End If
    End Function
End Module
