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

Public Class DecimalLiteralToken
    Inherits LiteralTokenBase(Of Decimal)

    Sub New(ByVal Range As Span, ByVal Literal As Decimal, ByVal Compiler As Compiler, ByVal TypeCharacter As LiteralTypeCharacters_Characters)
        MyBase.New(Range, BuiltInDataTypes.Decimal, Compiler, TypeCharacter, Literal)
    End Sub

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(Me.ToString & "D")
    End Sub
#End If
    Public Overrides Function ToString() As String
        Return Literal.ToString
    End Function
End Class
