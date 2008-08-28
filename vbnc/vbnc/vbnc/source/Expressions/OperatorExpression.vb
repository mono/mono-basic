' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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
'''OperatorExpression  ::=
'''	ArithmeticOperatorExpression  |
'''	RelationalOperatorExpression  |
'''	LikeOperatorExpression  |
'''	ConcatenationOperatorExpression  |
'''	ShortCircuitLogicalOperatorExpression  |
'''	LogicalOperatorExpression  |
'''	ShiftOperatorExpression
''' </summary>
''' <remarks></remarks>
Public MustInherit Class OperatorExpression
    Inherits Expression

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Shared Function IsUnaryMe(ByVal tm As tm) As Boolean
        Dim result As Boolean = True

        Return tm.Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)

        Return result
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Dim result As Boolean = True

        Return tm.Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)

        Return result
    End Function

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub
End Class
