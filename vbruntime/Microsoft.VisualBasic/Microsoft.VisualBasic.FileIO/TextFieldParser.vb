'
' TextFieldParser.vb
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
Imports System.ComponentModel
Imports System.IO
Imports System.Text

Namespace Microsoft.VisualBasic.FileIO
    Public Class TextFieldParser
        Implements IDisposable

        Private m_Reader As TextReader
        Private m_LeaveOpen As Boolean = False
        Private m_CommentTokens As String() = New String() {}
        Private m_Delimiters As String() = Nothing
        Private m_ErrorLine As String = String.Empty
        Private m_ErrorLineNumber As Long = -1
        Private m_FieldWidths As Integer() = Nothing
        Private m_HasFieldsEnclosedInQuotes As Boolean = True
        Private m_LineNumber As Long = -1
        Private m_TextFieldType As FieldType = FieldType.Delimited
        Private m_TrimWhiteSpace As Boolean = True

        Private m_PeekedLine As New Generic.Queue(Of String)
        Private m_MinFieldLength As Integer

#Region "Constructors"
        Public Sub New(ByVal stream As Stream)
            m_Reader = New StreamReader(stream)
        End Sub

        Public Sub New(ByVal reader As TextReader)
            m_Reader = reader
        End Sub

        Public Sub New(ByVal path As String)
            m_Reader = New StreamReader(path)
        End Sub

        Public Sub New(ByVal stream As Stream, ByVal defaultEncoding As Encoding)
            m_Reader = New StreamReader(stream, defaultEncoding)
        End Sub

        Public Sub New(ByVal path As String, ByVal defaultEncoding As Encoding)
            m_Reader = New StreamReader(path, defaultEncoding)
        End Sub

        Public Sub New(ByVal stream As Stream, ByVal defaultEncoding As Encoding, ByVal detectEncoding As Boolean)
            m_Reader = New StreamReader(stream, defaultEncoding, detectEncoding)
        End Sub

        Public Sub New(ByVal path As String, ByVal defaultEncoding As Encoding, ByVal detectEncoding As Boolean)
            m_Reader = New StreamReader(path, defaultEncoding, detectEncoding)
        End Sub

        Public Sub New(ByVal stream As Stream, ByVal defaultEncoding As Encoding, ByVal detectEncoding As Boolean, ByVal leaveOpen As Boolean)
            m_Reader = New StreamReader(stream, defaultEncoding, detectEncoding)
            m_LeaveOpen = leaveOpen
        End Sub
#End Region

#Region "Private Functions"
        Private Function GetDelimitedFields() As String()
            If m_Delimiters Is Nothing OrElse m_Delimiters.Length = 0 Then
                Throw New InvalidOperationException("Unable to read delimited fields because Delimiters is Nothing or empty.")
            End If

            Dim result As New Generic.List(Of String)
            Dim line As String
            Dim currentIndex As Integer
            Dim nextIndex As Integer

            line = GetNextLine()

            If line Is Nothing Then Return Nothing

            Do Until nextIndex >= line.Length
                result.Add(GetNextField(line, currentIndex, nextIndex))
                currentIndex = nextIndex
            Loop

            Return result.ToArray()
        End Function

        Private Function GetNextField(ByVal line As String, ByVal startIndex As Integer, ByRef nextIndex As Integer) As String
            Dim inQuote As Boolean
            Dim currentindex As Integer

            If nextIndex = Integer.MinValue Then
                nextIndex = Integer.MaxValue
                Return String.Empty
            End If

            If m_HasFieldsEnclosedInQuotes AndAlso line(currentindex) = """"c Then
                inQuote = True
                startIndex += 1
            End If

            currentindex = startIndex

            Dim mustMatch As Boolean
            For j As Integer = startIndex To line.Length - 1
                If inQuote Then
                    If line(j) = """"c Then
                        inQuote = False
                        mustMatch = True
                    End If
                    Continue For
                End If

                For i As Integer = 0 To m_Delimiters.Length - 1
                    If String.Compare(line, j, m_Delimiters(i), 0, m_Delimiters(i).Length) = 0 Then
                        nextIndex = j + m_Delimiters(i).Length
                        If nextIndex = line.Length Then
                            nextIndex = Integer.MinValue
                        End If
                        If mustMatch Then
                            Return line.Substring(startIndex, j - startIndex - 1)
                        Else
                            Return line.Substring(startIndex, j - startIndex)
                        End If
                    End If
                Next

                If mustMatch Then
                    RaiseDelimiterEx(line)
                End If
            Next

            If inQuote Then
                RaiseDelimiterEx(line)
            End If

            nextIndex = line.Length
            If mustMatch Then
                Return line.Substring(startIndex, nextIndex - startIndex - 1)
            Else
                Return line.Substring(startIndex)
            End If
        End Function

        Private Sub RaiseDelimiterEx(ByVal Line As String)
            m_ErrorLineNumber = m_LineNumber
            m_ErrorLine = Line
            Throw New MalformedLineException("Line " & m_ErrorLineNumber.ToString & " cannot be parsed using the current Delimiters.", m_ErrorLineNumber)
        End Sub

        Private Sub RaiseFieldWidthEx(ByVal Line As String)
            m_ErrorLineNumber = m_LineNumber
            m_ErrorLine = Line
            Throw New MalformedLineException("Line " & m_ErrorLineNumber.ToString & " cannot be parsed using the current FieldWidths.", m_ErrorLineNumber)
        End Sub

        Private Function GetWidthFields() As String()
            If m_FieldWidths Is Nothing OrElse m_FieldWidths.Length = 0 Then
                Throw New InvalidOperationException("Unable to read fixed width fields because FieldWidths is Nothing or empty.")
            End If

            Dim result(m_FieldWidths.Length - 1) As String
            Dim currentIndex As Integer
            Dim line As String

            line = GetNextLine()

            If line.Length < m_MinFieldLength Then
                RaiseFieldWidthEx(line)
            End If

            For i As Integer = 0 To result.Length - 1
                If m_TrimWhiteSpace Then
                    result(i) = line.Substring(currentIndex, m_FieldWidths(i)).Trim
                Else
                    result(i) = line.Substring(currentIndex, m_FieldWidths(i))
                End If
                currentIndex += m_FieldWidths(i)
            Next

            Return result
        End Function

        Private Function IsCommentLine(ByVal Line As String) As Boolean
            If m_CommentTokens Is Nothing Then Return False

            For Each str As String In m_CommentTokens
                If Line.StartsWith(str) Then Return True
            Next

            Return False
        End Function

        Private Function GetNextRealLine() As String
            Dim nextLine As String

            Do
                nextLine = ReadLine()
            Loop Until nextLine Is Nothing OrElse IsCommentLine(nextLine) = False

            Return nextLine
        End Function

        Private Function GetNextLine() As String
            If m_PeekedLine.Count > 0 Then
                Return m_PeekedLine.Dequeue
            Else
                Return GetNextRealLine()
            End If
        End Function
