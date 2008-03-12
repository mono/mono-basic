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

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If

Partial Public Class Parser
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
        Dim m_Imports As ImportsClauses = CodeFile.Imports

        While tm.CurrentToken.Equals(KS.Option)
            If OptionExplicitStatement.IsMe(tm) Then
                If m_OptionExplicit IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Explicit") AndAlso result
                End If
                m_OptionExplicit = ParseOptionExplicitStatement(CodeFile)
                If m_OptionExplicit Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf OptionStrictStatement.IsMe(tm) Then
                If m_OptionStrict IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Strict") AndAlso result
                End If
                m_OptionStrict = ParseOptionStrictStatement(CodeFile)
                If m_OptionStrict Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf OptionCompareStatement.IsMe(tm) Then
                If m_OptionCompare IsNot Nothing Then
                    result = Compiler.Report.ShowMessage(Messages.VBNC30225, tm.CurrentLocation, "Compare") AndAlso result
                End If
                m_OptionCompare = ParseOptionCompareStatement(CodeFile)
                If m_OptionCompare Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        CodeFile.Init(m_OptionCompare, m_OptionStrict, m_OptionExplicit, m_Imports)

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

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_IsBinary)

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
            Helper.ErrorRecoveryNotImplemented()
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
                If newClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                If Parent.Exists(newClause) Then
                    If newClause.IsNamespaceClause Then '
                        'ignore the duplication
                    ElseIf newClause.IsAliasClause Then
                        Parent.Compiler.Report.SaveMessage(Messages.VBNC30572, newClause.AsAliasClause.Name)
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
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.Init(m_Clause)
        Else
            Dim m_Clause As ImportsNamespaceClause
            m_Clause = ParseImportsNamespaceClause(Parent, str)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.Init(m_Clause)
        Else
            Dim m_Clause As ImportsNamespaceClause
            m_Clause = ParseImportsNamespaceClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        tm.AcceptIfNotInternalError(KS.Equals)

        m_Second = ParseImportsNamespaceClause(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If result IsNot Nothing AndAlso tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of Then
            Dim ctn As ConstructedTypeName = Nothing
            tm.RestoreToPoint(iCurrent)
            ctn = ParseConstructedTypeName(result)
            If ctn Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        Dim AssemblyTypes As New MemberDeclarations(assembly)

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
                Helper.ErrorRecoveryNotImplemented()
            End If
            ''	[  AttributesStatement+  ]
            'If vbnc.Attributes.IsMe(tm) Then
            '    If Me.ParseAttributes(result, AssemblyAttributes) = False Then
            '        Helper.ErrorRecoveryNot    Implemented()
            '    End If
            'End If

            '	[  NamespaceMemberDeclaration+  ]
            result = ParseAssemblyMembers(assembly, RootNamespace, AssemblyTypes) AndAlso result

            While tm.AcceptNewLine

            End While
            tm.AcceptEndOfFile()
            If iLastLocation.Equals(tm.CurrentLocation) Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30203, tm.CurrentLocation) AndAlso result
                tm.GotoNewline(False)
            End If
        Loop

        assembly.Init(AssemblyTypes, AssemblyAttributes)

        Return result
    End Function

    ''' <summary>
    '''  Attributes ::=	AttributeBlock  |	Attributes  AttributeBlock
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributes(ByVal Parent As ParsedObject, ByVal Attributes As Attributes) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Attributes IsNot Nothing)

        While AttributeBlock.IsMe(tm)
            If ParseAttributeBlock(Parent, Attributes) = False Then
                Helper.ErrorRecoveryNotImplemented()
            End If
        End While

        Return result
    End Function
    ''' <summary>
    '''  Parses attributes (if any). Always returns something.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseAttributes(ByVal Parent As ParsedObject) As Attributes
        Dim result As New Attributes(Parent)

        If Attributes.IsMe(tm) Then
            While AttributeBlock.IsMe(tm)
                If ParseAttributeBlock(Parent, result) = False Then
                    Helper.ErrorRecoveryNotImplemented()
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

        If ParseAttributeList(Parent, Attributes) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        result = tm.AcceptIfNotError(KS.GT) AndAlso result

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
            If Attribute Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If tm.AcceptIfNotError(KS.Colon) = False Then Helper.ErrorRecoveryNotImplemented()
        ElseIf tm.Accept(KS.Module) Then
            m_IsModule = True
            If tm.AcceptIfNotError(KS.Colon) = False Then Helper.ErrorRecoveryNotImplemented()
        End If

        m_SimpleTypeName = ParseSimpleTypeName(result)
        If m_SimpleTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.LParenthesis) Then
            If tm.CurrentToken <> KS.RParenthesis Then
                m_AttributeArguments = ParseAttributeArguments(result)
                If m_AttributeArguments Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
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
                    Helper.ErrorRecoveryNotImplemented()
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
                Helper.ErrorRecoveryNotImplemented()
            End If
        End If

        result.Init(m_AttributePositionalArgumentList, m_VariablePropertyInitializerList)

        Return result
    End Function

    ''' <summary>
    ''' Parses lists of type List ::= Item | List "," Item
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseList(Of T)(ByVal List As BaseList(Of T), ByVal ParseMethod As ParseDelegate_Parent(Of T), ByVal Parent As ParsedObject) As Boolean
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
        If m_IdentifierOrKeyword Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        tm.AcceptIfNotInternalError(KS.Colon)
        tm.AcceptIfNotInternalError(KS.Equals)

        m_AttributeArgumentExpression = ParseAttributeArgumentExpression(result)
        If m_AttributeArgumentExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            If qn Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.CurrentToken <> KS.LBrace Then
            If ShowErrors Then tm.AcceptIfNotError(KS.LBrace)
            Return Nothing
        End If

        m_ArrayElementInitializer = ParseArrayElementInitializer(result)
        If m_ArrayElementInitializer Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
                Helper.ErrorRecoveryNotImplemented()
            End If

            If tm.AcceptIfNotError(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented()
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
            result.Init(newAEI)
        Else
            Dim newExp As Expression
            newExp = ParseExpression(Parent)
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
            If newATM Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.Init(newATM)
        ElseIf ArraySizeInitializationModifier.CanBeMe(tm) Then
            Dim newASIM As ArraySizeInitializationModifier
            newASIM = ParseArraySizeInitializationModifer(result)
            If newASIM Is Nothing Then
                If m_ShowErrors = False Then Return Nothing
                Helper.ErrorRecoveryNotImplemented()
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
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        If vbnc.ArrayTypeModifiers.CanBeMe(tm) Then
            m_ArrayTypeModifiers = ParseArrayTypeModifiers(result)
            If m_ArrayTypeModifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Loop

        If tmp.Count <= 0 Then Helper.ErrorRecoveryNotImplemented()

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
                If newI Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                m_Clauses.Add(newI)
            Loop While tm.Accept(KS.Comma)
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
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
                Helper.ErrorRecoveryNotImplemented()
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

            m_TypeArgumentList = ParseTypeArgumentList(result)
            If m_TypeArgumentList Is Nothing Then Return Nothing
            m_ConstructedTypeName = New ConstructedTypeName(result, m_SimpleTypeName.AsQualifiedIdentifier, m_TypeArgumentList)
            result.Init(m_ConstructedTypeName)
        Else
            result.Init(m_SimpleTypeName)
        End If


        Return result
    End Function

    ''' <summary>
    ''' ConstructedTypeName ::=	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseConstructedTypeName(ByVal Parent As ParsedObject) As ConstructedTypeName
        Dim result As New ConstructedTypeName(Parent)

        Dim m_QualifiedIdentifier As QualifiedIdentifier = Nothing
        Dim m_TypeArgumentList As TypeArgumentList = Nothing

        m_QualifiedIdentifier = ParseQualifiedIdentifier(result)
        If m_QualifiedIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        tm.AcceptIfNotInternalError(KS.LParenthesis)
        tm.AcceptIfNotInternalError(KS.Of)

        If ParseList(Of TypeName)(m_TypeArgumentList, New ParseDelegate_Parent(Of TypeName)(AddressOf ParseTypeName), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_QualifiedIdentifier, m_TypeArgumentList)

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

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

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
            If m_ArrayTypeModifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            If newATM Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

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
            If m_BuiltInTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            If m_QualifiedIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            If m_First Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If Token.IsSomething(m_Second) Then m_First = New QualifiedIdentifier(Parent, m_First, m_Second)
            If tm.CurrentToken.IsIdentifierOrKeyword Then
                m_Second = tm.CurrentToken
                tm.NextToken()
            Else
                Compiler.Report.ShowMessage(Messages.VBNC30203)
                Return Nothing
            End If
        End While

        result.Init(m_First, m_Second)

        Return result
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

    Private Function ParseModifiers(ByVal Parent As ParsedObject, ByVal ValidModifiers As ModifierMasks) As Modifiers
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
    Private Function ParseInterfaceMembers(ByVal Parent As InterfaceDeclaration) As MemberDeclarations
        Dim result As New MemberDeclarations(Parent)

        Dim newMembers As New Generic.List(Of IMember)
        While True
            Dim attributes As Attributes
            attributes = New Attributes(Parent)
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If

            Dim newType As TypeDeclaration
            newType = ParseTypeDeclaration(Parent, attributes, Parent.Namespace)
            If newType IsNot Nothing Then
                result.Add(newType)
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
                If attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If

            If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()

            result.Add(newMember)
        End While

        Return result
    End Function

    Private Function ParseTypeMembers(ByVal Parent As TypeDeclaration) As MemberDeclarations
        Dim result As New MemberDeclarations(Parent)
        If ParseTypeMembers(Parent, result) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If
        Return result
    End Function

    ''' <summary>
    ''' Parses type members for classes, modules and structures.
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <remarks></remarks>
    Private Function ParseTypeMembers(ByVal Parent As TypeDeclaration, ByVal Members As MemberDeclarations) As Boolean
        Dim result As Boolean = True
        Dim isModuleDeclaration As Boolean = TypeOf Parent Is ModuleDeclaration

        Helper.Assert(TypeOf Parent Is ClassDeclaration OrElse isModuleDeclaration OrElse TypeOf Parent Is StructureDeclaration)

        Dim newMembers As New Generic.List(Of IMember)
        While True
            Dim attributes As Attributes
            attributes = New Attributes(Parent)
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If

            Dim newType As TypeDeclaration
            newType = ParseTypeDeclaration(Parent, attributes, Parent.Namespace)
            If newType IsNot Nothing Then
                Members.Add(newType)
                Continue While
            End If

            Dim newMember As IMember

            'Class and Structure declarations
            If isModuleDeclaration = False AndAlso OperatorDeclaration.IsMe(tm) Then
                newMember = ParseOperatorDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf isModuleDeclaration = False AndAlso ConversionOperatorDeclaration.IsMe(tm) Then
                newMember = ParseConversionOperatorDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                'Class, Structure and Module declarations
            ElseIf RegularEventDeclaration.IsMe(tm) Then
                newMember = ParseRegularEventDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf CustomEventDeclaration.IsMe(tm) Then
                newMember = ParseCustomEventMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf VariableDeclaration.IsMe(tm) Then
                Dim tmp As Generic.List(Of VariableDeclaration)
                tmp = ParseVariableMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If tmp Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                For Each item As VariableDeclaration In tmp
                    newMembers.Add(item)
                Next
                newMember = Nothing
            ElseIf ConstantDeclaration.IsMe(tm) Then
                Dim tmp As Generic.List(Of ConstantDeclaration)
                tmp = ParseConstantMemberDeclarations(Parent, New ParseAttributableInfo(Compiler, attributes))
                If tmp Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                For Each item As ConstantDeclaration In tmp
                    newMembers.Add(item)
                Next
                newMember = Nothing
            ElseIf ExternalSubDeclaration.IsMe(tm) Then
                newMember = ParseExternalSubDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf ExternalFunctionDeclaration.IsMe(tm) Then
                newMember = ParseExternalFunctionDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf SubDeclaration.IsMe(tm) Then
                newMember = ParseSubDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf FunctionDeclaration.IsMe(tm) Then
                newMember = ParseFunctionDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf RegularPropertyDeclaration.IsMe(tm) Then
                newMember = ParseRegularPropertyMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf MustOverridePropertyDeclaration.IsMe(tm) Then
                newMember = ParseMustOverridePropertyMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            ElseIf ConstructorDeclaration.IsMe(tm) Then
                newMember = ParseConstructorMember(Parent, New ParseAttributableInfo(Compiler, attributes))
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            Else
                If attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If

            If newMember IsNot Nothing Then newMembers.Add(newMember)
            If newMembers.Count = 0 Then Helper.ErrorRecoveryNotImplemented()
            Members.AddRange(newMembers)
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

    Private Function ParseAssemblyMembers(ByVal Parent As AssemblyDeclaration, ByVal RootNamespace As String, ByVal declarations As MemberDeclarations) As Boolean
        Dim result As Boolean = True
        Dim currentNameSpace As String = RootNamespace
        Dim currentNamespaces As New Generic.List(Of QualifiedIdentifier)

        Helper.Assert(declarations IsNot Nothing)

        While True
            Dim attributes As Attributes
            attributes = New Attributes(Parent)
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, attributes) = False Then Helper.ErrorRecoveryNotImplemented()

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
                declarations.Add(newType)
            ElseIf tm.Accept(KS.Namespace) Then
                Dim qi As QualifiedIdentifier
                qi = ParseQualifiedIdentifier(Parent)
                If qi Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                currentNamespaces.Add(qi)
                currentNameSpace = RootNamespace
                If currentNamespaces.Count > 0 Then
                    If currentNameSpace <> "" Then currentNameSpace &= "."
                    For i As Integer = 0 To currentNamespaces.Count - 2
                        currentNameSpace &= currentNamespaces(i).Name & "."
                    Next
                    currentNameSpace &= currentNamespaces(currentNamespaces.Count - 1).Name
                End If
                If tm.AcceptNewLine(True, True, True) = False Then Helper.ErrorRecoveryNotImplemented()
            ElseIf tm.Accept(KS.End, KS.Namespace) Then
                If tm.AcceptNewLine(True, False, True) = False Then Helper.ErrorRecoveryNotImplemented()
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
                    Helper.AddError(Compiler, tm.CurrentLocation, "'End Namespace' without 'Namespace'.")
                End If
            Else
                If attributes.Count > 0 Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Hanging attributes.")
                End If
                Exit While
            End If
        End While
        Return result
    End Function


End Class
