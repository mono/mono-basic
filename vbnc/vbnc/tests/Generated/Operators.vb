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
Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Collections

Namespace Z
    Public Class OperatorGenerator
        Shared Function Main(ByVal args() As String) As Integer
            If Not (args.Length = 1 AndAlso args(0) = "-generate") Then
                Return 0
            End If

            Generate()
        End Function

        Shared ReadOnly Property Path() As String
            Get
                Return IO.Path.Combine(Z.Constants.GetBasePath, Z.Constants.GetName)
            End Get
        End Property

        Shared Sub Generate()
            Console.WriteLine("Deleting " & IO.Path.Combine(Path, "*.vb") & "...")
            IO.Directory.CreateDirectory(Path)
            For Each file As String In IO.Directory.GetFiles(Path, "*.vb")
                IO.File.Delete(file)
                Console.WriteLine("Deleted " & file)
            Next

            Dim Test As New System.Text.StringBuilder()
            Dim TestSucceeds As New System.Text.StringBuilder
            Dim TestFails As New System.Text.StringBuilder
            Dim TestStart As New System.Text.StringBuilder
            Dim TestEnd As New System.Text.StringBuilder

            Dim name As String


            For i As Integer = 0 To Constants.BINARYOPERATORS.Length - 1
                Dim op As String = Constants.BINARYOPERATORS(i)

                TestStart.Length = 0
                TestStart.AppendLine("Option Strict Off")
                TestStart.AppendLine("Imports System")
                TestStart.AppendLine("Class Operator" & Constants.BINARYOPERATORSNAME(i) & "Test")
                TestStart.AppendLine("    Public Shared Function Main() As Integer")
                TestStart.AppendLine("        Dim obj As Object")
                TestStart.AppendLine("        Dim result As Integer")
                For j As Integer = 0 To Constants.TYPEVALUES.Length - 1
                    Dim left As String = Constants.TYPEVALUES(j)
                    TestStart.AppendLine("        Dim " & Constants.TYPES(j) & "value As " & Constants.TYPES(j) & " = " & Constants.TYPEVALUES3(j))
                Next
                TestStart.AppendLine("")

                TestEnd.Length = 0
                TestEnd.AppendLine("")
                TestEnd.AppendLine("        Return result")
                TestEnd.AppendLine("    End Function")
                TestEnd.AppendLine("End Class")

                TestSucceeds.Length = 0
                TestSucceeds.Append(TestStart)

                For j As Integer = 0 To Constants.TYPEVALUES.Length - 1
                    Dim left As String = Constants.TYPES(j) & "value"
                    For k As Integer = 0 To Constants.TYPEVALUES.Length - 1
                        Dim right As String = Constants.TYPES(k) & "value"

                        name = Constants.BINARYOPERATORSNAME(i) & "_" & Constants.TYPES(j) & "_" & Constants.TYPES(k) & ".vb"

                        Dim resultType As TypeCode
                        Dim errorNumber As Integer

                        resultType = TypeConverter.GetBinaryResultType(Constants.BINARYOPERATORSKS(i), Constants.BUILTINTYPESCODES(j), Constants.BUILTINTYPESCODES(k))
                        errorNumber = TypeConverter.GetErrorNumberForBinaryOperation(Constants.BINARYOPERATORSKS(i), Constants.BUILTINTYPESCODES(j), Constants.BUILTINTYPESCODES(k))

                        Test.Length = 0
                        If Constants.BUILTINTYPESCODES(j) = TypeCode.DateTime OrElse Constants.BUILTINTYPESCODES(k) = TypeCode.DateTime OrElse Constants.BUILTINTYPESCODES(j) = TypeCode.Char OrElse Constants.BUILTINTYPESCODES(k) = TypeCode.Char Then
                            Test.AppendLine("        Try")
                            Test.AppendLine("            obj = " & left & " " & op & " " & right)
                            Test.AppendLine("            If Type.GetTypeCode(obj.GetType()) <> TypeCode." & resultType.ToString() & " Then Console.WriteLine(""<" & name & "> Expected TypeCode." & resultType.ToString & " got TypeCode."" & Type.GetTypeCode(obj.GetType()).ToString ()) : result += 1")
                            Test.AppendLine("        Catch ex As InvalidCastException")
                            Test.AppendLine("            Console.WriteLine(ex.Message)")
                            Test.AppendLine("        Catch ex As Exception")
                            Test.AppendLine("            result += 1")
                            Test.AppendLine("            Console.WriteLine(ex.Message)")
                            Test.AppendLine("        End Try")
                        Else
                            Test.AppendLine("        obj = " & left & " " & op & " " & right)
                            Test.AppendLine("        If Type.GetTypeCode(obj.GetType()) <> TypeCode." & resultType.ToString() & " Then Console.WriteLine(""<" & name & "> Expected TypeCode." & resultType.ToString & " got TypeCode."" & Type.GetTypeCode(obj.GetType()).ToString ()) : result += 1")
                        End If
                        Test.AppendLine("")

                        If errorNumber > 0 Then
                            TestFails.Length = 0
                            TestFails.Append(TestStart)
                            TestFails.Append(Test)
                            TestFails.Append(TestEnd)

                            name = System.IO.Path.Combine(Path, errorNumber & "_" & name)
                            System.IO.File.WriteAllText(name, TestFails.ToString)
                        Else
                            TestSucceeds.Append(Test)
                        End If
                    Next
                Next


                TestSucceeds.Append(TestEnd)


                name = System.IO.Path.Combine(Path, "All_" & Constants.BINARYOPERATORSNAME(i) & "_Operators.vb")
                System.IO.File.WriteAllText(name, TestSucceeds.ToString)
            Next

        End Sub
    End Class
End Namespace
#End If