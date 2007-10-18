'Author:
'   K. Satya Sudha (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System
Module Test
    Function Main() As Integer
        Dim a As Boolean = "7" Like "?[]"
        If a <> True Then
            Throw New Exception("#A1 - Like operator failed")
        End If

        a = "7" Like "[]?"
        If a <> True Then
            Throw New Exception("#A2 - Like operator failed")
        End If

        a = "7" Like "[]*"
        If a <> True Then
            Throw New Exception("#A3 - Like operator failed")
        End If

        a = "7" Like "[]#"
        If a <> True Then
            Throw New Exception("#A4 - Like operator failed")
        End If

        a = "7" Like "#[]"
        If a <> True Then
            Throw New Exception("#A5 - Like operator failed")
        End If

    End Function
End Module

