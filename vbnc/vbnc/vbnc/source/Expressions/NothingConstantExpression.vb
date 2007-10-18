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

Public Class NothingConstantExpression
    Inherits ConstantExpression

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        Me.CheckTypeReferencesNotResolved()

        Return result
    End Function

    Protected Overrides Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.DesiredType IsNot Nothing)
        Emitter.EmitLoadNull(Info)

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent, Nothing, Nothing)
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Classification = New ValueClassification(Me, Nothing, System.DBNull.Value)
        Return True
    End Function

    Overrides ReadOnly Property ExpressionType() As Type
        Get
            Return Compiler.TypeCache.Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property ConstantValue() As Object
        Get
            Return System.DBNull.Value
        End Get
    End Property

    Public Overrides Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As Expression
        If NewParent IsNot Nothing Then NewParent = Me.Parent
        Return New NothingConstantExpression(NewParent)
    End Function
End Class
