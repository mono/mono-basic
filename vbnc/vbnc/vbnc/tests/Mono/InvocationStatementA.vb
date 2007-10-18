Imports Microsoft.VisualBasic
Imports System

Module InvocationStatementA
    Dim i As Integer = 0

    Delegate Function Df(ByVal a As Integer) As Object

    Function f1() As Object
        i += 1
        f2()
    End Function

    Function f2() As Integer
        i += 2
    End Function

    Function f2(ByVal i As Integer) As Integer
        i += 5
        Return i
    End Function

    Function f2(ByVal o As Object) As Boolean
        Return True
    End Function

    Function f3(ByVal j As Integer) As Object
        i += j
    End Function

    Function f4(ByVal j As Integer) As Object
        i += j * 10
    End Function

    Function Main() As Integer

        Call f1()
        If i <> 3 Then
            System.Console.WriteLine("#ISB1 - Invocation Statement failed") : Return 1
        End If

        If f2(i) <> 8 Then
            System.Console.WriteLine("#ISB2 - Invocation Statement failed") : Return 1
        End If

        If Not f2("Hello") Then
            System.Console.WriteLine("#ISB3 - Invocation Statement failed") : Return 1
        End If

        If Not f2(2.3D) Then
            System.Console.WriteLine("#ISB4 - Invocation Statement failed") : Return 1
        End If

        Dim d1, d2 As Df
        d1 = New Df(AddressOf f3)
        d2 = New Df(AddressOf f4)

        d1.Invoke(2) : d2.Invoke(2)
        If i <> 25 Then
            System.Console.WriteLine("#ISB5 - Invocation Statement failed") : Return 1
        End If

    End Function

End Module
