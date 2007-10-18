Option Strict Off
Imports System
Imports System.Globalization
Imports System.Threading

Module AssignmentStatementsA

    Function Main() As Integer
        Dim i As Integer
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        i = 2
        If i <> 2 Then
            System.Console.WriteLine("#ASA1 - Assignment Statement failed") : Return 1
        End If

        i = 2.3 * 3.45 / 2.3   ' Implicit type conversion
        If i <> 3 Then
            System.Console.WriteLine("#ASA2 - Assignment Statement failed") : Return 1
        End If

        Dim s As String = 2.3 * 3.45 / 2.3
        If s <> 3.45 Then
            System.Console.WriteLine("#ASA3 - Assignment Statement failed") : Return 1
        End If

        s = New Date(2004, 8, 17)
        If s <> New Date(2004, 8, 17) Then
            System.Console.WriteLine("#ASA4 - Assignment Statement failed") : Return 1
        End If

        If s <> "8/17/2004" Then
            System.Console.WriteLine("#ASA5 - Assignment Statement failed") : Return 1
        End If

        Dim obj As New Object()
        Dim obj1, obj2 As Object
        obj1 = obj
        obj2 = obj
        If Not obj1 Is obj2 Then
            System.Console.WriteLine("#ASA6 - Assignment Statement failed") : Return 1
        End If

        Dim obj3 As Object
        obj3 = i
        If obj3 <> 3 Then
            System.Console.WriteLine("#ASA7 - Assignment Statement failed") : Return 1
        End If

        i = 12
        i = obj3
        If i <> 3 Then
            System.Console.WriteLine("#ASA8 - Assignment Statement failed") : Return 1
        End If

    End Function

End Module
