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

#If GENERATOR Then

Namespace Z
    Public Class Constants
        Public Shared BUILTINTYPES As Type() = New Type() {GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort), GetType(Integer), GetType(UInteger), GetType(Long), GetType(ULong), GetType(Decimal), GetType(Single), GetType(Double), GetType(String), GetType(Boolean), GetType(Char), GetType(Date), GetType(Object)}
        Public Shared BUILTINTYPESCODES As TypeCode() = New TypeCode() {TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double, TypeCode.String, TypeCode.Boolean, TypeCode.Char, TypeCode.DateTime, TypeCode.Object}

        Public Shared TYPES() As String = {"Byte", "SByte", "Short", "UShort", "Integer", "UInteger", "Long", "ULong", "Decimal", "Single", "Double", "String", "Boolean", "Char", "Date", "Object"}
        Public Shared TYPEVALUES() As String = {"CByte(10)", "CSByte(20)", "30S", "40US", "50I", "60UI", "70L", "80UL", "90.09D", "CSng(100.001)", "110.011", """testvalue""", "True", """C""c", "#01/01/2000 12:34#", "Nothing"}
        Public Shared TYPEVALUESREAL() As Object = {CByte(10), CSByte(20), 30S, 40US, 50I, 60UI, 70L, 80UL, 90.09D, CSng(100.001), 110.011, "testvalue", True, "C"c, #1/1/2000 12:34:00 PM#, Nothing}
        Public Shared TYPEVALUES2() As String = {"CByte(11)", "CSByte(21)", "31S", "41US", "51I", "61UI", "71L", "81UL", "91.09D", "CSng(101.001)", "111.011", """failed""", "False", """X""c", "#12/31/1999 12:34#", """Something"""}
        Public Shared TYPEVALUES2REAL() As Object = {CByte(11), CSByte(21), 31S, 41US, 51I, 61UI, 71L, 81UL, 91.09D, CSng(101.001), 111.011, "failed", False, "X"c, #12/31/1999 12:34:00 PM#, "Something"}
        Public Shared TYPEVALUES3() As String = {"CByte(1)", "CSByte(1)", "1S", "1US", "1I", "1UI", "1L", "1UL", "1D", "1!", "1", """1""", "True", """1""c", "#01/01/2000 12:34#", "CObj(1)"}
        Public Shared TYPEVALUES3REAL() As Object = {CByte(1), CSByte(1), 1S, 1US, 1I, 1UI, 1L, 1UL, 1D, 1.0!, 1, "1", True, "1"c, #1/1/2000 12:34:00 PM#, Nothing}
        Public Shared STRINGTYPEVALUES() As String = {"""1""", """2""", """3""", """4""", """5""", """6""", """7""", """8""", """9.1""", """10.1""", """11.1""", """1""", "True", """1""", """#01/01/2000 12:34#""", """Nothing"""}

        Public Shared CONVERSIONS() As String = {"CByte", "CSByte", "CShort", "CUShort", "CInt", "CUInt", "CLng", "CULng", "CDec", "CSng", "CDbl", "CStr", "CBool", "CChar", "CDate", "CObj"}

        Public Shared BINARYOPERATORS() As String = {"=", "+", "-", "*", "/", "\", "^", "Mod", "And", "AndAlso", "Or", "OrElse", "XOr", ">", ">>", "<", "<<", "<=", ">=", "<>", "&", "Like"}
        Public Shared BINARYOPERATORSKS() As KS = {KS.Equals, KS.Add, KS.Minus, KS.Mult, KS.RealDivision, KS.IntDivision, KS.Power, KS.Mod, KS.And, KS.AndAlso, KS.Or, KS.OrElse, KS.Xor, KS.GT, KS.ShiftRight, KS.LT, KS.ShiftLeft, KS.LE, KS.GE, KS.NotEqual, KS.Concat, KS.Like}
        Public Shared BINARYOPERATORSNAME() As String = {"Equals", "Add", "Minus", "Multiplication", "RealDivision", "IntegerDivision", "Power", "Mod", "And", "AndAlso", "Or", "OrElse", "XOr", "GreaterThan", "RightShift", "LessThan", "LeftShift", "LessThanOrEqual", "GreaterThanOrEqual", "NotEqual", "Concat", "Like"}

        Public Shared UNARYOPERATORS() As String = {"Not", "-", "+"}
        Public Shared UNARYOPERATORSKS() As KS = {KS.Not, KS.Minus, KS.Add}
        Public Shared UNARYOPERATORSNAME() As String = {"Not", "Minus", "Add"}

        Public Shared ASSIGNMENTOPERATORS() As String = {"+=", "-=", "*=", "/=", "\=", "^=", ">>=", "<<=", "&="}
        Public Shared ASSIGNMENTOPERATORSKS() As KS = {KS.AddAssign, KS.MinusAssign, KS.MultAssign, KS.RealDivAssign, KS.IntDivAssign, KS.PowerAssign, KS.ShiftRightAssign, KS.ShiftLeftAssign, KS.ConcatAssign}
        Public Shared ASSIGNMENTOPERATORSNAME() As String = {"Add", "Minus", "Multiplication", "RealDivision", "IntegerDivision", "Power", "RightShift", "LeftShift", "Concat"}

        Shared Sub New()
            ReDim BUILTINTYPESCODES(BUILTINTYPES.Length - 1)
            For i As Integer = 0 To BUILTINTYPESCODES.Length - 1
                BUILTINTYPESCODES(i) = Type.GetTypeCode(BUILTINTYPES(i))
            Next
        End Sub

        Shared Function GetName() As String
            Return (New System.Diagnostics.StackFrame(1)).GetMethod().DeclaringType.Name
        End Function

        Shared Function GetBasePath() As String
            Return System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location))
        End Function

        Shared Sub WriteFile(ByVal Path As String, ByVal File As String, ByVal Contents As String)
            'Static license As String
            'If license Is Nothing Then license = IO.File.ReadAllText(license)
            Dim FileName As String = IO.Path.Combine(Path, File)
            If IO.Directory.Exists(Path) = False Then IO.Directory.CreateDirectory(Path)
            If IO.Directory.Exists(IO.Path.Combine(Path, "testoutput")) = False Then IO.Directory.CreateDirectory(IO.Path.Combine(Path, "testoutput"))
            'If Contents.Contains(license) = False Then Contents = license & Contents
            IO.File.WriteAllText(FileName, Contents)
        End Sub
    End Class
End Namespace

#End If
