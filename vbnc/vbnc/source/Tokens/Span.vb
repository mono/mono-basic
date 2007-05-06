' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

''' <summary>
''' Represents a span of code in a source file.
''' </summary>
''' <remarks></remarks>
Public Class Span
    Private m_Line As Integer
    Private m_Column As Short
    'Private m_EndLine As Integer
    Private m_EndColumn As Short
    Private m_File As CodeFile

    Public Shared ReadOnly CommandLineSpan As Span = New Span(Nothing, -1, -1, -1, -1)

    ''' <summary>
    ''' The line of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property Line() As Integer
        Get
            Return m_Line
        End Get
        Set(ByVal value As Integer)
            m_Line = value
        End Set
    End Property

    ''' <summary>
    ''' The column of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property Column() As Short
        Get
            Return m_Column
        End Get
        Set(ByVal value As Short)
            m_Column = value
        End Set
    End Property

    ''' <summary>
    ''' The file of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property File() As CodeFile
        Get
            Return m_File
        End Get
        Set(ByVal value As CodeFile)
            m_File = value
        End Set
    End Property

    Sub Dump(ByVal Xml As Xml.XmlWriter)
        Xml.WriteAttributeString("Location", Me.ToString(False))
    End Sub

    ''' <summary>
    ''' The location expressed as a string that the IDE can understand.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides Function ToString() As String
        Return ToString(False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IncludePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function ToString(ByVal IncludePath As Boolean) As String
        If m_Line < 0 Then
            Return "(in the commandline)"
        End If

        If Not File Is Nothing Then
            If IncludePath Then
                Return String.Format("{0} ({1},{2})", File.FileName, Line.ToString, Column.ToString)
            Else
                Return String.Format("{0} ({1},{2})", File.FileNameToReport, Line.ToString, Column.ToString)
            End If
        Else
            Return String.Format("({0},{1})", Line.ToString, Column.ToString)
        End If
    End Function

    ReadOnly Property AsString() As String
        Get
            Return ToString()
        End Get
    End Property

    ''' <summary>
    ''' The end line of the span.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property EndLine() As Integer
        Get
            Return Line
            'If m_EndLine < Line Then Return Line
            'Return m_EndLine
        End Get
        Set(ByVal value As Integer)
            'm_EndLine = value
        End Set
    End Property

    ''' <summary>
    ''' The end column of the span.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property EndColumn() As Short
        Get
            'If m_EndLine < Line Then
            '    Return Column
            'ElseIf m_EndLine = Line AndAlso 
            If m_EndColumn < Column Then
                Return Column
            Else
                Return m_EndColumn
            End If
        End Get
        Set(ByVal value As Short)
            m_EndColumn = value
        End Set
    End Property

    Sub SpanTo(ByVal Location As Span)
        m_EndColumn = Location.m_EndColumn
        'm_EndLine = Location.m_EndLine
    End Sub

    '''' <summary>
    '''' Default constructor. Does nothing.
    '''' </summary>
    '''' <remarks></remarks>
    'Public Sub New()
    '    'Default constructor
    'End Sub

    Public Sub New(ByVal File As CodeFile, ByVal StartLine As Integer, ByVal StartColumn As Short, Optional ByVal EndLine As Integer = 0, Optional ByVal EndColumn As Short = 0)
        ' m_EndLine = EndLine
        m_EndColumn = EndColumn
        m_File = File
        m_Line = StartLine
        m_Column = StartColumn
    End Sub

    Public Sub New(ByVal FromLocation As Span, ByVal ToLocation As Span)
        Me.New(FromLocation.File, FromLocation.Line, FromLocation.Column, ToLocation.Line, ToLocation.Column)
        Helper.Assert(FromLocation.File.Equals(ToLocation.File))
    End Sub

End Class
