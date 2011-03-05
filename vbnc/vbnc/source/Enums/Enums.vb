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
    ''' NumericTypeName  ::=  IntegralTypeName  |  FloatingPointTypeName  | "Decimal"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly NumericTypeNames As KS() = New KS() {KS.Single, KS.Double, KS.Byte, KS.SByte, KS.UShort, KS.Short, KS.UInteger, KS.Integer, KS.ULong, KS.Long, KS.Decimal}

    ''' <summary>
    ''' PrimitiveTypeName  ::=  NumericTypeName  | "Boolean" | "Date" | "Char" | "String"
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly PrimitiveTypeNames As KS() = New KS() {KS.Single, KS.Double, KS.Byte, KS.SByte, KS.UShort, KS.Short, KS.UInteger, KS.Integer, KS.ULong, KS.Long, KS.Decimal, KS.String, KS.Boolean, KS.Date, KS.Char}

    Class KW
        Public KeywordL As String
        Public KS As KS
    End Class

    Public m_Keywords(12) As Generic.Dictionary(Of Char, Generic.List(Of KW))

    Sub New()
        strSpecial(KS.LT) = "<"
        strSpecial(KS.GT) = ">"
        strSpecial(KS.Equals) = "="
        strSpecial(KS.NotEqual) = "<>"
        strSpecial(KS.LE) = "<="
        strSpecial(KS.GE) = ">="
        strSpecial(KS.Exclamation) = "!"
        strSpecial(KS.Concat) = "&"
        strSpecial(KS.Mult) = "*"
        strSpecial(KS.Add) = "+"
        strSpecial(KS.Minus) = "-"
        strSpecial(KS.Power) = "^"
        strSpecial(KS.RealDivision) = "/"
        strSpecial(KS.IntDivision) = "\"
        strSpecial(KS.Numeral) = "#"
        strSpecial(KS.LBrace) = "{"
        strSpecial(KS.RBrace) = "}"
        strSpecial(KS.LParenthesis) = "("
        strSpecial(KS.RParenthesis) = ")"
        strSpecial(KS.Dot) = "."
        strSpecial(KS.Comma) = ","
        strSpecial(KS.Colon) = ":"
        strSpecial(KS.ShiftLeft) = "<<"
        strSpecial(KS.ShiftRight) = ">>"
        strSpecial(KS.ConcatAssign) = "&="
        strSpecial(KS.AddAssign) = "+="
        strSpecial(KS.MinusAssign) = "-="
        strSpecial(KS.RealDivAssign) = "/="
        strSpecial(KS.IntDivAssign) = "\="
        strSpecial(KS.PowerAssign) = "^="
        strSpecial(KS.MultAssign) = "*="
        strSpecial(KS.ShiftLeftAssign) = "<<="
        strSpecial(KS.ShiftRightAssign) = ">>="

        For i As Integer = 0 To 12
            m_Keywords(i) = New Generic.Dictionary(Of Char, Generic.List(Of KW))
        Next

        AddKeyword("Ansi", KS.Ansi)
        AddKeyword("Auto", KS.Auto)
        AddKeyword("ByRef", KS.ByRef)
        AddKeyword("ByVal", KS.ByVal)
        AddKeyword("Const", KS.Const)
        AddKeyword("Default", KS.Default)
        AddKeyword("Dim", KS.Dim)
        AddKeyword("Friend", KS.Friend)
        AddKeyword("Inherits", KS.Inherits)
        AddKeyword("Narrowing", KS.Narrowing)
        AddKeyword("MustInherit", KS.MustInherit)
        AddKeyword("MustOverride", KS.MustOverride)
        AddKeyword("NotInheritable", KS.NotInheritable)
        AddKeyword("NotOverridable", KS.NotOverridable)
        AddKeyword("Optional", KS.Optional)
        AddKeyword("Overloads", KS.Overloads)
        AddKeyword("Overridable", KS.Overridable)
        AddKeyword("Overrides", KS.Overrides)
        AddKeyword("Partial", KS.Partial)
        AddKeyword("ParamArray", KS.ParamArray)
        AddKeyword("Private", KS.Private)
        AddKeyword("Protected", KS.Protected)
        AddKeyword("Public", KS.Public)
        AddKeyword("ReadOnly", KS.ReadOnly)
        AddKeyword("Shadows", KS.Shadows)
        AddKeyword("Shared", KS.Shared)
        AddKeyword("Static", KS.Static)
        AddKeyword("Unicode", KS.Unicode)
        AddKeyword("Widening", KS.Widening)
        AddKeyword("WithEvents", KS.WithEvents)
        AddKeyword("WriteOnly", KS.WriteOnly)
        AddKeyword("AddHandler", KS.AddHandler)
        AddKeyword("AddressOf", KS.AddressOf)
        AddKeyword("AndAlso", KS.AndAlso)
        AddKeyword("Alias", KS.Alias)
        AddKeyword("And", KS.And)
        AddKeyword("As", KS.As)
        AddKeyword("Boolean", KS.Boolean)
        AddKeyword("Byte", KS.Byte)
        AddKeyword("Call", KS.Call)
        AddKeyword("Case", KS.Case)
        AddKeyword("Catch", KS.Catch)
        AddKeyword("CBool", KS.CBool)
        AddKeyword("CByte", KS.CByte)
        AddKeyword("CChar", KS.CChar)
        AddKeyword("CDate", KS.CDate)
        AddKeyword("CDec", KS.CDec)
        AddKeyword("CDbl", KS.CDbl)
        AddKeyword("Char", KS.Char)
        AddKeyword("CInt", KS.CInt)
        AddKeyword("Class", KS.Class)
        AddKeyword("CLng", KS.CLng)
        AddKeyword("CObj", KS.CObj)
        AddKeyword("Continue", KS.Continue)
        AddKeyword("CSByte", KS.CSByte)
        AddKeyword("CShort", KS.CShort)
        AddKeyword("CSng", KS.CSng)
        AddKeyword("CStr", KS.CStr)
        AddKeyword("CUInt", KS.CUInt)
        AddKeyword("CULng", KS.CULng)
        AddKeyword("CUShort", KS.CUShort)
        AddKeyword("CType", KS.CType)
        AddKeyword("Date", KS.Date)
        AddKeyword("Decimal", KS.Decimal)
        AddKeyword("Declare", KS.Declare)
        AddKeyword("Delegate", KS.Delegate)
        AddKeyword("DirectCast", KS.DirectCast)
        AddKeyword("Do", KS.Do)
        AddKeyword("Double", KS.Double)
        AddKeyword("Each", KS.Each)
        AddKeyword("Else", KS.Else)
        AddKeyword("ElseIf", KS.ElseIf)
        AddKeyword("End", KS.End)
        AddKeyword("Enum", KS.Enum)
        AddKeyword("Erase", KS.Erase)
        AddKeyword("Error", KS.Error)
        AddKeyword("Event", KS.Event)
        AddKeyword("Exit", KS.Exit)
        AddKeyword("False", KS.False)
        AddKeyword("Finally", KS.Finally)
        AddKeyword("For", KS.For)
        AddKeyword("Function", KS.Function)
        AddKeyword("Get", KS.Get)
        AddKeyword("GetType", KS.GetType)
        AddKeyword("Global", KS.Global)
        AddKeyword("GoTo", KS.GoTo)
        AddKeyword("Handles", KS.Handles)
        AddKeyword("If", KS.If)
        AddKeyword("Implements", KS.Implements)
        AddKeyword("Imports", KS.Imports)
        AddKeyword("In", KS.In)
        AddKeyword("Integer", KS.Integer)
        AddKeyword("Interface", KS.Interface)
        AddKeyword("Is", KS.Is)
        AddKeyword("IsNot", KS.IsNot)
        AddKeyword("Let", KS.Let)
        AddKeyword("Lib", KS.Lib)
        AddKeyword("Like", KS.Like)
        AddKeyword("Long", KS.Long)
        AddKeyword("Loop", KS.Loop)
        AddKeyword("Me", KS.Me)
        AddKeyword("Mod", KS.Mod)
        AddKeyword("Module", KS.Module)
        AddKeyword("MyBase", KS.MyBase)
        AddKeyword("MyClass", KS.MyClass)
        AddKeyword("Namespace", KS.Namespace)
        AddKeyword("New", KS.New)
        AddKeyword("Next", KS.Next)
        AddKeyword("Not", KS.Not)
        AddKeyword("Nothing", KS.Nothing)
        AddKeyword("Object", KS.Object)
        AddKeyword("Of", KS.Of)
        AddKeyword("On", KS.On)
        AddKeyword("Operator", KS.Operator)
        AddKeyword("Option", KS.Option)
        AddKeyword("Or", KS.Or)
        AddKeyword("OrElse", KS.OrElse)
        AddKeyword("Property", KS.Property)
        AddKeyword("RaiseEvent", KS.RaiseEvent)
        AddKeyword("ReDim", KS.ReDim)
        AddKeyword("REM", KS.[REM])
        AddKeyword("RemoveHandler", KS.RemoveHandler)
        AddKeyword("Resume", KS.Resume)
        AddKeyword("Return", KS.Return)
        AddKeyword("SByte", KS.SByte)
        AddKeyword("Select", KS.Select)
        AddKeyword("Set", KS.Set)
        AddKeyword("Short", KS.Short)
        AddKeyword("Single", KS.Single)
        AddKeyword("Step", KS.Step)
        AddKeyword("Stop", KS.Stop)
        AddKeyword("String", KS.String)
        AddKeyword("Structure", KS.Structure)
        AddKeyword("Sub", KS.Sub)
        AddKeyword("SyncLock", KS.SyncLock)
        AddKeyword("Then", KS.Then)
        AddKeyword("Throw", KS.Throw)
        AddKeyword("To", KS.To)
        AddKeyword("True", KS.True)
        AddKeyword("Try", KS.Try)
        AddKeyword("TryCast", KS.TryCast)
        AddKeyword("TypeOf", KS.TypeOf)
        AddKeyword("UInteger", KS.UInteger)
        AddKeyword("ULong", KS.ULong)
        AddKeyword("UShort", KS.UShort)
        AddKeyword("Using", KS.Using)
        AddKeyword("Until", KS.Until)
        AddKeyword("Variant", KS.Variant)
        AddKeyword("When", KS.When)
        AddKeyword("While", KS.While)
        AddKeyword("With", KS.With)
        AddKeyword("Xor", KS.Xor)
    End Sub

    Private Sub AddKeyword(ByVal keyword As String, ByVal ks As KS)
        Dim list As Generic.List(Of KW) = Nothing
        Dim kw As New KW()
        kw.KeywordL = keyword.ToLowerInvariant()
        kw.KS = ks

        If Not m_Keywords(keyword.Length - 2).TryGetValue(keyword(0), list) Then
            list = New Generic.List(Of KW)
            m_Keywords(keyword.Length - 2)(keyword(0)) = list
        End If

        list.Add(kw)

        strSpecial(ks) = keyword
    End Sub

    Public Function GetKS(ByVal value As Char(), ByVal length As Integer) As KS
        Dim list As Generic.List(Of KW) = Nothing
        Dim ch0 As Char

        If length < 2 OrElse length > 14 Then Return KS.None
        ch0 = value(0)
        If ch0 >= "a"c AndAlso ch0 <= "z"c Then ch0 = VB.Chr(VB.Asc(ch0) - 32)

        If Not m_Keywords(length - 2).TryGetValue(ch0, list) Then Return KS.None

        If list Is Nothing Then Return KS.None

        For i As Integer = 0 To list.Count - 1
            Dim kwL As String = list(i).KeywordL
            For c As Integer = 1 To length - 1
                Dim kc As Char = value(c)

                If kc >= "A"c AndAlso kc <= "Z"c Then kc = VB.Chr(VB.Asc(kc) + 32)

                If kwL(c) = kc Then
                    If c = length - 1 Then Return list(i).KS
                    Continue For
                End If
                Exit For
            Next
        Next

        Return KS.None
    End Function
End Module