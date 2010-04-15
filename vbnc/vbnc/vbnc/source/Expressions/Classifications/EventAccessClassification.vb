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
''' Every event access has an associated type, namely the type of the event.
''' An event access may have an associated instance expression. An event access 
''' may appear as the first argument of the RaiseEvent, AddHandler and RemoveHandler
''' statements. In any other context, an expression classified as an event access
''' causes a compile-time error.
''' </summary>
''' <remarks></remarks>
Public Class EventAccessClassification
    Inherits ExpressionClassification

    Private m_EventInfo As Mono.Cecil.EventReference
    Private m_InstanceExpression As Expression

    ReadOnly Property EventInfo() As Mono.Cecil.EventReference
        Get
            Return m_EventInfo
        End Get
    End Property

    ''' <summary>
    ''' The type that contains the add_, remove_ and raise_ methods.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EventType() As Mono.Cecil.TypeReference
        Get
            Return m_EventInfo.EventType
        End Get
    End Property

    ''' <summary>
    ''' Loads the instance expression onto the evaluation stack.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If m_InstanceExpression IsNot Nothing Then
            result = m_InstanceExpression.GenerateCode(Info.Clone(Parent, True, False, m_InstanceExpression.ExpressionType)) AndAlso result
        End If

        Return result
    End Function

    ''' <summary>
    ''' The delegate type of the event handler.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Type() As Mono.Cecil.TypeReference
        Get
            Return m_EventInfo.EventType
        End Get
    End Property

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_InstanceExpression
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal EventInfo As Mono.Cecil.EventReference, Optional ByVal InstanceExpression As Expression = Nothing)
        MyBase.new(Classifications.EventAccess, Parent)
        m_EventInfo = EventInfo
        m_InstanceExpression = InstanceExpression
    End Sub
End Class
