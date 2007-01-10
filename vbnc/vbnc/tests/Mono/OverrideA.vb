Option Strict Off
Imports System

Class C1
    Overridable Function fun(ByVal j As Integer)
    End Function
End Class

Class C2
    Inherits C1
    Overrides Function fun(ByVal j As Integer)
        i = j
        Return i
    End Function
    Public i As Integer
End Class

Module InheritanceM
    Function Main() As Integer
        Dim a As Object = New C2()
        Try
            a.fun(a.i)
        Catch e As Exception
            System.Console.WriteLine(e.Message) : Return 1
        End Try
    End Function
End Module
