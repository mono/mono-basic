#Const DEBUG = True
#Const TEST = False

Imports System.Diagnostics

Class ConditionalAttribute1
    Private Shared V As Integer = 1

    Shared Function Main() As Integer
        a(2) 'This should add
        b(3) 'This should not add
#Const DEBUG = False
        a(5) 'This should not add
#Const TEST = True
        b(7) 'This should add

        Return v - (2 * 7)
    End Function

    <Conditional("DEBUG")> _
    Shared Sub A(ByVal value As Integer)
        v *= value
    End Sub

    <Conditional("TEST")> _
    Shared Sub B(ByVal value As Integer)
        v *= value
    End Sub
End Class