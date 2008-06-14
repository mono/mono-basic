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

Friend Class TestView
    Private m_HashTable As New Generic.Dictionary(Of Test, ListViewItem)
    Private m_Form As frmMain

    Sub New(ByVal Form As frmMain)
        m_Form = Form
    End Sub

    Function GetListViewItem(ByVal Test As Test) As ListViewItem
        If m_HashTable.ContainsKey(Test) = False Then
            m_HashTable.Add(Test, CreateListViewItem(Test))
            AddHandler Test.Executed, AddressOf Update
            AddHandler Test.Executing, AddressOf UpdateRunning
            AddHandler Test.Changed, AddressOf Update
        End If
        Return m_HashTable.Item(Test)
    End Function

    Private Function CreateListViewItem(ByVal Test As Test) As ListViewItem
        Dim newItem As New ListViewItem()
        newItem.Text = Test.Name
        newItem.Tag = Test

        newItem.ImageIndex = m_Form.GetIconIndex(rt.Test.Results.NotRun)
        newItem.SubItems.Add("")
        newItem.SubItems.Add("") 'Compiler
        newItem.SubItems.Add("") 'Result
        newItem.SubItems.Add("") 'OldResult
        newItem.SubItems.Add("") 'Failed Verification
        newItem.SubItems.Add("") 'Date
        newItem.SubItems.Add("") 'Path

        Update(newItem)

        Return newItem
    End Function

    Private Sub Update(ByVal Test As Test)
        Update(m_HashTable.Item(Test))
    End Sub

    Private Delegate Sub UpdateDelegate(ByVal Item As ListViewItem)
    Private Delegate Sub UpdateRunningDelegate(ByVal Item As Test)

    Private Sub UpdateRunning(ByVal Test As Test)
        Dim Item As ListViewItem = m_HashTable.Item(Test)

        If Item.ListView IsNot Nothing AndAlso Item.ListView.InvokeRequired Then
            Item.ListView.BeginInvoke(New UpdateRunningDelegate(AddressOf UpdateRunning), Test)
            Return
        End If

        Item.ImageIndex = m_Form.GetIconIndex(Test.Result)
        Item.SubItems(1).Text = ""
        Item.SubItems(2).Text = "Running..."
        Item.SubItems(3).Text = ""
        Item.SubItems(4).Text = ""
    End Sub

    Private Sub Update(ByVal Item As ListViewItem)
        Dim test As Test = DirectCast(Item.Tag, Test)

        If Item.ListView IsNot Nothing AndAlso Item.ListView.InvokeRequired Then
            Item.ListView.BeginInvoke(New UpdateDelegate(AddressOf Update), Item)
            Return
        End If

        Const datetimeformat As String = "dd/MM/yyyy HH:mm"
        Dim testresult As Test.Results
        If test.Result = rt.Test.Results.NotRun Then
            testresult = test.OldResult
        ElseIf test.Result = rt.Test.Results.Failed AndAlso (test.OldResult = rt.Test.Results.Regressed OrElse test.OldResult = rt.Test.Results.Success) Then
            testresult = rt.Test.Results.Regressed
        Else
            testresult = test.Result
        End If
        Item.SubItems(1).Text = ""
        Item.SubItems(5).Text = test.LastRun.ToString(datetimeformat)
        Item.ImageIndex = m_Form.GetIconIndex(testresult)

        If test.Run AndAlso test.VBNCVerification IsNot Nothing AndAlso test.VBNCVerification.Process IsNot Nothing AndAlso test.VBNCVerification.Process.FileVersion IsNot Nothing Then
            Item.SubItems(1).Text = test.VBNCVerification.Process.FileVersion.FileVersion & " (" & test.VBNCVerification.Process.LastWriteDate.ToString(datetimeformat) & ")"
        End If

        Item.SubItems(2).Text = test.Result.ToString
        Item.SubItems(3).Text = test.OldResult.ToString
        If test.FailedVerificationMessage <> "" Then
            Dim idx As Integer = test.FailedVerificationMessage.IndexOf(vbNewLine)
            If idx < 0 Then idx = test.FailedVerificationMessage.Length
            Item.SubItems(4).Text = test.FailedVerificationMessage.Substring(0, idx)
        Else
            Item.SubItems(4).Text = ""
        End If
        Item.SubItems(6).Text = test.BasePath
    End Sub
End Class
