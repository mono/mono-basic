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

Class CommandLine

	Private m_Compiler As String
	Private m_TestPath As String
	Private m_ExitOnSuccess As Boolean
	Private m_Spawn As Boolean
	Sub New()
		m_TestPath = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
		If IO.Directory.Exists(IO.Path.Combine(m_TestPath, "Tests")) Then
			m_TestPath = IO.Path.Combine(m_TestPath, "Tests")
		End If

		Dim tmp As String
		tmp = "%SYSTEMROOT%\Microsoft.NET\Framework\"
		tmp = Environment.ExpandEnvironmentVariables(tmp)

		Dim dirs As String() = IO.Directory.GetDirectories(tmp)
		For Each dir As String In dirs
			Dim file As String
			file = IO.Path.Combine(dir, "vbc.exe")
			If IO.File.Exists(file) Then
				m_Compiler = file
			End If
		Next
	End Sub

	ReadOnly Property Compiler() As String
		Get
			Return m_Compiler
		End Get
	End Property
	ReadOnly Property TestPath() As String
		Get
			Return m_TestPath
		End Get
	End Property
	ReadOnly Property ExitOnSuccess() As Boolean
		Get
			Return m_ExitOnSuccess
		End Get
	End Property
	ReadOnly Property Spawn() As Boolean
		Get
			Return m_Spawn
		End Get
	End Property
	''' <summary>
	''' Parses the commandline
	''' </summary>
	''' <param name="cmdArgs"></param>
	''' <remarks></remarks>
	Sub Parse(ByVal cmdArgs() As String)
		For i As Integer = 0 To UBound(cmdArgs)
			Dim str As String = cmdArgs(i)
			Dim cmd, value As String
			If str.StartsWith("""") AndAlso str.EndsWith("""") Then
				str = Mid(str, 2, str.Length - 2)
			End If
			If str.StartsWith("/") OrElse str.StartsWith("-") Then
				Dim iSep As Integer = str.IndexOf(":")
				If iSep = -1 Then
					cmd = str.Substring(1)
					value = ""
				Else
					cmd = str.Substring(1, iSep - 1)
					value = str.Substring(iSep + 1)
				End If

				Select Case cmd.ToLower
					Case "auto", "auto+"
						m_ExitOnSuccess = value = "" OrElse value = "+"
					Case "path", "p"
						m_TestPath = IO.Path.GetFullPath(value)
					Case "compiler", "c"
						m_Compiler = IO.Path.GetFullPath(value)
					Case "spawn"
						'Spawn another process and exit this process
						cmdArgs(i) = ""	'Zero out the spawn argument, otherwise it would be an interesting case of recursion!
						Dim cmdline As String
						'Quote all the arguments
						For j As Integer = 0 To UBound(cmdArgs)
							If cmdArgs(j) <> "" Then
								cmdArgs(j) = """" & cmdArgs(j) & """"
							End If
						Next
						cmdline = Join(cmdArgs, " ")

						Process.Start(System.Reflection.Assembly.GetExecutingAssembly.Location, cmdline)

						m_Spawn = True
					Case Else
						MsgBox("Unrecognized commandline option: " & str)
				End Select
			ElseIf str <> "" Then
				MsgBox("Unrecognized commandline option: " & str)
			End If
		Next
	End Sub
End Class
