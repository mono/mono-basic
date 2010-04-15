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
''' An expression with this classification can only appear as the left side of a member access. 
''' In any other context, an expression classified as a namespace causes a compile-time error.
''' </summary>
''' <remarks></remarks>
Public Class NamespaceClassification
    Inherits ExpressionClassification

    Private m_Namespace As [Namespace]

    Shadows ReadOnly Property [Namespace]() As [Namespace]
        Get
            Return m_Namespace
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal [Namespace] As [Namespace])
        MyBase.New(Classifications.Namespace, Parent)
        m_Namespace = [Namespace]
    End Sub
End Class
