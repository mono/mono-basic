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
''' DictionaryAccessExpression ::= [Expression] "!" IdentifierOrKeyword
''' </summary>
''' <remarks></remarks>
Public Class DictionaryAccessExpression
    Inherits Expression

    ''' <summary>
    ''' The first part may be nothing in a with block.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_FirstPart As Expression
    Private m_SecondPart As IdentifierOrKeyword
    Private m_SecondExpression As Expression

    Private m_DefaultProperty As Mono.Cecil.PropertyReference
    Private m_WithStatement As WithStatement
    Private m_IsLateBound As Boolean

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_FirstPart IsNot Nothing Then result = m_FirstPart.ResolveTypeReferences AndAlso result
        If m_SecondPart IsNot Nothing Then result = m_SecondPart.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub Init(ByVal FirstPart As Expression, ByVal SecondPart As IdentifierOrKeyword)
        m_FirstPart = FirstPart
        m_SecondPart = SecondPart
    End Sub
    
    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            If m_IsLateBound Then Return Compiler.TypeCache.System_Object
            Return m_DefaultProperty.PropertyType
        End Get
    End Property

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_IsLateBound Then
            Dim ie As Expression
            Dim lbaC As LateBoundAccessClassification
            If m_FirstPart Is Nothing Then
                ie = New LoadLocalExpression(Me, m_WithStatement.WithVariable)
            Else
                ie = m_FirstPart
            End If
            lbaC = New LateBoundAccessClassification(Me, ie, Nothing, Nothing)
            lbaC.Arguments = New ArgumentList(Me, New ConstantExpression(Me, m_SecondPart.Identifier, Compiler.TypeCache.System_String))
            result = LateBoundAccessToExpression.EmitLateIndexGet(Info, lbaC) AndAlso result
        Else
            If m_FirstPart IsNot Nothing Then
                result = m_FirstPart.GenerateCode(Info) AndAlso result
            Else
                Emitter.EmitLoadVariable(Info, m_WithStatement.WithVariable)
            End If
            result = m_SecondExpression.GenerateCode(Info) AndAlso result
            If Info.IsRHS Then
                Emitter.EmitCallOrCallVirt(Info, CecilHelper.FindDefinition(m_DefaultProperty).GetMethod)
            ElseIf Info.IsLHS Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Else
                Throw New InternalException(Me)
            End If
        End If

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Dim firsttp As Mono.Cecil.TypeReference
        If m_FirstPart IsNot Nothing Then
            result = m_FirstPart.ResolveExpression(Info) AndAlso result
            firsttp = m_FirstPart.ExpressionType
        Else
            m_WithStatement = Me.FindFirstParent(Of WithStatement)()
            firsttp = m_WithStatement.WithVariableExpression.ExpressionType
        End If

        If Helper.CompareType(Compiler.TypeCache.System_Object, firsttp) Then
            If Location.File(Compiler).IsOptionStrictOn Then
                Helper.AddError(Me)
                Return False
            End If
            m_IsLateBound = True
            Classification = New ValueClassification(Me, Compiler.TypeCache.System_Object)
            Return True
        End If
        Dim attr As Mono.Cecil.CustomAttribute = Helper.GetDefaultMemberAttribute(Compiler, firsttp)
        If attr Is Nothing Then Return Compiler.Report.ShowMessage(Messages.VBNC30367, Me.Location, firsttp.Name)

        Dim name As String = DirectCast(attr.ConstructorArguments(0).Value, String)
        Dim props As Mono.Collections.Generic.Collection(Of PropertyDefinition)
        Dim pgc As PropertyGroupClassification
        Dim arguments As New ArgumentList(Me)
        Dim instanceExpression As Expression

        If m_FirstPart IsNot Nothing Then
            instanceExpression = m_FirstPart
        Else
            instanceExpression = m_WithStatement.WithVariableExpression
        End If

        m_SecondExpression = New ConstantExpression(Me, m_SecondPart.Identifier, Compiler.TypeCache.System_String)

        props = CecilHelper.FindProperties(CecilHelper.FindDefinition(firsttp).Properties, name)
        pgc = New PropertyGroupClassification(Me, instanceExpression, props)
        arguments.Arguments.Add(New PositionalArgument(arguments, 0, m_SecondExpression))
        If Not pgc.ResolveGroup(arguments) Then
            pgc.ResolveGroup(arguments, True)
            Return False
        End If

        m_DefaultProperty = pgc.ResolvedProperty
        Classification = pgc

        result = Helper.IsConvertible(Me, m_SecondExpression, m_SecondExpression.ExpressionType, m_DefaultProperty.Parameters(0).ParameterType, True, m_SecondExpression, True, Nothing) AndAlso result
        If result Then
            arguments.Arguments(0).Expression = m_SecondExpression
        End If

        Return result
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Exclamation)
    End Function

    Shared Function IsUnaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Exclamation)
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function
End Class

