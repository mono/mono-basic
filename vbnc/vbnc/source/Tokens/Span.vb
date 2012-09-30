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

''' <summary>
''' Represents a span of code in a source file.
''' </summary>
''' <remarks></remarks>
Public Structure Span
    Private m_Line As UInteger
    Private m_Column As Byte
    Private m_EndColumn As Byte
    Private m_FileIndex As UShort

    Public Shared ReadOnly CommandLineSpan As Span

    Overloads Function Equals(ByVal Location As Span) As Boolean
        Return m_Line = Location.m_Line AndAlso m_Column = Location.m_Column AndAlso m_EndColumn = Location.m_EndColumn AndAlso m_FileIndex = Location.m_FileIndex
    End Function
    ''' <summary>
    ''' The line of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property Line() As UInteger
        Get
            Return m_Line
        End Get
    End Property

    ''' <summary>
    ''' The column of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property Column() As Byte
        Get
            Return m_Column
        End Get
    End Property

    ''' <summary>
    ''' The file of the location.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property File(ByVal Compiler As Compiler) As CodeFile
        Get
            If Compiler Is Nothing Then Return Nothing
            If m_FileIndex = 0 Then Return Nothing
            If m_FileIndex = UShort.MaxValue Then Return Nothing
            If m_FileIndex > Compiler.CommandLine.Files.Count Then Return Nothing
            Return Compiler.CommandLine.Files(m_FileIndex - 1)
        End Get
    End Property

    ReadOnly Property FileIndex() As UShort
        Get
            Return m_FileIndex
        End Get
    End Property

    ReadOnly Property HasFile() As Boolean
        Get
            Return m_FileIndex <> UShort.MaxValue AndAlso m_FileIndex <> 0
        End Get
    End Property

    ''' <summary>
    ''' The location expressed as a string that the IDE can understand.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Use another overload")> _
    Overrides Function ToString() As String
        Return ToString(False, Nothing)
    End Function

    Overloads Function ToString(ByVal Compiler As Compiler) As String
        Return ToString(False, Compiler)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IncludePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function ToString(ByVal IncludePath As Boolean, ByVal Compiler As Compiler) As String
        If m_Line < 0 Then
            Return "(in the commandline)"
        End If

        If Not File(Compiler) Is Nothing Then
            If IncludePath Then
                Return String.Format("{0} ({1},{2})", File(Compiler).FileName, Line.ToString, Column.ToString)
            Else
                Return String.Format("{0} ({1},{2})", File(Compiler).FileNameToReport, Line.ToString, Column.ToString)
            End If
        Else
            Return String.Format("({0},{1})", Line.ToString, Column.ToString)
        End If
    End Function

    ReadOnly Property AsString() As String
        Get
            Return ToString(Nothing)
        End Get
    End Property

    ReadOnly Property AsString(ByVal Compiler As Compiler) As String
        Get
            Return ToString(Compiler)
        End Get
    End Property

    ''' <summary>
    ''' The end column of the span.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property EndColumn() As Byte
        Get
            Return m_EndColumn + m_Column
        End Get
    End Property

    Sub SpanTo(ByVal Location As Span)
        m_EndColumn = Location.m_EndColumn
    End Sub

    Public Sub New(ByVal FileIndex As UShort, ByVal StartLine As UInteger, ByVal StartColumn As Byte, Optional ByVal EndColumn As Byte = 0)
        m_EndColumn = EndColumn
        m_FileIndex = FileIndex + 1US
        m_Line = StartLine
        m_Column = StartColumn
    End Sub

    Public Sub New(ByVal FromLocation As Span, ByVal ToLocation As Span)
        Me.New(FromLocation.FileIndex - 1US, FromLocation.Line, FromLocation.Column, ToLocation.Column)
        Helper.Assert(FromLocation.FileIndex = ToLocation.FileIndex)
    End Sub

    Public Shared Operator >(ByVal a As Span, ByVal b As Span) As Boolean
        If a.FileIndex <> b.FileIndex Then Throw New ArgumentException("The spans are in different files, cannot compare them")
        If a.Line > b.Line Then Return True
        If a.Line < b.Line Then Return False
        Return a.Column > b.Column
    End Operator

    Public Shared Operator <(ByVal a As Span, ByVal b As Span) As Boolean
        Return b > a
    End Operator

End Structure

