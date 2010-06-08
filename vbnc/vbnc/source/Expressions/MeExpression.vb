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
Public Class MeExpression
    Inherits InstanceExpression

    Public Overrides ReadOnly Property AsString() As String
        Get
            Return "Me"
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Emitter.EmitLoadMe(Info, Me.FindFirstParent(Of IType).CecilType)
        If CecilHelper.IsValueType(Info.DesiredType) AndAlso CecilHelper.IsByRef(Info.DesiredType) = False Then
            Emitter.EmitLoadObject(Info, Info.DesiredType)
        End If
        Return True
    End Function
End Class
