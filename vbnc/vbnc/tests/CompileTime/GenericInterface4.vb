Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection

Public Class GenericInterface4

    Private m_Queue As New Generic.LinkedList(Of String)

    ReadOnly Property Queue() As Generic.IEnumerable(Of String)
        Get
            Return m_Queue
        End Get
    End Property
End Class
