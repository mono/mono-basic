'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module F
    Dim i As Integer
    Function Main() As Integer
        If I <> 0 Then
            System.Console.WriteLine("Local Variables not working properly. Expected 0 but got" & i) : Return 1
        End If
    End Function
End Module
