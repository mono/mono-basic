Imports System.Collections
Imports System

Namespace GenericShadows1
    Class ParsedObject

    End Class

    Interface INameable

    End Interface

    Class Base(Of T As {ParsedObject})
        Inherits ParsedObject

        Private m_List As Generic.List(Of T)

        Sub New()
            m_list = New Generic.List(Of T)
        End Sub

        Function Add(ByVal Item As T) As T
            m_List.Add(Item)
            Return Item
        End Function

        ReadOnly Property Count() As Integer
            Get
                Return m_List.Count
            End Get
        End Property
    End Class

    Class Derived(Of T As {ParsedObject, INameable})
        Inherits Base(Of T)

        Shadows Function Add(ByVal Item As T) As T
            MyBase.Add(Item)
            Return Item
        End Function

    End Class

    Class Tester
        Inherits ParsedObject
        Implements INameable

        Shared Function Main() As Integer
            Dim dt As New Derived(Of Tester)
            Dim t As New Tester
            dt.Add(t)
            Return dt.Count - 1
        End Function
    End Class
End Namespace