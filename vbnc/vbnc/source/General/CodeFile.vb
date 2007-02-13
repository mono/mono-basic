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
''' Represents a file containing compilable code.
''' Also contains the file specific options of Visual Basic,
''' like Option Strict / Option Explicit / Option Compare.
''' These options are set to the options passed in on the command
''' line when the code file is created, later, when the file is 
''' parsed, the options are changed (if they are specified in the 
''' code of the file)
''' 
''' Start  ::=
''' [  OptionStatement+  ]
'''	[  ImportsStatement+  ]
'''	[  AttributesStatement+  ]
'''	[  NamespaceMemberDeclaration+  ]
''' </summary>
Public Class CodeFile
    Inherits BaseObject

    Private Shared m_UTF8Throw As System.Text.Encoding

    ''' <summary>
    ''' The filename of the file.
    ''' </summary>
    Private m_FileName As String

    ''' <summary>
    ''' The imports clauses this file has.
    ''' </summary>
    Private m_Imports As ImportsClauses

    ''' <summary>
    ''' The state of the Option Explicit flag in this file.
    ''' </summary>
    Private m_OptionExplicit As OptionExplicitStatement

    ''' <summary>
    ''' The state of the Option Strict flag in this file.
    ''' </summary>
    Private m_OptionStrict As OptionStrictStatement

    ''' <summary>
    ''' The state of the Option Compare flag in this file.
    ''' </summary>
    Private m_OptionCompare As OptionCompareStatement

    Private m_SymbolDocument As System.Diagnostics.SymbolStore.ISymbolDocumentWriter

    Private m_ConditionalConstants As New Generic.List(Of ConditionalConstants)
    Private m_ConditionalConstantsLines As New Generic.List(Of Integer)

    Sub AddConditionalConstants(ByVal Line As Integer, ByVal Constants As ConditionalConstants)
        m_ConditionalConstants.Add(Constants.Clone)
        m_ConditionalConstantsLines.Add(Line)
    End Sub

    Function GetConditionalConstants(ByVal Line As Integer) As ConditionalConstants
        If m_ConditionalConstantsLines.Count = 0 Then Return Nothing

        'If the first #const is after the line, no constants in this file at the line
        If m_ConditionalConstantsLines(0) > Line Then Return Nothing

        For i As Integer = 0 To m_ConditionalConstantsLines.Count - 1
            'If the current #const is after the line, the previous one corresponds to the line
            If m_ConditionalConstantsLines(i) > Line Then
                Return m_ConditionalConstants(i - 1)
            End If
        Next

        'If no constants are after the line, the last is the one.
        Return m_ConditionalConstants(m_ConditionalConstants.Count - 1)
    End Function

    Private Shared ReadOnly Property UTF8Throw() As System.Text.Encoding
        Get
            ' Use no preamble to let StreamReader use a non-throwing decoder
            ' when UTF-8 byte order mark found.
            If m_UTF8Throw Is Nothing Then m_UTF8Throw = New System.Text.UTF8Encoding(False, True)
            Return m_UTF8Throw
        End Get
    End Property

    ReadOnly Property SymbolDocument() As System.Diagnostics.SymbolStore.ISymbolDocumentWriter
        Get
            Return m_SymbolDocument
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return Helper.ResolveCodeCollection(m_Imports, Info)
    End Function

    ReadOnly Property OptionExplicit() As OptionExplicitStatement
        Get
            Return m_OptionExplicit
        End Get
    End Property

    ReadOnly Property OptionStrict() As OptionStrictStatement
        Get
            Return m_OptionStrict
        End Get
    End Property

    ReadOnly Property OptionCompare() As OptionCompareStatement
        Get
            Return m_OptionCompare
        End Get
    End Property

    Sub Init(ByVal OptionCompare As OptionCompareStatement, ByVal OptionStrict As OptionStrictStatement, ByVal OptionExplicit As OptionExplicitStatement, ByVal [Imports] As ImportsClauses)
        m_OptionCompare = OptionCompare
        m_OptionStrict = OptionStrict
        m_OptionExplicit = OptionExplicit
        m_Imports = [Imports]

        If Compiler.SymbolWriter IsNot Nothing Then
            m_SymbolDocument = Compiler.SymbolWriter.DefineDocument(Me.FileName, Nothing, Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' The state of the Option Explicit flag in this file.
    ''' Looks up in commandline options if not set.
    ''' </summary>
    ReadOnly Property IsOptionExplicitOn() As Boolean
        Get
            If m_OptionExplicit Is Nothing Then
                Return Compiler.CommandLine.OptionExplicit = CommandLine.OptionExplicitTypes.On
            Else
                Return m_OptionExplicit.IsOn
            End If
        End Get
    End Property

    ''' <summary>
    ''' The state of the Option Strict flag in this file.
    ''' Looks up in commandline options if not set.
    ''' </summary>
    ReadOnly Property IsOptionStrictOn() As Boolean
        Get
            If m_OptionStrict Is Nothing Then
                Return Compiler.CommandLine.OptionStrict = CommandLine.OptionStrictTypes.On
            Else
                Return m_OptionStrict.IsOn
            End If
        End Get
    End Property

    ''' <summary>
    ''' The state of the Option Compare flag in this file.
    ''' Looks up in commandline options if not set.
    ''' </summary>
    ReadOnly Property IsOptionCompareBinary() As Boolean
        Get
            If m_OptionCompare Is Nothing Then
                Return Compiler.CommandLine.OptionCompare = CommandLine.OptionCompareTypes.Binary
            Else
                Return m_OptionCompare.IsBinary
            End If
        End Get
    End Property

    ''' <summary>
    ''' The state of the Option Compare flag in this file.
    ''' Looks up in commandline options if not set.
    ''' </summary>
    ReadOnly Property IsOptionCompareText() As Boolean
        Get
            Return Not IsOptionCompareBinary
        End Get
    End Property

    ''' <summary>
    ''' The imports clauses this file has.
    ''' </summary>
    ReadOnly Property [Imports]() As ImportsClauses
        Get
            Return m_Imports
        End Get
    End Property

    ''' <summary>
    ''' Compare one CodeFile to another based on filename.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Overrides Function Equals(ByVal value As Object) As Boolean
        Dim file As CodeFile = TryCast(value, CodeFile)
        If file IsNot Nothing Then
            If file Is Me Then
                Return True
            Else
                Return Microsoft.VisualBasic.Strings.StrComp(m_FileName, file.FileName, Microsoft.VisualBasic.CompareMethod.Text) = 0
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Create a new code file with the specified filename.
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <remarks></remarks>
    Sub New(ByVal FileName As String, ByVal Parent As IBaseObject)
        MyBase.New(Parent)
        'Try to get the absolute path for all files.
        m_FileName = IO.Path.GetFullPath(FileName)
    End Sub

    ''' <summary>
    ''' Get the code of the file (the contents).
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Code() As String
        Get
            Dim FileStream As System.IO.FileStream
            Dim StreamReader As System.IO.StreamReader
            Dim Encoding As System.Text.Encoding
            Dim IsUTF8 As Boolean

            Try
                FileStream = New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                Try
                    Encoding = Compiler.CommandLine.Encoding
                    IsUTF8 = Encoding.CodePage = 65001

                    ' No UTF-8 detection required when default is UTF-8.
                    If Not IsUTF8 Then
                        ' Decode using UTF-8 when can be decoded using UTF-8.
                        StreamReader = New System.IO.StreamReader(FileStream, UTF8Throw, True)
                        Try
                            Return StreamReader.ReadToEnd()
                        Catch e As ArgumentException
                            FileStream.Position = 0
                        End Try
                    End If

                    ' Byte order mark was already detected when default is not UTF-8.
                    StreamReader = New System.IO.StreamReader(FileStream, Encoding, IsUTF8)
                    Return StreamReader.ReadToEnd()
                Finally
                    FileStream.Close()
                End Try
            Catch e As Exception
                Compiler.Report.ShowMessage(Messages.VBNC31007, FileName)
                Return ""
            End Try
        End Get
    End Property

    ReadOnly Property CodeStream() As IO.StreamReader
        Get
            Dim FileStream As System.IO.FileStream
            Dim StreamReader As System.IO.StreamReader
            Dim Encoding As System.Text.Encoding
            Dim IsUTF8 As Boolean

            Try
                FileStream = New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

                Encoding = Compiler.CommandLine.Encoding
                IsUTF8 = Encoding.CodePage = 65001

                ' No UTF-8 detection required when default is UTF-8.
                'If Not IsUTF8 Then
                '    ' Decode using UTF-8 when can be decoded using UTF-8.
                '    StreamReader = New System.IO.StreamReader(FileStream, UTF8Throw, True)
                '    Try
                '        Return StreamReader
                '    Catch e As ArgumentException
                '        FileStream.Position = 0
                '    End Try
                'End If

                ' Byte order mark was already detected when default is not UTF-8.
                StreamReader = New System.IO.StreamReader(FileStream, Encoding, IsUTF8)
                Return StreamReader
            Catch e As Exception
                Compiler.Report.ShowMessage(Messages.VBNC31007, FileName)
                Return Nothing
            End Try
        End Get
    End Property

    ''' <summary>
    ''' The Filename of the codefile.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property FileName() As String
        Get
            Return m_FileName
        End Get
    End Property

End Class
