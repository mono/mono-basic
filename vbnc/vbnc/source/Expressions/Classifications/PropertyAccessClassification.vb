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

Imports System.Reflection

''' <summary>
''' Every property access has an associated type, namely the type of the
''' property. A property access may have an associated instance expression.
''' 
''' •	A property access can be reclassified as a value. The property access 
''' expression is interpreted as an invocation expression of the Get accessor 
''' of the property. If the property has no getter, then a compile-time error occurs.
''' </summary>
''' <remarks></remarks>
Public Class PropertyAccessClassification
    Inherits ExpressionClassification

    Private m_LateBoundExpression As LateBoundAccessToPropertyAccessExpression
    Private m_InstanceExpression As Expression
    Private m_Parameters As ArgumentList

    Private m_Property As Mono.Cecil.PropertyReference
    Private m_Classification As PropertyGroupClassification

    ReadOnly Property Parameters() As ArgumentList
        Get
            If m_Parameters Is Nothing AndAlso m_Classification IsNot Nothing Then
                m_Parameters = m_Classification.Parameters
            End If
            Return m_Parameters
        End Get
    End Property

    ReadOnly Property ResolvedProperty() As Mono.Cecil.PropertyReference
        Get
            Return m_Property
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim method As Mono.Cecil.MethodReference

        If m_LateBoundExpression IsNot Nothing Then Return m_LateBoundExpression.GenerateCode(Info)

        Dim rside As EmitInfo
        rside = Info.Clone(Parent, True)

        If m_Classification IsNot Nothing Then
            m_InstanceExpression = m_Classification.InstanceExpression
            m_Property = m_Classification.ResolvedProperty
            m_Parameters = m_Classification.Parameters
        End If

        Helper.Assert(m_Property IsNot Nothing)

        If Info.IsLHS Then
            method = CecilHelper.GetCorrectMember(CecilHelper.FindDefinition(m_Property).SetMethod, m_Property.DeclaringType)
        Else
            method = CecilHelper.GetCorrectMember(CecilHelper.FindDefinition(m_Property).GetMethod, m_Property.DeclaringType)
        End If

        Helper.Assert(method IsNot Nothing)

        'If m_InstanceExpression IsNot Nothing Then
        '    rside = rside.Clone(m_Property.DeclaringType)
        '    result = m_InstanceExpression.GenerateCode(rside) AndAlso result
        'End If

        Dim exp As Expression()
        Dim args As ArgumentList
        Dim expCount As Integer

        If m_Parameters IsNot Nothing Then
            expCount = m_Parameters.Count
        End If
        If Info.IsLHS Then
            expCount += 1
        End If
        ReDim exp(expCount - 1)

        If m_Parameters IsNot Nothing Then
            'Dim params() As ParameterInfo = Helper.GetParameters(Compiler, method)
            'If Info.IsLHS Then
            '    'Remove the last parameter, it is a setter.
            '    Helper.Assert(params.GetUpperBound(0) >= 0)
            '    Dim tmpParameters As ParameterInfo()
            '    ReDim tmpParameters(params.GetUpperBound(0) - 1)
            '    Array.Copy(params, tmpParameters, tmpParameters.Length)
            '    params = tmpParameters
            'End If
            'result = m_Parameters.GenerateCode(rside, params) AndAlso result

            For i As Integer = 0 To m_Parameters.Count - 1
                exp(i) = m_Parameters(i).Expression
            Next
        End If

        If Info.IsLHS Then
            ' rside = rside.Clone(m_Property.PropertyType)
            'result = Info.RHSExpression.GenerateCode(rside) AndAlso result

            exp(expCount - 1) = Info.RHSExpression
        End If

        'Emitter.EmitCallOrCallVirt(Info, method)

        args = New ArgumentList(Me.Parent, exp)

        Helper.EmitArgumentsAndCallOrCallVirt(Info, m_InstanceExpression, args, method)

        Return result
    End Function

    <Obsolete()> Overloads Function ReclassifyToValue() As ValueClassification
        Return New ValueClassification(Me)
    End Function

    ReadOnly Property InstanceExpression() As Expression
        Get
            If m_InstanceExpression Is Nothing AndAlso m_Classification IsNot Nothing Then
                m_InstanceExpression = m_Classification.InstanceExpression
            End If
            Return m_InstanceExpression
        End Get
    End Property

    Property [Property]() As Mono.Cecil.PropertyReference
        Get
            Return m_Property
        End Get
        Set(ByVal value As Mono.Cecil.PropertyReference)
            m_Property = value
        End Set
    End Property

    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            If m_Property Is Nothing Then
                Helper.Assert(m_Classification IsNot Nothing)
                Helper.Assert(m_Classification.IsResolved)
                m_Property = m_Classification.ResolvedProperty
            End If
            Helper.Assert(m_Property IsNot Nothing)
            Return m_Property.PropertyType
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal [Property] As Mono.Cecil.PropertyReference, ByVal InstanceExpression As Expression, ByVal Parameters As ArgumentList)
        MyBase.New(Classifications.PropertyAccess, Parent)
        m_Property = [Property]
        m_InstanceExpression = InstanceExpression
        m_Parameters = Parameters
    End Sub

    Sub New(ByVal Classification As PropertyGroupClassification)
        MyBase.New(Classifications.PropertyAccess, Classification.Parent)
        m_Classification = Classification
    End Sub

    Sub New(ByVal Expression As LateBoundAccessToPropertyAccessExpression)
        MyBase.New(Classifications.PropertyAccess, Expression.Parent)
        m_LateBoundExpression = Expression
    End Sub
End Class
