Imports System
Module LongTypeCharTest
    Function Main() As Integer
        Dim m As Long
        m = f(20)
        If m <> 20 Then
            System.Console.WriteLine("LongTypeCharTest: failed") : Return 1
        End If
        Exit Function
    End Function

    Function f&(ByVal param%)
        f& = param
    End Function
End Module
