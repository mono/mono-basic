Imports System
Class C1
    Public a As Integer
    Private b As Integer
    Friend c As Integer
    Protected d As Integer
    Public Function S1() As Object ' All data members of the Class should be accessible
        a = 10
        b = 20
        c = 30
        d = 40
        S2()
    End Function
    Private Function S2() As Object
    End Function
End Class
Class C2
    Inherits C1
    Public Function DS1() As Object 'All data members except Private members should be accessible
        a = 100
        c = 300
        d = 400
    End Function
End Class
Class C3
    Public Function S1() As Object 'All Public and friEnd members should be accessible
        Dim myC As New C1()
        myC.a = 1000
        myC.c = 3000
    End Function
End Class
Module Accessibility
    Function Main() As Integer
        Dim myC1 As New C1()
        myC1.S1()

        Dim myC2 As New C2()
        myC2.DS1()
        Dim myC3 As New C3()
        myC3.S1()
    End Function
End Module
