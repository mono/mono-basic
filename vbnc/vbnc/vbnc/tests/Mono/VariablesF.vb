'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System
Class A
    Dim i As Integer
    Dim c As Char
    Dim by As Byte
    Dim l As Long
    Dim b As Boolean
    Dim s As Single
    Dim d As Double
    Dim de As Decimal
    Dim da As Date
    Dim st As String
    Function fun()
        If i <> 0 Then
            System.Console.WriteLine("Integer Default is not Zero") : Return 1
        End If
        If c <> Nothing Then
            System.Console.WriteLine("Char Default is not Nothing") : Return 1
        End If
        If by <> 0 Then
            System.Console.WriteLine("Byte Default is not Zero") : Return 1
        End If
        If l <> 0 Then
            System.Console.WriteLine("Long Default is not Zero") : Return 1
        End If
        If b <> False Then
            System.Console.WriteLine("Boolean Default is not Zero") : Return 1
        End If
        If s <> 0.0 Then
            System.Console.WriteLine("Single Default is not Zero") : Return 1
        End If
        If d <> 0.0 Then
            System.Console.WriteLine("Double Default is not Zero") : Return 1
        End If
        If de <> 0 Then
            System.Console.WriteLine("Decimal Default is not Zero") : Return 1
        End If
        If da <> "1/1/0001 12:00:00 AM" Then
            System.Console.WriteLine("Date Default is not 1/1/0001 12:00:00 AM") : Return 1
        End If
        If st <> 0 Then
            System.Console.WriteLine("String Default is not Null") : Return 1
        End If
    End Function
End Class

Module Default1
    Function Main() As Integer
        Dim a As A = New A()
        a.fun()
    End Function
End Module
