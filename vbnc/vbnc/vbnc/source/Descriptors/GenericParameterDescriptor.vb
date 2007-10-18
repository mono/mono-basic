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
Public Class GenericParameterDescriptor
    Inherits ParameterDescriptor

    Private m_Info As ParameterInfo

    Sub New(ByVal Parent As ParsedObject, ByVal ParameterType As Type, ByVal Info As ParameterInfo)
        MyBase.New(ParameterType, Info.Position, Parent, Info.Name)
        m_Info = Info
    End Sub

    Public Overrides ReadOnly Property IsParamArray() As Boolean
        Get
            Return Helper.IsParamArrayParameter(Compiler, m_Info)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultValue() As Object
        Get
            Return m_Info.DefaultValue
        End Get
    End Property
End Class
