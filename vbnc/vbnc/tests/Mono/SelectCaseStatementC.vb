'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

' Testing whether all kinds of primitive types work well as 'Select' expression
' Testing the case clauses like '<relational op> X'
Option Strict Off

Imports System

Module ConditionalStatementsC

    Function Main() As Integer
        Dim errMsg As String = ""
        Dim numMatches As Integer = 0

        Dim a As Byte = 12
        Select Case a
            Case Is < 11
                errMsg = errMsg & "#A1 Case statement not working with Select Expression of type byte " & vbCrLf
            Case Is >= 12
                numMatches += 1
                Console.WriteLine("Byte")
            Case 5 To 16
                errMsg = errMsg & "#A1 Case statement not working with Select Expression of type byte" & vbCrLf
        End Select

        Dim b As Short = 234
        Select Case b
            Case Is <= 23
                errMsg = errMsg & "#A2 Case statement not working with Select Expression of type Short" & vbCrLf
            Case Is = 200
                errMsg = errMsg & "#A2 Case statement not working with Select Expression of type Short" & vbCrLf
            Case Is <> 24
                numMatches += 1
                Console.WriteLine("Short")
        End Select

        Dim c As Integer = 45
        Select Case c
            Case Is < 23
                errMsg = errMsg & "#A3 Case statement not working with Select Expression of type Integer" & vbCrLf
            Case Is <= 44
                errMsg = errMsg & "#A3 Case statement not working with Select Expression of type Integer" & vbCrLf
        End Select

        Dim d As Long = 465
        Select Case d
            Case Is >= 480
                errMsg = errMsg & "#A4 Case statement not working with Select Expression of type Long" & vbCrLf
            Case Else
                numMatches += 1
                Console.WriteLine("Long")
        End Select

        Dim e As Decimal = 234232
        Select Case e
            Case 12 To 34
                errMsg = errMsg & "#A5 Case statement not working with Select Expression of type Decimal" & vbCrLf
            Case Is >= 200
                numMatches += 1
                Console.WriteLine("Decimal")
        End Select

        Dim f As Single = 23.5
        Select Case f
            Case Is <= 23.6
                numMatches += 1
                Console.WriteLine("Single")
            Case Is = 24
                errMsg = errMsg & "#A6 Case statement not working with Select Expression of type Single" & vbCrLf
        End Select

        Dim g As Double = 1.9
        Select Case g
            Case Is > 34
                errMsg = errMsg & "#A7 Case statement not working with Select Expression of type double" & vbCrLf
            Case Is < 20
                numMatches += 1
                Console.WriteLine("Double")
        End Select

        Dim h As String = "Sudha"
        Select Case h
            Case Is <> "Satya"
                numMatches += 1
                Console.WriteLine("String")
            Case Else
                errMsg = errMsg & "#A8 Case statement not working with Select Expression of type String" & vbCrLf
        End Select

        Dim i As Char = "4"
        Select Case i
            Case Is < "g"
                Console.WriteLine("Char")
                numMatches += 1
            Case Else
                errMsg = errMsg & "#A9 Case statement not working with Select Expression of type Char" & vbCrLf
        End Select

        Dim j As Object = 45.6
        Select Case j
            Case 23 To 90
                numMatches += 1
                Console.WriteLine("Object")
            Case 45, 23, 234
                errMsg = errMsg & "#A10 Case statement not working with Select Expression of type Object" & vbCrLf
        End Select

        Dim k As Date = #4/23/2005#
        Select Case k
            Case Is = #1/1/1998#
                errMsg = errMsg & "#A11 Case statement not working with Select Expression of type DateTime" & vbCrLf
            Case Is >= #1/1/2002#
                numMatches += 1
                Console.WriteLine("DateTime")
            Case Is <= #2/11/2006#
                errMsg = errMsg & "#A11 Case statement not working with Select Expression of type DateTime" & vbCrLf
        End Select

        If (errMsg <> "") Then
            Throw New Exception(errMsg)
        End If
        If numMatches <> 10 Then
            Throw New Exception("select-case statements not working properly")
        End If
    End Function

End Module
