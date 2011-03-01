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
''' ArrayNameModifier  ::=	ArrayTypeModifiers  |	ArraySizeInitializationModifier
''' </summary>
''' <remarks></remarks>
Public Class ArrayNameModifier
    Inherits ParsedObject

    Private m_ArrayModifier As ParsedObject

    ReadOnly Property ArrayModifier() As ParsedObject
        Get
            Return m_ArrayModifier
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_ArrayModifier.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ArrayModifier As ArrayTypeModifiers)
        m_ArrayModifier = ArrayModifier
    End Sub

    Sub Init(ByVal ArrayModifier As ArraySizeInitializationModifier)
        m_ArrayModifier = ArrayModifier
    End Sub

    Function CreateArrayType(ByVal OriginalType As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        If Me.IsArraySizeInitializationModifier Then
            Return Me.AsArraySizeInitializationModifier.CreateArrayType(OriginalType)
        ElseIf Me.IsArrayTypeModifiers Then
            Return Me.AsArrayTypeModifiers.CreateArrayType(OriginalType)
        Else
            Throw New InternalException(Me)
        End If
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return m_ArrayModifier.ResolveCode(Info)
    End Function

    ReadOnly Property IsArrayTypeModifiers() As Boolean
        Get
            Return TypeOf m_ArrayModifier Is ArrayTypeModifiers
        End Get
    End Property

    ReadOnly Property AsArrayTypeModifiers() As ArrayTypeModifiers
        Get
            Return DirectCast(m_ArrayModifier, ArrayTypeModifiers)
        End Get
    End Property

    ReadOnly Property IsArraySizeInitializationModifier() As Boolean
        Get
            Return TypeOf m_ArrayModifier Is ArraySizeInitializationModifier
        End Get
    End Property

    ReadOnly Property AsArraySizeInitializationModifier() As ArraySizeInitializationModifier
        Get
            Return DirectCast(m_ArrayModifier, ArraySizeInitializationModifier)
        End Get
    End Property

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return ArrayTypeModifier.CanBeMe(tm) OrElse ArraySizeInitializationModifier.CanBeMe(tm)
    End Function
End Class
