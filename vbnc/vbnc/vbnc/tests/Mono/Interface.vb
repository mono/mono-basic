Option Strict Off
Imports System
Interface I
    Function F()
End Interface

Class C
    Implements I

    Public Function F() Implements I.F
    End Function
End Class

Module InterfaceTest
    Function Main() As Integer
        Try

            Dim x As C = New C()
            x.F()
        Catch e As Exception
            System.Console.WriteLine("#A1:Interface:Failed-error creating instance of class implementing interface" + e.Message) : Return 1
        End Try

        Try
            Dim y As I = New C()
            y.F()
        Catch e As Exception
            System.Console.WriteLine("#A2:Interface:Failed - error declaring varaibles of the interface") : Return 1
            System.Console.WriteLine(e.Message) : Return 1
        End Try
    End Function
End Module
