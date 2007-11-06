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

Public Class IdentifierOrKeywordWithTypeArguments
    Inherits IdentifierOrKeyword

    Private m_TypeArguments As TypeArgumentList

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result
        If m_TypeArguments IsNot Nothing Then result = m_TypeArguments.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Token As Token, ByVal TypeArguments As TypeArgumentList)
        MyBase.Init(Token)
        m_TypeArguments = TypeArguments
    End Sub

    Shadows Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As IdentifierOrKeywordWithTypeArguments
        If NewParent Is Nothing Then NewParent = DirectCast(Me.Parent, ParsedObject)
        Dim result As New IdentifierOrKeywordWithTypeArguments(NewParent)
        If m_TypeArguments IsNot Nothing Then
            result.Init(MyBase.Token, m_TypeArguments.Clone(result))
        Else
            result.Init(MyBase.Token, Nothing)
        End If
        Return result
    End Function

    Shared Function CreateAndParseTo(ByRef result As Expression) As Boolean

        Return result.Compiler.Report.ShowMessage(Messages.VBNC99997, result.Location)

    End Function

    Shared Function IsMe(ByVal tm As tm) As Boolean
        Dim result As Boolean = True

        Return tm.Compiler.Report.ShowMessage(Messages.VBNC99997, tm.CurrentLocation)

        Return result
    End Function

    Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Location)

        Return result
    End Function

    ReadOnly Property TypeArguments() As TypeArgumentList
        Get
            Return m_TypeArguments
        End Get
    End Property
End Class
