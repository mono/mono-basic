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

Public MustInherit Class CompoundAssignmentStatement
    Inherits AssignmentStatement

    Private m_CompoundExpression As Expression

    Protected MustOverride Overloads Function ResolveStatement(ByVal LSide As Expression, ByVal RSide As Expression) As Expression

    Sub New(ByVal Parent As ParsedObject)
        MyBase.new(Parent)
    End Sub

    Public NotOverridable Overrides Function ResolveStatement(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveStatement(Info) AndAlso result
        m_CompoundExpression = ResolveStatement(LSide, RSide)

        result = m_CompoundExpression.ResolveExpression(info) AndAlso result

        m_CompoundExpression = Helper.CreateTypeConversion(Me, m_CompoundExpression, LSide.ExpressionType, result)

        Return result
    End Function

    Friend NotOverridable Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        'result = m_CompoundExpression.GenerateCode(Info.Clone(True, False, LSide.ExpressionType)) AndAlso result

        Dim lInfo As EmitInfo = Info.Clone(m_CompoundExpression)
        result = LSide.GenerateCode(lInfo) AndAlso result

        Return result
    End Function

    Public Overrides Function CreateTypeConversion() As Boolean
        Dim result As Boolean = True

        Return result
    End Function
End Class
