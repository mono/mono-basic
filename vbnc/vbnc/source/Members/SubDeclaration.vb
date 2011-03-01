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
''' SubDeclaration  ::=
'''	[  Attributes  ]  [  ProcedureModifier+  ] "Sub" SubSignature  [  HandlesOrImplements  ]  LineTerminator
'''	Block
'''	"End" "Sub" StatementTerminator
''' 
''' MustOverrideSubDeclaration  ::=
'''	[  Attributes  ]  [  MustOverrideProcedureModifier+  ] "Sub" SubSignature  [  HandlesOrImplements  ]
'''		StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class SubDeclaration
    Inherits MethodDeclaration

    Private m_HandlesOrImplements As HandlesOrImplements

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Protected Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.new(Parent)
    End Sub

    Sub New(ByVal Parent As TypeDeclaration, ByVal Name As String, ByVal MethodAttributes As Mono.Cecil.MethodAttributes, ByVal ParameterTypes As Mono.Cecil.TypeReference())
        MyBase.New(Parent)
        MyBase.Init(New Modifiers(), New SubSignature(Me, Name, ParameterTypes))
        MyBase.MethodAttributes = MethodAttributes
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Block As CodeBlock)
        MyBase.Init(Modifiers, Signature, Block)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal HandlesOrImplements As HandlesOrImplements, ByVal Block As CodeBlock)
        MyBase.Init(Modifiers, Signature, Block)
        m_HandlesOrImplements = HandlesOrImplements
    End Sub

    Protected Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal ImplementsClause As MemberImplementsClause, ByVal Block As CodeBlock)
        MyBase.Init(Modifiers, Signature, Block)
        If ImplementsClause IsNot Nothing Then m_HandlesOrImplements = New HandlesOrImplements(Me, ImplementsClause)
    End Sub

    Public Overrides ReadOnly Property HandlesOrImplements() As HandlesOrImplements
        Get
            Return m_HandlesOrImplements
        End Get
    End Property

    Function DefineHandlesOrImplements() As Boolean
        Dim result As Boolean = True

        If m_HandlesOrImplements IsNot Nothing Then
            Dim hclause As HandlesClause = m_HandlesOrImplements.HandlesClause
            Dim iclause As MemberImplementsClause = m_HandlesOrImplements.ImplementsClause
            If hclause IsNot Nothing Then
                'Helper.NotImplemented()
            ElseIf iclause IsNot Nothing Then
                result = iclause.DefineImplements(Me.CecilBuilder)
            Else
                Throw New InternalException(Me)
            End If
        End If

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Info Is Nothing Then
            Info = New EmitInfo(Me)
        End If

        result = DefineHandlesOrImplements() AndAlso result
        result = MyBase.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_HandlesOrImplements IsNot Nothing Then result = m_HandlesOrImplements.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_HandlesOrImplements IsNot Nothing Then result = m_HandlesOrImplements.ResolveCode(Info) AndAlso result

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    ReadOnly Property IsMustOverride() As Boolean
        Get
            Return Modifiers.Is(ModifierMasks.MustOverride)
        End Get
    End Property

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.MustOverrideProcedureModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Sub AndAlso tm.PeekToken(i + 1).IsIdentifier
    End Function

End Class
