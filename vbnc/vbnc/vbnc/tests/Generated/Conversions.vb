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
    Public Class ConversionGenerator
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
            For Each file As String In IO.Directory.GetFiles(Path, "*.vb")
                IO.File.Delete(file)
                Console.WriteLine("Deleted " & file)
            Next
            Console.WriteLine("Generating explicit conversions...")
            Generate(True)
            Console.WriteLine("Generating implicit conversions...")
            Generate(False)
        End Sub

        Shared Function IsFloatingPoint(ByVal Type As TypeCode) As Boolean
            Return Type = TypeCode.Single OrElse Type = TypeCode.Double OrElse Type = TypeCode.Decimal
        End Function

        Shared Sub Generate(ByVal Explicit As Boolean)
            Dim types As String() = Z.Constants.TYPES
            Dim typeValues As String() = Z.Constants.TYPEVALUES
            Dim conversions() As String = Z.Constants.CONVERSIONS
            Dim stringconversions() As String = Z.Constants.STRINGTYPEVALUES

            For i As Integer = 0 To types.GetUpperBound(0)
                Dim successFile As String
                Dim successFileContents As New Text.StringBuilder()
                Dim successFileMethodsToCall As New Generic.List(Of String)

                For j As Integer = 0 To types.GetUpperBound(0)
                    Dim tp1, tp2, conv As String
                    Dim val1 As String
                    Dim err As Integer

                    If i = j Then Continue For

                    err = TypeConverter.GetErrorNumberForConversion(Constants.BUILTINTYPESCODES(i), Constants.BUILTINTYPESCODES(j), Not Explicit)

                    tp1 = types(i)
                    tp2 = types(j)
                    If tp1 = "String" Then
                        val1 = stringconversions(j)
                    Else
                        val1 = typeValues(i)
                    End If

                    Dim Test As System.Text.StringBuilder = New System.Text.StringBuilder
                    Dim name As String
                    If Explicit Then
                        name = "ExplicitConversion" & tp1 & "To" & tp2 & "1"
                        conv = conversions(j) & "(%)"
                    Else
                        name = "ImplicitConversion" & tp1 & "To" & tp2 & "1"
                        conv = "%"
                    End If
                    Test.AppendLine("    Function " & name & "() As Integer")
                    Test.AppendLine("        Dim result As Boolean")
                    Test.AppendLine("        Dim source1 As " & tp1)
                    Test.AppendLine("        Dim dest1 As " & tp2)
                    Test.AppendLine("        Dim dest2 As " & tp2)
                    Test.AppendLine("")
                    Test.AppendLine("        Try")
                    Test.AppendLine("            source1 = " & val1)
                    Test.AppendLine("            dest1 = " & conv.Replace("%", "source1") & " 'Assignment from other variable")
                    Test.AppendLine("            dest2 = " & conv.Replace("%", val1) & " 'Assignment from constant")
                    Test.AppendLine("        Catch ex as Exception")
                    Test.AppendLine("            System.Console.WriteLine(""Unexpected error: "" & ex.Message)")
                    Test.AppendLine("            Return 2")
                    Test.AppendLine("        End Try")
                    Test.AppendLine("")
                    If tp1 = "Object" Then
                        Test.AppendLine("        result = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dest1, dest2, False)")
                    ElseIf tp1 = "Single" AndAlso tp2 = "Decimal" Then
                        'This seems to be a bug in vbc:
                        '
                        'Dim source As Single
                        'Dim dest1, dest2 as Decimal
                        'source = 100.001!
                        'dest1 = source     'IL: new DateTime (single)
                        'dest2 = 100.001!   'IL: new DateTime (int, int, int, bool, byte)
                        '
                        'Now dest1 = 100.001 and dest2 = 100.000999450684
                        'Looks like the single value is converted into a double before creating the constants for the datetime value.
                        Test.AppendLine("        result = (dest1 <> dest2) AndAlso (dest2 = New Decimal(CDbl(source1)) AndAlso (dest1 = New Decimal(CSng(source1))))")
                    Else
                        Test.AppendLine("        result = dest1 = dest2")
                    End If
                    Test.AppendLine("")
                    Test.AppendLine("        If result = False Then")
                    Test.AppendLine("            System.Console.WriteLine(""FAIL " & name & ", dest1={0},dest2={1}"", dest1, dest2)")
                    Test.AppendLine("            Return 1")
                    Test.AppendLine("        End If")
                    Test.AppendLine("    End Function")
                    Test.AppendLine()

                    If err <> 0 Then
                        Test.Insert(0, "Module " & name & VB.vbNewLine)
                        Test.AppendLine("    Function Main() As Integer")
                        Test.AppendLine("        Dim tmp As Integer")
                        Test.AppendLine("        ")
                        Test.AppendLine("        tmp += " & name)
                        Test.AppendLine("        ")
                        Test.AppendLine("        Return tmp")
                        Test.AppendLine("    End Function")
                        Test.AppendLine("End Module")

                        name = err.ToString() & " " & name
                        Constants.WriteFile(Path, name & ".vb", Test.ToString)
                        Console.WriteLine("Created file: " & name & ".vb")
                    Else
                        successFileContents.AppendLine(Test.ToString)
                        successFileMethodsToCall.Add(name)
                    End If

                Next

                If successFileContents.Length > 0 Then
                    If Explicit Then
                        successFile = "ExplicitConversion" & types(i) & "To_1"
                    Else
                        successFile = "ImplicitConversion" & types(i) & "To_1"
                    End If
                    successFileContents.Insert(0, "Module " & successFile & VB.vbNewLine)

                    successFileContents.AppendLine("    Function Main() As Integer")
                    successFileContents.AppendLine("        Dim tmp As Integer")
                    successFileContents.AppendLine("        ")
                    For Each str As String In successFileMethodsToCall
                        successFileContents.AppendLine("        tmp += " & str)
                    Next
                    successFileContents.AppendLine("        ")
                    successFileContents.AppendLine("        Return tmp")
                    successFileContents.AppendLine("    End Function")
                    successFileContents.AppendLine("End Module")

                    Constants.WriteFile(Path, successFile & ".vb", successFileContents.ToString)
                    Console.WriteLine("Created file: " & successFile & ".vb")
                End If
            Next
        End Sub
    End Class
End Namespace

#End If
