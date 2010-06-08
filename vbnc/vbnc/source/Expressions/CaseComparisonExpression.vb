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

Public Class CaseComparisonExpression
    Inherits Expression

    Private m_Test As Expression
    Private m_Operator As KS

    Sub New(ByVal Parent As CaseStatement, ByVal Test As Expression, ByVal [Operator] As KS)
        MyBase.New(Parent)
        m_Test = Test
        m_Operator = [Operator]
    End Sub

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
    End Function
End Class
