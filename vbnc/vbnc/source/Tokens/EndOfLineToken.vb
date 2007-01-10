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

Public Class EndOfLineToken
    Inherits Token

    ''' <summary>
    ''' Create a new end of line token. 
    ''' </summary>
    ''' <param name="Range"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Range As Span, ByVal Compiler As Compiler)
        MyBase.new(Range, Compiler)
    End Sub
#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.WriteLine(VB.vbNewLine)
    End Sub
#End If
    Public Overrides Function ToString() As String
        Return Constants.NEWLINE
    End Function
End Class
