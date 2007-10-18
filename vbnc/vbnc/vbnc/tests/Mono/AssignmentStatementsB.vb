Option Strict Off
Imports System
Imports Microsoft.VisualBasic

Module AssignmentStatementsB

    Function Main() As Integer

        Dim b As Byte = 0
        Dim ch As Char = "a"c
        Dim i As Integer = 0
        Dim str As String = "Hello "

        b += 1
        If b <> CByte(1) Then
            System.Console.WriteLine("#ASB1-Assignment Statement Failed") : Return 1
        End If

        b += i
        If b <> CByte(1) Then
            System.Console.WriteLine("#ASB2-Assignment Statement Failed") : Return 1
        End If

        b += CByte(i)
        If b <> CByte(1) Then
            System.Console.WriteLine("#ASB3-Assignment Statement Failed") : Return 1
        End If

        ch += ChrW(65)
        If ch <> CChar("aA") Then
            System.Console.WriteLine("#ASB4-Assignment Statement Failed") : Return 1
        End If

        str &= "World"
        If str <> "Hello World" Then
            System.Console.WriteLine("#ASB5-Assignment Statement Failed") : Return 1
        End If

        i += "12"
        If i <> 12 Then
            System.Console.WriteLine("#ASB6-Assignment Statement Failed") : Return 1
        End If

    End Function

End Module
