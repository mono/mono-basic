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
''' RegularEventMemberDeclaration  ::=
''' 	[  Attributes  ]  [  EventModifiers+  ]  "Event"  Identifier  ParametersOrType  [  ImplementsClause  ] StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class RegularEventDeclaration
    Inherits EventDeclaration

    Private m_ParametersOrType As ParametersOrType

    ''' <summary>The implicitly defined delegate (if not explicitly specified).</summary>
    Private m_ImplicitEventDelegate As DelegateDeclaration
    ''' <summary>The implicitly defined field holding the delegate variable (of not a custom event).</summary>
    Private m_Variable As TypeVariableDeclaration
    Private m_ElementsCreated As Boolean

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Identifier As Identifier, ByVal ParametersOrType As ParametersOrType, ByVal ImplementsClause As MemberImplementsClause)
        MyBase.Init(Modifiers, Identifier, ImplementsClause)
        m_ParametersOrType = ParametersOrType
    End Sub

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.EventModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Event)
    End Function

    ReadOnly Property Parameters() As ParameterList
        Get
            Return m_ParametersOrType.Parameters
        End Get
    End Property

    ReadOnly Property Type() As NonArrayTypeName
        Get
            Return m_ParametersOrType.Type
        End Get
    End Property

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result
        If m_ParametersOrType IsNot Nothing Then result = m_ParametersOrType.CreateDefinition AndAlso result

        Return result
    End Function

    Public ReadOnly Property EventField() As Mono.Cecil.FieldDefinition
        Get
            Helper.Assert(m_Variable IsNot Nothing)
            Helper.Assert(m_Variable.IsFieldVariable)
            Helper.Assert(m_Variable.FieldBuilder IsNot Nothing)
            Return m_Variable.FieldBuilder
        End Get
    End Property

    Public Function CreateRegularEventMembers() As Boolean
        Dim result As Boolean = True

        'An event creates the following members.
        '1 - if the event is not an explicit delegate, and not an implemented interface event, a nested delegate in the 
        '    parent called (name)EventHandler.
        '    the parameters to the delegate are the same as for the event 
        '    accessability is the same as for the event.
        '2 - a private variable in the parent called (name)Event of type (name)EventHandler.
        '    (unless it is an interface)
        '3 - an add_(name) method in the parent with 1 parameter of type (name)EventHandler.
        '    accessability is the same as for the event.
        '4 - an remove_(name) method in the parent with 1 parameter of type (name)EventHandler.
        '    accessability is the same as for the event.
        '5 - possibly a raise_(name) method in the parent as well.
        '    accessability is the same as for the event.
        '    this method seems to be created only for custom events.
        '6 - an event in the parent called (name) with the add, remove and raise methods of 3, 4 & 5
        '    accessability is the same as for the event.

        'TODO: remove m_ElementsCreated
        Helper.Assert(m_ElementsCreated = False)
        If m_ElementsCreated Then Return result
        m_ElementsCreated = True

        Dim addMethod As RegularEventHandlerDeclaration
        Dim removeMethod As RegularEventHandlerDeclaration
        Dim type As TypeName = Nothing

        If Me.Type IsNot Nothing Then type = New TypeName(Me, Me.Type)

        'Create the delegate, if necessary.
        If ImplementsClause IsNot Nothing AndAlso ImplementsClause.ImplementsList.Count > 0 Then
            'Nothing to do here
        ElseIf Me.Parameters IsNot Nothing Then
            m_ImplicitEventDelegate = New DelegateDeclaration(DeclaringType, DeclaringType.Namespace, New SubSignature(Me, Me.Name & "EventHandler", Me.Parameters.Clone()))
            m_ImplicitEventDelegate.Modifiers = Me.Modifiers
            result = m_ImplicitEventDelegate.CreateDefinition() AndAlso result
            result = m_ImplicitEventDelegate.CreateDelegateMembers AndAlso result

            EventType = m_ImplicitEventDelegate.CecilType
            DeclaringType.Members.Add(m_ImplicitEventDelegate)
        ElseIf type IsNot Nothing Then
            'nothing to do
        Else
            Throw New InternalException(Me)
        End If

        'Create the variable.
        If DeclaringType.IsInterface = False Then
            Dim eventVariableModifiers As Modifiers
            m_Variable = New TypeVariableDeclaration(DeclaringType)
            eventVariableModifiers = New Modifiers(ModifierMasks.Private)
            If Me.IsShared Then eventVariableModifiers.AddModifiers(ModifierMasks.Shared)
            If m_ImplicitEventDelegate IsNot Nothing Then
                m_Variable.Modifiers = eventVariableModifiers
                m_Variable.Name = Me.Name & "Event"
                m_Variable.VariableType = m_ImplicitEventDelegate.CecilType
            Else
                m_Variable.Modifiers = eventVariableModifiers
                m_Variable.Name = Me.Name & "Event"
            End If
            result = m_Variable.CreateDefinition AndAlso result
            DeclaringType.Members.Add(m_Variable)
        End If

        'Create the add method
        addMethod = New RegularEventHandlerDeclaration(Me, Me.Modifiers, KS.AddHandler, Me.Identifier)
        result = addMethod.CreateDefinition AndAlso result
        DeclaringType.Members.Add(addMethod)

        'Create the remove method
        removeMethod = New RegularEventHandlerDeclaration(Me, Me.Modifiers, KS.RemoveHandler, Me.Identifier)
        result = removeMethod.CreateDefinition AndAlso result
        DeclaringType.Members.Add(removeMethod)

        Helper.Assert(addMethod IsNot Nothing)
        Helper.Assert(addMethod.Name <> "")
        Helper.Assert(removeMethod IsNot Nothing)
        Helper.Assert(removeMethod.Name <> "")

        MyBase.AddMethod = addMethod
        MyBase.RemoveMethod = removeMethod

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_ParametersOrType.ResolveTypeReferences AndAlso result

        If ImplementsClause IsNot Nothing AndAlso ImplementsClause.ImplementsList.Count > 0 Then
            Dim ism As InterfaceMemberSpecifier
            ism = ImplementsClause.ImplementsList(0)

            Helper.Assert(ImplementsClause.ImplementsList.Count = 1)
            Helper.Assert(ism IsNot Nothing)

            result = ism.ResolveEarly() AndAlso result

            If result = False Then Return result

            Helper.Assert(ism.ResolvedEventInfo IsNot Nothing)

            Dim eD As Mono.Cecil.EventDefinition = CecilHelper.FindDefinition(ism.ResolvedEventInfo)

            If eD IsNot Nothing Then
                If eD.EventType Is Nothing Then
                    Dim red As RegularEventDeclaration = TryCast(eD.Annotations(Compiler), RegularEventDeclaration)
                    If red IsNot Nothing Then
                        result = red.ResolveTypeReferences AndAlso result
                    End If
                End If
                Helper.Assert(eD.EventType IsNot Nothing)
                EventType = eD.EventType
            Else
                EventType = ism.ResolvedEventInfo.EventType
            End If
            If m_Variable IsNot Nothing AndAlso m_Variable.VariableType Is Nothing Then
                m_Variable.VariableType = EventType
            End If
        End If

        If EventType IsNot Nothing Then
            'Nothing to do
        ElseIf Type IsNot Nothing Then
            Helper.Assert(EventType Is Nothing)
            EventType = Type.ResolvedType
            If Not m_Variable Is Nothing Then
                m_Variable.VariableType = EventType
            End If
        ElseIf Parameters IsNot Nothing Then
            Helper.Assert(EventType IsNot Nothing OrElse ImplementsClause IsNot Nothing)
        Else
            Throw New InternalException(Me)
        End If

        If m_ImplicitEventDelegate IsNot Nothing Then
            result = m_ImplicitEventDelegate.ResolveTypeReferences AndAlso result
        End If

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function
End Class
