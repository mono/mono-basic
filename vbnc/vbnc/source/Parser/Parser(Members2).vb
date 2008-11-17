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


Partial Class Parser
    ''' <summary>
    ''' ConstantMemberDeclaration  ::=	[  Attributes  ]  [  ConstantModifier+  ]  "Const"  ConstantDeclarators  StatementTerminator
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Function ParseConstantMemberDeclarations(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo) As Generic.List(Of ConstantDeclaration)
        Dim result As New Generic.List(Of ConstantDeclaration)

        Dim m_Modifiers As Modifiers

        m_Modifiers = ParseModifiers(Parent, ModifierMasks.ConstantModifiers)

        tm.AcceptIfNotInternalError(KS.Const)
        m_Modifiers.AddModifiers(ModifierMasks.Const)

        result = ParseConstantDeclarations(Parent, Info.Attributes, m_Modifiers)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        Return result
    End Function

    Private Function ParseConstantDeclarations(ByVal Parent As ParsedObject, ByVal Attributes As Attributes, ByVal Modifiers As Modifiers) As Generic.List(Of ConstantDeclaration)
        Dim result As New Generic.List(Of ConstantDeclaration)

        Do
            Dim newCD As ConstantDeclaration = Nothing
            newCD = ParseConstantDeclaration(Parent, New ParseAttributableInfo(Parent.Compiler, Attributes), Modifiers)
            If newCD Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        m_Modifiers = ParseModifiers(result, ModifierMasks.MustOverridePropertyModifiers)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If MemberImplementsClause.IsMe(tm) Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Signature, m_ImplementsClause)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.ConversionOperatorModifiers)

        tm.AcceptIfNotInternalError(KS.Operator)

        If vbnc.ConversionOperatorDeclaration.IsOverloadableConversionOperator(tm.CurrentToken) Then
            m_Operator = tm.CurrentToken : tm.NextToken()
        Else
            Throw New InternalException(result)
        End If

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Operand = ParseOperand(result)
        If m_Operand Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Operator) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Operator, m_Operand, m_ReturnTypeAttributes, m_TypeName, m_Block)

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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Equals) Then
            m_ConstantExpression = ParseExpression(result)
            If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_ConstantExpression = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(EnumIndex, Info.Attributes, m_Identifier, m_ConstantExpression)

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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        Dim m_ReturnTypeAttributes As New Attributes(Parent)
        Dim m_Block As CodeBlock

        m_Modifiers = ParseModifiers(result, ModifierMasks.OperatorModifiers)

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

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Operand1 = ParseOperand(result)
        If m_Operand1 Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Comma) Then
            m_Operand2 = ParseOperand(result)
            If m_Operand2 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_Operand2 = Nothing
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeName = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Operator) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_OperatorIdentifier, m_OperatorSymbol, m_Operand1, m_Operand2, m_ReturnTypeAttributes, m_TypeName, m_Block)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.MustOverrideProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Function)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If vbnc.HandlesOrImplements.IsMe(tm) Then
            m_HandlesOrImplements = ParseHandlesOrImplements(result)
            If m_HandlesOrImplements Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        If m_Modifiers.Is(ModifierMasks.MustOverride) = False Then
            m_Block = ParseCodeBlock(result, False)
            If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

            If tm.AcceptIfNotError(KS.End, KS.Function) = False Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        End If

        result.Init(Info.Attributes, m_Modifiers, m_Signature, m_HandlesOrImplements, m_Block)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.MustOverrideProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Signature = ParseSubSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If vbnc.HandlesOrImplements.IsMe(tm) Then
            m_HandlesOrImplements = ParseHandlesOrImplements(result)
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        If m_Modifiers.Is(ModifierMasks.MustOverride) = False Then
            m_Block = ParseCodeBlock(result, False)
            If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

            If tm.AcceptIfNotError(KS.End, KS.Sub) = False Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        End If

        result.Init(Info.Attributes, m_Modifiers, m_Signature, m_HandlesOrImplements, m_Block)

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
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.Init(m_Clause)
        ElseIf vbnc.MemberImplementsClause.IsMe(tm) Then
            Dim m_Clause As MemberImplementsClause
            m_Clause = ParseImplementsClause(result)
            If m_Clause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            Helper.ErrorRecoveryNotImplemented()
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
                Helper.ErrorRecoveryNotImplemented()
            End If
            Dim sne As New SimpleNameExpression(result)
            sne.Init(id, New TypeArgumentList(sne))
            m_First = sne
        End If
        If m_First Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.Dot) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Second = ParseIdentifierOrKeyword(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.InterfaceProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Signature = ParseSubSignature(Parent)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Signature)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.InterfaceProcedureModifiers)

        tm.AcceptIfNotInternalError(KS.Function)

        m_Signature = ParseFunctionSignature(result)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Signature)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.ExternalMethodModifiers)

        tm.AcceptIfNotInternalError(KS.Declare)

        If tm.CurrentToken.Equals(ModifierMasks.CharSetModifiers) Then
            m_CharsetModifier = tm.CurrentToken.Keyword
            tm.NextToken()
        End If

        tm.AcceptIfNotInternalError(KS.Sub)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        m_LibraryClause = ParseLibraryClause(result)
        If m_LibraryClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If AliasClause.IsMe(tm) Then
            m_AliasClause = ParseAliasClause(result)
            If m_AliasClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.Accept(KS.LParenthesis) Then
            m_ParameterList = New ParameterList(result)
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented()
                End If

                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_CharsetModifier, m_Identifier, m_LibraryClause, m_AliasClause, m_ParameterList)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.ExternalMethodModifiers)
        tm.AcceptIfNotInternalError(KS.Declare)

        If tm.CurrentToken.Equals(ModifierMasks.CharSetModifiers) Then
            m_CharsetModifier = tm.CurrentToken.Keyword
            tm.NextToken()
        End If

        tm.AcceptIfNotInternalError(KS.Function)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_LibraryClause = ParseLibraryClause(result)
        If m_LibraryClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If AliasClause.IsMe(tm) Then
            m_AliasClause = ParseAliasClause(result)
            If m_AliasClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If
        If tm.Accept(KS.LParenthesis) Then
            m_ParameterList = New ParameterList(result)
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented()
                End If

                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
        End If

        If tm.Accept(KS.As) Then
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_CharsetModifier, m_Identifier, m_LibraryClause, m_AliasClause, m_ParameterList, m_ReturnTypeAttributes, m_TypeName)

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
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If
        If tm.AcceptIfNotError(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented()

        m_ConstantExpression = ParseExpression(result)
        If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, Modifiers, m_Identifier, m_TypeName, m_ConstantExpression)

        'Don't parse a StatementTerminator as the VB spec says.
        Return result
    End Function

    ''' <summary>
    ''' LocalDeclarationStatement  ::=  LocalModifier VariableDeclarators StatementTerminator
    ''' </summary>
    Private Function ParseLocalDeclarationStatement(ByVal Parent As CodeBlock) As Generic.List(Of VariableDeclaration)
        Dim result As Generic.List(Of VariableDeclaration)

        Dim m_Modifiers As Modifiers

        m_Modifiers = ParseModifiers(Parent, ModifierMasks.LocalModifiers)

        result = ParseVariableDeclarators(Parent, m_Modifiers, New ParseAttributableInfo(Compiler, Nothing))
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        Return result
    End Function

    ''' <summary>
    ''' VariableMemberDeclaration  ::=	[  Attributes  ]  VariableModifier+  VariableDeclarators  StatementTerminator
    ''' </summary>
    Private Function ParseVariableMemberDeclaration(ByVal Parent As ParsedObject, ByVal Info As ParseAttributableInfo) As Generic.List(Of VariableDeclaration)
        Dim result As Generic.List(Of VariableDeclaration)

        Dim m_VariableModifiers As Modifiers

        m_VariableModifiers = ParseModifiers(Parent, ModifierMasks.VariableModifiers)

        result = ParseVariableDeclarators(Parent, m_VariableModifiers, Info)

        If tm.FindNewLineAndShowError() = False Then Helper.ErrorRecoveryNotImplemented()

        Return result
    End Function

    ''' <summary>
    ''' VariableDeclarators  ::= VariableDeclarator  |	VariableDeclarators  ,  VariableDeclarator
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableDeclarators(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of VariableDeclaration)
        Dim result As New Generic.List(Of VariableDeclaration)

        Do
            Dim tmp As New Generic.List(Of VariableDeclaration)
            tmp = ParseVariableDeclarator(Parent, Modifiers, Info)
            If tmp Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.AddRange(tmp)
        Loop While tm.Accept(KS.Comma)

        Return result
    End Function

    ''' <summary>
    ''' VariableDeclarator  ::=
    '''  	VariableIdentifiers  [  As  [  New  ]  TypeName  [  (  ArgumentList  )  ]  ]  |
    '''     VariableIdentifier   [  As  TypeName  ]  [  =  VariableInitializer  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableDeclarator(ByVal Parent As ParsedObject, ByVal Modifiers As Modifiers, ByVal Info As ParseAttributableInfo) As Generic.List(Of VariableDeclaration)
        Dim m_VariableIdentifiers As VariableIdentifiers
        Dim m_IsNew As Boolean
        Dim m_TypeName As TypeName
        Dim m_VariableInitializer As VariableInitializer
        Dim m_ArgumentList As ArgumentList

        m_VariableIdentifiers = ParseVariableIdentifiers(Parent)
        If m_VariableIdentifiers Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            m_IsNew = tm.Accept(KS.[New])
            If m_IsNew Then
                'Arrays not allowed.
                Dim m_NonArrayTypeName As NonArrayTypeName
                m_NonArrayTypeName = ParseNonArrayTypeName(Parent)
                If m_NonArrayTypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                m_TypeName = New TypeName(Parent, m_NonArrayTypeName)
            Else
                'Arrays allowed.
                m_TypeName = ParseTypeName(Parent)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If
        Else
            m_TypeName = Nothing
        End If

        If tm.Accept(KS.Equals) Then
            m_VariableInitializer = ParseVariableInitializer(Parent)
            If m_VariableInitializer Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            m_ArgumentList = Nothing
        ElseIf tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                m_ArgumentList = ParseArgumentList(Parent)
                If m_ArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
            Else
                m_ArgumentList = New ArgumentList(Parent)
            End If
            m_VariableInitializer = Nothing
        Else
            m_VariableInitializer = Nothing
            m_ArgumentList = Nothing
        End If

        Dim result As New Generic.List(Of VariableDeclaration)
        For Each identifier As VariableIdentifier In m_VariableIdentifiers
            result.Add(New VariableDeclaration(Parent, Info.Attributes, Modifiers, identifier, m_IsNew, m_TypeName, m_VariableInitializer, m_ArgumentList))
        Next


        Return result
    End Function

    Private Function ParseInterfacePropertyMemberDeclaration(ByVal Parent As TypeDeclaration, ByVal Info As ParseAttributableInfo) As InterfacePropertyMemberDeclaration
        Dim result As New InterfacePropertyMemberDeclaration(Parent)

        Dim m_Modifiers As Modifiers = Nothing
        Dim m_Signature As FunctionSignature = Nothing

        m_Modifiers = ParseModifiers(result, ModifierMasks.InterfacePropertyModifier)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Signature, Nothing)

        Return result
    End Function
End Class