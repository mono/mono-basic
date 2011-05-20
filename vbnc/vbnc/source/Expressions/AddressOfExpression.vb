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
''' Used to produce a method pointer.
''' Classification: MethodPointer
''' 
''' AddressOfExpression  ::= "AddressOf" Expression
''' 
''' An AddressOf expression is used to produce a method pointer. The expression consists of 
''' the AddressOf keyword and an expression that must be classified as a method group. The 
''' method group cannot be late-bound and it cannot refer to constructors.
''' 
''' The result is classified as a method pointer and the associated instance expression and type
''' argument list (if any) is the same as the associated instance expression and type argument 
''' list of the method group.
''' </summary>
''' <remarks></remarks>
Public Class AddressOfExpression
    Inherits Expression

    ''' <summary>
    ''' This expression must be classified as MethodGroup.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Expression As Expression
    Private m_ExpressionType As Mono.Cecil.TypeReference

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = Classification.AsMethodPointerClassification.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Public Function Clone() As AddressOfExpression
        Dim result As New AddressOfExpression(Parent, m_Expression)
        Dim mpc As MethodPointerClassification
        If Classification IsNot Nothing Then
            mpc = Classification.AsMethodPointerClassification
            If mpc.MethodGroup.MethodDeclaration IsNot Nothing Then
                result.Init(mpc.MethodGroup.MethodDeclaration, mpc.MethodGroup.InstanceExpression)
            End If
        End If
        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent)
        m_Expression = Expression
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Sub Init(ByVal Method As MethodDeclaration, ByVal InstanceExpression As Expression)
        Classification = New MethodPointerClassification(Me, New MethodGroupClassification(Me, InstanceExpression, Nothing, Method))
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.AddressOf)
    End Function

    Function Resolve(ByVal DelegateType As Mono.Cecil.TypeReference, ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        result = Classification.AsMethodPointerClassification.Resolve(DelegateType, ShowErrors) AndAlso result
        m_ExpressionType = DelegateType

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If Classification IsNot Nothing Then Return True

        result = m_Expression.ResolveExpression(New ResolveInfo(Info.Compiler, True)) AndAlso result

        If result = False Then Return False

        If m_Expression.Classification.IsMethodGroupClassification Then
            Dim mpClassification As MethodPointerClassification
            mpClassification = New MethodPointerClassification(Me, m_Expression.Classification.AsMethodGroupClassification)
            Classification = mpClassification

            m_ExpressionType = Info.Compiler.TypeCache.DelegateUnresolvedType
        Else
            Helper.AddError(Me, Me.Location.ToString(Compiler))
        End If

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            If MyBase.IsResolved Then
                Return m_ExpressionType
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property
End Class

