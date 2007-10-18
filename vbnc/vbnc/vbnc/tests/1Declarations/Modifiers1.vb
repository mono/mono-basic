'Test modifiers
Namespace Modifiers1
    Friend Structure d
        Dim field As String
        Private Structure c
            Dim field As String
        End Structure
    End Structure
    Public Structure e
        Dim field As String
        Private Structure b
            Dim field As String
        End Structure
    End Structure
End Namespace