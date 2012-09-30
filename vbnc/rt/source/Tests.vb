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

Imports System.IO

''' <summary>
''' A list of tests.
''' </summary>
''' <remarks></remarks>
Public Class Tests
    Inherits Generic.Dictionary(Of String, Test)

    Private m_Filename As String
    Private m_ResultsFilename As String
    Private m_Document As XmlDocument
    Private m_VBCPath As String
    Private m_VBNCPath As String

    Public Sub Append(ByVal Test As Test)
        For i As Integer = 0 To Integer.MaxValue
            If ContainsKey(i.ToString()) = False Then
                Test.ID = i.ToString()
                Add(Test)
                Return
            End If
        Next
    End Sub

    Public Shadows Sub Add(ByVal Test As Test)
        MyBase.Add(Test.ID, Test)
    End Sub

    Public Sub Load(ByVal Filename As String)
        m_Filename = Filename
        m_ResultsFilename = Path.Combine(Path.GetDirectoryName(m_Filename), Path.GetFileNameWithoutExtension(m_Filename) + ".results" + Path.GetExtension(m_Filename))
        m_Document = New XmlDocument()
        m_Document.Load(Filename)
        For Each node As XmlNode In m_Document.SelectNodes("/rt/test")
            Dim test As New Test(Me, node)
            If Me.ContainsKey(test.ID) Then
                Debug.WriteLine(String.Format("Test list already contains the test with id {0}", test.ID))
            Else
                Add(test)
            End If
        Next
        If File.Exists(m_ResultsFilename) Then
            m_Document = New XmlDocument()
            m_Document.Load(m_ResultsFilename)
            For Each node As XmlNode In m_Document.SelectNodes("/rt/test")
                Dim id As String = node.Attributes("id").Value
                Dim test As Test = Nothing

                If Me.TryGetValue(id, test) Then
                    test.LoadResult(node)
                End If
            Next
        End If
    End Sub

    Private Shared Sub CreateBackup(ByVal fn As String)
        Dim path As String
        Dim name As String
        Dim ext As String
        Dim counter As Integer
        Dim filename As String

        If Not File.Exists(fn) Then Return

        counter = 0
        path = IO.Path.GetDirectoryName(fn)
        name = IO.Path.GetFileNameWithoutExtension(fn)
        ext = IO.Path.GetExtension(fn)
        Do
            counter += 1
            filename = IO.Path.Combine(path, name & "." & counter.ToString() & ext)
        Loop While IO.File.Exists(filename)
        IO.File.Copy(fn, filename, False)
    End Sub

    Private Sub CreateBackup()
        CreateBackup(m_Filename)
        CreateBackup(m_ResultsFilename)
    End Sub

    Public Sub Save()
        CreateBackup()
        Save(False)
        Save(True)
    End Sub

    Public Sub Save(ByVal results As Boolean)
        Dim settings As New XmlWriterSettings
        Dim writer As XmlWriter

        settings.CloseOutput = False
        settings.ConformanceLevel = ConformanceLevel.Document
        settings.Indent = True
        settings.IndentChars = vbTab
        settings.NewLineHandling = NewLineHandling.None
        settings.NewLineChars = vbLf
        settings.NewLineOnAttributes = False
        settings.OmitXmlDeclaration = False

        Using fs As New IO.StreamWriter(If(results, m_ResultsFilename, m_Filename), False, New System.Text.UTF8Encoding(False))
            fs.NewLine = settings.NewLineChars
            writer = XmlWriter.Create(fs, settings)

            writer.WriteStartElement("rt")
            For Each Test As Test In Me.Values
                Test.Save(writer, results)
            Next
            writer.WriteEndElement()
            writer.Close()
            fs.WriteLine()
        End Using
    End Sub

    ReadOnly Property GetGreenCount() As Integer
        Get
            Return Me.GetTestsCount(Test.Results.Success, Test.Results.Success)
        End Get
    End Property

    ReadOnly Property GetRedCount() As Integer
        Get
            Return Me.GetTestsCount(Test.Results.Failed, Test.Results.Failed)
        End Get
    End Property

    ReadOnly Property Filename() As String
        Get
            Return m_Filename
        End Get
    End Property

    Property VBCPath() As String
        Get
            Return m_VBCPath
        End Get
        Set(ByVal value As String)
            m_VBCPath = value
        End Set
    End Property

    Property VBNCPath() As String
        Get
            Return m_VBNCPath
        End Get
        Set(ByVal value As String)
            m_VBNCPath = value
        End Set
    End Property

    ''' <summary>
    ''' The count of tests run
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsRun() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me.Values
                If test.Run Then result += 1
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The count of successful tests
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsSucceded() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me.Values
                If test.Run AndAlso test.Success = True Then result += 1
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The total time all tests have been executing.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property ExecutionTime() As TimeSpan
        Get
            Dim result As TimeSpan
            For Each test As Test In Me.Values
                If test.Run Then
                    result = result + test.TestDuration
                End If
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The count of failed tests
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsFailed() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me.Values
                If test.Run AndAlso test.Success = False Then result += 1
            Next
            Return result
        End Get
    End Property

    Sub GetTestsCount(ByVal result() As Integer)
        For Each t As Test In Me.Values
            result(t.Result) += 1
        Next
    End Sub

    Function GetTestsCount(ByVal MinResult As Test.Results, ByVal MaxResult As Test.Results) As Integer
        Dim result As Integer
        For Each t As Test In Me.Values
            If t.Result >= MinResult AndAlso t.Result <= MaxResult Then
                result += 1
            End If
        Next
        Return result
    End Function

    Function GetTestsMin(Optional ByVal MinResult As Test.Results = Test.Results.NotRun) As Tests
        If MinResult = Test.Results.NotRun Then Return Me
        Dim result As New Tests
        For Each test As Test In Me.Values
            If test.Result >= MinResult Then result.Add(test)
        Next
        Return result
    End Function

    Function GetTestsMax(Optional ByVal MaxResult As Test.Results = Test.Results.NotRun) As Tests
        Dim result As New Tests
        For Each test As Test In Me.Values
            If test.Result <= MaxResult Then result.Add(test)
        Next
        Return result
    End Function

    Function GetTestsRange(ByVal MinResult As Test.Results, ByVal MaxResult As Test.Results) As Tests
        Dim result As New Tests
        For Each test As Test In Me.Values
            If test.Result >= MinResult AndAlso test.Result <= MaxResult Then result.Add(test)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Get all tests that have failed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRedTests() As Tests
        Return GetTestsRange(Test.Results.Failed, Test.Results.Failed)
    End Function

    ''' <summary>
    ''' Get all tests that have reached at least level Results.Success
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGreenTests() As Tests
        Return GetTestsMin(Test.Results.Success)
    End Function

    Function GetNotRunTests() As Tests
        Return GetTestsMax(Test.Results.NotRun)
    End Function

    Function GetRunTests() As Tests
        Return GetTestsRange(Test.Results.Failed, Test.Results.Success)
    End Function
End Class
