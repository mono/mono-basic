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
''' AddHandlerStatement  ::= "AddHandler" Expression  ,  Expression  StatementTerminator
''' RemoveHandlerStatement  ::= "RemoveHandler" Expression "," Expression  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class AddOrRemoveHandlerStatement
    Inherits Statement

    Private m_Event As Expression
    Private m_EventHandler As Expression
    Private m_IsAddHandler As Boolean

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_Event IsNot Nothing Then result = m_Event.ResolveTypeReferences AndAlso result
        If m_EventHandler IsNot Nothing Then result = m_EventHandler.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Sub Init(ByVal [Event] As Expression, ByVal EventHandler As Expression, ByVal IsAddHandler As Boolean)
        m_Event = [Event]
        m_EventHandler = EventHandler
        m_IsAddHandler = IsAddHandler
    End Sub

    Sub Init(ByVal [Event] As Expression, ByVal EventHandler As MethodDeclaration, ByVal IsAddHandler As Boolean, ByVal InstanceExpression As Expression)
        Dim result As Boolean = True
        m_Event = [Event]
        m_IsAddHandler = IsAddHandler

        Dim o As Object = CType(CecilHelper.FindDefinition(m_Event.Classification.AsEventAccess.EventType), Mono.Cecil.TypeDefinition).Methods(0).Parameters

        Dim eventInfo As Mono.Cecil.EventReference = m_Event.Classification.AsEventAccess.EventInfo
        Dim objCreation As New DelegateOrObjectCreationExpression(Me)
        Dim methodPointer As New AddressOfExpression(Me)
        methodPointer.Init(EventHandler, InstanceExpression)
        objCreation.Init(eventInfo.EventType, New ArgumentList(objCreation, methodPointer))
        result = objCreation.ResolveExpression(ResolveInfo.Default(Compiler)) AndAlso result
        m_EventHandler = objCreation
    End Sub

    ReadOnly Property [Event]() As Expression
        Get
            Return m_Event
        End Get
    End Property

    ReadOnly Property EventHandler() As Expression
        Get
            Return m_EventHandler
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return GenerateCode(Info, m_IsAddHandler)
    End Function

    Friend Overloads Function GenerateCode(ByVal Info As EmitInfo, ByVal IsAddHandler As Boolean) As Boolean
        Dim result As Boolean = True

        Dim handler As Mono.Cecil.MethodReference

        '1 - Load the instance expression of the event (m_Event)
        '    (first argument to the add/remove method - the this pointer)
        '2 - Create the delegate to call when the event is fired.
        '    (second argument to the add/remove method).
        '2.1 - Load the instance expression of the eventhandler (m_EventHandler)
        '      (first argument to the delegate constructor).
        '2.2 - Load the method pointer of the eventhandler (m_EventHandler)
        '      (second argument to the delegate constructor).
        '2.2.1 - Load the instance expression of the eventhandler (m_EventHandler)
        '        (the argument of the load method pointer instruction)
        '2.2.2 - Call the method pointer creation instruction.
        '2.3 - Call the delegate's constructor.
        '3 - Call the add/remove method

        Helper.Assert(m_Event.Classification.IsEventAccessClassification)
        Helper.Assert(m_EventHandler.Classification.IsValueClassification)

        Dim evt As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition(m_Event.Classification.AsEventAccess.EventInfo)

        If IsAddHandler Then
            handler = evt.AddMethod
        Else
            handler = evt.RemoveMethod
        End If

        If Helper.IsShared(handler) = False Then
            result = m_Event.Classification.AsEventAccess.GenerateCode(Info) AndAlso result
        End If
        result = m_EventHandler.Classification.GenerateCode(Info.Clone(Me, True, False, m_EventHandler.ExpressionType)) AndAlso result

        Helper.Assert(handler IsNot Nothing)

        Emitter.EmitCallOrCallVirt(Info, handler)

        Return result
    End Function

    Public Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Event.ResolveExpression(Info) AndAlso result
        result = m_EventHandler.ResolveExpression(info) AndAlso result

        If result = False Then Return result

        Dim delegatetp As Mono.Cecil.TypeReference = m_Event.Classification.AsEventAccess.Type

        If m_EventHandler.Classification.IsMethodPointerClassification Then
            'result = m_EventHandler.Classification.AsMethodPointerClassification.Resolve(delegatetp) AndAlso result
            m_EventHandler = m_EventHandler.ReclassifyMethodPointerToValueExpression(delegatetp)
            result = m_EventHandler.ResolveExpression(Info) AndAlso result
            'result = Helper.VerifyValueClassification(m_EventHandler, Info) AndAlso result
        ElseIf m_EventHandler.Classification.IsValueClassification = False AndAlso m_EventHandler.Classification.CanBeValueClassification Then
            m_EventHandler = m_EventHandler.ReclassifyToValueExpression()
            result = m_EventHandler.ResolveExpression(Info) AndAlso result
        End If

        Helper.Assert(m_EventHandler.Classification.IsValueClassification)

        Compiler.Helper.AddCheck("The first argument must be an expression that is classified as an event access and the second argument must be an expression that is classified as a value. ")
        Compiler.Helper.AddCheck("The second argument's type must be the delegate type associated with the event access.")
        Return result
    End Function
End Class
