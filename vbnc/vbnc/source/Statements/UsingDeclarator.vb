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
''' Homebrew:
''' UsingDeclarator ::= 
'''  Identifier  [  As  [  New  ]  NonArrayTypeName  [  (  ArgumentList  )  ]  ]  |
'''  Identifier  [  As  NonArrayTypeName  ]  [  =  VariableInitializer  ]
'''
''' </summary>
''' <remarks></remarks>
Public Class UsingDeclarator
    Inherits ParsedObject

    Private m_Identifier As Identifier
    Private m_IsNew As Boolean
    Private m_TypeName As NonArrayTypeName
    Private m_ArgumentList As ArgumentList
    Private m_VariableInitializer As VariableInitializer
    ''' <summary>
    ''' If using a variable declared here or elsewhere.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_IsVariableDeclaration As Boolean
    Private m_VariableDeclaration As LocalVariableDeclaration

    Public UsingVariable As Mono.Cecil.Cil.VariableDefinition
    Public UsingVariableType As Mono.Cecil.TypeReference

    Private m_Constructor As Mono.Cecil.MethodReference

    ReadOnly Property VariableInitializer() As VariableInitializer
        Get
            Return m_VariableInitializer
        End Get
    End Property

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property ArgumentList() As ArgumentList
        Get
            Return m_ArgumentList
        End Get
    End Property

    ReadOnly Property IsNew() As Boolean
        Get
            Return m_isnew
        End Get
    End Property

    ReadOnly Property TypeName() As NonArrayTypeName
        Get
            Return m_TypeName
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal IsNew As Boolean, ByVal TypeName As NonArrayTypeName, ByVal ArgumentList As ArgumentList, ByVal VariableInitializer As VariableInitializer, ByVal IsVariableDeclaration As Boolean, ByVal VariableDeclaration As LocalVariableDeclaration)
        m_Identifier = Identifier
        m_IsNew = IsNew
        m_TypeName = TypeName
        m_ArgumentList = ArgumentList
        m_VariableInitializer = VariableInitializer
        m_IsVariableDeclaration = IsVariableDeclaration
        m_VariableDeclaration = VariableDeclaration
    End Sub

    ReadOnly Property IsVariableDeclaration() As Boolean
        Get
            Return m_IsVariableDeclaration
        End Get
    End Property

    ReadOnly Property VariableDeclaration() As LocalVariableDeclaration
        Get
            Return m_VariableDeclaration
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_IsVariableDeclaration Then
            result = m_VariableDeclaration.GenerateCode(Info.Clone(Me, True, False, UsingVariableType)) AndAlso result
            UsingVariable = m_VariableDeclaration.LocalBuilder
        Else
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End If

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = Helper.ResolveTypeReferences(m_TypeName, m_ArgumentList, m_VariableInitializer) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        'TODO: Resolve identifier
        If m_ArgumentList IsNot Nothing Then
            result = m_ArgumentList.ResolveCode(Info) AndAlso result
        End If

        If m_TypeName IsNot Nothing Then
            UsingVariableType = m_TypeName.ResolvedType
            m_IsVariableDeclaration = True
            If m_IsNew Then
                Dim grp As New MethodGroupClassification(Me, Nothing, Nothing, Nothing, CecilHelper.GetConstructors(CecilHelper.FindDefinition(m_TypeName.ResolvedType)))
                result = grp.ResolveGroup(m_ArgumentList) AndAlso result
                m_Constructor = grp.ResolvedConstructor
                If m_Constructor Is Nothing Then
                    result = Helper.AddError(Me) AndAlso result
                End If
            End If
        ElseIf m_VariableInitializer IsNot Nothing Then
            UsingVariableType = Compiler.TypeCache.System_Object
            m_IsVariableDeclaration = True
        Else
            Helper.Assert(m_ArgumentList Is Nothing)
            'Helper.Assert(m_Identifier IsNot Nothing)
            m_IsVariableDeclaration = False
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End If

        If m_VariableInitializer IsNot Nothing Then
            Dim expInfo As ExpressionResolveInfo
            expInfo = New ExpressionResolveInfo(Compiler, UsingVariableType)
            result = m_VariableInitializer.ResolveCode(expInfo) AndAlso result
        End If

        Return result
    End Function

End Class
