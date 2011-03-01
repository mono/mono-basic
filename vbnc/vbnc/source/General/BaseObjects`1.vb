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
''' A list of BaseObjects
''' </summary>
''' <remarks></remarks>
Public Class BaseObjects(Of T)
    Inherits Generic.List(Of T)

    Private m_Parent As BaseObject

    ''' <summary>
    ''' Get the compiler compiling right now.
    ''' </summary>
    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    ''' <summary>
    ''' The parent of this list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable ReadOnly Property Parent() As BaseObject
        Get
            Return m_Parent
        End Get
    End Property

    ''' <summary>
    ''' Creates a new baseobject collection for the specified parent.
    ''' </summary>
    Sub New(ByVal Parent As BaseObject)
        MyBase.New()
        m_Parent = Parent
    End Sub

    Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True
        For i As Integer = 0 To Count - 1
            Dim pO As BaseObject = TryCast(CObj(Item(i)), BaseObject)
            If pO IsNot Nothing Then
                result = pO.ResolveCode(Info) AndAlso result
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' Calls GenerateCode on all the types in this collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return Helper.GenerateCodeCollection(Me, Info)
    End Function

    Function ResolveTypeReferences() As Boolean
        Return Helper.ResolveTypeReferencesCollection(Me)
    End Function
End Class
