Imports System
Imports System.IO
Public Module modmain
    Dim f As Object = True
    Function Main() As Integer
        Do While Not f
            Return 1
        Loop
        Return 0
    End Function
End Module