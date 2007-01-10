'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Option Explicit Off
Module F
    Function Main() As Integer
        If fun <> Nothing Then
            System.Console.WriteLine("Local Variables not working properly. Expected Nothing but got" & fun) : Return 1
        End If
    End Function
End Module
