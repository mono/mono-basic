'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module ExpConversionBooleantoStringA
    Function Main() As Integer
        Dim a As Boolean = False
        Dim b As String = a.toString()
        If b <> "False" Then
            System.Console.WriteLine("Conversion of Boolean to String not working. Expected False but got " & b) : Return 1
        End If
    End Function
End Module
