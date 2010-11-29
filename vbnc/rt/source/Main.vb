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

Imports System
Imports System.Diagnostics
Imports System.io
Imports Microsoft.VisualBasic
Imports System.Collections

Module MainModule
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

            DisableErrorReporting()

            For Each str As String In cmdArgs
                If str = "/console" OrElse str = "-console" OrElse str = "--console" Then
                    Return ConsoleRunner.Run(cmdArgs)
                End If
            Next

            frmMain = New frmMain
            Application.Run(frmMain)
        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.GetType.ToString & vbNewLine & ex.StackTrace)
        Finally
            EnableErrorReporting()
        End Try
    End Function

    Sub ViewFiles(ByVal ParamArray Filenames As String())
        For Each file As String In Filenames
            If Not IO.File.Exists(file) Then
                Dim result As MsgBoxResult = MsgBox(String.Format("The file '{0}' does not exist. Do you want to create it?", file), MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, "File does not exist")
                If result = MsgBoxResult.Yes Then
                    IO.File.WriteAllText(file, String.Empty)
                ElseIf result = MsgBoxResult.Cancel Then
                    Return
                Else
                    Continue For
                End If
            End If
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
