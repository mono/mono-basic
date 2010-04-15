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
''' ConversionOperatorDeclaration    ::=
'''	[  Attributes  ]  [  ConversionOperatorModifier+  ]  "Operator" "CType" "("  Operand  ")"
'''		[  "As"  [  Attributes  ]  TypeName  ]  LineTerminator
'''	[  Block  ]
'''	"End" "Operator" StatementTerminator
''' </summary>
''' <remarks></remarks>
Public Class ConversionOperatorDeclaration
    Inherits FunctionDeclaration
    
    Private m_Operator As Token
    Private m_Operand As Operand

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal [Operator] As Token, ByVal Operand As Operand, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName, ByVal Block As CodeBlock)
        Dim mySignature As New FunctionSignature(Me)
        Dim parameters As New ParameterList(Me)

        parameters.Add(New Parameter(parameters, Operand.Name, Operand.TypeName))

        If Modifiers.Is(ModifierMasks.Widening) Then
            mySignature.Init("op_Implicit", Nothing, parameters, ReturnTypeAttributes, TypeName, Me.Location)
        ElseIf Modifiers.Is(ModifierMasks.Narrowing) Then
            mySignature.Init("op_Explicit", Nothing, parameters, ReturnTypeAttributes, TypeName, Me.Location)
        Else
            Helper.AddError(Me)
        End If

        m_Operator = [Operator]
        m_Operand = Operand

        MyBase.Init(Modifiers, mySignature, Block)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = m_Operand.ResolveTypeReferences AndAlso result

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Shared Function IsOverloadableConversionOperator(ByVal token As Token) As Boolean
        Return token = KS.CType
    End Function

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.ConversionOperatorModifiers)
            i += 1
        End While
        If tm.PeekToken(i).Equals(KS.Operator) = False Then Return False
        Return IsOverloadableConversionOperator(tm.PeekToken(i + 1))
    End Function

    ReadOnly Property Operand() As Operand
        Get
            Return m_operand
        End Get
    End Property

End Class
