'
' InteractionTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Information 
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
Public Class InteractionTests
    <SetUp()> _
    Public Sub GetReady()
    End Sub

    <TearDown()> _
    Public Sub Clean()
    End Sub

#Region "Switch tests"
    <Test()> _
    Public Sub Switch_1()
        Dim CityName As String = "Rome"
        Dim res As String

        res = Interaction.Switch(CityName = "London", "English", CityName = "Rome", "Italian", CityName = "Paris", "French")
        Assert.AreEqual("Italian", res)

    End Sub

    <Test()> _
    Public Sub Switch_2()

        Dim caughtException As Boolean = False
        Dim CityName As String = Nothing

        Try
            Interaction.Switch(CityName = "London", CityName = "Rome", "Italian", CityName = "Paris", "French")
        Catch e As ArgumentException
            Assert.AreEqual("Argument 'VarExpr' is not a valid value.", e.Message)
            caughtException = True
        End Try
        Assert.AreEqual(True, caughtException)
    End Sub

    <Test()> _
    Public Sub Switch_3()

        Dim res As String

        res = Interaction.Switch(0, "English", 0, "Italian", 342, "French")
        Assert.AreEqual("French", res)

    End Sub
    Public Class Test1

    End Class

#End Region

End Class



