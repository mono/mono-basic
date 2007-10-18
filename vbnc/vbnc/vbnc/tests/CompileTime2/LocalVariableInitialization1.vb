Imports System
Imports System.Collections
Imports System.Reflection

Namespace LocalVariableInitialization1
    Module Test
        Sub New()
            Dim v As type = GetType(Test)
            Dim fields As FieldInfo()
            fields = v.GetFields()
        End Sub

        Function Main() As Integer

        End Function
    End Module
End Namespace