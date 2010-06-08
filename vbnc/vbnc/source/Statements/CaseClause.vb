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
''' CaseClause  ::=
'''	   [ "Is" ]  ComparisonOperator  Expression  |
'''	   Expression  [ "To" Expression  ]
''' ComparisonOperator  ::=  "="  |  "&lt;&gt;" | "&lt;" | "&gt;" | "=&gt;" | "=&lt;"
''' </summary>
''' <remarks></remarks>
Public Class CaseClause
    Inherits ParsedObject

    Public Shared ReadOnly RelationalOperators As KS() = New KS() {KS.Equals, KS.NotEqual, KS.GT, KS.GE, KS.LT, KS.LE}

    Private m_Expression1 As Expression
    Private m_Expression2 As Expression
    Private m_Comparison As KS = KS.Equals

    Private m_ComparisonExpression As Expression

    ReadOnly Property Expression1() As Expression
        Get
            Return m_Expression1
        End Get
    End Property

    ReadOnly Property Expression2() As Expression
        Get
            Return m_Expression2
        End Get
    End Property

    ReadOnly Property Comparison() As KS
        Get
            Return m_Comparison
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Expression1 IsNot Nothing Then result = m_Expression1.ResolveTypeReferences AndAlso result
        If m_Expression2 IsNot Nothing Then result = m_Expression2.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal Expression1 As Expression, ByVal Expression2 As Expression, ByVal Comparison As KS)
        m_Expression1 = Expression1
        m_Expression2 = Expression2
        m_Comparison = Comparison
    End Sub

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim casestmt As CaseStatement = Me.FindFirstParent(Of CaseStatement)()
        Dim selectstmt As SelectStatement = Me.FindFirstParent(Of SelectStatement)()

        result = m_ComparisonExpression.GenerateCode(Info.Clone(Me, True, False, Compiler.TypeCache.System_Boolean)) AndAlso result

        Emitter.EmitBranchIfTrue(Info, casestmt.StartCode)

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Expression1.ResolveExpression(Info) AndAlso result
        result = Helper.VerifyValueClassification(m_Expression1, Info) AndAlso result

        Dim selectstmt As SelectStatement = Me.FindFirstParent(Of SelectStatement)()

        m_Expression1 = Helper.CreateTypeConversion(Me, m_Expression1, selectstmt.Test.ExpressionType, result)

        If m_Expression2 IsNot Nothing Then
            result = m_Expression2.ResolveExpression(Info) AndAlso result
            result = Helper.VerifyValueClassification(m_Expression2, Info) AndAlso result
            m_Expression2 = Helper.CreateTypeConversion(Me, m_Expression2, selectstmt.Test.ExpressionType, result)

            Dim lside, rside As Expression
            lside = New GEExpression(Me, selectstmt.CachedTest, m_Expression1)
            rside = New LEExpression(Me, selectstmt.CachedTest, m_Expression2)
            m_ComparisonExpression = New AndExpression(Me, lside, rside)
        Else
            Select Case m_Comparison
                Case KS.GT
                    m_ComparisonExpression = New GTExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case KS.LT
                    m_ComparisonExpression = New LTExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case KS.GE
                    m_ComparisonExpression = New GEExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case KS.LE
                    m_ComparisonExpression = New LEExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case KS.Equals, KS.None
                    m_ComparisonExpression = New EqualsExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case KS.NotEqual
                    m_ComparisonExpression = New NotEqualsExpression(Me, selectstmt.CachedTest, m_Expression1)
                Case Else
                    Throw New InternalException(Me)
            End Select
        End If
        result = m_ComparisonExpression.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result

        Return result
    End Function
End Class
