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
        Dim result As New ClassDeclaration(Parent, [Namespace])

        Dim m_Attributes As Attributes
        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_TypeParameters As TypeParameters
        Dim m_Inherits As NonArrayTypeName
        Dim m_TypeImplementsClauses As TypeImplementsClauses
        Dim m_Members As MemberDeclarations
        Dim m_DeclaringType As TypeDeclaration

        m_DeclaringType = TryCast(Parent, TypeDeclaration)
        Helper.Assert(m_DeclaringType IsNot Nothing OrElse TypeOf Parent Is AssemblyDeclaration)

        m_Attributes = Attributes
        m_Modifiers = ParseModifiers(result, ModifierMasks.ClassModifiers)

        tm.AcceptIfNotInternalError(KS.Class) 'ClassDeclaration should not be created if no "Class" is found...

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(result)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeParameters = Nothing
        End If

        If tm.Accept(KS.Inherits) Then
            m_Inherits = ParseNonArrayTypeName(result)
            If m_Inherits Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_Inherits = Nothing
        End If

        If TypeImplementsClauses.IsMe(tm) Then
            m_TypeImplementsClauses = ParseTypeImplementsClauses(result)
            If m_TypeImplementsClauses Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeImplementsClauses = Nothing
        End If

        m_Members = ParseTypeMembers(result)
        If m_Members Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Class) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(attributes, m_Modifiers, m_DeclaringType, m_Members, m_Identifier, m_TypeParameters, m_Inherits, m_TypeImplementsClauses)

        Return result
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
        Dim result As New DelegateDeclaration(Parent, [Namespace])

        Dim Modifiers As Modifiers
        Dim m_Signature As SubSignature

        Modifiers = ParseModifiers(result, ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Delegate)

        If tm.Accept(KS.Function) Then
            m_Signature = ParseFunctionSignature(result)
        ElseIf tm.Accept(KS.Sub) Then
            m_Signature = ParseSubSignature(result)
        Else
            Throw New InternalException(result)
        End If
        If m_Signature Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Attributes, Modifiers, m_Signature)

        Return result
    End Function

    ''' <summary>
    ''' Parses enum members.
    ''' Never returns nothing.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseEnumMembers(ByVal Parent As EnumDeclaration) As MemberDeclarations
        Dim result As New MemberDeclarations(Parent)
        Dim newConst As EnumMemberDeclaration
        Dim constAttributes As Attributes

        Do Until tm.CurrentToken.Equals(KS.End, KS.Enum)
            constAttributes = New Attributes(Parent)
            If vbnc.Attributes.IsMe(tm) Then
                If ParseAttributes(Parent, constAttributes) = False Then Helper.ErrorRecoveryNotImplemented()
            End If

            newConst = ParseEnumMemberDeclaration(Parent, New ParseAttributableInfo(Compiler, constAttributes), result.Declarations.Count)
            If newConst Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            result.Declarations.Add(newConst)
        Loop

        Return result
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
        Dim result As New EnumDeclaration(Parent, [Namespace])
        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_QualifiedName As KS = KS.Integer
        Dim m_Members As MemberDeclarations

        m_Modifiers = ParseModifiers(result, ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Enum)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.Accept(KS.As) Then
            If tm.CurrentToken.Equals(Enums.IntegralTypeNames) Then
                m_QualifiedName = tm.CurrentToken.Keyword
                tm.NextToken()
            Else
                Helper.AddError(Compiler, tm.CurrentLocation, "Enum type must be integral")
            End If
        End If
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Members = ParseEnumMembers(result)
        If m_Members Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Enum) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(attributes, m_Modifiers, m_Members, m_Identifier, m_QualifiedName)

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
        Dim result As New InterfaceDeclaration(Parent, [Namespace])

        Dim m_Modifiers As Modifiers
        Dim m_Identifier As Identifier
        Dim m_TypeParameters As TypeParameters
        Dim m_InterfaceBases As InterfaceBases
        Dim m_Members As MemberDeclarations

        m_Modifiers = ParseModifiers(result, ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Interface)

        m_Identifier = ParseIdentifier(result)
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(result)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeParameters = Nothing
        End If

        If InterfaceBases.IsMe(tm) Then
            m_InterfaceBases = ParseInterfaceBases(result)
            If m_InterfaceBases Is Nothing Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_InterfaceBases = Nothing
        End If

        m_Members = ParseInterfaceMembers(result)
        If m_Members Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Interface) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(attributes, m_Modifiers, m_Members, m_Identifier, m_TypeParameters, m_InterfaceBases)

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
        Dim result As New ModuleDeclaration(Parent, [Namespace])

        Dim m_Modifiers As Modifiers
        Dim m_Members As MemberDeclarations
        Dim m_Name As Identifier

        m_Modifiers = ParseModifiers(result, ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Module)

        m_Name = ParseIdentifier(result)
        If m_Name Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        m_Members = ParseTypeMembers(result)
        If m_Members Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Module) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Attributes, m_Modifiers, m_Members, m_Name, 0)

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
        Dim result As New StructureDeclaration(Parent, [Namespace])

        Dim m_Modifiers As Modifiers
        Dim m_Members As MemberDeclarations
        Dim m_Name As Identifier
        Dim m_TypeParameters As TypeParameters
        Dim m_Implements As TypeImplementsClauses

        m_Modifiers = ParseModifiers(result, ModifierMasks.StructureModifiers)

        tm.AcceptIfNotInternalError(KS.Structure)

        m_Name = ParseIdentifier(result)
        If m_Name Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptEndOfStatement = False Then
            m_TypeParameters = ParseTypeParameters(result)
            If m_TypeParameters Is Nothing Then Helper.ErrorRecoveryNotImplemented()
            If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()
        Else
            m_TypeParameters = Nothing
        End If

        m_Implements = ParseTypeImplementsClauses(result)
        If m_Implements Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        m_Members = ParseTypeMembers(result)
        If m_Members Is Nothing Then Helper.ErrorRecoveryNotImplemented()

        If tm.AcceptIfNotError(KS.End, KS.Structure) = False Then Helper.ErrorRecoveryNotImplemented()
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented()

        result.Init(Attributes, m_Modifiers, m_Members, m_Name, m_TypeParameters, m_Implements)

        Return result
    End Function

End Class