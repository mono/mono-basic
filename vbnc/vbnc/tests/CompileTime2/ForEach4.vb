Imports System
Imports System.Collections
Imports System.Reflection

Namespace ForEach4
    Class Test
        Shared Function AddResources() As Boolean
            'This function is not understood by Reflector.
            Dim result As Boolean = True

            Dim reader As System.Resources.IResourceReader
            For Each resource As System.Collections.DictionaryEntry In reader

            Next

            Return result
        End Function

        Shared Function Main() As Integer
        End Function
    End Class
End Namespace