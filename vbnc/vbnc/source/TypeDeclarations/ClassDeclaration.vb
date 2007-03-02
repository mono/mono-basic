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

Imports System.Reflection
Imports System.Reflection.Emit

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
''' <remarks></remarks>
Public Class ClassDeclaration
    Inherits PartialTypeDeclaration
    Implements IHasImplicitMembers

    Private m_Inherits As NonArrayTypeName

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.New(Parent, [Namespace])
    End Sub

    Shadows Sub Init(ByVal CustomAttributes As Attributes, ByVal Modifiers As Modifiers, ByVal DeclaringType As TypeDeclaration, ByVal Members As MemberDeclarations, ByVal Name As IdentifierToken, ByVal TypeParameters As TypeParameters, ByVal [Inherits] As NonArrayTypeName, ByVal TypeImplementsClauses As TypeImplementsClauses)
        MyBase.Init(CustomAttributes, Modifiers, Members, Name, TypeParameters, TypeImplementsClauses)
        m_Inherits = [Inherits]
    End Sub

    ReadOnly Property [Inherits]() As NonArrayTypeName
        Get
            Return m_Inherits
        End Get
    End Property


    ''' <summary>
    ''' Returns the default constructor (non-private, non-shared, with no parameters) for the base type (if any). 
    ''' If no constructor found, returns nothing.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBaseDefaultConstructor() As ConstructorInfo
        If Me.BaseType.IsGenericType Then
            Helper.Assert(Me.m_Inherits.IsConstructedTypeName)
            Return Compiler.Helper.GetDefaultGenericConstructor(Me.m_Inherits.AsConstructedTypeName)
        Else
            Return Compiler.Helper.GetDefaultConstructor(Me.BaseType)
        End If
    End Function

    Public Overrides ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes
        Get
            Dim result As TypeAttributes = MyBase.TypeAttributes

            If Me.Modifiers.Is(KS.MustInherit) Then
                result = result Or Reflection.TypeAttributes.Abstract
            ElseIf Me.Modifiers.Is(KS.NotInheritable) Then
                result = result Or Reflection.TypeAttributes.Sealed
            End If

            Return result
        End Get
    End Property

    Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

        If m_Inherits IsNot Nothing Then
            result = m_Inherits.ResolveTypeReferences AndAlso result
            If result = False Then Return result
            BaseType = m_Inherits.ResolvedType
        Else
            BaseType = Compiler.TypeCache.Object
        End If

        result = MyBase.ResolveType AndAlso result

        Helper.Assert(BaseType IsNot Nothing)

        'Find the default constructors for this class
        Me.FindDefaultConstructors()

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(info) AndAlso result
        vbnc.Helper.Assert(result = (Compiler.Report.Errors = 0))

        Return result
    End Function

    Private Function CreateImplicitMembers() As Boolean Implements IHasImplicitMembers.CreateImplicitMembers
        Dim result As Boolean = True
        'If a type contains no instance constructor declarations, a default constructor 
        'is automatically provided. The default constructor simply invokes the 
        'parameterless constructor of the direct base type. If the direct 
        'base type does not have an accessible parameterless constructor, 
        'a compile-time error occurs. 
        'The declared access type for the default constructor is always Public. 
        If HasInstanceConstructors = False Then
            Dim baseDefaultCtor As ConstructorInfo
            baseDefaultCtor = Me.GetBaseDefaultConstructor()

            If baseDefaultCtor IsNot Nothing Then
                If baseDefaultCtor.IsPrivate Then
                    Helper.AddError("No default constructor can be created because base class has no accessible default constructor.")
                    result = False
                Else
                    DefaultInstanceConstructor = ConstructorDeclaration.CreateDefaultConstructor(Me)
                    Members.Add(DefaultInstanceConstructor)
                End If
            Else
                Helper.AddError("No default constructor can be created because base class has no default constructor.")
                result = False
            End If
        End If

        If DefaultSharedConstructor Is Nothing AndAlso Me.HasSharedFieldsWithInitializers Then
            DefaultSharedConstructor = ConstructorDeclaration.CreateTypeConstructor(Me)
            Members.Add(DefaultSharedConstructor)
            BeforeFieldInit = True
        End If

        Return result
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        'Define type parameters
        result = MyBase.DefineTypeHierarchy AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(Enums.ClassModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Class)
    End Function

End Class
