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
''' The value contains the data type's literal type character (if any),
''' and the friendly value value contains the identifier type character (if any)
''' </summary>
''' <remarks></remarks>
Public Class TypeCharacters
    Public Enum Characters
        None = -1
        ''' <summary>
        ''' %
        ''' </summary>
        ''' <remarks></remarks>
        IntegerTypeCharacter
        ''' <summary>
        ''' &amp;
        ''' </summary>
        ''' <remarks></remarks>
        LongTypeCharacter
        ''' <summary>
        ''' @
        ''' </summary>
        ''' <remarks></remarks>
        DecimalTypeCharacter
        ''' <summary>
        ''' !
        ''' </summary>
        ''' <remarks></remarks>
        SingleTypeCharacter
        ''' <summary>
        ''' #
        ''' </summary>
        ''' <remarks></remarks>
        DoubleTypeCharacter
        ''' <summary>
        ''' $
        ''' </summary>
        ''' <remarks></remarks>
        StringTypeCharacter
    End Enum
    Private Const m_Characters As String = "%&@!#$"
    Private Shared m_DataTypes() As KS = {KS.None, KS.Integer, KS.Long, KS.Decimal, KS.Single, KS.Double, KS.String}

    Shared Function TypeCharacterToType(ByVal Compiler As Compiler, ByVal TypeCharacter As TypeCharacters.Characters) As Type
        Select Case GetDataType(TypeCharacter)
            Case KS.Integer
                Return Compiler.TypeCache.Integer
            Case KS.Long
                Return Compiler.TypeCache.Long
            Case KS.Decimal
                Return Compiler.TypeCache.Decimal
            Case KS.Single
                Return Compiler.TypeCache.Single
            Case KS.Double
                Return Compiler.TypeCache.Double
            Case KS.String
                Return Compiler.TypeCache.String
            Case Else
                Throw New InternalException("Unknown typecharacter: " & TypeCharacter.ToString())
        End Select
    End Function

    Shared Function IsTypeCharacter(ByVal chr As Char, ByRef result As Characters) As Boolean
        result = GetTypeCharacter(chr)
        Return result <> Characters.None
    End Function

    Shared Function IsTypeCharacter(ByVal chr As Char) As Boolean
        Return GetTypeCharacter(chr) <> Characters.None
    End Function

    Shared Function GetTypeCharacter(ByVal chr As Char) As Characters
        Return CType(m_Characters.IndexOf(chr), Characters)
    End Function

    Shared Function GetTypeCharacter(ByVal chr As Characters) As String
        If chr <> Characters.None Then
            Return m_Characters.Chars(chr)
        Else
            Return ""
        End If
    End Function

    Shared Function GetDataType(ByVal TypeCharacter As Characters) As KS
        Return m_DataTypes(TypeCharacter + 1)
    End Function

    ''' <summary>
    ''' This class cannot be created.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
        '
    End Sub
End Class