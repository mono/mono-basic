' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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

''' <summary>
''' ConstructedTypeName ::=	
'''     QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
'''     ConstructedTypeName "." QualifiedIdentifier [LAMESPEC]
'''     ConstructedTypeName "." QualifiedIdentifier "(" "Of" TypeArgumentList ")" [LAMESPEC]
''' 
''' LAMESPEC:
'''   A(Of T).B.C(Of S).D.E(Of U)
''' </summary>
''' <remarks></remarks>
Public Class ConstructedTypeName
    Inherits ParsedObject

    Private m_ConstructedTypeName As ConstructedTypeName
    Private m_QualifiedIdentifier As QualifiedIdentifier
    Private m_TypeArgumentList As TypeArgumentList

    Private m_ResolvedType As Mono.Cecil.TypeReference
    Private m_OpenResolvedType As Mono.Cecil.TypeReference
    Private m_ClosedResolvedType As Mono.Cecil.TypeReference

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal QualifiedIdentifier As QualifiedIdentifier, ByVal TypeArgumentList As TypeArgumentList)
        MyBase.new(Parent)
        m_QualifiedIdentifier = QualifiedIdentifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    Sub Init(ByVal ConstructedTypeName As ConstructedTypeName, ByVal QualifiedIdentifier As QualifiedIdentifier, ByVal TypeArgumentList As TypeArgumentList)
        m_ConstructedTypeName = ConstructedTypeName
        m_QualifiedIdentifier = QualifiedIdentifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    Sub Init(ByVal QualifiedIdentifier As QualifiedIdentifier, ByVal TypeArgumentList As TypeArgumentList)
        m_QualifiedIdentifier = QualifiedIdentifier
        m_TypeArgumentList = TypeArgumentList
    End Sub

    Public ReadOnly Property ConstructedTypeName() As ConstructedTypeName
        Get
            Return m_ConstructedTypeName
        End Get
    End Property

    ReadOnly Property OpenResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_OpenResolvedType
        End Get
    End Property

    ReadOnly Property ClosedResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ClosedResolvedType
        End Get
    End Property

    ReadOnly Property TypeArgumentList() As TypeArgumentList
        Get
            Return m_TypeArgumentList
        End Get
    End Property

    ReadOnly Property QualifiedIdentifier() As QualifiedIdentifier
        Get
            Return m_QualifiedIdentifier
        End Get
    End Property

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_TypeArgumentList IsNot Nothing Then result = m_TypeArgumentList.ResolveTypeReferences AndAlso result
        If m_ConstructedTypeName IsNot Nothing Then result = m_ConstructedTypeName.ResolveTypeReferences AndAlso result

        If result = False Then Return False

        If m_ConstructedTypeName IsNot Nothing Then
            Dim cache As MemberCache
            Dim entry As MemberCacheEntry
            Dim argumentCount As Integer
            Dim tp As TypeReference
            Dim git As GenericInstanceType
            Dim tpGit As Mono.Cecil.GenericInstanceType
            Dim allArguments As New Generic.List(Of TypeReference)
            Dim declType As TypeReference
            Dim declGit As GenericInstanceType

            tp = m_ConstructedTypeName.ResolvedType
            tpGit = TryCast(tp, GenericInstanceType)
            cache = Compiler.TypeManager.GetCache(CecilHelper.GetGenericTypeDefinition(m_ConstructedTypeName.ResolvedType))

            'Get the generic arguments
            declType = tp
            While declType IsNot Nothing
                declGit = TryCast(declType, GenericInstanceType)
                If declGit IsNot Nothing Then
                    allArguments.InsertRange(0, declGit.GenericArguments)
                End If
                declType = declType.DeclaringType
            End While

            If m_TypeArgumentList IsNot Nothing Then
                argumentCount = allArguments.Count + m_TypeArgumentList.Count
            Else
                argumentCount = allArguments.Count
            End If

            If m_TypeArgumentList IsNot Nothing Then
                For i As Integer = 0 To m_TypeArgumentList.Count - 1
                    allArguments.Add(m_TypeArgumentList(i).ResolvedType)
                Next
            End If

            entry = cache.LookupFlattened(Helper.CreateGenericTypename(m_QualifiedIdentifier.Name, If(m_TypeArgumentList IsNot Nothing, m_TypeArgumentList.Count, 0)), Me.FindFirstParent_IType.CecilType)

            If entry Is Nothing OrElse entry.Members.Count = 0 Then
                Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                Return False
            ElseIf entry.Members.Count > 1 Then
                Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                Return False
            Else
                Dim memberType As Mono.Cecil.TypeReference = TryCast(entry.Members(0), Mono.Cecil.TypeReference)
                If memberType IsNot Nothing Then
                    git = New GenericInstanceType(CecilHelper.GetGenericTypeDefinition(memberType))
                    git.GenericArguments.AddRange(allArguments)
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                    Return False
                End If
            End If

            If git IsNot Nothing Then
                m_ResolvedType = git
            Else
                m_ResolvedType = cache.Type
            End If
        ElseIf m_TypeArgumentList IsNot Nothing Then
            Dim nri As New TypeNameResolutionInfo(Me, Me, m_TypeArgumentList.Count)
            result = nri.Resolve AndAlso result

            If result = False Then Return result

            If nri.FoundOnlyOneObject Then
                If nri.FoundIs(Of IType)() Then
                    m_OpenResolvedType = nri.FoundAs(Of IType).CecilType
                ElseIf nri.FoundIs(Of Mono.Cecil.TypeReference)() Then
                    m_OpenResolvedType = nri.FoundAsType
                Else
                    Helper.AddError(Me)
                End If
                m_ClosedResolvedType = Compiler.TypeManager.MakeGenericType(Me, m_OpenResolvedType, m_TypeArgumentList.ArgumentCollection)
                m_ResolvedType = m_ClosedResolvedType
            Else
                Helper.AddError(Me)
            End If
        Else
            Helper.Stop()
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = VerifyConstraints(True) AndAlso result

        Return result
    End Function

    Function VerifyConstraints(ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        Dim parameters As Mono.Collections.Generic.Collection(Of GenericParameter)
        Dim arguments As Mono.Collections.Generic.Collection(Of TypeReference)
        Dim git As GenericInstanceType
        Dim td As TypeDefinition

        git = TryCast(m_ResolvedType, GenericInstanceType)
        td = CecilHelper.FindDefinition(git)

        If git Is Nothing OrElse td Is Nothing Then Return True

        parameters = td.GenericParameters
        arguments = git.GenericArguments

        result = Helper.VerifyConstraints(Me, parameters, arguments, ShowErrors)

        Return result
    End Function

    ReadOnly Property Name() As String
        Get
            Return m_QualifiedIdentifier.Name
        End Get
    End Property
End Class
