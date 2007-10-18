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

Imports Microsoft.VisualBasic
#If GENERATOR Then

Namespace Z
    Module Generator
        Private BUILTINTYPES As New ArrayList(New Type() {GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort), GetType(Integer), GetType(UInteger), GetType(Long), GetType(ULong), GetType(Decimal), GetType(Single), GetType(Double), GetType(String), GetType(Boolean), GetType(Char), GetType(Date), GetType(Object)})

        Private TYPES() As String = {"Byte", "SByte", "Short", "UShort", "Integer", "UInteger", "Long", "ULong", "Decimal", "Single", "Double", "String", "Boolean", "Char", "Date", "Object"}
        Private TYPES2() As Type = {GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort), GetType(Integer), GetType(UInteger), GetType(Long), GetType(ULong), GetType(Decimal), GetType(Single), GetType(Double), GetType(String), GetType(Boolean), GetType(Char), GetType(Date), GetType(Object)}
        Private TYPEVALUES() As String = {"CByte(10)", "CSByte(20)", "30S", "40US", "50I", "60UI", "70L", "80UL", "90.09D", "100.001!", "110.011", """testvalue""", "True", """C""c", "#01/01/2000 12:34#", "Nothing"}
        Private TYPEVALUES2() As String = {"CByte(11)", "CSByte(21)", "31S", "41US", "51I", "61UI", "71L", "81UL", "91.09D", "101.001!", "111.011", """failed""", "False", """X""c", "#12/31/1999 12:34#", """Something"""}
        Private TYPEVALUES3() As String = {"CByte(1)", "CSByte(1)", "1S", "1US", "1I", "1UI", "1L", "1UL", "1D", "1!", "1", """1""", "True", """1""c", "#01/01/2000 12:34#", "Nothing"}
        Private CONVERSIONS() As String = {"CByte", "CSByte", "CShort", "CUShort", "CInt", "CUInt", "CLng", "CULng", "CDec", "CSng", "CDbl", "CStr", "CBool", "CChar", "CDate", "CObj"}

        Private BINARYOPERATORS() As String = {"=", "+", "-", "*", "/", "\", "^", "Mod", "And", "AndAlso", "Or", "OrElse", "XOr", ">", ">>", "<", "<<", "<=", ">=", "<>", "&", "Like"}
        Private BINARYOPERATORSKS() As KS = {KS.Equals, KS.Add, KS.Minus, KS.Mult, KS.RealDivision, KS.IntDivision, KS.Power, KS.Mod, KS.And, KS.AndAlso, KS.Or, KS.OrElse, KS.Xor, KS.GT, KS.ShiftRight, KS.LT, KS.ShiftLeft, KS.LE, KS.GE, KS.NotEqual, KS.Concat, KS.Like}
        Private BINARYOPERATORSNAME() As String = {"Equals", "Add", "Minus", "Multiplication", "RealDivision", "IntegerDivision", "Power", "Mod", "And", "AndAlso", "Or", "OrElse", "XOr", "GreaterThan", "RightShift", "LessThan", "LeftShift", "LessThanOrEqual", "GreaterThanOrEqual", "NotEqual", "Concat", "Like"}

        Private UNARYOPERATORS() As String = {"Not", "-", "+"}
        Private UNARYOPERATORSKS() As KS = {KS.Not, KS.Minus, KS.Add}
        Private UNARYOPERATORSNAME() As String = {"Not", "Minus", "Add"}

        Private ASSIGNMENTOPERATORS() As String = {"+=", "-=", "*=", "/=", "\=", "^=", ">>=", "<<=", "&="}
        Private ASSIGNMENTOPERATORSKS() As KS = {KS.AddAssign, KS.MinusAssign, KS.MultAssign, KS.RealDivAssign, KS.IntDivAssign, KS.PowerAssign, KS.ShiftRightAssign, KS.ShiftLeftAssign, KS.ConcatAssign}
        Private ASSIGNMENTOPERATORSNAME() As String = {"Add", "Minus", "Multiplication", "RealDivision", "IntegerDivision", "Power", "RightShift", "LeftShift", "Concat"}

        Private Const BASEPATH As String = "Z:\mono\head\mono-basic\vbnc\"
        Private Const LICENSE As String = BASEPATH & "\License FileHeader.txt"
        Private Const BASEPATHTESTS As String = BASEPATH & "\vbnc\tests\Generated\"


        ''' <summary>
        ''' This method generates all auto generated tests.
        ''' </summary>
        ''' <remarks></remarks>
        Sub Generate()
            Try
                Try
                    DeleteFiles(BASEPATHTESTS)
                Catch
                End Try
                GenOperators()
                GenConversions()
                GenConversions2()
                GenByRefs()
                GenArrayElements()
                GenSelfTest()
            Catch ex As Exception
                MsgBox(ex.Message & VB.vbNewLine & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        ''' Deletes all vb, pdb, exe and dll files in the directory and subdirectories.
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <remarks></remarks>
        Sub DeleteFiles(ByVal Path As String)
            Using p As New System.Diagnostics.Process()
                p.StartInfo.FileName = System.Environment.ExpandEnvironmentVariables("%COMSPEC%")
                p.StartInfo.WorkingDirectory = Path
                p.StartInfo.Arguments = "/C del /S *.vb *.pdb *.dll *.exe *.exceptions.output.xml"
                p.Start()
                p.WaitForExit()
            End Using
        End Sub

        Sub WriteFile(ByVal Path As String, ByVal File As String, ByVal Contents As String)
            'Static license As String
            'If license Is Nothing Then license = IO.File.ReadAllText(license)
            Dim FileName As String = IO.Path.Combine(Path, File)
            If IO.Directory.Exists(Path) = False Then IO.Directory.CreateDirectory(Path)
            If IO.Directory.Exists(IO.Path.Combine(Path, "testoutput")) = False Then IO.Directory.CreateDirectory(IO.Path.Combine(Path, "testoutput"))
            'If Contents.Contains(license) = False Then Contents = license & Contents
            IO.File.WriteAllText(FileName, Contents)
        End Sub

        Sub WriteFile(ByVal FileName As String, ByVal Contents As String)
            WriteFile(IO.Path.GetDirectoryName(FileName), IO.Path.GetFileName(FileName), Contents)
        End Sub

        Sub GenUserOperators()
            Const PATH As String = BASEPATHTESTS & "UserOperators"
            Dim code As System.Text.StringBuilder
            Dim testname As String

            For i As Integer = 0 To BINARYOPERATORS.GetUpperBound(0)
                Dim binaryoperator As String = BINARYOPERATORS(i)
                Dim binaryoperatorname As String = BINARYOPERATORSNAME(i)

                If Char.IsLetterOrDigit(binaryoperator.Chars(0)) Then Continue For

                testname = binaryoperatorname & "Operator1"
                code = New Text.StringBuilder()
                code.AppendLine("Module " & testname)
                code.AppendLine(vbTab & "Class Operand")
                code.AppendLine(vbTab & vbTab & "Public Number as Integer")
                code.AppendLine(vbTab & vbTab & "Shared Operator " & binaryoperator & "(ByVal op1 As Operand, ByVal op2 As Operand) As Integer")
                code.AppendLine(vbTab & vbTab & vbTab & "Return op1.Number + op2.Number")
                code.AppendLine(vbTab & vbTab & "End Operator")
                code.AppendLine(vbTab & "End Class")
                code.AppendLine(vbTab & "Class Consumer")
                code.AppendLine(vbTab & vbTab & "Shared Function Main() As Integer")
                code.AppendLine(vbTab & vbTab & vbTab & "Dim o1 As New Operand, o2 As New Operand")
                code.AppendLine(vbTab & vbTab & vbTab & "Dim i As Integer")
                code.AppendLine(vbTab & vbTab & vbTab & "")
                code.AppendLine(vbTab & vbTab & vbTab & "o1.Number = 1")
                code.AppendLine(vbTab & vbTab & vbTab & "o2.Number = 2")
                code.AppendLine(vbTab & vbTab & vbTab & "")
                code.AppendLine(vbTab & vbTab & vbTab & "i = o1 " & binaryoperator & " o2")
                code.AppendLine(vbTab & vbTab & vbTab & "")
                code.AppendLine(vbTab & vbTab & vbTab & "If i = 3 Then")
                code.AppendLine(vbTab & vbTab & vbTab & vbTab & "Return 0")
                code.AppendLine(vbTab & vbTab & vbTab & "Else")
                code.AppendLine(vbTab & vbTab & vbTab & vbTab & "Return 1")
                code.AppendLine(vbTab & vbTab & vbTab & "End If")
                code.AppendLine(vbTab & vbTab & "End Function")
                code.AppendLine(vbTab & "End Class")
                code.AppendLine("End Module")

                WriteFile(PATH, testname & ".vb", code.ToString)
            Next
        End Sub

        Sub GenOperators()
            Const path As String = BASEPATHTESTS & "Operators"
            Dim code As New System.Text.StringBuilder(2000)
            Dim errname As String = ""

            For i As Integer = 0 To VB.UBound(TYPES)
                Dim lefttype As String = TYPES(i)
                Dim lefttypecode As TypeCode = Helper.GetTypeCode(Nothing, TYPES2(i))

                For k As Integer = 0 To VB.UBound(UNARYOPERATORS)
                    Dim unaryop As String = UNARYOPERATORS(k)
                    Dim unaryopname As String = UNARYOPERATORSNAME(k)
                    Dim unaryname As String = "UnaryOperator_" & unaryopname & "_" & lefttype

                    Dim resulttypecode As TypeCode = TypeConverter.GetUnaryResultType(UNARYOPERATORSKS(k), lefttypecode)
                    Dim resulttype As String

                    If resulttypecode = TypeCode.Empty Then
                        resulttype = "Object" 'This test is supposed to fail.
                        errname = "30487 "
                    Else
                        errname = ""
                        resulttype = TypeCodeToType(resulttypecode).FullName
                    End If
                    code.Length = 0
                    code.AppendLine("Public Module " & unaryname & "1")
                    code.AppendLine(vbTab & "Public Function Main() As Integer")
                    code.AppendLine(vbTab & vbTab & "Dim leftvalue As " & lefttype)
                    code.AppendLine(vbTab & vbTab & "Dim result As " & resulttype)
                    code.AppendLine()
                    code.AppendLine(vbTab & vbTab & "leftvalue = " & TYPEVALUES3(i))
                    code.AppendLine()
                    code.AppendLine(vbTab & vbTab & "result = " & unaryop & " leftvalue")
                    code.AppendLine(vbTab & "End Function")
                    code.AppendLine("End Module")
                    WriteFile(IO.Path.Combine(path, "Unary"), errname & unaryname & "1.vb", code.ToString)
                Next

                For j As Integer = 0 To VB.UBound(TYPES)
                    Dim righttype As String = TYPES(j)
                    Dim righttypecode As TypeCode = Helper.GetTypeCode(Nothing, TYPES2(j))
                    For k As Integer = 0 To VB.UBound(BINARYOPERATORS)
                        Dim binaryop As String = BINARYOPERATORS(k)
                        Dim binaryopname As String = BINARYOPERATORSNAME(k)
                        Dim binaryname As String = "BinaryOperator_" & binaryopname & "_" & lefttype & "_" & righttype
                        Dim resulttypecode As TypeCode = TypeConverter.GetBinaryResultType(BINARYOPERATORSKS(k), lefttypecode, righttypecode)
                        Dim resulttype As String

                        errname = ""
                        If resulttypecode = TypeCode.Empty Then
                            resulttype = "Object" 'This test is supposed to fail.
                            If (binaryop = "<<" OrElse binaryop = ">>") AndAlso lefttype <> "Date" AndAlso lefttype <> "Char" Then
                                If righttype = "Date" Then
                                    errname = "30311 "
                                ElseIf righttype = "Char" Then
                                    errname = "32006 "
                                End If
                            End If
                            If errname = "" Then errname = "30452 "
                        Else
                            resulttype = TypeCodeToType(resulttypecode).FullName
                        End If
                        code.Length = 0
                        code.AppendLine("Public Module " & binaryname & "1")
                        code.AppendLine(vbTab & "Public Function Main() As Integer")
                        code.AppendLine(vbTab & vbTab & "Dim leftvalue As " & lefttype)
                        code.AppendLine(vbTab & vbTab & "Dim rightvalue As " & righttype)
                        code.AppendLine(vbTab & vbTab & "Dim result As " & resulttype)
                        code.AppendLine()
                        code.AppendLine(vbTab & vbTab & "leftvalue = " & TYPEVALUES3(i))
                        code.AppendLine(vbTab & vbTab & "rightvalue = " & TYPEVALUES3(j))
                        code.AppendLine()
                        code.AppendLine(vbTab & vbTab & "result = leftvalue " & binaryop & " rightvalue")
                        code.AppendLine(vbTab & "End Function")
                        code.AppendLine("End Module")
                        WriteFile(IO.Path.Combine(path, "Binary"), errname & binaryname & "1.vb", code.ToString)
                    Next

                    For k As Integer = 0 To VB.UBound(ASSIGNMENTOPERATORS)
                        Dim assignmentop As String = ASSIGNMENTOPERATORS(k)
                        Dim assignmentopname As String = ASSIGNMENTOPERATORSNAME(k)
                        Dim assignmentname As String = "AssignmentOperator_" & assignmentopname & "Assign_" & lefttype & "_" & righttype
                        Dim resulttypecode As TypeCode = TypeConverter.GetBinaryResultType(BINARYOPERATORSKS(k), lefttypecode, righttypecode)
                        Dim resulttype As String

                        If resulttypecode = TypeCode.Empty Then
                            resulttype = "Object" 'This test is supposed to fail.
                            errname = "30452 "
                        Else
                            errname = ""
                            resulttype = lefttype
                        End If
                        code.Length = 0
                        code.AppendLine("Public Module " & assignmentname & "1")
                        code.AppendLine(vbTab & "Public Function Main() As Integer")
                        code.AppendLine(vbTab & vbTab & "Dim rightvalue As " & righttype)
                        code.AppendLine(vbTab & vbTab & "Dim result As " & lefttype)
                        code.AppendLine()
                        code.AppendLine(vbTab & vbTab & "result = " & TYPEVALUES3(i))
                        code.AppendLine(vbTab & vbTab & "rightvalue = " & TYPEVALUES3(j))
                        code.AppendLine()
                        code.AppendLine(vbTab & vbTab & "result " & assignmentop & " rightvalue")
                        code.AppendLine(vbTab & "End Function")
                        code.AppendLine("End Module")
                        WriteFile(IO.Path.Combine(path, "Assignment"), errname & assignmentname & "1.vb", code.ToString)
                    Next
                Next
            Next
        End Sub

        Function TypeCodeToType(ByVal Code As TypeCode) As Type

            Select Case Code
                Case TypeCode.Boolean
                    Return GetType(Boolean)
                Case TypeCode.Byte
                    Return GetType(Byte)
                Case TypeCode.Char
                    Return GetType(Char)
                Case TypeCode.DateTime
                    Return GetType(Date)
                Case TypeCode.DBNull
                    Throw New InternalException("")
                Case TypeCode.Decimal
                    Return GetType(Decimal)
                Case TypeCode.Double
                    Return GetType(Double)
                Case TypeCode.Empty
                    Throw New InternalException("")
                Case TypeCode.Int16
                    Return GetType(Short)
                Case TypeCode.Int32
                    Return GetType(Integer)
                Case TypeCode.Int64
                    Return GetType(Long)
                Case TypeCode.Object
                    Return GetType(Object)
                Case TypeCode.SByte
                    Return GetType(SByte)
                Case TypeCode.Single
                    Return GetType(Single)
                Case TypeCode.String
                    Return GetType(String)
                Case TypeCode.UInt16
                    Return GetType(UShort)
                Case TypeCode.UInt32
                    Return GetType(UInteger)
                Case TypeCode.UInt64
                    Return GetType(ULong)
                Case Else
                    Throw New InternalException("")
            End Select
        End Function

        Sub GenConversions()
            GenConversions(True)
            GenConversions(False)
        End Sub

        Sub GenConversions(ByVal Explicit As Boolean)
            Const path As String = BASEPATHTESTS & "Conversions"
            For i As Integer = 0 To TYPES.GetUpperBound(0)
                For j As Integer = 0 To TYPES.GetUpperBound(0)
                    Dim tp1, tp2, conv As String
                    Dim val1 As String
                    If i = j Then Continue For
                    tp1 = TYPES(i)
                    tp2 = TYPES(j)
                    val1 = TYPEVALUES(j)

                    Dim Test As System.Text.StringBuilder = New System.Text.StringBuilder
                    Dim name As String
                    If Explicit Then
                        name = "ExplicitConversion" & tp2 & "To" & tp1 & "1"
                        conv = CONVERSIONS(i)
                    Else
                        name = "ImplicitConversion" & tp2 & "To" & tp1 & "1"
                        conv = ""
                    End If
                    Test.AppendLine("Module " & name)
                    Test.AppendLine("    Function Main() As Integer")
                    Test.AppendLine("        Dim result As Boolean")
                    Test.AppendLine("        Dim value1 As " & tp2)
                    Test.AppendLine("        Dim value2 As " & tp1)
                    Test.AppendLine("        Dim const2 As " & tp1)
                    Test.AppendLine("")
                    Test.AppendLine("        value1 = " & val1)
                    Test.AppendLine("        value2 = " & conv & VB.IIf(Explicit, "(value1)", "value1").ToString)
                    Test.AppendLine("        const2 = " & conv & VB.IIf(Explicit, "(" & val1 & ")", val1).ToString)
                    Test.AppendLine("")
                    If tp1 = "Object" Then
                        Test.AppendLine("        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(value2, const2, False)")
                    Else
                        Test.AppendLine("        result = value2 = const2")
                    End If
                    Test.AppendLine("")
                    Test.AppendLine("        If result = False Then")
                    Test.AppendLine("            System.Console.WriteLine(""FAIL " & name & """)")
                    Test.AppendLine("            Return 1")
                    Test.AppendLine("        End If")
                    Test.AppendLine("    End Function")
                    Test.AppendLine("End Module")
                    Test.AppendLine()

                    If IO.Directory.Exists(path) = False OrElse IO.Directory.GetFiles(path, "3*" & name & ".vb").Length = 0 Then
                        'Only write the file if it is a correct conversion.
                        WriteFile(path, name & ".vb", Test.ToString)
                    End If
                Next
            Next
        End Sub

        Sub GenConversions2()
            GenConversions2(True)
            GenConversions2(False)
        End Sub

        Sub GenConversions2(ByVal Explicit As Boolean)
            Const path As String = BASEPATHTESTS & "Conversions"
            Dim file As New System.Text.StringBuilder
            Dim name As String

            If Explicit Then
                name = "ExplicitConversion1"
            Else
                name = "ImplicitConversion1"
            End If

            file.AppendLine("Module " & name)

            For i As Integer = 0 To TYPES.GetUpperBound(0)
                For j As Integer = 0 To TYPES.GetUpperBound(0)
                    Dim tp1, tp2, conv As String
                    Dim val1 As String
                    If i = j Then Continue For
                    Dim funcname As String
                    tp1 = TYPES(i)
                    tp2 = TYPES(j)
                    val1 = TYPEVALUES(j)

                    Dim Test As System.Text.StringBuilder = New System.Text.StringBuilder
                    If Explicit Then
                        funcname = "ExplicitConversion" & tp2 & "To" & tp1 & "1"
                        conv = CONVERSIONS(i)
                    Else
                        funcname = "ImplicitConversion" & tp2 & "To" & tp1 & "1"
                        conv = ""
                    End If
                    Test.AppendLine("    Function " & funcname & "() As Integer")
                    Test.AppendLine("        Dim result As Boolean")
                    Test.AppendLine("        Dim value1 As " & tp2)
                    Test.AppendLine("        Dim value2 As " & tp1)
                    Test.AppendLine("        Dim const2 As " & tp1)
                    Test.AppendLine("")
                    Test.AppendLine("        value1 = " & val1)
                    Test.AppendLine("        value2 = " & conv & VB.IIf(Explicit, "(value1)", "value1").ToString)
                    Test.AppendLine("        const2 = " & conv & VB.IIf(Explicit, "(" & val1 & ")", val1).ToString)
                    Test.AppendLine("")
                    Test.AppendLine("        result = value2 = const2")
                    Test.AppendLine("")
                    Test.AppendLine("        If result = False Then")
                    Test.AppendLine("            System.Console.WriteLine(""FAIL " & funcname & """)")
                    Test.AppendLine("            Return 1")
                    Test.AppendLine("        End If")
                    Test.AppendLine("    End Function")
                    file.Append(Test)
                Next
            Next

            file.AppendLine("End Module")
            file.AppendLine()

            WriteFile(path, name & ".vb", file.ToString)
        End Sub

        Sub GenByRefs()
            Const path As String = BASEPATHTESTS & "ByRefs"
            Dim all As New System.Text.StringBuilder
            Dim test As System.Text.StringBuilder
            For i As Integer = 0 To VB.UBound(TYPES)
                test = New System.Text.StringBuilder
                Dim type As String = TYPES(i)
                Dim value As String = TYPEVALUES(i)
                Dim value2 As String = TYPEVALUES2(i)
                Dim name As String = "ByRef" & type & "1"
                test.AppendLine("Module " & name)
                test.AppendLine("    Function Main() As Integer")
                test.AppendLine("        Dim result As Boolean")
                test.AppendLine("        Dim testvalue As " & type)
                test.AppendLine("")
                test.AppendLine("        testvalue = " & value2)
                test.AppendLine("        Tester(testvalue)")
                test.AppendLine("        result = testvalue = " & value)
                test.AppendLine("")
                test.AppendLine("        If result = False Then")
                test.AppendLine("            System.Console.WriteLine(""FAIL " & name & """)")
                test.AppendLine("            Return 1")
                test.AppendLine("        End If")
                test.AppendLine("    End Function")
                test.AppendLine()
                test.AppendLine("    Sub Tester(ByRef value As " & type & ")")
                test.AppendLine("        value = " & value)
                test.AppendLine("    End Sub")
                test.AppendLine("End Module")
                test.AppendLine()

                WriteFile(path, name & ".vb", test.ToString)
                all.Append(test.ToString)
            Next
        End Sub

        Sub GenArrayElements()
            Const path As String = BASEPATHTESTS & "ArrayElements"
            Dim test As System.Text.StringBuilder
            For i As Integer = 0 To VB.UBound(TYPES)
                test = New System.Text.StringBuilder
                Dim type As String = TYPES(i)
                Dim value As String = TYPEVALUES(i)
                Dim value2 As String = TYPEVALUES2(i)
                Dim name As String = "Array" & type & "Elements1"
                test.AppendLine("Module " & name)
                test.AppendLine("    Function Main() As Integer")
                test.AppendLine("        Dim result As Boolean")
                test.AppendLine("        Dim testvalue(" & i.ToString & ") As " & type)
                test.AppendLine("")
                test.AppendLine("        testvalue(" & i.ToString & ") = " & value)
                test.AppendLine("        result = testvalue(" & i.ToString & ") = " & value)
                test.AppendLine("")
                test.AppendLine("        If result = False Then")
                test.AppendLine("            System.Console.WriteLine(""FAIL " & name & """)")
                test.AppendLine("            Return 1")
                test.AppendLine("        End If")
                test.AppendLine("    End Function")
                test.AppendLine()
                test.AppendLine("End Module")
                test.AppendLine()

                WriteFile(path, name & ".vb", test.ToString)
            Next
        End Sub

        Sub GenSelfTest()
            'Extracts all code files from the vbproj file.
            Dim PROJFILENAMEs As String = BASEPATH & "vbnc\source\vbnc.vbproj"
            Dim RSPFILENAMEs As String = BASEPATH & "vbnc\tests\SelfTest\SelfCompile.files.windows"
            Dim RSPFILENAMELINUX As String = BASEPATH & "vbnc\tests\SelfTest\SelfCompile.files.linux"
            Dim newPaths As String = "..\..\source\"

            Dim projFileName As String
            Dim rspFileName As String
            projFileName = PROJFILENAMEs
            rspFileName = RSPFILENAMEs

            If IO.File.Exists(projFileName) = False Then
                Return
            End If

            Dim files As New Text.StringBuilder
            Using x As New Xml.XmlTextReader(projFileName)
                Dim i As Integer
                While x.Read()
                    Dim filename As String
                    Dim prefix As String
                    prefix = ""
                    filename = ""
                    If x.Name = "Compile" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                        x.MoveToAttribute("Include")
                        filename = x.Value
                    ElseIf x.Name = "EmbeddedResource" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                        x.MoveToAttribute("Include")
                        filename = x.Value
                        prefix = "/res:"
                    End If
                    If filename <> "" Then
                        filename = IO.Path.Combine(newPaths, x.Value)
                        filename = filename.Replace("%29", ")")
                        filename = filename.Replace("%28", "(")
                        files.AppendLine("""" & prefix & filename & """")
                        i += 1
                    End If
                End While
                'Diagnostics.Debug.WriteLine("Total: " & i.ToString)
                files.AppendLine("# Total: " & i.ToString & " files.")
            End Using
            WriteFile(rspFileName, files.ToString)

            Dim file As String = IO.Path.Combine(IO.Path.GetDirectoryName(RSPFILENAMEs), "SelfCompileWindows.vb")
            Dim text As String = "'Created version " & Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString & VB.vbNewLine
            If IO.File.ReadAllText(file).Contains(text) = False Then
                IO.File.AppendAllText(file, text)
            End If
            Try
                IO.File.WriteAllText(RSPFILENAMELINUX, IO.File.ReadAllText(RSPFILENAMEs).Replace("\", "/"))
            Catch
            End Try
        End Sub

        Sub GenRuntimeTest()
            'Extracts all code files from the vbproj file.
            Dim PATH As String = IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(BASEPATH))
            Dim PROJFILENAMEs As String = PATH & "\mono-basic\vbruntime\Microsoft.VisualBasic\2005VB.vbproj"
            Dim RSPFILENAMEs As String = BASEPATH & "\vbnc\tests\VBRunTime\Microsoft.VisualBasic.files.windows"
            Dim RSPFILENAMELINUX As String = BASEPATH & "\vbnc\tests\VBRunTime\Microsoft.VisualBasic.files.linux"
            Dim newPaths As String = "..\..\..\..\mono-basic\vbruntime\Microsoft.VisualBasic\"

            Dim projFileName As String
            Dim rspFileName As String
            projFileName = PROJFILENAMEs
            rspFileName = RSPFILENAMEs

            If IO.File.Exists(projFileName) = False Then
                Return
            End If

            Dim files As New Text.StringBuilder
            Using x As New Xml.XmlTextReader(projFileName)
                Dim i As Integer
                While x.Read()
                    Dim filename As String
                    Dim prefix As String
                    prefix = ""
                    filename = ""
                    If x.Name = "Compile" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                        x.MoveToAttribute("Include")
                        filename = x.Value
                    ElseIf x.Name = "EmbeddedResource" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                        x.MoveToAttribute("Include")
                        filename = x.Value
                        prefix = "/res:"
                    End If
                    If filename <> "" Then
                        filename = newPaths & x.Value
                        filename = filename.Replace("%29", ")")
                        filename = filename.Replace("%28", "(")
                        files.AppendLine("""" & prefix & filename & """")
                        i += 1
                    End If
                End While
                files.AppendLine("""/keyfile:" & newPaths & "msfinal.pub""")
                'Diagnostics.Debug.WriteLine("Total: " & i.ToString)
                files.AppendLine("# Total: " & i.ToString & " files.")
            End Using
            WriteFile(rspFileName, files.ToString)

            Try
                IO.File.WriteAllText(RSPFILENAMELINUX, IO.File.ReadAllText(RSPFILENAMEs).Replace("\", "/"))
            Catch
            End Try
        End Sub

        Function pGenTypeCombinations() As String
            Dim result As String = ""
            Dim vals() As Integer = CType([Enum].GetValues(GetType(TypeCode)), Integer())
            For i As Integer = 0 To VB.UBound(vals)
                For j As Integer = 0 To VB.UBound(vals)
                    Dim i1, j1 As TypeCode
                    i1 = CType(vals(i), TypeCode)
                    j1 = CType(vals(j), TypeCode)
                    result &= i1.ToString & "_" & j1.ToString & " = TypeCode." & i1.ToString & " << SHIFT Or TypeCode." & j1.ToString & VB.vbNewLine
                Next
            Next
            Dim d As Short = 3S
            Return result
        End Function

        Sub pGen1()
            Dim tp As ArrayList = BUILTINTYPES

            Dim code As New Text.StringBuilder
            Dim t1, t2 As Type
            Dim className As String = "Gen1"
            Dim conversion As ConversionType
            Dim iCount As Integer
            code.AppendLine("Public Class " & className)
            For i As Integer = 0 To tp.Count - 1
                For j As Integer = 0 To tp.Count - 1
                    t1 = DirectCast(tp(i), Type)
                    t2 = DirectCast(tp(j), Type)
                    If i <> j Then
                        conversion = TypeResolution.Conversion(Helper.GetTypeCode(Nothing, t1), Helper.GetTypeCode(Nothing, t2)).Conversion
                        If conversion = ConversionType.Explicit Then
                            'code.AppendLine("   'Explicit conversion")
                        Else
                            code.AppendLine("   'Implicit conversion")
                            code.AppendLine("   Public Function " & t1.Name & "_To_" & t2.Name & "() As Object")
                            code.AppendLine("       Throw New NotImplementedException()")
                            code.AppendLine("   End Function")
                            code.AppendLine()
                            iCount += 1
                        End If
                    End If
                Next
            Next
            code.AppendLine("End Class")
            code.Insert(0, String.Format("'A total of {0} functions have been generated." & VB.vbNewLine, iCount.ToString))
            IO.File.WriteAllText("Auto Gen\" & className & ".vb", code.ToString)
        End Sub

        Sub pGen2()
            Dim path As String = "E:\Rolf\Proyectos\VB.NET\Compilers\vbnc\public\vbnc\test\Temp"
            Dim files As String() = IO.Directory.GetFiles(path, "*.vb")
            For Each str As String In files
                Dim bld As New Text.StringBuilder
                bld.AppendLine("Public Class " & VB.Left(IO.Path.GetFileNameWithoutExtension(str), str.Length))
                bld.AppendLine("	Sub Test")
                bld.AppendLine()
                bld.AppendLine("	End Sub")
                bld.AppendLine("End Class")
                IO.File.WriteAllText(str, bld.ToString)
            Next
        End Sub

    End Module
End Namespace
#End If