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
'''SimpleExpression  ::=
'''	LiteralExpression  |
'''	ParenthesizedExpression  |
'''	InstanceExpression  |
'''	SimpleNameExpression  |
'''	AddressOfExpression
''' </summary>
''' <remarks></remarks>
Public Class SimpleExpression
    Inherits Expression

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim result As Boolean = True

        result = LiteralExpression.IsMe(tm) OrElse ParenthesizedExpression.IsMe(tm) OrElse InstanceExpression.IsMe(tm) OrElse SimpleNameExpression.IsMe(tm) OrElse AddressOfExpression.IsMe(tm)

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

#If DEBUG Then
    Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)

    End Sub
#End If

End Class
