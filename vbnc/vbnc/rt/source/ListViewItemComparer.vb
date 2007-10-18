' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer

    Private m_Column As Integer
    Private m_Ascending As Boolean = True

    Private WithEvents m_List As ListView

    Public Sub New(Optional ByVal List As ListView = Nothing)
        m_Column = 0
        m_List = List
    End Sub

    Public Sub New(ByVal column As Integer, ByVal Ascending As Boolean)
        m_Column = column
        m_Ascending = Ascending
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim asc As Integer = CInt(IIf(m_Ascending, 1, -1))
        Return [String].Compare(CType(x, ListViewItem).SubItems(m_Column).Text, CType(y, ListViewItem).SubItems(m_Column).Text) * CInt(asc)
    End Function

    Private Sub m_List_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles m_List.ColumnClick
        Try
            Dim header As ColumnHeader
            Dim list As ListView = DirectCast(sender, ListView)

            header = list.Columns(e.Column)
            If e.Column = m_Column Then
                m_Ascending = Not m_Ascending
            Else
                m_Column = e.Column
                m_Ascending = True
            End If

            list.ListViewItemSorter = Me
            list.Sort()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub
End Class
