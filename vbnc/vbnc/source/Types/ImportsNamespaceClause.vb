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
''' ImportsNamespaceClause  ::=	QualifiedIdentifier  |	ConstructedTypeName
''' 
''' ConstructedTypeName  ::=
'''	QualifiedIdentifier  "("  "Of"  TypeArgumentList  ")"
'''    
''' Only namespaces, classes, structures, enumerated types, and standard modules may be imported.
''' </summary>
''' <remarks></remarks>
Public Class ImportsNamespaceClause
    Inherits ParsedObject

    Private m_Object As BaseObject

    Private m_Namespace As [Namespace]
    Private m_Type As Mono.Cecil.TypeReference

    ReadOnly Property [Object]() As BaseObject
        Get
            Return m_Object
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Location As Span)
        MyBase.New(Parent, Location)
        'Helper.Assert(Me.HasLocation)
    End Sub

    Sub Init(ByVal Obj As BaseObject)
        m_Object = Obj
    End Sub

    ReadOnly Property NamespaceImported() As [Namespace]
        Get
            Return m_Namespace
        End Get
    End Property

    ReadOnly Property TypeImported() As Mono.Cecil.TypeReference 'Descriptor
        Get
            Return m_Type
        End Get
    End Property

    ReadOnly Property IsNamespaceImport() As Boolean
        Get
            Return m_Namespace IsNot Nothing
        End Get
    End Property

    ReadOnly Property IsTypeImport() As Boolean
        Get
            Return m_Type IsNot Nothing
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim nri As TypeNameResolutionInfo
        If IsConstructedTypeName Then
            nri = New TypeNameResolutionInfo(AsConstructedTypeName, Me)
        ElseIf IsQualifiedIdentifier Then
            nri = New TypeNameResolutionInfo(AsQualifiedIdentifier, Me)
        Else
            Throw New InternalException(Me)
        End If
        nri.IsImportsResolution = True
        result = nri.Resolve AndAlso result

        If nri.FoundOnlyOneObject = False Then
            'Do not propage error condition here, since this message is a warning
			Compiler.Report.ShowMessage(Messages.VBNC40056, Location, Name)
            Return True
        End If

        If nri.FoundIs(Of [Namespace])() Then
            m_Namespace = nri.FoundAs(Of [Namespace])()
        ElseIf nri.FoundIs(Of TypeDeclaration)() Then
            m_Type = nri.FoundAs(Of TypeDeclaration).CecilType
        ElseIf nri.FoundIs(Of Type)() Then
            m_Type = nri.FoundAs(Of Mono.Cecil.TypeReference)()
        ElseIf nri.FoundIs(Of Mono.Cecil.TypeReference)() Then
            m_Type = nri.FoundAs(Of Mono.Cecil.TypeReference)()
        Else
            Helper.AddError(Me)
        End If
        Return result
    End Function

    Private ReadOnly Property AsConstructedTypeName() As ConstructedTypeName
        Get
            Return DirectCast(m_Object, ConstructedTypeName)
        End Get
    End Property

    Private ReadOnly Property AsQualifiedIdentifier() As QualifiedIdentifier
        Get
            Return DirectCast(m_Object, QualifiedIdentifier)
        End Get
    End Property

    Private ReadOnly Property IsQualifiedIdentifier() As System.Boolean
        Get
            Return TypeOf m_Object Is QualifiedIdentifier
        End Get
    End Property

    Private ReadOnly Property IsConstructedTypeName() As System.Boolean
        Get
            Return TypeOf m_Object Is ConstructedTypeName
        End Get
    End Property

    ''' <summary>
    ''' The namespace or type that is imported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            If IsQualifiedIdentifier Then
                Return AsQualifiedIdentifier.Name
            ElseIf IsConstructedTypeName Then
                Return AsConstructedTypeName.Name
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

End Class
