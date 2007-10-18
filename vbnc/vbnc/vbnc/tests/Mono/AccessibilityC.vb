Imports System
Class C1
    Protected Friend a As Integer = 20
End Class
Class C2
    Inherits C1
    Public Function S() As Object
        Try
            If a <> 20 Then
                System.Console.WriteLine("#A1-AccessibilityA:Failed-error accessing value of protected friend data member from derived class") : Return 1

            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Function
End Class

Module Accessibility
    Function Main() As Integer
        Dim myC1 As New C1()
        Try
            If myC1.a <> 20 Then
                System.Console.WriteLine("#A2-AccessibilityA:Failed-error accessing value of protected friend data member from another module") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Dim myC2 As New C2()
        myC2.S()

    End Function
End Module
