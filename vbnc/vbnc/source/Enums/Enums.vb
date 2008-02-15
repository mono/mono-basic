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

Module Enums
    ''' <summary>
    ''' The ks enum strings
    ''' </summary>
    ''' <remarks></remarks>
    Friend strSpecial(KS.NumberOfItems) As String

    Private keywordLookup As New Hashtable(Helper.StringComparer)

    ''' <summary>
    ''' The ks enums as friendly strings for dump support (i.e. NewLine as &lt;NewLine&gt;, etc.)
    ''' </summary>
    ''' <remarks></remarks>
    Friend strSpecialFriendly(KS.NumberOfItems) As String

    ''' <summary>
    ''' BinaryExpression ::= 
    '''   AndAlsoExpression | AndExpression | BinaryAddExpression | BinarySubExpression |
    '''   ConcatExpression | EqualsExpression | ExponentExpression | GEExpression |
    '''   GTExpression | IntDivisionExpression | IsExpression | IsNotExpression |
    '''   LEExpression | LikeExpression | LShiftExpression | LTExpression |
    '''   ModExpression | MultExpression | NotEqualsExpression | OrElseExpression |
    '''   OrExpression | RealDivisionExpression | RShiftExpression | XOrExpression
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly BinaryOperators As KS() = {KS.AndAlso, KS.And, KS.Add, KS.Minus, KS.Concat, KS.Equals, KS.Power, KS.GE, KS.GT, KS.IntDivision, KS.Is, KS.IsNot, KS.LE, KS.Like, KS.ShiftLeft, KS.LT, KS.Mod, KS.Mult, KS.NotEqual, KS.OrElse, KS.Or, KS.RealDivision, KS.ShiftRight, KS.Xor}

    ''' <summary>
    ''' UnaryExpression ::= UnaryMinusExpression | UnaryNotExpression | UnaryPlusExpression
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly UnaryOperators As KS() = {KS.Add, KS.Minus, KS.Not}
    ''' <summary>
    '''CastTarget  ::=
    '''	"CBool" | "CByte" | "CChar" | "CDate" | "CDec" | "CDbl" | "CInt" | "CLng" | "CObj" | "CSByte" | "CShort" |
    '''	"CSng" | "CStr" | "CUInt" | "CULng" | "CUShort"
    '''     ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly CastTargets As KS() = {KS.CBool, KS.CByte, KS.CChar, KS.CDate, KS.CDec, KS.CDbl, KS.CInt, KS.CLng, KS.CObj, KS.CSByte, KS.CShort, KS.CSng, KS.CStr, KS.CUInt, KS.CULng, KS.CUShort}

    ''' <summary>
    '''CastExpression  ::=
    '''	"DirectCast" (...)
    '''	"TryCast" (...)
    '''	"CType" (...)
    '''	CastTarget  (...)
    '''CastTarget  ::=
    '''	"CBool" | "CByte" | "CChar" | "CDate" | "CDec" | "CDbl" | "CInt" | "CLng" | "CObj" | "CSByte" | "CShort" |
    '''	"CSng" | "CStr" | "CUInt" | "CULng" | "CUShort"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly CastExpressionTargets As KS() = {KS.CBool, KS.CByte, KS.CChar, KS.CDate, KS.CDec, KS.CDbl, KS.CInt, KS.CLng, KS.CObj, KS.CSByte, KS.CShort, KS.CSng, KS.CStr, KS.CUInt, KS.CULng, KS.CUShort, KS.DirectCast, KS.TryCast, KS.CType}

    ''' <summary>
    ''' CCUnaryOperator  ::=  "+"  | "-"  |  "Not"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly CCUnaryOperators As KS() = New KS() {KS.Add, KS.Minus, KS.Not}

    ''' <summary>
    ''' CCBinaryOperator  ::=  "+" | "-" | "*" | "/" | "\" | "Mod" | "^" | "=" | "&lt;&gt;" | "&lt;" |  "&gt;" | "&lt;=" |
    '''                        "&gt;=" | "&amp;" | "And" | "Or" | "Xor" | "AndAlso" | "OrElse" |  "&lt;&lt;" | "&gt;&gt;"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly CCBinaryOperators As KS() = New KS() {KS.Add, KS.Minus, KS.Mult, KS.RealDivision, KS.IntDivision, KS.Mod, KS.Power, KS.Equals, KS.NotEqual, KS.LT, KS.GT, KS.LE, KS.GE, KS.Concat, KS.And, KS.Or, KS.Xor, KS.AndAlso, KS.OrElse, KS.ShiftLeft, KS.ShiftRight}

    ''' <summary>
    ''' BuiltInTypeName  ::=  "Object" |  PrimitiveTypeName
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly BuiltInTypeTypeNames As KS() = New KS() {KS.Boolean, KS.Byte, KS.SByte, KS.Short, KS.UShort, KS.Integer, KS.UInteger, KS.Long, KS.ULong, KS.Decimal, KS.Single, KS.Double, KS.Object, KS.Date, KS.String, KS.Char}

    ''' <summary>
    ''' FloatingPointTypeName  ::=  "Single" | "Double"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly FloatingPointTypeNames As KS() = New KS() {KS.Single, KS.Double}

    ''' <summary>
    ''' IntegralTypeName  ::= "Byte" | "SByte "| "UShort" | "Short "| "UInteger" | "Integer" | "ULong" | "Long"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly IntegralTypeNames As KS() = New KS() {KS.Byte, KS.SByte, KS.UShort, KS.Short, KS.UInteger, KS.Integer, KS.ULong, KS.Long}

    ''' <summary>
    ''' NumericTypeName  ::=  IntegralTypeName  |  FloatingPointTypeName  | "Decimal"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly NumericTypeNames As KS() = New KS() {KS.Single, KS.Double, KS.Byte, KS.SByte, KS.UShort, KS.Short, KS.UInteger, KS.Integer, KS.ULong, KS.Long, KS.Decimal}

    ''' <summary>
    ''' PrimitiveTypeName  ::=  NumericTypeName  | "Boolean" | "Date" | "Char" | "String"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly PrimitiveTypeNames As KS() = New KS() {KS.Single, KS.Double, KS.Byte, KS.SByte, KS.UShort, KS.Short, KS.UInteger, KS.Integer, KS.ULong, KS.Long, KS.Decimal, KS.String, KS.Boolean, KS.Date, KS.Char}
    
    '''' <summary>
    '''' ExternalMethodModifier  ::=  AccessModifier  |  "Shadows" | "Overloads"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ExternalMethodModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows, KS.Overloads}

    '''' <summary>
    '''' CharsetModifier  ::=  "Ansi" | "Unicode" |  "Auto"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly CharSetModifiers As KS() = New KS() {KS.Auto, KS.Unicode, KS.Ansi}

    '''' <summary>
    '''' AccessModifier  ::=  "Public" |  "Protected" | "Friend" | "Private" | "Protected" "Friend"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly AccessModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private}
    '''' <summary>
    '''' VariableModifier  ::= AccessModifier  |	Shadows  |	Shared  |	ReadOnly  |	WithEvents  |	Dim
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly VariableModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows, KS.Shared, KS.ReadOnly, KS.WithEvents, KS.Dim}

    '''' <summary>
    '''' ConstantModifier  ::=  AccessModifier  |  Shadows
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ConstantModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows}

    '''' <summary>
    '''' LocalModifier  ::=  "Static" |"Dim" | "Const"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly LocalModifiers As KS() = New KS() {KS.Dim, KS.Static, KS.Const}

    '''' <summary>
    '''' ParameterModifier  ::=  ByVal  |  ByRef  |  Optional  |  ParamArray
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ParameterModifiers As KS() = New KS() {KS.ByVal, KS.ByRef, KS.Optional, KS.ParamArray}

    '''' <summary>
    '''' TypeModifier  ::=  AccessModifier  |  "Shadows"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly TypeModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows}

    '''' <summary>
    '''' ClassModifier  ::=  TypeModifier  |  "MustInherit"  |  "NotInheritable"  |  "Partial"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ClassModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows, KS.MustInherit, KS.NotInheritable, KS.Partial}

    '''' <summary>
    '''' StructureModifier  ::=  TypeModifier  |  "Partial"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly StructureModifiers As KS() = New KS() {KS.Public, KS.Protected, KS.Friend, KS.Private, KS.Shadows, KS.Partial}

    '''' <summary>
    '''' EventModifiers  ::=  AccessModifier  |  "Shadows" |  "Shared"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly EventModifiers As KS() = New KS() {KS.Public, KS.Private, KS.Friend, KS.Protected, KS.Shadows, KS.Shared}

    '''' <summary>
    '''' ProcedureModifier ::= AccessModifier | "Shadows" | "Shared" | "Overridable" | "NotOverridable" | "Overrides" | "Overloads"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ProcedureModifiers As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shadows, KS.Shared, KS.Overridable, KS.NotOverridable, KS.Overrides, KS.Overloads}

    '''' <summary>
    '''' MustOverrideProcedureModifier  ::=  ProcedureModifier  |  "MustOverride"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly MustOverrideProcedureModifiers As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shadows, KS.Shared, KS.Overridable, KS.NotOverridable, KS.Overrides, KS.Overloads, KS.MustOverride}

    '''' <summary>
    '''' MustOverridePropertyModifier  ::=  PropertyModifier  | "MustOverride"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly MustOverridePropertyModifiers As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shared, KS.Overridable, KS.NotOverridable, KS.Overrides, KS.Overloads, KS.Default, KS.ReadOnly, KS.WriteOnly, KS.MustOverride}

    '''' <summary>
    '''' PropertyModifier  ::=  ProcedureModifier  |  "Default"  |  "ReadOnly"  |  "WriteOnly"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly PropertyModifiers As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shared, KS.Overridable, KS.NotOverridable, KS.Overrides, KS.Overloads, KS.Default, KS.ReadOnly, KS.WriteOnly, KS.Shadows}

    '''' <summary>
    '''' InterfacePropertyModifier  ::=	"Shadows"  |	"Overloads"  |	"Default"  |	"ReadOnly"  |	"WriteOnly"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly InterfacePropertyModifier As KS() = New KS() {KS.Shadows, KS.Overloads, KS.Default, KS.ReadOnly, KS.WriteOnly}

    '''' <summary>
    '''' ConstructorModifier  ::=  AccessModifier  |  "Shared"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ConstructorModifiers As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shared}

    '''' <summary>
    '''' OperatorModifier  ::=  "Public"  | "Shared"  |  "Overloads"  |  "Shadows"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly OperatorModifiers As KS() = New KS() {KS.Public, KS.Shared, KS.Overloads, KS.Shadows}

    '''' <summary>
    '''' InterfaceEventModifiers  ::=  "Shadows"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly InterfaceEventModifiers As KS() = New KS() {KS.Shadows}

    '''' <summary>
    '''' InterfaceProcedureModifier  ::=  "Shadows" | "Overloads"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly InterfaceProcedureModifiers As KS() = New KS() {KS.Shadows, KS.Overloads}

    '''' <summary>
    '''' ConversionOperatorModifier  ::=  "Widening" | "Narrowing" |  ConversionModifier
    '''' LAMESPEC: ConversionModifier should be OperatorModifier
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ConversionOperatorModifiers As KS() = New KS() {KS.Widening, KS.Narrowing, KS.Public, KS.Shared, KS.Overloads, KS.Shadows}

    '''' <summary>
    '''' ConstructorModifier  ::=  AccessModifier  |  "Shared"
    '''' </summary>
    '''' <remarks></remarks>
    'Public ReadOnly ConstructorModifier As KS() = New KS() {KS.Public, KS.Friend, KS.Protected, KS.Private, KS.Shared}

    Private m_KSAttributes(KS.NumberOfItems) As KSEnumStringAttribute

    Sub New()
        Dim stringAttribute As KSEnumStringAttribute
        Dim fields() As Reflection.FieldInfo
        Dim EnumDeclaration As Type = GetType(KS)
        Dim attribType As Type = GetType(EnumStringAttribute)

        fields = EnumDeclaration.GetFields()
        For Each field As Reflection.FieldInfo In fields
            If field.IsSpecialName Then Continue For
            If System.Attribute.IsDefined(field, attribType) Then
                stringAttribute = DirectCast(System.Attribute.GetCustomAttribute(field, attribType), KSEnumStringAttribute)
                Dim value As KS
                value = DirectCast(field.GetValue(Nothing), KS)
                strSpecial(value) = stringAttribute.Value
                strSpecialFriendly(value) = stringAttribute.FriendlyValue

                If stringAttribute.IsKeyword Then
                    keywordLookup.Add(stringAttribute.Value, field.GetValue(Nothing))
                End If
            End If
        Next
    End Sub

    Function GetKSStringAttribute(ByVal Value As KS) As KSEnumStringAttribute
        If m_KSAttributes(Value) Is Nothing Then
            m_KSAttributes(Value) = DirectCast(System.Attribute.GetCustomAttribute(GetType(KS).GetField(Value.ToString), GetType(KSEnumStringAttribute)), KSEnumStringAttribute)
            'Return DirectCast(System.Attribute.GetCustomAttribute(GetType(KS).GetField(Value.ToString), GetType(KSEnumStringAttribute)), KSEnumStringAttribute)
        End If
        Return m_KSAttributes(Value)
    End Function

    Function GetStringAttribute(ByVal Value As [Enum]) As EnumStringAttribute
        Helper.Assert(Value IsNot Nothing)
        Helper.Assert(Value.GetType.GetField(Value.ToString) IsNot Nothing)
        Helper.Assert(System.Attribute.GetCustomAttribute(Value.GetType.GetField(Value.ToString), GetType(EnumStringAttribute)) IsNot Nothing)
        Return DirectCast(System.Attribute.GetCustomAttribute(Value.GetType.GetField(Value.ToString), GetType(EnumStringAttribute)), EnumStringAttribute)
    End Function

    Function GetKS(ByVal Name As String) As KS
        If keywordLookup.Contains(Name) Then
            Return CType(keywordLookup.Item(Name), KS)
        Else
            Return KS.None
        End If
    End Function
End Module