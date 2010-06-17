'
' FileSystemOperationUIQuestion.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com>
'
' Copyright (C) 2007 Novell (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'

#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System.IO
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Microsoft.VisualBasic.FileIO
    Friend Class FileSystemOperationUIQuestion
        Inherits System.Windows.Forms.Form

        Private m_Answer As Answer

        Sub New()
            InitializeComponent()
        End Sub

        Shadows Function ShowDialog() As Answer
            MyBase.ShowDialog()
            Return m_Answer
        End Function

        Public Enum Answer
            Yes
            YesToAll
            No
            NoToAll
            Cancel
        End Enum

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            m_Answer = Answer.Cancel
            Close()
        End Sub

        Private Sub cmdNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNo.Click
            If System.Windows.Forms.Control.ModifierKeys = Windows.Forms.Keys.Shift Then
                m_Answer = Answer.NoToAll
            Else
                m_Answer = Answer.No
            End If
            Close()
        End Sub

        Private Sub cmdYesToAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdYesToAll.Click
            m_Answer = Answer.YesToAll
            Close()
        End Sub

        Private Sub cmdYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdYes.Click
            m_Answer = Answer.Yes
            Close()
        End Sub


#Region "Designer code"
        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
            Me.lblTitle = New System.Windows.Forms.Label
            Me.lblText1 = New System.Windows.Forms.Label
            Me.lblText2 = New System.Windows.Forms.Label
            Me.lblDateA = New System.Windows.Forms.Label
            Me.lblSizeA = New System.Windows.Forms.Label
            Me.lblDateB = New System.Windows.Forms.Label
            Me.lblSizeB = New System.Windows.Forms.Label
            Me.cmdYes = New System.Windows.Forms.Button
            Me.cmdYesToAll = New System.Windows.Forms.Button
            Me.cmdNo = New System.Windows.Forms.Button
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.iconA = New System.Windows.Forms.PictureBox
            Me.iconB = New System.Windows.Forms.PictureBox
            CType(Me.iconA, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.iconB, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Location = New System.Drawing.Point(29, 9)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(193, 13)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "This folder already has a file called '{0}'."
            '
            'lblText1
            '
            Me.lblText1.AutoSize = True
            Me.lblText1.Location = New System.Drawing.Point(29, 46)
            Me.lblText1.Name = "lblText1"
            Me.lblText1.Size = New System.Drawing.Size(189, 13)
            Me.lblText1.TabIndex = 1
            Me.lblText1.Text = "Do you want to replace the existing file"
            '
            'lblText2
            '
            Me.lblText2.AutoSize = True
            Me.lblText2.Location = New System.Drawing.Point(29, 110)
            Me.lblText2.Name = "lblText2"
            Me.lblText2.Size = New System.Drawing.Size(94, 13)
            Me.lblText2.TabIndex = 2
            Me.lblText2.Text = "with this other file?"
            '
            'lblDateA
            '
            Me.lblDateA.AutoSize = True
            Me.lblDateA.Location = New System.Drawing.Point(89, 90)
            Me.lblDateA.Name = "lblDateA"
            Me.lblDateA.Size = New System.Drawing.Size(78, 13)
            Me.lblDateA.TabIndex = 3
            Me.lblDateA.Text = "modificado: {0}"
            '
            'lblSizeA
            '
            Me.lblSizeA.AutoSize = True
            Me.lblSizeA.Location = New System.Drawing.Point(89, 71)
            Me.lblSizeA.Name = "lblSizeA"
            Me.lblSizeA.Size = New System.Drawing.Size(34, 13)
            Me.lblSizeA.TabIndex = 4
            Me.lblSizeA.Text = "size a"
            '
            'lblDateB
            '
            Me.lblDateB.AutoSize = True
            Me.lblDateB.Location = New System.Drawing.Point(89, 156)
            Me.lblDateB.Name = "lblDateB"
            Me.lblDateB.Size = New System.Drawing.Size(78, 13)
            Me.lblDateB.TabIndex = 3
            Me.lblDateB.Text = "modificado: {0}"
            '
            'lblSizeB
            '
            Me.lblSizeB.AutoSize = True
            Me.lblSizeB.Location = New System.Drawing.Point(89, 137)
            Me.lblSizeB.Name = "lblSizeB"
            Me.lblSizeB.Size = New System.Drawing.Size(34, 13)
            Me.lblSizeB.TabIndex = 4
            Me.lblSizeB.Text = "size b"
            '
            'cmdYes
            '
            Me.cmdYes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdYes.Location = New System.Drawing.Point(30, 186)
            Me.cmdYes.Name = "cmdYes"
            Me.cmdYes.Size = New System.Drawing.Size(75, 23)
            Me.cmdYes.TabIndex = 5
            Me.cmdYes.Text = "&Yes"
            Me.cmdYes.UseVisualStyleBackColor = True
            '
            'cmdYesToAll
            '
            Me.cmdYesToAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdYesToAll.Location = New System.Drawing.Point(111, 186)
            Me.cmdYesToAll.Name = "cmdYesToAll"
            Me.cmdYesToAll.Size = New System.Drawing.Size(75, 23)
            Me.cmdYesToAll.TabIndex = 6
            Me.cmdYesToAll.Text = "Yes to &all"
            Me.cmdYesToAll.UseVisualStyleBackColor = True
            '
            'cmdNo
            '
            Me.cmdNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdNo.Location = New System.Drawing.Point(192, 186)
            Me.cmdNo.Name = "cmdNo"
            Me.cmdNo.Size = New System.Drawing.Size(75, 23)
            Me.cmdNo.TabIndex = 7
            Me.cmdNo.Text = "&No"
            Me.cmdNo.UseVisualStyleBackColor = True
            '
            'cmdCancel
            '
            Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdCancel.Location = New System.Drawing.Point(273, 186)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
            Me.cmdCancel.TabIndex = 8
            Me.cmdCancel.Text = "&Cancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'iconA
            '
            Me.iconA.Location = New System.Drawing.Point(45, 71)
            Me.iconA.Name = "iconA"
            Me.iconA.Size = New System.Drawing.Size(32, 32)
            Me.iconA.TabIndex = 9
            Me.iconA.TabStop = False
            '
            'iconB
            '
            Me.iconB.Location = New System.Drawing.Point(45, 137)
            Me.iconB.Name = "iconB"
            Me.iconB.Size = New System.Drawing.Size(32, 32)
            Me.iconB.TabIndex = 9
            Me.iconB.TabStop = False
            '
            'FileSystemOperationUIQuestion
            '
            Me.AcceptButton = Me.cmdYes
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(360, 221)
            Me.Controls.Add(Me.iconB)
            Me.Controls.Add(Me.iconA)
            Me.Controls.Add(Me.cmdCancel)
            Me.Controls.Add(Me.cmdNo)
            Me.Controls.Add(Me.cmdYesToAll)
            Me.Controls.Add(Me.cmdYes)
            Me.Controls.Add(Me.lblSizeB)
            Me.Controls.Add(Me.lblDateB)
            Me.Controls.Add(Me.lblSizeA)
            Me.Controls.Add(Me.lblDateA)
            Me.Controls.Add(Me.lblText2)
            Me.Controls.Add(Me.lblText1)
            Me.Controls.Add(Me.lblTitle)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FileSystemOperationUIQuestion"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "Confirm file overwrite"
            CType(Me.iconA, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.iconB, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblTitle As System.Windows.Forms.Label
        Friend WithEvents lblText1 As System.Windows.Forms.Label
        Friend WithEvents lblText2 As System.Windows.Forms.Label
        Friend WithEvents lblDateA As System.Windows.Forms.Label
        Friend WithEvents lblSizeA As System.Windows.Forms.Label
        Friend WithEvents lblDateB As System.Windows.Forms.Label
        Friend WithEvents lblSizeB As System.Windows.Forms.Label
        Friend WithEvents cmdYes As System.Windows.Forms.Button
        Friend WithEvents cmdYesToAll As System.Windows.Forms.Button
        Friend WithEvents cmdNo As System.Windows.Forms.Button
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents iconA As System.Windows.Forms.PictureBox
        Friend WithEvents iconB As System.Windows.Forms.PictureBox
#End Region

    End Class
End Namespace
#End If
