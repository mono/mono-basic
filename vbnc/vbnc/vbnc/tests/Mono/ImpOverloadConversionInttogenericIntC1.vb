'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Expected to call Long
Option Strict Off

Module ImpConversionInttogenericInt
    Function fun(ByVal i As Short)
        Return 2
    End Function
    Function fun(ByVal i As Long)
        Return 3
    End Function
    Function fun(ByVal i As Byte)
        Return 4
    End Function
    Function Main() As Integer
        Dim i As Integer = 10
        i = fun(i)
        If i <> 3 Then
            System.Console.WriteLine("Implicit Conversion not working. Expected 3 but got " & i) : Return 1
        End If

    End Function
End Module
