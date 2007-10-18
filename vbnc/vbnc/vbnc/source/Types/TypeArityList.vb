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
''' TypeArityList  ::=
'''	    ,  |
'''	    TypeParameterList  ,
'''
''' </summary>
''' <remarks></remarks>
Public Class TypeArityList
    Inherits ParsedObject

    Private m_TypeParameters As TypeParameters()

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(parent)
    End Sub

    ReadOnly Property AsTypeArray() As Type()
        Get
            Dim t As Type = GetType(System.Collections.Generic.Dictionary(Of ,))
            Dim result(m_TypeParameters.Length - 1) As Type
            For i As Integer = 0 To result.Length - 1
                'result(i) = m_TypeParameters(i).Parameters
            Next
            Return result
        End Get
    End Property

    Sub Init(ByVal TypeParameters() As TypeParameters)
        m_TypeParameters = TypeParameters
    End Sub

    ReadOnly Property TypeParameters() As TypeParameters()
        Get
            Return m_TypeParameters
        End Get
    End Property

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To m_TypeParameters.Length - 1
            If m_TypeParameters(i) IsNot Nothing Then
                result = m_TypeParameters(i).ResolveTypeReferences() AndAlso result
            End If
        Next

        Return result
    End Function
End Class
