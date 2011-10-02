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

Public MustInherit Class Statement
    Inherits ParsedObject

    Overridable ReadOnly Property IsOneLiner() As Boolean
        Get
            If TypeOf Me.Parent Is CodeBlock Then
                Return DirectCast(Me.Parent, CodeBlock).IsOneLiner
            ElseIf TypeOf Me.Parent Is Statement Then
                Return DirectCast(Me.Parent, Statement).IsOneLiner
            Else
                Throw New InternalException(Me)
            End If
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, Location As Span)
        MyBase.New(Parent, Location)
    End Sub

    ReadOnly Property FindParentCodeBlock() As CodeBlock
        Get
            Return MyBase.FindFirstParent(Of CodeBlock)()
        End Get
    End Property

    MustOverride Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean

    <Obsolete("Call ResolveStatement"), ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotOverridable Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return ResolveStatement(Info)
    End Function
End Class
