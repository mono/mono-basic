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

Imports System.Reflection
Imports System.Reflection.Emit

#If DEBUG Then
#Const DEBUGEMISSION = DEBUG
#End If

Public Class EmitInfo
    ''' <summary>
    ''' Is this a right hand side expression?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_IsRHS As Boolean

    ''' <summary>
    ''' The right hand side expression.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_RHSExpression As Expression

    ''' <summary>
    ''' Is this an explicit conversion?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_IsExplicitConversion As Boolean

    ''' <summary>
    ''' The method where the code is located.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Method As IMethod

    ''' <summary>
    ''' The type of the values at the stack.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Stack As EmitStack

    ''' <summary>
    ''' The desired type to emit.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_DesiredType As Type


    Private m_InExceptionFilter As Boolean

    Private m_Context As ParsedObject

    ReadOnly Property Context() As ParsedObject
        Get
            Return m_Context
        End Get
    End Property

    ReadOnly Property Location() As Span
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Used by Emitter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InExceptionFilter() As Boolean
        Get
            Return m_InExceptionFilter
        End Get
        Set(ByVal value As Boolean)
            m_InExceptionFilter = value
        End Set
    End Property

    ReadOnly Property RHSExpression() As Expression
        Get
            Return m_RHSExpression
        End Get
    End Property

    ReadOnly Property IsOptionCompareText() As Boolean
        Get
            Return m_Method.Location.File(Compiler).IsOptionCompareText
        End Get
    End Property

    ReadOnly Property IntegerOverflowChecks() As Boolean
        Get
            Return Not Compiler.CommandLine.RemoveIntChecks
        End Get
    End Property

    ReadOnly Property IsExplicitConversion() As Boolean
        Get
            Return m_IsExplicitConversion
        End Get
    End Property

    ReadOnly Property DesiredType() As Type
        Get
            Return m_DesiredType
        End Get
    End Property

    ''' <summary>
    ''' Is this a right hand side expression?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsRHS() As Boolean
        Get
            Return m_IsRHS
        End Get
    End Property

    ''' <summary>
    ''' Is this a left hand side expression?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsLHS() As Boolean
        Get
            Return Not m_IsRHS
        End Get
    End Property

#If DEBUGEMISSION Then
    ''' <summary>
    ''' The ILGenerator used to emit the code.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ILGen() As EmitLog
        Get
            Static tmp As EmitLog
            If tmp Is Nothing Then tmp = New EmitLog(m_Method.ILGenerator, Compiler)
            Return tmp
        End Get
    End Property
#Else
    ''' <summary>
    ''' The ILGenerator used to emit the code.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ILGen() As ILGenerator
        Get
            Return m_Method.ILGenerator
        End Get
    End Property
#End If

    ReadOnly Property Stack() As EmitStack
        Get
            Return m_Stack
        End Get
    End Property

    ''' <summary>
    ''' The method where the code is located.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property Method() As IMethod
        Get
            Return m_Method
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Method.Compiler
        End Get
    End Property

    ''' <summary>
    ''' Create a new EmitInfo starting in the specified method.
    ''' </summary>
    ''' <param name="Method"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Method As IMethod)
        m_Method = Method
        m_Stack = New EmitStack(Compiler)
    End Sub

    ''' <summary>
    ''' Clone the emitinfo for a left hand side expression.
    ''' </summary>
    ''' <param name="RHSExpression"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Clone(ByVal Context As ParsedObject, ByVal RHSExpression As Expression) As EmitInfo
        Dim result As New EmitInfo(Me)

        result.m_IsRHS = False
        result.m_RHSExpression = RHSExpression
        result.m_DesiredType = Nothing
        result.m_Context = Context

        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Context"></param>
    ''' <param name="IsRHS">Default = True</param>
    ''' <param name="IsExplicitConversion"></param>
    ''' <param name="DesiredType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Clone(ByVal Context As ParsedObject, ByVal IsRHS As Boolean, Optional ByVal IsExplicitConversion As Boolean = False, Optional ByVal DesiredType As Type = Nothing) As EmitInfo
        Dim result As New EmitInfo(Me)
        result.m_IsRHS = IsRHS
        result.m_IsExplicitConversion = IsExplicitConversion
        result.m_DesiredType = DesiredType
        result.m_RHSExpression = Nothing
        result.m_Context = Context
        Return result
    End Function

    Function Clone(ByVal Context As ParsedObject, ByVal DesiredType As Type) As EmitInfo
        Dim result As New EmitInfo(Me)
        result.m_DesiredType = DesiredType
        result.m_RHSExpression = Nothing
        result.m_Context = Context
        Return result
    End Function

    ''' <summary>
    ''' Create a new EmitInfo copying the values from the specified info.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal Info As EmitInfo)
        Me.m_DesiredType = Info.m_DesiredType
        Me.m_IsExplicitConversion = Info.m_IsExplicitConversion
        Me.m_IsRHS = Info.m_IsRHS
        Me.m_Method = Info.m_Method
        Me.m_Stack = Info.m_Stack
        Me.m_RHSExpression = Info.m_RHSExpression
    End Sub
End Class
