Imports system.collections
Imports system.collections.generic

Namespace TCClaims
    Public Class TCClaimsWorkspaceDocument
        Implements IComparable

        Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
        End Function
        Public Class TCClaimsWorkspaceDocumentCollection
            Inherits Generic.List(Of TCClaimsWorkspaceDocument)

            Public Sub New()
                MyBase.New()
            End Sub

            Public Sub SortByTitle()
                Me.Sort(AddressOf TCClaimsWorkspaceDocument.CompareByTitle)
            End Sub
        End Class

        Private Shared Function CompareByTitle(ByVal doc1 As TCClaimsWorkspaceDocument, ByVal doc2 As TCClaimsWorkspaceDocument) As Integer
            Return String.Compare(doc1.tostring, doc2.tostring)
        End Function
    End Class
End Namespace
