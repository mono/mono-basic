'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Dim a As Integer = 0
    Function GetIndex() As Object
        a = a + 1
        Return 1
    End Function

    Function Main() As Integer
        Dim a1(2) As Integer
        a1(CInt(GetIndex())) = a1(CInt(GetIndex())) + 1
        If a <> 2 Then
            System.Console.WriteLine("Assingment not working properly. Expected 2 but got " & a) : Return 1
        End If
        a = 0
        a1(CInt(GetIndex())) += 1
        If a <> 1 Then
            System.Console.WriteLine("Compound Assingment not working properly. Expected 1 but got " & a) : Return 1
        End If
    End Function
End Module
