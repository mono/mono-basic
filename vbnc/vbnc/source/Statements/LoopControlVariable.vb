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

''' <summary>
''' LoopControlVariable  ::=
'''	   Identifier  [  ArrayNameModifier  ] "As" TypeName  |
'''	   Expression
''' </summary>
''' <remarks></remarks>
Public Class LoopControlVariable
    Inherits ParsedObject

    Private m_Identifier As Identifier
    Private m_ArrayNameModifier As ArrayNameModifier
    Private m_TypeName As TypeName
    Private m_Expression As Expression

    Private m_Declaration As VariableDeclaration

    ReadOnly Property Identifier() As Identifier
        Get
            Return m_Identifier
        End Get
    End Property

    ReadOnly Property ArrayNameModifier() As ArrayNameModifier
        Get
            Return m_ArrayNameModifier
        End Get
    End Property

    ReadOnly Property TypeName() As TypeName
        Get
            Return m_typename
        End Get
    End Property

    ReadOnly Property Expression() As expression
        Get
            Return m_Expression
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_ArrayNameModifier IsNot Nothing Then result = m_ArrayNameModifier.ResolveTypeReferences AndAlso result
        If m_TypeName IsNot Nothing Then result = m_TypeName.ResolveTypeReferences AndAlso result
        If m_Expression IsNot Nothing Then result = m_Expression.ResolveTypeReferences AndAlso result

        If m_Declaration IsNot Nothing Then result = m_Declaration.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Identifier As Identifier, ByVal ArrayNameModifier As ArrayNameModifier, ByVal TypeName As TypeName, ByVal Expression As Expression)
        m_Identifier = Identifier
        m_ArrayNameModifier = ArrayNameModifier
        m_TypeName = TypeName
        m_Expression = Expression
    End Sub

    Function GetVariableDeclaration() As VariableDeclaration
        Return m_Declaration
    End Function

    ReadOnly Property IsVariableDeclaration() As Boolean
        Get
            Return m_TypeName IsNot Nothing
        End Get
    End Property

    ''' <summary>
    ''' Store the stack value into the loop control variable.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function EmitStoreVariable(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Helper.Assert(Info.RHSExpression IsNot Nothing)
        If m_Declaration IsNot Nothing Then
            Helper.Assert(m_Declaration.LocalBuilder IsNot Nothing)
            result = Info.RHSExpression.Classification.GenerateCode(Info.Clone(Me, True, False, m_Declaration.LocalBuilder.LocalType)) AndAlso result
            Emitter.EmitStoreVariable(Info, m_Declaration.LocalBuilder)
        Else
            result = m_Expression.GenerateCode(Info) AndAlso result
        End If
        Return result
    End Function

    ''' <summary>
    ''' Loads the loop control variable onto the stack
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function EmitLoadVariable(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        If m_Declaration IsNot Nothing Then
            Helper.Assert(m_Declaration.LocalBuilder IsNot Nothing)
            Emitter.EmitLoadVariable(Info, m_Declaration.LocalBuilder)
        Else
            result = m_Expression.GenerateCode(Info.Clone(Me, True, False)) AndAlso result
        End If
        Return result
    End Function

    ''' <summary>
    ''' This creates the variable.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_Declaration IsNot Nothing Then
            result = m_Declaration.DefineLocalVariable(Info) AndAlso result
            result = m_Declaration.GenerateCode(Info) AndAlso result
        Else
            'm_LoopVariableBuilder()
        End If

        Return result
    End Function

    ReadOnly Property VariableType() As Type
        Get
            If m_Expression IsNot Nothing Then
                Return m_Expression.ExpressionType
            Else
                Return m_TypeName.ResolvedType
            End If
        End Get
    End Property

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression IsNot Nothing Then
            result = m_Expression.ResolveExpression(Info) AndAlso result

            Dim iie As InvocationOrIndexExpression = TryCast(m_Expression, InvocationOrIndexExpression)
            If iie IsNot Nothing AndAlso iie.IsLateBoundArray Then
                Return Compiler.Report.ShowMessage(Messages.VBNC30039, Location) AndAlso result
            End If
        Else
            'result = m_Identifier.Resolve AndAlso result
            'result = m_ArrayNameModifier.Resolve AndAlso result
            result = m_TypeName.ResolveTypeReferences AndAlso result
            m_Declaration = New VariableDeclaration(Me, Nothing, New Modifiers(), m_Identifier, False, m_TypeName, Nothing, Nothing)
            result = m_Declaration.ResolveTypeReferences() AndAlso result
            result = m_Declaration.ResolveMember(ResolveInfo.Default(Info.Compiler)) AndAlso result
            result = m_Declaration.ResolveCode(info) AndAlso result
        End If

        Return result
    End Function
End Class
