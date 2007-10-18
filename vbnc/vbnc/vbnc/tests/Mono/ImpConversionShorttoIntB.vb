'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ImpConversionShorttoIntegerC
    Function Main() As Integer
        Dim a As Short = 111
        Dim b As Integer = 111 + a
        If b <> 222 Then
            System.Console.WriteLine("Addition of Short & Integer not working. Expected 222 but got " & b) : Return 1
        End If
    End Function
End Module



