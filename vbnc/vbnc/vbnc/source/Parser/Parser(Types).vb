' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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
            result.UpdateDefinition()
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
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

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

        If tm.AcceptIfNotError(KS.End, KS.Class) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If result.CustomAttributes IsNot Nothing Then
            result.CustomAttributes.AddRange(Attributes)
        Else
            result.CustomAttributes = Attributes
        End If
        result.UpdateDefinition()

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
        result.UpdateDefinition()

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
            constAttributes = New Attributes(Parent)
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
        Dim m_QualifiedName As KS = KS.Integer

        m_Modifiers = ParseModifiers(ModifierMasks.TypeModifiers)

        tm.AcceptIfNotInternalError(KS.Enum)

        m_Identifier = ParseIdentifier()
        If m_Identifier Is Nothing Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.Accept(KS.As) Then
            If tm.CurrentToken.Equals(Enums.IntegralTypeNames) Then
                m_QualifiedName = tm.CurrentToken.Keyword
                tm.NextToken()
            Else
                Helper.AddError(Compiler, tm.CurrentLocation, "Enum type must be integral")
            End If
        End If
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        result = New EnumDeclaration(Parent, [Namespace], m_Identifier, m_QualifiedName)

        If ParseEnumMembers(result) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If tm.AcceptIfNotError(KS.End, KS.Enum) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

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
        result.UpdateDefinition()

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

        If tm.AcceptIfNotError(KS.End, KS.Module) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)
        If tm.AcceptEndOfStatement(, True) = False Then Helper.ErrorRecoveryNotImplemented(tm.CurrentLocation)

        If result.CustomAttributes IsNot Nothing Then
            result.CustomAttributes.AddRange(Attributes)
        Else
            result.CustomAttributes = Attributes
        End If
        result.Modifiers = m_Modifiers
        result.UpdateDefinition()

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

End Class