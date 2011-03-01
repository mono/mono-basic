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
''' A late-bound access, which represents a method or property access deferred until run-time. 
''' A late-bound access may have an associated instance expression and an associated type argument list. 
''' The type of a late-bound access is always Object.
''' 
''' 	A late-bound access can be reclassified as a value.
''' 	A late-bound access can be reclassified as a late-bound method or late-bound property access. In a situation where a late-bound access can be reclassified both as a method access and as a property access, reclassification to a property access is preferred.
''' </summary>
''' <remarks></remarks>
Public Class LateBoundAccessClassification
    Inherits ExpressionClassification

    Private m_InstanceExpression As Expression
    Private m_TypeArguments As TypeArgumentList
    Private m_Name As String
    Private m_Arguments As ArgumentList
    Private m_LateBoundType As Mono.Cecil.TypeReference

    Property Arguments() As ArgumentList
        Get
            Return m_Arguments
        End Get
        Set(ByVal value As ArgumentList)
            m_Arguments = value
        End Set
    End Property

    ReadOnly Property Name() As String
        Get
            Return m_Name
        End Get
    End Property

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_InstanceExpression
        End Get
    End Property

    ReadOnly Property TypeArguments() As TypeArgumentList
        Get
            Return m_TypeArguments
        End Get
    End Property

    Property LateBoundType() As Mono.Cecil.TypeReference
        Get
            Return m_LateBoundType
        End Get
        Set(ByVal value As Mono.Cecil.TypeReference)
            m_LateBoundType = value
        End Set
    End Property

    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Object
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="InstanceExpression">May be Nothing</param>
    ''' <param name="TypeArguments">May be Nothing</param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal TypeArguments As TypeArgumentList, ByVal Name As String)
        MyBase.New(Classifications.LateBoundAccess, Parent)
        m_InstanceExpression = InstanceExpression
        m_Name = Name
        m_TypeArguments = TypeArguments
    End Sub
End Class
