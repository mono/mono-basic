Imports System
Imports System.Collections
Imports System.Reflection

Namespace MethodInvocation19
    Class ParsedObject

    End Class

    Class QualifiedIdentifier
        Inherits ParsedObject
    End Class

    Class ConstructedTypeName
        Inherits ParsedObject
    End Class

    Partial Public Class Tester

        Private Function ParseConstructedTypeName(ByVal Parent As ParsedObject) As ConstructedTypeName
            Dim result As ConstructedTypeName

            Dim m_QualifiedIdentifier As QualifiedIdentifier

            m_QualifiedIdentifier = ParseQualifiedIdentifier(result)
        End Function


    End Class
    Partial Class Tester
        Private Shared Function ParseQualifiedIdentifier(ByVal Parent As ParsedObject, ByVal str As String) As QualifiedIdentifier

        End Function
        Private Function ParseQualifiedIdentifier(ByVal Parent As ParsedObject) As QualifiedIdentifier

        End Function
    End Class

    Class Test
        Shared Function Main() As Integer

        End Function
    End Class
End Namespace