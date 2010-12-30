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
    ''' The path of the filename as given to the compiler.
    ''' Only includes the path, not the filename.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_RelativePath As String

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

    ''' <summary>
    ''' The state of the Option Infer flag in this file.
    ''' </summary>
    Private m_OptionInfer As OptionInferStatement

    Private m_SymbolDocument As Mono.Cecil.Cil.Document

    Private m_ConditionalConstants As New Generic.List(Of ConditionalConstants)
    Private m_ConditionalConstantsLines As New Generic.List(Of UInteger)

    Private m_Code As String

    Public ReadOnly Property SymbolDocument() As Mono.Cecil.Cil.Document
        Get
            If m_SymbolDocument Is Nothing Then
                m_SymbolDocument = New Mono.Cecil.Cil.Document(System.IO.Path.Combine(m_RelativePath.Replace("<"c, "["c).Replace(">"c, "]"c), m_FileName.Replace("<"c, "["c).Replace(">"c, "]"c)))
                m_SymbolDocument.Language = Cil.DocumentLanguage.Basic
                m_SymbolDocument.LanguageVendor = Cil.DocumentLanguageVendor.Microsoft
                m_SymbolDocument.Type = Cil.DocumentType.Text
            End If
            Return m_SymbolDocument
        End Get
    End Property

    Sub AddConditionalConstants(ByVal Line As UInteger, ByVal Constants As ConditionalConstants)
        m_ConditionalConstants.Add(Constants.Clone)
        m_ConditionalConstantsLines.Add(Line)
    End Sub

    Function GetConditionalConstants(ByVal Line As UInteger) As ConditionalConstants
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

    Property RelativePath() As String
        Get
            Return m_RelativePath
        End Get
        Set(ByVal value As String)
            m_RelativePath = value
        End Set
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_Imports.ResolveCode(Info)
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

    ReadOnly Property OptionInfer As OptionInferStatement
        Get
            Return m_OptionInfer
        End Get
    End Property

    Sub Init(ByVal OptionCompare As OptionCompareStatement, ByVal OptionStrict As OptionStrictStatement, ByVal OptionExplicit As OptionExplicitStatement, ByVal OptionInfer As OptionInferStatement, ByVal [Imports] As ImportsClauses)
        m_OptionCompare = OptionCompare
        m_OptionStrict = OptionStrict
        m_OptionExplicit = OptionExplicit
        m_OptionInfer = OptionInfer
        m_Imports = [Imports]
        Helper.AssertNotNothing(m_Imports)
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

    Public Shadows ReadOnly Property IsOptionInferOn As Boolean
        Get
            If m_OptionInfer Is Nothing Then
                Return Compiler.CommandLine.OptionInfer = CommandLine.OptionInferTypes.On
            Else
                Return m_OptionInfer.IsOn
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
    Sub New(ByVal FileName As String, ByVal RelativePath As String, ByVal Parent As BaseObject)
        MyBase.New(Parent)
        'Try to get the absolute path for all files.
        If FileName Is Nothing OrElse FileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0 Then
            m_FileName = FileName
        Else
            m_FileName = IO.Path.GetFullPath(FileName)
        End If
        m_RelativePath = RelativePath
    End Sub

    Sub New(ByVal FileName As String, ByVal RelativePath As String, ByVal Parent As BaseObject, ByVal Code As String)
        Me.New(FileName, RelativePath, Parent)
        m_Code = Code
    End Sub

    ReadOnly Property CodeStream() As IO.StreamReader
        Get
            Dim Stream As System.IO.Stream
            Dim StreamReader As System.IO.StreamReader

            Try
                If m_Code IsNot Nothing Then
                    Stream = New System.IO.MemoryStream(Compiler.CommandLine.Encoding.GetBytes(m_Code))
                Else
                    Stream = New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                End If

                StreamReader = New System.IO.StreamReader(Stream, Compiler.CommandLine.Encoding, True)

                Return StreamReader
            Catch e As Exception
                Compiler.Report.ShowMessage(Messages.VBNC31007, Span.CommandLineSpan, FileName)
                Return Nothing
            End Try
        End Get
    End Property

    ''' <summary>
    ''' This function is only used for error reporting, no need to do things fast.
    ''' </summary>
    ''' <param name="LineNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DoesLineEndWithLineContinuation(ByVal LineNumber As UInteger) As Boolean
        Dim lines() As String
        Using stream As IO.StreamReader = CodeStream
            lines = stream.ReadToEnd().Split(New String() {VB.vbCrLf, VB.vbCr, VB.vbLf}, StringSplitOptions.None)
        End Using
        If lines.Length < LineNumber Then Return False

        Dim line As String = lines(CInt(LineNumber - 1UI))
        Do While line.Length > 0 AndAlso Scanner.IsWhiteSpace(line(line.Length - 1))
            line = line.Substring(0, line.Length - 1)
        Loop
        Return line.EndsWith(" _")
    End Function

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

    ''' <summary>
    ''' The filename to report to the user in errors.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property FileNameToReport() As String
        Get
            If m_FileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars) >= 0 Then
                Return m_FileName
            End If
            Return System.IO.Path.Combine(m_RelativePath, System.IO.Path.GetFileName(m_FileName))
        End Get
    End Property

End Class
