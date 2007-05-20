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
Public Enum LiteralTypeCharacters_Characters As Byte
    None '= -1
    <KSEnumString("S")> ShortCharacter
    <KSEnumString("US")> UnsignedShortCharacter
    <KSEnumString("I")> IntegerCharacter
    <KSEnumString("UI")> UnsignedIntegerCharacter
    <KSEnumString("L")> LongCharacter
    <KSEnumString("UL")> UnsignedLongCharacter
    <KSEnumString("%")> IntegerTypeCharacter
    <KSEnumString("&")> LongTypeCharacter

    <KSEnumString("F")> SingleCharacter
    <KSEnumString("R")> DoubleCharacter
    <KSEnumString("D")> DecimalCharacter
    <KSEnumString("!")> SingleTypeCharacter
    <KSEnumString("#")> DoubleTypeCharacter
    <KSEnumString("@")> DecimalTypeCharacter

End Enum

Public Class LiteralTypeCharacters
    Private Shared m_Characters() As String = {"S", "US", "I", "UI", "L", "UL", "%", "&", "F", "R", "D", "!", "#", "@"}
    Private Shared m_Types() As BuiltInDataTypes = {BuiltInDataTypes.Short, BuiltInDataTypes.UShort, BuiltInDataTypes.Integer, BuiltInDataTypes.UInteger, BuiltInDataTypes.Long, BuiltInDataTypes.ULong, BuiltInDataTypes.Integer, BuiltInDataTypes.Long, BuiltInDataTypes.Single, BuiltInDataTypes.Double, BuiltInDataTypes.Decimal, BuiltInDataTypes.Single, BuiltInDataTypes.Double, BuiltInDataTypes.Decimal}

    Shared Function IsIntegral(ByVal chr As LiteralTypeCharacters_Characters) As Boolean
        Return chr >= LiteralTypeCharacters_Characters.ShortCharacter AndAlso chr <= LiteralTypeCharacters_Characters.LongTypeCharacter
    End Function

    Private Sub New()
        'This class cannot be created.
    End Sub

    Shared Function GetBuiltInType(ByVal chr As LiteralTypeCharacters_Characters) As BuiltInDataTypes
        If chr = LiteralTypeCharacters_Characters.None Then
            Throw New InternalException("")
        Else
            Return m_Types(chr - 1)
        End If
    End Function

    Shared Function GetTypeCharacter(ByVal chr As String) As LiteralTypeCharacters_Characters
        chr = chr.ToUpperInvariant
        For i As Integer = 0 To m_Characters.GetUpperBound(0)
            If m_Characters(i).Equals(chr, StringComparison.Ordinal) Then Return CType(i + 1, LiteralTypeCharacters_Characters)
        Next
        Return LiteralTypeCharacters_Characters.None
    End Function

    Shared Function GetTypeCharacter(ByVal chr As LiteralTypeCharacters_Characters) As String
        If chr = LiteralTypeCharacters_Characters.None Then
            Return ""
        Else
            Return m_Characters(chr - 1)
        End If
    End Function
End Class
