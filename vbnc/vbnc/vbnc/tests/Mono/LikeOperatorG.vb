'Author:
'   K. Satya Sudha (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System
Module Test
    Function Main() As Integer
        Dim a As Boolean = "7" Like "[][!!][]"
        If a <> True Then
            Console.WriteLine("#A1 - Like operator failed")
            Return 1
        End If

        a = "7" Like "[][!-][]"
        If a <> True Then
            Console.WriteLine("#A2 - Like operator failed")
            Return 1
        End If

        a = "d" Like "[abc!def]"
        If a = False Then
            Console.WriteLine("#A3 - Like operator failed")
            Return 1
        End If

        a = "r" Like "[!-*]"
        If a <> True Then
            Console.WriteLine("#A4 - Like operator failed")
            Return 1
        End If

        a = "-" Like "[!-*]"
        If a <> False Then
            Console.WriteLine("#A5 - Like operator failed")
            Return 1
        End If

        a = "*" Like "[!-*]"
        If a <> False Then
            Console.WriteLine("#A6 - Like operator failed")
            Return 1
        End If

        a = "%" Like "[!#--D]"
        If a <> False Then
            Console.WriteLine("#A7 - Like operator failed")
            Return 1
        End If

        a = "A" Like "[?-D]"
        If a <> True Then
            Console.WriteLine("#A8 - Like operator failed")
            Return 1
        End If

        a = "-" Like "[?-D]"
        If a <> False Then
            Console.WriteLine("#A9 - Like operator failed")
            Return 1
        End If

        a = "-" Like "[--D]"
        If a <> True Then
            Console.WriteLine("#A10 - Like operator failed")
            Return 1
        End If

        a = "0" Like "[--D]"
        If a <> True Then
            Console.WriteLine("#A11 - Like operator failed")
            Return 1
        End If

        a = "+" Like "[*--D]"
        If a <> True Then
            Console.WriteLine("#A12 - Like operator failed")
            Return 1
        End If

        a = "[" Like "[[-a]"
        If a <> True Then
            Console.WriteLine("#A13 - Like operator failed")
            Return 1
        End If

        a = "-" Like "[a-c-e]"
        If a <> True Then
            Console.WriteLine("#A14 - Like operator failed")
            Return 1
        End If

        a = "d" Like "[a-c-e]"
        If a <> False Then
            Console.WriteLine("#A15 - Like operator failed")
            Return 1
        End If

        a = "b" Like "[a-c-e]"
        If a <> True Then
            Console.WriteLine("#A16 - Like operator failed")
            Return 1
        End If

    End Function
End Module

