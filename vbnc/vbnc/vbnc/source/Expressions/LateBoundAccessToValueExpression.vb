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

Public Class LateBoundAccessToValueExpression
    Inherits LateBoundAccessToExpression

    Sub New(ByVal Parent As ParsedObject, ByVal LateBoundAccessClassification As LateBoundAccessClassification)
        MyBase.new(Parent, LateBoundAccessClassification)
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Me.Classification = New ValueClassification(Me)

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        If Me.LateBoundAccess.Name Is Nothing Then
            result = EmitLateIndexGet(Info) AndAlso result
        Else
            result = EmitLateGet(Info) AndAlso result
        End If


        Return result
    End Function

End Class
