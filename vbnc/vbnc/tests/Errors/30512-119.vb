Option Strict On
Imports System.Data
Class A
    Sub Main()
        Dim s As String = Me!TABLE_TYPE
        'Dim SchemaTable As DataTable
        'For table As Integer = 0 To SchemaTable.Rows.Count - 1
        '    If SchemaTable.Rows(table)!TABLE_TYPE.ToString = "TABLE" Then
        '    End If
        'Next
    End Sub

    Default ReadOnly Property I(ByVal a As Integer) As String
        Get
            Return ""
        End Get
    End Property
End Class