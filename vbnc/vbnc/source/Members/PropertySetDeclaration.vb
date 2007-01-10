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

Imports System.Reflection
Imports System.Reflection.Emit

''' <summary>
''' PropertySetDeclaration  ::=
'''	[  Attributes  ]  [  AccessModifier  ]  "Set" [  (  ParameterList  )  ]  LineTerminator
'''	[  Block  ]
'''	"End" "Set" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class PropertySetDeclaration
    Inherits PropertyHandlerDeclaration

    Public Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.New(Parent)
    End Sub

    Public Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal PropertySignature As FunctionSignature, ByVal ImplementsClause As MemberImplementsClause, ByVal Block As CodeBlock)
        Dim mySignature As SubSignature
        Dim name As String
        Dim typeParams As TypeParameters
        Dim params As ParameterList

        mySignature = New SubSignature(Me)

        If PropertySignature.TypeParameters IsNot Nothing Then
            typeParams = PropertySignature.TypeParameters.Clone(mySignature)
        Else
            typeParams = Nothing
        End If
        If PropertySignature.Parameters IsNot Nothing Then
            params = PropertySignature.Parameters.Clone(mySignature)
        Else
            params = New ParameterList(mySignature)
        End If
        name = "set_" & PropertySignature.Name

        mySignature.Init(name, typeParams, params)

        mySignature.Parameters.Add(New Parameter(mySignature.Parameters, "value", PropertySignature.TypeName))

        MyBase.Init(Attributes, Modifiers, mySignature, ImplementsClause, Block)
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(Enums.AccessModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Set
    End Function

End Class