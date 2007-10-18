'Author:
'   K. Satya Sudha (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System
Module Test
    Function Main() As Integer
        Dim a As Boolean = "7" Like "[!0-6]"
        If a <> True Then
            Console.Writeline("#A1 - Like operator failed")
            Return 1
        End If

        a = "7" Like "[][!0-6][]"
        If a <> True Then
            Console.Writeline("#A2 - Like operator failed")
            Return 1
        End If

        a = "7" Like "[][!06][]"
        If a <> True Then
            Console.Writeline("#A3 - Like operator failed")
            Return 1
        End If

        a = "6" Like "[][0-6!8-9][]"
        If a <> True Then
            Console.Writeline("#A4 - Like operator failed")
            Return 1
        End If

        a = "67" Like "[!0-58-9][567]"
        If a <> True Then
            Console.Writeline("#A5 - Like operator failed")
            Return 1
        End If

        a = "7" Like "[0-5!]"
        If a = True Then
            Console.Writeline("#A6 - Like operator failed")
            Return 1
        End If

        a = "6" Like "[][0-6!8-9][]"
        If a <> True Then
            Console.Writeline("#A7 - Like operator failed")
            Return 1
        End If

    End Function
End Module

