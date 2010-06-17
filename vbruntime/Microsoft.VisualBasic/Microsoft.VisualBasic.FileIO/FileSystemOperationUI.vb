'
' FileSystemOperationUI.vb
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

    Friend Class FileSystemOperationUI
        Inherits System.Windows.Forms.Form

        Private m_LastUpdate As Date = Date.MinValue
        Private m_Start As Date = Date.MinValue
        Private m_Started As Boolean
        Private m_Operation As FileSystemOperation

        Private m_SourceDirectory As String
        Private m_DestinationDirectory As String
        Private m_File As String

        Sub New(ByVal Operation As FileSystemOperation)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            m_Operation = Operation
        End Sub

        Public Sub UpdateInfo(ByVal PercentDone As Double)
            UpdateInfo(m_SourceDirectory, m_DestinationDirectory, m_File, PercentDone)
        End Sub

        Private Sub UpdateInfoInternal(ByVal PercentDone As Double)
            If PercentDone < 0.1 OrElse PercentDone > 99.9 Then
                lblTimeLeft.Text = "..."
            Else
                Dim estimatedTime As Double
                estimatedTime = (((Date.Now - m_Start).TotalSeconds / PercentDone) * 100)
                lblTimeLeft.Text = (estimatedTime - (Date.Now - m_Start).TotalSeconds).ToString("0") & " seconds"
            End If
            barProgress.Value = CInt(PercentDone)
        End Sub

        Public Sub UpdateInfo(ByVal SourceDirectory As String, ByVal DestinationDirectory As String, ByVal File As String, ByVal PercentDone As Double)
            If m_Started = False Then
                m_Start = Date.Now
                m_Started = True
            End If

            m_SourceDirectory = SourceDirectory
            m_DestinationDirectory = DestinationDirectory
            m_File = File

            If (Date.Now - m_LastUpdate).TotalSeconds < 1 Then Return

            If Not DestinationDirectory Is Nothing AndAlso DestinationDirectory.Length <> 0 Then
                lblDirs.Text = String.Format("From '{0}' to '{1}'", Path.GetFileName(SourceDirectory), Path.GetFileName(DestinationDirectory))
                lblFile.Text = File
            Else
                'lblDirs.Text = SourceDirectory
                lblFile.Text = SourceDirectory
            End If
            UpdateInfoInternal(PercentDone)

            m_LastUpdate = Date.Now
            System.Windows.Forms.Application.DoEvents()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            m_Operation.Cancel()
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
            Me.barProgress = New System.Windows.Forms.ProgressBar
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.lblFile = New System.Windows.Forms.Label
            Me.lblDirs = New System.Windows.Forms.Label
            Me.lblTimeLeft = New System.Windows.Forms.Label
            Me.SuspendLayout()
            '
            'barProgress
            '
            Me.barProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.barProgress.Location = New System.Drawing.Point(12, 66)
            Me.barProgress.Name = "barProgress"
            Me.barProgress.Size = New System.Drawing.Size(307, 23)
            Me.barProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            Me.barProgress.TabIndex = 0
            '
            'cmdCancel
            '
            Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(244, 97)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
            Me.cmdCancel.TabIndex = 1
            Me.cmdCancel.Text = "&Cancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'lblFile
            '
            Me.lblFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFile.Location = New System.Drawing.Point(13, 44)
            Me.lblFile.Name = "lblFile"
            Me.lblFile.Size = New System.Drawing.Size(307, 13)
            Me.lblFile.TabIndex = 2
            Me.lblFile.Text = "File"
            '
            'lblDirs
            '
            Me.lblDirs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblDirs.Location = New System.Drawing.Point(13, 24)
            Me.lblDirs.Name = "lblDirs"
            Me.lblDirs.Size = New System.Drawing.Size(307, 13)
            Me.lblDirs.TabIndex = 3
            Me.lblDirs.Text = "Dirs"
            '
            'lblTimeLeft
            '
            Me.lblTimeLeft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblTimeLeft.Location = New System.Drawing.Point(9, 102)
            Me.lblTimeLeft.Name = "lblTimeLeft"
            Me.lblTimeLeft.Size = New System.Drawing.Size(226, 13)
            Me.lblTimeLeft.TabIndex = 3
            Me.lblTimeLeft.Text = "TimeLeft"
            '
            'FileSystemOperationUI
            '
            Me.AcceptButton = Me.cmdCancel
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(331, 132)
            Me.Controls.Add(Me.lblTimeLeft)
            Me.Controls.Add(Me.lblDirs)
            Me.Controls.Add(Me.lblFile)
            Me.Controls.Add(Me.cmdCancel)
            Me.Controls.Add(Me.barProgress)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FileSystemOperationUI"
#If mono_not_yet Then
            Me.ShowIcon = False
#End If
            Me.ShowInTaskbar = False
            Me.Text = "Operation"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents barProgress As System.Windows.Forms.ProgressBar
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents lblFile As System.Windows.Forms.Label
        Friend WithEvents lblDirs As System.Windows.Forms.Label
        Friend WithEvents lblTimeLeft As System.Windows.Forms.Label
#End Region

    End Class
End Namespace
#End If
