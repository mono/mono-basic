Imports System

Module M
    Enum E
        A
        B
    End Enum

    Function InInt(ByVal i As Integer) As Object
    End Function

    Function InLong(ByVal i As Long) As Object
    End Function

    Function InEnum(ByVal e As E) As Object
    End Function

    Function Main() As Integer
        Dim e1 As E

        e1 = E.A
        If e1.GetType().ToString() <> GetType(E).ToString() Then
            System.Console.WriteLine("#A1: wrong type") : Return 1
        End If
        If E.A.GetType().ToString() <> GetType(E).ToString() Then
            System.Console.WriteLine("#A2: wrong type") : Return 1
        End If
        Dim e2 As E
        e2 = e1
        Dim i As Integer
        i = e2
        InInt(e2)
        InInt(E.B)
        InLong(e2)
        InLong(E.B)
        InEnum(e2)
        InEnum(0)
    End Function
End Module
