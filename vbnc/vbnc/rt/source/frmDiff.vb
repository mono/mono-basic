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

Class frmDiff
	Private m_Diff As XmlCompare
	Function ShowDiff(ByVal Owner As IWin32Window, ByVal Diff As XmlCompare) As DialogResult
		Try
			m_Diff = Diff
			txtFile1.Text = Diff.File1
			txtFile2.Text = Diff.File2
			txtDiff.Text = Diff.Result
			txtDiff1.Text = IO.File.ReadAllText(Diff.File1)
			txtDiff2.Text = IO.File.ReadAllText(Diff.File2)
			Return Me.ShowDialog(Owner)
		Catch ex As System.Exception
			MsgBox(ex.Message & vbNewLine & ex.StackTrace)
		End Try
	End Function

	Private Sub frmDiff_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
		Try
			txtFile1.Width = (Me.ClientSize.Width \ 2) - txtFile1.Left * 2
			txtFile2.Width = txtFile1.Width
			txtFile2.Left = txtFile1.Left + txtFile1.Width + txtFile1.Left * 2
			txtDiff1.Left = txtFile1.Left
			txtDiff2.Left = txtFile2.Left
			txtDiff1.Width = txtFile1.Width
			txtDiff2.Width = txtFile2.Width
			txtDiff1.Top = txtDiff.Top + txtDiff.Height + txtFile1.Left
			txtDiff2.Top = txtDiff1.Top
		Catch ex As System.Exception
			MsgBox(ex.Message & vbNewLine & ex.StackTrace)
		End Try
	End Sub

	Private Sub cmdAcceptChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAcceptChanges.Click
		Try
			If txtFile1.Text Like "*.verified.xml" Then
				IO.File.Delete(txtFile1.Text)
				IO.File.Move(txtFile2.Text, txtFile1.Text)
			ElseIf txtFile2.Text Like "*.verified.xml" Then
				IO.File.Delete(txtFile2.Text)
				IO.File.Move(txtFile1.Text, txtFile2.Text)
			Else
				MsgBox("No file is the verified one!")
			End If
		Catch ex As System.Exception
			MsgBox(ex.Message & vbNewLine & ex.StackTrace)
		End Try
	End Sub
End Class