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
''' VariableInitializer  ::=  RegularInitializer  |  ArrayElementInitializer
''' RegularInitializer ::= Expression
''' </summary>
''' <remarks></remarks>
Public Class VariableInitializer
    Inherits ParsedObject

    Private m_Initializer As ParsedObject

    ReadOnly Property Initializer() As ParsedObject
        Get
            Return m_Initializer
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_Initializer.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal Initializer As ParsedObject)
        m_Initializer = Initializer
        Helper.StopIfDebugging(m_Initializer Is Nothing)
    End Sub

    ReadOnly Property InitializerExpression() As Expression
        Get
            Return TryCast(m_Initializer, Expression)
        End Get
    End Property

    ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Dim exp As Expression = InitializerExpression
            If exp IsNot Nothing Then
                Return exp.ExpressionType
            Else
                Return Nothing
            End If
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = m_Initializer.GenerateCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        Dim expInfo As ExpressionResolveInfo = TryCast(Info, ExpressionResolveInfo)

        result = m_Initializer.ResolveCode(Info) AndAlso result

        If result = False Then Return result

        Dim initExp As Expression = TryCast(m_Initializer, Expression)
        If initExp IsNot Nothing Then
            If initExp.Classification.IsValueClassification = False Then
                If initExp.Classification.IsMethodPointerClassification Then
                    Dim exp As ExpressionResolveInfo = TryCast(Info, ExpressionResolveInfo)
                    If exp IsNot Nothing AndAlso exp.LHSType IsNot Nothing Then
                        initExp = initExp.ReclassifyMethodPointerToValueExpression(exp.LHSType)
                    Else
                        initExp = initExp.ReclassifyToValueExpression
                    End If
                Else
                    initExp = initExp.ReclassifyToValueExpression
                End If
                If initExp Is Nothing Then
                    result = False
                Else
                    result = initExp.ResolveExpression(ResolveInfo.Default(Info.Compiler)) AndAlso result
                End If
            End If

            If result = False Then Return result

            If expInfo IsNot Nothing Then
                initExp = Helper.CreateTypeConversion(Me, initExp, expInfo.LHSType, result)
            Else
                Helper.StopIfDebugging()
            End If
            m_Initializer = initExp
        End If

        Return result
    End Function

    ReadOnly Property IsRegularInitializer() As Boolean
        Get
            Return TypeOf m_Initializer Is Expression
        End Get
    End Property

    ReadOnly Property AsRegularInitializer() As Expression
        Get
            Return DirectCast(m_Initializer, Expression)
        End Get
    End Property

    ReadOnly Property IsArrayElementInitializer() As Boolean
        Get
            Return TypeOf m_Initializer Is ArrayElementInitializer
        End Get
    End Property

    ReadOnly Property AsArrayElementInitializer() As ArrayElementInitializer
        Get
            Return DirectCast(m_Initializer, ArrayElementInitializer)
        End Get
    End Property

End Class

