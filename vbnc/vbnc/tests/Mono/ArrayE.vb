Imports System

Module ArrayE

    Dim a As Integer() = {1, 2, 3, 4}

    Public Property myprop() As Integer()
        Get
            Return a
        End Get
        Set(ByVal Value As Integer())
            a = Value
        End Set
    End Property

    Function Main() As Integer

        ReDim Preserve myprop(6)
        Dim arr As Integer() = {1, 2, 3, 4, 10, 12, 14}
        myprop(4) = 10
        myprop(5) = 12
        myprop(6) = 14

        For i As Integer = 0 To myprop.Length - 1
            Console.WriteLine(myprop(i))
            If myprop(i) <> arr(i) Then
                System.Console.WriteLine("#AE1 - Array Statement failed") : Return 1
            End If
        Next

    End Function

End Module