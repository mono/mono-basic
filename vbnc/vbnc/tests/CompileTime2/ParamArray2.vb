Imports System
Imports System.Collections
Imports System.Reflection

Namespace ParamArray2
    Enum KS
        value
    End Enum

    Class Test
        Shared Sub Test(ByVal ParamArray Modifiers As KS())
            Dim m_Modifiers As Generic.List(Of KS)
            m_Modifiers = New Generic.List(Of KS)(Modifiers)
        End Sub
        Shared Function Main() As Integer
            test()
        End Function
    End Class
End Namespace