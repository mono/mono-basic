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

Public MustInherit Class PartialTypeDeclaration
    Inherits GenericTypeDeclaration

    ''' <summary>
    ''' A list of all the partial declarations.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_PartialDeclarations As Generic.List(Of PartialTypeDeclaration)

    ''' <summary>
    ''' The main declaration that is in the parse tree.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_MainDeclaration As PartialTypeDeclaration

    Private m_TypeImplementsClauses As TypeImplementsClauses

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String)
        MyBase.new(Parent, [Namespace])
    End Sub

    Shadows Sub Init(ByVal CustomAttributes As Attributes, ByVal Modifiers As Modifiers, ByVal Members As MemberDeclarations, ByVal Name As Identifier, ByVal TypeParameters As TypeParameters, ByVal [Implements] As TypeImplementsClauses)
        MyBase.Init(CustomAttributes, Modifiers, Members, Name, TypeParameters)
        m_TypeImplementsClauses = [Implements]
        If m_TypeImplementsClauses Is Nothing Then m_TypeImplementsClauses = New TypeImplementsClauses(Me)
    End Sub

    ReadOnly Property MainPartialDeclaration() As PartialTypeDeclaration
        Get
            Return m_MainDeclaration
        End Get
    End Property

    ReadOnly Property [Implements]() As TypeImplementsClauses
        Get
            Return m_TypeImplementsClauses
        End Get
    End Property

    ReadOnly Property IsPartial() As Boolean
        Get
            Return m_MainDeclaration IsNot Nothing
        End Get
    End Property

    ReadOnly Property IsMainPartialDeclaration() As Boolean
        Get
            Return m_MainDeclaration Is Me
        End Get
    End Property

    Public Overrides Property TypeBuilder() As System.Reflection.Emit.TypeBuilder
        Get
            Dim result As TypeBuilder
            result = MyBase.TypeBuilder
            If result Is Nothing AndAlso m_MainDeclaration IsNot Nothing AndAlso m_MainDeclaration IsNot Me Then
                result = m_MainDeclaration.TypeBuilder
            End If

            'Helper.Assert(result IsNot Nothing)

            Return result
        End Get
        Protected Set(ByVal value As System.Reflection.Emit.TypeBuilder)
            MyBase.TypeBuilder = value
        End Set
    End Property

    ReadOnly Property PartialDeclarations() As Generic.List(Of PartialTypeDeclaration)
        Get
            Return m_PartialDeclarations
        End Get
    End Property

    Public Overrides ReadOnly Property TypeAttributes() As System.Reflection.TypeAttributes
        Get
            Dim mods As Modifiers
            Dim result As TypeAttributes

            If m_PartialDeclarations IsNot Nothing AndAlso m_PartialDeclarations.Count > 1 Then
                mods = New Modifiers()
                For Each tp As PartialTypeDeclaration In m_PartialDeclarations
                    mods.AddModifiers(tp.Modifiers.Mask)
                Next
            Else
                mods = Modifiers
            End If

            result = Helper.getTypeAttributeScopeFromScope(mods, IsNestedType)

            Return result
        End Get
    End Property

    Public Sub AddPartialDeclaration(ByVal Declaration As PartialTypeDeclaration)
        Helper.Assert(Helper.CompareName(Me.Name, Declaration.Name) AndAlso Helper.CompareName(Me.Namespace, Declaration.Namespace))

        If Declaration.GetType IsNot Me.GetType Then
            Helper.AddError(Me, "Cannot mix partial class declarations with partial structure declarations")
            Return
        End If

        If m_MainDeclaration Is Nothing Then
            m_MainDeclaration = Me
            m_PartialDeclarations = New Generic.List(Of PartialTypeDeclaration)
            m_PartialDeclarations.Add(Me)
        End If
        m_PartialDeclarations.Add(Declaration)
        Declaration.m_MainDeclaration = Me
        Declaration.m_PartialDeclarations = m_PartialDeclarations

        Members.Declarations.AddRange(Declaration.Members.Declarations)
        CustomAttributes.AddRange(Declaration.CustomAttributes)
        Declaration.Members = Members

        For Each member As IMember In Members
            member.DeclaringType = m_MainDeclaration
        Next

        m_TypeImplementsClauses.Clauses.AddRange(Declaration.m_TypeImplementsClauses.Clauses)
        Declaration.m_TypeImplementsClauses = m_TypeImplementsClauses

        Compiler.Helper.AddCheck("Type argument names must be equal and have the same requirements (Type: " & Me.FullName & ")")

    End Sub

    Public Overrides Function ResolveType() As Boolean
        Dim result As Boolean = True

        Static recursive As Boolean
        If recursive Then Return True

        If m_TypeImplementsClauses IsNot Nothing Then
            result = m_TypeImplementsClauses.ResolveTypeReferences AndAlso result
            MyBase.ImplementedTypes = m_TypeImplementsClauses.GetTypes
        End If

        result = MyBase.ResolveType AndAlso result

        If Me.IsPartial Then
            recursive = True

            For Each item As PartialTypeDeclaration In m_PartialDeclarations
                If item IsNot Me Then result = item.ResolveType AndAlso result
            Next
            recursive = False

            If CheckForPartialKeyword() = False Then
                Dim first As PartialTypeDeclaration = Me
                For i As Integer = 0 To m_PartialDeclarations.Count - 1
                    Dim current As PartialTypeDeclaration = m_PartialDeclarations(i)
                    Dim parent, parentname As String

                    If current Is Me Then Continue For

                    If current.IsNestedType Then
                        parent = current.DeclaringType.DescriptiveType
                        parentname = current.DeclaringType.Name
                    Else
                        parent = "namespace"
                        parentname = current.Namespace
                        If parentname = String.Empty Then parentname = "<Default>"
                    End If
                    'We show the error twice, with the location of each type
                    'vbc doesn't do it like this, which has always bothered me since the class with the error is almost
                    'always the one with the location that's not reported.
                    Compiler.Report.ShowMessage(Messages.VBNC30179, first.Location, first.DescriptiveType, first.Name, current.DescriptiveType, current.Name, parent, parentname)
                Next
                result = False
            End If
            If TypeOf Me Is ClassDeclaration Then
                Dim inheritedTypes() As Type
                inheritedTypes = GetInheritedTypes()
                If inheritedTypes.Length > 0 Then
                    Dim tmpType As Type
                    tmpType = CheckUniqueType(inheritedTypes)
                    If tmpType Is Nothing Then
                        Return Helper.AddError(Me, "Partial classes must inherit from only one base class.")
                    Else
                        BaseType = tmpType
                    End If
                Else
                    Helper.Assert(BaseType IsNot Nothing) 'Should already be set to System.Object.
                End If
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Static recursive As Boolean
        If recursive Then Return True

        result = MyBase.ResolveTypeReferences AndAlso result

        If Me.IsPartial Then
            recursive = True
            For Each item As PartialTypeDeclaration In m_PartialDeclarations
                'If item IsNot Me Then result = item.ResolveTypeReferences AndAlso result
            Next
            recursive = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Checks that all types are equal.
    ''' Returns nothing if types are not equal.
    ''' </summary>
    ''' <param name="Types"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUniqueType(ByVal Types() As Type) As Type
        Helper.Assert(Types.Length >= 1)
        For i As Integer = 1 To Types.Length - 1
            If Helper.CompareType(Types(0), Types(i)) = False Then Return Nothing
        Next
        Return Types(0)
    End Function

    ''' <summary>
    ''' Returns the inherited types of all the partial classes.
    ''' There will be one type for every class that has an inherits clause.
    ''' Types may be duplicated.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInheritedTypes() As Type()
        Helper.Assert(Me.IsPartial)
        If TypeOf Me Is StructureDeclaration Then Return New Type() {}
        Dim result As New Generic.List(Of Type)
        For Each partialDeclaration As ClassDeclaration In m_PartialDeclarations
            If partialDeclaration.Inherits IsNot Nothing Then
                Helper.Assert(partialDeclaration.Inherits.ResolvedType IsNot Nothing)
                result.Add(partialDeclaration.Inherits.ResolvedType)
            End If
        Next
        Return result.ToArray
    End Function

    Private Function CheckForPartialKeyword() As Boolean
        Helper.Assert(Me.IsPartial)
        For Each partialDeclaration As PartialTypeDeclaration In m_PartialDeclarations
            If partialDeclaration.Modifiers.Is(ModifierMasks.Partial) Then Return True
        Next
        Return False
    End Function

    Public Overrides Function DefineTypeHierarchy() As Boolean
        Dim result As Boolean = True

        If m_TypeImplementsClauses IsNot Nothing Then
            Dim tmp As New Generic.List(Of Type)
            For Each clause As NonArrayTypeName In m_TypeImplementsClauses.Clauses
                tmp.Add(clause.ResolvedType)
                'Dim type As Type
                'type = Helper.GetTypeOrTypeBuilder(clause.ResolvedType)
                'TypeBuilder.AddInterfaceImplementation(type)
            Next
            ImplementedTypes = tmp.ToArray
        End If

        result = MyBase.DefineTypeHierarchy() AndAlso result

        Return result
    End Function
End Class
