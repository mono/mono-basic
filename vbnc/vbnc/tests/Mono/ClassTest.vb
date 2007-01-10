Imports System
Class C1
    Public a As Integer = 47
    Public Function S() As Object
        Dim myInnerC As New C2()
        Dim b As Integer = myInnerC.H()
        If b <> 147 Then
            System.Console.WriteLine("#A5:Error in accessing inner class") : Return 1
        End If
    End Function
    Public Function G() As Integer
        Return 38
    End Function
    Class C2
        Public Function H() As Integer
            Return 147
        End Function
    End Class
End Class
Module M
    Function Main() As Integer
        Try
            Dim myCInstance As New C1()
        Catch e As Exception
            Console.WriteLine("#A1:Error in creating class instance:" + e.Message)
        End Try

        Dim myC As New C1()

        Try
            If myC.a <> 47 Then
                System.Console.WriteLine("#A2-ClassTest:Failed-Error in accessing class data member") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        Try
            myC.S()
        Catch e As Exception
            Console.WriteLine("#A3:Error in accessing Function from a class:" + e.Message)
        End Try

        Try
            Dim c As Integer = myC.G()
            If c <> 38 Then
                System.Console.WriteLine("#A4-ClassTest:Failed-Error in  accessing Function of a class") : Return 1
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Function
End Module
