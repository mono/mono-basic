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
Imports System.Reflection.Emit
''' <summary>
''' DelegateDeclaration  ::=
''' [  Attributes  ]  [  TypeModifier+  ]  "Delegate" MethodSignature  StatementTerminator
''' MethodSignature  ::=  SubSignature  |  FunctionSignature
''' 
''' LAMESPEC: should be something like:
''' [  Attributes  ]  [  TypeModifier+  ]  "Delegate" FunctionOrSub MethodSignature  StatementTerminator
''' FunctionOrSub ::= "Function" | "Sub"
''' </summary>
''' <remarks></remarks>
Public Class DelegateDeclaration
    Inherits GenericTypeDeclaration

    Public Const STR_Invoke As String = "Invoke"
    Public Const STR_EndInvoke As String = "EndInvoke"
    Public Const STR_BeginInvoke As String = "BeginInvoke"

    Private m_Signature As SubSignature
    Private m_ImplicitElementsCreated As Boolean

    Private m_Constructor As ConstructorDeclaration
    Private m_Invoke As SubDeclaration
    Private m_BeginInvoke As FunctionDeclaration
    Private m_EndInvoke As SubDeclaration

    Public Sub New(ByVal Parent As TypeDeclaration, ByVal Signature As SubSignature)
        MyBase.New(Parent, Parent.Namespace, Signature.Identifier, Signature.TypeParameters)
        m_Signature = Signature
    End Sub

    Public Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As String, ByVal Signature As SubSignature)
        MyBase.New(Parent, [Namespace], Helper.CreateGenericTypename(Signature.Identifier, Signature.TypeParameters), Signature.TypeParameters)
        m_Signature = Signature
    End Sub

    Function CreateDelegateMembers() As Boolean
        Dim result As Boolean = True
        Dim ReturnType As TypeName
        Dim Parameters As ParameterList = m_Signature.Parameters

        If m_ImplicitElementsCreated Then Return True
        m_ImplicitElementsCreated = True

        Helper.Assert(Me.Members.Count = 0)

        If TypeOf m_Signature Is FunctionSignature Then
            ReturnType = DirectCast(m_Signature, FunctionSignature).TypeName
            If ReturnType Is Nothing Then ReturnType = New TypeName(Me, Compiler.TypeCache.System_Object)
        Else
            ReturnType = Nothing
        End If

        'Create the constructor
        m_Constructor = New ConstructorDeclaration(Me)
        m_Constructor.Init(Nothing)
        m_Constructor.Signature.Parameters.Add("TargetObject", Compiler.TypeCache.System_Object)
        m_Constructor.Signature.Parameters.Add("TargetMethod", Compiler.TypeCache.System_IntPtr)
        result = m_Constructor.CreateDefinition() AndAlso result

        m_Constructor.MethodAttributes = Mono.Cecil.MethodAttributes.Public Or Mono.Cecil.MethodAttributes.SpecialName Or Mono.Cecil.MethodAttributes.RTSpecialName
        m_Constructor.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.Runtime

        Members.Add(m_Constructor)

        Dim paramCount As Integer
        If Parameters IsNot Nothing Then paramCount = Parameters.Count

        Dim invokeParameters As ParameterList
        Dim beginInvokeParameters As ParameterList
        Dim endInvokeParameters As ParameterList

        Dim invokeSignature As SubSignature
        Dim beginInvokeSignature As FunctionSignature
        Dim endInvokeSignature As SubSignature

        'Invoke takes same types as delegate, and same return type
        'BeginInvoke takes same types as delegate + AsyncCallback + Object, and return type is IAsyncResult
        'EndInvoke takes byref types of delegate + IAsyncResult, and same return type

        If ReturnType Is Nothing Then
            m_Invoke = New SubDeclaration(Me)
            m_EndInvoke = New SubDeclaration(Me)
        Else
            m_Invoke = New FunctionDeclaration(Me)
            m_EndInvoke = New FunctionDeclaration(Me)
        End If
        m_BeginInvoke = New FunctionDeclaration(Me)

        invokeParameters = New ParameterList(m_Invoke)
        beginInvokeParameters = New ParameterList(m_BeginInvoke)
        endInvokeParameters = New ParameterList(m_EndInvoke)

        For i As Integer = 0 To paramCount - 1
            invokeParameters.Add(Parameters(i).Clone(invokeParameters))
            beginInvokeParameters.Add(Parameters(i).Clone(beginInvokeParameters))
            If Parameters(i).Modifiers.Is(ModifierMasks.ByRef) Then
                endInvokeParameters.Add(Parameters(i).Clone(endInvokeParameters))
            End If
        Next
        beginInvokeParameters.Add(New Parameter(beginInvokeParameters, "DelegateCallback", Compiler.TypeCache.System_AsyncCallback))
        beginInvokeParameters.Add(New Parameter(beginInvokeParameters, "DelegateAsyncState", Compiler.TypeCache.System_Object))
        endInvokeParameters.Add(New Parameter(endInvokeParameters, "DelegateAsyncResult", Compiler.TypeCache.System_IAsyncResult))

        If ReturnType Is Nothing Then
            invokeSignature = New SubSignature(m_Invoke, STR_Invoke, invokeParameters)
            endInvokeSignature = New SubSignature(m_EndInvoke, STR_EndInvoke, endInvokeParameters)
        Else
            invokeSignature = New FunctionSignature(m_Invoke)
            DirectCast(invokeSignature, FunctionSignature).Init(STR_Invoke, Nothing, invokeParameters, Nothing, ReturnType, Me.Location)
            endInvokeSignature = New FunctionSignature(m_EndInvoke)
            DirectCast(endInvokeSignature, FunctionSignature).Init(STR_EndInvoke, Nothing, endInvokeParameters, Nothing, ReturnType, Me.Location)
        End If
        beginInvokeSignature = New FunctionSignature(m_BeginInvoke, STR_BeginInvoke, beginInvokeParameters, Compiler.TypeCache.System_IAsyncResult, Me.Location)

        m_Invoke.Init(New Modifiers(), invokeSignature, Nothing, Nothing)
        result = m_Invoke.CreateDefinition AndAlso result
        m_BeginInvoke.Init(New Modifiers(), beginInvokeSignature, Nothing, Nothing)
        result = m_BeginInvoke.CreateDefinition AndAlso result
        m_EndInvoke.Init(New Modifiers(), endInvokeSignature, Nothing, Nothing)
        result = m_EndInvoke.CreateDefinition AndAlso result

        Dim attr As Mono.Cecil.MethodAttributes
        Dim implattr As Mono.Cecil.MethodImplAttributes = Mono.Cecil.MethodImplAttributes.Runtime
        attr = Mono.Cecil.MethodAttributes.Public Or Mono.Cecil.MethodAttributes.NewSlot Or Mono.Cecil.MethodAttributes.Virtual Or Mono.Cecil.MethodAttributes.CheckAccessOnOverride

        'If Me.DeclaringType IsNot Nothing AndAlso Me.DeclaringType.TypeDescriptor.IsInterface Then
        '    attr = attr Or MethodAttributes.CheckAccessOnOverride
        'End If

        m_Invoke.MethodAttributes = attr
        m_BeginInvoke.MethodAttributes = attr
        m_EndInvoke.MethodAttributes = attr

        m_Invoke.MethodImplAttributes = implattr
        m_BeginInvoke.MethodImplAttributes = implattr
        m_EndInvoke.MethodImplAttributes = implattr

        Members.Add(m_BeginInvoke)
        Members.Add(m_EndInvoke)
        Members.Add(m_Invoke)

        Return result
    End Function

    Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        BaseType = Compiler.TypeCache.System_MulticastDelegate

        result = MyBase.ResolveTypeReferences AndAlso result

        result = m_Signature.ResolveTypeReferences(False) AndAlso result

        Return result
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.TypeModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Delegate)
    End Function

    ReadOnly Property Signature() As SubSignature
        Get
            Return m_Signature
        End Get
    End Property

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        TypeAttributes = Helper.getTypeAttributeScopeFromScope(Modifiers, IsNestedType) Or Mono.Cecil.TypeAttributes.Sealed
        If m_Signature IsNot Nothing Then result = m_Signature.CreateDefinition AndAlso result

        Return result
    End Function
End Class
