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
''' All the keywords.
''' </summary>
''' <remarks></remarks>
Public Enum KS
    None

    'First real keywords (on top modifiers, so that they can be used by Modifiers as bitshifts)

    [Ansi]
    [Auto]
    [ByRef]
    [ByVal]
    [Const]
    [Default]
    [Dim]
    [Friend]
    [Inherits]
    [MustInherit]
    [MustOverride]
    [Narrowing]
    [NotInheritable]
    [NotOverridable]
    [Optional]
    [Overloads]
    [Overridable]
    [Overrides]
    [Partial]
    [ParamArray]
    [Private]
    [Protected]
    [Public]
    [ReadOnly]
    [Shadows]
    [Shared]
    [Static]
    [Unicode]
    [Widening]
    [WithEvents]
    [WriteOnly]

    [AddHandler]
    [AddressOf]
    [AndAlso]
    [Alias]
    [And]
    [As]
    [Boolean]
    [Byte]
    [Call]
    [Case]
    [Catch]
    [CBool]
    [CByte]
    [CChar]
    [CDate]
    [CDec]
    [CDbl]
    [Char]
    [CInt]
    [Class]
    [CLng]
    [CObj]
    [Continue]
    [CSByte]
    [CShort]
    [CSng]
    [CStr]
    [CUInt]
    [CULng]
    [CUShort]
    [CType]
    [Date]
    [Decimal]
    [Declare]
    [Delegate]
    [DirectCast]
    [Do]
    [Double]
    [Each]
    [Else]
    [ElseIf]
    [End]
    [Enum]
    [Erase]
    [Error]
    [Event]
    [Exit]
    [False]
    [Finally]
    [For]
    [Function]
    [Get]
    [GetType]
    [Global]
    [GoTo]
    [Handles]
    [If]
    [Implements]
    [Imports]
    [In]
    [Integer]
    [Interface]
    [Is]
    [IsNot]
    [Let]
    [Lib]
    [Like]
    [Long]
    [Loop]
    [Me]
    [Mod]
    [Module]
    [MyBase]
    [MyClass]
    [Namespace]
    [New]
    [Next]
    [Not]
    [Nothing]
    [Object]
    [Of]
    [On]
    [Operator]
    [Option]
    [Or]
    [OrElse]
    [Property]
    [RaiseEvent]
    [ReDim]
    [REM]
    [RemoveHandler]
    [Resume]
    [Return]
    [SByte]
    [Select]
    [Set]
    [Short]
    [Single]
    [Step]
    [Stop]
    [String]
    [Structure]
    [Sub]
    [SyncLock]
    [Then]
    [Throw]
    [To]
    [True]
    [Try]
    [TryCast]
    [TypeOf]
    [UInteger]
    [ULong]
    [UShort]
    [Using]
    [Until]
    [Variant]
    [When]
    [While]
    [With]
    [Xor]

    'Real symbols
    LT
    GT
    Equals
    NotEqual
    LE
    GE
    Exclamation
    Interrogation
    Concat
    Mult
    Add
    Minus
    Power
    RealDivision
    IntDivision
    Numeral
    LBrace
    RBrace
    LParenthesis
    RParenthesis
    Dot
    Comma
    Colon
    ShiftLeft
    ShiftRight

    'Assignment symbols
    ConcatAssign
    AddAssign
    MinusAssign
    RealDivAssign
    IntDivAssign
    PowerAssign
    MultAssign
    ShiftLeftAssign
    ShiftRightAssign
    NumberOfItems 'This is the number of constants in this enum
End Enum


