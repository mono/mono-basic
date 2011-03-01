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

Public MustInherit Class EventHandlerDeclaration
    Inherits MethodDeclaration

    ''' <summary>
    ''' The type of this handler (AddHandler, RemoveHandler or RaiseEvent)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_HandlerType As KS

    Sub New(ByVal Parent As EventDeclaration)
        MyBase.new(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal HandlerType As KS, ByVal EventName As Identifier, ByVal Parameters As ParameterList, ByVal Code As CodeBlock)

        m_HandlerType = HandlerType

        Dim prefix As String
        Dim name As String
        Select Case m_HandlerType
            Case KS.AddHandler
                prefix = "add_"
            Case KS.RemoveHandler
                prefix = "remove_"
            Case KS.RaiseEvent
                prefix = "raise_"
            Case Else
                Throw New InternalException(Me)
        End Select
        name = prefix & EventName.Name

        Dim mySignature As SubSignature

        mySignature = New SubSignature(Me)
        mySignature.Init(New Identifier(mySignature, name, EventName.Location, EventName.TypeCharacter), Nothing, Parameters)

        MyBase.Init(Modifiers, mySignature, Code)
    End Sub

    ''' <summary>
    ''' The type of this handler (AddHandler, RemoveHandler or RaiseEvent)
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property HandlerType() As KS
        Get
            Return m_HandlerType
        End Get
    End Property

    ''' <summary>
    ''' The event declaration for this event handler.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EventParent() As EventDeclaration
        Get
            Return DirectCast(MyBase.Parent, EventDeclaration)
        End Get
    End Property
End Class
