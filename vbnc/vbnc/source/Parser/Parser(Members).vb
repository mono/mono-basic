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
    ''' VariableIdentifiers  ::=
    '''	            VariableIdentifier  |
    '''	            VariableIdentifiers  ,  VariableIdentifier
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseVariableIdentifiers(ByVal Parent As ParsedObject) As VariableIdentifiers
        Dim result As New VariableIdentifiers(Parent)

        If ParseList(Of VariableIdentifier)(result, New ParseDelegate_Parent(Of VariableIdentifier)(AddressOf ParseVariableIdentifier), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented()
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

        m_Modifiers = ParseModifiers(result, ModifierMasks.ConstructorModifiers)

        tm.AcceptIfNotInternalError(KS.Sub)
        tm.AcceptIfNotInternalError(KS.[New])

        If tm.Accept(KS.LParenthesis) Then
            If tm.Accept(KS.RParenthesis) = False Then
                If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                    Helper.ErrorRecoveryNotImplemented()
                End If
                If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
            End If
        End If

        m_Signature = New SubSignature(result, "", m_ParameterList)

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Sub) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, m_Signature, m_Block)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.PropertyModifiers)

        tm.AcceptIfNotInternalError(KS.Property)

        m_Signature = ParseFunctionSignature(result)
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement() = False Then
            m_ImplementsClause = ParseImplementsClause(result)
            If m_ImplementsClause Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_ImplementsClause = Nothing
        End If

        Do
            m_Attributes = ParseAttributes(result)
            If PropertyGetDeclaration.IsMe(tm) Then
                If m_Get IsNot Nothing Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Found more than one Get Property.")
                End If
                m_Get = ParsePropertyGetMember(result, New ParseAttributableInfo(Compiler, m_Attributes), m_Signature, m_ImplementsClause, m_Modifiers.Mask)
                If m_Get Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                m_Attributes = Nothing
            ElseIf PropertySetDeclaration.IsMe(tm) Then
                If m_Set IsNot Nothing Then
                    Helper.AddError(Compiler, tm.CurrentLocation, "Found more than one Set Property.")
                End If
                m_Set = ParsePropertySetMember(result, New ParseAttributableInfo(Compiler, m_Attributes), m_Signature, m_ImplementsClause, m_Modifiers.Mask)
                If m_Set Is Nothing Then Helper.ErrorRecoveryNotImplemented()
                m_Attributes = Nothing
            Else
                If m_Attributes IsNot Nothing AndAlso m_Attributes.Count > 0 Then
                    'Hanging attributes.
                    Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)
                End If
                Exit Do
            End If
        Loop

        If tm.AcceptIfNotError(KS.End, KS.Property) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        If m_Modifiers.Is(ModifierMasks.ReadOnly) AndAlso m_Get Is Nothing Then
            Compiler.Report.ShowMessage(Messages.VBNC30126, m_Signature.Location)
        End If
        If m_Modifiers.Is(ModifierMasks.WriteOnly) AndAlso m_Set Is Nothing Then
            Compiler.Report.ShowMessage(Messages.VBNC30125, m_Signature.Location)
        End If

        If m_Modifiers.Is(ModifierMasks.ReadOnly) = False AndAlso m_Modifiers.Is(ModifierMasks.WriteOnly) = False Then
            If m_Get Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30124, m_Signature.Location)
            End If
            If m_Set Is Nothing Then
                Compiler.Report.ShowMessage(Messages.VBNC30124, m_Signature.Location)
            End If
        End If


        result.Init(Info.Attributes, m_Modifiers, m_Signature, m_Get, m_Set, m_ImplementsClause)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.AccessModifiers)
        If m_Modifiers.Empty = False Then
            m_Modifiers.AddModifiers(ParentModifiers And (Not ModifierMasks.AccessModifiers))
        Else
            m_Modifiers.AddModifiers(ParentModifiers)
        End If
        tm.AcceptIfNotInternalError(KS.Set)

        If tm.Accept(KS.LParenthesis) Then
            If ParseList(Of Parameter)(m_ParameterList, New ParseDelegate_Parent(Of Parameter)(AddressOf ParseParameter), m_ParameterList) = False Then
                Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Set) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()


        result.Init(Info.Attributes, m_Modifiers, ParentSignature, ParentImplements, m_Block, m_ParameterList)

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

        m_Modifiers = ParseModifiers(result, ModifierMasks.AccessModifiers)
        If m_Modifiers.Empty = False Then
            m_Modifiers.AddModifiers(ParentModifiers And (Not ModifierMasks.AccessModifiers))
        Else
            m_Modifiers.AddModifiers(ParentModifiers)
        End If

        tm.AcceptIfNotInternalError(KS.Get)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Block = ParseCodeBlock(result, False)
        If m_Block Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Get) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Info.Attributes, m_Modifiers, ParentSignature, ParentImplements, m_Block)

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
                    Helper.ErrorRecoveryNotImplemented()
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
            Helper.ErrorRecoveryNotImplemented()
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
        Dim m_ReturnTypeAttributes As New Attributes(result)
        Dim m_TypeName As TypeName = Nothing

        If ParseSubSignature(result, m_Identifier, m_TypeParameters, m_ParameterList) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.Accept(KS.As) Then
            If Attributes.IsMe(tm) Then
                If ParseAttributes(result, m_ReturnTypeAttributes) = False Then
                    Helper.ErrorRecoveryNotImplemented()
                End If
            End If
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        If tm.AcceptIfNotError(KS.LParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptIfNotError(KS.Of) = False Then Helper.ErrorRecoveryNotImplemented()

        Dim m_TypeParameters As New TypeParameterList(result)

        If ParseList(Of TypeParameter)(m_TypeParameters, New ParseDelegate_Parent(Of TypeParameter)(AddressOf ParseTypeParameter), m_TypeParameters) = False Then
            Helper.ErrorRecoveryNotImplemented()
        End If

        If tm.AcceptIfNotError(KS.RParenthesis) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(m_TypeParameters)

        Return result
    End Function

    ''' <summary>
    ''' TypeParameter  ::= 	Identifier  [  TypeParameterConstraints  ]
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ParseTypeParameter(ByVal Parent As ParsedObject) As TypeParameter
        Dim result As New TypeParameter(Parent)

        Helper.Assert(TypeOf Parent Is TypeParameterList)

        Dim m_Identifier As Identifier
        Dim m_TypeParameterConstraints As TypeParameterConstraints
        Dim GenericParameterPosition As Integer

        Dim parentList As TypeParameterList

        parentList = DirectCast(Parent, TypeParameterList)
        GenericParameterPosition = parentList.Count + 1

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If TypeParameterConstraints.CanBeMe(tm) Then
            m_TypeParameterConstraints = ParseTypeParameterConstraints(result)
            If m_TypeParameterConstraints Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeParameterConstraints = Nothing
        End If

        result.Init(m_Identifier, m_TypeParameterConstraints, GenericParameterPosition)

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
                Helper.ErrorRecoveryNotImplemented()
            End If
            If tm.AcceptIfNotError(KS.RBrace) = False Then Helper.ErrorRecoveryNotImplemented()
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
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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

        m_Modifiers = ParseModifiers(result, ModifierMasks.ParameterModifiers)

        m_ParameterIdentifier = ParseParameterIdentifier(result)
        If m_ParameterIdentifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            m_TypeName = ParseTypeName(result)
            If m_TypeName Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeName = Nothing
        End If

        If tm.Accept(KS.Equals) Then
            m_ConstantExpression = ParseExpression(result)
            If m_ConstantExpression Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If vbnc.ArrayNameModifier.CanBeMe(tm) Then
            m_ArrayNameModifier = ParseArrayNameModifier(result)
            If m_ArrayNameModifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        End If

        result.Init(m_Identifier, m_ArrayNameModifier)

        Return result
    End Function

    Private Function ParseImplementsClause(ByVal Parent As ParsedObject) As MemberImplementsClause
        Dim result As New MemberImplementsClause(Parent)

        Dim m_ImplementsList As New MemberImplementsList(Parent)

        tm.AcceptIfNotInternalError(KS.Implements)

        If ParseList(Of InterfaceMemberSpecifier)(m_ImplementsList, New ParseDelegate_Parent(Of InterfaceMemberSpecifier)(AddressOf ParseInterfaceMemberSpecifier), Parent) = False Then
            Helper.ErrorRecoveryNotImplemented()
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
            If m_2 Is Nothing Then Helper.ErrorRecoveryNotImplemented()
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
        Else
            Helper.AddError(Compiler, tm.CurrentLocation)
        End If

        result.Init(m_1, m_2)

        Return result
    End Function
End Class