'
' FileSystemTestGenerator.vb
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
'
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'

Imports System.Text
Imports System.IO
Imports System.Reflection
Imports NUnit.Framework

Public Class FileSystemTestGenerator
    Structure StructA
        Dim i As Integer
    End Structure
    Structure StructB
        Dim a As StructA
    End Structure

    Shared Function Generate() As String
        Dim builder As New StringBuilder

        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")
        Threading.Thread.CurrentThread.CurrentUICulture = Threading.Thread.CurrentThread.CurrentCulture

        Dim generator As New FileSystemTestGenerator
        Dim basedir As String = "G:\Volatile\head\"

        If basedir = "" Then
            Throw New Exception("You need to set 'basedir' to where mono-basic resides.")
        End If

        basedir = IO.Path.Combine(basedir, "mono-basic\vbruntime\Test\Microsoft.VisualBasic\")

        builder.AppendLine("'Generated, don't change anything here.")
        builder.AppendLine("Imports System")
        builder.AppendLine("Imports System.IO")
        builder.AppendLine("Imports System.Reflection")
        builder.AppendLine("Imports NUnit.Framework")
        builder.AppendLine("")
        builder.AppendLine("<TestFixture ()> _")
        builder.AppendLine("Public Class FileSystemTestGenerated")

        builder.AppendLine("{0}")

        Dim fstg As String = File.ReadAllText(Path.Combine(basedir, "FileSystemTestGenerator.vb"))
        Dim startindex As Integer = fstg.IndexOf("'START" & "COPY")
        Dim endindex As Integer = fstg.IndexOf("'END" & "COPY")
        fstg = fstg.Substring(startindex, endindex - startindex)
        builder.AppendLine(fstg)

        generator.GenerateOutputTests(builder)
        generator.GenerateInputTests(builder)

        builder.AppendLine("{0}")
        builder.AppendLine("End Class")

        Dim result As String
        result = builder.Replace("{0}", vbTab).ToString
        File.WriteAllText(Path.Combine(basedir, "FileSystemTestGenerated.vb"), result)

        Return result
    End Function

    Private Class DataInfo
        Public Type As Type
        Public Value As Object
        Public ValueAsCode As String

        Sub New(ByVal Type As Type, ByVal Value As Object, Optional ByVal Code As String = Nothing)
            Me.Type = Type
            Me.Value = Value
            Me.ValueAsCode = Code
        End Sub

        Sub New(ByVal Value As Object, Optional ByVal Code As String = Nothing)
            Me.Type = Value.GetType
            Me.Value = Value
            Me.ValueAsCode = Code
        End Sub

        Function GetVBFSType() As Type
            Dim tp As Type = Me.Type
            Select Case Type.GetTypeCode(tp)
                Case TypeCode.DBNull, TypeCode.Object
                    If tp.IsArray Then
                        tp = GetType(System.Array)
                    Else
                        tp = GetType(Object)
                    End If
                Case TypeCode.SByte
                    tp = GetType(Short)
                Case TypeCode.UInt16
                    tp = GetType(Integer)
                Case TypeCode.UInt32
                    tp = GetType(Long)
                Case TypeCode.UInt64
                    tp = GetType(Decimal)
            End Select
            Return tp
        End Function

        Function GetValueAsCode() As String
            If Not ValueAsCode Is Nothing Then Return ValueAsCode
            Dim tc As TypeCode = Type.GetTypeCode(Type)
            Select Case tc
                Case TypeCode.Boolean
                    Return Value.ToString
                Case TypeCode.Double
                    Return Value.ToString & "R"
                Case TypeCode.Single
                    Return Value.ToString & "F"
                Case TypeCode.UInt32
                    Return Value.ToString & "UI"
                Case TypeCode.Int32
                    Return Value.ToString & "I"
                Case TypeCode.SByte
                    Return "CSByte (" & Value.ToString & ")"
                Case TypeCode.Byte
                    Return "CByte (" & Value.ToString & ")"
                Case TypeCode.Int16
                    Return Value.ToString & "S"
                Case TypeCode.UInt16
                    Return Value.ToString & "US"
                Case TypeCode.UInt64
                    Return Value.ToString & "UL"
                Case TypeCode.Int64
                    Return Value.ToString & "L"
                Case TypeCode.Decimal
                    Return Value.ToString & "D"
                Case TypeCode.DateTime
                    Dim d As Date = #1/31/2001 12:32:00 PM#
                    Return "#" & CDate(Value).ToString("MM/dd/yyyy hh:mm:ss tt") & "#"
                Case TypeCode.Char
                    Return """" & Value.ToString & """c"
                Case TypeCode.String
                    Return """" & Value.ToString & """"
                Case Else
                    Return """<huh: " & tc.ToString() & "?> """""
            End Select
        End Function
    End Class

    Private Shared Datas As New Generic.List(Of DataInfo)
    Private Shared TestFileData As New Generic.List(Of Byte())

    Private Shared OpenModes As OpenMode() = {OpenMode.Append, OpenMode.Binary, OpenMode.Input, OpenMode.Output, OpenMode.Random}

    Shared Sub New()
        Datas.Clear()
        'Datas.Add(New DataInfo(Missing.Value, "System.Reflection.Missing.Value"))�
        Datas.Add(New DataInfo(DBNull.Value, "System.DBNull.Value"))
        Datas.Add(New DataInfo(True))
        Datas.Add(New DataInfo(False))
        Datas.Add(New DataInfo(Char.MinValue))
        Datas.Add(New DataInfo(Char.MaxValue))
        Datas.Add(New DataInfo(GetType(Short), SByte.MinValue, "SByte.MinValue"))
        Datas.Add(New DataInfo(GetType(Short), SByte.MaxValue, "SByte.MaxValue"))
        Datas.Add(New DataInfo(Byte.MinValue))
        Datas.Add(New DataInfo(Byte.MaxValue))
        Datas.Add(New DataInfo(Int16.MinValue, "Int16.MinValue"))
        Datas.Add(New DataInfo(Int16.MaxValue, "Int16.MaxValue"))
        Datas.Add(New DataInfo(GetType(Integer), UInt16.MinValue, "UInt16.MinValue"))
        Datas.Add(New DataInfo(GetType(Integer), UInt16.MaxValue, "UInt16.MaxValue"))
        Datas.Add(New DataInfo(Int32.MinValue, "Int32.MinValue"))
        Datas.Add(New DataInfo(Int32.MaxValue, "Int32.MaxValue"))
        Datas.Add(New DataInfo(GetType(Long), UInt32.MinValue, "UInt32.MinValue"))
        Datas.Add(New DataInfo(GetType(Long), UInt32.MaxValue, "UInt32.MaxValue"))
        Datas.Add(New DataInfo(Int64.MinValue, "Int64.MinValue"))
        Datas.Add(New DataInfo(Int64.MaxValue, "Int64.MaxValue"))
        Datas.Add(New DataInfo(GetType(Decimal), UInt64.MinValue, "UInt64.MinValue"))
        Datas.Add(New DataInfo(GetType(Decimal), UInt64.MaxValue, "UInt64.MaxValue"))
        Datas.Add(New DataInfo(Single.MinValue, "Single.MinValue"))
        Datas.Add(New DataInfo(Single.MaxValue, "Single.MaxValue"))
        Datas.Add(New DataInfo(Single.Epsilon, "Single.Epsilon"))
        Datas.Add(New DataInfo(Single.NaN, "Single.NaN"))
        Datas.Add(New DataInfo(Single.NegativeInfinity, "Single.NegativeInfinity"))
        Datas.Add(New DataInfo(Single.PositiveInfinity, "Single.PositiveInfinity"))
        Datas.Add(New DataInfo(Double.MinValue, "Double.MinValue"))
        Datas.Add(New DataInfo(Double.MaxValue, "Double.MaxValue"))
        Datas.Add(New DataInfo(Double.Epsilon, "Double.Epsilon"))
        Datas.Add(New DataInfo(Double.NaN, "Double.NaN"))
        Datas.Add(New DataInfo(Double.NegativeInfinity, "Double.NegativeInfinity"))
        Datas.Add(New DataInfo(Double.PositiveInfinity, "Double.PositiveInfinity"))
        Datas.Add(New DataInfo(Decimal.MinValue))
        Datas.Add(New DataInfo(Decimal.MaxValue))
        Datas.Add(New DataInfo(DateTime.MinValue, "DateTime.MinValue"))
        Datas.Add(New DataInfo(DateTime.MaxValue, "DateTime.MaxValue"))
        Datas.Add(New DataInfo(String.Empty))
        Datas.Add(New DataInfo(GetType(String), Nothing, "CStr (Nothing)"))
        Datas.Add(New DataInfo(GetType(Object), Nothing, "CObj (Nothing)"))
        Datas.Add(New DataInfo(GetType(ValueType), Nothing, "CType (Nothing, System.ValueType)"))

    End Sub

    'STARTCOPY
    Private DATA_DIR As String

    Sub New()
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")
        Threading.Thread.CurrentThread.CurrentUICulture = Threading.Thread.CurrentThread.CurrentCulture

        DATA_DIR = Environment.GetEnvironmentVariable("DATA_DIR")
        If DATA_DIR Is Nothing OrElse DATA_DIR = String.Empty Then
            DATA_DIR = Path.Combine(Path.GetTempPath, MethodInfo.GetCurrentMethod.DeclaringType.Name.Replace("Generator", "Generated"))
        End If
    End Sub

    Private Sub Initialize()
        If Directory.Exists(DATA_DIR) = False Then
            Directory.CreateDirectory(DATA_DIR)
        End If
    End Sub

    Private Sub CleanUp()
        'Close all files and delete them
        Microsoft.VisualBasic.FileSystem.FileClose()
        If Directory.Exists(DATA_DIR) Then
            Directory.Delete(DATA_DIR, True)
        End If
    End Sub

    'ENDCOPY

    Shared Function FindMethodWithParameterTypeX(ByVal methods As Generic.List(Of MethodInfo), ByVal paramindex As Integer, ByVal type As Type) As MethodInfo
        For i As Integer = 0 To methods.Count - 1
            Dim minf As MethodInfo = methods(i)
            If minf.GetParameters()(paramindex).ParameterType Is type Then
                Return minf
            End If
        Next
        Return Nothing
    End Function

    Shared Function CreateArguments(ByVal method As MethodInfo) As Object()
        Dim result As Object()
        ReDim result(method.GetParameters.Length - 1)

        result(0) = 1 'FileNumber

        For i As Integer = 0 To method.GetParameters.Length - 1
            If method.GetParameters(i).IsOptional Then
                result(i) = method.GetParameters(i).DefaultValue
            End If
        Next

        Return result
    End Function

    Shared Function CreateObjectArray(ByVal type As Type, ByVal value As Object) As Boolean
        Return Not (type Is GetType(Object()) AndAlso value IsNot Nothing AndAlso value.GetType Is GetType(Object()))
    End Function

    Private Shared Function CreateObjectArrayCode(ByVal hasParamArray As Boolean, ByVal data As DataInfo) As String
        Dim code As String = data.GetValueAsCode
        If hasParamArray Then
            code = "New Object () {" & code & "}"
        End If
        Return code
    End Function

    Shared Function CreateArguments(ByVal method As MethodInfo, ByVal value As Object, ByRef hasParamArray As Boolean, Optional ByVal doConversion As Boolean = False) As Object()
        Dim result As Object()
        ReDim result(method.GetParameters.Length - 1)

        hasParamArray = False

        For i As Integer = 0 To method.GetParameters.Length - 1
            Dim param As ParameterInfo = method.GetParameters(i)
            If param.Name = "Output" OrElse param.Name = "Value" Then
                If doConversion AndAlso (value Is Nothing OrElse param.ParameterType IsNot value.GetType AndAlso param.ParameterType IsNot GetType(Object)) Then
                    Try
                        'value = Convert.ChangeType(value, param.ParameterType)
                    Catch ex As Exception
                        Stop
                    End Try
                End If
                If param.IsDefined(GetType(ParamArrayAttribute), False) AndAlso CreateObjectArray(param.ParameterType, value) Then
                    result(i) = New Object() {value}
                    hasParamArray = True
                Else
                    result(i) = value
                End If
            ElseIf param.IsOptional Then
                result(i) = param.DefaultValue
            ElseIf param.Name = "FileNumber" Then
                result(i) = 1
            ElseIf param.Name = "CharCount" Then
            Else
                Stop
            End If
        Next

        Return result
    End Function

    Private Shared Function FindDataParameter(ByVal method As MethodInfo) As ParameterInfo
        For Each param As ParameterInfo In method.GetParameters
            If param.Name = "Value" OrElse param.Name = "Output" Then
                Return param
            End If
        Next
        Return Nothing
    End Function

    Private Shared Function SelectMethod(ByVal methods As Generic.List(Of MethodInfo), ByVal data As DataInfo) As MethodInfo

        'Only one method, return that one
        If methods.Count = 1 Then Return methods(0)

        'Find any exact matches
        For Each method As MethodInfo In methods
            Dim param As ParameterInfo = FindDataParameter(method)
            If param.ParameterType Is data.Type Then Return method
        Next

        'Find any Object matches
        For Each method As MethodInfo In methods
            If FindDataParameter(method).ParameterType Is GetType(Object) Then Return method
        Next

        Stop

        Return Nothing
    End Function

    Public Shared Function CompareBytes(ByVal aa() As Byte, ByVal bb() As Byte) As Boolean
        If aa.Length <> bb.Length Then Return False
        For i As Integer = 0 To aa.Length - 1
            If aa(i) <> bb(i) Then Return False
        Next
        Return True
    End Function

    Private Shared Sub AddTestFile(ByVal data As Byte())
        For i As Integer = 0 To TestFileData.Count - 1
            If TestFileData(i).Length = data.Length Then Return
            If CompareBytes(TestFileData(i), data) Then Return
        Next
        TestFileData.Add(data)
    End Sub

    Private Sub GenerateOutputTests(ByVal builder As StringBuilder)
        Try
            Dim filename As String
            Dim counter As Integer

            For Each str As String In New String() {"FilePut", "FilePutObject", "Print", "PrintLine", "Write", "WriteLine"}
                Dim methods As New Generic.List(Of MethodInfo)(GetType(Microsoft.VisualBasic.FileSystem).GetMethods(BindingFlags.Static Or BindingFlags.Public))
                For i As Integer = methods.Count - 1 To 0 Step -1
                    If methods(i).Name <> str Then methods.RemoveAt(i)
                Next

                Dim testname As String = str & "Test"
                Dim base_filename As String = Path.Combine(DATA_DIR, testname)

                For Each openmode As OpenMode In OpenModes
                    counter = 1
                    For Each data As DataInfo In Datas
                        Dim method As MethodInfo = SelectMethod(methods, data)
                        Dim parameter As ParameterInfo = FindDataParameter(method)
                        Dim unique As String = "_" & openmode.ToString() & "_" & counter.ToString()
                        Dim tp As Type = data.GetVBFSType
                        Dim exception As Exception = Nothing
                        Dim result As Byte() = Nothing
                        Dim hasParamArray As Boolean

                        If data.Value IsNot Nothing AndAlso parameter.ParameterType.IsAssignableFrom(data.Value.GetType) = False Then
                            Continue For
                        End If

                        filename = base_filename & unique

                        GeneratePrologue(builder, testname, unique, data)

                        Try
                            Dim args As Object() = CreateArguments(method, data.Value, hasParamArray, str = "FilePut" OrElse str = "FilePutObject")
                            Initialize()
                            FileOpen(1, filename, openmode)
                            method.Invoke(Nothing, args)
                            FileClose(1)
                            result = IO.File.ReadAllBytes(filename)
                            AddTestFile(result)
                            'Helper.CompareBytes(result, IO.File.ReadAllBytes(filename), Path.GetFileName(filename))
                        Catch tex As TargetInvocationException
                            Dim ex As Exception = tex
                            While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                ex = ex.InnerException
                            End While
                            exception = ex
                        Catch ex As Exception
                            While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                ex = ex.InnerException
                            End While
                            exception = ex
                        Finally
                            CleanUp()
                        End Try
                        builder.AppendLine("{0}{0}{0}Initialize()")
                        builder.AppendLine("{0}{0}{0}FileOpen(1, filename, OpenMode." & openmode.ToString() & ")")
                        Dim code As String = CreateObjectArrayCode(hasParamArray, data)
                        If code.StartsWith("CObj (") Then
                            builder.AppendLine("{0}{0}{0}" & method.Name & "(CObj(1), " & code & ")")
                        Else
                            builder.AppendLine("{0}{0}{0}" & method.Name & "(1, " & code & ")")
                        End If

                        GenerateEpilogue(builder, exception, result, data, testname)
                        counter += 1
                    Next
                Next
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Stop
        Finally
            CleanUp()
        End Try
    End Sub

    Private Sub GenerateInputTests(ByVal builder As StringBuilder)
        Try
            Dim filename As String
            Dim counter As Integer

            Debug.WriteLine("Got " & TestFileData.Count & " test files.")

            For k As Integer = 0 To TestFileData.Count - 1
                Dim testfile As Byte() = TestFileData(k)
                'LineInput and InputString returns string objects
                For Each str As String In New String() {"LineInput", "InputString"}
                    Dim method As MethodInfo = GetType(Microsoft.VisualBasic.FileSystem).GetMethod(str)

                    counter = 1
                    Dim testname As String = str & "Test"
                    Dim base_filename As String = Path.Combine(DATA_DIR, testname)

                    For Each openmode As OpenMode In OpenModes
                        Dim unique As String = "_" & k.ToString() & "_" & openmode.ToString() & "_" & counter.ToString()
                        Dim exception As Exception = Nothing
                        Dim hasParamArray As Boolean
                        Dim args As Object() = CreateArguments(method, Nothing, hasParamArray, False)
                        Dim result As Object = Nothing

                        filename = base_filename & unique

                        GeneratePrologue(builder, testname, unique, Nothing)

                        If str = "InputString" Then
                            args(1) = testfile.Length
                        End If

                        Try
                            Initialize()
                            IO.File.WriteAllBytes(filename, testfile)
                            FileOpen(1, filename, openmode)
                            result = method.Invoke(Nothing, args)
                            FileClose(1)
                        Catch tex As TargetInvocationException
                            Dim ex As Exception = tex
                            While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                ex = ex.InnerException
                            End While
                            exception = ex
                        Catch ex As Exception
                            While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                ex = ex.InnerException
                            End While
                            exception = ex
                        Finally
                            CleanUp()
                        End Try

                        builder.AppendLine("{0}{0}{0}Dim value As String = Nothing")
                        builder.AppendLine("{0}{0}{0}Initialize()")
                        builder.AppendLine("{0}{0}{0}Helper.WriteAllBytes (filename, " & Helper.CreateCode(testfile) & ")")
                        builder.AppendLine("{0}{0}{0}FileOpen(1, filename, OpenMode." & openmode.ToString() & ")")
                        If str = "InputString" Then
                            builder.AppendLine("{0}{0}{0}value = " & method.Name & "(1, " & testfile.Length.ToString() & ")")
                        Else
                            builder.AppendLine("{0}{0}{0}value = " & method.Name & "(1)")
                        End If

                        GenerateEpilogue(builder, exception, result, Nothing, testname, testfile)
                        counter += 1
                    Next
                Next
                'FileGet, FileGetObject and Input returns data in byref parameters
                For Each str As String In New String() {"FileGet", "FileGetObject", "Input"}
                    Dim methods As New Generic.List(Of MethodInfo)(GetType(Microsoft.VisualBasic.FileSystem).GetMethods(BindingFlags.Static Or BindingFlags.Public))
                    For i As Integer = methods.Count - 1 To 0 Step -1
                        If methods(i).Name <> str Then methods.RemoveAt(i)
                    Next
                    For Each method As MethodInfo In methods
                        counter = 1
                        Dim parameter As ParameterInfo = FindDataParameter(method)
                        Dim testname As String = str & "Test"
                        Dim base_filename As String = Path.Combine(DATA_DIR, testname)

                        For Each openmode As OpenMode In OpenModes
                            Dim unique As String = "_" & k.ToString() & "_" & parameter.ParameterType.GetElementType.Name & "_" & openmode.ToString() & "_" & counter.ToString()
                            Dim exception As Exception = Nothing
                            Dim hasParamArray As Boolean
                            Dim args As Object() = CreateArguments(method, Nothing, hasParamArray, False)
                            Dim result As Object = Nothing

                            filename = base_filename & unique

                            GeneratePrologue(builder, testname, unique, Nothing)

                            Try
                                Initialize()
                                IO.File.WriteAllBytes(filename, testfile)
                                FileOpen(1, filename, openmode)
                                method.Invoke(Nothing, args)
                                FileClose(1)
                                result = args(parameter.Position)
                            Catch tex As TargetInvocationException
                                Dim ex As Exception = tex
                                While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                    ex = ex.InnerException
                                End While
                                exception = ex
                            Catch ex As Exception
                                While TypeOf ex Is TargetInvocationException AndAlso ex.InnerException IsNot Nothing
                                    ex = ex.InnerException
                                End While
                                exception = ex
                            Finally
                                CleanUp()
                            End Try

                            builder.AppendLine("{0}{0}{0}Dim value As " & parameter.ParameterType.GetElementType.FullName & " = Nothing")
                            builder.AppendLine("{0}{0}{0}Initialize()")
                            builder.AppendLine("{0}{0}{0}Helper.WriteAllBytes (filename, " & Helper.CreateCode(testfile) & ")")
                            builder.AppendLine("{0}{0}{0}FileOpen(1, filename, OpenMode." & openmode.ToString() & ")")
                            builder.AppendLine("{0}{0}{0}" & method.Name & "(1, value)")

                            GenerateEpilogue(builder, exception, result, Nothing, testname)
                            counter += 1
                        Next
                    Next
                Next
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Stop
        Finally
            CleanUp()
        End Try
    End Sub

    Private Shared Sub GeneratePrologue(ByVal builder As StringBuilder, ByVal testname As String, ByVal unique As String, ByVal data As DataInfo)
        builder.AppendLine("{0}<Test ()> _")
        builder.AppendLine("{0}Sub " & testname & unique & "()")
        'builder.AppendLine("{0}{0}Dim base_filename As String = Path.Combine (DATA_DIR, """ & Helper.Stringify(testname) & """)")
        builder.AppendLine("{0}{0}Dim filename As String = Path.Combine (DATA_DIR, """ & Helper.Stringify(testname) & unique & """)")
        'builder.AppendLine("{0}{0}")
        'builder.AppendLine("{0}{0}filename = base_filename & """ & unique & """")
        builder.AppendLine("{0}{0}Try")
    End Sub

    Private Shared total As Integer, exceptions As Integer

    Private Sub GenerateEpilogue(ByVal builder As StringBuilder, ByVal exception As Exception, ByVal result As Object, ByVal data As DataInfo, ByVal name As String, Optional ByVal input() As Byte = Nothing)
        builder.AppendLine("{0}{0}{0}FileClose(1)")

        Dim useLike As Boolean

        useLike = exception IsNot Nothing AndAlso exception.Message.Contains(DATA_DIR)

        If exception IsNot Nothing Then
            Dim msg As String = Helper.Stringify(exception.Message).Replace(DATA_DIR, "<DATA_DIR>")
            builder.AppendLine("{0}{0}{0}Assert.Fail (""Expected " & exception.GetType.FullName & " ('" & msg & "')"", filename)")
        Else
            If TypeOf result Is Byte() Then
                builder.AppendLine("{0}{0}{0}Helper.CompareBytes (Helper.ReadAllBytes (filename), " & Helper.CreateCode(DirectCast(result, Byte())) & ", filename)")
            Else
                builder.AppendLine("{0}{0}{0}Assert.AreEqual (" & Helper.CreateCode(result) & ", value, filename)")
            End If
        End If
        builder.AppendLine("{0}{0}Catch ex as NUnit.Framework.AssertionException")
        builder.AppendLine("{0}{0}{0}Throw")
        builder.AppendLine("{0}{0}Catch ex As Exception")
        If exception IsNot Nothing Then
            Dim isWeirdExc As Boolean = exception.GetType Is GetType(NullReferenceException) AndAlso name.Contains("InputString") AndAlso input IsNot Nothing AndAlso input.Length <> 0

            If isWeirdExc Then
                builder.AppendLine("{0}{0}{0}Assert.AreEqual (""" & exception.GetType.FullName & """, ex.GetType.FullName, filename)")
            Else
                builder.AppendLine("{0}{0}{0}Assert.AreEqual (""" & exception.GetType.FullName & """, ex.GetType.FullName, filename)")
            End If
            If useLike Then
                Dim dir As String
                If DATA_DIR.EndsWith(Path.DirectorySeparatorChar) Then dir = DATA_DIR Else dir = DATA_DIR & Path.DirectorySeparatorChar
                Dim pattern As String = Helper.Stringify(exception.Message).Replace(dir, "*").Replace("""", "?").Replace("'", "?")
                builder.AppendLine("{0}{0}{0}Assert.IsTrue (ex.Message Like """ & pattern & """, filename & "" - <"" & ex.Message & ""> didn't match <" & pattern & ">"")")
            Else
                If exception.GetType Is GetType(InvalidCastException) AndAlso exception.Message.Contains("Conversion") Then
                    builder.AppendLine("{0}{0}{0}Assert.AreEqual (""" & Helper.Stringify(exception.Message) & """, ex.Message, filename)")
                ElseIf isWeirdExc Then
                    builder.AppendLine("{0}{0}{0}Assert.AreEqual (""" & Helper.Stringify(exception.Message) & """, ex.Message, filename)")
                Else
                    builder.AppendLine("{0}{0}{0}Assert.AreEqual (""" & Helper.Stringify(exception.Message) & """, ex.Message.Replace (""Acess"", ""Access""), filename)")
                End If
            End If
        Else
            builder.AppendLine("{0}{0}{0}Assert.Fail (""Did not expect any exception, got "" & ex.GetType.FullName & "" ("" & ex.Message, filename)")
        End If
        builder.AppendLine("{0}{0}Finally")
        builder.AppendLine("{0}{0}{0}CleanUp()")
        builder.AppendLine("{0}{0}End Try")
        builder.AppendLine("{0}End Sub")
    End Sub
End Class
