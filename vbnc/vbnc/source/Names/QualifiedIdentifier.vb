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
''' QualifiedIdentifier ::= Identifier | "Global" "." IdentifierOrKeyword | QualifiedIdentifier "." IdentifierOrKeyword
''' </summary>
''' <remarks></remarks>
Public Class QualifiedIdentifier
    Inherits ParsedObject

    Private m_First As ParsedObject
    Private m_Second As Token

    Private m_ResolvedType As Mono.Cecil.TypeReference

    Private m_Name As String

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Location As Span)
        MyBase.New(Parent, location)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal First As ParsedObject, ByVal Second As Token)
        MyBase.new(Parent)
        Me.Init(First, Second)
    End Sub

    Sub Init(ByVal First As ParsedObject, ByVal Second As Token)
        m_First = First
        m_Second = Second
    End Sub

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return m_ResolvedType
        End Get
    End Property

    Function ResolveAsTypeName(ByVal AsAttributeTypeName As Boolean, Optional ByVal TypeArity As Integer = 0) As Boolean
        Dim result As Boolean = True
        Dim nri As New TypeNameResolutionInfo(Me, Me)
        Dim resolvedType As Mono.Cecil.TypeReference

        nri.IsAttributeTypeName = AsAttributeTypeName
        nri.TypeArgumentCount = TypeArity
        result = nri.Resolve() AndAlso result
        If result = False Then Return result
        If nri.FoundOnlyOneObject Then
            If nri.FoundIsType Then
                resolvedType = nri.FoundAsType
            ElseIf nri.FoundIs(Of TypeParameter)() Then
                resolvedType = nri.FoundAsType 'New TypeParameterDescriptor(nri.FoundAs(Of TypeParameter)())
            ElseIf nri.FoundIs(Of ImportsClause)() Then
                Dim ic As ImportsClause = nri.FoundAs(Of ImportsClause)()
                If ic.IsNamespaceClause Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30182, Me.Location)
                ElseIf ic.AsAliasClause.Second.IsNamespaceImport Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30182, Me.Location)
                Else
                    resolvedType = ic.AsAliasClause.Second.TypeImported
                    If resolvedType Is Nothing Then
                        Return Compiler.Report.ShowMessage(Messages.VBNC30002, Me.Location, ic.AsAliasClause.Name)
                    End If
                End If
            Else
                resolvedType = Nothing
                Return Helper.AddError(Me, "Could not resolve: '" & Name & "'")
            End If
        ElseIf nri.FoundObjects.Count > 1 Then
            resolvedType = Nothing
            Return Helper.AddError(Me, "Could not resolve (>1 results): '" & Name & "'")
        Else
            resolvedType = Nothing
            Return Helper.AddError(Me, "Could not resolve (no result): '" & Name & "'")
        End If

        m_ResolvedType = resolvedType

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True 'Nothing to do here
    End Function

    ''' <summary>
    ''' Resolves this typename to a type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ResolveAsTypeName() As Boolean
        Return ResolveAsTypeName(False)
    End Function

    ReadOnly Property First() As ParsedObject
        Get
            Return m_First
        End Get
    End Property

    Property Second() As Token
        Get
            Return m_Second
        End Get
        Set(ByVal value As Token)
            m_Second = value
        End Set
    End Property

    ReadOnly Property FirstAsIdentifier() As Identifier
        Get
            Return DirectCast(m_First, Identifier)
        End Get
    End Property

    ReadOnly Property IsFirstIdentifier() As Boolean
        Get
            Return TypeOf m_First Is Identifier
        End Get
    End Property

    ReadOnly Property IsFirstGlobal() As Boolean
        Get
            Return TypeOf m_First Is GlobalExpression
        End Get
    End Property

    ReadOnly Property FirstAsGlobal() As GlobalExpression
        Get
            Return DirectCast(m_First, GlobalExpression)
        End Get
    End Property

    ReadOnly Property FirstAsQualifiedIdentifier() As QualifiedIdentifier
        Get
            Return DirectCast(m_First, QualifiedIdentifier)
        End Get
    End Property

    ReadOnly Property IsFirstQualifiedIdentifier() As Boolean
        Get
            Return TypeOf m_First Is QualifiedIdentifier
        End Get
    End Property

    ''' <summary>
    ''' The complete name for this qualified identifier.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            If m_Name Is Nothing Then
                m_Name = String.Empty
                If m_First IsNot Nothing Then
                    Dim id As Identifier = TryCast(m_First, Identifier)
                    Dim qid As QualifiedIdentifier = TryCast(m_First, QualifiedIdentifier)
                    If id IsNot Nothing Then
                        m_Name = id.Name
                    ElseIf qid IsNot Nothing Then
                        m_Name = qid.Name
                    Else
                        m_Name = m_First.ToString
                    End If
                Else
                    Throw New InternalException(Me)
                End If
                If Token.IsSomething(m_Second) Then
                    m_Name = m_Name & "." & m_Second.ToString
                End If
            End If
            Return m_Name
        End Get
    End Property

    Shared Function CanBeQualifiedIdentifier(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsIdentifier OrElse tm.CurrentToken.Equals(KS.Global)
    End Function

End Class
