Imports System.Collections.Generic
Imports System.IO

Class frmNewTest
    Private m_Main As frmMain

    Sub New(ByVal Main As frmMain)
        m_Main = Main

        InitializeComponent()
    End Sub

    Private Sub cmdCreateTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateTest.Click
        Try
            m_Main.CreateTest(txtName.Text, txtFilename.Text)
            Close()
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