Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethod5
    Interface BaseObject

    End Interface
    Class Statement
        Implements BaseObject
    End Class
    Class ForEachStatement
        Inherits Statement
    End Class
    Class ForStatement
        Inherits Statement
    End Class

    Class T
        Function FindFirstParent(Of T1, T2)() As BaseObject

        End Function
        Sub Test()
            Dim m_ContainingStatement As Statement
            m_ContainingStatement = CType(Me.FindFirstParent(Of ForEachStatement, ForStatement)(), Statement)
        End Sub
    End Class
    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace