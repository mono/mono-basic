'
' InformationTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Information 
'
' Guy Cohen (guyc@mainsoft.com)
'
' 

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
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
Imports System
Imports System.IO
Imports System.Collections
Imports Microsoft.VisualBasic

<TestFixture()> _
Public Class InformationTests
    <SetUp()> _
    Public Sub GetReady()
    End Sub

    <TearDown()> _
    Public Sub Clean()
    End Sub

#Region "IsNothing tests"
    <Test()> _
    Public Sub IsNothing_1()
        Dim oDT1 As Byte
        Dim oDT2 As Short
        Dim oDT3 As Integer
        Dim oDT4 As Long
        Dim oDT5 As Single
        Dim oDT6 As Double
        Dim oDT7 As Decimal
        Dim oDT8 As String = Nothing
        Dim oDT9 As Object = Nothing
        Dim oDT10 As Boolean
        Dim oDT11 As Char
        Dim oDT12 As Date

        Assert.AreEqual(False, Information.IsNothing(oDT1))
        Assert.AreEqual(False, Information.IsNothing(oDT2))
        Assert.AreEqual(False, Information.IsNothing(oDT3))
        Assert.AreEqual(False, Information.IsNothing(oDT4))
        Assert.AreEqual(False, Information.IsNothing(oDT5))
        Assert.AreEqual(False, Information.IsNothing(oDT6))
        Assert.AreEqual(False, Information.IsNothing(oDT7))
        Assert.AreEqual(True, Information.IsNothing(oDT8))
        Assert.AreEqual(True, Information.IsNothing(oDT9))
        Assert.AreEqual(False, Information.IsNothing(oDT10))
        Assert.AreEqual(False, Information.IsNothing(oDT11))
        Assert.AreEqual(False, Information.IsNothing(oDT12))

    End Sub

    <Test()> _
    Public Sub IsNothing_2()
        Dim oDT1 As Test1 = Nothing
        Assert.AreEqual(True, Information.IsNothing(oDT1))

    End Sub
    Public Class Test1

    End Class

#End Region

End Class



