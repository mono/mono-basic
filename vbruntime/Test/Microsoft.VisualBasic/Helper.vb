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

<TestFixture()> _
Public Class Helper

    'TargetJvmNotSupported - AppDomain.GetAssemblies
    <Test(), Category("TargetJvmNotSupported")> _
    Public Sub PrintRuntimePath()
        For Each a As Reflection.Assembly In AppDomain.CurrentDomain.GetAssemblies
            If a.FullName.Contains("VisualBasic") Then
                Console.WriteLine("")
                Console.WriteLine("Using runtime in: " + a.Location)
            End If
        Next
    End Sub

#If NET_VER >= 2.0 Then
    Public Shared Sub CompareBytes(ByVal aa() As Byte, ByVal bb() As Byte, ByVal testname As String)
        If aa.Length <> bb.Length Then
            Assert.Fail(String.Format("{0}_CF1 - '{1} <{3}>' and '{2} <{4}>' does not have same size", testname, "a", "b", aa.Length, bb.Length))
        End If

        For i As Integer = 0 To aa.Length - 1
            If aa(i) <> bb(i) Then
                Assert.Fail(String.Format("{0}_CF1 - '{1} <{3:X},{5}>' and '{2} <{4:X},{5}>' differs at position {5}", testname, "a", "b", aa(i), bb(i), i, Chr(aa(i)), Chr(bb(i))))
            End If
        Next
    End Sub

    Public Shared Sub CompareFile(ByVal a As String, ByVal b As String, ByVal testName As String)
        Dim msg As String = ""

        Using aa As New FileStream(a, FileMode.Open, FileAccess.Read)
            Using bb As New FileStream(b, FileMode.Open, FileAccess.Read)
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
            End Using
        End Using

        If msg <> "" Then
            Assert.Fail(msg)
        End If
    End Sub

    Public Shared Sub CompareDirectory(ByVal a As String, ByVal b As String, ByVal name As String)
        Dim filesA() As String = System.IO.Directory.GetFiles(a)
        Dim filesB() As String = System.IO.Directory.GetFiles(b)

        Dim namesA As New Generic.List(Of String)
        Dim namesB As New Generic.List(Of String)

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

#End If
End Class