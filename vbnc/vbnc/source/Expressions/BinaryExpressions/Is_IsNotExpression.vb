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


Public Class Is_IsNotExpression
    Inherits BinaryExpression

    Private m_Keyword As KS
    Private m_DesiredNothingType As Mono.Cecil.TypeReference

    Overrides ReadOnly Property IsOverloadable() As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveExpressions(Info) AndAlso result

        If Not result Then Return False

        If CecilHelper.IsValueType(m_LeftExpression.ExpressionType) AndAlso CecilHelper.IsNullable(m_LeftExpression.ExpressionType) = False Then
            If Keyword = KS.Is Then
                Compiler.Report.ShowMessage(Messages.VBNC30020, Me.Location, Helper.ToString(Compiler, m_LeftExpression.ExpressionType))
            Else
                Compiler.Report.ShowMessage(Messages.VBNC31419, Me.Location, Helper.ToString(Compiler, m_LeftExpression.ExpressionType))
            End If
        End If
        If CecilHelper.IsValueType(m_RightExpression.ExpressionType) AndAlso CecilHelper.IsNullable(m_RightExpression.ExpressionType) = False Then
            If Keyword = KS.Is Then
                Compiler.Report.ShowMessage(Messages.VBNC30020, Me.Location, Helper.ToString(Compiler, m_RightExpression.ExpressionType))
            Else
                Compiler.Report.ShowMessage(Messages.VBNC31419, Me.Location, Helper.ToString(Compiler, m_RightExpression.ExpressionType))
            End If
        End If

        If result = False Then Return False

        result = MyBase.ResolveExpressionInternal(Info) AndAlso result

        If result AndAlso CecilHelper.IsGenericParameter(m_LeftExpression.ExpressionType) Then
            m_LeftExpression = New BoxExpression(Me, m_LeftExpression, m_LeftExpression.ExpressionType)
            m_DesiredNothingType = Compiler.TypeCache.System_Object
        End If
        If result AndAlso CecilHelper.IsGenericParameter(m_RightExpression.ExpressionType) Then
            m_RightExpression = New BoxExpression(Me, m_RightExpression, m_RightExpression.ExpressionType)
            m_DesiredNothingType = Compiler.TypeCache.System_Object
        End If

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        ValidateBeforeGenerateCode(Info)

        If CecilHelper.IsValueType(m_LeftExpression.ExpressionType) Then
            Throw New InternalException(Me)
        End If
        If CecilHelper.IsValueType(m_RightExpression.ExpressionType) Then
            Throw New InternalException(Me)
        End If

        Dim desiredType As Mono.Cecil.TypeReference = m_DesiredNothingType
        If TypeOf m_LeftExpression Is NothingConstantExpression = False Then
            If desiredType Is Nothing Then desiredType = m_LeftExpression.ExpressionType
        ElseIf TypeOf m_RightExpression Is NothingConstantExpression = False Then
            If desiredType Is Nothing Then desiredType = m_LeftExpression.ExpressionType
        Else
            'If Nothing Is / IsNot Nothing Then...
            Emitter.EmitLoadValue(Info, m_Keyword = KS.Is)
            Return result
        End If

        If desiredType IsNot Nothing Then
            If CecilHelper.IsByRef(desiredType) Then desiredType = CecilHelper.GetElementType(desiredType)
            Info = Info.Clone(Me, True, False, desiredType)
        End If

        result = m_LeftExpression.GenerateCode(Info) AndAlso result
        result = m_RightExpression.GenerateCode(Info) AndAlso result

        If Keyword = KS.Is Then
            Emitter.EmitIs(Info)
        ElseIf Keyword = KS.IsNot Then
            Emitter.EmitIsNot(Info)
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim rvalue As Object = Nothing
        Dim lvalue As Object = Nothing

        If Not m_LeftExpression.GetConstant(lvalue, ShowError) Then Return False
        If Not m_RightExpression.GetConstant(rvalue, ShowError) Then Return False

        If lvalue Is Nothing Or rvalue Is Nothing Then
            result = True
            Return True
        End If

        If ShowError Then Show30059()
        Return False
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Boolean
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal LExp As Expression, ByVal RExp As Expression, ByVal Keyword As KS)
        MyBase.New(Parent, LExp, RExp)
        m_Keyword = Keyword
        Helper.Assert(m_Keyword = KS.Is OrElse m_Keyword = KS.IsNot)
    End Sub

    Public Overrides ReadOnly Property Keyword() As KS
        Get
            Helper.Assert(m_Keyword = KS.Is OrElse m_Keyword = KS.IsNot)
            Return m_Keyword
        End Get
    End Property
End Class

