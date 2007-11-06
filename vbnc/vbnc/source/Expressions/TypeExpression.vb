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
''' A "GetType" expression, a "TypeOf ... Is" expression, or an "Is" expression.
''' 
''' TypeExpression  ::=
'''	  GetTypeExpression  |
'''	  TypeOfIsExpression  |
'''	  IsExpression
''' </summary>
''' <remarks></remarks>
Public Class TypeExpression
    Inherits Expression

    Shared Function IsUnaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.GetType, KS.TypeOf, KS.Is)
    End Function

    Shared Function IsBinaryMe(ByVal tm As tm) As Boolean
        Return tm.CurrentToken.Equals(KS.Is)
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean
        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(Me.GetType.ToString)
    End Sub
#End If

    Protected Overrides Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)
    End Function
End Class
