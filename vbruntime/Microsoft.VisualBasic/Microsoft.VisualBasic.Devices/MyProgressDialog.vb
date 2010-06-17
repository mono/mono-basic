'
' MyProgressDialog.vb
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

Imports System.Net

Namespace Microsoft.VisualBasic.Devices
    Friend Class MyProgressDialog
        Inherits System.Windows.Forms.Form

        Private WithEvents m_Client As Net.WebClient

        Sub New(ByVal Client As Net.WebClient, ByVal Status As String)
            m_Client = Client
            lblStatus.Text = Status
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents barProgress As System.Windows.Forms.ProgressBar
        Friend WithEvents lblStatus As System.Windows.Forms.Label

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.barProgress = New System.Windows.Forms.ProgressBar
            Me.lblStatus = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'cmdCancel
            '
            Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(290, 118)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
            Me.cmdCancel.TabIndex = 0
            Me.cmdCancel.Text = "Button1"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'barProgress
            '
            Me.barProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.barProgress.Location = New System.Drawing.Point(12, 92)
            Me.barProgress.Name = "barProgress"
            Me.barProgress.Size = New System.Drawing.Size(353, 20)
            Me.barProgress.TabIndex = 1
            '
            'lblStatus
            '
            Me.lblStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblStatus.Location = New System.Drawing.Point(12, 9)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(353, 80)
            Me.lblStatus.TabIndex = 2
            Me.lblStatus.Text = "Label1"
            '
            'NetworkProgressDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(377, 153)
            Me.Controls.Add(Me.lblStatus)
            Me.Controls.Add(Me.barProgress)
            Me.Controls.Add(Me.cmdCancel)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "NetworkProgressDialog"
            Me.ShowInTaskbar = False
            Me.Text = "Working..."
            Me.ResumeLayout(False)

        End Sub

        Private Sub m_Client_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles m_Client.DownloadFileCompleted
            barProgress.Value = 100
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End Sub

        Private Sub m_Client_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles m_Client.DownloadProgressChanged
            barProgress.Value = e.ProgressPercentage
        End Sub

        Private Sub m_Client_UploadFileCompleted(ByVal sender As Object, ByVal e As System.Net.UploadFileCompletedEventArgs) Handles m_Client.UploadFileCompleted
            barProgress.Value = 100
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End Sub

        Private Sub m_Client_UploadProgressChanged(ByVal sender As Object, ByVal e As System.Net.UploadProgressChangedEventArgs) Handles m_Client.UploadProgressChanged
            barProgress.Value = e.ProgressPercentage
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            m_Client.CancelAsync()
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Close()
        End Sub
    End Class
End Namespace
#End If
