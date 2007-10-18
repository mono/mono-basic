REM Target: library

Imports System
              
NameSpace NSEvent
                                                                  
    Public Class C

        Delegate Sub EvtHan(ByVal i As Integer, ByVal y As String)
        Public Event E As EvtHan

        Public Sub S()
            RaiseEvent E(10, "abc")
        End Sub
    End Class

End NameSpace
