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

Partial Public Class Parser

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
            Throw New InternalException(parent)
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
            If m_Exception Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        tm.AcceptIfNotInternalError(KS.Resume)
        m_IsResumeNext = tm.Accept(KS.Next)

        Return New ResumeStatement(Parent, m_IsResumeNext)
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
        If m_Clauses Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If tm.Accept(KS.Error) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.Accept(KS.Resume) Then
            If tm.Accept(KS.Next) = False Then Helper.ErrorRecoveryNotImplemented()
            m_IsResumeNext = True
        Else
            If tm.Accept(KS.GoTo) = False Then Helper.ErrorRecoveryNotImplemented()
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
                    Helper.ErrorRecoveryNotImplemented()
                    Compiler.Report.ShowMessage(Messages.VBNC90011, "-1")
                End If
            ElseIf tm.CurrentToken.IsIdentifier Then
                m_Label = tm.CurrentToken
                tm.NextToken()
            Else
                Helper.ErrorRecoveryNotImplemented()
                Compiler.Report.ShowMessage(Messages.VBNC30203)
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

        tm.AcceptIfNotInternalError(KS.Exit)
        If tm.CurrentToken.Equals(KS.Sub, KS.Function, KS.Property, KS.Do, KS.For, KS.Try, KS.While, KS.Select) Then
            m_ExitWhat = tm.CurrentToken.Keyword
            tm.NextToken()
        Else
            Compiler.Report.ShowMessage(Messages.VBNC30240)
            Return Nothing
        End If

        Return New ExitStatement(Parent, m_ExitWhat)
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
            Compiler.Report.ShowMessage(Messages.VBNC30781)
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
        If m_Targets Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_Targets)

        Return result
    End Function

    Private Function ParseReturnStatement(ByVal Parent As ParsedObject) As ReturnStatement
        Dim result As New ReturnStatement(Parent)

        Dim m_Expression As Expression

        tm.AcceptIfNotInternalError(KS.Return)
        If Not tm.CurrentToken.IsEndOfStatement Then
            m_Expression = ParseExpression(result)
            If m_Expression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_Expression = Nothing
        End If

        result.Init(m_Expression)

        Return result
    End Function

    Private Function ParseRedimClauses(ByVal Parent As ReDimStatement) As RedimClauses
        Dim result As New RedimClauses(Parent)
        If ParseList(Of RedimClause)(result, New ParseDelegate_Parent(Of RedimClause)(AddressOf ParseRedimClause), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented()
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
        Dim m_ArgumentList As ArgumentList

        Dim tmpExpression As Expression = Nothing
        tmpExpression = ParseExpression(result)
        If tmpExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        Dim invExpression As InvocationOrIndexExpression = TryCast(tmpExpression, InvocationOrIndexExpression)
        If invExpression IsNot Nothing Then
            m_Expression = invExpression.Expression
            m_ArgumentList = invExpression.ArgumentList
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
            Return Nothing
        End If

        result.Init(m_Expression, m_ArgumentList)

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
        If m_ErrNumber Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Target = ParseExpression(result)
        If m_Target Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Start = ParseExpression(result)
        If m_Start Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Comma) Then
            m_Length = ParseExpression(result)
            If m_Length Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_Length = Nothing
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.Accept(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Source = ParseExpression(result)
        If m_Source Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.While) = False Then Helper.ErrorRecoveryNotImplemented()

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
        If m_WithExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()
        m_Code = ParseCodeBlock(result, IsOneLiner)

        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.End, KS.With) = False Then Helper.ErrorRecoveryNotImplemented()

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
        Dim m_VariableDeclaration As VariableDeclaration

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
                        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
                    End If
                End If
                If m_ArgumentList Is Nothing Then m_ArgumentList = New ArgumentList(result)
            End If

            m_VariableDeclaration = New VariableDeclaration(result, Nothing, m_Identifier, m_IsNew, m_TypeName, m_VariableInitializer, m_ArgumentList)
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
                Helper.ErrorRecoveryNotImplemented()
            End If
            m_UsingResources = newDecls
        Else
            'This is an expression
            Dim exp As Expression = Nothing
            exp = ParseExpression(result)
            If exp Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            m_UsingResources = exp
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If newDecls IsNot Nothing Then
            For Each decl As UsingDeclarator In newDecls
                If decl.IsVariableDeclaration Then
                    m_Code.Variables.Add(decl.VariableDeclaration)
                End If
            Next
        End If

        If tm.Accept(KS.End, KS.Using) = False Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Lock Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.End, KS.SyncLock) = False Then Helper.ErrorRecoveryNotImplemented()

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
        If result Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.Loop) = False Then Helper.ErrorRecoveryNotImplemented()
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
            Compiler.Report.ShowMessage(Messages.VBNC30238)
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
        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_TryCode = ParseCodeBlock(result, IsOneLiner)
        If m_TryCode Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_Catches = New BaseObjects(Of CatchStatement)(result)
        While tm.CurrentToken = KS.Catch
            Dim newCatch As CatchStatement
            newCatch = ParseCatchStatement(result, IsOneLiner)
            m_Catches.Add(newCatch)
        End While

        If tm.Accept(KS.Finally) Then
            If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()
            m_FinallyBlock = ParseCodeBlock(result, IsOneLiner)
            If m_FinallyBlock Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_FinallyBlock = Nothing
        End If

        If tm.Accept(KS.End, KS.Try) = False Then Helper.ErrorRecoveryNotImplemented()

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
                If tm.AcceptIfNotError(KS.As) = False Then Helper.ErrorRecoveryNotImplemented()
                m_TypeName = ParseNonArrayTypeName(result)
                If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.Accept(KS.When) Then
                m_When = ParseExpression(result)
                If m_When Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If m_TrueCode Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_ElseIfs = New BaseObjects(Of ElseIfStatement)(result)
        While tm.CurrentToken = KS.ElseIf OrElse (m_OneLiner = False AndAlso tm.CurrentToken = KS.Else AndAlso tm.PeekToken = KS.If)
            Dim newElseIf As ElseIfStatement
            newElseIf = ParseElseIfStatement(result, m_OneLiner)
            m_ElseIfs.Add(newElseIf)
        End While

        If tm.Accept(KS.Else) Then
            If m_OneLiner = False Then
                If tm.AcceptEndOfStatement(False, True) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
            m_FalseCode = ParseCodeBlock(result, m_OneLiner)
            If m_FalseCode Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Condition Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        'ElseIf cannot be a oneliner...
        tm.Accept(KS.Then) '"Then" is not required.
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        If IsOneLiner Then
            Helper.AddError(Compiler, tm.CurrentLocation)
            'TODO: Add error, 
        End If

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Test Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(IsOneLiner, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Cases = New BaseObjects(Of CaseStatement)(result)
        While tm.CurrentToken = KS.Case
            Dim newCase As CaseStatement
            newCase = ParseCaseStatement(result, IsOneLiner)
            m_Cases.Add(newCase)
        End While

        If tm.Accept(KS.End, KS.Select) = False Then Helper.ErrorRecoveryNotImplemented()

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
                Helper.ErrorRecoveryNotImplemented()
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
        If m_LoopControlVariable Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.In) = False Then Helper.ErrorRecoveryNotImplemented()

        m_InExpression = ParseExpression(result)
        If m_InExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.Next) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.CurrentToken.IsEndOfStatement = False Then
            m_NextExpression = ParseExpression(result)
            If m_NextExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_LoopControlVariable Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.Equals) = False Then Helper.ErrorRecoveryNotImplemented()

        m_LoopStartExpression = ParseExpression(result)
        If m_LoopStartExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.To) = False Then Helper.ErrorRecoveryNotImplemented()

        m_LoopEndExpression = ParseExpression(result)
        If m_LoopEndExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Step) Then
            m_LoopStepExpression = ParseExpression(result)
            If m_LoopStepExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_LoopStepExpression = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Code = ParseCodeBlock(result, IsOneLiner)
        If m_Code Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Next) = False Then
            Compiler.Report.ShowMessage(Messages.VBNC30084, tm.CurrentLocation)
            Return result
        End If

        If tm.CurrentToken.IsEndOfStatement = False Then
            m_NextExpressionList = New ExpressionList(result)
            If ParseList(Of Expression)(m_NextExpressionList, New ParseDelegate_Parent(Of Expression)(AddressOf ParseExpression), result) = False Then
                Helper.ErrorRecoveryNotImplemented()
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
            If tm.CurrentToken.Equals(CaseClause.RelationalOperators) = False Then
                Compiler.Report.ShowMessage(Messages.VBNC30239)
                m_Comparison = KS.Equals
            Else
                m_Comparison = tm.CurrentToken.Symbol
                tm.NextToken()
            End If
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        ElseIf tm.CurrentToken.Equals(CaseClause.RelationalOperators) Then
            m_Comparison = tm.CurrentToken.Symbol
            tm.NextToken()
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_Expression1 = ParseExpression(result)
            If m_Expression1 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.Accept(KS.To) Then
                m_Expression2 = ParseExpression(result)
                If m_Expression2 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Event Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.Comma) = False Then Helper.ErrorRecoveryNotImplemented()

        m_EventHandler = ParseExpression(result)
        If m_EventHandler Is Nothing Then Helper.ErrorRecoveryNotImplemented()

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
        If m_Clauses Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

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
End Class