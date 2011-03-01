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
''' EventAccessorDeclaration  ::= AddHandlerDeclaration | RemoveHandlerDeclaration | RaiseEventDeclaration
''' </summary>
''' <remarks></remarks>
Public Class EventAccessorDeclarations
    Inherits ParsedObject

    Private m_Handlers(2) As CustomEventHandlerDeclaration

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal [AddHandler] As CustomEventHandlerDeclaration, ByVal [RemoveHandler] As CustomEventHandlerDeclaration, ByVal [RaiseEvent] As CustomEventHandlerDeclaration)
        Me.AddHandler = [AddHandler]
        Me.RemoveHandler = [RemoveHandler]
        Me.RaiseEvent = [RaiseEvent]
    End Sub

    ReadOnly Property Handlers() As CustomEventHandlerDeclaration()
        Get
            Return m_Handlers
        End Get
    End Property

    Property [AddHandler]() As CustomEventHandlerDeclaration
        Get
            Return m_Handlers(0)
        End Get
        Private Set(ByVal value As CustomEventHandlerDeclaration)
            If m_Handlers(0) IsNot Nothing Then
                Helper.AddError(Me)
            End If
            m_Handlers(0) = value
        End Set
    End Property

    Property [RemoveHandler]() As CustomEventHandlerDeclaration
        Get
            Return m_Handlers(1)
        End Get
        Private Set(ByVal value As CustomEventHandlerDeclaration)
            If m_Handlers(1) IsNot Nothing Then
                Helper.AddError(Me)
            End If
            m_Handlers(1) = value
        End Set
    End Property

    Property [RaiseEvent]() As CustomEventHandlerDeclaration
        Get
            Return m_Handlers(2)
        End Get
        Private Set(ByVal value As CustomEventHandlerDeclaration)
            If m_Handlers(2) IsNot Nothing Then
                Helper.AddError(Me)
            End If
            m_Handlers(2) = value
        End Set
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return Helper.GenerateCodeCollection(m_Handlers, Info)
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        For i As Integer = 0 To m_Handlers.Length - 1
            result = m_Handlers(i).ResolveCode(Info) AndAlso result
        Next
        Return result
    End Function
End Class
