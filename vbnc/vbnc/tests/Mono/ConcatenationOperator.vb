Imports System
Imports System.Globalization
Imports System.Threading

Module ConcatenationOperator
    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim a As String = "Hello "
        Dim b As String = "World"

        Dim c As String = a & b
        If c <> "Hello World" Then
            System.Console.WriteLine("#A1-Concatenation Failed") : Return 1
        End If

        c = a & CInt(123)
        If c <> "Hello 123" Then
            System.Console.WriteLine("#A2-Concatenation Failed") : Return 1
        End If

        c = a & Nothing
        If c <> "Hello " Then
            System.Console.WriteLine("#A3-Concatenation Failed") : Return 1
        End If

        c = Nothing & a
        If c <> "Hello " Then
            System.Console.WriteLine("#A4-Concatenation Failed") : Return 1
        End If

        c = a & CDec(123.23)
        If c <> "Hello 123.23" Then
            System.Console.WriteLine("#A5-Concatenation Failed") : Return 1
        End If

        c = a & CBool(123)
        If c <> "Hello True" Then
            System.Console.WriteLine("#A6-Concatenation Failed") : Return 1
        End If

    End Function

End Module
