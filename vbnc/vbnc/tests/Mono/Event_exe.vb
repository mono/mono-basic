REM CompilerOptions: /r:Event_dll.dll

Imports System
Imports NSEvent

Namespace NSEvent
    Class C1
        Inherits C

        Function call_S() As Object
            S()
        End Function

        Sub EH(ByVal i As Integer, ByVal y As String) Handles MyBase.E
            Console.WriteLine("event-H called")
        End Sub
    End Class

End Namespace

Module M
    Function Main() As Integer
        Dim y As New C1()
        y.call_S()
    End Function
End Module
