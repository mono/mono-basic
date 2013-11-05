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

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If

Public Class Parser
    Private tm As tm
    Private m_Compiler As Compiler

    Private m_ShowErrors As Boolean = True

    Private ReadOnly Property ShowErrors() As Boolean
        Get
            Return m_ShowErrors
        End Get
    End Property

    Public ReadOnly Property Compiler() As Compiler
        Get
            Return m_Compiler
        End Get
    End Property

    Public Sub New(ByVal Compiler As Compiler)
        m_Compiler = Compiler
        tm = m_Compiler.tm
        Helper.Assert(tm IsNot Nothing)
    End Sub

    Public Sub New(ByVal Compiler As Compiler, ByVal TokenReader As Scanner)
        m_Compiler = Compiler
        tm = New tm(Compiler, TokenReader)
        tm.NextToken()
        Helper.Assert(tm IsNot Nothing)
    End Sub

    Public Function Parse(ByVal RootNamespace As String, ByVal assembly As AssemblyDeclaration) As Boolean
        Dim result As Boolean = True

        result = ParseAssemblyDeclaration(RootNamespace, assembly) AndAlso result

        result = Compiler.Report.Errors = 0 AndAlso result

        Return result
    End Function

    ''' <summary>
    ''' Can be called multiple times. (Will just exit).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseFileHeader(ByVal CodeFile As CodeFile, ByVal [Assembly] As AssemblyDeclaration) As Boolean
        Dim result As Boolean = True

        Dim m_OptionExplicit As OptionExplicitStatement = CodeFile.OptionExplicit
        Dim m_OptionStrict As OptionStrictStatement = CodeFile.OptionStrict
        Dim m_OptionCompare As OptionCompareStatement = CodeFile.OptionCompare
        Dim m_OptionInfer As OptionInferStatement = CodeFile.OptionInfer
        Dim m_Imports As ImportsClauses = CodeFile.Imports

        While tm.CurrentToken.Equals(KS.Option)
            If OptionExplicitStatement.IsMe(tm) Then
                If m_OptionExplicit IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Explicit") AndAlso result
                End If
                m_OptionExplicit = ParseOptionExplicitStatement(CodeFile)
                If m_OptionExplicit Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf OptionStrictStatement.IsMe(tm) Then
                If m_OptionStrict IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Strict") AndAlso result
                End If
                m_OptionStrict = ParseOptionStrictStatement(CodeFile)
                If m_OptionStrict Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf OptionCompareStatement.IsMe(tm) Then
                If m_OptionCompare IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Compare") AndAlso result
                End If
                m_OptionCompare = ParseOptionCompareStatement(CodeFile)
                If m_OptionCompare Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf OptionInferStatement.IsMe(tm) Then
                If m_OptionInfer IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Infer") AndAlso result
                End If
                m_OptionInfer = ParseOptionInferStatement(CodeFile)
                If m_OptionInfer Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                result = Compiler.Report.ShowMessage(Messages.VBNC30206, tm.CurrentLocation) AndAlso result
                tm.GotoNewline(False)
            End If
        End While

        If m_Imports Is Nothing Then m_Imports = New ImportsClauses([Assembly])
        Dim tmpImportsStatements As Generic.List(Of ImportsStatement)
        tmpImportsStatements = ParseImportsStatements([Assembly])
        For Each imp As ImportsStatement In tmpImportsStatements
            m_Imports.AddRange(imp.Clauses)
        Next

        CodeFile.Init(m_OptionCompare, m_OptionStrict, m_OptionExplicit, m_OptionInfer, m_Imports)

        Return result
    End Function
    ''' <summary>
    ''' OptionCompareStatement  ::=  "Option" "Compare" CompareOption  StatementTerminator
    ''' CompareOption  ::=  "Binary" | "Text"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOptionCompareStatement(ByVal Parent As BaseObject) As OptionCompareStatement
        Dim result As New OptionCompareStatement(Parent)

        Dim m_IsBinary As Boolean

        tm.AcceptIfNotInternalError(KS.Option)
        tm.AcceptIfNotInternalError("Compare")

        If tm.Accept("Text") Then
            m_IsBinary = False
        ElseIf tm.Accept("Binary") Then
            m_IsBinary = True
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30207, tm.CurrentLocation)
            tm.GotoNewline(False)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_IsBinary)

        Return result
    End Function

    ''' <summary>
    ''' OptionStrictStatement  ::=  "Option" "Strict" [  OnOff  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOptionInferStatement(ByVal Parent As BaseObject) As OptionInferStatement
        Dim result As New OptionInferStatement(Parent)

        Dim m_Off As Boolean

        tm.AcceptIfNotInternalError(KS.Option)
        tm.AcceptIfNotInternalError("Infer")

        If tm.Accept(KS.On) Then
            m_Off = False
        ElseIf tm.Accept("Off") Then
            m_Off = True
        ElseIf Not tm.AcceptEndOfStatement() Then
            Compiler.Report.ShowMessage(Messages.VBNC30620, tm.CurrentLocation)
            tm.GotoNewline(False)
        End If

        result.Init(m_Off)

        Return result
    End Function


    ''' <summary>
    ''' OptionStrictStatement  ::=  "Option" "Strict" [  OnOff  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOptionStrictStatement(ByVal Parent As BaseObject) As OptionStrictStatement
        Dim result As New OptionStrictStatement(Parent)

        Dim m_Off As Boolean

        tm.AcceptIfNotInternalError(KS.Option)
        tm.AcceptIfNotInternalError("Strict")

        If tm.Accept(KS.On) Then
            m_Off = False
        ElseIf tm.Accept("Off") Then
            m_Off = True
        ElseIf Not tm.AcceptEndOfStatement() Then
            Compiler.Report.ShowMessage(Messages.VBNC30620, tm.CurrentLocation)
            tm.GotoNewline(False)
        End If

        result.Init(m_Off)

        Return result
    End Function

    ''' <summary>
    ''' OptionExplicitStatement  ::=  Option  Explicit  [  OnOff  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOptionExplicitStatement(ByVal Parent As BaseObject) As OptionExplicitStatement
        Dim result As New OptionExplicitStatement(Parent)

        Dim m_Off As Boolean
        tm.AcceptIfNotInternalError(KS.Option)
        tm.AcceptIfNotInternalError("Explicit")

        If tm.Accept(KS.On) Then
            m_Off = False
        ElseIf tm.Accept("Off") Then
            m_Off = True
        ElseIf Not tm.AcceptEndOfStatement() Then
            Compiler.Report.ShowMessage(Messages.VBNC30640, tm.CurrentLocation)
            tm.GotoNewline(False)
        End If

        result.Init(m_Off)
        Return result
    End Function

    ''' <summary>
    ''' ImportsClauses  ::= ImportsClause  | ImportsClauses  ","  ImportsClause
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseImportsClauses(ByVal Parent As ImportsStatement) As ImportsClauses
        Dim result As New ImportsClauses(Parent)

        If ParseList(Of ImportsClause)(result, New ParseDelegate_Parent(Of ImportsClause)(AddressOf ParseImportsClause), result) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Parses clauses seen on the command line.
    ''' ImportsClauses  ::= ImportsClause  | ImportsClauses  ","  ImportsClause
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function ParseImportsClauses(ByVal Parent As ImportsClauses, ByVal str As String) As Boolean
        Dim result As Boolean = True

        For Each clause As String In str.Split(","c)
            If clause <> "" Then
                Dim newClause As ImportsClause
                newClause = ParseImportsClause(Parent, str)
                If newClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)
                If Parent.Exists(newClause) Then
                    If newClause.IsNamespaceClause Then '
                        'ignore the duplication
                    ElseIf newClause.IsAliasClause Then
                        Parent.Compiler.Report.SaveMessage(Messages.VBNC30572, Span.CommandLineSpan, newClause.AsAliasClause.Name)
                    Else
                        Throw New InternalException("")
                    End If
                Else
                    Parent.Add(newClause)
                End If
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' ImportsClause  ::=  ImportsAliasClause  |  ImportsNamespaceClause
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function ParseImportsClause(ByVal Parent As ParsedObject, ByVal str As String) As ImportsClause
        Dim result As New ImportsClause(Parent)

        If ImportsAliasClause.IsMe(str) Then
            Dim m_Clause As ImportsAliasClause
            m_Clause = ParseImportsAliasClause(Parent, str)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)
            result.Init(m_Clause)
        Else
            Dim m_Clause As ImportsNamespaceClause
            m_Clause = ParseImportsNamespaceClause(Parent, str)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)
            result.Init(m_Clause)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ImportsClause  ::=  ImportsAliasClause  |  ImportsNamespaceClause
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseImportsClause(ByVal Parent As ParsedObject) As ImportsClause
        Dim result As New ImportsClause(Parent)

        If ImportsAliasClause.IsMe(tm) Then
            Dim m_Clause As ImportsAliasClause
            m_Clause = ParseImportsAliasClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(m_Clause)
        Else
            Dim m_Clause As ImportsNamespaceClause
            m_Clause = ParseImportsNamespaceClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(m_Clause)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ImportsAliasClause  ::=
    '''	Identifier  =  QualifiedIdentifier  |
    '''	Identifier  =  ConstructedTypeName
    ''' 
    ''' ConstructedTypeName  ::=
    '''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    ''' 
    ''' This overload is used when parsing commandline imports.
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ParseImportsAliasClause(ByVal Parent As ParsedObject, ByVal str As String) As ImportsAliasClause
        Dim result As New ImportsAliasClause(Parent)

        Dim m_Identifier As Identifier
        Dim m_Second As ImportsNamespaceClause = Nothing

        Dim values() As String = str.Split("="c)
        If values.Length <> 2 Then Return Nothing

        m_Identifier = New Identifier(result, values(0), Span.CommandLineSpan, TypeCharacters.Characters.None)

        m_Second = ParseImportsNamespaceClause(result, values(1))
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)

        result.Init(m_Identifier, m_Second)

        Return result
    End Function

    ''' <summary>
    ''' ImportsAliasClause  ::=
    '''	Identifier  =  QualifiedIdentifier  |
    '''	Identifier  =  ConstructedTypeName
    ''' 
    ''' ConstructedTypeName  ::=
    '''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseImportsAliasClause(ByVal Parent As ParsedObject) As ImportsAliasClause
        Dim result As New ImportsAliasClause(Parent)

        Dim m_Identifier As Identifier
        Dim m_Second As ImportsNamespaceClause = Nothing

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        tm.AcceptIfNotInternalError(KS.Equals)

        m_Second = ParseImportsNamespaceClause(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Identifier, m_Second)

        Return result
    End Function

    ''' <summary>
    ''' ImportsNamespaceClause  ::=	QualifiedIdentifier  |	ConstructedTypeName
    ''' 
    ''' ConstructedTypeName  ::=
    '''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    '''    
    ''' Only namespaces, classes, structures, enumerated types, and standard modules may be imported.
    ''' This overload is used when parsing commandline imports.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function ParseImportsNamespaceClause(ByVal Parent As ParsedObject, ByVal str As String) As ImportsNamespaceClause
        Dim result As New ImportsNamespaceClause(Parent)

        Dim qi As QualifiedIdentifier = Nothing
        qi = ParseQualifiedIdentifier(result, str)
        If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)

        result.Init(qi)

        Return result
    End Function

    ''' <summary>
    ''' ImportsNamespaceClause  ::=	QualifiedIdentifier  |	ConstructedTypeName
    ''' 
    ''' ConstructedTypeName  ::=
    '''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    '''    
    ''' Only namespaces, classes, structures, enumerated types, and standard modules may be imported.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseImportsNamespaceClause(ByVal Parent As ParsedObject) As ImportsNamespaceClause
        Dim result As New ImportsNamespaceClause(Parent)

        Dim iCurrent As RestorablePoint = tm.GetRestorablePoint
        Dim qi As QualifiedIdentifier

        qi = ParseQualifiedIdentifier(result)
        If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If result IsNot Nothing AndAlso tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of Then
            Dim ctn As ConstructedTypeName = Nothing
            tm.RestoreToPoint(iCurrent)
            ctn = ParseConstructedTypeName(result)
            If ctn Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(ctn)
        Else
            tm.IgnoreRestoredPoint()
            result.Init(qi)
        End If

        Return result
    End Function

    Private Function ParseAssemblyDeclaration(ByVal RootNamespace As String, ByVal assembly As AssemblyDeclaration) As Boolean
        Dim result As Boolean = True

        Dim iLastLocation As Span

        Dim AssemblyAttributes As New Attributes(assembly)

        tm.NextToken() 'Goto the first token

        Do Until tm.CurrentToken.IsEndOfCode
#If EXTENDEDDEBUG Then
            Dim iFileCount, iTotalFiles As Integer
            iFileCount += 1
            iTotalFiles = Me.Compiler.CommandLine.Files.Count
            Me.Compiler.Report.WriteLine(Report.ReportLevels.Debug, "Parsing file " & tm.CurrentToken.Location.File.FileName & " (" & iFileCount & " of " & iTotalFiles & " files)")
#End If
            iLastLocation = tm.CurrentLocation

            While tm.AcceptNewLine

            End While
            '[  OptionStatement+  ]
            '[  ImportsStatement+  ]

            If Me.ParseFileHeader(tm.CurrentLocation.File(Compiler), assembly) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            ''	[  AttributesStatement+  ]
            'If vbnc.Attributes.IsMe(tm) Then
            '    If Me.ParseAttributes(result, AssemblyAttributes) = False Then
            '        Helper.ErrorRecoveryNot    Implemented()
            '    End If
            'End If

            '	[  NamespaceMemberDeclaration+  ]
            result = ParseAssemblyMembers(assembly, RootNamespace) AndAlso result

            While tm.AcceptNewLine

            End While
            tm.AcceptEndOfFile()
            If iLastLocation.Equals(tm.CurrentLocation) Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation) AndAlso result
                tm.GotoNewline(False)
            End If
        Loop

        assembly.Init(AssemblyAttributes)

        Return result
    End Function

    ''' <summary>
    '''  Attributes ::=	AttributeBlock  |	Attributes  AttributeBlock
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributes(ByVal Parent As ParsedObject, ByRef Attributes As Attributes) As Boolean
        Dim result As Boolean = True

        While AttributeBlock.IsMe(tm)
            If Attributes Is Nothing Then Attributes = New Attributes(Parent)
            If ParseAttributeBlock(Parent, Attributes) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End While

        Return result
    End Function
    ''' <summary>
    '''  Parses attributes (if any). Always returns something.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributes(ByVal Parent As ParsedObject) As Attributes
        Dim result As Attributes = Nothing

        If Attributes.IsMe(tm) Then
            While AttributeBlock.IsMe(tm)
                If result Is Nothing Then result = New Attributes(Parent)
                If ParseAttributeBlock(Parent, result) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If
            End While
        End If

        Return result
    End Function
    ''' <summary>
    ''' AttributeBlock  ::=  "&lt;"  AttributeList  "&gt;"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributeBlock(ByVal Parent As ParsedObject, ByVal Attributes As Attributes) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Attributes IsNot Nothing)

        tm.AcceptIfNotInternalError(KS.LT)
        tm.AcceptNewLine()

        If ParseAttributeList(Parent, Attributes) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        tm.AcceptNewLine()
        result = tm.AcceptIfNotError(KS.GT) AndAlso result
        If Attributes(0).IsAssembly = False AndAlso Attributes(0).IsModule = False Then
            tm.AcceptNewLine()
        End If

        Return result
    End Function

    ''' <summary>
    ''' AttributeList  ::=	Attribute  | AttributeList  ,  Attribute
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributeList(ByVal Parent As ParsedObject, ByVal Attributes As Attributes) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Attributes IsNot Nothing)

        Do
            Dim Attribute As Attribute
            Attribute = ParseAttribute(Parent)
            If Attribute Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Attributes.Add(Attribute)
        Loop While tm.Accept(KS.Comma)

        Return result
    End Function

    ''' <summary>
    ''' Attribute          ::= [  AttributeModifier  ":"  ]  SimpleTypeName  [  "("  [  AttributeArguments  ]  ")"  ]
    ''' AttributeModifier  ::=  "Assembly" | "Module"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttribute(ByVal Parent As ParsedObject) As Attribute
        Dim result As New Attribute(Parent)

        Dim m_IsAssembly As Boolean
        Dim m_IsModule As Boolean
        Dim m_SimpleTypeName As SimpleTypeName = Nothing
        Dim m_AttributeArguments As AttributeArguments = Nothing

        If tm.Accept("Assembly") Then
            m_IsAssembly = True
            If tm.AcceptIfNotError(KS.Colon) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.Accept(KS.Module) Then
            m_IsModule = True
            If tm.AcceptIfNotError(KS.Colon) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        m_SimpleTypeName = ParseSimpleTypeName(result)
        If m_SimpleTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.LParenthesis) Then
            If tm.CurrentToken <> KS.RParenthesis Then
                m_AttributeArguments = ParseAttributeArguments(result)
                If m_AttributeArguments Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_IsAssembly, m_IsModule, m_SimpleTypeName, m_AttributeArguments)

        Return result
    End Function

    ''' <summary>
    ''' AttributeArguments  ::=	
    '''     AttributePositionalArgumentList  |
    ''' 	AttributePositionalArgumentList  ,  VariablePropertyInitializerList  |
    '''	    VariablePropertyInitializerList
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributeArguments(ByVal Parent As ParsedObject) As AttributeArguments
        Dim result As New AttributeArguments(Parent)

        Dim m_AttributePositionalArgumentList As New AttributePositionalArgumentList(result)
        Dim m_VariablePropertyInitializerList As New VariablePropertyInitializerList(result)

        If AttributePositionalArgumentList.CanBeMe(tm) Then
            Do
                Dim newObject As AttributeArgumentExpression
                newObject = ParseAttributeArgumentExpression(Parent)
                If newObject Is Nothing Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If
                m_AttributePositionalArgumentList.Add(newObject)

                If tm.CurrentToken = KS.Comma Then
                    Dim current As RestorablePoint = tm.GetRestorablePoint
                    tm.NextToken()
                    If AttributePositionalArgumentList.CanBeMe(tm) = False Then
                        tm.RestoreToPoint(current)
                        Exit Do
                    Else
                        tm.RestoreToPoint(current)
                    End If
                End If
            Loop While tm.Accept(KS.Comma)
        End If

        If m_AttributePositionalArgumentList.Count = 0 OrElse tm.Accept(KS.Comma) Then
            If ParseList(Of VariablePropertyInitializer)(m_VariablePropertyInitializerList, New ParseDelegate_Parent(Of VariablePropertyInitializer)(AddressOf ParseVariablePropertyInitializer), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If

        result.Init(m_AttributePositionalArgumentList, m_VariablePropertyInitializerList)

        Return result
    End Function

    ''' <summary>
    ''' Parses lists of type List ::= Item | List "," Item
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseList(Of T As ParsedObject)(ByVal List As BaseList(Of T), ByVal ParseMethod As ParseDelegate_Parent(Of T), ByVal Parent As ParsedObject) As Boolean
        Helper.Assert(List IsNot Nothing, "List was nothing, tm.CurrentToken=" & tm.CurrentLocation.ToString(Compiler))
        Do
            Dim newObject As T
            newObject = ParseMethod(Parent)
            If newObject Is Nothing Then
                Return False
            End If
            List.Add(newObject)
        Loop While tm.Accept(KS.Comma)
        Return True
    End Function

    Private Delegate Function ParseDelegate_Parent(Of T)(ByVal Parent As ParsedObject) As T

    ''' <summary>
    ''' VariablePropertyInitializer  :: IdentifierOrKeyword  ":="  AttributeArgumentExpression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariablePropertyInitializer(ByVal Parent As ParsedObject) As VariablePropertyInitializer
        Dim result As New VariablePropertyInitializer(Parent)

        Dim m_IdentifierOrKeyword As IdentifierOrKeyword
        Dim m_AttributeArgumentExpression As AttributeArgumentExpression

        m_IdentifierOrKeyword = ParseIdentifierOrKeyword(result)
        If m_IdentifierOrKeyword Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        tm.AcceptIfNotInternalError(KS.Colon)
        tm.AcceptIfNotInternalError(KS.Equals)
        tm.AcceptNewLine()

        m_AttributeArgumentExpression = ParseAttributeArgumentExpression(result)
        If m_AttributeArgumentExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_IdentifierOrKeyword, m_AttributeArgumentExpression)

        Return result
    End Function

    Private Function ParseIdentifierOrKeyword(ByVal Parent As ParsedObject) As IdentifierOrKeyword
        Dim result As IdentifierOrKeyword

        If tm.CurrentToken.IsIdentifierOrKeyword Then
            result = New IdentifierOrKeyword(Parent, tm.CurrentToken)
            tm.NextToken()
        Else
            Helper.AddError(Compiler, tm.CurrentLocation)
            result = Nothing
        End If

        Return result
    End Function

    ''' <summary>
    ''' AttributeArgumentExpression  ::=
    '''   ConstantExpression  |
    '''   GetTypeExpression  |
    '''   ArrayCreationExpression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributeArgumentExpression(ByVal Parent As ParsedObject) As AttributeArgumentExpression
        Dim result As New AttributeArgumentExpression(Parent)

        Dim m_Expression As Expression

        If tm.CurrentToken = KS.GetType Then
            m_Expression = ParseGetTypeExpression(result)
        ElseIf tm.CurrentToken = KS.[New] Then
            m_Expression = ParseArrayCreationExpression(result)
        Else
            m_Expression = ParseExpression(result)
        End If

        result.Init(m_Expression)

        Return result
    End Function



    ''' <summary>
    ''' Type | QualifiedIdentifier ( Of [TypeArityList] )
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseGetTypeTypeName(ByVal Parent As GetTypeExpression) As GetTypeTypeName
        'TypeName |
        'QualifiedIdentifier (Of [TypeArityList])
        'TypeArityList ::=
        ' , |
        ' TypeParameterList ,
        Dim result As New GetTypeTypeName(Parent)

        'First try to parse as typename, if no 
        'success try as qualifiedidentifier.
        Dim m_TypeName As TypeName
        Dim iCurPos As RestorablePoint = tm.GetRestorablePoint

        m_TypeName = ParseTypeName(result)
        If m_TypeName Is Nothing Then
            tm.RestoreToPoint(iCurPos)

            Dim qn As QualifiedIdentifier

            qn = ParseQualifiedIdentifier(result)
            If qn Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            tm.AcceptIfNotInternalError(KS.LParenthesis)
            tm.AcceptIfNotError(KS.Of)

            Dim typeArity As Integer = 1
            Do While tm.Accept(KS.Comma)
                typeArity += 1
            Loop

            tm.AcceptIfNotError(KS.RParenthesis)

            result.Init(qn, typeArity)
        Else
            tm.IgnoreRestoredPoint()
            result.Init(m_TypeName)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ArrayCreationExpression    ::= "New" NonArrayTypeName ArraySizeInitializationModifier ArrayElementInitializer
    ''' 
    ''' LAMESPEC? I think the following should be used:
    ''' ArrayCreationExpression    ::= "New" NonArrayTypeName ArrayNameModifier ArrayElementInitializer
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArrayCreationExpression(ByVal Parent As ParsedObject) As ArrayCreationExpression
        Dim result As New ArrayCreationExpression(Parent)

        Dim m_ArrayElementInitializer As ArrayElementInitializer
        Dim m_NonArrayTypeName As NonArrayTypeName
        Dim m_ArrayNameModifier As ArrayNameModifier

        tm.AcceptIfNotInternalError(KS.[New])

        m_NonArrayTypeName = ParseNonArrayTypeName(result)

        If tm.CurrentToken <> KS.LParenthesis Then
            If ShowErrors Then tm.AcceptIfNotError(KS.LParenthesis)
            Return Nothing
        End If
        If ArrayNameModifier.CanBeMe(tm) = False Then
            If ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC90007, tm.CurrentLocation, tm.CurrentToken.ToString)
            Return Nothing
        End If

        m_ArrayNameModifier = ParseArrayNameModifier(result)
        If m_ArrayNameModifier Is Nothing Then
            If m_ShowErrors = False Then Return Nothing
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.CurrentToken <> KS.LBrace Then
            If ShowErrors Then tm.AcceptIfNotError(KS.LBrace)
            Return Nothing
        End If

        m_ArrayElementInitializer = ParseArrayElementInitializer(result)
        If m_ArrayElementInitializer Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_NonArrayTypeName, m_ArrayNameModifier, m_ArrayElementInitializer)

        Return result
    End Function

    ''' <summary>
    ''' ArrayElementInitializer  ::=  {  [  VariableInitializerList  ]  }
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArrayElementInitializer(ByVal Parent As ParsedObject) As ArrayElementInitializer
        Dim result As New ArrayElementInitializer(Parent)

        Dim m_VariableInitializerList As VariableInitializerList

        m_VariableInitializerList = New VariableInitializerList(result)

        tm.AcceptIfNotInternalError(KS.LBrace)
        If tm.Accept(KS.RBrace) = False Then

            If ParseList(Of VariableInitializer)(m_VariableInitializerList, New ParseDelegate_Parent(Of VariableInitializer)(AddressOf ParseVariableInitializer), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            If tm.AcceptIfNotError(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_VariableInitializerList)

        Return result
    End Function

    ''' <summary>
    ''' VariableInitializer  ::=  RegularInitializer  |  ArrayElementInitializer
    ''' RegularInitializer ::= Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableInitializer(ByVal Parent As ParsedObject) As VariableInitializer
        Dim result As New VariableInitializer(Parent)

        If ArrayElementInitializer.CanBeMe(tm) Then
            Dim newAEI As ArrayElementInitializer
            newAEI = ParseArrayElementInitializer(Parent)
            If newAEI Is Nothing Then Return Nothing
            result.Init(newAEI)
        Else
            Dim newExp As Expression
            newExp = ParseExpression(Parent)
            If newExp Is Nothing Then Return Nothing
            result.Init(newExp)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ArrayNameModifier  ::=	ArrayTypeModifiers  |	ArraySizeInitializationModifier
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArrayNameModifier(ByVal Parent As ParsedObject) As ArrayNameModifier
        Dim result As New ArrayNameModifier(Parent)

        If ArrayTypeModifiers.CanBeMe(tm) Then
            Dim newATM As ArrayTypeModifiers
            newATM = ParseArrayTypeModifiers(result)
            If newATM Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(newATM)
        ElseIf ArraySizeInitializationModifier.CanBeMe(tm) Then
            Dim newASIM As ArraySizeInitializationModifier
            newASIM = ParseArraySizeInitializationModifer(result)
            If newASIM Is Nothing Then
                If m_ShowErrors = False Then Return Nothing
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            result.Init(newASIM)
        Else
            Throw New InternalException(result)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ArraySizeInitializationModifier  ::= "("  BoundList  ")"  [  ArrayTypeModifiers  ]
    ''' LAMESPEC this might be correct? REMOVED, CURRENTLY USING ^ SPEC!
    ''' ArraySizeInitializationModifier  ::= "("  [ BoundList]  ")"  [  ArrayTypeModifiers  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArraySizeInitializationModifer(ByVal Parent As ParsedObject) As ArraySizeInitializationModifier
        Dim result As New ArraySizeInitializationModifier(Parent)

        Dim m_BoundList As BoundList = Nothing
        Dim m_ArrayTypeModifiers As ArrayTypeModifiers = Nothing

        tm.AcceptIfNotInternalError(KS.LParenthesis)

        m_BoundList = ParseBoundList(result)
        If m_BoundList Is Nothing Then
            If m_ShowErrors = False Then Return Nothing
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If vbnc.ArrayTypeModifiers.CanBeMe(tm) Then
            m_ArrayTypeModifiers = ParseArrayTypeModifiers(result)
            If m_ArrayTypeModifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_BoundList, m_ArrayTypeModifiers)

        Return result
    End Function

    ''' <summary>
    ''' InterfaceBase   ::= Inherits  InterfaceBases  StatementTerminator
    ''' InterfaceBases  ::= NonArrayTypeName  | InterfaceBases  ","  NonArrayTypeName
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInterfaceBases(ByVal Parent As ParsedObject) As InterfaceBases
        Dim result As New InterfaceBases(Parent)
        Dim tmp As New Generic.List(Of NonArrayTypeName)

        Do While tm.Accept(KS.Inherits)
            Do
                Dim newBase As NonArrayTypeName
                newBase = ParseNonArrayTypeName(result)
                tmp.Add(newBase)
            Loop While tm.Accept(KS.Comma)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Loop

        If tmp.Count <= 0 Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(tmp.ToArray)

        Return result
    End Function

    ''' <summary>
    ''' TypeImplementsClause  ::=  "Implements" Implements StatementTerminator
    ''' Implements  ::=	NonArrayTypeName  |	Implements  ","  NonArrayTypeName
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeImplementsClauses(ByVal Parent As ParsedObject) As TypeImplementsClauses
        Dim result As New TypeImplementsClauses(Parent)

        Dim m_Clauses As New Generic.List(Of NonArrayTypeName)

        Do While tm.Accept(KS.Implements)
            Do
                Dim newI As NonArrayTypeName
                newI = ParseNonArrayTypeName(result)
                If newI Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                m_Clauses.Add(newI)
            Loop While tm.Accept(KS.Comma)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Loop

        result.Init(m_Clauses)

        Return result
    End Function

    ''' <summary>
    ''' BoundList::= Expression | "0" "To" Expression  | UpperBoundList ,  Expression
    ''' UpperBoundList::= Expression | UpperBoundList , Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseBoundList(ByVal Parent As ParsedObject) As BoundList
        Dim result As New BoundList(Parent)

        Dim newExp As Expression = Nothing
        Dim tmp As New Generic.List(Of Expression)

        Do
            If tm.CurrentToken.IsIntegerLiteral AndAlso tm.CurrentToken.IntegralLiteral = 0 AndAlso tm.PeekToken.Equals(KS.To) Then tm.NextToken(2)
            newExp = ParseExpression(result)
            If newExp Is Nothing Then
                If m_ShowErrors = False Then Return Nothing
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            tmp.Add(newExp)
        Loop While tm.Accept(KS.Comma)

        result.Init(tmp.ToArray)

        Return result
    End Function

    ''' <summary>
    ''' NonArrayTypeName  ::= SimpleTypeName  |	ConstructedTypeName
    ''' SimpleTypeName    ::= QualifiedIdentifier  |	*BuiltInTypeName*
    ''' BuiltInTypeName   ::= "Object"  |  *PrimitiveTypeName*
    ''' PrimitiveTypeName      ::=  *NumericTypeName*  |  "Boolean" |  "Date"  |  "Char"  |  "String"
    ''' NumericTypeName        ::=  *IntegralTypeName*  |  *FloatingPointTypeName*  |  "Decimal"
    ''' IntegralTypeName       ::=  "Byte"  |  "SByte"  |  "UShort"  |  "Short"  |  "UInteger"  |  "Integer"  |  "ULong"  |  "Long"
    ''' FloatingPointTypeName  ::=  "Single"  |  "Double"
    ''' ConstructedTypeName    ::=  QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseNonArrayTypeName(ByVal Parent As ParsedObject) As NonArrayTypeName
        Dim result As New NonArrayTypeName(Parent)

        Dim m_SimpleTypeName As SimpleTypeName
        Dim m_ConstructedTypeName As ConstructedTypeName

        m_SimpleTypeName = ParseSimpleTypeName(result)

        If m_SimpleTypeName Is Nothing Then Return Nothing

        If m_SimpleTypeName.IsQualifiedIdentifier AndAlso tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of Then
            Dim m_TypeArgumentList As TypeArgumentList
            Dim m_Q As QualifiedIdentifier
            Dim m_NestedTypeName As ConstructedTypeName

            m_TypeArgumentList = ParseTypeArgumentList(result)
            If m_TypeArgumentList Is Nothing Then Return Nothing
            m_ConstructedTypeName = New ConstructedTypeName(result)
            m_ConstructedTypeName.Init(m_SimpleTypeName.AsQualifiedIdentifier, m_TypeArgumentList)

            Do While tm.Accept(KS.Dot)
                m_Q = ParseQualifiedIdentifier(result)
                m_TypeArgumentList = Nothing

                m_NestedTypeName = New ConstructedTypeName(m_ConstructedTypeName)

                If tm.Accept(KS.LParenthesis) Then
                    tm.AcceptIfNotError(KS.Of)

                    m_TypeArgumentList = New TypeArgumentList(m_NestedTypeName)
                    If ParseList(Of TypeName)(m_TypeArgumentList, New ParseDelegate_Parent(Of TypeName)(AddressOf ParseTypeName), Parent) = False Then
                        Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                    End If

                    tm.AcceptIfNotError(KS.RParenthesis)
                End If

                m_NestedTypeName.Init(m_ConstructedTypeName, m_Q, m_TypeArgumentList)

                m_ConstructedTypeName = m_NestedTypeName
            Loop

            result.Init(m_ConstructedTypeName)
        Else
            result.Init(m_SimpleTypeName)
        End If

        If tm.Accept(KS.Interrogation) Then
            result.IsNullable = True
        End If

        Return result
    End Function

    ''' <summary>
    ''' ConstructedTypeName ::=	
    '''     QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    '''     ConstructedTypeName "." QualifiedIdentifier [LAMESPEC]
    '''     ConstructedTypeName "." QualifiedIdentifier "(" "Of" TypeArgumentList ")" [LAMESPEC]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConstructedTypeName(ByVal Parent As ParsedObject) As ConstructedTypeName
        Dim result As ConstructedTypeName
        Dim current As ConstructedTypeName

        Dim m_QualifiedIdentifier As QualifiedIdentifier
        Dim m_TypeArgumentList As TypeArgumentList

        current = Nothing

        Do

            If current Is Nothing Then
                result = New ConstructedTypeName(Parent)
            Else
                result = New ConstructedTypeName(current)
            End If

            m_QualifiedIdentifier = ParseQualifiedIdentifier(result)
            If m_QualifiedIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            m_TypeArgumentList = Nothing

            If tm.Accept(KS.LParenthesis) Then
                tm.AcceptIfNotError(KS.Of)

                If ParseList(Of TypeName)(m_TypeArgumentList, New ParseDelegate_Parent(Of TypeName)(AddressOf ParseTypeName), Parent) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If

                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            result.Init(current, m_QualifiedIdentifier, m_TypeArgumentList)

            current = result
        Loop While tm.Accept(KS.Dot)

        Return result
    End Function


    ''' <summary>
    ''' TypeArgumentList ::=	"("  "Of"  TypeArgumentList  ")"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeArgumentList(ByVal Parent As ParsedObject) As TypeArgumentList
        Dim result As New TypeArgumentList(Parent)

        tm.AcceptIfNotInternalError(KS.LParenthesis)
        tm.AcceptIfNotInternalError(KS.Of)

        If ParseList(Of TypeName)(result, New ParseDelegate_Parent(Of TypeName)(AddressOf ParseTypeName), Parent) = False Then
            Return Nothing
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function


    ''' <summary>
    ''' TypeName ::= ArrayTypeName | NonArrayTypeName
    ''' 
    ''' ArrayTypeName          ::=  NonArrayTypeName  ArrayTypeModifiers
    ''' ArrayTypeModifiers     ::=  ArrayTypeModifier+
    ''' ArrayTypeModifier      ::=  "("  [  RankList  ]  ")"
    ''' RankList               ::=  ","  | RankList
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeName(ByVal Parent As ParsedObject) As TypeName
        Dim result As New TypeName(Parent)

        Dim m_NonArrayTypeName As NonArrayTypeName
        Dim m_ArrayTypeModifiers As ArrayTypeModifiers
        Dim m_ArrayTypeName As ArrayTypeName

        m_NonArrayTypeName = ParseNonArrayTypeName(result)

        If m_NonArrayTypeName Is Nothing Then Return Nothing

        If ArrayTypeName.CanBeArrayTypeModifier(tm) Then
            m_ArrayTypeName = New ArrayTypeName(Parent)

            m_ArrayTypeModifiers = ParseArrayTypeModifiers(m_ArrayTypeName)
            If m_ArrayTypeModifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            m_NonArrayTypeName.Parent = m_ArrayTypeName

            m_ArrayTypeName.Init(m_NonArrayTypeName, m_ArrayTypeModifiers)

            result.Init(m_ArrayTypeName)
        Else
            result.Init(m_NonArrayTypeName)
        End If


        Return result
    End Function

    ''' <summary>
    ''' ArrayTypeModifiers  ::=  ArrayTypeModifier+
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArrayTypeModifiers(ByVal Parent As ParsedObject) As ArrayTypeModifiers
        Dim result As New ArrayTypeModifiers(Parent)

        Dim tmp As New Generic.List(Of ArrayTypeModifier)
        Do
            Dim newATM As ArrayTypeModifier
            newATM = ParseArrayTypeModifier(result)
            If newATM Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            tmp.Add(newATM)
        Loop While ArrayTypeModifier.CanBeMe(tm)

        result.Init(tmp.ToArray)

        Return result
    End Function

    ''' <summary>
    ''' ArrayTypeModifier  ::=  "("  [  RankList  ]  ")"
    ''' RankList  ::= ","  | RankList  ","
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArrayTypeModifier(ByVal Parent As ParsedObject) As ArrayTypeModifier
        Dim result As New ArrayTypeModifier(Parent)

        tm.AcceptIfNotInternalError(KS.LParenthesis)

        Dim m_Ranks As Integer
        Do
            m_Ranks += 1
        Loop While tm.Accept(KS.Comma)

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Ranks)

        Return result
    End Function

    ''' <summary>
    ''' SimpleTypeName ::= QualifiedIdentifier | BuiltInTypeName
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSimpleTypeName(ByVal Parent As ParsedObject) As SimpleTypeName
        Dim result As New SimpleTypeName(Parent)

        If BuiltInTypeName.IsBuiltInTypeName(tm) Then
            Dim m_BuiltInTypeName As BuiltInTypeName
            m_BuiltInTypeName = ParseBuiltinTypeName(result)
            If m_BuiltInTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            result.Init(m_BuiltInTypeName)
        Else
            Dim m_QualifiedIdentifier As QualifiedIdentifier

            If QualifiedIdentifier.CanBeQualifiedIdentifier(tm) = False Then
                If tm.CurrentToken.IsKeyword Then
                    Compiler.Report.ShowMessage(Messages.VBNC30180, tm.CurrentLocation)
                    tm.NextToken()
                End If
                Return Nothing
            End If

            m_QualifiedIdentifier = ParseQualifiedIdentifier(result)
            If m_QualifiedIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            result.Init(m_QualifiedIdentifier)
        End If

        Return result
    End Function

    Private Shared Function ParseQualifiedIdentifier(ByVal Parent As ParsedObject, ByVal str As String) As QualifiedIdentifier
        Dim result As New QualifiedIdentifier(Parent)

        Dim m_First As ParsedObject
        Dim m_Second As Token = Nothing

        Dim first As String
        Dim second As String = Nothing
        Dim isplit As Integer = str.LastIndexOf("."c)

        If isplit >= 0 Then
            first = str.Substring(0, isplit)
            second = str.Substring(isplit + 1)
        Else
            first = str
        End If

        If first.Contains("."c) Then
            m_First = ParseQualifiedIdentifier(result, first)
            If m_First Is Nothing Then Helper.ErrorRecoveryNotImplemented(Parent.Location)
        ElseIf first.Length > 7 AndAlso Helper.CompareName(first.Substring(1, 7), "Global.") Then
            m_First = New GlobalExpression(result)
        Else
            m_First = New Identifier(Parent, first, Parent.Location, TypeCharacters.Characters.None)
        End If

        If second IsNot Nothing Then
            m_Second = Token.CreateIdentifierToken(Span.CommandLineSpan, second)
        End If

        result.Init(m_First, m_Second)

        Return result
    End Function

    ''' <summary>
    ''' QualifiedIdentifier ::= Identifier | "Global" "." IdentifierOrKeyword | QualifiedIdentifier "." IdentifierOrKeyword
    ''' 
    ''' Call if CurrentToken is identifier or "Global".
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseQualifiedIdentifier(ByVal Parent As ParsedObject) As QualifiedIdentifier
        Dim result As New QualifiedIdentifier(Parent)

        Helper.Assert(vbnc.QualifiedIdentifier.CanBeQualifiedIdentifier(tm))

        Dim m_First As ParsedObject
        Dim m_Second As Token = Nothing

        If tm.CurrentToken.IsIdentifier Then
            m_First = ParseIdentifier(result)
        ElseIf tm.CurrentToken.Equals(KS.Global) Then
            m_First = ParseGlobalExpression(result)
            If tm.CurrentToken <> KS.Dot Then Return Nothing
        Else
            Throw New InternalException(result)
        End If

        While tm.Accept(KS.Dot)
            tm.AcceptNewLine()
            If Token.IsSomething(m_Second) Then m_First = New QualifiedIdentifier(Parent, m_First, m_Second)
            If tm.CurrentToken.IsIdentifierOrKeyword Then
                m_Second = tm.CurrentToken
                tm.NextToken()
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation)
                Return Nothing
            End If
        End While

        result.Init(m_First, m_Second)

        Return result
    End Function

    Private Function ParseIdentifier() As Identifier
        Return ParseIdentifier(CType(Nothing, ParsedObject))
    End Function

    Private Function ParseIdentifier(ByVal Parent As ParsedObject) As Identifier
        Dim result As Identifier

        If tm.CurrentToken.IsIdentifier Then
            result = New Identifier(Parent, tm.CurrentToken.Identifier, tm.CurrentLocation, tm.CurrentTypeCharacter)
            tm.NextToken()
        Else
            result = Nothing
        End If

        Return result
    End Function

    Private Function ParseBuiltinTypeName(ByVal Parent As ParsedObject) As BuiltInTypeName
        Dim m_Typename As KS

        If vbnc.BuiltInTypeName.IsBuiltInTypeName(tm) = False Then Throw New InternalException(Parent)

        m_Typename = tm.CurrentToken.Keyword
        tm.NextToken()

        Return New BuiltInTypeName(Parent, m_Typename)
    End Function

    Private Function ParseModifiers(ByVal ValidModifiers As ModifierMasks) As Modifiers
        Dim result As New Modifiers()

        While tm.CurrentToken.Equals(ValidModifiers)
            result.AddModifier(tm.CurrentToken.Keyword)
            tm.NextToken()
        End While

        Return result
    End Function

    ''' <summary>
    ''' Parses type members for interfaces.
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseInterfaceMembers(ByVal Parent As InterfaceDeclaration) As Boolean
        Dim newMembers As New Generic.List(Of IMember)
        While True
            Dim attributes As Attributes = Nothing

            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            Dim newType As TypeDeclaration
            newType = ParseTypeDeclaration(Parent, attributes, Parent.Namespace)
            If newType IsNot Nothing AndAlso Not Parent.Members.Contains(newType) Then
                Parent.Members.Add(newType)
                Continue While
            End If

            Dim newMember As IMember
            'InterfaceDeclarations
            If InterfaceEventMemberDeclaration.IsMe(tm) Then
                newMember = ParseInterfaceEventMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
            ElseIf InterfaceFunctionDeclaration.IsMe(tm) Then
                newMember = ParseInterfaceFunctionDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
            ElseIf InterfaceSubDeclaration.IsMe(tm) Then
                newMember = ParseInterfaceSubDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
            ElseIf InterfacePropertyMemberDeclaration.IsMe(tm) Then
                newMember = ParseInterfacePropertyMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
            Else
                If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If

            If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            Parent.Members.Add(newMember)
        End While

        Return True
    End Function

    ''' <summary>
    ''' Parses type members for classes, modules and structures.
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <remarks></remarks>
    Private Function ParseTypeMembers(ByVal Parent As TypeDeclaration) As Boolean
        Dim result As Boolean = True
        Dim isModuleDeclaration As Boolean = TypeOf Parent Is ModuleDeclaration

        Helper.Assert(TypeOf Parent Is ClassDeclaration OrElse isModuleDeclaration OrElse TypeOf Parent Is StructureDeclaration)

        Dim newMembers As New Generic.List(Of IMember)
        While True
            Dim attributes As Attributes = Nothing

            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            Dim newType As TypeDeclaration
            newType = ParseTypeDeclaration(Parent, attributes, Parent.Namespace)
            If newType IsNot Nothing Then
                Parent.Members.Add(newType)
                Continue While
            End If

            Dim newMember As IMember

            'Class and Structure declarations
            If isModuleDeclaration = False AndAlso OperatorDeclaration.IsMe(tm) Then
                newMember = ParseOperatorDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf isModuleDeclaration = False AndAlso ConversionOperatorDeclaration.IsMe(tm) Then
                newMember = ParseConversionOperatorDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                'Class, Structure and Module declarations
            ElseIf RegularEventDeclaration.IsMe(tm) Then
                newMember = ParseRegularEventDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf CustomEventDeclaration.IsMe(tm) Then
                newMember = ParseCustomEventMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf VariableDeclaration.IsMe(tm) Then
                Dim tmp As Generic.List(Of TypeVariableDeclaration)
                tmp = ParseTypeVariableMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If tmp Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                For Each item As TypeVariableDeclaration In tmp
                    newMembers.Add(item)
                Next
                newMember = Nothing
            ElseIf ConstantDeclaration.IsMe(tm) Then
                Dim tmp As Generic.List(Of ConstantDeclaration)
                tmp = ParseConstantMemberDeclarations(Parent, New ParseAttributableInfo(Compiler, attributes))
                If tmp Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                For Each item As ConstantDeclaration In tmp
                    newMembers.Add(item)
                Next
                newMember = Nothing
            ElseIf ExternalSubDeclaration.IsMe(tm) Then
                newMember = ParseExternalSubDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf ExternalFunctionDeclaration.IsMe(tm) Then
                newMember = ParseExternalFunctionDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf SubDeclaration.IsMe(tm) Then
                newMember = ParseSubDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf FunctionDeclaration.IsMe(tm) Then
                newMember = ParseFunctionDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf RegularPropertyDeclaration.IsMe(tm) Then
                newMember = ParseRegularPropertyMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf MustOverridePropertyDeclaration.IsMe(tm) Then
                newMember = ParseMustOverridePropertyMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf ConstructorDeclaration.IsMe(tm) Then
                newMember = ParseConstructorMember(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If

            If newMember IsNot Nothing Then newMembers.Add(newMember)
            If newMembers.Count = 0 Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Parent.Members.AddRange(newMembers)
            newMembers.Clear()
        End While

        Return result
    End Function

    ''' <summary>
    ''' Parses a type declaration. Returns nothing if no type declaration was found.
    ''' Parses only one typedeclaration.
    ''' Type declaration = Class, Module, Structure, Enum, Delegate, Interface declaration.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="Namespace"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseTypeDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As TypeDeclaration
        Dim result As TypeDeclaration
        If ClassDeclaration.IsMe(tm) Then
            result = ParseClassDeclaration(Parent, Attributes, [Namespace])
        ElseIf EnumDeclaration.IsMe(tm) Then
            result = ParseEnumDeclaration(Parent, Attributes, [Namespace])
        ElseIf StructureDeclaration.IsMe(tm) Then
            result = ParseStructureDeclaration(Parent, Attributes, [Namespace])
        ElseIf InterfaceDeclaration.IsMe(tm) Then
            result = ParseInterfaceDeclaration(Parent, Attributes, [Namespace])
        ElseIf DelegateDeclaration.IsMe(tm) Then
            result = ParseDelegateDeclaration(Parent, Attributes, [Namespace])
        ElseIf ModuleDeclaration.IsMe(tm) Then
            result = ParseModuleDeclaration(Parent, Attributes, [Namespace])
        Else
            result = Nothing
        End If
        Return result
    End Function

    Private Function ParseAssemblyMembers(ByVal Parent As AssemblyDeclaration, ByVal RootNamespace As String) As Boolean
        Dim result As Boolean = True
        Dim currentNameSpace As String = RootNamespace
        Dim currentNamespaces As New Generic.List(Of QualifiedIdentifier)

        While True
            Dim attributes As Attributes = Nothing

            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

                If tm.AcceptEndOfStatement Then
                    For Each attrib As Attribute In attributes
                        If attrib.IsAssembly = False AndAlso attrib.IsModule = False Then
                            If attrib.Location.File(Compiler).DoesLineEndWithLineContinuation(attrib.Location.Line) Then
                                result = Compiler.Report.ShowMessage(Messages.VBNC30203, attrib.Location) AndAlso result
                            Else
                                result = Compiler.Report.ShowMessage(Messages.VBNC32035, attrib.Location) AndAlso result
                            End If
                        End If
                    Next
                    Parent.Attributes.AddRange(attributes)
                    Continue While
                End If
            End If

            Dim newType As TypeDeclaration
            newType = ParseTypeDeclaration(Parent, attributes, currentNameSpace)
            If newType IsNot Nothing Then
                If Not Parent.Members.Contains(newType) Then
                    'This may be false for partial types
                    Parent.Members.Add(newType)
                End If
            ElseIf tm.Accept(KS.Namespace) Then
                Dim qi As QualifiedIdentifier
                qi = ParseQualifiedIdentifier(Parent)
                If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                currentNamespaces.Add(qi)
                currentNameSpace = RootNamespace
                If currentNamespaces.Count > 0 Then
                    If currentNameSpace <> "" Then currentNameSpace &= "."
                    For i As Integer = 0 To currentNamespaces.Count - 2
                        currentNameSpace &= currentNamespaces(i).Name & "."
                    Next
                    currentNameSpace &= currentNamespaces(currentNamespaces.Count - 1).Name
                End If
                If tm.AcceptNewLine(True, True, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            ElseIf tm.Accept(KS.End, KS.Namespace) Then
                If tm.AcceptNewLine(True, False, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                If currentNamespaces.Count >= 1 Then
                    currentNamespaces.RemoveAt(currentNamespaces.Count - 1)
                    currentNameSpace = RootNamespace
                    If currentNamespaces.Count > 0 Then
                        If currentNameSpace <> "" Then currentNameSpace &= "."
                        For i As Integer = 0 To currentNamespaces.Count - 2
                            currentNameSpace &= currentNamespaces(i).Name & "."
                        Next
                        currentNameSpace &= currentNamespaces(currentNamespaces.Count - 1).Name
                    End If
                Else
                    result = Compiler.Report.ShowMessage(Messages.VBNC30623, tm.CurrentLocation)
                End If
            Else
                If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If
        End While
        Return result
    End Function


    ''' <summary>
    ''' EventAccessorDeclaration  ::= AddHandlerDeclaration | RemoveHandlerDeclaration | RaiseEventDeclaration
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEventAccessorDeclarations(ByVal Parent As EventDeclaration, ByVal EventName As Identifier, ByVal EventModifiers As Modifiers) As EventAccessorDeclarations
        Dim result As New EventAccessorDeclarations(Parent)
        Dim parsing As Boolean = True

        Dim m_AddHandler As CustomEventHandlerDeclaration = Nothing
        Dim m_RemoveHandler As CustomEventHandlerDeclaration = Nothing
        Dim m_RaiseEvent As CustomEventHandlerDeclaration = Nothing

        Do
            Dim attributes As Attributes = Nothing
            If vbnc.Attributes.IsMe(tm) Then
                ParseAttributes(result, attributes)
            End If
            If CustomEventHandlerDeclaration.IsMe(tm) Then
                Dim newMember As CustomEventHandlerDeclaration
                newMember = ParseCustomEventHandlerDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes), EventName, EventModifiers)
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                Select Case newMember.HandlerType
                    Case KS.AddHandler
                        m_AddHandler = newMember
                    Case KS.RemoveHandler
                        m_RemoveHandler = newMember
                    Case KS.RaiseEvent
                        m_RaiseEvent = newMember
                    Case Else
                        Throw New InternalException(result)
                End Select
            Else
                If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation)
                End If
                Exit Do
            End If
        Loop

        result.Init(m_AddHandler, m_RemoveHandler, m_RaiseEvent)

        result.HasErrors = m_AddHandler IsNot Nothing AndAlso m_RemoveHandler IsNot Nothing AndAlso m_RaiseEvent IsNot Nothing

        Return result
    End Function

    ''' <summary>
    ''' CustomEventMemberDeclaration  ::=
    '''	[  Attributes  ]  [  EventModifiers+  ]  "Custom" "Event" Identifier "As" TypeName  [  ImplementsClause  ]
    '''		StatementTerminator
    '''		EventAccessorDeclaration+
    '''	"End" "Event" StatementTerminator
    ''' 
    ''' LAMESPEC!!! Using the following:
    ''' CustomEventMemberDeclaration  ::=
    '''	[  Attributes  ]  [  EventModifiers+  ]  "Custom" "Event" Identifier "As" NonArrayTypeName  [  ImplementsClause  ]
    '''		StatementTerminator
    '''		EventAccessorDeclaration+
    '''	"End" "Event" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseCustomEventMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As CustomEventDeclaration
        Dim result As New CustomEventDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_TypeName As NonArrayTypeName
        Dim m_ImplementsClause As MemberImplementsClause = Nothing
        Dim m_EventAccessorDeclarations As EventAccessorDeclarations = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.EventModifiers)

        tm.AcceptIfNotInternalError("Custom")
        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.As) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_TypeName = ParseNonArrayTypeName(result)
        If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement = False Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        m_EventAccessorDeclarations = ParseEventAccessorDeclarations(result, m_Identifier, m_Modifiers)
        If m_EventAccessorDeclarations Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Event) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Identifier, m_TypeName, m_ImplementsClause)

        result.AddMethod = m_EventAccessorDeclarations.AddHandler
        result.RemoveMethod = m_EventAccessorDeclarations.RemoveHandler
        result.RaiseMethod = m_EventAccessorDeclarations.RaiseEvent

        Return result
    End Function

    ''' <summary>
    ''' InterfaceEventMemberDeclaration  ::=
    '''	[  Attributes  ]  [  InterfaceEventModifiers+  ]  "Event"  Identifier  ParametersOrType  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInterfaceEventMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As InterfaceEventMemberDeclaration
        Dim result As New InterfaceEventMemberDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_ParametersOrType As ParametersOrType


        m_Modifiers = ParseModifiers(ModifierMasks.InterfaceEventModifiers)

        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_ParametersOrType = ParseParametersOrType(result)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Identifier, m_ParametersOrType, Nothing)

        Return result
    End Function

    ''' <summary>
    ''' ParametersOrType  ::= [  "(" [  ParameterList  ]  ")"  ]  | "As"  NonArrayTypeName
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseParametersOrType(ByVal Parent As ParsedObject) As ParametersOrType
        Dim result As New ParametersOrType(Parent)

        Dim m_NonArrayTypeName As NonArrayTypeName
        Dim m_ParameterList As ParameterList

        If tm.Accept(KS.As) Then
            m_NonArrayTypeName = ParseNonArrayTypeName(result)
            result.Init(m_NonArrayTypeName)
        Else
            m_ParameterList = New ParameterList(result)
            If tm.Accept(KS.LParenthesis) Then
                If tm.Accept(KS.RParenthesis) = False Then
                    If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                        Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                    End If
                    If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                Else
                    m_ParameterList = New ParameterList(result)
                End If
                result.Init(m_ParameterList)
            Else
                result.Init(m_ParameterList)
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' RegularEventMemberDeclaration  ::=
    ''' 	[  Attributes  ]  [  EventModifiers+  ]  "Event"  Identifier  ParametersOrType  [  ImplementsClause  ] StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseRegularEventDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As RegularEventDeclaration
        Dim result As New RegularEventDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_ParametersOrType As ParametersOrType
        Dim m_ImplementsClause As MemberImplementsClause

        m_Modifiers = ParseModifiers(ModifierMasks.EventModifiers)

        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_ParametersOrType = ParseParametersOrType(result)

        If MemberImplementsClause.IsMe(tm) Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_ImplementsClause = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Identifier, m_ParametersOrType, m_ImplementsClause)

        Return result
    End Function

    ''' <summary>
    ''' AddHandlerDeclaration  ::=
    '''	[  Attributes  ]  "AddHandler" "(" ParameterList ")" LineTerminator
    '''	[  Block  ]
    '''	"End" "AddHandler" StatementTerminator
    ''' 
    ''' RemoveHandlerDeclaration  ::=
    '''	[  Attributes  ]  "RemoveHandler" "("  ParameterList  ")"  LineTerminator
    '''	[  Block  ]
    '''	"End" "RemoveHandler" StatementTerminator
    ''' 
    ''' LAMESPEC: should be:
    ''' RemoveHandlerDeclaration  ::=
    '''	[  Attributes  ]  "RemoveHandler" "("  [ ParameterList  ] ")"  LineTerminator
    '''	[  Block  ]
    '''	"End" "RemoveHandler" StatementTerminator
    ''' 
    ''' RaiseEventDeclaration  ::=
    '''	[  Attributes  ]  "RaiseEvent" (  ParameterList  )  LineTerminator
    '''	[  Block  ]
    '''	"End" "RaiseEvent" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseCustomEventHandlerDeclaration(ByVal Parent As EventDeclaration, ByVal Info As ParseAttributableInfo, ByVal EventName As Identifier, ByVal EventModifiers As Modifiers) As CustomEventHandlerDeclaration
        Dim result As New CustomEventHandlerDeclaration(Parent)

        Dim m_ParameterList As New ParameterList(result)
        Dim m_Block As CodeBlock
        Dim m_HandlerType As KS
        Dim m_Modifiers As Modifiers

        If tm.CurrentToken.Equals(KS.AddHandler, KS.RemoveHandler, KS.RaiseEvent) Then
            m_HandlerType = tm.CurrentToken.Keyword
            tm.NextToken()
        Else
            Throw New InternalException(result)
        End If

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.RParenthesis) = False Then
            If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, m_HandlerType) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If m_ParameterList Is Nothing Then m_ParameterList = New ParameterList(result)

        If m_HandlerType = KS.RaiseEvent Then
            m_Modifiers = New Modifiers(ModifierMasks.Private)
        Else
            m_Modifiers = EventModifiers
        End If


        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_ParameterList, m_Block, m_HandlerType, EventName)

        Return result
    End Function


    Private Function ParseGlobalExpression(ByVal Parent As ParsedObject) As GlobalExpression
        tm.AcceptIfNotInternalError(KS.Global)
        Return New GlobalExpression(Parent)
    End Function

    Private Function ParseBuiltInTypeExpression(ByVal Parent As ParsedObject) As BuiltInTypeExpression
        Dim result As New BuiltInTypeExpression(Parent)

        Dim m_Type As BuiltInDataTypes

        Helper.Assert(tm.CurrentToken.Equals(Enums.BuiltInTypeTypeNames))

        m_Type = CType(tm.CurrentToken.Keyword, BuiltInDataTypes)
        tm.NextToken()

        result.Init(m_Type)

        Return result
    End Function

    ''' <summary>
    ''' MemberAccessExpression ::= [ [ MemberAccessBase ] "." ] IdentifierOrKeyword
    ''' MemberAccessBase ::= Expression | BuiltInTypeName | "Global" | "MyClass" | "MyBase"
    ''' 
    ''' TODO: Is this correct? Is the "." optional in a MemberAccessExpression?
    ''' LAMESPEC: IdentifierOrKeyword should be followed by type parameters...
    ''' MemberAccessExpression ::= [ [ MemberAccessBase ] "." ] IdentifierOrKeyword [ TypeParametersList ]
    ''' </summary>
    ''' <param name="FirstExpression"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseMemberAccessExpression(ByVal Parent As ParsedObject, ByVal FirstExpression As Expression) As MemberAccessExpression
        Dim result As New MemberAccessExpression(Parent)

        Dim m_First As Expression
        Dim m_Second As IdentifierOrKeywordWithTypeArguments

        m_First = FirstExpression 'Might be nothing.
        If m_First IsNot Nothing Then m_First.Parent = result
        'According to the language specification, the dot is optional,
        'but that doesn't seem to be correct... so let's make it 
        'required
        tm.AcceptIfNotInternalError(KS.Dot)
        'Specifically, this is not a MemberAccessExpression without the
        'dot, so it is an internal error.
        tm.AcceptNewLine()
        m_Second = ParseIdentifierOrKeywordWithTypeArguments(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_First, m_Second)

        Return result
    End Function

    Private Function ParseIdentifierOrKeywordWithTypeArguments(ByVal Parent As ParsedObject) As IdentifierOrKeywordWithTypeArguments
        Dim result As New IdentifierOrKeywordWithTypeArguments(Parent)

        Dim m_TypeArguments As TypeArgumentList
        Dim m_Token As Token

        If tm.CurrentToken.IsIdentifierOrKeyword Then
            m_Token = tm.CurrentToken
            tm.NextToken()
        Else
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Return Nothing
        End If

        If tm.CurrentToken.Equals(KS.LParenthesis) AndAlso tm.PeekToken.Equals(KS.Of) Then
            m_TypeArguments = ParseTypeArgumentList(result)
            If m_TypeArguments Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeArguments = Nothing
        End If

        result.Init(m_Token, m_TypeArguments)

        Return result
    End Function

    ''' <summary>
    ''' DictionaryAccessExpression ::= [Expression] "!" IdentifierOrKeyword
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseDictionaryAccessExpression(ByVal Parent As ParsedObject, ByVal FirstPart As Expression) As DictionaryAccessExpression
        Dim result As New DictionaryAccessExpression(Parent)

        Dim m_FirstPart As Expression
        Dim m_SecondPart As IdentifierOrKeyword

        m_FirstPart = FirstPart
        If m_FirstPart IsNot Nothing Then m_FirstPart.Parent = result
        tm.AcceptIfNotInternalError(KS.Exclamation)
        If tm.CurrentToken.IsIdentifierOrKeyword Then
            m_SecondPart = ParseIdentifierOrKeyword(result)
            If m_SecondPart Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation)
            Return Nothing
        End If

        result.Init(m_FirstPart, m_SecondPart)

        Return result
    End Function

    Private Function ParseCByteExpression(ByVal Parent As ParsedObject) As CByteExpression
        Dim result As New CByteExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CByte)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCBoolExpression(ByVal Parent As ParsedObject) As CBoolExpression
        Dim result As New CBoolExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CBool)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCCharExpression(ByVal Parent As ParsedObject) As CCharExpression
        Dim result As New CCharExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CChar)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDateExpression(ByVal Parent As ParsedObject) As CDateExpression
        Dim result As New CDateExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDate)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDblExpression(ByVal Parent As ParsedObject) As CDblExpression
        Dim result As New CDblExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDbl)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDecExpression(ByVal Parent As ParsedObject) As CDecExpression
        Dim result As New CDecExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDec)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCIntExpression(ByVal Parent As ParsedObject) As CIntExpression
        Dim result As New CIntExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CInt)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCLngExpression(ByVal Parent As ParsedObject) As CLngExpression
        Dim result As New CLngExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CLng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCObjExpression(ByVal Parent As ParsedObject) As CObjExpression
        Dim result As New CObjExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CObj)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCSByteExpression(ByVal Parent As ParsedObject) As CSByteExpression
        Dim result As New CSByteExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CSByte)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCShortExpression(ByVal Parent As ParsedObject) As CShortExpression
        Dim result As New CShortExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CShort)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCSngExpression(ByVal Parent As ParsedObject) As CSngExpression
        Dim result As New CSngExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CSng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCStrExpression(ByVal Parent As ParsedObject) As CStrExpression
        Dim result As New CStrExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CStr)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCUIntExpression(ByVal Parent As ParsedObject) As CUIntExpression
        Dim result As New CUIntExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CUInt)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCULngExpression(ByVal Parent As ParsedObject) As CULngExpression
        Dim result As New CULngExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CULng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCUShortExpression(ByVal Parent As ParsedObject) As CUShortExpression
        Dim result As New CUShortExpression(Parent, True)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CUShort)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    ''' <summary>
    ''' VariableIdentifier  ::=  Identifier  [  ArrayNameModifier  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableIdentifier(ByVal Parent As ParsedObject) As VariableIdentifier
        Dim result As New VariableIdentifier(Parent)

        Dim m_Identifier As Identifier
        Dim m_ArrayNameModifier As ArrayNameModifier

        If tm.CurrentToken.IsIdentifier = False Then
            Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation)
            Return Nothing
        End If

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Interrogation) Then result.IsNullable = True

        If vbnc.ArrayNameModifier.CanBeMe(tm) Then
            m_ArrayNameModifier = ParseArrayNameModifier(result)
            If m_ArrayNameModifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_ArrayNameModifier = Nothing
        End If

        result.Init(m_Identifier, m_ArrayNameModifier)

        Return result
    End Function

    ''' <summary>
    ''' RaiseEventStatement  ::= "RaiseEvent" IdentifierOrKeyword [ "(" [ ArgumentList ] ")" ] StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseRaiseEventStatement(ByVal Parent As ParsedObject) As RaiseEventStatement
        Dim result As New RaiseEventStatement(Parent)

        Dim m_Event As SimpleNameExpression
        Dim m_Arguments As ArgumentList
        Dim m_Identifier As IdentifierOrKeyword

        tm.AcceptIfNotInternalError(KS.RaiseEvent)

        m_Identifier = ParseIdentifierOrKeyword(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Event = New SimpleNameExpression(result)
        m_Event.Identifier = New Identifier(m_Event, m_Identifier.Identifier, m_Identifier.Location, TypeCharacters.Characters.None)

        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                m_Arguments = ParseArgumentList(result)
                If m_Arguments Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                m_Arguments = New ArgumentList(result)
            End If
        Else
            m_Arguments = Nothing
        End If

        result.Init(m_Event, m_Arguments)

        Return result
    End Function

    ''' <summary>
    ''' InvocationExpression: Expression [ "(" [ ArgumentList ] ")" ]
    ''' IndexExpression:      Expression "(" [ ArgumentList ] ")"
    ''' Note that for the index expression the parenthesis are not optional.
    ''' This is reflected by the fact that m_ArgumentList is not nothing if 
    ''' parenthesis are provided.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInvocationOrIndexExpression(ByVal Parent As ParsedObject, ByVal First As Expression) As InvocationOrIndexExpression
        Dim result As New InvocationOrIndexExpression(Parent)

        Dim m_Expression As Expression
        Dim m_ArgumentList As ArgumentList

        m_Expression = First

        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                m_ArgumentList = ParseArgumentList(result)
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                m_ArgumentList = New ArgumentList(result)
            End If
        Else
            m_ArgumentList = Nothing
        End If

        result.Init(m_Expression, m_ArgumentList)

        Return result
    End Function

    ''' <summary>
    ''' ParenthesizedExpression  ::=  "("  Expression  ")"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseParenthesizedExpression(ByVal Parent As ParsedObject) As ParenthesizedExpression
        Dim result As New ParenthesizedExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.LParenthesis)

        m_Expression = ParseExpression(result)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        result.Init(m_Expression)

        Return result
    End Function


    Private Function ParseUnaryMinusExpression(ByVal Info As ExpressionParseInfo) As UnaryMinusExpression
        Dim result As New UnaryMinusExpression(Info.Parent)

        Dim m_Expression As Expression
        tm.AcceptIfNotInternalError(KS.Minus)

        m_Expression = ParseExponent(Info)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCTypeExpression(ByVal Parent As ParsedObject, ByVal GetKeyword As KS) As CTypeExpression
        Dim result As CTypeExpression = Nothing

        Dim m_DestinationType As TypeName
        Dim m_Expression As Expression

        Select Case GetKeyword
            Case KS.CType
                result = New CTypeExpression(Parent, True)
            Case KS.DirectCast
                result = New DirectCastExpression(Parent)
            Case KS.TryCast
                result = New TryCastExpression(Parent)
            Case Else
                Throw New InternalException(result)
        End Select

        tm.AcceptIfNotInternalError(GetKeyword)
        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        m_Expression = ParseExpression(result)
        If tm.AcceptIfNotError(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        m_DestinationType = ParseTypeName(result)
        If m_DestinationType Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)


        result.Init(m_Expression, m_DestinationType)

        Return result
    End Function

    Private Function ParseUnaryNotExpression(ByVal Info As ExpressionParseInfo) As UnaryNotExpression
        Dim result As New UnaryNotExpression(Info.Parent)

        Dim m_Expression As Expression
        tm.AcceptIfNotInternalError(KS.Not)

        m_Expression = ParseComparison(Info)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseUnaryPlusExpression(ByVal Info As ExpressionParseInfo) As UnaryPlusExpression
        Dim result As New UnaryPlusExpression(Info.Parent)
        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.Add)

        m_Expression = ParseExponent(Info)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseArrayInitializerExpression(ByVal Parent As ParsedObject) As ArrayInitializerExpression
        Dim result As New ArrayInitializerExpression(Parent)

        Dim m_Initializers As New Expressions()

        tm.AcceptIfNotInternalError(KS.LBrace)

        If tm.CurrentToken <> KS.RBrace Then
            Do
                Dim newExp As Expression
                newExp = ParseExpression(result)
                If newExp Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

                m_Initializers.Add(newExp)
            Loop While tm.Accept(KS.Comma)
        End If

        If tm.Accept(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Initializers)

        Return result
    End Function

    ''' <summary>
    ''' LoopControlVariable  ::=
    '''	   Identifier  [  ArrayNameModifier  ] "As" TypeName  |
    '''	   Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseLoopControlVariable(ByVal Parent As ParsedObject) As LoopControlVariable
        Dim result As New LoopControlVariable(Parent)

        Dim m_Identifier As Identifier
        Dim m_ArrayNameModifier As ArrayNameModifier = Nothing
        Dim m_TypeName As TypeName = Nothing
        Dim m_Expression As Expression = Nothing

        'First try first option
        Dim tmpANM As ArrayNameModifier = Nothing
        Dim iCurrent As RestorablePoint = tm.GetRestorablePoint
        Dim doExpression As Boolean = True
        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then
            Dim exp As Expression = ParseExpression(result)
            Dim constant As Object = Nothing
            If exp IsNot Nothing AndAlso exp.GetConstant(constant, False) Then
                Compiler.Report.ShowMessage(Messages.VBNC30074, tm.CurrentLocation)
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation)
            End If
        End If
        If m_Identifier IsNot Nothing Then
            If ArrayNameModifier.CanBeMe(tm) Then
                tmpANM = ParseArrayNameModifier(result)
                If tmpANM Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tmpANM Is Nothing AndAlso tm.Accept(KS.As) Then
                m_ArrayNameModifier = tmpANM
                m_TypeName = ParseTypeName(result)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                doExpression = False
            End If
        End If

        If doExpression Then
            tm.RestoreToPoint(iCurrent)
            m_Expression = ParseExpression(New ExpressionParseInfo(result, True))
            If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            tm.IgnoreRestoredPoint()
        End If

        result.Init(m_Identifier, m_ArrayNameModifier, m_TypeName, m_Expression)

        Return result
    End Function

    ''' <summary>
    ''' NewExpression ::= ObjectCreationExpression | ArrayCreationExpression | DelegateCreationExpression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseNewExpression(ByVal Parent As ParsedObject) As NewExpression
        Dim result As New NewExpression(Parent)
        Dim iCurrent As RestorablePoint = tm.GetRestorablePoint
        Dim bShowingErrors As Boolean

        Dim ace As ArrayCreationExpression

        bShowingErrors = Me.ShowErrors
        Me.m_ShowErrors = False
        ace = ParseArrayCreationExpression(result)
        Me.m_ShowErrors = bShowingErrors

        If ace IsNot Nothing Then
            tm.IgnoreRestoredPoint()
            result.Init(ace)
        Else
            tm.RestoreToPoint(iCurrent)
            Dim doce As DelegateOrObjectCreationExpression
            doce = ParseDelegateOrObjectCreationExpression(result)
            If doce Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(doce)
        End If

        Return result
    End Function

    ''' <summary>
    ''' DelegateCreationExpression ::= "New" NonArrayTypeName "(" Expression ")"
    ''' ObjectCreationExpression   ::= "New" NonArrayTypeName [ "(" [ ArgumentList ] ")" ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseDelegateOrObjectCreationExpression(ByVal Parent As ParsedObject) As DelegateOrObjectCreationExpression
        Dim result As New DelegateOrObjectCreationExpression(Parent)

        Dim m_NonArrayTypeName As NonArrayTypeName = Nothing
        Dim m_ArgumentList As ArgumentList = Nothing

        tm.AcceptIfNotInternalError(KS.[New])
        m_NonArrayTypeName = ParseNonArrayTypeName(result)

        If tm.Accept(KS.LParenthesis) Then
            If tm.CurrentToken <> KS.RParenthesis Then
                m_ArgumentList = ParseArgumentList(result)
                If m_ArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If
        If m_ArgumentList Is Nothing Then m_ArgumentList = New ArgumentList(result)

        result.Init(m_NonArrayTypeName, m_ArgumentList)

        Return result
    End Function

    ''' <summary>
    ''' ArgumentList  ::=	PositionalArgumentList  ,  NamedArgumentList  |
    '''                     PositionalArgumentList  |
    '''	                    NamedArgumentList
    ''' 
    ''' PositionalArgumentList  ::=  Expression  |  PositionalArgumentList  ","  [  Expression  ]
    ''' 
    ''' NamedArgumentList  ::=  IdentifierOrKeyword  ":="  Expression  |  NamedArgumentList  ,  IdentifierOrKeyword  :=  Expression
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseArgumentList(ByVal Parent As ParsedObject) As ArgumentList
        Dim result As New ArgumentList(Parent)

        Dim m_Arguments As New BaseObjects(Of Argument)(result)

        'First parse positional arguments
        Do
            'Check for named argument.
            If NamedArgument.CanBeMe(tm) Then Exit Do

            Dim exp As Expression
            exp = Nothing

            If tm.CurrentToken.Equals(KS.Comma) = False Then
                exp = ParseExpression(result)
            End If

            Dim newPA As PositionalArgument
            newPA = New PositionalArgument(result, m_Arguments.Count, exp)
            m_Arguments.Add(newPA)
        Loop While tm.Accept(KS.Comma)

        'Then parse named arguments
        If NamedArgument.CanBeMe(tm) Then
            Do
                Dim newArgument As NamedArgument
                newArgument = ParseNamedArgument(result)
                m_Arguments.Add(newArgument)
            Loop While tm.Accept(KS.Comma)
        End If

        result.Init(m_Arguments)

        Return result
    End Function

    Private Function ParseNamedArgument(ByVal Parent As ParsedObject) As NamedArgument
        Dim result As New NamedArgument(Parent)
        Dim Name As String
        Dim Expression As Expression = Nothing

        If tm.CurrentToken.IsIdentifier Then
            Name = tm.CurrentToken.Identifier
        ElseIf tm.CurrentToken.IsKeyword Then
            Name = tm.CurrentToken.Identifier
        Else
            Throw New InternalException(result)
        End If
        tm.NextToken()
        tm.AcceptIfNotInternalError(KS.Colon)
        tm.AcceptIfNotInternalError(KS.Equals)
        tm.AcceptNewLine()

        Expression = ParseExpression(result)
        If Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)


        result.Init(Name, Expression)

        Return result
    End Function

    Private Function ParseMyClassExpression(ByVal Parent As ParsedObject) As MyClassExpression
        Dim result As MyClassExpression

        tm.AcceptIfNotInternalError(KS.MyClass)
        result = New MyClassExpression(Parent)

        Return result
    End Function

    Private Function ParseMyBaseExpression(ByVal Parent As ParsedObject) As MyBaseExpression
        Dim result As MyBaseExpression

        tm.AcceptIfNotInternalError(KS.MyBase)
        result = New MyBaseExpression(Parent)

        Return result
    End Function

    Private Function ParseMeExpression(ByVal Parent As ParsedObject) As MeExpression
        Dim result As MeExpression

        tm.AcceptIfNotInternalError(KS.Me)
        result = New MeExpression(Parent)

        Return result
    End Function
    ''' <summary>
    ''' A single identifier followed by an optional type argument list.
    ''' 
    ''' SimpleNameExpression ::= Identifier [ "(" "Of" TypeArgumentList ")" ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSimpleNameExpression(ByVal Parent As ParsedObject) As SimpleNameExpression
        Dim result As New SimpleNameExpression(Parent)

        Dim m_Identifier As Identifier
        Dim m_TypeArgumentList As TypeArgumentList

        m_Identifier = ParseIdentifier(result)
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of Then
            m_TypeArgumentList = ParseTypeArgumentList(result)
            If m_TypeArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            'If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeArgumentList = Nothing
        End If

        result.Init(m_Identifier, m_TypeArgumentList)

        Return result
    End Function

    Private Function ParseCodeBlock(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As CodeBlock
        Dim result As New CodeBlock(Parent)
        Dim breakloop As Boolean

        Do
            If IsOneLiner = False AndAlso LabelDeclarationStatement.CanBeMe(tm) Then
                Dim newLabel As LabelDeclarationStatement
                newLabel = ParseLabelDeclarationStatement(result)
                If newLabel Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                result.AddStatement(newLabel)
                result.AddLabel(newLabel)
            ElseIf MidAssignStatement.IsMe(tm) Then
                Dim newMidAssign As MidAssignStatement
                newMidAssign = ParseMidAssignmentStatement(result, IsOneLiner)
                If newMidAssign Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                result.AddStatement(newMidAssign)
            ElseIf tm.CurrentToken.IsIdentifier OrElse _
              tm.CurrentToken.Equals(KS.Dot, KS.Me, KS.MyClass, KS.MyBase) OrElse _
              tm.CurrentToken.Equals(Enums.BuiltInTypeTypeNames) OrElse _
              tm.CurrentToken.Equals(KS.Global) OrElse _
              tm.CurrentToken.Equals(KS.DirectCast, KS.TryCast, KS.CType) OrElse _
              tm.CurrentToken.Equals(KS.GetType) Then
                'Must appear after the label check.
                'Must appear before the symbol check.
                'Must appear before the keywords check
                Dim lside, rside As Expression

                lside = ParseExpression(New ExpressionParseInfo(result, True, False))
                If lside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                If tm.CurrentToken.IsSymbol Then
                    Select Case tm.CurrentToken.Symbol
                        Case KS.Equals
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New AssignmentStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.AddAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New AddAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ConcatAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New ConcatAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.RealDivAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New DivisionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.IntDivAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New IntDivisionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.MultAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New MultiplicationAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.PowerAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New PowerAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ShiftRightAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New RShiftAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ShiftLeftAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New LShiftAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.MinusAssign
                            tm.NextToken()
                            tm.AcceptNewLine()
                            Dim newStmt As New SubtractionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case Else
                            Dim newStmt As New CallStatement(result)
                            newStmt.Init(lside)
                            result.AddStatement(newStmt)
                    End Select
                Else
                    Dim newStmt As New CallStatement(result)
                    newStmt.Init(lside)
                    result.AddStatement(newStmt)
                End If
            ElseIf tm.CurrentToken.IsKeyword Then
                Select Case tm.CurrentToken.Keyword
                    Case KS.Dim, KS.Static, KS.Const
                        Dim newVariables As Generic.List(Of LocalVariableDeclaration)
                        newVariables = ParseLocalDeclarationStatement(result)
                        If newVariables Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddVariables(newVariables)
                    Case KS.SyncLock
                        Dim newLock As SyncLockStatement
                        newLock = ParseSyncLockStatement(result, IsOneLiner)
                        If newLock Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newLock)
                    Case KS.Try
                        Dim newTry As TryStatement
                        newTry = ParseTryStatement(result, IsOneLiner)
                        If newTry Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newTry)
                    Case KS.Throw
                        Dim newThrow As ThrowStatement
                        newThrow = ParseThrowStatement(result)
                        If newThrow Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newThrow)
                    Case KS.With
                        Dim newWith As WithStatement
                        newWith = ParseWithStatement(result, IsOneLiner)
                        If newWith Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newWith)
                    Case KS.Select
                        Dim newSelect As SelectStatement
                        newSelect = ParseSelectStatement(result, IsOneLiner)
                        If newSelect Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newSelect)
                    Case KS.If
                        Dim newIf As IfStatement
                        newIf = ParseIfStatement(result, IsOneLiner)
                        If newIf Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newIf)
                    Case KS.Do
                        Dim newDo As DoStatement
                        newDo = ParseDoStatement(result, IsOneLiner)
                        If newDo Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newDo)
                    Case KS.Stop
                        Dim newStop As StopStatement
                        newStop = ParseStopStatement(result)
                        If newStop Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newStop)
                    Case KS.End
                        Dim newEnd As EndStatement
                        If tm.PeekToken.IsEndOfStatement() Then
                            newEnd = ParseEndStatement(result)
                            If newEnd Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            result.AddStatement(newEnd)
                        Else
                            breakloop = True
                        End If
                    Case KS.While
                        Dim newWhile As WhileStatement
                        newWhile = ParseWhileStatement(result, IsOneLiner)
                        If newWhile Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newWhile)
                    Case KS.Exit
                        Dim newExit As ExitStatement
                        newExit = ParseExitStatement(result)
                        If newExit Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newExit)
                    Case KS.Return
                        Dim newReturn As ReturnStatement
                        newReturn = ParseReturnStatement(result)
                        If newReturn Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newReturn)
                    Case KS.For
                        If tm.PeekToken.Equals(KS.Each) Then
                            Dim newFor As ForEachStatement
                            newFor = ParseForEachStatement(result, IsOneLiner)
                            If newFor Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            result.AddStatement(newFor)
                        Else
                            Dim newFor As ForStatement
                            newFor = ParseForStatement(result, IsOneLiner)
                            If newFor Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                            result.AddStatement(newFor)
                        End If
                    Case KS.Continue
                        Dim newContinue As ContinueStatement
                        newContinue = ParseContinueStatement(result, IsOneLiner)
                        If newContinue Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newContinue)
                    Case KS.GoTo
                        Dim newGoto As GotoStatement
                        newGoto = ParseGotoStatement(result)
                        If newGoto Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newGoto)
                    Case KS.On
                        Dim newOnError As OnErrorStatement
                        newOnError = ParseOnErrorStatement(result)
                        If newOnError Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newOnError)
                    Case KS.Error
                        Dim newError As ErrorStatement
                        newError = ParseErrorStatement(result)
                        If newError Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newError)
                    Case KS.AddHandler, KS.RemoveHandler
                        Dim newAddHandler As AddOrRemoveHandlerStatement
                        newAddHandler = ParseAddOrRemoveHandlerStatement(result)
                        If newAddHandler Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newAddHandler)
                    Case KS.RaiseEvent
                        Dim newRaiseEvent As RaiseEventStatement
                        newRaiseEvent = ParseRaiseEventStatement(result)
                        If newRaiseEvent Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newRaiseEvent)
                    Case KS.Call
                        Dim newCall As CallStatement
                        newCall = ParseCallStatement(result)
                        If newCall Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newCall)
                    Case KS.Erase
                        Dim newErase As EraseStatement
                        newErase = ParseEraseStatement(result)
                        If newErase Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newErase)
                    Case KS.ReDim
                        Dim newReDim As ReDimStatement
                        newReDim = ParseReDimStatement(result)
                        If newReDim Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newReDim)
                    Case KS.Resume
                        Dim newResume As ResumeStatement
                        newResume = ParseResumeStatement(result)
                        If newResume Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newResume)
                    Case KS.Using
                        Dim newUsing As UsingStatement
                        newUsing = ParseUsingStatement(result, IsOneLiner)
                        If newUsing Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                        result.AddStatement(newUsing)
                    Case Else
                        breakloop = True
                End Select
            ElseIf tm.CurrentToken.Equals(KS.Colon) Then
                'tm.NextToken()
            Else
                breakloop = True
            End If

            If breakloop = False Then
                If IsOneLiner Then
                    If tm.Accept(KS.Colon) = False Then
                        breakloop = True
                    End If
                Else
                    If tm.AcceptEndOfStatement(False, Compiler.Report.Errors = 0) = False Then
                        Return result
                    End If
                End If
            End If

            If result.FirstStatement Is Nothing AndAlso breakloop = False Then
                If result.Statements.Count = 1 Then
                    result.FirstStatement = result.Statements(0)
                    'ElseIf result.Variables.Count >= 1 Then
                    'result.FirstStatement = result.Variables(0)
                ElseIf result.Statements.Count > 1 Then 'OrElse result.Variables.Count > 1 Then
                    Throw New InternalException(result)
                Else
                    'Do nothing. No statements were parsed.
                End If
            End If
        Loop Until breakloop = True
        Return result
    End Function

    Private Function ParseExpressionList(ByVal Parent As ParsedObject) As ExpressionList
        Dim result As New ExpressionList(Parent)

        If ParseList(Of Expression)(result, New ParseDelegate_Parent(Of Expression)(AddressOf ParseExpression), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        Return result
    End Function

    <Obsolete()> Private Function ParseExpression() As Expression
        Dim result As Expression = Nothing

        Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        '        result = ParseOr_OrElse_Xor(Info)

        Return result
    End Function

    Private Function ParseExpression(ByVal Info As ExpressionParseInfo) As Expression
        Dim result As Expression = Nothing

        result = ParseOr_OrElse_Xor(Info)

        Return result
    End Function

    Public Function ParseExpression(ByVal Parent As ParsedObject) As Expression
        Dim result As Expression = Nothing

        result = ParseOr_OrElse_Xor(New ExpressionParseInfo(Parent))

        Return result
    End Function
    ''' <summary>
    ''' GetTypeExpression ::= "GetType" "(" GetTypeTypeName ")"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseGetTypeExpression(ByVal Parent As ParsedObject) As GetTypeExpression
        Dim result As New GetTypeExpression(Parent)

        tm.AcceptIfNotInternalError(KS.GetType)
        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Dim m_TypeName As GetTypeTypeName
        m_TypeName = ParseGetTypeTypeName(result)

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_TypeName)

        Return result
    End Function

    Private Function Parse(ByVal Info As ExpressionParseInfo) As Expression
        Dim result As Expression

        result = ParseOr_OrElse_Xor(Info)

        Return result
    End Function

    Private Function ParseIdentifier(ByVal Info As ExpressionParseInfo) As Expression
        Dim value As Expression = Nothing
        Dim result As Boolean = True

        If tm.CurrentToken.IsLiteral Then
            value = ParseLiteralExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken = KS.Dot Then
            value = ParseMemberAccessExpression(Info.Parent, Nothing)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken = KS.Exclamation Then
            value = ParseDictionaryAccessExpression(Info.Parent, Nothing)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken.Equals(Enums.BuiltInTypeTypeNames) Then
            value = ParseBuiltInTypeExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken.IsIdentifier Then
            value = ParseSimpleNameExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken = KS.LBrace Then
            value = ParseArrayInitializerExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken = KS.LParenthesis Then
            value = ParseParenthesizedExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken.Equals(KS.Add, KS.Minus) Then
            value = ParseUnaryPlusMinus(Info)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken.IsKeyword Then
            Select Case tm.CurrentToken.Keyword
                Case KS.Not
                    value = ParseNot(Info)
                Case KS.DirectCast, KS.TryCast, KS.CType
                    value = ParseCTypeExpression(Info.Parent, tm.CurrentToken.Keyword)
                Case KS.AddressOf
                    value = ParseAddressOfExpression(Info.Parent)
                Case KS.[New]
                    value = ParseNewExpression(Info.Parent)
                Case KS.CInt
                    value = ParseCIntExpression(Info.Parent)
                Case KS.CBool
                    value = ParseCBoolExpression(Info.Parent)
                Case KS.CByte
                    value = ParseCByteExpression(Info.Parent)
                Case KS.CChar
                    value = ParseCCharExpression(Info.Parent)
                Case KS.CDate
                    value = ParseCDateExpression(Info.Parent)
                Case KS.CDbl
                    value = ParseCDblExpression(Info.Parent)
                Case KS.CDec
                    value = ParseCDecExpression(Info.Parent)
                Case KS.CLng
                    value = ParseCLngExpression(Info.Parent)
                Case KS.CObj
                    value = ParseCObjExpression(Info.Parent)
                Case KS.CSByte
                    value = ParseCSByteExpression(Info.Parent)
                Case KS.CShort
                    value = ParseCShortExpression(Info.Parent)
                Case KS.CSng
                    value = ParseCSngExpression(Info.Parent)
                Case KS.CStr
                    value = ParseCStrExpression(Info.Parent)
                Case KS.CUInt
                    value = ParseCUIntExpression(Info.Parent)
                Case KS.CULng
                    value = ParseCULngExpression(Info.Parent)
                Case KS.CUShort
                    value = ParseCUShortExpression(Info.Parent)
                Case KS.True, KS.False
                    value = ParseBooleanLiteralExpression(Info.Parent)
                Case KS.Nothing
                    tm.AcceptIfNotInternalError(KS.Nothing)
                    value = New NothingConstantExpression(Info.Parent)
                Case KS.GetType
                    value = ParseGetTypeExpression(Info.Parent)
                Case KS.TypeOf
                    value = ParseTypeOfExpression(Info.Parent)
                Case KS.Me
                    value = ParseMeExpression(Info.Parent)
                Case KS.MyBase
                    value = ParseMyBaseExpression(Info.Parent)
                Case KS.MyClass
                    value = ParseMyClassExpression(Info.Parent)
                Case KS.Global
                    Dim newGlobal As GlobalExpression
                    newGlobal = ParseGlobalExpression(Info.Parent)
                    value = ParseMemberAccessExpression(Info.Parent, newGlobal)
                Case KS.If
                    value = ParseIfExpression(Info.Parent)
            End Select
        Else
            value = Nothing
        End If

        Do While result
            If tm.CurrentToken = KS.Dot Then
                Dim newExp As MemberAccessExpression
                newExp = ParseMemberAccessExpression(Info.Parent, value)
                value = newExp
            ElseIf tm.CurrentToken = KS.Exclamation Then
                Dim newExp As DictionaryAccessExpression
                newExp = ParseDictionaryAccessExpression(Info.Parent, value)
                value = newExp
            ElseIf tm.CurrentToken = KS.LParenthesis Then
                If tm.PeekToken.IsIntegerLiteral AndAlso tm.PeekToken.IntegralLiteral = 0 AndAlso tm.PeekToken(2).Equals(KS.To) Then
                    Exit Do
                End If
                Dim newExp As InvocationOrIndexExpression
                newExp = ParseInvocationOrIndexExpression(Info.Parent, value)
                value = newExp
            Else
                Exit Do
            End If
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Loop

        Return value
    End Function

    Private Function ParseExponent(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseIdentifier(Info)

        While tm.Accept(KS.Power)
            tm.AcceptNewLine()
            rSide = ParseIdentifier(Info)
            lSide = New ExponentExpression(Info.Parent, lSide, rSide)
        End While

        Return lSide
    End Function

    Private Function ParseUnaryPlusMinus(ByVal Info As ExpressionParseInfo) As Expression
        Dim result As UnaryExpression

        If tm.CurrentToken = KS.Add Then
            result = ParseUnaryPlusExpression(Info)
        ElseIf tm.CurrentToken = KS.Minus Then
            result = ParseUnaryMinusExpression(Info)
        Else
            Return ParseExponent(Info)
        End If

        Return result
    End Function

    Private Function ParseMultDiv(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseUnaryPlusMinus(Info)

        While tm.CurrentToken.Equals(KS.Mult, KS.RealDivision)
            Dim op As KS
            op = tm.CurrentToken.Symbol
            tm.NextToken()
            tm.AcceptNewLine()
            rSide = ParseUnaryPlusMinus(Info)
            If op = KS.Mult Then
                lSide = New MultExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.RealDivision Then
                lSide = New RealDivisionExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While

        Return lSide
    End Function

    Private Function ParseIntDiv(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseMultDiv(Info)

        While tm.Accept(KS.IntDivision)
            tm.AcceptNewLine()
            rSide = ParseMultDiv(Info)
            lSide = New IntDivisionExpression(Info.Parent, lSide, rSide)
        End While

        Return lSide
    End Function

    Private Function ParseMod(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseIntDiv(Info)

        While tm.Accept(KS.Mod)
            tm.AcceptNewLine()
            rSide = ParseIntDiv(Info)
            lSide = New ModExpression(Info.Parent, lSide, rSide)
        End While

        Return lSide
    End Function

    Private Function ParsePlusMinus(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseMod(Info)

        While tm.CurrentToken.Equals(KS.Add, KS.Minus)
            Dim op As KS
            op = tm.CurrentToken.Symbol
            tm.NextToken()
            tm.AcceptNewLine()
            rSide = ParseMod(Info)
            If op = KS.Add Then
                lSide = New BinaryAddExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.Minus Then
                lSide = New BinarySubExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While


        Return lSide
    End Function

    Private Function ParseConcat(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParsePlusMinus(Info)

        While tm.Accept(KS.Concat)
            tm.AcceptNewLine()
            rSide = ParsePlusMinus(Info)
            lSide = New ConcatExpression(Info.Parent, lSide, rSide)
        End While

        Return lSide
    End Function

    Private Function ParseBitshift(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseConcat(Info)

        While tm.CurrentToken.Equals(KS.ShiftRight, KS.ShiftLeft)
            Dim op As KS
            op = tm.CurrentToken.Symbol
            tm.NextToken()
            tm.AcceptNewLine()
            rSide = ParseConcat(Info)
            If op = KS.ShiftRight Then
                lSide = New RShiftExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.ShiftLeft Then
                lSide = New LShiftExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While

        Return lSide
    End Function

    Private Function ParseComparison(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseBitshift(Info)
        While tm.CurrentToken.Equals(KS.NotEqual, KS.LE, KS.LT, KS.GT, KS.GE, KS.Like, KS.IsNot) OrElse _
          (tm.CurrentToken = KS.Equals AndAlso Info.IsLeftSide = False) OrElse _
          (tm.CurrentToken = KS.Is AndAlso Info.IsInTypeOf = False)
            Dim op As KS
            If tm.CurrentToken.IsSymbol Then
                op = tm.CurrentToken.Symbol
            ElseIf tm.CurrentToken.IsKeyword Then
                op = tm.CurrentToken.Keyword
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If

            tm.NextToken()
            tm.AcceptNewLine()

            rSide = ParseBitshift(Info)

            If op = KS.Equals Then
                lSide = New EqualsExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.NotEqual Then
                lSide = New NotEqualsExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.GE Then
                lSide = New GEExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.GT Then
                lSide = New GTExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.LE Then
                lSide = New LEExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.LT Then
                lSide = New LTExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.Is OrElse op = KS.IsNot Then
                lSide = New Is_IsNotExpression(Info.Parent, lSide, rSide, op)
            ElseIf op = KS.Like Then
                lSide = New LikeExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While

        Return lSide
    End Function

    Private Function ParseNot(ByVal Info As ExpressionParseInfo) As Expression
        Dim result As UnaryNotExpression

        If tm.CurrentToken = KS.Not Then
            result = ParseUnaryNotExpression(Info)
        Else
            Return ParseComparison(Info)
        End If

        Return result
    End Function

    Private Function ParseAnd_AndAlso(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseNot(Info)

        While tm.CurrentToken.Equals(KS.And, KS.AndAlso)
            Dim op As KS
            op = tm.CurrentToken.Keyword
            tm.NextToken()
            tm.AcceptNewLine()
            rSide = ParseNot(Info)
            If op = KS.And Then
                lSide = New AndExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.AndAlso Then
                lSide = New AndAlsoExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While

        Return lSide
    End Function

    Private Function ParseOr_OrElse_Xor(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseAnd_AndAlso(Info)

        While tm.CurrentToken.Equals(KS.Or, KS.OrElse, KS.Xor)
            Dim op As KS
            op = tm.CurrentToken.Keyword
            tm.NextToken()
            tm.AcceptNewLine()
            rSide = ParseAnd_AndAlso(Info)
            If op = KS.Or Then
                lSide = New OrExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.OrElse Then
                lSide = New OrElseExpression(Info.Parent, lSide, rSide)
            ElseIf op = KS.Xor Then
                lSide = New XOrExpression(Info.Parent, lSide, rSide)
            Else
                Throw New InternalException(tm.CurrentLocation)
            End If
        End While

        Return lSide
    End Function

    ''' <summary>
    ''' AddressOfExpression  ::= "AddressOf" Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAddressOfExpression(ByVal Parent As ParsedObject) As AddressOfExpression
        Dim result As New AddressOfExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.AddressOf)

        m_Expression = ParseExpression(result)

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseIfExpression(ByVal Parent As ParsedObject) As IfExpression
        Dim result As New IfExpression(Parent)
        Dim expressions As ExpressionList

        tm.AcceptIfNotInternalError(KS.If)

        If Not tm.Accept(KS.LParenthesis) Then
            Compiler.Report.ShowMessage(Messages.VBNC30199, tm.CurrentLocation)
            tm.GotoNewline(False)
            Return result
        End If

        If tm.Accept(KS.RParenthesis) Then
            Compiler.Report.ShowMessage(Messages.VBNC33104, tm.CurrentLocation)
            tm.GotoNewline(False)
            Return result
        End If

        expressions = ParseExpressionList(result)

        If expressions.Count < 2 OrElse expressions.Count > 3 Then
            Compiler.Report.ShowMessage(Messages.VBNC33104, tm.CurrentLocation)
            tm.GotoNewline(False)
            Return result
        End If

        If Not tm.Accept(KS.RParenthesis) Then
            Compiler.Report.ShowMessage(Messages.VBNC30198, tm.CurrentLocation)
            tm.GotoNewline(False)
            Return result
        End If

        result.Condition = expressions(0)
        result.SecondPart = expressions(1)
        If expressions.Count = 3 Then
            result.ThirdPart = expressions(2)
        End If

        Return result
    End Function

    ''' <summary>
    ''' "TypeOf" Expression "Is" TypeName
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeOfExpression(ByVal Parent As ParsedObject) As TypeOfExpression
        Dim result As New TypeOfExpression(Parent)

        Dim m_Expression As Expression
        Dim m_Is As Boolean
        Dim m_Type As TypeName

        tm.AcceptIfNotInternalError(KS.TypeOf)

        m_Expression = ParseExpression(New ExpressionParseInfo(result, False, True))
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Is) Then
            m_Is = True
        ElseIf tm.Accept(KS.IsNot) Then
            m_Is = False
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            Return Nothing
        End If
        tm.AcceptNewLine()

        m_Type = ParseTypeName(result)
        If m_Type Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Expression, m_Is, m_Type)

        Return result
    End Function

    ''' <summary>
    ''' LiteralExpression  ::=  Literal
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseLiteralExpression(ByVal Parent As ParsedObject) As LiteralExpression
        Dim result As LiteralExpression

        Dim m_Value As Token
        m_Value = tm.CurrentToken
        If m_Value.IsLiteral = False Then
            result = Nothing
        Else
            result = New LiteralExpression(Parent)
            result.Init(m_Value)
            tm.NextToken()
        End If

        Return result
    End Function

    Private Function ParseBooleanLiteralExpression(ByVal Parent As ParsedObject) As BooleanLiteralExpression
        Dim result As New BooleanLiteralExpression(Parent)

        Dim m_Value As Boolean

        If tm.Accept(KS.True) Then
            m_Value = True
        ElseIf tm.Accept(KS.False) Then
            m_Value = False
        Else
            Throw New InternalException(result)
        End If

        result.Init(m_Value)

        Return result
    End Function

    ''' <summary>
    ''' VariableIdentifiers  ::=
    '''	            VariableIdentifier  |
    '''	            VariableIdentifiers  ,  VariableIdentifier
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableIdentifiers(ByVal Parent As ParsedObject) As VariableIdentifiers
        Dim result As New VariableIdentifiers(Parent)

        If ParseList(Of VariableIdentifier)(result, New ParseDelegate_Parent(Of VariableIdentifier)(AddressOf ParseVariableIdentifier), Parent) = False Then
            tm.GotoNewline(True)
        End If

        Return result
    End Function

    ''' <summary>
    ''' ConstructorMemberDeclaration  ::=
    ''' [  Attributes  ]  [  ConstructorModifier+  ]  "Sub" "New" [  "("  [  ParameterList  ]  ")"  ]  LineTerminator
    '''	[  Block  ]
    '''	"End" "Sub" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConstructorMember(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As ConstructorDeclaration
        Dim result As New ConstructorDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_ParameterList As New ParameterList(result)
        Dim m_Signature As SubSignature
        Dim m_Block As CodeBlock

        m_Modifiers = ParseModifiers(ModifierMasks.ConstructorModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)
        tm.AcceptIfNotInternalError(KS.[New])

        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If

        m_Signature = New SubSignature(result, "", m_ParameterList)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Sub) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' RegularPropertyMemberDeclaration  ::=
    '''	[  Attributes  ]  [  PropertyModifier+  ] "Property" FunctionSignature  [  ImplementsClause  ]
    '''		LineTerminator
    '''	PropertyAccessorDeclaration+
    '''	"End" "Property" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseRegularPropertyMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As RegularPropertyDeclaration
        Dim result As New RegularPropertyDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_Signature As FunctionSignature
        Dim m_ImplementsClause As MemberImplementsClause
        Dim m_Attributes As New Attributes(result)
        Dim m_Get As PropertyGetDeclaration = Nothing
        Dim m_Set As PropertySetDeclaration = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.PropertyModifiers)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        result.Signature = m_Signature

        If tm.AcceptEndOfStatement() = False Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_ImplementsClause = Nothing
        End If

        Do
            m_Attributes = ParseAttributes(result)
            If PropertyGetDeclaration.IsMe(tm) Then
                If m_Get IsNot Nothing Then
                    Compiler.Report.ShowMessage(Messages.VBNC30443, tm.CurrentLocation)
                End If
                m_Get = ParsePropertyGetMember(result, New ParseAttributableInfo(Compiler, m_Attributes), m_Signature, m_ImplementsClause, m_Modifiers.Mask)
                If m_Get Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                m_Attributes = Nothing
            ElseIf PropertySetDeclaration.IsMe(tm) Then
                If m_Set IsNot Nothing Then
                    Compiler.Report.ShowMessage(Messages.VBNC30444, tm.CurrentLocation)
                End If
                m_Set = ParsePropertySetMember(result, New ParseAttributableInfo(Compiler, m_Attributes), m_Signature, m_ImplementsClause, m_Modifiers.Mask)
                If m_Set Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                m_Attributes = Nothing
            Else
                If m_Attributes IsNot Nothing AndAlso m_Attributes.Count > 0 Then
                    'Hanging attributes.
                    Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
                End If
                Exit Do
            End If
        Loop

        If tm.AcceptIfNotError(KS.End, KS.Property) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If m_Modifiers.Is(ModifierMasks.ReadOnly) Then

            If m_Get Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30126, m_Signature.Location)
            End If

            If m_Set IsNot Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30022, m_Set.Location)
            End If

        End If

        If m_Modifiers.Is(ModifierMasks.WriteOnly) Then

            If m_Set Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30125, m_Signature.Location)
            End If

            If m_Get IsNot Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30023, m_Get.Location)
            End If

        End If
        If m_Modifiers.Is(ModifierMasks.ReadOnly) = False AndAlso m_Modifiers.Is(ModifierMasks.WriteOnly) = False Then
            If m_Get Is Nothing OrElse m_Set Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30124, m_Signature.Location)
            End If
        End If


        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, m_Get, m_Set, m_ImplementsClause)

        Return result
    End Function

    ''' <summary>
    ''' PropertySetDeclaration  ::=
    '''	[  Attributes  ]  [  AccessModifier  ]  "Set" [  (  ParameterList  )  ]  LineTerminator
    '''	[  Block  ]
    '''	"End" "Set" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParsePropertySetMember(ByVal Parent As PropertyDeclaration, ByVal Info As ParseAttributableInfo, ByVal ParentSignature As FunctionSignature, ByVal ParentImplements As MemberImplementsClause, ByVal ParentModifiers As ModifierMasks) As PropertySetDeclaration
        Dim result As New PropertySetDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_ParameterList As New ParameterList(result)
        Dim m_Block As CodeBlock

        m_Modifiers = ParseModifiers(ModifierMasks.AccessModifiers)
        If m_Modifiers.Empty = False Then
            m_Modifiers.AddModifiers(ParentModifiers And (Not ModifierMasks.AccessModifiers))
        Else
            m_Modifiers.AddModifiers(ParentModifiers)
        End If
        tm.AcceptIfNotInternalError(KS.Set)

        If tm.Accept(KS.LParenthesis) Then
            If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Set) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)


        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, ParentImplements, m_Block, m_ParameterList)

        Return result
    End Function

    ''' <summary>
    ''' PropertyGetDeclaration  ::=
    '''	[  Attributes  ]  [  AccessModifier  ]  Get  LineTerminator
    '''	[  Block  ]
    '''	End  Get  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParsePropertyGetMember(ByVal Parent As PropertyDeclaration, ByVal Info As ParseAttributableInfo, ByVal ParentSignature As FunctionSignature, ByVal ParentImplements As MemberImplementsClause, ByVal ParentModifiers As ModifierMasks) As PropertyGetDeclaration
        Dim result As New PropertyGetDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_Block As CodeBlock

        m_Modifiers = ParseModifiers(ModifierMasks.AccessModifiers)
        If m_Modifiers.Empty = False Then
            m_Modifiers.AddModifiers(ParentModifiers And (Not ModifierMasks.AccessModifiers))
        Else
            m_Modifiers.AddModifiers(ParentModifiers)
        End If

        tm.AcceptIfNotInternalError(KS.Get)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Get) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, ParentImplements, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' Tries to parse a sub signature. Returns false if not successful.
    ''' SubSignature  ::=  Identifier  [  TypeParameterList  ]  [  "("  [  ParameterList  ]  ")"  ]
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="m_Identifier">Output parameter, must be nothing on entry.</param>
    ''' <param name="m_TypeParameters">Output parameter, must be nothing on entry.</param>
    ''' <param name="m_ParameterList">Input/Output parameter, must not be nothing on entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseSubSignature(ByVal Parent As ParsedObject, ByRef m_Identifier As Identifier, ByRef m_TypeParameters As TypeParameters, ByVal m_ParameterList As ParameterList) As Boolean
        Dim result As Boolean = True

        'Helper.Assert(m_Identifier Is Nothing)
        Helper.Assert(m_TypeParameters Is Nothing)
        Helper.Assert(m_ParameterList IsNot Nothing)

        m_Identifier = ParseIdentifier(Parent)
        result = m_Identifier IsNot Nothing AndAlso result

        If vbnc.TypeParameters.IsMe(tm) Then
            m_TypeParameters = ParseTypeParameters(Parent)
        End If
        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If
                result = tm.AcceptIfNotError(KS.RParenthesis) AndAlso result
            End If
        End If

        'Helper.Assert(m_Identifier IsNot Nothing)

        Return result
    End Function

    ''' <summary>
    ''' SubSignature  ::=  Identifier  [  TypeParameterList  ]  [  "("  [  ParameterList  ]  ")"  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSubSignature(ByVal Parent As ParsedObject) As SubSignature
        Dim result As New SubSignature(Parent)

        Dim m_Identifier As Identifier = Nothing
        Dim m_TypeParameters As TypeParameters = Nothing
        Dim m_ParameterList As New ParameterList(result)

        If ParseSubSignature(result, m_Identifier, m_TypeParameters, m_ParameterList) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_Identifier, m_TypeParameters, m_ParameterList)

        Return result
    End Function

    ''' <summary>
    ''' FunctionSignature  ::=  SubSignature  [  "As"  [  Attributes  ]  TypeName  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseFunctionSignature(ByVal Parent As ParsedObject) As FunctionSignature
        Dim result As New FunctionSignature(Parent)

        Dim m_Identifier As Identifier = Nothing
        Dim m_TypeParameters As TypeParameters = Nothing
        Dim m_ParameterList As New ParameterList(result)
        Dim m_ReturnTypeAttributes As Attributes = Nothing
        Dim m_TypeName As TypeName = Nothing

        If ParseSubSignature(result, m_Identifier, m_TypeParameters, m_ParameterList) = False Then
            tm.GotoNewline(True)
        End If

        If tm.Accept(KS.As) Then
            If Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_Identifier, m_TypeParameters, m_ParameterList, m_ReturnTypeAttributes, m_TypeName, New Span(m_Identifier.Location, tm.CurrentLocation))

        Return result
    End Function


    ''' <summary>
    ''' TypeParameters  ::= "("  "Of"  TypeParameterList  ")"
    ''' CHANGED: Switched name of TypeParameters and TypeParameterList
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function ParseTypeParameters(ByVal Parent As ParsedObject) As TypeParameters
        Dim result As New TypeParameters(Parent)

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptIfNotError(KS.Of) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If ParseList(Of TypeParameter)(result.Parameters, New ParseDelegate_Parent(Of TypeParameter)(AddressOf ParseTypeParameter), result.Parameters) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function

    ''' <summary>
    ''' TypeParameter  ::= 	Identifier  [  TypeParameterConstraints  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeParameter(ByVal Parent As ParsedObject) As TypeParameter
        Dim result As New TypeParameter(Parent)
        Dim m_Identifier As Identifier
        Dim m_TypeParameterConstraints As TypeParameterConstraints
        Dim parentList As TypeParameterList

        Helper.Assert(TypeOf Parent Is TypeParameterList)

        parentList = DirectCast(Parent, TypeParameterList)
        result.GenericParameterPosition = parentList.Count + 1

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            result.Identifier = m_Identifier
        End If

        If TypeParameterConstraints.CanBeMe(tm) Then
            m_TypeParameterConstraints = ParseTypeParameterConstraints(result)
            If m_TypeParameterConstraints Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.TypeParameterConstraints = m_TypeParameterConstraints
        Else
            m_TypeParameterConstraints = Nothing
        End If

        Return result
    End Function

    ''' <summary>
    ''' TypeParameterConstraints  ::= 	As  Constraint  |	As  {  ConstraintList  }
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeParameterConstraints(ByVal Parent As ParsedObject) As TypeParameterConstraints
        Dim result As New TypeParameterConstraints(Parent)

        tm.AcceptIfNotInternalError(KS.As)

        Dim m_ConstraintList As New ConstraintList(result)

        If tm.Accept(KS.LBrace) Then
            If ParseList(Of Constraint)(m_ConstraintList, New ParseDelegate_Parent(Of Constraint)(AddressOf ParseConstraint), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.AcceptIfNotError(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            Dim tmpConstraint As Constraint = Nothing
            tmpConstraint = ParseConstraint(result)
            m_ConstraintList = New ConstraintList(result, tmpConstraint)
        End If

        result.Init(m_ConstraintList)

        Return result
    End Function

    ''' <summary>
    ''' Constraint  ::=  TypeName  |  "New"
    ''' LAMESPEC? Using the following:
    ''' Constraint  ::= TypeName | "New" | "Class" | "Structure"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConstraint(ByVal Parent As ParsedObject) As Constraint
        Dim result As New Constraint(Parent)

        Dim m_Special As KS
        Dim m_TypeName As TypeName = Nothing

        If tm.CurrentToken.Equals(KS.[New], KS.Class, KS.Structure) Then
            m_Special = tm.CurrentToken.Keyword
            tm.NextToken()
        Else
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_TypeName, m_Special)

        Return result
    End Function

    ''' <summary>
    ''' Parameter            ::= [  Attributes  ]  ParameterModifier+  ParameterIdentifier  [  "As"  TypeName  ]  [  "="  ConstantExpression  ]
    ''' ParameterModifier    ::= "ByVal" | "ByRef" | "Optional" | "ParamArray"
    ''' ParameterIdentifier  ::= Identifier  [  ArrayNameModifier  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseParameter(ByVal Parent As ParsedObject) As Parameter
        Helper.Assert(TypeOf Parent Is ParameterList)
        Dim result As New Parameter(DirectCast(Parent, ParameterList))

        Dim m_Attributes As New Attributes(result)
        Dim m_Modifiers As Modifiers
        Dim m_ParameterIdentifier As ParameterIdentifier
        Dim m_TypeName As TypeName
        Dim m_ConstantExpression As Expression

        If vbnc.Attributes.IsMe(tm) Then
            ParseAttributes(result, m_Attributes)
        End If

        m_Modifiers = ParseModifiers(ModifierMasks.ParameterModifiers)

        m_ParameterIdentifier = ParseParameterIdentifier(result)
        If m_ParameterIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeName = Nothing
        End If

        If tm.Accept(KS.Equals) Then
            tm.AcceptNewLine()
            m_ConstantExpression = ParseExpression(result)
            If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_ConstantExpression = Nothing
        End If

        result.Init(m_Attributes, m_Modifiers, m_ParameterIdentifier, m_TypeName, m_ConstantExpression)

        Return result
    End Function

    ''' <summary>
    ''' ParameterIdentifier  ::=  Identifier  [  ArrayNameModifier  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseParameterIdentifier(ByVal Parent As Parameter) As ParameterIdentifier
        Dim result As New ParameterIdentifier(Parent)

        Dim m_Identifier As Identifier
        Dim m_ArrayNameModifier As ArrayNameModifier = Nothing

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If vbnc.ArrayNameModifier.CanBeMe(tm) Then
            m_ArrayNameModifier = ParseArrayNameModifier(result)
            If m_ArrayNameModifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_Identifier, m_ArrayNameModifier)

        Return result
    End Function

    Private Function ParseImplementsClause(ByVal Parent As ParsedObject) As MemberImplementsClause
        Dim result As New MemberImplementsClause(Parent)

        Dim m_ImplementsList As New MemberImplementsList(Parent)

        tm.AcceptIfNotInternalError(KS.Implements)

        If ParseList(Of InterfaceMemberSpecifier)(m_ImplementsList, New ParseDelegate_Parent(Of InterfaceMemberSpecifier)(AddressOf ParseInterfaceMemberSpecifier), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_ImplementsList)

        Return result
    End Function

    Private Function ParseInterfaceMemberSpecifier(ByVal Parent As ParsedObject) As InterfaceMemberSpecifier
        Dim result As New InterfaceMemberSpecifier(Parent)

        Dim m_NonArrayTypeName As NonArrayTypeName = Nothing
        Dim m_1 As NonArrayTypeName = Nothing
        Dim m_2 As IdentifierOrKeyword = Nothing

        m_NonArrayTypeName = ParseNonArrayTypeName(result)

        If tm.Accept(KS.Dot) Then
            m_1 = m_NonArrayTypeName
            m_2 = ParseIdentifierOrKeyword(result)
            If m_2 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf m_NonArrayTypeName.IsSimpleTypeName AndAlso m_NonArrayTypeName.AsSimpleTypeName.IsQualifiedIdentifier Then
            Dim stn As SimpleTypeName = m_NonArrayTypeName.AsSimpleTypeName
            Dim qi As QualifiedIdentifier = stn.AsQualifiedIdentifier
            m_1 = m_NonArrayTypeName
            If Token.IsSomething(qi.Second) Then
                m_2 = New IdentifierOrKeyword(result, qi.Second)
                qi.Second = Nothing
            Else
                Helper.AddError(Compiler, tm.CurrentLocation)
            End If
        ElseIf m_NonArrayTypeName.IsConstructedTypeName Then
            Dim constructedTypeName As ConstructedTypeName = m_NonArrayTypeName.AsConstructedTypeName
            If constructedTypeName.QualifiedIdentifier IsNot Nothing AndAlso constructedTypeName.ConstructedTypeName IsNot Nothing AndAlso constructedTypeName.TypeArgumentList Is Nothing Then
                If constructedTypeName.QualifiedIdentifier.IsFirstIdentifier AndAlso Token.IsSomething(constructedTypeName.QualifiedIdentifier.Second) = False Then
                    m_1 = New NonArrayTypeName(result)
                    m_1.Init(constructedTypeName.ConstructedTypeName)
                    m_2 = New IdentifierOrKeyword(result, constructedTypeName.QualifiedIdentifier.FirstAsIdentifier.Identifier, KS.None)
                Else
                    Helper.AddError(Compiler, tm.CurrentLocation)
                End If
            Else
                Helper.AddError(Compiler, tm.CurrentLocation)
            End If
        Else
            Helper.AddError(Compiler, tm.CurrentLocation)
        End If

        result.Init(m_1, m_2)

        Return result
    End Function

    ''' <summary>
    ''' ConstantMemberDeclaration  ::=	[  Attributes  ]  [  ConstantModifier+  ]  "Const"  ConstantDeclarators  StatementTerminator
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Function ParseConstantMemberDeclarations(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo) As Generic.List(Of ConstantDeclaration)
        Dim result As New Generic.List(Of ConstantDeclaration)

        Dim m_Modifiers As Modifiers

        m_Modifiers = ParseModifiers(ModifierMasks.ConstantModifiers)

        tm.AcceptIfNotInternalError(KS.Const)
        m_Modifiers.AddModifiers(ModifierMasks.Const)

        result = ParseConstantDeclarations(Parent, Info.Attributes, m_Modifiers)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function

    Private Function ParseConstantDeclarations(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal Modifiers As Modifiers) As Generic.List(Of ConstantDeclaration)
        Dim result As New Generic.List(Of ConstantDeclaration)

        Do
            Dim newCD As ConstantDeclaration = Nothing
            newCD = ParseConstantDeclaration(Parent, New ParseAttributableInfo(Parent.Compiler, Attributes), Modifiers)
            If newCD Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Add(newCD)
        Loop While tm.Accept(KS.Comma)

        Return result
    End Function

    ''' <summary>
    ''' MustOverridePropertyMemberDeclaration  ::=
    '''	[  Attributes  ]  [  MustOverridePropertyModifier+  ]  "Property" FunctionSignature  [  ImplementsClause  ]
    '''		StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseMustOverridePropertyMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As MustOverridePropertyDeclaration
        Dim result As New MustOverridePropertyDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As FunctionSignature = Nothing
        Dim m_ImplementsClause As MemberImplementsClause = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.MustOverridePropertyModifiers)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        result.Signature = m_Signature

        If MemberImplementsClause.IsMe(tm) Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, , , m_ImplementsClause)

        Return result
    End Function

    ''' <summary>
    ''' ConversionOperatorDeclaration    ::=
    '''	[  Attributes  ]  [  ConversionOperatorModifier+  ]  "Operator" "CType" "("  Operand  ")"
    '''		[  "As"  [  Attributes  ]  TypeName  ]  LineTerminator
    '''	[  Block  ]
    '''	"End" "Operator" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConversionOperatorDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As ConversionOperatorDeclaration
        Dim result As New ConversionOperatorDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Operator As Token = Nothing
        Dim m_Operand As Operand = Nothing
        Dim m_ReturnTypeAttributes As Attributes = Nothing
        Dim m_TypeName As TypeName = Nothing
        Dim m_Block As CodeBlock = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.ConversionOperatorModifiers)

        tm.AcceptIfNotInternalError(KS.Operator)

        If vbnc.ConversionOperatorDeclaration.IsOverloadableConversionOperator(tm.CurrentToken) Then
            m_Operator = tm.CurrentToken : tm.NextToken()
        Else
            Throw New InternalException(result)
        End If

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Operand = ParseOperand(result)
        If m_Operand Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Operator) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Operator, m_Operand, m_ReturnTypeAttributes, m_TypeName, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' EnumMemberDeclaration  ::=  [  Attributes  ]  Identifier  [  "="  ConstantExpression  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEnumMemberDeclaration(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo, ByVal EnumIndex As Integer) As EnumMemberDeclaration
        Dim result As New EnumMemberDeclaration(Parent)

        Dim m_Identifier As Identifier
        Dim m_ConstantExpression As Expression

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Equals) Then
            m_ConstantExpression = ParseExpression(result)
            If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_ConstantExpression = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(EnumIndex, m_Identifier, m_ConstantExpression)

        Return result
    End Function

    ''' <summary>
    ''' Operand  ::=  [  "ByVal"  ]  Identifier  [  "As"  TypeName  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOperand(ByVal Parent As ParsedObject) As Operand
        Dim result As New Operand(Parent)

        Dim m_Identifier As Identifier
        Dim m_TypeName As TypeName

        tm.Accept(KS.ByVal)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeName = Nothing
        End If

        result.Init(m_Identifier, m_TypeName)

        Return result
    End Function


    ''' <summary>
    ''' BinaryOperatorDeclaration  ::=
    '''	[  Attributes  ]  [  OperatorModifier+  ]  "Operator"  OverloadableBinaryOperator
    '''		"("  Operand  ","  Operand  ")"  [ "As"  [  Attributes  ]  TypeName  ]  LineTerminator
    '''	[  Block  ]
    '''	"End" "Operator" StatementTerminator
    ''' 
    ''' UnaryOperatorDeclaration  ::=
    '''	[  Attributes  ]  [  OperatorModifier+  ]  "Operator" OverloadableUnaryOperator 
    '''     "("  Operand  ")" 		[  "As" [  Attributes  ]  TypeName  ]  LineTerminator
    '''	[  Block  ]
    '''	"End" "Operator" StatementTerminator
    ''' OverloadableUnaryOperator  ::=  "+"  | "-"  |  "Not"  |  "IsTrue"  |  "IsFalse"
    ''' </summary>
    ''' <remarks></remarks>

    Private Function ParseOperatorDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As OperatorDeclaration
        Dim result As New OperatorDeclaration(Parent)

        Dim m_Modifiers As Modifiers
        Dim m_OperatorSymbol As KS
        Dim m_OperatorIdentifier As String = Nothing
        Dim m_Operand1 As Operand
        Dim m_Operand2 As Operand
        Dim m_TypeName As TypeName
        Dim m_ReturnTypeAttributes As Attributes = Nothing
        Dim m_Block As CodeBlock

        m_Modifiers = ParseModifiers(ModifierMasks.OperatorModifiers)

        tm.AcceptIfNotInternalError(KS.Operator)

        If vbnc.OperatorDeclaration.IsOverloadableOperator(tm.CurrentToken) Then
            If tm.CurrentToken.IsIdentifier Then
                m_OperatorIdentifier = DirectCast(tm.Reader.TokenData, String)
            Else
                m_OperatorSymbol = tm.CurrentToken.Symbol
            End If
            tm.NextToken()
        Else
            Throw New InternalException(result)
        End If

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Operand1 = ParseOperand(result)
        If m_Operand1 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Comma) Then
            m_Operand2 = ParseOperand(result)
            If m_Operand2 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Operand2 = Nothing
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeName = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Operator) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_OperatorIdentifier, m_OperatorSymbol, m_Operand1, m_Operand2, m_ReturnTypeAttributes, m_TypeName, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' FunctionDeclaration  ::=
    '''	[  Attributes  ]  [  ProcedureModifier+  ]  "Function" FunctionSignature  [  HandlesOrImplements  ]
    '''		LineTerminator
    '''	Block
    '''	"End" "Function" StatementTerminator
    ''' 
    ''' MustOverrideFunctionDeclaration  ::=
    '''	[  Attributes  ]  [  MustOverrideProcedureModifier+  ]  "Function" FunctionSignature
    '''		[  HandlesOrImplements  ]  StatementTerminator
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseFunctionDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As FunctionDeclaration
        Dim result As New FunctionDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As FunctionSignature = Nothing
        Dim m_HandlesOrImplements As HandlesOrImplements = Nothing
        Dim m_Block As CodeBlock = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.MustOverrideProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Function)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If vbnc.HandlesOrImplements.IsMe(tm) Then
            m_HandlesOrImplements = ParseHandlesOrImplements(result)
            If m_HandlesOrImplements Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then tm.GotoNewline(True)

        If m_Modifiers.Is(ModifierMasks.MustOverride) = False Then
            m_Block = ParseCodeBlock(result, False)
            If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            If tm.AcceptIfNotError(KS.End, KS.Function) = False Then
                tm.GotoNewline(False)
            End If
            If tm.AcceptEndOfStatement(, True) = False Then
                tm.GotoNewline(True)
            End If
        End If

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, m_HandlesOrImplements, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' SubDeclaration  ::=
    '''	[  Attributes  ]  [  ProcedureModifier+  ] "Sub" SubSignature  [  HandlesOrImplements  ]  LineTerminator
    '''	Block
    '''	"End" "Sub" StatementTerminator
    ''' 
    ''' MustOverrideSubDeclaration  ::=
    '''	[  Attributes  ]  [  MustOverrideProcedureModifier+  ] "Sub" SubSignature  [  HandlesOrImplements  ]
    '''		StatementTerminator
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSubDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As SubDeclaration
        Dim result As New SubDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As SubSignature = Nothing
        Dim m_HandlesOrImplements As HandlesOrImplements = Nothing
        Dim m_Block As CodeBlock = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.MustOverrideProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Signature = ParseSubSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If vbnc.HandlesOrImplements.IsMe(tm) Then
            m_HandlesOrImplements = ParseHandlesOrImplements(result)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If m_Modifiers.Is(ModifierMasks.MustOverride) = False Then
            m_Block = ParseCodeBlock(result, False)
            If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

            If tm.AcceptIfNotError(KS.End, KS.Sub) = False Then
                tm.GotoNewline(False)
            End If
            If tm.AcceptEndOfStatement(, True) = False Then
                tm.GotoNewline(True)
            End If
        End If

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, m_HandlesOrImplements, m_Block)

        Return result
    End Function

    ''' <summary>
    ''' HandlesOrImplements  ::=  HandlesClause  |  ImplementsClause
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseHandlesOrImplements(ByVal Parent As ParsedObject) As HandlesOrImplements
        Dim result As New HandlesOrImplements(Parent)

        If vbnc.HandlesClause.IsMe(tm) Then
            Dim m_Clause As HandlesClause
            m_Clause = ParseHandlesClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(m_Clause)
        ElseIf vbnc.MemberImplementsClause.IsMe(tm) Then
            Dim m_Clause As MemberImplementsClause
            m_Clause = ParseImplementsClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            result.Init(m_Clause)
        Else
            Throw New InternalException(result)
        End If

        Return result
    End Function

    ''' <summary>
    ''' HandlesClause  ::=  [  "Handles" EventHandlesList  ]
    ''' LAMESPEC: shouldn't it be:
    ''' HandlesClause  ::=  "Handles" EventHandlesList
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseHandlesClause(ByVal Parent As ParsedObject) As HandlesClause
        Dim result As New HandlesClause(Parent)

        Dim m_List As New EventHandlesList(result)

        tm.AcceptIfNotInternalError(KS.Handles)

        If ParseList(Of EventMemberSpecifier)(m_List, New ParseDelegate_Parent(Of EventMemberSpecifier)(AddressOf ParseEventMemberSpecifier), result) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        result.Init(m_List)

        Return result
    End Function

    ''' <summary>
    ''' EventMemberSpecifier  ::=
    '''  QualifiedIdentifier  "."  IdentifierOrKeyword  |
    '''  MyBase  "."  IdentifierOrKeyword  |
    '''	 Me  "."  IdentifierOrKeyword
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEventMemberSpecifier(ByVal Parent As ParsedObject) As EventMemberSpecifier
        Dim result As New EventMemberSpecifier(Parent)

        Dim m_First As Expression
        Dim m_Second As IdentifierOrKeyword

        If tm.CurrentToken = KS.MyBase Then
            m_First = ParseMyBaseExpression(result)
        ElseIf tm.CurrentToken = KS.Me Then
            m_First = ParseMeExpression(result)
        Else
            Dim id As Identifier
            id = ParseIdentifier(result)
            If id Is Nothing Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            Dim sne As New SimpleNameExpression(result)
            sne.Init(id, New TypeArgumentList(sne))
            m_First = sne
        End If
        If m_First Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.Dot) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Second = ParseIdentifierOrKeyword(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_First, m_Second)

        Return result
    End Function

    ''' <summary>
    ''' InterfaceSubDeclaration  ::= 
    ''' [  Attributes  ]  [  InterfaceProcedureModifier+  ]  "Sub" SubSignature  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInterfaceSubDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As InterfaceSubDeclaration
        Dim result As New InterfaceSubDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As SubSignature = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.InterfaceProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Signature = ParseSubSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature)

        Return result
    End Function

    ''' <summary>
    ''' InterfaceFunctionDeclaration  ::=
    '''	[  Attributes  ]  [  InterfaceProcedureModifier+  ] "Function" FunctionSignature  StatementTerminator
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInterfaceFunctionDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As InterfaceFunctionDeclaration
        Dim result As New InterfaceFunctionDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As FunctionSignature = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.InterfaceProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Function)

        m_Signature = ParseFunctionSignature(result)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature)

        Return result
    End Function

    ''' <summary>
    ''' ExternalSubDeclaration ::=
    ''' 	[  Attributes  ]  [  ExternalMethodModifier+  ] "Declare" [  CharsetModifier  ] "Sub" Identifier
    '''		LibraryClause  [  AliasClause  ]  [  (  [  ParameterList  ]  )  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseExternalSubDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As ExternalSubDeclaration
        Dim result As New ExternalSubDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_CharsetModifier As KS
        Dim m_Identifier As Identifier
        Dim m_LibraryClause As LibraryClause = Nothing
        Dim m_AliasClause As AliasClause = Nothing
        Dim m_ParameterList As ParameterList = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.ExternalMethodModifiers)

        tm.AcceptIfNotInternalError(KS.Declare)

        If tm.CurrentToken.Equals(ModifierMasks.CharSetModifiers) Then
            m_CharsetModifier = tm.CurrentToken.Keyword
            tm.NextToken()
        End If

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        m_LibraryClause = ParseLibraryClause(result)
        If m_LibraryClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If AliasClause.IsMe(tm) Then
            m_AliasClause = ParseAliasClause(result)
            If m_AliasClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.Accept(KS.LParenthesis) Then
            m_ParameterList = New ParameterList(result)
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If

                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_CharsetModifier, m_Identifier, m_LibraryClause, m_AliasClause, m_ParameterList)

        Return result
    End Function

    ''' <summary>
    ''' ExternalFunctionDeclaration  ::=
    '''	[  Attributes  ]  [  ExternalMethodModifier+  ]  "Declare" [  CharsetModifier  ] "Function" Identifier
    '''		LibraryClause  [  AliasClause  ]  [  (  [  ParameterList  ]  )  ]  [  As  [  Attributes  ]  TypeName  ]
    '''		StatementTerminator
    ''' 
    ''' CharsetModifier  ::=  "Ansi" | "Unicode" |  "Auto"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseExternalFunctionDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As ExternalFunctionDeclaration
        Dim result As New ExternalFunctionDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_CharsetModifier As KS = KS.None
        Dim m_Identifier As Identifier
        Dim m_LibraryClause As LibraryClause = Nothing
        Dim m_AliasClause As AliasClause = Nothing
        Dim m_ParameterList As ParameterList = Nothing
        Dim m_ReturnTypeAttributes As Attributes = Nothing
        Dim m_TypeName As TypeName = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.ExternalMethodModifiers)
        tm.AcceptIfNotInternalError(KS.Declare)

        If tm.CurrentToken.Equals(ModifierMasks.CharSetModifiers) Then
            m_CharsetModifier = tm.CurrentToken.Keyword
            tm.NextToken()
        End If

        tm.AcceptIfNotInternalError(KS.Function)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_LibraryClause = ParseLibraryClause(result)
        If m_LibraryClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If AliasClause.IsMe(tm) Then
            m_AliasClause = ParseAliasClause(result)
            If m_AliasClause Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If
        If tm.Accept(KS.LParenthesis) Then
            m_ParameterList = New ParameterList(result)
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                End If

                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_CharsetModifier, m_Identifier, m_LibraryClause, m_AliasClause, m_ParameterList, m_ReturnTypeAttributes, m_TypeName)

        Return result
    End Function

    ''' <summary>
    ''' AliasClause  ::=  "Alias" StringLiteral
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAliasClause(ByVal Parent As ParsedObject) As AliasClause
        Dim result As New AliasClause(Parent)

        Dim m_StringLiteral As Token = Nothing

        tm.AcceptIfNotInternalError(KS.Alias)

        If tm.CurrentToken.IsStringLiteral Then
            m_StringLiteral = tm.CurrentToken
            tm.NextToken()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        result.Init(m_StringLiteral)

        Return result
    End Function

    ''' <summary>
    ''' LibraryClause  ::=  "Lib" StringLiteral
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseLibraryClause(ByVal Parent As ParsedObject) As LibraryClause
        Dim result As New LibraryClause(Parent)

        Dim m_StringLiteral As Token

        tm.AcceptIfNotInternalError(KS.Lib)

        If tm.CurrentToken.IsStringLiteral Then
            m_StringLiteral = tm.CurrentToken
            tm.NextToken()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            m_StringLiteral = Nothing
        End If

        result.Init(m_StringLiteral)

        Return result
    End Function

    ''' <summary>
    ''' ConstantDeclarator  ::=  Identifier  [  As  TypeName  ]  =  ConstantExpression  StatementTerminator
    ''' TODO: Is this a spec bug? ------------------------------------------------------^^^^^^^^^^^^^^^^^^^?
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConstantDeclaration(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo, ByVal Modifiers As Modifiers) As ConstantDeclaration
        Dim result As New ConstantDeclaration(Parent)

        Dim m_Identifier As Identifier
        Dim m_TypeName As TypeName = Nothing
        Dim m_ConstantExpression As Expression = Nothing

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If
        If tm.AcceptIfNotError(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        tm.AcceptNewLine()

        m_ConstantExpression = ParseExpression(result)
        If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(Modifiers, m_Identifier, m_TypeName, m_ConstantExpression)

        'Don't parse a StatementTerminator as the VB spec says.
        Return result
    End Function

    ''' <summary>
    ''' LocalDeclarationStatement  ::=  LocalModifier VariableDeclarators StatementTerminator
    ''' </summary>
    Private Function ParseLocalDeclarationStatement(ByVal Parent As CodeBlock) As Generic.List(Of LocalVariableDeclaration)
        Dim result As Generic.List(Of LocalVariableDeclaration)

        Dim m_Modifiers As Modifiers

        m_Modifiers = ParseModifiers(ModifierMasks.LocalModifiers)

        result = ParseLocalVariableDeclarators(Parent, m_Modifiers, New ParseAttributableInfo(Compiler, Nothing))
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function

    ''' <summary>
    ''' VariableMemberDeclaration  ::=	[  Attributes  ]  VariableModifier+  VariableDeclarators  StatementTerminator
    ''' </summary>
    Private Function ParseTypeVariableMemberDeclaration(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo) As Generic.List(Of TypeVariableDeclaration)
        Dim result As Generic.List(Of TypeVariableDeclaration)

        Dim m_VariableModifiers As Modifiers

        m_VariableModifiers = ParseModifiers(ModifierMasks.VariableModifiers)

        result = ParseTypeVariableDeclarators(Parent, m_VariableModifiers, Info)

        tm.AcceptNewLine(GotoNewline:=True, ReportError:=True)

        Return result
    End Function

    ''' <summary>
    ''' VariableMemberDeclaration  ::=	[  Attributes  ]  VariableModifier+  VariableDeclarators  StatementTerminator
    ''' </summary>
    Private Function ParseLocalVariableMemberDeclaration(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo) As Generic.List(Of LocalVariableDeclaration)
        Dim result As Generic.List(Of LocalVariableDeclaration)

        Dim m_VariableModifiers As Modifiers

        m_VariableModifiers = ParseModifiers(ModifierMasks.VariableModifiers)

        result = ParseLocalVariableDeclarators(Parent, m_VariableModifiers, Info)

        If tm.FindNewLineAndShowError() = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function

    ''' <summary>
    ''' VariableDeclarators  ::= VariableDeclarator  |	VariableDeclarators  ,  VariableDeclarator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseLocalVariableDeclarators(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of LocalVariableDeclaration)
        Dim result As New Generic.List(Of LocalVariableDeclaration)

        Do
            Dim tmp As New Generic.List(Of LocalVariableDeclaration)
            tmp = ParseLocalVariableDeclarator(Parent, Modifiers, Info)
            If tmp Is Nothing Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                result.AddRange(tmp)
            End If
        Loop While tm.Accept(KS.Comma)

        Return result
    End Function

    ''' <summary>
    ''' VariableDeclarators  ::= VariableDeclarator  |	VariableDeclarators  ,  VariableDeclarator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeVariableDeclarators(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of TypeVariableDeclaration)
        Dim result As New Generic.List(Of TypeVariableDeclaration)

        Do
            Dim tmp As New Generic.List(Of TypeVariableDeclaration)
            'Console.WriteLine("ParseTypeVariableDeclarators...")
            tmp = ParseTypeVariableDeclarator(Parent, Modifiers, Info)
            'Console.WriteLine("Got something: {0}", tmp Is Nothing = False)
            If tmp Is Nothing Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                result.AddRange(tmp)
            End If
        Loop While tm.Accept(KS.Comma)

        Return result
    End Function

    Private Function ParseLocalVariableDeclarator(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of LocalVariableDeclaration)
        Dim result As New Generic.List(Of LocalVariableDeclaration)
        If ParseVariableDeclarator(Parent, Modifiers, Info, result, True) = False Then
            Console.WriteLine("Returned false")
            Return Nothing
        End If
        Return result
    End Function

    Private Function ParseTypeVariableDeclarator(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of TypeVariableDeclaration)
        Dim result As New Generic.List(Of TypeVariableDeclaration)
        Dim tmp As Boolean

        'Console.Write("ParseTypeVariableDeclarator...")
        tmp = ParseVariableDeclarator(Parent, Modifiers, Info, result, False)
        'Console.WriteLine("tmp: {0}", tmp)
        If tmp = False Then
            'Console.WriteLine("tmp was false")
            Return Nothing
        End If
        'Console.WriteLine("tmp was true")
        Return result
    End Function

    ''' <summary>
    ''' VariableDeclarator  ::=
    '''  	VariableIdentifiers  [  As  [  New  ]  TypeName  [  (  ArgumentList  )  ]  ]  |
    '''     VariableIdentifier   [  As  TypeName  ]  [  =  VariableInitializer  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableDeclarator(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo, ByVal result As IList, ByVal local As Boolean) As Boolean
        Dim m_VariableIdentifiers As VariableIdentifiers
        Dim m_IsNew As Boolean
        Dim m_TypeName As TypeName
        Dim m_VariableInitializer As VariableInitializer
        Dim m_ArgumentList As ArgumentList

        m_VariableIdentifiers = ParseVariableIdentifiers(Parent)
        If m_VariableIdentifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            m_IsNew = tm.Accept(KS.[New])
            If m_IsNew Then
                'Arrays not allowed.
                Dim m_NonArrayTypeName As NonArrayTypeName
                m_NonArrayTypeName = ParseNonArrayTypeName(Parent)
                If m_NonArrayTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                m_TypeName = New TypeName(Parent, m_NonArrayTypeName)
            Else
                'Arrays allowed.
                m_TypeName = ParseTypeName(Parent)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        Else
            m_TypeName = Nothing
        End If

        If tm.Accept(KS.Equals) Then
            tm.AcceptNewLine()
            m_VariableInitializer = ParseVariableInitializer(Parent)
            If m_VariableInitializer Is Nothing Then Compiler.Report.ShowMessage(Messages.VBNC30201, tm.CurrentLocation)
            m_ArgumentList = Nothing
        ElseIf tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                m_ArgumentList = ParseArgumentList(Parent)
                If m_ArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            Else
                m_ArgumentList = New ArgumentList(Parent)
            End If
            m_VariableInitializer = Nothing
        Else
            m_VariableInitializer = Nothing
            m_ArgumentList = Nothing
        End If

        'result Dim result As New Generic.List(Of VariableDeclaration)
        For Each identifier As VariableIdentifier In m_VariableIdentifiers
            Dim varD As VariableDeclaration
            If local Then
                varD = New LocalVariableDeclaration(Parent, Modifiers, identifier, m_IsNew, m_TypeName, m_VariableInitializer, m_ArgumentList)
            Else
                varD = New TypeVariableDeclaration(Parent, Modifiers, identifier, m_IsNew, m_TypeName, m_VariableInitializer, m_ArgumentList)
            End If
            varD.Location = identifier.Location
            varD.CustomAttributes = Info.Attributes
            result.Add(varD)
        Next

        Return True
    End Function

    Private Function ParseInterfacePropertyMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As InterfacePropertyMemberDeclaration
        Dim result As New InterfacePropertyMemberDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As FunctionSignature = Nothing

        m_Modifiers = ParseModifiers(ModifierMasks.InterfacePropertyModifier)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        result.Signature = m_Signature

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Info.Attributes
        result.Init(m_Modifiers, m_Signature, Nothing)

        Return result
    End Function


    ''' <summary>
    ''' LabelDeclarationStatement  ::=  LabelName  ":"
    ''' LabelName  ::=  Identifier  |  IntLiteral
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseLabelDeclarationStatement(ByVal Parent As ParsedObject) As LabelDeclarationStatement
        Dim m_Label As Token

        If tm.CurrentToken.IsIdentifier OrElse tm.CurrentToken.IsIntegerLiteral Then
            m_Label = tm.CurrentToken
            tm.NextToken()
        Else
            Throw New InternalException(Parent)
        End If

        If tm.CurrentToken.Equals(KS.Colon) = False Then
            Throw New InternalException(Parent)
        End If

        If tm.PeekToken.IsEndOfLineOnly Then
            tm.NextToken()
        End If

        Return New LabelDeclarationStatement(Parent, m_Label)
    End Function

    ''' <summary>
    ''' ThrowStatement  ::= "Throw" [  Expression  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseThrowStatement(ByVal Parent As ParsedObject) As ThrowStatement
        Dim result As New ThrowStatement(Parent)

        Dim m_Exception As Expression

        tm.AcceptIfNotInternalError(KS.Throw)

        If tm.CurrentToken.IsEndOfStatement = False Then
            m_Exception = ParseExpression(result)
            If m_Exception Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Exception = Nothing
        End If

        result.Init(m_Exception)

        Return result
    End Function

    Private Function ParseStopStatement(ByVal Parent As ParsedObject) As StopStatement
        tm.AcceptIfNotInternalError(KS.Stop)

        Return New StopStatement(Parent)
    End Function

    Private Function ParseResumeStatement(ByVal Parent As ParsedObject) As ResumeStatement
        Dim m_IsResumeNext As Boolean
        Dim m_TargetLabel As Token? = Nothing
        Dim m_TargetLocation As Span? = Nothing

        tm.AcceptIfNotInternalError(KS.Resume)
        If tm.Accept(KS.Next) Then
            m_IsResumeNext = True
        ElseIf tm.CurrentToken.IsIdentifier OrElse tm.CurrentToken.IsIntegerLiteral Then
            m_TargetLabel = tm.CurrentToken
            m_TargetLocation = tm.CurrentLocation
            tm.NextToken()
        End If

        Return New ResumeStatement(Parent, m_IsResumeNext, m_TargetLabel, m_TargetLocation)
    End Function

    ''' <summary>
    ''' RedimStatement  ::= "ReDim" [ "Preserve" ]  RedimClauses  StatementTerminator
    ''' RedimClauses  ::=
    '''	   RedimClause  |
    '''	   RedimClauses  ","  RedimClause
    ''' RedimClause  ::=  Expression  ArraySizeInitializationModifier
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseReDimStatement(ByVal Parent As ParsedObject) As ReDimStatement
        Dim result As New ReDimStatement(Parent)

        Dim m_IsPreserve As Boolean
        Dim m_Clauses As RedimClauses

        tm.AcceptIfNotInternalError(KS.ReDim)
        If tm.CurrentToken.Equals("Preserve") Then
            m_IsPreserve = True
            tm.NextToken()
        End If

        m_Clauses = ParseRedimClauses(result)
        If m_Clauses Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_IsPreserve, m_Clauses)

        Return result
    End Function

    ''' <summary>
    ''' OnErrorStatement  ::=  "On" "Error" ErrorClause  StatementTerminator
    ''' ErrorClause  ::=
    '''	   "GoTo"  "-"  "1" |
    '''	   "GoTo"  "0"  |
    '''	   GotoStatement  |
    '''	   "Resume" "Next"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseOnErrorStatement(ByVal Parent As ParsedObject) As OnErrorStatement
        Dim m_IsResumeNext As Boolean
        Dim m_Label As Token = Nothing
        Dim m_IsGotoMinusOne As Boolean
        Dim m_IsGotoZero As Boolean

        tm.AcceptIfNotInternalError(KS.On)
        If tm.Accept(KS.Error) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.Accept(KS.Resume) Then
            If tm.Accept(KS.Next) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            m_IsResumeNext = True
        Else
            If tm.Accept(KS.GoTo) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.CurrentToken.IsIntegerLiteral Then
                If tm.CurrentToken.IntegralLiteral = 0 Then
                    m_IsGotoZero = True
                Else
                    m_Label = tm.CurrentToken
                End If
                tm.NextToken()
            ElseIf tm.CurrentToken = KS.Minus AndAlso tm.PeekToken.IsIntegerLiteral Then
                If tm.PeekToken.IntegralLiteral = 1 Then
                    m_IsGotoMinusOne = True
                    tm.NextToken(2)
                Else
                    Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                    Compiler.Report.ShowMessage(Messages.VBNC90011, tm.CurrentLocation, "-1")
                End If
            ElseIf tm.CurrentToken.IsIdentifier Then
                m_Label = tm.CurrentToken
                tm.NextToken()
            Else
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation)
                Return Nothing
            End If
        End If

        Return New OnErrorStatement(Parent, m_IsResumeNext, m_Label, m_IsGotoMinusOne, m_IsGotoZero)
    End Function


    ''' <summary>
    ''' GotoStatement  ::=  "GoTo" LabelName  StatementTerminator
    ''' LabelName ::= Identifier | IntLiteral
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseGotoStatement(ByVal Parent As ParsedObject) As GotoStatement
        Dim m_GotoWhere As Token

        tm.AcceptIfNotInternalError(KS.GoTo)
        If tm.CurrentToken.IsIdentifier OrElse tm.CurrentToken.IsIntegerLiteral Then
            m_GotoWhere = tm.CurrentToken
            tm.NextToken()
        Else
            Return Nothing
        End If

        Return New GotoStatement(Parent, m_GotoWhere)
    End Function

    ''' <summary>
    ''' ExitStatement  ::=  "Exit" ExitKind  StatementTerminator
    ''' ExitKind  ::=  "Do" | "For" | "While" | "Select" | "Sub" | "Function" | "Property" | "Try"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseExitStatement(ByVal Parent As ParsedObject) As ExitStatement
        Dim m_ExitWhat As KS
        Dim exitLocation As Span

        tm.AcceptIfNotInternalError(KS.Exit)
        If tm.CurrentToken.Equals(KS.Sub, KS.Function, KS.Property, KS.Do, KS.For, KS.Try, KS.While, KS.Select) Then
            m_ExitWhat = tm.CurrentToken.Keyword
            exitLocation = tm.CurrentLocation
            tm.NextToken()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30240, tm.CurrentLocation)
            Return Nothing
        End If

        Return New ExitStatement(Parent, m_ExitWhat, exitLocation)
    End Function

    ''' <summary>
    ''' EndStatement  ::= "End" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEndStatement(ByVal Parent As ParsedObject) As EndStatement
        tm.AcceptIfNotInternalError(KS.End)
        Return New EndStatement(Parent)
    End Function

    ''' <summary>
    '''ContinueStatement  ::=  "Continue" ContinueKind  StatementTerminator
    '''ContinueKind  ::=  "Do" | "For" | "While"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseContinueStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As ContinueStatement
        Dim result As New ContinueStatement(Parent)

        Dim m_ContinueWhat As KS

        tm.AcceptIfNotInternalError(KS.Continue)
        If tm.CurrentToken.Equals(KS.Do, KS.For, KS.While) Then
            m_ContinueWhat = tm.CurrentToken.Keyword
            tm.NextToken()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30781, tm.CurrentLocation)
            Return Nothing
        End If

        result.Init(m_ContinueWhat)

        Return result
    End Function

    ''' <summary>
    ''' EraseStatement  ::= "Erase" EraseExpressions  StatementTerminator
    ''' EraseExpressions  ::=
    '''	  Expression  |
    '''	  EraseExpressions  ,  Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEraseStatement(ByVal Parent As ParsedObject) As EraseStatement
        Dim result As New EraseStatement(Parent)

        Dim m_Targets As ExpressionList

        tm.AcceptIfNotInternalError(KS.Erase)

        m_Targets = ParseExpressionList(Parent)
        If m_Targets Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Targets)

        Return result
    End Function

    Private Function ParseReturnStatement(ByVal Parent As ParsedObject) As ReturnStatement
        Dim result As New ReturnStatement(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.Return)
        If Not tm.CurrentToken.IsEndOfStatement Then
            m_Expression = ParseExpression(result)
            If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Expression = Nothing
        End If

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseRedimClauses(ByVal Parent As ReDimStatement) As RedimClauses
        Dim result As New RedimClauses(Parent)
        If ParseList(Of RedimClause)(result, New ParseDelegate_Parent(Of RedimClause)(AddressOf ParseRedimClause), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If
        Return result
    End Function

    ''' <summary>
    ''' RedimClause  ::=  Expression  ArraySizeInitializationModifier
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseRedimClause(ByVal Parent As ParsedObject) As RedimClause
        Dim result As New RedimClause(Parent)

        Dim m_Expression As Expression
        Dim asim As ArraySizeInitializationModifier

        m_Expression = ParseExpression(result)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Dim invExpression As InvocationOrIndexExpression = TryCast(m_Expression, InvocationOrIndexExpression)
        If invExpression IsNot Nothing Then
            m_Expression = invExpression.Expression
            asim = New ArraySizeInitializationModifier(result)
            Dim bl As New BoundList(asim)
            Dim exp() As Expression

            ReDim exp(invExpression.ArgumentList.Count - 1)
            For i As Integer = 0 To invExpression.ArgumentList.Count - 1
                exp(i) = invExpression.ArgumentList(i).Expression
            Next
            bl.Init(exp)
            asim.Init(bl, Nothing)

        ElseIf tm.CurrentToken.Equals(KS.LParenthesis) AndAlso tm.PeekToken.IsIntegerLiteral AndAlso tm.PeekToken.IntegralLiteral = 0 AndAlso tm.PeekToken(2).Equals(KS.To) Then
            asim = ParseArraySizeInitializationModifer(result)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            Return Nothing
        End If

        result.Init(m_Expression, asim)

        Return result
    End Function

    ''' <summary>
    ''' ErrorStatement  ::=  "Error" Expression  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseErrorStatement(ByVal Parent As ParsedObject) As ErrorStatement
        Dim result As New ErrorStatement(Parent)

        Dim m_ErrNumber As Expression

        tm.AcceptIfNotInternalError(KS.Error)

        m_ErrNumber = ParseExpression(result)
        If m_ErrNumber Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_ErrNumber)

        Return result
    End Function

    ''' <summary>
    ''' MidAssignmentStatement  ::=
    '''	   "Mid" [ "$" ]  "("  Expression "," Expression  [ "," Expression  ] ")"  =  Expression  
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseMidAssignmentStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As MidAssignStatement
        Dim result As New MidAssignStatement(Parent)

        Dim m_Target As Expression
        Dim m_Start As Expression
        Dim m_Length As Expression
        Dim m_Source As Expression

        tm.AcceptIfNotInternalError("Mid")

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Target = ParseExpression(result)
        If m_Target Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Start = ParseExpression(result)
        If m_Start Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Comma) Then
            m_Length = ParseExpression(result)
            If m_Length Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Length = Nothing
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.Accept(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Source = ParseExpression(result)
        If m_Source Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Target, m_Start, m_Length, m_Source)

        Return result
    End Function
    ''' <summary>
    ''' WhileStatement  ::=
    '''	   "While" BooleanExpression  StatementTerminator
    '''	         [  Block  ]
    '''	   "End" "While" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseWhileStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As WhileStatement
        Dim result As New WhileStatement(Parent)

        Dim m_Condition As Expression
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.While)
        m_Condition = ParseExpression(result)
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.While) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Condition, m_Code)

        Return result
    End Function

    ''' <summary>
    ''' WithStatement  ::=
    '''	   "With" Expression  StatementTerminator
    '''	        [  Block  ]
    '''	   "End" "With" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseWithStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As WithStatement
        Dim result As New WithStatement(Parent)

        Dim m_WithExpression As Expression
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.With)

        m_WithExpression = ParseExpression(result)
        If m_WithExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        m_Code = ParseCodeBlock(result, IsOneLiner)

        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.End, KS.With) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Code, m_WithExpression)

        Return result
    End Function

    ''' <summary>
    ''' Homebrew:
    ''' UsingDeclarator ::= 
    '''  Identifier  [  As  [  New  ]  NonArrayTypeName  [  (  ArgumentList  )  ]  ]  |
    '''  Identifier  [  As  NonArrayTypeName  ]  [  =  VariableInitializer  ]
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseUsingDeclarator(ByVal Parent As ParsedObject) As UsingDeclarator
        Dim result As New UsingDeclarator(Parent)

        Dim m_Identifier As Identifier
        Dim m_IsNew As Boolean
        Dim m_IsVariableDeclaration As Boolean
        Dim m_TypeName As NonArrayTypeName
        Dim m_VariableInitializer As VariableInitializer = Nothing
        Dim m_ArgumentList As ArgumentList = Nothing
        Dim m_VariableDeclaration As LocalVariableDeclaration

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            m_IsVariableDeclaration = True
            m_IsNew = tm.Accept(KS.[New])

            m_TypeName = ParseNonArrayTypeName(result)

            If m_IsNew = False Then
                If tm.Accept(KS.Equals) Then
                    m_VariableInitializer = ParseVariableInitializer(result)
                End If
            Else
                If tm.Accept(KS.LParenthesis) Then
                    If tm.Accept(KS.RParenthesis) = False Then
                        m_ArgumentList = ParseArgumentList(result)
                        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                    End If
                End If
                If m_ArgumentList Is Nothing Then m_ArgumentList = New ArgumentList(result)
            End If

            m_VariableDeclaration = New LocalVariableDeclaration(result, m_Identifier, m_IsNew, m_TypeName, m_VariableInitializer, m_ArgumentList)
        Else
            m_VariableDeclaration = Nothing
            m_VariableInitializer = Nothing
            m_ArgumentList = Nothing
            m_TypeName = Nothing
        End If


        result.Init(m_Identifier, m_IsNew, m_TypeName, m_ArgumentList, m_VariableInitializer, m_IsVariableDeclaration, m_VariableDeclaration)

        Return result
    End Function

    ''' <summary>
    ''' UsingStatement  ::=
    '''	"Using" UsingResources  StatementTerminator
    '''		[  Block  ]
    '''	"End" "Using" StatementTerminator
    ''' 
    ''' UsingResources  ::=  VariableDeclarators  |  Expression
    ''' 
    ''' LAMESPEC!?
    ''' I'm using this:
    ''' UsingResources ::= UsingDeclarators | Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseUsingStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As UsingStatement
        Dim result As New UsingStatement(Parent)

        Dim m_UsingResources As ParsedObject
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.Using)

        Dim newDecls As UsingDeclarators = Nothing
        If tm.CurrentToken.IsIdentifier AndAlso tm.PeekToken.Equals(KS.Equals, KS.As) Then
            'This is a variable declaration
            newDecls = New UsingDeclarators(result)
            If ParseList(Of UsingDeclarator)(newDecls, New ParseDelegate_Parent(Of UsingDeclarator)(AddressOf ParseUsingDeclarator), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            m_UsingResources = newDecls
        Else
            'This is an expression
            Dim exp As Expression = Nothing
            exp = ParseExpression(result)
            If exp Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            m_UsingResources = exp
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If newDecls IsNot Nothing Then
            For Each decl As UsingDeclarator In newDecls
                If decl.IsVariableDeclaration Then
                    m_Code.Variables.Add(decl.VariableDeclaration)
                End If
                decl.Parent = m_Code
            Next
        End If

        If tm.Accept(KS.End, KS.Using) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_UsingResources, m_Code)

        Return result
    End Function

    ''' <summary>
    '''SyncLockStatement  ::=
    '''	"SyncLock" Expression  StatementTerminator
    '''	   [  Block  ]
    '''	"End" "SyncLock" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSyncLockStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As SyncLockStatement
        Dim result As New SyncLockStatement(Parent)

        Dim m_Lock As Expression
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.SyncLock)

        m_Lock = ParseExpression(result)
        If m_Lock Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.End, KS.SyncLock) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Lock, m_Code)

        Return result
    End Function

    Private Function ParseDoStatementCondition(ByVal Parent As ParsedObject, ByRef IsWhile As Boolean) As Expression
        Dim result As Expression = Nothing

        If tm.Accept(KS.While) Then
            IsWhile = True
            result = ParseExpression(Parent)
        ElseIf tm.Accept(KS.Until) Then
            IsWhile = False
            result = ParseExpression(Parent)
        Else
            Throw New InternalException(result)
        End If
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        Return result
    End Function

    ''' <summary>
    ''' DoLoopStatement  ::=  DoTopLoopStatement  |  DoBottomLoopStatement
    ''' DoTopLoopStatement  ::=
    '''	   "Do" [  WhileOrUntil  BooleanExpression  ]  StatementTerminator
    '''	       [  Block  ]
    '''	   "Loop" StatementTerminator
    ''' DoBottomLoopStatement  ::=
    '''	   "Do" StatementTerminator
    '''	       [  Block  ]
    '''	   "Loop" WhileOrUntil  BooleanExpression  StatementTerminator
    '''WhileOrUntil  ::= "While" | "Until"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseDoStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As DoStatement
        Dim result As New DoStatement(Parent)

        Dim m_PreCondition As Expression
        Dim m_PostCondition As Expression
        Dim m_IsWhile As Boolean
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.Do)
        If tm.CurrentToken.Equals(KS.While, KS.Until) Then
            m_PreCondition = ParseDoStatementCondition(result, m_IsWhile)
            If m_PreCondition Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            End If
        Else
            m_PreCondition = Nothing
        End If

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.Loop) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.CurrentToken.Equals(KS.While, KS.Until) Then
            m_PostCondition = ParseDoStatementCondition(result, m_IsWhile)
            If m_PostCondition Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            End If
        Else
            m_PostCondition = Nothing
        End If

        result.Init(m_PreCondition, m_PostCondition, m_IsWhile, m_Code)

        If m_PreCondition IsNot Nothing AndAlso m_PostCondition IsNot Nothing Then
            'helper.AddError "error BC30238: 'Loop' cannot have a condition if matching 'Do' has one."
            Compiler.Report.ShowMessage(Messages.VBNC30238, tm.CurrentLocation)
            result.HasErrors = True
        End If

        Return result
    End Function

    ''' <summary>
    '''TryStatement  ::=
    '''	"Try" StatementTerminator
    '''	   [  Block  ]
    '''	[  CatchStatement+  ]
    '''	[  FinallyStatement  ]
    '''	"End" "Try" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTryStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As TryStatement
        Dim result As New TryStatement(Parent)

        Dim m_TryCode As CodeBlock
        Dim m_FinallyBlock As CodeBlock
        Dim m_Catches As BaseObjects(Of CatchStatement)

        tm.AcceptIfNotInternalError(KS.Try)
        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_TryCode = ParseCodeBlock(result, IsOneLiner)
        If m_TryCode Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Catches = New BaseObjects(Of CatchStatement)(result)
        While tm.CurrentToken = KS.Catch
            Dim newCatch As CatchStatement
            newCatch = ParseCatchStatement(result, IsOneLiner)
            m_Catches.Add(newCatch)
        End While

        If tm.Accept(KS.Finally) Then
            If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            m_FinallyBlock = ParseCodeBlock(result, IsOneLiner)
            If m_FinallyBlock Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_FinallyBlock = Nothing
        End If

        If tm.Accept(KS.End, KS.Try) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Catches, m_TryCode, m_FinallyBlock)

        Return result
    End Function

    ''' <summary>
    ''' CatchStatement  ::=
    '''	   "Catch" [  Identifier "As" NonArrayTypeName  ]  [ "When" BooleanExpression  ]  StatementTerminator
    '''	      [  Block  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseCatchStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As CatchStatement
        Dim result As New CatchStatement(Parent)

        Dim m_Code As CodeBlock
        Dim m_Variable As Identifier = Nothing
        Dim m_When As Expression = Nothing
        Dim m_TypeName As NonArrayTypeName = Nothing

        tm.AcceptIfNotInternalError(KS.Catch)

        If tm.AcceptEndOfStatement(IsOneLiner) = False Then
            m_Variable = ParseIdentifier(result)
            If m_Variable IsNot Nothing Then
                If tm.AcceptIfNotError(KS.As) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                m_TypeName = ParseNonArrayTypeName(result)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.Accept(KS.When) Then
                m_When = ParseExpression(result)
                If m_When Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        result.Init(m_Variable, m_TypeName, m_When, m_Code)

        Return result
    End Function
    ''' <summary>
    ''' IfStatement  ::=  BlockIfStatement  |  LineIfThenStatement
    ''' BlockIfStatement  ::=
    '''	   "If" BooleanExpression  [ "Then" ]  StatementTerminator
    '''	        [  Block  ]
    '''	   [  ElseIfStatement+  ]
    '''	   [  ElseStatement  ]
    '''	   "End" "If" StatementTerminator
    ''' ElseIfStatement  ::=
    '''	   "ElseIf" BooleanExpression  [ "Then" ]  StatementTerminator
    '''	        [  Block  ]
    ''' ElseStatement  ::=
    '''	   "Else" StatementTerminator
    '''	        [  Block  ]
    ''' LineIfThenStatement  ::=
    '''	   "If" BooleanExpression "Then" Statements  [ "Else" Statements  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseIfStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As IfStatement
        Dim result As New IfStatement(Parent)

        Dim m_Condition As Expression
        Dim m_TrueCode As CodeBlock
        Dim m_FalseCode As CodeBlock
        Dim m_OneLiner As Boolean
        Dim m_ElseIfs As BaseObjects(Of ElseIfStatement)

        tm.AcceptIfNotInternalError(KS.If)
        m_Condition = ParseExpression(result)
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Then) = False Then
            m_OneLiner = False 'Cannot be a oneliner if Then is not found.
            If IsOneLiner Then
                Helper.AddError(Compiler, tm.CurrentLocation, "report error BC30081, 'if' must end with a matching 'end if'")
                tm.GotoNewline(False)
            Else
                tm.AcceptEndOfStatement(False, True)
            End If
        Else
            If IsOneLiner = False Then
                m_OneLiner = Not tm.AcceptEndOfStatement(False, False)
            Else
                m_OneLiner = True
            End If
        End If

        m_TrueCode = ParseCodeBlock(result, m_OneLiner)
        If m_TrueCode Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_ElseIfs = New BaseObjects(Of ElseIfStatement)(result)
        While tm.CurrentToken = KS.ElseIf OrElse (m_OneLiner = False AndAlso tm.CurrentToken = KS.Else AndAlso tm.PeekToken = KS.If)
            Dim newElseIf As ElseIfStatement
            newElseIf = ParseElseIfStatement(result, m_OneLiner)
            m_ElseIfs.Add(newElseIf)
        End While

        If tm.Accept(KS.Else) Then
            If m_OneLiner = False Then
                If tm.AcceptEndOfStatement(False, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
            m_FalseCode = ParseCodeBlock(result, m_OneLiner)
            If m_FalseCode Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_FalseCode = Nothing
        End If

        If m_OneLiner = False Then tm.AcceptIfNotError(KS.End, KS.If)

        result.Init(m_Condition, m_FalseCode, m_TrueCode, m_OneLiner, m_ElseIfs)

        Return result
    End Function

    ''' <summary>
    ''' ElseIfStatement  ::=
    '''	   "ElseIf" BooleanExpression  [  Then  ]  StatementTerminator
    '''	        [  Block  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseElseIfStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As ElseIfStatement
        Dim result As New ElseIfStatement(Parent)

        Dim m_Condition As Expression
        Dim m_Code As CodeBlock

        If tm.Accept(KS.Else) Then
            'This is not in the spec, but MS is accepting it anyway.
            'See test Bugs/aspnet2.vb for a test case.
            tm.AcceptIfNotInternalError(KS.If)
        Else
            tm.AcceptIfNotInternalError(KS.ElseIf)
        End If
        m_Condition = ParseExpression(result)
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        'ElseIf cannot be a oneliner...
        tm.Accept(KS.Then) '"Then" is not required.
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If IsOneLiner Then
            Helper.AddError(Compiler, tm.CurrentLocation)
            'TODO: Add error, 
        End If

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Code, m_Condition)

        Return result
    End Function

    ''' <summary>
    ''' SelectStatement  ::=
    '''	   "Select" [ "Case" ]  Expression  StatementTerminator
    '''	        [  CaseStatement+  ]
    '''	        [  CaseElseStatement  ]
    '''	   "End" "Select" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseSelectStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As SelectStatement
        Dim result As New SelectStatement(Parent)

        Dim m_Test As Expression
        Dim m_Cases As BaseObjects(Of CaseStatement)

        tm.AcceptIfNotInternalError(KS.Select)

        tm.Accept(KS.Case) '"Case" is not required

        m_Test = ParseExpression(result)
        If m_Test Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Cases = New BaseObjects(Of CaseStatement)(result)
        While tm.CurrentToken = KS.Case
            Dim newCase As CaseStatement
            newCase = ParseCaseStatement(result, IsOneLiner)
            m_Cases.Add(newCase)
        End While

        If tm.Accept(KS.End, KS.Select) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Test, m_Cases)

        Return result
    End Function

    ''' <summary>
    ''' CaseStatement  ::=
    '''	   "Case" CaseClauses  StatementTerminator
    '''	        [  Block  ]
    ''' CaseElseStatement  ::=
    '''	   "Case" "Else" StatementTerminator
    '''	   [  Block  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseCaseStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As CaseStatement
        Dim result As New CaseStatement(Parent)

        Dim m_IsElse As Boolean
        Dim m_Clauses As CaseClauses
        Dim m_Block As CodeBlock

        tm.AcceptIfNotInternalError(KS.Case)
        If tm.Accept(KS.Else) Then
            m_IsElse = True
            m_Clauses = Nothing
        Else
            m_Clauses = New CaseClauses(result)
            If ParseList(Of CaseClause)(m_Clauses, New ParseDelegate_Parent(Of CaseClause)(AddressOf ParseCaseClause), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If
        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        m_Block = ParseCodeBlock(result, IsOneLiner)
        If m_Block Is Nothing Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        result.Init(m_IsElse, m_Clauses, m_Block)

        Return result
    End Function

    Private Function ParseCallStatement(ByVal Parent As ParsedObject) As CallStatement
        Dim result As New CallStatement(Parent)

        Dim m_Target As Expression
        tm.AcceptIfNotInternalError(KS.Call)
        m_Target = ParseExpression(result)

        result.Init(m_Target)

        Return result
    End Function

    ''' <summary>
    ''' ForEachStatement  ::=
    '''	   "For" "Each" LoopControlVariable "In" Expression  StatementTerminator
    '''	         [  Block  ]
    '''	   "Next" [Expression  ]  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseForEachStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As ForEachStatement
        Dim result As New ForEachStatement(Parent)

        Dim m_LoopControlVariable As LoopControlVariable
        Dim m_InExpression As Expression
        Dim m_NextExpression As Expression
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.For)
        tm.AcceptIfNotInternalError(KS.Each)

        m_LoopControlVariable = ParseLoopControlVariable(result)
        If m_LoopControlVariable Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.In) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        tm.AcceptNewLine()

        m_InExpression = ParseExpression(result)
        If m_InExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.Next) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.CurrentToken.IsEndOfStatement = False Then
            m_NextExpression = ParseExpression(m_Code)
            If m_NextExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_NextExpression = Nothing
        End If

        result.Init(m_LoopControlVariable, m_InExpression, m_NextExpression, m_Code)

        Return result
    End Function

    ''' <summary>
    ''' ForStatement  ::=
    '''	   "For" LoopControlVariable  "="  Expression  "To"  Expression  [  "Step"  Expression  ]  StatementTerminator
    '''	      [ Block  ]
    '''	   "Next" [  NextExpressionList  ]  StatementTerminator
    ''' LoopControlVariable  ::=
    '''	   Identifier  [  ArrayNameModifier  ] "As" TypeName  |
    '''	   Expression
    ''' NextExpressionList  ::=
    '''	   Expression  |
    '''	   NextExpressionList "," Expression
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseForStatement(ByVal Parent As ParsedObject, ByVal IsOneLiner As Boolean) As ForStatement
        Dim result As New ForStatement(Parent)

        Dim m_LoopControlVariable As LoopControlVariable
        Dim m_LoopStartExpression As Expression
        Dim m_LoopEndExpression As Expression
        Dim m_LoopStepExpression As Expression
        Dim m_NextExpressionList As ExpressionList
        Dim m_Code As CodeBlock

        tm.AcceptIfNotInternalError(KS.For)
        m_LoopControlVariable = ParseLoopControlVariable(result)
        If m_LoopControlVariable Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        tm.AcceptNewLine()

        m_LoopStartExpression = ParseExpression(result)
        If m_LoopStartExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.To) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_LoopEndExpression = ParseExpression(result)
        If m_LoopEndExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Step) Then
            m_LoopStepExpression = ParseExpression(result)
            If m_LoopStepExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_LoopStepExpression = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Next) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC30084, tm.CurrentLocation)
            Return result
        End If

        If tm.CurrentToken.IsEndOfStatement = False Then
            m_NextExpressionList = New ExpressionList(result)
            If ParseList(Of Expression)(m_NextExpressionList, New ParseDelegate_Parent(Of Expression)(AddressOf ParseExpression), result) = False Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        Else
            m_NextExpressionList = Nothing
        End If

        result.Init(m_LoopControlVariable, m_LoopStartExpression, m_LoopEndExpression, m_LoopStepExpression, m_NextExpressionList, m_Code)

        Return result
    End Function

    Private Function ParseCaseClause(ByVal Parent As ParsedObject) As CaseClause
        Dim result As New CaseClause(Parent)

        Dim m_Expression1 As Expression
        Dim m_Expression2 As Expression = Nothing
        Dim m_Comparison As KS

        If tm.Accept(KS.Is) Then
            tm.AcceptNewLine()
            If tm.CurrentToken.Equals(CaseClause.RelationalOperators) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30239, tm.CurrentLocation)
                m_Comparison = KS.Equals
            Else
                m_Comparison = tm.CurrentToken.Symbol
                tm.NextToken()
                tm.AcceptNewLine()
            End If
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        ElseIf tm.CurrentToken.Equals(CaseClause.RelationalOperators) Then
            m_Comparison = tm.CurrentToken.Symbol
            tm.NextToken()
            tm.AcceptNewLine()
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.Accept(KS.To) Then
                m_Expression2 = ParseExpression(result)
                If m_Expression2 Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If
        End If

        result.Init(m_Expression1, m_Expression2, m_Comparison)

        Return result
    End Function

    ''' <summary>
    ''' AddHandlerStatement  ::= "AddHandler" Expression  ,  Expression  StatementTerminator
    ''' RemoveHandlerStatement  ::= "RemoveHandler" Expression "," Expression  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAddOrRemoveHandlerStatement(ByVal Parent As ParsedObject) As AddOrRemoveHandlerStatement
        Dim result As New AddOrRemoveHandlerStatement(Parent)

        Dim m_Event As Expression
        Dim m_EventHandler As Expression
        Dim m_IsAddHandler As Boolean

        If tm.Accept(KS.AddHandler) Then
            m_IsAddHandler = True
        ElseIf tm.Accept(KS.RemoveHandler) Then
            m_IsAddHandler = False
        Else
            Throw New InternalException(result)
        End If

        m_Event = ParseExpression(result)
        If m_Event Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        m_EventHandler = ParseExpression(result)
        If m_EventHandler Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Event, m_EventHandler, m_IsAddHandler)

        Return result
    End Function

    Private Function ParseImportsStatements(ByVal Parent As ParsedObject) As Generic.List(Of ImportsStatement)
        Dim result As New Generic.List(Of ImportsStatement)
        While ImportsStatement.IsMe(tm)
            Dim newI As ImportsStatement
            newI = ParseImportsStatement(Parent)
            result.Add(newI)
        End While
        Return result
    End Function

    ''' <summary>
    ''' ImportsStatement  ::=  "Imports" ImportsClauses  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseImportsStatement(ByVal Parent As ParsedObject) As ImportsStatement
        Dim result As New ImportsStatement(Parent)

        Dim m_Clauses As ImportsClauses

        tm.AcceptIfNotInternalError(KS.Imports)

        m_Clauses = ParseImportsClauses(result)
        If m_Clauses Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.Init(m_Clauses)

        Return result
    End Function

    ''' <summary>
    ''' Parses a imports statement as specified on the commandline.
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseImportsStatement(ByVal Parent As ImportsStatement, ByVal str As String) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Parent IsNot Nothing)
        Helper.Assert(Parent.Clauses IsNot Nothing)

        result = ParseImportsClauses(Parent.Clauses, str) AndAlso result

        Return result
    End Function

    Private Function GetPartialType(ByVal Parent As ParsedObject, ByVal m_Identifier As Identifier, ByVal m_TypeParameters As TypeParameters, ByVal m_Modifiers As Modifiers, ByVal IsClass As Boolean, ByVal [Namespace] As String) As PartialTypeDeclaration
        Dim result As PartialTypeDeclaration

        'Get the actual name of the type including generic number
        Dim CompleteName As String
        Dim GenericName As Identifier
        If m_TypeParameters Is Nothing Then
            GenericName = m_Identifier
        Else
            GenericName = New Identifier(Helper.CreateGenericTypename(m_Identifier.Name, m_TypeParameters.Parameters.Count))
        End If

        CompleteName = GenericName.Name
        If TypeOf Parent Is AssemblyDeclaration AndAlso [Namespace] <> String.Empty Then
            CompleteName = [Namespace] & "." & CompleteName
        End If

        'Try to find the type in the parent
        Dim partialType As TypeDeclaration = FindTypeInParent(Parent, CompleteName)
        Dim partialClassOrStruct As PartialTypeDeclaration = TryCast(partialType, PartialTypeDeclaration)

        If partialType IsNot Nothing Then
            'There is already a type with the same name
            result = partialClassOrStruct
            result.IsPartial = True
            result.Modifiers = result.Modifiers.AddModifiers(m_Modifiers.Mask)
            result.PartialModifierFound = result.Modifiers.Is(ModifierMasks.Partial) OrElse m_Modifiers.Is(ModifierMasks.Partial)
        ElseIf partialType IsNot Nothing Then
            'There is another type with the same name
            Helper.AddError(tm.Compiler, tm.CurrentLocation, "Two types with the same name: " & m_Identifier.Name)
            Return Nothing
        Else
            'No type with the same name.
            If IsClass Then
                result = New ClassDeclaration(Parent, [Namespace], GenericName, m_TypeParameters)
            Else
                result = New StructureDeclaration(Parent, [Namespace], GenericName, m_TypeParameters)
            End If
            result.Modifiers = m_Modifiers
        End If

        Return result
    End Function
    ''' <summary>
    ''' ClassDeclaration  ::=
    '''	[  Attributes  ]  [  ClassModifier+  ]  "Class"  Identifier  [  TypeParameters  ]  StatementTerminator
    '''	[  ClassBase  ]
    '''	[  TypeImplementsClause+  ]
    '''	[  ClassMemberDeclaration+  ]
    '''	"End" "Class" StatementTerminator
    ''' 
    ''' ClassBase ::= Inherits NonArrayTypeName StatementTerminator
    ''' </summary>
    ''' <param name="Parent">Should be the declaring type of the assembly itself it is not a nested type.</param>
    ''' <param name="Attributes"></param>
    ''' <param name="Namespace"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseClassDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As ClassDeclaration
        Dim result As ClassDeclaration
        Dim partialType As PartialTypeDeclaration

        Dim m_Attributes As Attributes
        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_TypeParameters As TypeParameters
        Dim m_Inherits As NonArrayTypeName
        Dim m_DeclaringType As TypeDeclaration

        m_DeclaringType = TryCast(Parent, TypeDeclaration)
        Helper.Assert(m_DeclaringType IsNot Nothing OrElse TypeOf Parent Is AssemblyDeclaration)

        m_Attributes = Attributes
        m_Modifiers = ParseModifiers(ModifierMasks.ClassModifiers)

        tm.AcceptIfNotInternalError(KS.Class)

        m_Identifier = ParseIdentifier(CType(Nothing, ParsedObject))
        If m_Identifier Is Nothing Then
            ShowIdentifierExpected(tm.CurrentLocation())
            Return Nothing
        End If

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(Nothing)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeParameters = Nothing
        End If

        'Here we have enough information to know if it's a partial type or not
        partialType = GetPartialType(Parent, m_Identifier, m_TypeParameters, m_Modifiers, True, [Namespace])

        result = TryCast(partialType, ClassDeclaration)
        If result Is Nothing Then
            If partialType IsNot Nothing Then
                Helper.AddError(tm.Compiler, tm.CurrentLocation, "Partial types must be either all classes or all structures.")
            Else
                'Error message has already been shown
            End If
            Return Nothing
        End If

        m_Identifier.Parent = result
        If m_TypeParameters IsNot Nothing Then
            m_TypeParameters.Parent = result
        End If

        If tm.Accept(KS.Inherits) Then
            m_Inherits = ParseNonArrayTypeName(result)
            If m_Inherits Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_Inherits = Nothing
        End If
        If m_Inherits IsNot Nothing Then result.AddInheritsClause(m_Inherits)

        If TypeImplementsClauses.IsMe(tm) Then
            result.Implements = ParseTypeImplementsClauses(result)
            If result.Implements Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If ParseTypeMembers(result) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Class) = False Then
            tm.GotoNewline(False)
        End If
        If tm.AcceptEndOfStatement(, True) = False Then
            tm.GotoNewline(True)
        End If

        If Attributes IsNot Nothing Then
            If result.CustomAttributes IsNot Nothing Then
                result.CustomAttributes.AddRange(Attributes)
            Else
                result.CustomAttributes = Attributes
            End If
        End If

        Return result
    End Function

    Private Function FindTypeInParent(ByVal Parent As ParsedObject, ByVal CompleteName As String) As TypeDeclaration
        Dim assemblyParent As AssemblyDeclaration = TryCast(Parent, AssemblyDeclaration)
        Dim typeParent As TypeDeclaration = TryCast(Parent, TypeDeclaration)
        Dim partialType As TypeDeclaration = Nothing
        Dim partialTypes As Generic.List(Of INameable)

        If assemblyParent IsNot Nothing Then
            partialType = assemblyParent.FindTypeWithFullname(CompleteName)
        ElseIf typeParent IsNot Nothing Then
            partialTypes = typeParent.Members.Index.Item(CompleteName)
            If partialTypes IsNot Nothing Then
                If partialTypes.Count = 1 Then
                    partialType = TryCast(partialTypes(0), TypeDeclaration)
                    If partialType Is Nothing Then
                        Helper.AddError(Compiler, tm.CurrentLocation, "Already a member with the name " & CompleteName)
                    End If
                ElseIf partialTypes.Count > 1 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Already a member with the name " & CompleteName)
                End If
            End If
        End If
        Return partialType
    End Function

    ''' <summary>
    ''' DelegateDeclaration  ::=
    ''' [  Attributes  ]  [  TypeModifier+  ]  "Delegate" MethodSignature  StatementTerminator
    ''' MethodSignature  ::=  SubSignature  |  FunctionSignature
    ''' 
    ''' LAMESPEC: should be something like:
    ''' [  Attributes  ]  [  TypeModifier+  ]  "Delegate" FunctionOrSub MethodSignature  StatementTerminator
    ''' FunctionOrSub ::= "Function" | "Sub"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseDelegateDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As DelegateDeclaration
        Dim result As DelegateDeclaration

        Dim Modifiers As Modifiers
        Dim m_Signature As SubSignature
        Dim isSub As Boolean

        Modifiers = ParseModifiers(ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Delegate)

        If tm.Accept(KS.Function) Then
            isSub = False
        ElseIf tm.Accept(KS.Sub) Then
            isSub = True
        Else
            Throw New InternalException(Parent)
        End If

        If isSub Then
            m_Signature = ParseSubSignature(Parent)
        Else
            m_Signature = ParseFunctionSignature(Parent)
        End If

        result = New DelegateDeclaration(Parent, [Namespace], m_Signature)

        m_Signature.Parent = result

        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Attributes
        result.Modifiers = Modifiers

        Return result
    End Function

    ''' <summary>
    ''' Parses enum members.
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseEnumMembers(ByVal Parent As EnumDeclaration) As Boolean
        Dim newConst As EnumMemberDeclaration
        Dim constAttributes As Attributes

        Do Until tm.CurrentToken.Equals(KS.End, KS.Enum)
            constAttributes = Nothing
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, constAttributes) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            End If

            newConst = ParseEnumMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, constAttributes), Parent.Members.Count)
            If newConst Is Nothing Then
                Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
                Return False
            End If
            Parent.Members.Add(newConst)
        Loop

        Return True
    End Function

    ''' <summary>
    ''' EnumDeclaration  ::=
    '''	[  Attributes  ]  [  TypeModifier+  ]  "Enum"  Identifier  [  "As"  IntegralTypeName  ]  StatementTerminator
    '''	   EnumMemberDeclaration+
    '''	"End" "Enum"  StatementTerminator
    ''' 
    ''' LAMESPEC: IntegralTypeName is QualifiedName in the spec. (QualifiedName doesn't exist...)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseEnumDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As EnumDeclaration
        Dim result As EnumDeclaration
        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim has_error As Boolean
        Dim location As Span

        m_Modifiers = ParseModifiers(ModifierMasks.TypeModifiers)

        location = tm.CurrentLocation
        tm.AcceptIfNotInternalError(KS.Enum)

        m_Identifier = ParseIdentifier()
        If m_Identifier Is Nothing Then
            ShowIdentifierExpected(location)
            tm.GotoNewline(True)
            m_Identifier = New Identifier("dummy")
            has_error = True
        ElseIf m_Identifier.HasTypeCharacter Then
            Compiler.Report.ShowMessage(Messages.VBNC30468, location)
            has_error = True
        End If

        result = New EnumDeclaration(Parent, [Namespace], m_Identifier)
        result.Location = location

        If tm.Accept(KS.As) Then
            If tm.CurrentToken.IsKeyword Then
                Select Case tm.CurrentToken.Keyword
                    Case KS.Byte
                        result.EnumConstantType = Compiler.TypeCache.System_Byte
                    Case KS.SByte
                        result.EnumConstantType = Compiler.TypeCache.System_SByte
                    Case KS.Short
                        result.EnumConstantType = Compiler.TypeCache.System_Int16
                    Case KS.UShort
                        result.EnumConstantType = Compiler.TypeCache.System_UInt16
                    Case KS.Integer
                        result.EnumConstantType = Compiler.TypeCache.System_Int32
                    Case KS.UInteger
                        result.EnumConstantType = Compiler.TypeCache.System_UInt32
                    Case KS.Long
                        result.EnumConstantType = Compiler.TypeCache.System_Int64
                    Case KS.ULong
                        result.EnumConstantType = Compiler.TypeCache.System_UInt64
                    Case Else
                        'Just set anything that will cause the correct error to be shown
                        result.EnumConstantType = Compiler.TypeCache.System_Object
                End Select
                tm.NextToken()
            Else
                result.EnumType = ParseNonArrayTypeName(result)
                If result.EnumType Is Nothing AndAlso has_error = False Then
                    Compiler.Report.ShowMessage(Messages.VBNC30182, tm.CurrentLocation)
                    tm.GotoNewline(True)
                    has_error = True
                End If
            End If
        End If

        If tm.AcceptEndOfStatement(, Not has_error) = False AndAlso has_error = False Then
            tm.GotoNewline(True)
            has_error = True
        End If

        If ParseEnumMembers(result) = False AndAlso has_error = False Then
            tm.GotoNewline(True)
            has_error = True
        End If

        If Not has_error AndAlso result.Members.Count = 0 Then
            Compiler.Report.ShowMessage(Messages.VBNC30280, result.Location, result.Name)
            has_error = True
        End If

        If tm.AcceptIfNotError(KS.End, KS.Enum) = False Then tm.GotoNewline(True)
        If tm.AcceptEndOfStatement(, True) = False Then tm.GotoNewline(True)

        result.CustomAttributes = Attributes
        result.Modifiers = m_Modifiers

        Return result
    End Function

    ''' <summary>
    ''' InterfaceDeclaration  ::=
    '''	[  Attributes  ]  [  TypeModifier+  ]  "Interface" Identifier  [  TypeParameters  ]  StatementTerminator
    '''	[  InterfaceBase+  ]
    '''	[  InterfaceMemberDeclaration+  ]
    '''	"End" "Interface" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseInterfaceDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As InterfaceDeclaration
        Dim result As InterfaceDeclaration

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_GenericName As Identifier
        Dim m_TypeParameters As TypeParameters

        m_Modifiers = ParseModifiers(ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Interface)

        m_Identifier = ParseIdentifier()
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(Parent)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            m_GenericName = Helper.CreateGenericTypename(m_Identifier, m_TypeParameters)
        Else
            m_TypeParameters = Nothing
            m_GenericName = m_Identifier
        End If

        result = New InterfaceDeclaration(Parent, [Namespace], m_GenericName, m_TypeParameters)

        If m_TypeParameters IsNot Nothing Then
            m_TypeParameters.Parent = result
        End If

        If InterfaceBases.IsMe(tm) Then
            result.InterfaceBases = ParseInterfaceBases(result)
            If result.InterfaceBases Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        End If

        If ParseInterfaceMembers(result) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Interface) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Attributes
        result.Modifiers = m_Modifiers

        Return result
    End Function

    ''' <summary>
    ''' ModuleDeclaration  ::=
    '''	[  Attributes  ]  [  TypeModifier+  ]  "Module"  Identifier  StatementTerminator
    '''	[  ModuleMemberDeclaration+  ]
    '''	"End" "Module" StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseModuleDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As ModuleDeclaration
        Dim result As ModuleDeclaration

        Dim m_Modifiers As Modifiers
        Dim m_Name As Identifier

        m_Modifiers = ParseModifiers(ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Module)

        m_Name = ParseIdentifier()
        If m_Name Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result = New ModuleDeclaration(Parent, [Namespace], m_Name)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If ParseTypeMembers(result) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Module) = False Then
            tm.GotoNewline(False)
        End If
        If tm.AcceptEndOfStatement(, True) = False Then
            tm.GotoNewline(True)
        End If

        If result.CustomAttributes IsNot Nothing Then
            result.CustomAttributes.AddRange(Attributes)
        Else
            result.CustomAttributes = Attributes
        End If
        result.Modifiers = m_Modifiers

        Return result
    End Function

    ''' <summary>
    ''' StructureDeclaration  ::=
    '''	[  Attributes  ]  [  StructureModifier+  ]  "Structure" Identifier  [  TypeParameters  ]	StatementTerminator
    '''	[  TypeImplementsClause+  ]
    '''	[  StructMemberDeclaration+  ]
    '''	"End" "Structure"  StatementTerminator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseStructureDeclaration(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal [Namespace] As String) As StructureDeclaration
        Dim result As StructureDeclaration = Nothing
        Dim partialType As PartialTypeDeclaration

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_TypeParameters As TypeParameters
        Dim m_DeclaringType As TypeDeclaration
        Dim m_Attributes As Attributes

        m_DeclaringType = TryCast(Parent, TypeDeclaration)
        Helper.Assert(m_DeclaringType IsNot Nothing OrElse TypeOf Parent Is AssemblyDeclaration)

        m_Attributes = Attributes
        m_Modifiers = ParseModifiers(ModifierMasks.StructureModifiers)

        tm.AcceptIfNotInternalError(KS.Structure)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(result)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        Else
            m_TypeParameters = Nothing
        End If

        'Here we have enough information to know if it's a partial type or not
        partialType = GetPartialType(Parent, m_Identifier, m_TypeParameters, m_Modifiers, False, [Namespace])

        result = TryCast(partialType, StructureDeclaration)
        If result Is Nothing Then
            If partialType IsNot Nothing Then
                Helper.AddError(tm.Compiler, tm.CurrentLocation, "Partial types must be either all classes or all structures.")
            Else
                'Error message has already been shown
            End If
            Return Nothing
        End If

        m_Identifier.Parent = result
        If m_TypeParameters IsNot Nothing Then
            m_TypeParameters.Parent = result
        End If

        result.Implements = ParseTypeImplementsClauses(result)
        If result.Implements Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If ParseTypeMembers(result) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Structure) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result.CustomAttributes = Attributes

        Return result
    End Function

    Public Sub ShowIdentifierExpected(ByVal Location As Span)
        If tm.CurrentToken.IsKeyword Then
            Compiler.Report.ShowMessage(Messages.VBNC30183, Location)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30203, Location)
        End If
    End Sub
End Class

