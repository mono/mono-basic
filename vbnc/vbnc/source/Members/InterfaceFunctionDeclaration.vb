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
''' InterfaceFunctionDeclaration  ::=
'''	[  Attributes  ]  [  InterfaceProcedureModifier+  ] "Function" FunctionSignature  StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class InterfaceFunctionDeclaration
    Inherits FunctionDeclaration
    
    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As FunctionSignature)
        MyBase.Init(Modifiers, Signature, Nothing)
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.InterfaceProcedureModifiers)
            i += 1
        End While
        Return tm.PeekToken(i).Equals(KS.Function)
    End Function
End Class
