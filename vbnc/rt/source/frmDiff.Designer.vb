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

Partial Class frmDiff
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.cmdClose = New System.Windows.Forms.Button
        Me.lblFile1 = New System.Windows.Forms.Label
        Me.lblFile2 = New System.Windows.Forms.Label
        Me.txtFile1 = New System.Windows.Forms.TextBox
        Me.txtFile2 = New System.Windows.Forms.TextBox
        Me.txtDiff1 = New System.Windows.Forms.TextBox
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.txtDiff2 = New System.Windows.Forms.TextBox
        Me.txtDiff = New System.Windows.Forms.TextBox
        Me.cmdAcceptChanges = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Location = New System.Drawing.Point(604, 484)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "&Close"
        '
        'lblFile1
        '
        Me.lblFile1.Location = New System.Drawing.Point(13, 24)
        Me.lblFile1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.lblFile1.Name = "lblFile1"
        Me.lblFile1.Size = New System.Drawing.Size(329, 21)
        Me.lblFile1.TabIndex = 2
        Me.lblFile1.Tag = ""
        Me.lblFile1.Text = "File 1:"
        '
        'lblFile2
        '
        Me.lblFile2.Location = New System.Drawing.Point(349, 24)
        Me.lblFile2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.lblFile2.Name = "lblFile2"
        Me.lblFile2.Size = New System.Drawing.Size(329, 21)
        Me.lblFile2.TabIndex = 3
        Me.lblFile2.Text = "File 2:"
        '
        'txtFile1
        '
        Me.txtFile1.Location = New System.Drawing.Point(13, 46)
        Me.txtFile1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.txtFile1.Name = "txtFile1"
        Me.txtFile1.Size = New System.Drawing.Size(329, 21)
        Me.txtFile1.TabIndex = 4
        '
        'txtFile2
        '
        Me.txtFile2.Location = New System.Drawing.Point(349, 46)
        Me.txtFile2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.txtFile2.Name = "txtFile2"
        Me.txtFile2.Size = New System.Drawing.Size(329, 21)
        Me.txtFile2.TabIndex = 5
        '
        'txtDiff1
        '
        Me.txtDiff1.Location = New System.Drawing.Point(13, 347)
        Me.txtDiff1.Multiline = True
        Me.txtDiff1.Name = "txtDiff1"
        Me.txtDiff1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDiff1.Size = New System.Drawing.Size(330, 124)
        Me.txtDiff1.TabIndex = 6
        Me.txtDiff1.WordWrap = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Visible = True
        '
        'txtDiff2
        '
        Me.txtDiff2.Location = New System.Drawing.Point(348, 347)
        Me.txtDiff2.Multiline = True
        Me.txtDiff2.Name = "txtDiff2"
        Me.txtDiff2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDiff2.Size = New System.Drawing.Size(330, 124)
        Me.txtDiff2.TabIndex = 7
        Me.txtDiff2.WordWrap = False
        '
        'txtDiff
        '
        Me.txtDiff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiff.Location = New System.Drawing.Point(13, 74)
        Me.txtDiff.Multiline = True
        Me.txtDiff.Name = "txtDiff"
        Me.txtDiff.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDiff.Size = New System.Drawing.Size(666, 263)
        Me.txtDiff.TabIndex = 8
        Me.txtDiff.WordWrap = False
        '
        'cmdAcceptChanges
        '
        Me.cmdAcceptChanges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAcceptChanges.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdAcceptChanges.Location = New System.Drawing.Point(13, 484)
        Me.cmdAcceptChanges.Name = "cmdAcceptChanges"
        Me.cmdAcceptChanges.Size = New System.Drawing.Size(330, 23)
        Me.cmdAcceptChanges.TabIndex = 9
        Me.cmdAcceptChanges.Text = "&Set new output as verified"
        '
        'frmDiff
        '
        Me.AcceptButton = Me.cmdAcceptChanges
        Me.ClientSize = New System.Drawing.Size(691, 519)
        Me.Controls.Add(Me.cmdAcceptChanges)
        Me.Controls.Add(Me.txtDiff)
        Me.Controls.Add(Me.txtDiff2)
        Me.Controls.Add(Me.txtDiff1)
        Me.Controls.Add(Me.txtFile2)
        Me.Controls.Add(Me.txtFile1)
        Me.Controls.Add(Me.lblFile2)
        Me.Controls.Add(Me.lblFile1)
        Me.Controls.Add(Me.cmdClose)
        Me.Name = "frmDiff"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmDiff"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lblFile1 As System.Windows.Forms.Label
    Friend WithEvents lblFile2 As System.Windows.Forms.Label
    Friend WithEvents txtFile1 As System.Windows.Forms.TextBox
    Friend WithEvents txtFile2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiff1 As System.Windows.Forms.TextBox
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents txtDiff2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiff As System.Windows.Forms.TextBox
    Friend WithEvents cmdAcceptChanges As System.Windows.Forms.Button
End Class
