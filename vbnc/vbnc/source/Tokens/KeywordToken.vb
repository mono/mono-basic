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

Public Class KeywordToken
    Inherits Token

    Private m_Keyword As KS

    Overloads Shared Function IsKeyword(ByVal str As String, ByRef Keyword As KS) As Boolean
        Dim special As KS
        special = Enums.GetKS(str)
        If special <> KS.None Then
            Keyword = special
            Return True
        Else
            Return False
        End If
    End Function
    ReadOnly Property Identifier() As String
        Get
            Return Enums.strSpecial(m_Keyword)
        End Get
    End Property
    ReadOnly Property Keyword() As KS
        Get
            Return m_Keyword
        End Get
    End Property
    Sub New(ByVal Range As Span, ByVal Keyword As KS, ByVal Compiler As Compiler)
        MyBase.New(Range, Compiler)
        ChangeKeyword(Keyword)
    End Sub
    Sub ChangeKeyword(ByVal toKeyword As KS)
        Helper.Assert(Enums.GetKSStringAttribute(toKeyword).IsKeyword OrElse _
            Enums.GetKSStringAttribute(toKeyword).IsMultiKeyword OrElse _
            Enums.strSpecial(toKeyword).Contains("#") = True)
        m_Keyword = toKeyword
    End Sub
    Public Overrides Function Equals(ByVal Special As KS) As Boolean
        Return m_Keyword = Special
    End Function
#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        Dumper.Write(Identifier)
    End Sub
#End If
    Public Overrides Function ToString() As String
        Return Identifier
    End Function
End Class
