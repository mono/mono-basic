Option Strict Off
' Testing implicitly declared local varable in funtion

Imports System

Module LocalVariablesB

    Function f1(ByVal a As Integer, ByVal b As Integer) As Integer
        f1 = a + b
    End Function

    Function f2(ByVal x As Integer)
        f2 = x
        If f2 = 0 Then
            Return 0
        Else
            Return f2 + f2(f2 - 1)
        End If

    End Function


    Function Main() As Integer

        Dim a As Integer

        a = f1(10, 12)
        If a <> 22 Then
            System.Console.WriteLine("#LV1") : Return 1
        End If

        a = f2(5)
        If a <> 15 Then
            System.Console.WriteLine("#LV2") : Return 1
        End If

    End Function

End Module