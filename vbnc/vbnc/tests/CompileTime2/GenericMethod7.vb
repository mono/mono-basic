Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethod7
    Class ParsedObject

    End Class
    Class BaseList(Of T)
        Inherits Generic.List(Of T)

    End Class
    Delegate Function ParseDelegate_Parent(Of T)(ByVal Parent As ParsedObject) As T
    Class Test
        Private Function ParseList(Of M As ParsedObject)(ByVal List As BaseList(Of M), ByVal ParseMethod As ParseDelegate_Parent(Of M), ByVal Parent As ParsedObject) As Boolean
            Dim newObject As M
            List.Add(newObject)
            Return True
        End Function
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace