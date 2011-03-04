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

Public Class BuiltInTypeName
    Inherits ParsedObject

    Private m_TypeName As KS

    ReadOnly Property TypeName() As KS
        Get
            Return m_TypeName
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal TypeName As KS)
        MyBase.neW(Parent)
        m_TypeName = Typename
    End Sub

    ReadOnly Property ResolvedType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeResolution.KeywordToType(m_TypeName)
        End Get
    End Property

    ''' <summary>
    ''' No type references to resolve here.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ResolveTypeReferences() As Boolean
        Return True
    End Function

    ''' <summary>
    ''' No code to resolve here.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return True
    End Function

    ReadOnly Property Name() As String
        Get
            Return m_Typename.ToString
        End Get
    End Property

    Shared Function IsBuiltInTypeName(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(Enums.BuiltInTypeTypeNames)
    End Function
End Class
