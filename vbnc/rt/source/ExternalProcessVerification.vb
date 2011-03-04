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

Public Class ExternalProcessVerification
    Inherits VerificationBase

    Private m_Process As ExternalProcessExecutor

    ReadOnly Property Process() As ExternalProcessExecutor
        Get
            Return m_Process
        End Get
    End Property

    ''' <summary>
    ''' Creates a new verifier using an external process.
    ''' </summary>
    ''' <param name="Test"></param>
    ''' <param name="Executable">The program to use.</param>
    ''' <param name="ExpandableCommandLine">The parameters to the program. 
    ''' Strings that are expanded are: %OUTPUTASSEMBLY% and %OUTPUTVBCASSEMBLY%.
    ''' Environment variables are also expanded.</param>
    ''' <remarks></remarks>
    Sub New(ByVal Test As Test, ByVal Executable As String, Optional ByVal ExpandableCommandLine As String = "%OUTPUTASSEMBLY%")
        MyBase.New(Test)
        If String.IsNullOrEmpty(ExpandableCommandLine) Then ExpandableCommandLine = "%OUTPUTASSEMBLY%"
        m_Process = New ExternalProcessExecutor(Executable, ExpandableCommandLine)
        m_Process.ExpandCmdLine(New String() {"%OUTPUTASSEMBLY%", "%OUTPUTVBCASSEMBLY%"}, New String() {Test.OutputAssembly(), Test.OutputVBCAssembly()})
    End Sub

    Protected Overrides Function RunVerification() As Boolean
        Dim result As Boolean

        result = m_Process.RunProcess()

        If result = False Then
            MyBase.DescriptiveMessage = Name & " failed: " & vbNewLine & m_Process.StdOut
            Return False
        End If

        If m_Process.TimedOut = False Then
            If m_Process.ExitCode <> Me.ExpectedExitCode Then
                MyBase.DescriptiveMessage = Name & " failed, expected exit code " & Me.ExpectedExitCode & " but process exited with exit code " & m_Process.ExitCode & vbNewLine
                result = False
            Else
                Dim actualErrors As New Generic.List(Of ErrorInfo)
                Dim errorReport As String
                Dim ei As ErrorInfo
                Dim line As String
                Dim stdout As String = m_Process.StdOut

                Using reader As New System.IO.StringReader(stdout)
                    line = reader.ReadLine
                    Do While line IsNot Nothing
                        If line.Contains("<MyGenerator>") Then
                            MyBase.DescriptiveMessage = Name & " failed, <MyGenerator> shown in error message" & vbNewLine
                            result = False
                        ElseIf line.Contains("VBNC9999") AndAlso line.Contains("VBNC99998") = False Then
                            MyBase.DescriptiveMessage = Name & " failed, VBNC9999? shown in error message" & vbNewLine
                            result = False
                        ElseIf line.Contains("CHANGEME") Then
                            MyBase.DescriptiveMessage = Name & " failed, CHANGEME shown in error message" & vbNewLine
                            result = False
                        Else
                            ei = ErrorInfo.ParseLine(line)
                            If ei IsNot Nothing Then actualErrors.Add(ei)
                        End If

                        line = reader.ReadLine
                    Loop
                End Using

                If result Then
                    If (ExpectedErrors Is Nothing OrElse ExpectedErrors.Count = 0) AndAlso actualErrors.Count > 0 Then
                        MyBase.DescriptiveMessage = String.Format("{0} failed, expected 0 errors, got {1} errors{2}", Name, actualErrors.Count, vbNewLine)
                        result = False
                    ElseIf ExpectedErrors IsNot Nothing AndAlso ExpectedErrors.Count <> actualErrors.Count Then
                        MyBase.DescriptiveMessage = String.Format("{0} failed, expected {1} errors, got {2} errors{3}", Name, ExpectedErrors.Count, actualErrors.Count, vbNewLine)
                        result = False
                    ElseIf ExpectedErrors IsNot Nothing Then
                        errorReport = String.Empty
                        For i As Integer = 0 To ExpectedErrors.Count - 1
                            Dim msg As String = Nothing
                            If ErrorInfo.Compare(ExpectedErrors(i), actualErrors(i), msg) = False Then
                                errorReport += String.Format("Error #{0}: {1}", i + 1, msg)
                            End If
                        Next
                        If errorReport <> String.Empty Then
                            MyBase.DescriptiveMessage = String.Format("{0} failed error verification: {2}", Name, vbNewLine, errorReport)
                            result = False
                        End If
                    End If
                End If
            End If

            If result Then
                MyBase.DescriptiveMessage = Name & " succeeded."
            End If
        Else
            result = False
            MyBase.DescriptiveMessage = Name & " failed, process was killed due to a time-out (" & m_Process.TimeOut.ToString & " ms)." & vbNewLine
        End If

        MyBase.DescriptiveMessage &= vbNewLine & m_Process.StdOut

        If m_Process.Executable.Contains("PEVerify") Then
            Debug.WriteLine(MyBase.DescriptiveMessage)
            If MyBase.DescriptiveMessage.ToLowerInvariant.Contains("warning") Then
                'Stop
            End If
        End If

        Return result
    End Function
End Class

