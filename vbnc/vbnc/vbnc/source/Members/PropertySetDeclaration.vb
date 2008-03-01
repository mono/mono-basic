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

    Public Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal ImplementsClause As MemberImplementsClause, ByVal Block As CodeBlock, ByVal params As ParameterList)
        Dim mySignature As SubSignature = New SubSignature(Me, "set_" & PropertySignature.Name, params)
        params = mySignature.Parameters

        If PropertySignature.TypeParameters IsNot Nothing Then
            mySignature.TypeParameters = PropertySignature.TypeParameters.Clone(mySignature)
        Else
            mySignature.TypeParameters = Nothing
        End If

        If params.Count = 0 Then
            Dim param As Parameter
            If PropertySignature.ReturnType IsNot Nothing Then
                param = New Parameter(params, "value", PropertySignature.ReturnType)
            Else
                param = New Parameter(params, "value", PropertySignature.TypeName)
            End If
            params.Add(param)
        End If

        MyBase.Init(Attributes, Modifiers, mySignature, ImplementsClause, Block)
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.AccessModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Set
    End Function

End Class