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

Public Enum UnaryOperators
    [Add] = KS.Add
    [Minus] = KS.Minus
    [Not] = KS.Not
End Enum

Public Enum BinaryOperators
    [And] = KS.And
    [Like] = KS.Like
    [Mod] = KS.Mod
    [Or] = KS.Or
    [XOr] = KS.Xor
    LT = KS.LT
    GT = KS.GT
    Equals = KS.Equals
    NotEqual = KS.NotEqual
    LE = KS.LE
    GE = KS.GE
    Concat = KS.Concat
    Mult = KS.Mult
    Add = KS.Add
    Minus = KS.Minus
    Power = KS.Power
    RealDivision = KS.RealDivision
    IntDivision = KS.IntDivision
    ShiftLeft = KS.ShiftLeft
    ShiftRight = KS.ShiftRight
    IsTrue = KS.NumberOfItems + 1
    IsFalse
End Enum
