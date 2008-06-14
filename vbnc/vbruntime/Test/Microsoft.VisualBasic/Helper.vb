'
' Helper.vb - Helper routines
'
' Rolf Bjarne Kvinge (RKvinge@novell.com)
'
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
Imports NUnit.Framework
Imports System.IO
Imports System.Collections

<TestFixture()> _
Public Class Helper

    'TargetJvmNotSupported - AppDomain.GetAssemblies
#If Not TARGET_JVM Then
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub PrintRuntimePath()
        For Each a As Reflection.Assembly In AppDomain.CurrentDomain.GetAssemblies
            If a.FullName.IndexOf("VisualBasic") >= 0 Then
                Console.WriteLine("")
                Console.WriteLine("Using runtime in: " + a.Location)
            End If
        Next
    End Sub
#End If
    Shared Sub SetThreadCulture()
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")
        Threading.Thread.CurrentThread.CurrentUICulture = Threading.Thread.CurrentThread.CurrentCulture
    End Sub

    Public Shared Sub CompareBytes(ByVal aa() As Byte, ByVal bb() As Byte, ByVal testname As String)
        If aa.Length <> bb.Length Then
            Assert.Fail(String.Format("{0}- '{1} <{3}>' and '{2} <{4}>' does not have same size", testname, "a", "b", aa.Length, bb.Length))
        End If

        For i As Integer = 0 To aa.Length - 1
            If aa(i) <> bb(i) Then
                Assert.Fail(String.Format("{0} - '{1} <{3:X},""{5}"">' and '{2} <{4:X},""{6}"">' differs at position {5}", testname, "a", "b", aa(i), bb(i), i, Chr(aa(i)), Chr(bb(i))))
            End If
        Next
    End Sub

    Public Shared Sub CompareFile(ByVal a As String, ByVal b As String, ByVal testName As String)
        Dim msg As String = ""
        Dim aa As FileStream = Nothing
        Dim bb As FileStream = Nothing
        Try
            aa = New FileStream(a, FileMode.Open, FileAccess.Read)
            bb = New FileStream(b, FileMode.Open, FileAccess.Read)
            If aa.Length <> bb.Length Then
                msg = String.Format("{0}_CF1 - '{1} <{3}>' and '{2} <{4}>' does not have same size", testName, a, b, aa.Length, bb.Length)
            Else
                Dim aaa(1023) As Byte
                Dim bbb(1023) As Byte
                Dim reada, readb As Integer
                Do
                    reada = aa.Read(aaa, 0, 1024)
                    readb = bb.Read(bbb, 0, 1024)
                    For i As Integer = 0 To reada - 1
                        If aaa(i) <> bbb(i) Then

                            msg = String.Format("{0}_CF1 - '{1} <{3:X},{5}>' and '{2} <{4:X},{5}>' differs at position {5}", testName, a, b, aaa(i), bbb(i), aa.Position, Chr(aaa(i)), Chr(bbb(i)))
                            Exit Do
                        End If
                    Next
                Loop Until aa.Length = aa.Position OrElse reada = 0 OrElse readb = 0
            End If
        Finally
            If Not bb Is Nothing Then bb.Close()
            If Not aa Is Nothing Then aa.Close()
        End Try

        If msg <> "" Then
            Assert.Fail(msg)
        End If
    End Sub

    Public Shared Sub CompareDirectory(ByVal a As String, ByVal b As String, ByVal name As String)
        Dim filesA() As String = System.IO.Directory.GetFiles(a)
        Dim filesB() As String = System.IO.Directory.GetFiles(b)

        Dim namesA As New ArrayList
        Dim namesB As New ArrayList

        For Each filename As String In filesA
            namesA.Add(System.IO.Path.GetFileName(filename))
        Next
        For Each filename As String In filesB
            namesB.Add(System.IO.Path.GetFileName(filename))
        Next

        If filesA.Length <> filesB.Length Then
            Assert.Fail("{2}_CD1 - '{0}' and '{1}' does not contain the same number of files", a, b, name)
        End If

        For Each str As String In namesA
            If namesB.Contains(str) = False Then
                Assert.Fail("{2}_CD2 - '{0}' contains '{2}', but '{1}' does not", a, b, name, str)
            End If
        Next

        For Each str As String In namesB
            If namesA.Contains(str) = False Then
                Assert.Fail("{2}_CD3 - '{0}' contains '{2}', but '{1}' does not", b, a, name, str)
            End If
        Next

        For Each str As String In filesA
            CompareFile(str, Path.Combine(b, System.IO.Path.GetFileName(str)), name)
        Next

        'xxxxxxxxxxxxxx


        Dim subdirsA() As String = System.IO.Directory.GetDirectories(a)
        Dim subdirsB() As String = System.IO.Directory.GetDirectories(b)

        namesA.Clear()
        For Each filename As String In subdirsA
            namesA.Add(System.IO.Path.GetFileName(filename))
        Next
        namesB.Clear()
        For Each filename As String In subdirsB
            namesB.Add(System.IO.Path.GetFileName(filename))
        Next

        If namesA.Count <> namesB.Count Then
            Assert.Fail("{2}_CD4 - '{0}' and '{1}' does not contain the same number of subdirectories", a, b, name)
        End If

        For Each str As String In namesA
            If namesB.Contains(str) = False Then
                Assert.Fail("{2}_CD5 - '{0}' contains '{2}', but '{1}' does not", a, b, name, str)
            End If
        Next

        For Each str As String In namesB
            If namesA.Contains(str) = False Then
                Assert.Fail("{2}_CD6 - '{0}' contains '{2}', but '{1}' does not", b, a, name, str)
            End If
        Next

        For Each str As String In subdirsA
            CompareDirectory(str, Path.Combine(b, System.IO.Path.GetFileName(str)), name)
        Next
    End Sub

    Public Shared Function CreateCode(ByVal obj As Object) As String
        If TypeOf obj Is Byte() Then
            Return CreateCode(DirectCast(obj, Byte()))
        ElseIf TypeOf obj Is String Then
            Return CreateCode(DirectCast(obj, String))
        ElseIf obj Is Nothing Then
            Return "Nothing"
        ElseIf TypeOf obj Is Boolean Then
            If CBool(obj) Then
                Return "True"
            Else
                Return "False"
            End If
        ElseIf TypeOf obj Is Byte Then
            Dim value As Byte = CByte(obj)
            Return "CByte(" & value.ToString & ")"
        ElseIf TypeOf obj Is Short Then
            Dim value As Short = CShort(obj)
            If value = Short.MinValue Then Return "Short.MinValue"
            Return "CShort(" & value.ToString & ")"
        ElseIf TypeOf obj Is Integer Then
            Dim value As Integer = CInt(obj)
            If value = Integer.MinValue Then Return "Integer.MinValue"
            Return "CInt(" & value.ToString & ")"
        ElseIf TypeOf obj Is Long Then
            Dim value As Long = CLng(obj)
            If value = Long.MinValue Then Return "Long.MinValue"
            Return value.ToString & "L"
        ElseIf TypeOf obj Is Decimal Then
            Dim value As Decimal = CDec(obj)
            Return value.ToString & "D"
        ElseIf TypeOf obj Is Double Then
            Dim value As Double = CDbl(obj)
            If value = Double.NaN Then Return "Double.NaN"
            If value.ToString = "NaN" Then Return "Double.NaN"
            Return "CDbl(" & value.ToString & ")"
        ElseIf TypeOf obj Is Single Then
            Dim value As Single = CSng(obj)
            If value = Single.NaN Then Return "Single.NaN"
            If value.ToString = "NaN" Then Return "Single.NaN"
            Return "CSng(" & value.ToString & ")"
        ElseIf TypeOf obj Is Date Then
            Dim value As Date = CDate(obj)
            Return "#" & value.ToString("MM/dd/yyyy hh:mm:ss tt") & "#"
        ElseIf TypeOf obj Is Char Then
            Dim value As Char = CChar(obj)
            Return """" & value.ToString & """c"
        ElseIf TypeOf obj Is String Then
            Dim value As String = CStr(obj)
            Return """" & value.ToString & """"
        ElseIf TypeOf obj Is DBNull Then
            Return "System.DBNull.Value"
        Else
            Stop
            Return "Nothing"
        End If
    End Function

    Public Shared Function CreateCode(ByVal bytes() As Byte) As String
        Dim builder As New System.Text.StringBuilder(bytes.Length * 2 + 10)
        builder.Append("new Byte () { ")
        For i As Integer = 0 To bytes.Length - 1
            builder.Append(bytes(i).ToString())
            builder.Append(", ")
        Next
        If bytes.Length > 0 Then
            builder.Length -= 2
        End If
        builder.Append(" }")
        Return builder.ToString
    End Function

    Public Shared Function CreateCode(ByVal str As String) As String
        Return """" & Stringify(str) & """"
    End Function

    Public Shared Function Stringify(ByVal str As String) As String
        str = str.Replace("""", """""")
        str = str.Replace(vbNewLine, """ & vbNewLine & """)
        Return str
    End Function

    Public Shared Function ReadAllBytes(ByVal Filename As String) As Byte()
        Dim result As Byte()
        Dim fs As FileStream = Nothing
        Try
            fs = New FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            ReDim result(CInt(fs.Length) - 1)
            fs.Read(result, 0, result.Length)
            Return result
        Finally
            If Not fs Is Nothing Then fs.Close()
        End Try
    End Function

    Public Shared Function ReadAllText(ByVal Filename As String) As String
        Dim fs As StreamReader = Nothing
        Try
            fs = New StreamReader(Filename, System.Text.Encoding.GetEncoding(1252))
            Return fs.ReadToEnd
        Finally
            If Not fs Is Nothing Then fs.Close()
        End Try
    End Function

    Public Shared Sub WriteAllBytes(ByVal Filename As String, ByVal Contents As Byte())
        Dim fs As FileStream = Nothing
        Try
            fs = New FileStream(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
            fs.Write(Contents, 0, Contents.Length)
        Finally
            If Not fs Is Nothing Then fs.Close()
        End Try
    End Sub

    Public Shared Sub WriteAllText(ByVal Filename As String, ByVal Contents As String)
        Dim fs As StreamWriter = Nothing
        Try
            fs = New StreamWriter(Filename, False, System.Text.Encoding.GetEncoding(1252))
            fs.Write(Contents)
        Finally
            If Not fs Is Nothing Then fs.Close()
        End Try
    End Sub

    Public Shared Sub AppendAllText(ByVal Filename As String, ByVal Contents As String)
        Dim fs As FileStream = Nothing
        Dim sw As StreamWriter = Nothing
        Try
            fs = New FileStream(Filename, FileMode.Append, FileAccess.Write, FileShare.None)
            sw = New StreamWriter(fs, System.Text.Encoding.GetEncoding(1252))
            sw.Write(Contents)
        Finally
            If Not sw Is Nothing Then sw.Close()
            If Not fs Is Nothing Then fs.Close()
        End Try
    End Sub
End Class