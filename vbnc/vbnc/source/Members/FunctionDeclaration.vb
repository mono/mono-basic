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
''' FunctionDeclaration  ::=
'''	[  Attributes  ]  [  ProcedureModifier+  ]  "Function" FunctionSignature  [  HandlesOrImplements  ]
'''		LineTerminator
'''	Block
'''	"End" "Function" StatementTerminator
''' 
''' MustOverrideFunctionDeclaration  ::=
'''	[  Attributes  ]  [  MustOverrideProcedureModifier+  ]  "Function" FunctionSignature
'''		[  HandlesOrImplements  ]  StatementTerminator
'''
''' </summary>
''' <remarks></remarks>
Public Class FunctionDeclaration
    Inherits SubDeclaration

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As TypeDeclaration, ByVal Name As String, ByVal MethodAttributes As Mono.Cecil.MethodAttributes, ByVal ParameterTypes As Mono.Cecil.TypeReference(), ByVal ReturnType As Mono.Cecil.TypeReference, ByVal Location As Span)
        MyBase.New(Parent)
        MyBase.Init(New Modifiers(), New FunctionSignature(Me, Name, New ParameterList(Me, ParameterTypes), ReturnType, Location), CType(Nothing, MemberImplementsClause), Nothing)
        MyBase.MethodAttributes = MethodAttributes
    End Sub

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.MustOverrideProcedureModifiers)
            i += 1
        End While
        Return tm.PeekToken(i) = KS.Function
    End Function

    Shadows ReadOnly Property Signature() As FunctionSignature
        Get
            Return DirectCast(MyBase.Signature, FunctionSignature)
        End Get
    End Property

    Public Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = Signature.VerifyParameterNamesDoesntMatchFunctionName() AndAlso result
        result = MyBase.ResolveMember(Info) AndAlso result

        Return result
    End Function
End Class
