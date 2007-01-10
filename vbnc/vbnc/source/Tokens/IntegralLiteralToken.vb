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
#Const EXTENDED = False

Public Class IntegralLiteralToken(Of Type)
    Inherits LiteralTokenBase(Of Type)
    'Implements IIntegralLiteralToken

    Private m_Base As IntegerBase

    ReadOnly Property Base() As IntegerBase 'Implements IIntegralLiteralToken.Base
        Get
            Return m_Base
        End Get
    End Property

    Sub New(ByVal Range As Span, ByVal Literal As Type, ByVal Base As IntegerBase, ByVal LiteralTypeCharacter As LiteralTypeCharacters_Characters, ByVal Compiler As Compiler)
        MyBase.New(Range, TypeResolution.TypeCodeToBuiltInType(Helper.GetTypeCode(Literal.GetType)), Compiler, LiteralTypeCharacter, Literal)
        m_Base = Base
    End Sub

    Public Overrides Function ToString() As String
        Select Case m_Base
#If EXTENDED Then
            Case IntegerBase.Binary
                Return "&B" & Helper.IntToBin(CULng(Literal.ToString))
#End If
            Case IntegerBase.Decimal
                Return Literal.ToString
            Case IntegerBase.Hex
                Return "&H" & Helper.IntToHex(CULng(Literal.ToString))
            Case IntegerBase.Octal
                Return "&O" & Helper.IntToOct(CULng(Literal.ToString))
            Case Else
                Throw New InternalException(Me)
        End Select
    End Function

    Public ReadOnly Property IntegralLiteral() As ULong 'Implements IIntegralLiteralToken.IntegralLiteral
        Get
            Return CULng(Literal.ToString)
        End Get
    End Property
End Class
