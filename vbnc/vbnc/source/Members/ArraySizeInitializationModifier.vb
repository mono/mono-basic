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
''' ArraySizeInitializationModifier  ::= "("  BoundList  ")"  [  ArrayTypeModifiers  ]
''' LAMESPEC this might be correct? REMOVED, CURRENTLY USING ^ SPEC!
''' ArraySizeInitializationModifier  ::= "("  [ BoundList]  ")"  [  ArrayTypeModifiers  ]
''' </summary>
''' <remarks></remarks>
Public Class ArraySizeInitializationModifier
    Inherits ParsedObject

    Private m_BoundList As BoundList
    Private m_ArrayTypeModifiers As ArrayTypeModifiers

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        If m_BoundList IsNot Nothing Then result = m_BoundList.ResolveTypeReferences AndAlso result
        If m_ArrayTypeModifiers IsNot Nothing Then result = m_ArrayTypeModifiers.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal BoundList As BoundList, ByVal ArrayTypeModifiers As ArrayTypeModifiers)
        m_BoundList = BoundList
        m_ArrayTypeModifiers = ArrayTypeModifiers
    End Sub

    ReadOnly Property BoundList() As BoundList
        Get
            Return m_BoundList
        End Get
    End Property

    ReadOnly Property ArrayTypeModifiers() As ArrayTypeModifiers
        Get
            Return m_ArrayTypeModifiers
        End Get
    End Property

    Function CreateArrayType(ByVal OriginalType As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.TypeReference = OriginalType

        If m_ArrayTypeModifiers IsNot Nothing Then
            result = m_ArrayTypeModifiers.CreateArrayType(result)
        End If

        If m_BoundList.Expressions.GetUpperBound(0) = 0 Then
            result = CecilHelper.MakeArrayType(result)
        Else
            Dim arr As ArrayType
            arr = CecilHelper.MakeArrayType(result, m_BoundList.Expressions.GetUpperBound(0) + 1)
            result = arr
            If arr.Rank > 1 Then
                For d As Integer = 0 To arr.Rank - 1
                    arr.Dimensions(d) = New ArrayDimension(New Nullable(Of Integer)(0), Nothing)
                Next
            End If
        End If

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        If m_BoundList IsNot Nothing Then result = m_BoundList.ResolveCode(Info) AndAlso result
        If m_ArrayTypeModifiers IsNot Nothing Then result = m_ArrayTypeModifiers.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        If tm.CurrentToken = KS.LParenthesis Then
            If tm.PeekToken.Equals(KS.Comma, KS.RParenthesis) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

End Class
