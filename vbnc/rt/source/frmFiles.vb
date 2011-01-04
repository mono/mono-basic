Imports System.Collections.Generic
Imports System.IO

Class frmFiles
    Private m_Main As frmMain

    Sub New(ByVal Main As frmMain)
        m_Main = Main

        InitializeComponent()
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        Try
            'Check each file
            Dim files As New List(Of String)
            Dim dir As String = Path.GetDirectoryName(m_Main.Tests.Filename)
            Dim found As Boolean

            For Each vbfile As String In Directory.EnumerateFiles(dir, "*.vb", SearchOption.AllDirectories)
                found = False
                If vbfile.EndsWith("Bugs\bug-80967.vb") Then Continue For
                If Path.GetFileName(Path.GetDirectoryName(vbfile)) = "Generated" Then Continue For
                For Each t As Test In m_Main.Tests.Values
                    For j As Integer = 0 To t.Files.Count - 1
                        If vbfile = Path.GetFullPath(Path.Combine(t.FullWorkingDirectory, t.Files(j))) Then
                            found = True
                            Exit For
                        End If
                    Next
                    If found Then Exit For
                Next
                If found Then Continue For
                files.Add(vbfile)
            Next

            For Each s As String In files
                lstFiles.Items.Add(s)
            Next
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdCreateTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateTest.Click
        Try
            For Each item As ListViewItem In lstFiles.SelectedItems
                Using frmNew As New frmNewTest(m_Main)
                    frmNew.txtName.Text = Path.GetFileNameWithoutExtension(item.Text)
                    frmNew.txtFilename.Text = item.Text
                    frmNew.ShowDialog(Me)
                End Using
            Next
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Try
            Close()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub
End Class