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

Public Enum UnaryOperators
    <KSEnumString("op_UnaryPlus")> [Add] = KS.Add
    <KSEnumString("op_UnaryNegation")> [Minus] = KS.Minus
    <KSEnumString("op_OnesComplement")> [Not] = KS.Not
End Enum

Public Enum BinaryOperators
    <KSEnumString("op_BitwiseAnd")> [And] = KS.And
    <KSEnumString("op_Like")> [Like] = KS.Like
    <KSEnumString("op_Modulus")> [Mod] = KS.Mod
    <KSEnumString("op_BitwiseOr")> [Or] = KS.Or
    <KSEnumString("op_ExclusiveOr")> [XOr] = KS.Xor
    <KSEnumString("op_LessThan")> LT = KS.LT
    <KSEnumString("op_GreaterThan")> GT = KS.GT
    <KSEnumString("op_Equality")> Equals = KS.Equals
    <KSEnumString("op_Inequality")> NotEqual = KS.NotEqual
    <KSEnumString("op_LessThanOrEqual")> LE = KS.LE
    <KSEnumString("op_GreaterThanOrEqual")> GE = KS.GE
    <KSEnumString("op_Concatenate")> Concat = KS.Concat
    <KSEnumString("op_Multiply")> Mult = KS.Mult
    <KSEnumString("op_Addition")> Add = KS.Add
    <KSEnumString("op_Subtraction")> Minus = KS.Minus
    <KSEnumString("op_Exponent")> Power = KS.Power
    <KSEnumString("op_Division")> RealDivision = KS.RealDivision
    <KSEnumString("op_IntegerDivision")> IntDivision = KS.IntDivision
    <KSEnumString("op_LeftShift")> ShiftLeft = KS.ShiftLeft
    <KSEnumString("op_RightShift")> ShiftRight = KS.ShiftRight
    <KSEnumString("op_True")> IsTrue = KS.NumberOfItems + 1
    <KSEnumString("op_False")> IsFalse
End Enum
