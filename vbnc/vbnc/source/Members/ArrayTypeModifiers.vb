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
''' ArrayTypeModifiers  ::=  ArrayTypeModifier+
''' </summary>
''' <remarks></remarks>
Public Class ArrayTypeModifiers
    Inherits ParsedObject

    Private m_ArrayTypeModifiers() As ArrayTypeModifier

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_ArrayTypeModifiers Is Nothing OrElse Helper.ResolveTypeReferencesCollection(m_ArrayTypeModifiers)
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal ArrayTypeModifiers() As ArrayTypeModifier)
        m_ArrayTypeModifiers = ArrayTypeModifiers
    End Sub

    Function CreateArrayType(ByVal OriginalType As Mono.Cecil.TypeReference) As Mono.Cecil.TypeReference
        Dim result As Mono.Cecil.TypeReference = Helper.GetTypeOrTypeReference(Compiler, OriginalType)
        Dim mods() As ArrayTypeModifier = m_ArrayTypeModifiers
        For i As Integer = mods.GetUpperBound(0) To 0 Step -1
            Dim arr As ArrayType
            arr = CecilHelper.MakeArrayType(result, mods(i).Ranks)
            result = arr
            If arr.Rank > 1 Then
                For d As Integer = 0 To arr.Rank - 1
                    arr.Dimensions(d) = New ArrayDimension(New Nullable(Of Integer)(0), Nothing)
                Next
            End If
        Next
        Return result
    End Function

    ReadOnly Property ArrayTypeModifiers() As ArrayTypeModifier()
        Get
            Return m_ArrayTypeModifiers
        End Get
    End Property

    ''' <summary>
    ''' There is no code to resolve here.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True ' Helper.ResolveCodeCollection(m_ArrayTypeModifiers)
    End Function

    Overrides Function ToString() As String
        Dim result As String = ""
        For Each atn As ArrayTypeModifier In m_ArrayTypeModifiers
            result &= atn.ToString
        Next
        Return result
    End Function

    Shared Function CanBeMe(ByVal tm As tm) As Boolean
        Return ArrayTypeModifier.CanBeMe(tm)
    End Function

End Class
