' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Class FileTabPage
    Inherits TabPage

    Private m_Test As Test
    Private m_FileName As String

    ReadOnly Property FileName() As String
        Get
            Return m_FileName
        End Get
    End Property

    Sub New(ByVal Test As Test, ByVal Filename As String)
        MyBase.New(IO.Path.GetFileName(Filename))

        Me.InitializeComponent()

        m_FileName = IO.Path.Combine(Test.FullWorkingDirectory, Filename)
        Try
            If IO.File.Exists(m_FileName) Then
                txtFile.Text = Join(IO.File.ReadAllText(m_FileName).Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.None), vbCrLf)
            Else
                txtFile.Text = "File not found: " & m_FileName
            End If
        Catch ex As Security.SecurityException
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        Catch ex As IO.IOException
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

End Class
