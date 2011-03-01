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
''' Classification: Value
''' </summary>
''' <remarks></remarks>
Public Class GetTypeExpression
    Inherits TypeExpression

    Private m_TypeName As GetTypeTypeName

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return m_TypeName.ResolveTypeReferences
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub Init(ByVal TypeName As GetTypeTypeName)
        m_TypeName = TypeName
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Emitter.EmitLoadToken(Info, m_TypeName.ResolvedType)
        Emitter.EmitCallOrCallVirt(Info, Compiler.TypeCache.System_Type__GetTypeFromHandle_RuntimeTypeHandle)

        Return result
    End Function

    Public Overrides Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        Dim attrib As Attribute = Me.FindFirstParent(Of Attribute)()
        If attrib Is Nothing Then
            If ShowError Then Show30059()
            Return False
        End If

        result = m_TypeName.ResolvedType
        Return True
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_TypeName.ResolveCode(Info) AndAlso result
        Classification = New ValueClassification(Me)

        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Return Compiler.TypeCache.System_Type
        End Get
    End Property
End Class
