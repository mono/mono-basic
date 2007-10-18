' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_TypeName.ResolveCode(info) AndAlso result
        Classification = New ValueClassification(Me)

        Return result
    End Function

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Return m_TypeName.ResolvedType
        End Get
    End Property


    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Dim attrib As Attribute = Me.FindFirstParent(Of Attribute)()
            Return attrib IsNot Nothing
        End Get
    End Property

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Compiler.TypeCache.System_Type
        End Get
    End Property


#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write("GetType(")
        Compiler.Dumper.Dump(m_TypeName)
        Dumper.Write(")")
    End Sub
#End If
End Class
