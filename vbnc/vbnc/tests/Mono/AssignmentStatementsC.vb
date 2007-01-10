Imports System
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading

Module AssignmentStatementsC

    Private str As String = "Hello VB.NET World"
    Public Property mystr() As String
        Get
            Return str
        End Get
        Set(ByVal Value As String)
            str = Value
        End Set
    End Property

    Function Main() As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Mid(str, 7) = "MS.NET"
        If str <> "Hello MS.NET World" Then
            System.Console.WriteLine("#ASC1 - Assignment Statement failed") : Return 1
        End If

        Mid(str, 7, 5) = "ASP.NET"
        If str <> "Hello ASP.NT World" Then
            System.Console.WriteLine("#ASC2 - Assignment Statement failed") : Return 1
        End If

        Mid(str, 7) = "VisualBasic .NET World"
        If str <> "Hello VisualBasic " Then
            System.Console.WriteLine("#ASC3 - Assignment Statement failed") : Return 1
        End If

        Mid(str, 7) = CStr(2.5 * 34.59)
        If str <> "Hello 86.475Basic " Then
            System.Console.WriteLine("#ASC4 - Assignment Statement failed") : Return 1
        End If

        Mid(mystr, 7) = "MS.NET"

        If mystr <> "Hello MS.NETBasic " Then
            System.Console.WriteLine("#ASC5 - Assignment Statement failed") : Return 1
        End If

    End Function

End Module
