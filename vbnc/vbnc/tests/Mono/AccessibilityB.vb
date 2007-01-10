Imports System
Class C1
    Public a As Integer = 20
    Private b As Integer = 30
    Friend c As Integer = 40
    Protected d As Integer = 50
    Public Function S1() As Object ' All data members of the class should be accessible
        Try

            If a <> 20 Then
                System.Console.WriteLine("#A1-Accessibility:Failed-error accessing value of public data member frm same class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            If b <> 30 Then
                System.Console.WriteLine("#A2-Accessibility:Failed-error accessing value of private data  member from same class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try

            If c <> 40 Then
                System.Console.WriteLine("#A3-Accessibility:Failed-error accessing value of friend data  member from same class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            If d <> 50 Then
                System.Console.WriteLine("#A4-Accessibility:Failed-error accessing value of protected data  member from same class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        S2()
    End Function
    Private Function S2() As Object
    End Function
End Class
Class C2
    Inherits C1
    Public Function DS1() As Object
        'All data members except private members should be accessible
        Try
            If a <> 20 Then
                System.Console.WriteLine("#A5-Accessibility:Failed-error accessing value of public data  member from derived class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            If c <> 40 Then
                System.Console.WriteLine("#A6-Accessibility:Failed-error accessing value of friend data  member from derived class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            If d <> 50 Then
                System.Console.WriteLine("#A7-Accessibility:Failed-error accessing value of protected data  member from derived") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

    End Function
End Class
Class C3
    Public Function S1() As Object 'All public and friend members should be accessible
        Dim myC As New C1()
        Try
            If myC.a <> 20 Then
                System.Console.WriteLine("#A8-Accessibility:Failed-error accessing value of public data  member from another class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            If myC.c <> 40 Then
                System.Console.WriteLine("#A9-Accessibility:Failed-error accessing value of friend data  member from another class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

    End Function
End Class
Module Accessibility
    Function Main() As Integer
        Dim myC1 As New C1()
        myC1.S1()
        Try
            If myC1.a <> 20 Then
                System.Console.WriteLine("#A10-Accessibility:Failed-error accessing value of  public data member form another module") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Dim myC2 As New C2()
        myC2.DS1()
        Dim myC3 As New C3()
        myC3.S1()
    End Function
End Module
