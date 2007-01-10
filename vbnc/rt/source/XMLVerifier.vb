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

Friend Class XMLVerifier
    Inherits VerificationBase

    Sub New(ByVal Test As Test)
        MyBase.New(Test)
        MyBase.Name = "XML verification"
        'delete the old output xml files.
        For Each xmlfile As String In Test.GetOutputFiles
            IO.File.Delete(xmlfile)
        Next
    End Sub

    Protected Overrides Function RunVerification() As Boolean
        Dim m_result As Boolean = True
        Dim m_Errors As New Generic.List(Of String)

        'Test the xml output
        Dim xmlfilesVerified As New Hashtable(System.StringComparer.InvariantCultureIgnoreCase)
        Dim xmlfilesOutput As New Hashtable(System.StringComparer.InvariantCultureIgnoreCase)
        Dim filesVerified As String()
        Dim filesOutput As String()

        If IO.Directory.Exists(Test.OutputPath) = False Then
            IO.Directory.CreateDirectory(Test.OutputPath)
        End If

        filesVerified = IO.Directory.GetFiles(Test.OutputPath, Test.Name & Test.VerifiedPattern)
        filesOutput = IO.Directory.GetFiles(Test.OutputPath, Test.Name & Test.OutputPattern)

        If filesVerified.Length = 0 AndAlso filesOutput.Length > 0 Then
            'If there is no verified files, set the current output as verified.
            MainModule.ChangeOutputToVerified(Test, False, True)
            filesVerified = IO.Directory.GetFiles(Test.OutputPath, Test.Name & Test.VerifiedPattern)
        End If

        For Each filename As String In filesVerified
            xmlfilesVerified.Add(filename, filename)
        Next
        For Each filename As String In filesOutput
            xmlfilesOutput.Add(filename, filename)
        Next

        Dim strMsg As String
        For Each xmlfileVerified As String In xmlfilesVerified.Values
            Dim xmlfileOutput As String
            xmlfileOutput = xmlfileVerified.Replace(".verified.", ".output.")
            If xmlfilesOutput.ContainsValue(xmlfileOutput) Then
                xmlfilesOutput.Remove(xmlfileOutput)
                Dim diff As XmlCompare
                diff = XmlCompare.Compare(xmlfileVerified, xmlfileOutput)
                If diff.Equal = False Then
                    m_result = False ' Results.VerificationFailed
                    strMsg = String.Format("Error comparing xml files: " & vbNewLine & "    {0}" & vbNewLine & "    {1}" & vbNewLine, xmlfileOutput, xmlfileVerified)
                    'm_XmlDiffs.Add(diff)
                    m_Errors.Add(strMsg)
                End If
            Else
                m_result = False ' Results.VerificationFailed
                strMsg = "Xml result not found:"
                strMsg &= vbNewLine & "    " & xmlfileOutput
                strMsg &= vbNewLine & "corresponding to verified xml result:"
                strMsg &= vbNewLine & "    " & xmlfileVerified
                m_Errors.Add(strMsg)
            End If
        Next

        For Each xmlfile As String In xmlfilesOutput.Values
            m_result = False ' Results.VerificationFailed
            strMsg = "Verified xml result not found for output file: " & xmlfile
            m_Errors.Add(strMsg)
        Next

        If m_Errors.Count > 0 Then
            MyBase.DescriptiveMessage = Join(m_Errors.ToArray, vbNewLine & vbNewLine)
        Else
            MyBase.DescriptiveMessage = Name & " succeeded." & vbNewLine
        End If

        Return m_result
    End Function

    Private Sub DoVerification()
     
    End Sub
End Class
