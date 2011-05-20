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
''' (not in documentation)
''' UnaryExpression ::= UnaryMinusExpression | UnaryNotExpression | UnaryPlusExpression
''' </summary>
''' <remarks></remarks>
Public MustInherit Class UnaryExpression
    Inherits OperatorExpression

    Private m_Expression As Expression
    Private m_ExpressionType As Mono.Cecil.TypeReference

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Protected Sub ValidateBeforeGenerateCode(ByVal Info As EmitInfo)
        Helper.Assert(Classification.IsValueClassification)
        Helper.Assert(Info.IsRHS)
    End Sub

#If DEBUG Then
    Protected MustOverride Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
#End If
    MustOverride ReadOnly Property Keyword() As KS

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(Enums.UnaryOperators)
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim operandType As TypeCode

        result = m_Expression.ResolveExpression(Info) AndAlso result

        If result = False Then Return False

        If m_Expression.Classification.IsValueClassification = False Then
            If m_Expression.Classification.CanBeValueClassification Then
                m_Expression = m_Expression.ReclassifyToValueExpression
                result = m_Expression.ResolveExpression(Info) AndAlso result
            Else
                result = Helper.AddError(Me, "Value must be value classification.") AndAlso result
            End If
        End If

        operandType = Me.OperandTypeCode

        If operandType = TypeCode.Empty Then
            Compiler.Report.ShowMessage(Messages.VBNC30487, Location, Enums.strSpecial(Me.Keyword), Helper.ToString(Expression, Expression.ExpressionType))
            result = False
        Else
            'If X is an intrinsic types, look up the result type in our operator tables and use that.
            'If X is not an intrinsic type, do overload resolution on the set of operators to be considered.
            Dim destinationType As Mono.Cecil.TypeReference
            Dim isRightIntrinsic As Boolean = Helper.GetTypeCode(Compiler, m_Expression.ExpressionType) <> TypeCode.Object OrElse Helper.CompareType(Compiler.TypeCache.System_Object, Me.m_Expression.ExpressionType)

            If isRightIntrinsic Then
                m_ExpressionType = Compiler.TypeResolution.TypeCodeToType(Me.ExpressionTypeCode)
                If Helper.GetTypeCode(Compiler, m_Expression.ExpressionType) <> operandType Then
                    Dim ctypeexp As CTypeExpression
                    destinationType = Compiler.TypeResolution.TypeCodeToType(operandType)
                    ctypeexp = New CTypeExpression(Me, m_Expression, destinationType)
                    result = ctypeexp.ResolveExpression(Info) AndAlso result
                    m_Expression = ctypeexp
                End If
                Classification = New ValueClassification(Me)
            Else
                Dim methods As New Generic.List(Of Mono.Cecil.MethodReference)
                Dim methodClassification As MethodGroupClassification

                methods = Helper.GetUnaryOperators(Compiler, CType(Me.Keyword, UnaryOperators), Me.m_Expression.ExpressionType)

                methodClassification = New MethodGroupClassification(Me, Nothing, Nothing, New Expression() {Me.m_Expression}, methods.ToArray)
                result = methodClassification.ResolveGroup(New ArgumentList(Me, New Expression() {Me.m_Expression})) AndAlso result
                result = methodClassification.SuccessfullyResolved AndAlso result
                m_ExpressionType = methodClassification.ResolvedMethodInfo.ReturnType
                Classification = methodClassification
            End If

            If Location.File(Compiler).IsOptionStrictOn AndAlso Helper.CompareType(m_Expression.ExpressionType, Compiler.TypeCache.System_Object) Then
                result = Compiler.Report.ShowMessage(Messages.VBNC30038, Me.Location, Enums.strSpecial(Keyword))
            End If
        End If

        Return result
    End Function

    ReadOnly Property ExpressionTypeCode() As TypeCode
        Get
            Return TypeConverter.GetUnaryResultType(Me.Keyword, Helper.GetTypeCode(Compiler, Expression.ExpressionType))
        End Get
    End Property

    ReadOnly Property OperandType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.TypeCodeToType(OperandTypeCode)
        End Get
    End Property

    ReadOnly Property OperandTypeCode() As TypeCode
        Get
            Return TypeConverter.GetUnaryOperandType(Me.Keyword, ExpressionTypeCode)
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return m_ExpressionType
        End Get
    End Property
End Class

