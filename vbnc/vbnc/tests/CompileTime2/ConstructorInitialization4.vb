Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization4
    Enum ks
        value1
        value2
    End Enum
    Class Test
        Shared var As ks() = {ks.value1, ks.value2}
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace