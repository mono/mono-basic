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