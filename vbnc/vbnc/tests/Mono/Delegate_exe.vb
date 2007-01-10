REM CompilerOptions: /r:Delegate_dll.dll

Imports System
Imports NSDelegate

Class C1
    Dim x1 As New C()
    Function __f() As Object
        System.Console.WriteLine("__f called")
    End Function


    Public Function s() As Object
        x1.callSD(AddressOf Me.__f)
    End Function
End Class

Module M
    Function main() As Integer
        Dim x As New C1()
        x.s()
    End Function
End Module
