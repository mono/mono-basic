' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
''' PropertyGetDeclaration  ::=
'''	[  Attributes  ]  [  AccessModifier  ]  Get  LineTerminator
'''	[  Block  ]
'''	End  Get  StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class PropertyGetDeclaration
    Inherits PropertyHandlerDeclaration

    Public Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.New(Parent)
    End Sub

    Public Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal ImplementsClause As MemberImplementsClause, ByVal Block As CodeBlock)
        Dim mySignature As FunctionSignature

        mySignature = New FunctionSignature(Me)

        Dim typeParams As TypeParameters
        Dim retTypeAttributes As Attributes
        Dim name As String
        Dim params As ParameterList
        Dim typename As TypeName

        typeParams = PropertySignature.TypeParameters
        If PropertySignature.ReturnTypeAttributes IsNot Nothing Then
            retTypeAttributes = PropertySignature.ReturnTypeAttributes.Clone(mySignature)
        Else
            retTypeAttributes = Nothing
        End If
        If PropertySignature.Parameters IsNot Nothing Then
            params = PropertySignature.Parameters.Clone(Me)
        Else
            params = Nothing
        End If
        If PropertySignature.TypeName IsNot Nothing Then
            typename = PropertySignature.TypeName
        ElseIf PropertySignature.ReturnType IsNot Nothing Then
            typename = New TypeName(mySignature, PropertySignature.ReturnType)
        Else
            typename = Nothing
        End If
        name = "get_" & PropertySignature.Name

        mySignature.Init(New Identifier(mySignature, name, PropertySignature.Location, PropertySignature.Identifier.TypeCharacter), typeParams, params, retTypeAttributes, typename, PropertySignature.Location)

        MyBase.Init(Modifiers, mySignature, ImplementsClause, Block)
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.AccessModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Get
    End Function

End Class
