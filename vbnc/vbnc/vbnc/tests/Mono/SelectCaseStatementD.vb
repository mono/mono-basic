'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

' Testing whether variables work well as 'case' expressions
' 'Select' expressions and 'case' expressions are of different types
Option Strict Off

Imports System

Module SelectCaseStatementD

    Function Main() As Integer
        Dim errMsg As String = ""
        Dim numMatches As Integer = 0

        Dim a As Byte = 5
        Dim b As Integer = 5
        Dim c As Decimal = 10
        Dim d As String = "15"

        Select Case a
            Case Is <= b
                numMatches += 1
            Case Is >= c
                errMsg = errMsg & "#A1 Case statement not working with Select Expression of type byte " & vbCrLf
            Case c To d
                errMsg = errMsg & "#A1 Case statement not working with Select Expression of type byte" & vbCrLf
        End Select

        a = 12
        Select Case a
            Case Is <= d
                numMatches += 1
            Case Is = c
                errMsg = errMsg & "#A2 Case statement not working with Select Expression of type Short" & vbCrLf
            Case Is <> b
                errMsg = errMsg & "#A2 Case statement not working with Select Expression of type Short" & vbCrLf
        End Select

        a = 20
        Select Case a
            Case Is < c, Is = b, d To 90
                numMatches += 1
            Case Else
                errMsg = errMsg & "#A3 Case statement not working with Select Expression of type Integer" & vbCrLf
        End Select
        If (errMsg <> "") Then
            Throw New Exception(errMsg)
        End If
        If (numMatches <> 3) Then
            Throw New Exception("Select Case statement not working properly")
        End If
    End Function

End Module
