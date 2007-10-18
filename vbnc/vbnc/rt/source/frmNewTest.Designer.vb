<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewTest
    Inherits System.Windows.Forms.Form

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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTestName = New System.Windows.Forms.TextBox
        Me.chkCreateCode = New System.Windows.Forms.CheckBox
        Me.chkCreateMain = New System.Windows.Forms.CheckBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdSaveAndEdit = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.lblActualName = New System.Windows.Forms.Label
        Me.lblPath = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(300, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name (no numbers needed):"
        '
        'txtTestName
        '
        Me.txtTestName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTestName.Location = New System.Drawing.Point(15, 46)
        Me.txtTestName.Name = "txtTestName"
        Me.txtTestName.Size = New System.Drawing.Size(717, 21)
        Me.txtTestName.TabIndex = 1
        '
        'chkCreateCode
        '
        Me.chkCreateCode.AutoSize = True
        Me.chkCreateCode.Checked = True
        Me.chkCreateCode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCreateCode.Location = New System.Drawing.Point(12, 72)
        Me.chkCreateCode.Name = "chkCreateCode"
        Me.chkCreateCode.Size = New System.Drawing.Size(85, 17)
        Me.chkCreateCode.TabIndex = 2
        Me.chkCreateCode.Text = "Create code"
        Me.chkCreateCode.UseVisualStyleBackColor = True
        '
        'chkCreateMain
        '
        Me.chkCreateMain.AutoSize = True
        Me.chkCreateMain.Checked = True
        Me.chkCreateMain.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCreateMain.Location = New System.Drawing.Point(158, 72)
        Me.chkCreateMain.Name = "chkCreateMain"
        Me.chkCreateMain.Size = New System.Drawing.Size(126, 17)
        Me.chkCreateMain.TabIndex = 3
        Me.chkCreateMain.Text = "Create Main function"
        Me.chkCreateMain.UseVisualStyleBackColor = True
        '
        'txtCode
        '
        Me.txtCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(12, 131)
        Me.txtCode.Multiline = True
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(720, 328)
        Me.txtCode.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(300, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Preview:"
        '
        'cmdSaveAndEdit
        '
        Me.cmdSaveAndEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSaveAndEdit.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdSaveAndEdit.Location = New System.Drawing.Point(581, 465)
        Me.cmdSaveAndEdit.Name = "cmdSaveAndEdit"
        Me.cmdSaveAndEdit.Size = New System.Drawing.Size(151, 32)
        Me.cmdSaveAndEdit.TabIndex = 7
        Me.cmdSaveAndEdit.Text = "Save and Edit"
        Me.cmdSaveAndEdit.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Location = New System.Drawing.Point(414, 465)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(151, 32)
        Me.cmdClose.TabIndex = 6
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'lblActualName
        '
        Me.lblActualName.Location = New System.Drawing.Point(353, 73)
        Me.lblActualName.Name = "lblActualName"
        Me.lblActualName.Size = New System.Drawing.Size(379, 17)
        Me.lblActualName.TabIndex = 8
        Me.lblActualName.Text = "Actual filename:"
        '
        'lblPath
        '
        Me.lblPath.Location = New System.Drawing.Point(353, 90)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(379, 17)
        Me.lblPath.TabIndex = 9
        Me.lblPath.Text = "Path:"
        '
        'frmNewTest
        '
        Me.AcceptButton = Me.cmdSaveAndEdit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(748, 510)
        Me.Controls.Add(Me.lblPath)
        Me.Controls.Add(Me.lblActualName)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdSaveAndEdit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.chkCreateMain)
        Me.Controls.Add(Me.chkCreateCode)
        Me.Controls.Add(Me.txtTestName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmNewTest"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTestName As System.Windows.Forms.TextBox
    Friend WithEvents chkCreateCode As System.Windows.Forms.CheckBox
    Friend WithEvents chkCreateMain As System.Windows.Forms.CheckBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSaveAndEdit As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lblActualName As System.Windows.Forms.Label
    Friend WithEvents lblPath As System.Windows.Forms.Label
End Class
