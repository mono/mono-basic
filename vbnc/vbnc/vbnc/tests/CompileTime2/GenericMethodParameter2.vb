Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericMethodParameter2
    Class ConstructorDeclaration

        Shared Function Main() As Integer

        End Function
    End Class

    Class Base(Of T)
        Inherits Generic.List(Of T)
    End Class

    Class Derived
        Inherits Base(Of Object)

        Function GetSpecificMembers(Of T)() As Generic.List(Of T)
            Dim result As New Generic.List(Of T)

            For Each obj As Object In Me
                If TypeOf obj Is T Then
                    result.Add(CType(CObj(obj), T))
                End If
            Next

            Return result
        End Function


    End Class
End Namespace