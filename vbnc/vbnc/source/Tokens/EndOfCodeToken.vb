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
''' Token representing the end of code. Inherits EndOfFile token, so that 
''' every time you check for end of file (or end of line), a end of code will also be true.
''' </summary>
''' <remarks></remarks>
Public Class EndOfCodeToken
    Inherits EndOfFileToken

    Sub New(ByVal Compiler As Compiler)
        MyBase.New(New Span(), Compiler) 'No location for the end of the code...
    End Sub

    Public Overrides Function ToString() As String
        Return Constants.ENDOFCODE 'Enums.strSpecialFriendly(KS.CodeEnd)
    End Function
End Class
