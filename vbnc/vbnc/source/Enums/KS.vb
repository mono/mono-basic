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
''' All the keywords.
''' </summary>
''' <remarks></remarks>
Public Enum KS
    <KSEnumString("", "NONE")> None
    'First True keywords
    <KSEnumString("AddHandler", Nothing, True, False, KS.End_AddHandler)> [AddHandler]
    <KSEnumString("AddressOf", Nothing, True, False)> [AddressOf]
    <KSEnumString("AndAlso", Nothing, True, False)> [AndAlso]
    <KSEnumString("Alias", Nothing, True, False)> [Alias]
    <KSEnumString("And", Nothing, True, False)> [And]
    <KSEnumString("Ansi", Nothing, True, False)> [Ansi]
    <KSEnumString("As", Nothing, True, False)> [As]
    ' <KSEnumString("Assembly", Nothing, True, False)> [Assembly]
    <KSEnumString("Auto", Nothing, True, False)> [Auto]
    <KSEnumString("Boolean", Nothing, True, False)> [Boolean]
    <KSEnumString("ByRef", Nothing, True, False)> [ByRef]
    <KSEnumString("Byte", Nothing, True, False)> [Byte]
    <KSEnumString("ByVal", Nothing, True, False)> [ByVal]
    <KSEnumString("Call", Nothing, True, False)> [Call]
    <KSEnumString("Case", Nothing, True, False)> [Case]
    <KSEnumString("Catch", Nothing, True, False)> [Catch]
    <KSEnumString("CBool", Nothing, True, False)> [CBool]
    <KSEnumString("CByte", Nothing, True, False)> [CByte]
    <KSEnumString("CChar", Nothing, True, False)> [CChar]
    <KSEnumString("CDate", Nothing, True, False)> [CDate]
    <KSEnumString("CDec", Nothing, True, False)> [CDec]
    <KSEnumString("CDbl", Nothing, True, False)> [CDbl]
    <KSEnumString("Char", Nothing, True, False)> [Char]
    <KSEnumString("CInt", Nothing, True, False)> [CInt]
    <KSEnumString("Class", Nothing, True, False, KS.End_Class)> [Class]
    <KSEnumString("CLng", Nothing, True, False)> [CLng]
    <KSEnumString("CObj", Nothing, True, False)> [CObj]
    <KSEnumString("Const", Nothing, True, False)> [Const]
    <KSEnumString("Continue", Nothing, True, False)> [Continue]
    <KSEnumString("CSByte", Nothing, True, False)> [CSByte]
    <KSEnumString("CShort", Nothing, True, False)> [CShort]
    <KSEnumString("CSng", Nothing, True, False)> [CSng]
    <KSEnumString("CStr", Nothing, True, False)> [CStr]
    <KSEnumString("CUInt", Nothing, True, False)> [CUInt]
    <KSEnumString("CULng", Nothing, True, False)> [CULng]
    <KSEnumString("CUShort", Nothing, True, False)> [CUShort]
    <KSEnumString("CType", Nothing, True, False)> [CType]
    <KSEnumString("Custom Event", Nothing, True, False)> [CustomEvent]
    <KSEnumString("Date", Nothing, True, False)> [Date]
    <KSEnumString("Decimal", Nothing, True, False)> [Decimal]
    <KSEnumString("Declare", Nothing, True, False)> [Declare]
    <KSEnumString("Default", Nothing, True, False)> [Default]
    <KSEnumString("Delegate", Nothing, True, False)> [Delegate]
    <KSEnumString("Dim", Nothing, True, False)> [Dim]
    <KSEnumString("DirectCast", Nothing, True, False)> [DirectCast]
    <KSEnumString("Do", Nothing, True, False)> [Do]
    <KSEnumString("Double", Nothing, True, False)> [Double]
    <KSEnumString("Each", Nothing, True, False)> [Each]
    <KSEnumString("Else", Nothing, True, False)> [Else]
    <KSEnumString("ElseIf", Nothing, True, False)> [ElseIf]
    <KSEnumString("End", Nothing, True, False)> [End]
    <KSEnumString("Enum", Nothing, True, False, KS.End_Enum)> [Enum]
    <KSEnumString("Erase", Nothing, True, False)> [Erase]
    <KSEnumString("Error", Nothing, True, False)> [Error]
    <KSEnumString("Event", Nothing, True, False, KS.End_Event)> [Event]
    <KSEnumString("Exit", Nothing, True, False)> [Exit]
    <KSEnumString("False", Nothing, True, False)> [False]
    <KSEnumString("Finally", Nothing, True, False)> [Finally]
    <KSEnumString("For", Nothing, True, False)> [For]
    <KSEnumString("Friend", Nothing, True, False)> [Friend]
    <KSEnumString("Function", Nothing, True, False, KS.End_Function)> [Function]
    <KSEnumString("Get", Nothing, True, False, KS.End_Get)> [Get]
    <KSEnumString("GetType", Nothing, True, False)> [GetType]
    <KSEnumString("Global", Nothing, True, False)> [Global]
    <KSEnumString("GoTo", Nothing, True, False)> [GoTo]
    <KSEnumString("Handles", Nothing, True, False)> [Handles]
    <KSEnumString("If", Nothing, True, False, KS.End_If)> [If]
    <KSEnumString("Implements", Nothing, True, False)> [Implements]
    <KSEnumString("Imports", Nothing, True, False)> [Imports]
    <KSEnumString("In", Nothing, True, False)> [In]
    <KSEnumString("Inherits", Nothing, True, False)> [Inherits]
    <KSEnumString("Integer", Nothing, True, False)> [Integer]
    <KSEnumString("Interface", Nothing, True, False, KS.End_Interface)> [Interface]
    <KSEnumString("Is", Nothing, True, False)> [Is]
    <KSEnumString("IsNot", Nothing, True, False)> [IsNot]
    <KSEnumString("Let", Nothing, True, False)> [Let]
    <KSEnumString("Lib", Nothing, True, False)> [Lib]
    <KSEnumString("Like", Nothing, True, False)> [Like]
    <KSEnumString("Long", Nothing, True, False)> [Long]
    <KSEnumString("Loop", Nothing, True, False)> [Loop]
    <KSEnumString("Me", Nothing, True, False)> [Me]
    <KSEnumString("Mod", Nothing, True, False)> [Mod]
    <KSEnumString("Module", Nothing, True, False, KS.End_Module)> [Module]
    <KSEnumString("MustInherit", Nothing, True, False)> [MustInherit]
    <KSEnumString("MustOverride", Nothing, True, False)> [MustOverride]
    <KSEnumString("MyBase", Nothing, True, False)> [MyBase]
    <KSEnumString("MyClass", Nothing, True, False)> [MyClass]
    <KSEnumString("Namespace", Nothing, True, False, KS.End_Namespace)> [Namespace]
    <KSEnumString("Narrowing", Nothing, True, False)> [Narrowing]
    <KSEnumString("New", Nothing, True, False)> [New]
    <KSEnumString("Next", Nothing, True, False)> [Next]
    <KSEnumString("Not", Nothing, True, False)> [Not]
    <KSEnumString("Nothing", Nothing, True, False)> [Nothing]
    <KSEnumString("NotInheritable", Nothing, True, False)> [NotInheritable]
    <KSEnumString("NotOverridable", Nothing, True, False)> [NotOverridable]
    <KSEnumString("Object", Nothing, True, False)> [Object]
    <KSEnumString("Of", Nothing, True, False)> [Of]
    <KSEnumString("On", Nothing, True, False)> [On]
    <KSEnumString("Operator", Nothing, True, False, KS.End_Operator)> [Operator]
    <KSEnumString("Option", Nothing, True, False)> [Option]
    <KSEnumString("Optional", Nothing, True, False)> [Optional]
    <KSEnumString("Or", Nothing, True, False)> [Or]
    <KSEnumString("Orelse", Nothing, True, False)> [OrElse]
    <KSEnumString("Overloads", Nothing, True, False)> [Overloads]
    <KSEnumString("Overridable", Nothing, True, False)> [Overridable]
    <KSEnumString("Overrides", Nothing, True, False)> [Overrides]
    <KSEnumString("Partial", Nothing, True, False)> [Partial]
    <KSEnumString("ParamArray", Nothing, True, False)> [ParamArray]
    '<KSEnumString("Preserve", Nothing, True, False)> Preserve
    <KSEnumString("Private", Nothing, True, False)> [Private]
    <KSEnumString("Property", Nothing, True, False, KS.End_Property)> [Property]
    <KSEnumString("Protected", Nothing, True, False)> [Protected]
    <KSEnumString("Public", Nothing, True, False)> [Public]
    <KSEnumString("RaiseEvent", Nothing, True, False, KS.End_RaiseEvent)> [RaiseEvent]
    <KSEnumString("ReadOnly", Nothing, True, False)> [ReadOnly]
    <KSEnumString("ReDim", Nothing, True, False)> [ReDim]
    <KSEnumString("REM", Nothing, True, False)> [REM]
    <KSEnumString("RemoveHandler", Nothing, True, False, KS.End_RemoveHandler)> [RemoveHandler]
    <KSEnumString("Resume", Nothing, True, False)> [Resume]
    <KSEnumString("Return", Nothing, True, False)> [Return]
    <KSEnumString("SByte", Nothing, True, False)> [SByte]
    <KSEnumString("Select", Nothing, True, False, KS.End_Select)> [Select]
    <KSEnumString("Set", Nothing, True, False, KS.End_Set)> [Set]
    <KSEnumString("Shadows", Nothing, True, False)> [Shadows]
    <KSEnumString("Shared", Nothing, True, False)> [Shared]
    <KSEnumString("Short", Nothing, True, False)> [Short]
    <KSEnumString("Single", Nothing, True, False)> [Single]
    <KSEnumString("Static", Nothing, True, False)> [Static]
    <KSEnumString("Step", Nothing, True, False)> [Step]
    <KSEnumString("Stop", Nothing, True, False)> [Stop]
    <KSEnumString("String", Nothing, True, False)> [String]
    <KSEnumString("Structure", Nothing, True, False, KS.End_Structure)> [Structure]
    <KSEnumString("Sub", Nothing, True, False, KS.End_Sub)> [Sub]
    <KSEnumString("SyncLock", Nothing, True, False, KS.End_SyncLock)> [SyncLock]
    <KSEnumString("Then", Nothing, True, False)> [Then]
    <KSEnumString("Throw", Nothing, True, False)> [Throw]
    <KSEnumString("To", Nothing, True, False)> [To]
    <KSEnumString("True", Nothing, True, False)> [True]
    <KSEnumString("Try", Nothing, True, False, KS.End_Try)> [Try]
    <KSEnumString("TryCast", Nothing, True, False)> [TryCast]
    <KSEnumString("TypeOf", Nothing, True, False)> [TypeOf]
    <KSEnumString("UInteger", Nothing, True, False)> [UInteger]
    <KSEnumString("ULong", Nothing, True, False)> [ULong]
    <KSEnumString("UShort", Nothing, True, False)> [UShort]
    <KSEnumString("Using", Nothing, True, False, KS.End_Using)> [Using]
    <KSEnumString("Unicode", Nothing, True, False)> [Unicode]
    <KSEnumString("Until", Nothing, True, False)> [Until]
    <KSEnumString("Variant", Nothing, True, False)> [Variant]
    <KSEnumString("When", Nothing, True, False)> [When]
    <KSEnumString("While", Nothing, True, False, KS.End_While)> [While]
    <KSEnumString("Widening", Nothing, True, False)> [Widening]
    <KSEnumString("With", Nothing, True, False, KS.End_With)> [With]
    <KSEnumString("WithEvents", Nothing, True, False)> [WithEvents]
    <KSEnumString("WriteOnly", Nothing, True, False)> [WriteOnly]
    <KSEnumString("Xor", Nothing, True, False)> [Xor]
    'Semi keywords
    <KSEnumString("#End", Nothing, False, False)> ConditionalEnd
    <KSEnumString("#Const", Nothing, False, False)> ConditionalConst
    <KSEnumString("#End If", Nothing, False, False, KS.ConditionalIf)> ConditionalEndIf
    <KSEnumString("#End Region", Nothing, False, False, KS.ConditionalRegion)> ConditionalEndRegion
    <KSEnumString("#End ExternalSource", Nothing, False, False, KS.ConditionalExternalSource)> ConditionalEndExternalSource
    <KSEnumString("#Else", Nothing, False, False)> ConditionalElse
    <KSEnumString("#ElseIf", Nothing, False, False)> ConditionalElseIf
    <KSEnumString("#ExternalSource", Nothing, False, False, KS.ConditionalEndExternalSource)> ConditionalExternalSource
    <KSEnumString("#If", Nothing, False, False, KS.ConditionalEndIf)> ConditionalIf
    <KSEnumString("#Region", Nothing, False, False, KS.ConditionalEndRegion)> ConditionalRegion

    <KSEnumString("End AddHandler", Nothing, False, False, KS.AddHandler)> End_AddHandler
    <KSEnumString("End Class", Nothing, False, False, KS.Class)> End_Class
    <KSEnumString("End Enum", Nothing, False, False, KS.Enum)> End_Enum
    <KSEnumString("End Event", Nothing, False, False, KS.Event)> End_Event
    <KSEnumString("End Function", Nothing, False, False, KS.Function)> End_Function
    <KSEnumString("End Get", Nothing, False, False, KS.Get)> End_Get
    <KSEnumString("End If", Nothing, False, False, KS.If)> End_If
    <KSEnumString("End Interface", Nothing, False, False, KS.Interface)> End_Interface
    <KSEnumString("End Module", Nothing, False, False, KS.Module)> End_Module
    <KSEnumString("End Namespace", Nothing, False, False, KS.Namespace)> End_Namespace
    <KSEnumString("End Operator", Nothing, False, False, KS.Operator)> End_Operator
    <KSEnumString("End Property", Nothing, False, False, KS.Property)> End_Property
    <KSEnumString("End RaiseEvent", Nothing, False, False, KS.RaiseEvent)> End_RaiseEvent
    <KSEnumString("End RemoveHandler", Nothing, False, False, KS.RemoveHandler)> End_RemoveHandler
    <KSEnumString("End Select", Nothing, False, False, KS.Select)> End_Select
    <KSEnumString("End Set", Nothing, False, False, KS.Set)> End_Set
    <KSEnumString("End Structure", Nothing, False, False, KS.Structure)> End_Structure
    <KSEnumString("End Sub", Nothing, False, False, KS.Sub)> End_Sub
    <KSEnumString("End SynckLock", Nothing, False, False, KS.SyncLock)> End_SyncLock
    <KSEnumString("End Try", Nothing, False, False, KS.Try)> End_Try
    <KSEnumString("End While", Nothing, False, False, KS.While)> End_While
    <KSEnumString("End With", Nothing, False, False, KS.With)> End_With
    <KSEnumString("End Using", Nothing, False, False, KS.Using)> End_Using

    'Real symbols
    <KSEnumString("<", Nothing, False, True)> LT
    <KSEnumString(">", Nothing, False, True)> GT
    <KSEnumString("=", Nothing, False, True)> Equals
    <KSEnumString("<>", Nothing, False, True)> NotEqual
    <KSEnumString("<=", Nothing, False, True)> LE
    <KSEnumString(">=", Nothing, False, True)> GE
    <KSEnumString("!", Nothing, False, True)> Exclamation
    <KSEnumString("&", Nothing, False, True)> Concat
    <KSEnumString("*", Nothing, False, True)> Mult
    <KSEnumString("+", Nothing, False, True)> Add
    <KSEnumString("-", Nothing, False, True)> Minus
    <KSEnumString("^", Nothing, False, True)> Power
    <KSEnumString("/", Nothing, False, True)> RealDivision
    <KSEnumString("\", Nothing, False, True)> IntDivision
    <KSEnumString("#", Nothing, False, True)> Numeral
    <KSEnumString("{", Nothing, False, True)> LBrace
    <KSEnumString("}", Nothing, False, True)> RBrace
    <KSEnumString("(", Nothing, False, True)> LParenthesis
    <KSEnumString(")", Nothing, False, True)> RParenthesis
    <KSEnumString(".", " .", False, True)> Dot
    <KSEnumString(",", Nothing, False, True)> Comma
    <KSEnumString(":", Nothing, False, True)> Colon
    ''' <summary>
    ''' &lt;&lt;
    ''' </summary>
    ''' <remarks></remarks>
    <KSEnumString("<<", Nothing, False, True)> ShiftLeft
    ''' <summary>
    ''' &gt;&gt;
    ''' </summary>
    ''' <remarks></remarks>
    <KSEnumString(">>", Nothing, False, True)> ShiftRight

    'Assignment symbols
    <KSEnumString("&=", Nothing, False, True)> ConcatAssign '		L"&="
    <KSEnumString("+=", Nothing, False, True)> AddAssign 'L"+="
    <KSEnumString("-=", Nothing, False, True)> MinusAssign 'L"-="
    <KSEnumString("/=", Nothing, False, True)> RealDivAssign 'L"/="
    <KSEnumString("\=", Nothing, False, True)> IntDivAssign 'L"\="
    <KSEnumString("^=", Nothing, False, True)> PowerAssign 'L"^="
    <KSEnumString("*=", Nothing, False, True)> MultAssign 'L"*="
    <KSEnumString("<<=", Nothing, False, True)> ShiftLeftAssign '<<=
    <KSEnumString(">>=", Nothing, False, True)> ShiftRightAssign '>>=
    NumberOfItems 'This is the number of constants in this enum
End Enum

