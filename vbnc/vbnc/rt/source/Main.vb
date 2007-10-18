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

Option Explicit On
Option Strict On

Imports System
Imports System.Diagnostics
Imports System.io
Imports Microsoft.VisualBasic
Imports System.Collections

Module MainModule

    Public CommandLine As New CommandLine

    Public frmMain As frmMain

	''' <summary>
	''' The main function...
	''' </summary>
	''' <param name="cmdArgs"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Function Main(ByVal cmdArgs() As String) As Integer
        Try
            Application.EnableVisualStyles()

            Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.BelowNormal

            CommandLine.Parse(cmdArgs)
            If CommandLine.Spawn Then Return 0

            'Try to upgrade the settings
            If My.Settings.IsFirstRun Then
                My.Settings.Upgrade()
                My.Settings.IsFirstRun = False
                Debug.WriteLine("Settings have been upgraded.")
            End If

            DisableErrorReporting()

            frmMain = New frmMain
            Using frmMain
                If CommandLine.ExitOnSuccess Then
                    frmMain.WindowState = FormWindowState.Maximized
                    frmMain.WindowState = FormWindowState.Minimized
                    frmMain.Visible = True
                    frmMain.RunTests()
                    While frmMain.TestExecutor.QueueCount > 0
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While

                    If frmMain.Tests.TestsFailed = 0 Then
                        Return 0
                    Else
                        frmMain.Hide()
                        frmMain.WindowState = FormWindowState.Maximized
                        frmMain.Text = "FAILED " & frmMain.Text
                    End If
                End If
                If Not (frmMain.Disposing OrElse frmMain.IsDisposed) Then frmMain.ShowDialog()
            End Using

        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.GetType.ToString & vbNewLine & ex.StackTrace)
        Finally
            EnableErrorReporting()
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Filename"></param>
    ''' <param name="newExt">Don't include a dot.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeExtension(ByVal Filename As String, ByVal newExt As String) As String
        Return IO.Path.Combine(IO.Path.GetDirectoryName(Filename), IO.Path.GetFileNameWithoutExtension(Filename)) & "." & newExt
    End Function

	Function ChangeOutputToVerified(ByVal Test As Test, ByVal Overwrite As Boolean, Optional ByVal Copy As Boolean = False) As Integer
		Dim result As Integer
		Try
			Dim xmlfiles As String()
			xmlfiles = Test.GetOutputFiles

			For Each xmlfile As String In xmlfiles
				Dim verified As String
				verified = xmlfile.Replace(Test.OutputExtension, Test.VerifiedExtension)
				If verified.Contains(".exceptions.") = False Then
					If IO.File.Exists(verified) Then
						If Overwrite Then
							IO.File.Delete(verified)
							If Copy Then
								IO.File.Copy(xmlfile, verified)
							Else
								IO.File.Move(xmlfile, verified)
							End If
							result += 1
						End If
					Else
						If Copy Then
							IO.File.Copy(xmlfile, verified)
						Else
							IO.File.Move(xmlfile, verified)
						End If
						result += 1
					End If
				End If
			Next
		Catch ex As Exception
			MsgBox("Error while changing output xml files to verified xml files: " & ex.Message)
        End Try


		Return result
	End Function

	Sub ChangeOutputToVerified(ByVal Directory As String, ByVal Overwrite As Boolean, ByVal Recursive As Boolean)
		Try
			Dim xmlfiles As String()
			xmlfiles = IO.Directory.GetFiles(Directory, "*.*.output.xml")
			For Each xmlfile As String In xmlfiles
				Dim verified As String
				verified = xmlfile.Replace(".output.xml", ".verified.xml")
				If verified.Contains(".exceptions.") = False Then
					If IO.File.Exists(verified) Then
						If Overwrite Then
							IO.File.Delete(verified)
							IO.File.Move(xmlfile, verified)
						End If
					Else
						IO.File.Move(xmlfile, verified)
					End If
				End If
			Next
			If Recursive Then
				Dim dirs() As String
				dirs = IO.Directory.GetDirectories(Directory)
				For Each dir As String In dirs
					ChangeOutputToVerified(dir, Overwrite, False)
				Next
			End If
		Catch ex As Exception
			MsgBox("Error while changing output xml files to verified xml files: " & ex.Message)
		End Try
	End Sub


    Sub ViewFiles(ByVal ParamArray Filenames As String())
        For Each file As String In Filenames
            Process.Start(file)
        Next
    End Sub

	<Conditional("DEBUG")> Sub StopIfDebugging()
		If Diagnostics.Debugger.IsAttached Then
			Stop
		End If
    End Sub

    Private Sub EnableErrorReporting()
        Try
            CheckForRegistryConformance()
            If My.Settings.ModifyRegistry = "Y" Then
                If Environment.OSVersion.Platform <> PlatformID.Unix Then
                    Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PCHealth\ErrorReporting", "ShowUI", 1, Microsoft.Win32.RegistryValueKind.DWord)
                End If
            End If
        Catch ex As Exception
            Global.System.Diagnostics.Debug.WriteLine("Could not enable error reporting: " & ex.Message)
        End Try
    End Sub

    Private Sub DisableErrorReporting()
        Try
            CheckForRegistryConformance()
            If My.Settings.ModifyRegistry = "Y" Then
                If Environment.OSVersion.Platform <> PlatformID.Unix Then
                    Microsoft.Win32.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PCHealth\ErrorReporting", "ShowUI", 0, Microsoft.Win32.RegistryValueKind.DWord)
                End If
            End If
        Catch ex As Exception
            Global.System.Diagnostics.Debug.WriteLine("Could not disable error reporting: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckForRegistryConformance()
        If My.Settings.ModifyRegistry = "" Then
            Dim result As MsgBoxResult
            result = MsgBox("This application will modify registry values for the entire machine, OK?" & vbNewLine & "(Check source for exact keys and values)", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question)
            If result = MsgBoxResult.Yes Then
                My.Settings.ModifyRegistry = "Y"
            ElseIf result = MsgBoxResult.No Then
                My.Settings.ModifyRegistry = "N"
            End If
        End If
    End Sub
End Module
