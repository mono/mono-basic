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
            Dim attributes As Attributes
            attributes = New Attributes(result)
            If vbnc.Attributes.IsMe(tm) Then
                ParseAttributes(result, attributes)
            End If
            If CustomEventHandlerDeclaration.IsMe(tm) Then
                Dim newMember As CustomEventHandlerDeclaration
                newMember = ParseCustomEventHandlerDeclaration(Parent, New ParseAttributableInfo(Compiler, attributes), EventName, EventModifiers)
                If newMember Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        m_Modifiers = ParseModifiers(result, ModifierMasks.EventModifiers)

        tm.AcceptIfNotInternalError("Custom")
        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.As) = False Then Helper.ErrorRecoveryNotImplemented()

        m_TypeName = ParseNonArrayTypeName(result)
        If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement = False Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        End If

        m_EventAccessorDeclarations = ParseEventAccessorDeclarations(result, m_Identifier, m_Modifiers)
        If m_EventAccessorDeclarations Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Event) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Identifier, m_TypeName, m_ImplementsClause)

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


        m_Modifiers = ParseModifiers(result, ModifierMasks.InterfaceEventModifiers)

        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_ParametersOrType = ParseParametersOrType(result)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Identifier, m_ParametersOrType, Nothing)

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
                        Helper.ErrorRecoveryNotImplemented()
                    End If
                    If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
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

        m_Modifiers = ParseModifiers(result, ModifierMasks.EventModifiers)

        tm.AcceptIfNotInternalError(KS.Event)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_ParametersOrType = ParseParametersOrType(result)

        If MemberImplementsClause.IsMe(tm) Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_ImplementsClause = Nothing
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Identifier, m_ParametersOrType, m_ImplementsClause)

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

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.RParenthesis) = False Then
            If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        End If
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, m_HandlerType) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        If m_ParameterList Is Nothing Then m_ParameterList = New ParameterList(result)

        If m_HandlerType = KS.RaiseEvent Then
            m_Modifiers = New Modifiers(ModifierMasks.Private)
        Else
            m_modifiers = EventModifiers
        End If


        result.Init(Info.Attributes, m_Modifiers, m_ParameterList, m_Block, m_HandlerType, EventName)

        Return result
    End Function

End Class