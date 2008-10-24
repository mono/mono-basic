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

        m_Second = ParseIdentifierOrKeywordWithTypeArguments(result)
        If m_Second Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            Helper.ErrorRecoveryNotImplemented()
            Return Nothing
        End If

        If tm.CurrentToken.Equals(KS.LParenthesis) AndAlso tm.PeekToken.Equals(KS.Of) Then
            m_TypeArguments = ParseTypeArgumentList(result)
            If m_TypeArguments Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If m_SecondPart Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30203)
            Return Nothing
        End If

        result.Init(m_FirstPart, m_SecondPart)

        Return result
    End Function

    Private Function ParseCByteExpression(ByVal Parent As ParsedObject) As CByteExpression
        Dim result As New CByteExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CByte)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCBoolExpression(ByVal Parent As ParsedObject) As CBoolExpression
        Dim result As New CBoolExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CBool)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCCharExpression(ByVal Parent As ParsedObject) As CCharExpression
        Dim result As New CCharExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CChar)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDateExpression(ByVal Parent As ParsedObject) As CDateExpression
        Dim result As New CDateExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDate)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDblExpression(ByVal Parent As ParsedObject) As CDblExpression
        Dim result As New CDblExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDbl)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCDecExpression(ByVal Parent As ParsedObject) As CDecExpression
        Dim result As New CDecExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CDec)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCIntExpression(ByVal Parent As ParsedObject) As CIntExpression
        Dim result As New CIntExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CInt)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCLngExpression(ByVal Parent As ParsedObject) As CLngExpression
        Dim result As New CLngExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CLng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCObjExpression(ByVal Parent As ParsedObject) As CObjExpression
        Dim result As New CObjExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CObj)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCSByteExpression(ByVal Parent As ParsedObject) As CSByteExpression
        Dim result As New CSByteExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CSByte)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCShortExpression(ByVal Parent As ParsedObject) As CShortExpression
        Dim result As New CShortExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CShort)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCSngExpression(ByVal Parent As ParsedObject) As CSngExpression
        Dim result As New CSngExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CSng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCStrExpression(ByVal Parent As ParsedObject) As CStrExpression
        Dim result As New CStrExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CStr)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCUIntExpression(ByVal Parent As ParsedObject) As CUIntExpression
        Dim result As New CUIntExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CUInt)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCULngExpression(ByVal Parent As ParsedObject) As CULngExpression
        Dim result As New CULngExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CULng)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCUShortExpression(ByVal Parent As ParsedObject) As CUShortExpression
        Dim result As New CUShortExpression(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.CUShort)

        m_Expression = ParseParenthesizedExpression(Parent)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
            Compiler.Report.ShowMessage(Messages.VBNC30203)
            Return Nothing
        End If

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If vbnc.ArrayNameModifier.CanBeMe(tm) Then
            m_ArrayNameModifier = ParseArrayNameModifier(result)
            If m_ArrayNameModifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_Event = New SimpleNameExpression(result)
        m_Event.Identifier = New Identifier(m_Event, m_Identifier.Identifier, m_Identifier.Location, TypeCharacters.Characters.None)

        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                m_Arguments = ParseArgumentList(result)
                If m_Arguments Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
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
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
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
        Dim result As New ParenthesizedExpression(parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.LParenthesis)

        m_Expression = ParseExpression(result)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
        End If

        result.init(m_Expression)

        Return result
    End Function


    Private Function ParseUnaryMinusExpression(ByVal Info As ExpressionParseInfo) As UnaryMinusExpression
        Dim result As New UnaryMinusExpression(Info.Parent)

        Dim m_Expression As Expression
        tm.AcceptIfNotInternalError(KS.Minus)

        m_Expression = ParseExponent(Info)
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseCTypeExpression(ByVal Parent As ParsedObject, ByVal GetKeyword As KS) As CTypeExpression
        Dim result As CTypeExpression = Nothing

        Dim m_DestinationType As TypeName
        Dim m_Expression As Expression

        Select Case GetKeyword
            Case KS.CType
                result = New CTypeExpression(Parent)
            Case KS.DirectCast
                result = New DirectCastExpression(Parent)
            Case KS.TryCast
                result = New TryCastExpression(Parent)
            Case Else
                Throw New InternalException(result)
        End Select

        tm.AcceptIfNotInternalError(GetKeyword)
        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        m_Expression = ParseExpression(result)
        If tm.AcceptIfNotError(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented()
        m_DestinationType = ParseTypeName(result)
        If m_DestinationType Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()


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
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
                If newExp Is Nothing Then Helper.ErrorRecoveryNotImplemented()

                m_Initializers.Add(newExp)
            Loop While tm.Accept(KS.Comma)
        End If

        If tm.Accept(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        If m_Identifier IsNot Nothing Then
            If ArrayNameModifier.CanBeMe(tm) Then
                tmpANM = ParseArrayNameModifier(result)
                If tmpANM Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If
            If tmpANM Is Nothing AndAlso tm.Accept(KS.As) Then
                m_ArrayNameModifier = tmpANM
                m_TypeName = ParseTypeName(result)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                doExpression = False
            End If
        End If

        If doExpression Then
            tm.RestoreToPoint(iCurrent)
            m_Expression = ParseExpression(New ExpressionParseInfo(result, True))
            If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            If doce Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                If m_ArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If

            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
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

        Dim m_Arguments As New Generic.List(Of Argument)

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

        Expression = ParseExpression(result)
        If Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()


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
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.CurrentToken = KS.LParenthesis AndAlso tm.PeekToken = KS.Of Then
            m_TypeArgumentList = ParseTypeArgumentList(result)
            If m_TypeArgumentList Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                If newLabel Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                result.AddStatement(newLabel)
                result.AddLabel(newLabel)
            ElseIf MidAssignStatement.IsMe(tm) Then
                Dim newMidAssign As MidAssignStatement
                newMidAssign = ParseMidAssignmentStatement(result, IsOneLiner)
                If newMidAssign Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                If lside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                If tm.CurrentToken.IsSymbol Then
                    Select Case tm.CurrentToken.Symbol
                        Case KS.Equals
                            tm.NextToken()
                            Dim newStmt As New AssignmentStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.AddAssign
                            tm.NextToken()
                            Dim newStmt As New AddAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ConcatAssign
                            tm.NextToken()
                            Dim newStmt As New ConcatAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.RealDivAssign
                            tm.NextToken()
                            Dim newStmt As New DivisionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.IntDivAssign
                            tm.NextToken()
                            Dim newStmt As New IntDivisionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.MultAssign
                            tm.NextToken()
                            Dim newStmt As New MultiplicationAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.PowerAssign
                            tm.NextToken()
                            Dim newStmt As New PowerAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ShiftRightAssign
                            tm.NextToken()
                            Dim newStmt As New RShiftAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.ShiftLeftAssign
                            tm.NextToken()
                            Dim newStmt As New LShiftAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            newStmt.Init(lside, rside)
                            result.AddStatement(newStmt)
                        Case KS.MinusAssign
                            tm.NextToken()
                            Dim newStmt As New SubtractionAssignStatement(result)
                            rside = ParseExpression(New ExpressionParseInfo(newStmt, False, False))
                            If rside Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                        Dim newVariables As Generic.List(Of VariableDeclaration)
                        newVariables = ParseLocalDeclarationStatement(result)
                        If newVariables Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddVariables(newVariables)
                    Case KS.SyncLock
                        Dim newLock As SyncLockStatement
                        newLock = ParseSyncLockStatement(result, IsOneLiner)
                        If newLock Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newLock)
                    Case KS.Try
                        Dim newTry As TryStatement
                        newTry = ParseTryStatement(result, IsOneLiner)
                        If newTry Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newTry)
                    Case KS.Throw
                        Dim newThrow As ThrowStatement
                        newThrow = ParseThrowStatement(result)
                        If newThrow Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newThrow)
                    Case KS.With
                        Dim newWith As WithStatement
                        newWith = ParseWithStatement(result, IsOneLiner)
                        If newWith Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newWith)
                    Case KS.Select
                        Dim newSelect As SelectStatement
                        newSelect = ParseSelectStatement(result, IsOneLiner)
                        If newSelect Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newSelect)
                    Case KS.If
                        Dim newIf As IfStatement
                        newIf = ParseIfStatement(result, IsOneLiner)
                        If newIf Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newIf)
                    Case KS.Do
                        Dim newDo As DoStatement
                        newDo = ParseDoStatement(result, IsOneLiner)
                        If newDo Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newDo)
                    Case KS.Stop
                        Dim newStop As StopStatement
                        newStop = ParseStopStatement(result)
                        If newStop Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newStop)
                    Case KS.End
                        Dim newEnd As EndStatement
                        If tm.PeekToken.IsEndOfStatement() Then
                            newEnd = ParseEndStatement(result)
                            If newEnd Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            result.AddStatement(newEnd)
                        Else
                            breakloop = True
                        End If
                    Case KS.While
                        Dim newWhile As WhileStatement
                        newWhile = ParseWhileStatement(result, IsOneLiner)
                        If newWhile Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newWhile)
                    Case KS.Exit
                        Dim newExit As ExitStatement
                        newExit = ParseExitStatement(result)
                        If newExit Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newExit)
                    Case KS.Return
                        Dim newReturn As ReturnStatement
                        newReturn = ParseReturnStatement(result)
                        If newReturn Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newReturn)
                    Case KS.For
                        If tm.PeekToken.Equals(KS.Each) Then
                            Dim newFor As ForEachStatement
                            newFor = ParseForEachStatement(result, IsOneLiner)
                            If newFor Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            result.AddStatement(newFor)
                        Else
                            Dim newFor As ForStatement
                            newFor = ParseForStatement(result, IsOneLiner)
                            If newFor Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                            result.AddStatement(newFor)
                        End If
                    Case KS.Continue
                        Dim newContinue As ContinueStatement
                        newContinue = ParseContinueStatement(result, IsOneLiner)
                        If newContinue Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newContinue)
                    Case KS.GoTo
                        Dim newGoto As GotoStatement
                        newGoto = ParseGotoStatement(result)
                        If newGoto Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newGoto)
                    Case KS.On
                        Dim newOnError As OnErrorStatement
                        newOnError = ParseOnErrorStatement(result)
                        If newOnError Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newOnError)
                    Case KS.Error
                        Dim newError As ErrorStatement
                        newError = ParseErrorStatement(result)
                        If newError Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newError)
                    Case KS.AddHandler, KS.RemoveHandler
                        Dim newAddHandler As AddOrRemoveHandlerStatement
                        newAddHandler = ParseAddOrRemoveHandlerStatement(result)
                        If newAddHandler Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newAddHandler)
                    Case KS.RaiseEvent
                        Dim newRaiseEvent As RaiseEventStatement
                        newRaiseEvent = ParseRaiseEventStatement(result)
                        If newRaiseEvent Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newRaiseEvent)
                    Case KS.Call
                        Dim newCall As CallStatement
                        newCall = ParseCallStatement(result)
                        If newCall Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newCall)
                    Case KS.Erase
                        Dim newErase As EraseStatement
                        newErase = ParseEraseStatement(result)
                        If newErase Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newErase)
                    Case KS.ReDim
                        Dim newReDim As ReDimStatement
                        newReDim = ParseReDimStatement(result)
                        If newReDim Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newReDim)
                    Case KS.Resume
                        Dim newResume As ResumeStatement
                        newResume = ParseResumeStatement(result)
                        If newResume Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                        result.AddStatement(newResume)
                    Case KS.Using
                        Dim newUsing As UsingStatement
                        newUsing = ParseUsingStatement(result, IsOneLiner)
                        If newUsing Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
            Helper.ErrorRecoveryNotImplemented()
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
        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        Dim m_TypeName As GetTypeTypeName
        m_TypeName = ParseGetTypeTypeName(result)

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        result.init(m_TypeName)

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
            If value Is Nothing Then helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken = KS.Dot Then
            value = ParseMemberAccessExpression(Info.Parent, Nothing)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken = KS.Exclamation Then
            value = ParseDictionaryAccessExpression(Info.Parent, Nothing)
            If value Is Nothing Then helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken.Equals(Enums.BuiltInTypeTypeNames) Then
            value = ParseBuiltInTypeExpression(Info.Parent)
            If value Is Nothing Then helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken.IsIdentifier Then
            value = ParseSimpleNameExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken = KS.LBrace Then
            value = ParseArrayInitializerExpression(Info.Parent)
            If value Is Nothing Then helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken = KS.LParenthesis Then
            value = ParseParenthesizedExpression(Info.Parent)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken.Equals(KS.Add, KS.Minus) Then
            value = ParseUnaryPlusMinus(Info)
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                Case Else
                    Helper.Stop()
            End Select
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
                Dim newExp As InvocationOrIndexExpression
                newExp = ParseInvocationOrIndexExpression(Info.Parent, value)
                value = newExp
            Else
                Exit Do
            End If
            If value Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Loop

        Return value
    End Function

    Private Function ParseExponent(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseIdentifier(Info)

        While tm.Accept(KS.Power)
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
            rSide = ParseMultDiv(Info)
            lSide = New IntDivisionExpression(Info.Parent, lSide, rSide)
        End While

        Return lSide
    End Function

    Private Function ParseMod(ByVal Info As ExpressionParseInfo) As Expression
        Dim lSide, rSide As Expression

        lSide = ParseIntDiv(Info)

        While tm.Accept(KS.Mod)
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
        If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Is) Then
            m_Is = True
        ElseIf tm.Accept(KS.IsNot) Then
            m_Is = False
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            Return Nothing
        End If

        m_Type = ParseTypeName(result)
        If m_Type Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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

End Class