#End Region

#Region "Public Functions"
        Public Sub Close()
            If m_Reader IsNot Nothing AndAlso m_LeaveOpen = False Then
                m_Reader.Close()
            End If
            m_Reader = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub

        Public Function PeekChars(ByVal numberOfChars As Integer) As String
            If numberOfChars < 1 Then Throw New ArgumentException("numberOfChars has to be a positive, non-zero number", "numberOfChars")

            Dim peekedLines() As String
            Dim theLine As String = Nothing
            If m_PeekedLine.Count > 0 Then
                peekedLines = m_PeekedLine.ToArray
                For i As Integer = 0 To m_PeekedLine.Count - 1
                    If IsCommentLine(peekedLines(i)) = False Then
                        theLine = peekedLines(i)
                        Exit For
                    End If
                Next
            End If

            If theLine Is Nothing Then
                Do
                    theLine = m_Reader.ReadLine
                    m_PeekedLine.Enqueue(theLine)
                Loop Until theLine Is Nothing OrElse IsCommentLine(theLine) = False
            End If

            If theLine IsNot Nothing Then
                If theLine.Length <= numberOfChars Then
                    Return theLine
                Else
                    Return theLine.Substring(0, numberOfChars)
                End If
            Else
                Return Nothing
            End If
        End Function

        Public Function ReadFields() As String()
            Select Case m_TextFieldType
                Case FieldType.Delimited
                    Return GetDelimitedFields()
                Case FieldType.FixedWidth
                    Return GetWidthFields()
                Case Else
                    Return GetDelimitedFields()
            End Select
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function ReadLine() As String
            If m_PeekedLine.Count > 0 Then
                Return m_PeekedLine.Dequeue
            End If
            If m_LineNumber = -1 Then
                m_LineNumber = 1
            Else
                m_LineNumber += 1
            End If
            Return m_Reader.ReadLine
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function ReadToEnd() As String
            Return m_Reader.ReadToEnd
        End Function

        Public Sub SetDelimiters(ByVal ParamArray delimiters As String())
            Me.Delimiters = delimiters
            'm_TextFieldType = FieldType.Delimited
        End Sub

        Public Sub SetFieldWidths(ByVal ParamArray fieldWidths As Integer())
            Me.FieldWidths = fieldWidths
            'm_TextFieldType = FieldType.FixedWidth
        End Sub
#End Region

#Region "Properties"
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Property CommentTokens() As String()
            Get
                Return m_CommentTokens
            End Get
            Set(ByVal value As String())
                m_CommentTokens = value
            End Set
        End Property

        Public Property Delimiters() As String()
            Get
                Return m_Delimiters
            End Get
            Set(ByVal value As String())
                m_Delimiters = value
            End Set
        End Property

        Public ReadOnly Property EndOfData() As Boolean
            Get
                Return PeekChars(1) Is Nothing
            End Get
        End Property

        Public ReadOnly Property ErrorLine() As String
            Get
                Return m_ErrorLine
            End Get
        End Property

        Public ReadOnly Property ErrorLineNumber() As Long
            Get
                Return m_ErrorLineNumber
            End Get
        End Property

        Public Property FieldWidths() As Integer()
            Get
                Return m_FieldWidths
            End Get
            Set(ByVal value As Integer())
                m_FieldWidths = value
                If m_FieldWidths IsNot Nothing Then
                    m_MinFieldLength = 0
                    For i As Integer = 0 To m_FieldWidths.Length - 1
                        m_MinFieldLength += value(i)
                    Next
                End If
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Property HasFieldsEnclosedInQuotes() As Boolean
            Get
                Return m_HasFieldsEnclosedInQuotes
            End Get
            Set(ByVal value As Boolean)
                m_HasFieldsEnclosedInQuotes = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property LineNumber() As Long
            Get
                Return m_LineNumber
            End Get
        End Property

        Public Property TextFieldType() As FieldType
            Get
                Return m_TextFieldType
            End Get
            Set(ByVal value As FieldType)
                m_TextFieldType = value
            End Set
        End Property

        Public Property TrimWhiteSpace() As Boolean
            Get
                Return m_TrimWhiteSpace
            End Get
            Set(ByVal value As Boolean)
                m_TrimWhiteSpace = value
            End Set
        End Property
#End Region

#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                Close()
            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
