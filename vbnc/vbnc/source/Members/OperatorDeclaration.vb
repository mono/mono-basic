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
''' BinaryOperatorDeclaration  ::=
'''	[  Attributes  ]  [  OperatorModifier+  ]  "Operator"  OverloadableBinaryOperator
'''		"("  Operand  ","  Operand  ")"  [ "As"  [  Attributes  ]  TypeName  ]  LineTerminator
'''	[  Block  ]
'''	"End" "Operator" StatementTerminator
''' 
''' UnaryOperatorDeclaration  ::=
'''	[  Attributes  ]  [  OperatorModifier+  ]  "Operator" OverloadableUnaryOperator 
'''     "("  Operand  ")" 		[  "As" [  Attributes  ]  TypeName  ]  LineTerminator
'''	[  Block  ]
'''	"End" "Operator" StatementTerminator
''' OverloadableUnaryOperator  ::=  "+"  | "-"  |  "Not"  |  "IsTrue"  |  "IsFalse"
''' </summary>
''' <remarks></remarks>
Public Class OperatorDeclaration
    Inherits FunctionDeclaration

    'Private m_Operator As Token
    Private m_Operand1 As Operand
    Private m_Operand2 As Operand

    Sub New(ByVal Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Attributes"></param>
    ''' <param name="Modifiers"></param>
    ''' <param name="Identifier">non-null if set</param>
    ''' <param name="Symbol">None if not set</param>
    ''' <param name="Operand1"></param>
    ''' <param name="Operand2"></param>
    ''' <param name="ReturnTypeAttributes"></param>
    ''' <param name="TypeName"></param>
    ''' <param name="Block"></param>
    ''' <remarks></remarks>
    Shadows Sub Init(ByVal Attributes As Attributes, ByVal Modifiers As Modifiers, ByVal Identifier As String, ByVal Symbol As KS, ByVal Operand1 As Operand, ByVal Operand2 As Operand, ByVal ReturnTypeAttributes As Attributes, ByVal TypeName As TypeName, ByVal Block As CodeBlock)

        Helper.Assert(Identifier Is Nothing Xor Symbol = KS.None)

        Dim mySignature As New FunctionSignature(Me)
        Dim parameters As New ParameterList(mySignature)
        Dim name As String

        parameters.Add(New Parameter(parameters, Operand1.Name, Operand1.TypeName))
        If Operand2 IsNot Nothing Then
            parameters.Add(New Parameter(parameters, Operand2.Name, Operand2.TypeName))
        End If


        If Identifier IsNot Nothing Then
            Dim opname As String
            opname = Identifier
            If Helper.CompareName(opname, "IsTrue") Then
                name = "op_True"
            ElseIf Helper.CompareName(opname, "IsFalse") Then
                name = "op_False"
            Else
                Throw New InternalException(Me)
            End If
        Else
            Select Case Symbol
                Case KS.Add
                    If Operand2 IsNot Nothing Then
                        name = "op_Addition"
                    Else
                        name = "op_UnaryPlus"
                    End If
                Case KS.Minus
                    If Operand2 IsNot Nothing Then
                        name = "op_Subtraction"
                    Else
                        name = "op_UnaryNegation"
                    End If
                Case KS.Mult
                    name = "op_Multiply"
                Case KS.IntDivision
                    name = "op_IntegerDivision"
                Case KS.RealDivision
                    name = "op_Division"
                Case KS.Concat
                    name = "op_Concatenate"
                Case KS.Like
                    name = "op_Like"
                Case KS.Mod
                    name = "op_Modulus"
                Case KS.And
                    name = "op_BitwiseAnd"
                Case KS.Or
                    name = "op_BitwiseOr"
                Case KS.Xor
                    name = "op_ExclusiveOr"
                Case KS.Power
                    name = "op_Exponent"
                Case KS.ShiftLeft
                    name = "op_LeftShift"
                Case KS.ShiftRight
                    name = "op_RightShift"
                Case KS.Equals
                    name = "op_Equality"
                Case KS.NotEqual
                    name = "op_Inequality"
                Case KS.GT
                    name = "op_GreaterThan"
                Case KS.LT
                    name = "op_LessThan"
                Case KS.GE
                    name = "op_GreaterThanOrEqual"
                Case KS.LE
                    name = "op_LessThanOrEqual"
                Case KS.Not
                    name = "op_OnesComplement"
                Case Else
                    Throw New InternalException(Me)
            End Select
        End If
        Helper.Assert(name <> "")
        mySignature.Init(name, Nothing, parameters, ReturnTypeAttributes, TypeName, Me.Location)

        'm_Operator = [Operator]
        m_Operand1 = Operand1
        m_Operand2 = Operand2

        MyBase.Init(Attributes, Modifiers, mySignature, Block)
    End Sub


    ReadOnly Property Operand1() As Operand
        Get
            Return m_operand1
        End Get
    End Property

    ReadOnly Property Operand2() As Operand
        Get
            Return m_Operand2
        End Get
    End Property

    'ReadOnly Property [Operator]() As token
    '    Get
    '        Return m_Operator
    '    End Get
    'End Property

    ''' <summary>
    ''' OverloadableBinaryOperator  ::=
    '''	"+" | "-" | "*" |  "/"  |  "\"  |  "&amp;" |  "Like"  |  "Mod"  |  "And"  |  "Or" |  "Xor"  |
    '''	"^" | "&lt;&lt;" |  "&gt;&gt;" | "="  |  "&lt;&gt;" | "&gt;" |  "&lt;" |  "&gt;="  |  "&lt;="
    ''' </summary>
    ''' <param name="token"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsOverloadableBinaryOperator(ByVal token As Token) As Boolean
        Return token = KS.Add OrElse token = KS.Minus OrElse token = KS.Mult _
        OrElse token = KS.IntDivision OrElse token = KS.RealDivision OrElse token = KS.Concat _
        OrElse token = KS.Like OrElse token = KS.Mod OrElse token = KS.And OrElse token = KS.Or _
        OrElse token = KS.Xor OrElse token = KS.Power OrElse token = KS.ShiftLeft _
        OrElse token = KS.ShiftRight OrElse token = KS.Equals OrElse token = KS.NotEqual _
        OrElse token = KS.GT OrElse token = KS.LT OrElse token = KS.GE OrElse token = KS.LE
    End Function

    ''' <summary>
    ''' OverloadableUnaryOperator  ::=  "+"  |  "-"  |  "Not"  |  "IsTrue"  |  "IsFals"
    ''' </summary>
    ''' <param name="token"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function IsOverloadableUnaryOperator(ByVal token As Token) As Boolean
        Return token = KS.Add OrElse token = KS.Minus OrElse token = KS.Not OrElse token.Equals("IsTrue") OrElse token.Equals("IsFalse")
    End Function

    Shared Function IsOverloadableOperator(ByVal token As Token) As Boolean
        Return IsOverloadableBinaryOperator(token) OrElse IsOverloadableUnaryOperator(token)
    End Function

    Shared Shadows Function IsMe(ByVal tm As tm) As Boolean
        Dim i As Integer
        While tm.PeekToken(i).Equals(ModifierMasks.OperatorModifiers)
            i += 1
        End While
        If tm.PeekToken(i).Equals(KS.Operator) = False Then Return False
        If IsOverloadableOperator(tm.PeekToken(i + 1)) = False Then Return False
        Return True
    End Function
End Class
