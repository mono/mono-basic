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

<Flags()> _
Public Enum ModifierMasks
    [Ansi] = 1 << KS.Ansi
    [Auto] = 1 << KS.Auto
    [ByRef] = 1 << KS.ByRef
    [ByVal] = 1 << KS.ByVal
    [Const] = 1 << KS.Const
    [Default] = 1 << KS.Default
    [Dim] = 1 << KS.Dim
    [Friend] = 1 << KS.Friend
    [Inherits] = 1 << KS.Inherits
    [MustInherit] = 1 << KS.MustInherit
    [MustOverride] = 1 << KS.MustOverride
    [Narrowing] = 1 << KS.Narrowing
    [NotInheritable] = 1 << KS.NotInheritable
    [NotOverridable] = 1 << KS.NotOverridable
    [Optional] = 1 << KS.Optional
    [Overloads] = 1 << KS.Overloads
    [Overridable] = 1 << KS.Overridable
    [Overrides] = 1 << KS.Overrides
    [Partial] = 1 << KS.Partial
    [ParamArray] = 1 << KS.ParamArray
    [Private] = 1 << KS.Private
    [Protected] = 1 << KS.Protected
    [Public] = 1 << KS.Public
    [ReadOnly] = 1 << KS.ReadOnly
    [Shadows] = 1 << KS.Shadows
    [Shared] = 1 << KS.Shared
    [Static] = 1 << KS.Static
    [Unicode] = 1 << KS.Unicode
    [Widening] = 1 << KS.Widening
    [WithEvents] = 1 << KS.WithEvents
    [WriteOnly] = 1 << KS.WriteOnly



    ''' <summary>
    ''' ExternalMethodModifier  ::=  AccessModifier  |  "Shadows" | "Overloads"
    ''' </summary>
    ''' <remarks></remarks>
    ExternalMethodModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.Overloads

    ''' <summary>
    ''' CharsetModifier  ::=  "Ansi" | "Unicode" |  "Auto"
    ''' </summary>
    ''' <remarks></remarks>
    CharSetModifiers = ModifierMasks.Auto Or ModifierMasks.Unicode Or ModifierMasks.Ansi

    ''' <summary>
    ''' AccessModifier  ::=  "Public" |  "Protected" | "Friend" | "Private" | "Protected" "Friend"
    ''' </summary>
    ''' <remarks></remarks>
    AccessModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private
    ''' <summary>
    ''' VariableModifier  ::= AccessModifier  |	Shadows  |	Shared  |	ReadOnly  |	WithEvents  |	Dim
    ''' </summary>
    ''' <remarks></remarks>
    VariableModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.Shared Or ModifierMasks.ReadOnly Or ModifierMasks.WithEvents Or ModifierMasks.Dim

    ''' <summary>
    ''' ConstantModifier  ::=  AccessModifier  |  Shadows
    ''' </summary>
    ''' <remarks></remarks>
    ConstantModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows

    ''' <summary>
    ''' LocalModifier  ::=  "Static" |"Dim" | "Const"
    ''' </summary>
    ''' <remarks></remarks>
    LocalModifiers = ModifierMasks.Dim Or ModifierMasks.Static Or ModifierMasks.Const

    ''' <summary>
    ''' ParameterModifier  ::=  ByVal  |  ByRef  |  Optional  |  ParamArray
    ''' </summary>
    ''' <remarks></remarks>
    ParameterModifiers = ModifierMasks.ByVal Or ModifierMasks.ByRef Or ModifierMasks.Optional Or ModifierMasks.ParamArray

    ''' <summary>
    ''' TypeModifier  ::=  AccessModifier  |  "Shadows"
    ''' </summary>
    ''' <remarks></remarks>
    TypeModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows

    ''' <summary>
    ''' ClassModifier  ::=  TypeModifier  |  "MustInherit"  |  "NotInheritable"  |  "Partial"
    ''' </summary>
    ''' <remarks></remarks>
    ClassModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.MustInherit Or ModifierMasks.NotInheritable Or ModifierMasks.Partial

    ''' <summary>
    ''' StructureModifier  ::=  TypeModifier  |  "Partial"
    ''' </summary>
    ''' <remarks></remarks>
    StructureModifiers = ModifierMasks.Public Or ModifierMasks.Protected Or ModifierMasks.Friend Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.Partial

    ''' <summary>
    ''' EventModifiers  ::=  AccessModifier  |  "Shadows" |  "Shared"
    ''' </summary>
    ''' <remarks></remarks>
    EventModifiers = ModifierMasks.Public Or ModifierMasks.Private Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Shadows Or ModifierMasks.Shared

    ''' <summary>
    ''' ProcedureModifier ::= AccessModifier | "Shadows" | "Shared" | "Overridable" | "NotOverridable" | "Overrides" | "Overloads"
    ''' </summary>
    ''' <remarks></remarks>
    ProcedureModifiers = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.Shared Or ModifierMasks.Overridable Or ModifierMasks.NotOverridable Or ModifierMasks.Overrides Or ModifierMasks.Overloads

    ''' <summary>
    ''' MustOverrideProcedureModifier  ::=  ProcedureModifier  |  "MustOverride"
    ''' </summary>
    ''' <remarks></remarks>
    MustOverrideProcedureModifiers = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shadows Or ModifierMasks.Shared Or ModifierMasks.Overridable Or ModifierMasks.NotOverridable Or ModifierMasks.Overrides Or ModifierMasks.Overloads Or ModifierMasks.MustOverride

    ''' <summary>
    ''' MustOverridePropertyModifier  ::=  PropertyModifier  | "MustOverride"
    ''' </summary>
    ''' <remarks></remarks>
    MustOverridePropertyModifiers = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shared Or ModifierMasks.Overridable Or ModifierMasks.NotOverridable Or ModifierMasks.Overrides Or ModifierMasks.Overloads Or ModifierMasks.Default Or ModifierMasks.ReadOnly Or ModifierMasks.WriteOnly Or ModifierMasks.MustOverride

    ''' <summary>
    ''' PropertyModifier  ::=  ProcedureModifier  |  "Default"  |  "ReadOnly"  |  "WriteOnly"
    ''' </summary>
    ''' <remarks></remarks>
    PropertyModifiers = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shared Or ModifierMasks.Overridable Or ModifierMasks.NotOverridable Or ModifierMasks.Overrides Or ModifierMasks.Overloads Or ModifierMasks.Default Or ModifierMasks.ReadOnly Or ModifierMasks.WriteOnly Or ModifierMasks.Shadows

    ''' <summary>
    ''' InterfacePropertyModifier  ::=	"Shadows"  |	"Overloads"  |	"Default"  |	"ReadOnly"  |	"WriteOnly"
    ''' </summary>
    ''' <remarks></remarks>
    InterfacePropertyModifier = ModifierMasks.Shadows Or ModifierMasks.Overloads Or ModifierMasks.Default Or ModifierMasks.ReadOnly Or ModifierMasks.WriteOnly

    ''' <summary>
    ''' ConstructorModifier  ::=  AccessModifier  |  "Shared"
    ''' </summary>
    ''' <remarks></remarks>
    ConstructorModifiers = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shared

    ''' <summary>
    ''' OperatorModifier  ::=  "Public"  | "Shared"  |  "Overloads"  |  "Shadows"
    ''' </summary>
    ''' <remarks></remarks>
    OperatorModifiers = ModifierMasks.Public Or ModifierMasks.Shared Or ModifierMasks.Overloads Or ModifierMasks.Shadows

