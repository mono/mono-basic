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

Public Structure Modifiers
    Private m_ModifierMask As ModifierMasks

    Overloads Shared Function IsKS(ByVal KS As KS, ByVal Mask As ModifierMasks) As Boolean
        If KS > KS.WriteOnly Then Return False
        Dim modifier As ModifierMasks = KSToMask(KS)
        Return (modifier And Mask) = modifier
    End Function

    Shared Function KSToMask(ByVal Modifier As KS) As ModifierMasks
        Return CType(1 << Modifier, ModifierMasks)
    End Function

    ReadOnly Property Empty() As Boolean
        Get
            Return m_ModifierMask = 0
        End Get
    End Property

    Sub New(ByVal Mask As ModifierMasks)
        m_ModifierMask = Mask
    End Sub

    Sub New(ByVal Modifiers As Modifiers)
        m_ModifierMask = Modifiers.m_ModifierMask
    End Sub

    ReadOnly Property Mask() As ModifierMasks
        Get
            Return m_ModifierMask
        End Get
    End Property

    ''' <summary>
    ''' Adds a modifier to the list if the modifier isn't there already.
    ''' </summary>
    ''' <param name="Modifier"></param>
    ''' <remarks></remarks>
    Public Function AddModifier(ByVal Modifier As KS) As Modifiers
        m_ModifierMask = m_ModifierMask Or KSToMask(Modifier)
        Return Me
    End Function

    ''' <summary>
    ''' Adds a modifier to the list if the modifier isn't there already.
    ''' </summary>
    ''' <param name="Modifier"></param>
    ''' <remarks></remarks>
    Public Function AddModifiers(ByVal Modifier As ModifierMasks) As Modifiers
        m_ModifierMask = m_ModifierMask Or Modifier
        Return Me
    End Function

    ReadOnly Property AccessibilityMask() As ModifierMasks
        Get
            Return m_ModifierMask And (ModifierMasks.Public Or ModifierMasks.Private Or ModifierMasks.Friend Or ModifierMasks.Protected)
        End Get
    End Property

    ReadOnly Property [Is](ByVal Modifier As ModifierMasks) As Boolean
        Get
            Return (m_ModifierMask And Modifier) = Modifier
        End Get
    End Property

    ReadOnly Property IsAny(ByVal Modifier As ModifierMasks) As Boolean
        Get
            Return (m_ModifierMask And Modifier) > 0
        End Get
    End Property

    Function GetMethodAttributeScope() As Mono.Cecil.MethodAttributes
        If Me.Is(ModifierMasks.Public) Then
            Return Mono.Cecil.MethodAttributes.Public
        ElseIf Me.Is(ModifierMasks.Friend) Then
            If Me.Is(ModifierMasks.Protected) Then
                Return Mono.Cecil.MethodAttributes.FamORAssem
            Else
                Return Mono.Cecil.MethodAttributes.Assembly
            End If
        ElseIf Me.Is(ModifierMasks.Protected) Then
            Return Mono.Cecil.MethodAttributes.Family
        ElseIf Me.Is(ModifierMasks.Private) Then
            Return Mono.Cecil.MethodAttributes.Private
        Else
            Return Mono.Cecil.MethodAttributes.Public
        End If
    End Function

    Function GetFieldAttributeScope(ByVal TypeDeclaration As TypeDeclaration) As Mono.Cecil.FieldAttributes
        If Me.Is(ModifierMasks.Public) Then
            Return Mono.Cecil.FieldAttributes.Public
        ElseIf Me.Is(ModifierMasks.Friend) Then
            If Me.Is(ModifierMasks.Protected) Then
                Return Mono.Cecil.FieldAttributes.FamORAssem
            Else
                Return Mono.Cecil.FieldAttributes.Assembly
            End If
        ElseIf Me.Is(ModifierMasks.Protected) Then
            Return Mono.Cecil.FieldAttributes.Family
        ElseIf Me.Is(ModifierMasks.Private) Then
            Return Mono.Cecil.FieldAttributes.Private
        ElseIf Me.Is(ModifierMasks.Dim) OrElse Me.Is(ModifierMasks.Const) Then
            If TypeOf TypeDeclaration Is StructureDeclaration Then
                Return Mono.Cecil.FieldAttributes.Public
            Else
                Return Mono.Cecil.FieldAttributes.Private
            End If
        ElseIf TypeOf TypeDeclaration Is EnumDeclaration Then
            Return Mono.Cecil.FieldAttributes.Public
        Else
            Return Mono.Cecil.FieldAttributes.Private
        End If
    End Function

End Structure
