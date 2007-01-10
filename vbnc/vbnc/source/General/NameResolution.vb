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

'A constant expression is an expression whose value can be fully evaluated at compile time. The type of a constant expression can be Byte, Short, Integer, Long, Char, Single, Double, Decimal, Boolean, String, or any enumeration type. The following constructs are permitted in constant expressions: 
'Literals (including Nothing).
'References to constant type members or constant locals.
'References to members of enumeration types.
'Parenthesized subexpressions.
'Coercion expressions, provided the target type is one of the types listed above. Coercions to and from String are an exception to this rule and not allowed because String conversions are always done in the current culture of the execution environment at run time.
'The +, - and Not unary operators.
'The +, -, *, ^, Mod, /, \, <<, >>, &, And, Or, Xor, AndAlso, OrElse, =, <, >, <>, <=, and => binary operators, provided each operand is of a type listed above.
'The following run-time functions: 
'Microsoft.VisualBasic.Strings.ChrW
'Microsoft.VisualBasic.Strings.Chr, if the constant value is between 0 and 128
'Microsoft.VisualBasic.Strings.AscW, if the constant string is not empty
'Microsoft.VisualBasic.Strings.Asc, if the constant string is not empty

'Constant expressions of an integral type (Long, Integer, Short, Byte) can be implicitly converted to a narrower integral type, and constant expressions of type Double can be implicitly converted to Single, provided the value of the constant expression is within the range of the destination type. These narrowing conversions are allowed regardless of whether permissive or strict semantics are being used.

Imports System.Reflection

Public Class NameResolution
    Inherits Helper

    'Constant methods.
    'Private Shared m_Asc_Char As MethodInfo ', if the constant string is not empty
    'Private Shared m_Asc_String As MethodInfo ', if the constant string is not empty
    'Private Shared m_AscW_Char As MethodInfo ', if the constant string is not empty
    'Private Shared m_AscW_String As MethodInfo ', if the constant string is not empty
    'Private Shared m_Chr_Integer As MethodInfo ', if the constant value is between 0 and 128
    'Private Shared m_ChrW_Integer As MethodInfo
    'Private Shared m_AllConstantFunctions As ArrayList

    Public Shared StringComparer As System.StringComparer = System.StringComparer.OrdinalIgnoreCase
    Public Shared StringComparison As StringComparison = StringComparison.InvariantCultureIgnoreCase

    Sub New(ByVal Compiler As Compiler)
        MyBase.New(Compiler)
        LoadReferencedModules()
    End Sub

    Private Shared Function IsMethod(ByVal m1 As MethodInfo, ByVal Name As String, ByVal ParameterType As Type, ByVal ReturnType As Type) As Boolean
        If m1.IsGenericMethod Then Return False
        If m1.IsGenericMethodDefinition Then Return False

        If CompareNameOrdinal(m1.Name, Name) = False Then Return False

        If Helper.CompareType(m1.ReturnType, ReturnType) = False Then Return False

        Dim p1 As ParameterInfo()
        p1 = m1.GetParameters()
        If p1.Length <> 1 Then Return False

        If Helper.CompareType(p1(0).ParameterType, ParameterType) = False Then Return False

        Return True
    End Function

    Function IsConstantMethod(ByVal Method As MethodInfo, ByVal Parameter As Object, ByRef Result As Object) As Boolean
        If Method.MemberType <> MemberTypes.Method Then Return False
        If Not CompareName(Method.DeclaringType.Namespace, "Microsoft.VisualBasic", True) Then Return False
        If Not CompareName(Method.DeclaringType.Name, "Strings", True) Then Return False

#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine("IsConstantMethod: " & Method.Name & ", parameter=" & Parameter.ToString & ", parameter.gettype=" & Parameter.GetType.Name)
#End If
        Dim isConstant As Boolean
        If IsMethod(Method, "Chr", Compiler.TypeCache.Integer, Compiler.TypeCache.Char) Then
            If TypeOf Parameter Is Integer = False Then Return False
            Dim intParam As Integer = CInt(Parameter)
            'CHECK: Documentation says <= 128, vbc says < 128.
            isConstant = intParam >= 0 AndAlso intParam < 128
            If isConstant Then Result = Microsoft.VisualBasic.Strings.Chr(intParam)
        ElseIf IsMethod(Method, "ChrW", Compiler.TypeCache.Integer, Compiler.TypeCache.Char) Then
            Helper.Assert(TypeOf Parameter Is Integer)
            isConstant = True
            Result = Microsoft.VisualBasic.Strings.ChrW(CInt(Parameter))
        ElseIf IsMethod(Method, "Asc", Compiler.TypeCache.Char, Compiler.TypeCache.Integer) Then
            isConstant = TypeOf Parameter Is Char
            If isConstant Then Result = Microsoft.VisualBasic.Asc(CChar(Parameter))
        ElseIf IsMethod(Method, "AscW", Compiler.TypeCache.Char, Compiler.TypeCache.Integer) Then
            isConstant = TypeOf Parameter Is Char
            If isConstant Then Result = Microsoft.VisualBasic.AscW(CChar(Parameter))
        ElseIf IsMethod(Method, "Asc", Compiler.TypeCache.String, Compiler.TypeCache.Integer) Then
            isConstant = TypeOf Parameter Is String AndAlso CStr(Parameter) <> ""
            If isConstant Then Result = Microsoft.VisualBasic.Asc(CStr(Parameter))
        ElseIf IsMethod(Method, "AscW", Compiler.TypeCache.String, Compiler.TypeCache.Integer) Then
            isConstant = TypeOf Parameter Is String AndAlso CStr(Parameter) <> ""
            If isConstant Then Result = Microsoft.VisualBasic.AscW(CStr(Parameter))
        Else
            Return False
        End If

        Return isConstant
    End Function

    Private Sub LoadReferencedModules()
        ''Also load the StandardModule attribute from the loaded assembly, otherwise the types arent equal even though they actually are...
        ''A problem may occur if a user-created assembly has modules but Microsoft.VisualBasic isn't referenced...
        ''Might actually need to do name comparison of each custom attribute...
        'For Each ass As Assembly In Compiler.TypeManager.Assemblies
        '    If ass.GetName.Name = "Microsoft.VisualBasic" Then
        '        Dim tpStrings As Type
        '        tpStrings = ass.GetType("Microsoft.VisualBasic.Strings")
        '        Helper.Assert(tpStrings IsNot Nothing)
        '        m_ChrW_Integer = tpStrings.GetMethod("ChrW", New Type() {Compiler.TypeCache.Integer})
        '        m_Chr_Integer = tpStrings.GetMethod("Chr", New Type() {Compiler.TypeCache.Integer})
        '        m_AscW_String = tpStrings.GetMethod("AscW", New Type() {Compiler.TypeCache.String})
        '        m_AscW_Char = tpStrings.GetMethod("AscW", New Type() {Compiler.TypeCache.Char})
        '        m_Asc_String = tpStrings.GetMethod("Asc", New Type() {Compiler.TypeCache.String})
        '        m_Asc_Char = tpStrings.GetMethod("Asc", New Type() {Compiler.TypeCache.Char})
        '        m_AllConstantFunctions = New ArrayList(New MethodInfo() {m_ChrW_Integer, m_Chr_Integer, m_AscW_String, m_AscW_Char, m_Asc_String, m_Asc_Char})
        '    End If
        'Next
    End Sub

End Class
