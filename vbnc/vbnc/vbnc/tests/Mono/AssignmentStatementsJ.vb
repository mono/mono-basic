'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module Test
    Dim a, b, c, d, e, f, g, h, i, j As Integer

    Function FA() As Integer
        a += 1
    End Function
    Function FB() As Integer
        b += 1
    End Function
    Function FC() As Integer
        c += 1
    End Function
    Function FD() As Integer
        d += 1
    End Function
    Function FE(ByVal q As Integer) As Integer
        e += 1
    End Function

    Function Main() As Integer
        Dim a1(2) As Integer
        Dim a2(2, 2) As Integer
        Dim a3(2)() As Integer
        Dim result As Integer

        a1(FA()) += 1
        If a <> 1 Then
            System.Console.WriteLine("A Compound Assingment not working properly. Expected 1 but got " & a)
            result += 1
        End If

        a2(FB(), FB()) += 1
        If b <> 2 Then
            System.Console.WriteLine("B Compound Assingment not working properly. Expected 1 but got " & b)
            result += 1
        End If

        ReDim a3(0)(20)
        a3(fc)(fd) += 1
        If c <> 1 Then
            System.Console.WriteLine("C Compound Assingment not working properly. Expected 1 but got " & c)
            result += 1
        End If
        If d <> 1 Then
            System.Console.WriteLine("F Compound Assingment not working properly. Expected 1 but got " & d)
            result += 1
        End If

        Return result
    End Function
End Module