''' <summary>
''' InterfaceEventModifiers  ::=  "Shadows"
''' </summary>
''' <remarks></remarks>
    InterfaceEventModifiers = ModifierMasks.Shadows

    ''' <summary>
    ''' InterfaceProcedureModifier  ::=  "Shadows" | "Overloads"
    ''' </summary>
    ''' <remarks></remarks>
    InterfaceProcedureModifiers = ModifierMasks.Shadows Or ModifierMasks.Overloads

    ''' <summary>
    ''' ConversionOperatorModifier  ::=  "Widening" | "Narrowing" |  ConversionModifier
    ''' LAMESPEC: ConversionModifier should be OperatorModifier
    ''' </summary>
    ''' <remarks></remarks>
    ConversionOperatorModifiers = ModifierMasks.Widening Or ModifierMasks.Narrowing Or ModifierMasks.Public Or ModifierMasks.Shared Or ModifierMasks.Overloads Or ModifierMasks.Shadows

    ''' <summary>
    ''' ConstructorModifier  ::=  AccessModifier  |  "Shared"
    ''' </summary>
    ''' <remarks></remarks>
    ConstructorModifier = ModifierMasks.Public Or ModifierMasks.Friend Or ModifierMasks.Protected Or ModifierMasks.Private Or ModifierMasks.Shared
End Enum
