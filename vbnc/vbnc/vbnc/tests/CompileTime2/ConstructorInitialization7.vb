Imports System
Imports System.Collections
Imports System.Reflection

Namespace ConstructorInitialization7
    Module Test
        Public Value1 As Integer = 2
        Public Value2 As Integer
        
        Sub New()
            Value2 = 3
        End Sub

        Function Main() As Integer
            Return Value1 - Value2 + 1
        End Function
    End Module
End Namespace