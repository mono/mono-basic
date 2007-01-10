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

Public Class frmNewTest
    Private m_BaseName As String
    Private m_ActualName As String
    Private m_Path As String

    Private Function CreateFullName() As String
        Return IO.Path.Combine(m_Path, m_ActualName & ".vb")
    End Function

    Private Sub CreateActualName()
        Dim number As Integer = 0
        Dim fullname As String

        Do
            number += 1
            m_ActualName = m_BaseName & number.ToString
            fullname = CreateFullName()
        Loop While IO.File.Exists(fullname)

        lblActualName.Text = "Actual filename:  " & m_ActualName & ".vb"
        lblPath.Text = "Path: " & m_Path
    End Sub

    Private Sub chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCreateMain.CheckedChanged, chkCreateCode.CheckedChanged
        Try
            CreateCode()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateCode()
        Dim code As New System.Text.StringBuilder
        If chkCreateCode.Checked Then
            code.AppendLine("Class " & ActualName)
            If chkCreateMain.Checked Then
                code.AppendLine("   Shared Function Main() As Integer")
                code.AppendLine("       Dim result As Boolean")
                code.AppendLine()
                code.AppendLine()
                code.AppendLine()
                code.AppendLine("       If result = False Then")
                code.AppendLine("           System.Console.WriteLine(""FAIL " & ActualName & """)")
                code.AppendLine("           System.Console.WriteLine(""(detailed message)"")")
                code.AppendLine("           Return 1")
                code.AppendLine("       End If")
                code.AppendLine("   End Function")
            End If
            code.AppendLine("End Class")
        End If
        txtCode.Text = code.ToString
    End Sub

    ReadOnly Property ActualName() As String
        Get
            Return m_ActualName
        End Get
    End Property

    Private Sub txtTestName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTestName.TextChanged
        Try
            m_BaseName = txtTestName.Text
            CreateActualName()
            CreateCode()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Shadows Function ShowDialog(ByVal Parent As Form, ByVal Path As String, Optional ByVal BaseTestName As String = "") As DialogResult
        m_Path = Path
        If BaseTestName <> "" Then
            Do While IsNumeric(BaseTestName.Chars(BaseTestName.Length - 1))
                BaseTestName = BaseTestName.Substring(0, BaseTestName.Length - 1)
            Loop
            m_BaseName = BaseTestName
            txtTestName.Text = m_BaseName
            CreateActualName()
            CreateCode()
        End If

        Return MyBase.ShowDialog(Parent)
    End Function

    Private Sub cmdSaveAndEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveAndEdit.Click
        Try
            IO.File.WriteAllText(CreateFullName, txtCode.Text)
            MainModule.ViewFiles(CreateFullName)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub
End Class