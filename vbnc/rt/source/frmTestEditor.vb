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

Public Class frmTestEditor

    Private Sub frmTestEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If txtFile.Text = "" Then
                MsgBox("Specify a file name")
                Exit Sub
            ElseIf IO.File.Exists(IO.Path.Combine(txtFolder.Text, txtFile.Text)) Then
                If MsgBox("Do you want to overwrite the current file?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            End If

            IO.File.WriteAllText(IO.Path.Combine(txtFolder.Text, txtFile.Text), txtCode.Text, System.Text.Encoding.GetEncoding(65001))

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Sub Colorize()

    End Sub

    Private Sub txtCode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.Enter
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.Leave
        Me.AcceptButton = cmdSave
    End Sub
End Class