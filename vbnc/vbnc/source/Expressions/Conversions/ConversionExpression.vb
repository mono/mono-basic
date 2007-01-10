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

Public MustInherit Class ConversionExpression
    Inherits Expression

    Private m_Expression As Expression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Expression.ResolveTypeReferences
    End Function

    ReadOnly Property Expression() As Expression
        Get
            Return m_Expression
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal Expression As Expression)
        MyBase.New(Parent)
        m_Expression = Expression
    End Sub

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Expression As Expression)
        m_Expression = Expression
    End Sub

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            If m_Expression.IsConstant = False Then
                Return False
            Else
                Dim value As Object
                value = m_Expression.ConstantValue
                Dim result As Object = Nothing
                If Compiler.TypeResolution.CheckNumericRange(value, result, ExpressionType) Then
                    Return True
                Else
                    Return False
                End If
                'Return TypeResolution.IsImplicitlyConvertible(ExpressionType, m_Expression.ExpressionType)
            End If
        End Get
    End Property

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_Expression.IsResolved = False Then
            result = m_Expression.ResolveExpression(Info) AndAlso result
        End If
        result = Helper.VerifyValueClassification(m_Expression, Info) AndAlso result
        Classification = New ValueClassification(Me)

        Return result
    End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("C" & ExpressionType.Name)
        m_Expression.Dump(Dumper)
    End Sub
#End If
End Class
