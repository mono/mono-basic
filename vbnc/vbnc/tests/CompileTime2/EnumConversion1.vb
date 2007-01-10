Imports System
Imports System.Collections
Imports System.Reflection

Namespace EnumConversion1
    Enum T1
        value = 1
    End Enum
    Enum T2
        value = 2
    End Enum

    Class Test
        Shared Function Main() As Integer
            Dim v As T1
            v = CType(T2.Value, T1)
            Return v - 2
        End Function
    End Class
End Namespace