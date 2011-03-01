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
''' Represents a literal (constant) expression in code.
''' Classification: Value (as it's base class ConstantExpression)
''' 
''' LiteralExpression  ::=  Literal
''' </summary>
''' <remarks></remarks>
Public Class LiteralExpression
    Inherits ConstantExpression

    ''' <summary>
    ''' Save the value of the literal expression here.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Value As Token

    Public Overrides ReadOnly Property AsString() As String
        Get
            If TypeOf m_Value.LiteralValue Is String Then
                Return """" & m_Value.LiteralValue.ToString & """"
            Else
                Return m_Value.LiteralValue.ToString()
            End If
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Value As Token)
        MyBase.Init(Value.LiteralValue, CecilHelper.GetType(Compiler, Value.LiteralValue))
        m_Value = Value
    End Sub

    Shadows Sub Init(ByVal Value As Object, ByVal ExpressionType As TypeReference)
        MyBase.Init(Value, ExpressionType)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Info.IsRHS Then
            Helper.Assert(Me.Classification.IsValueClassification)
            If Me.Classification.CanBeValueClassification Then
                result = Me.Classification.GenerateCode(Info) AndAlso result
            Else
                Throw New InternalException(Me)
            End If
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.IsLiteral
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim constant As Object = Nothing

        If Not GetConstant(constant, True) Then Return False

        Classification = New ValueClassification(Me, ExpressionType, constant)
        Return True
    End Function
End Class