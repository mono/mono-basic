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
''' ExternalFunctionDeclaration  ::=
'''	[  Attributes  ]  [  ExternalMethodModifier+  ]  "Declare" [  CharsetModifier  ] "Function" Identifier
'''		LibraryClause  [  AliasClause  ]  [  (  [  ParameterList  ]  )  ]  [  As  [  Attributes  ]  TypeName  ]
'''		StatementTerminator
''' 
''' CharsetModifier  ::=  "Ansi" | "Unicode" |  "Auto"
''' </summary>
''' <remarks></remarks>
Public Class ExternalFunctionDeclaration
    Inherits ExternalSubDeclaration

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal CharsetModifier As KS, ByVal Identifier As Identifier, ByVal LibraryClause As LibraryClause, ByVal AliasClause As AliasClause, ByVal ParameterList As ParameterList, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName)

        Dim mySignature As New FunctionSignature(Me)
        mySignature.Init(Identifier, Nothing, ParameterList, ReturnTypeAttributes, TypeName, Me.Location)

        MyBase.Init(Attributes, Modifiers, CharsetModifier, LibraryClause, AliasClause, mySignature)
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ExternalMethodModifiers)
            i += 1
        End While
        If tm.PeekToken(i) <> KS.Declare Then Return False
        If tm.PeekToken(i + 1).Equals(ModifierMasks.CharSetModifiers) Then i += 1
        Return tm.PeekToken(i + 1) = KS.Function
    End Function

    Shadows ReadOnly Property Signature() As FunctionSignature
        Get
            Return DirectCast(MyBase.Signature, FunctionSignature)
        End Get
    End Property
End Class
