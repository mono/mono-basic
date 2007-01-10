'Author:
'   K. Satya Sudha (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System
Module Test
    Function Main() As Integer
        Dim a As Boolean = "c" Like "[abc6!0-58-9]"
        If a <> True Then
            Console.WriteLine("#A1 - Like operator failed")
            Return 1
        End If

        a = "5" Like "[abc0-4def8-9!]"
        If a = True Then
            Console.WriteLine("#A2 - Like operator failed")
            Return 1
        End If

        a = "5" Like "[!abc0-4def8-9]"
        If a <> True Then
            Console.WriteLine("#A3 - Like operator failed")
            Return 1
        End If

        a = "e" Like "[a-f!567o-z]"
        If a <> True Then
            Console.WriteLine("#A4 - Like operator failed")
            Return 1
        End If

        a = "p" Like "[a-f!567o-z]"
        If a = False Then
            Console.WriteLine("#A5 - Like operator failed")
            Return 1
        End If

        a = "d" Like "[a-c-e-g]"
        If a <> False Then
            Console.WriteLine("#A6 - Like operator failed")
            Return 1
        End If

        a = "b" Like "[a-c-e-g]"
        If a <> True Then
            Console.WriteLine("#A7 - Like operator failed")
            Return 1
        End If

    End Function
End Module

