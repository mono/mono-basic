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

Public Class BooleanLiteralExpression
    Inherits LiteralExpression

    Private m_Value As Boolean

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Value As Boolean)
        m_Value = Value
    End Sub

    Public Overrides Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Expression
        If NewParent IsNot Nothing Then NewParent = Me.Parent
        Dim result As New BooleanLiteralExpression(NewParent)
        result.Init(m_Value)
        Return result
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get

            Return Compiler.TypeCache.System_Boolean '_Descriptor
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Return m_Value
        End Get
    End Property


#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(m_Value.ToString)
    End Sub
#End If
End Class
