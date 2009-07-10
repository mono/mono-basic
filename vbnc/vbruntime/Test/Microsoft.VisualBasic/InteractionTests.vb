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
Imports Microsoft.Win32

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

#If Not TARGET_JVM Then
#Region "MsgBox tests"
    <Category("UI")> _
    <Test()> _
    Public Sub MsgBoxTest()
        Dim abort As MsgBoxResult = MsgBoxResult.Abort
        Dim b As MsgBoxResult = MsgBoxResult.Cancel
        Dim c As MsgBoxResult = MsgBoxResult.Ignore

        Assert.AreEqual(MsgBoxResult.Abort, MsgBox("Press Abort", MsgBoxStyle.AbortRetryIgnore), "abort")
        Assert.AreEqual(MsgBoxResult.Retry, MsgBox("Press Retry", MsgBoxStyle.AbortRetryIgnore), "retry")
        Assert.AreEqual(MsgBoxResult.Ignore, MsgBox("Press Ignore", MsgBoxStyle.AbortRetryIgnore), "ignore")
        Assert.AreEqual(MsgBoxResult.Ok, MsgBox("Press OK", MsgBoxStyle.OkCancel), "ok")
        Assert.AreEqual(MsgBoxResult.Cancel, MsgBox("Press Cancel", MsgBoxStyle.OkCancel), "cancel")
        Assert.AreEqual(MsgBoxResult.Yes, MsgBox("Press Yes", MsgBoxStyle.YesNo), "yes")
        Assert.AreEqual(MsgBoxResult.No, MsgBox("Press No", MsgBoxStyle.YesNoCancel), "no")
    End Sub
#End Region
#End If

#Region "Partition tests"

    <Test()> _
    Public Sub Partition_1()
        Dim str1 As String
        str1 = Interaction.Partition(1, 0, 9, 5)
        str1 = str1 + Interaction.Partition(1, 0, 9, 5)
        str1 = str1 + Interaction.Partition(1, 20, 199, 10)
        str1 = str1 + Interaction.Partition(1, 100, 1010, 20)

        Assert.AreEqual(" 0: 4 0: 4   : 19    :  99", str1)
    End Sub

    <Test()> _
    <ExpectedException(GetType(ArgumentException))> _
    Public Sub Partition_2()
        Dim str_tmp As String
        str_tmp = Interaction.Partition(12, 5, 3, 2)
    End Sub

    <Test()> _
    <ExpectedException(GetType(ArgumentException))> _
  Public Sub Partition_3()
        Dim str_tmp As String
        str_tmp = Interaction.Partition(12, 5, 7, 0)
    End Sub

    <Test()> _
  Public Sub Partition_4()
        Dim str1 As String = ""
        str1 = str1 + Interaction.Partition(267, 100, 24469, 1)

        Assert.AreEqual("  267:  267", str1)
    End Sub

#End Region

#If Not TARGET_JVM Then
#Region "GetAllSettings tests"

    <Test()> _
Public Sub GetAllSettings_1()
        Dim res_setting As String(,)
        Dim index, elm_count As Integer
        Dim tmp_str As String
        Dim regk As RegistryKey
        Dim arr_str As String()


        regk = Registry.CurrentUser
        regk = regk.CreateSubKey("Test_APP")
        regk = regk.OpenSubKey("GetAllSettings_1")

        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go1", "Val_Go1")
        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go2", "Val_Go2")
        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go3", "Val_Go3")

        res_setting = Interaction.GetAllSettings("Test_APP", "GetAllSettings_1")

        Assert.AreEqual("Go2", res_setting(1, 0))
        Assert.AreEqual("Val_Go2", res_setting(1, 1))

    End Sub

    <Test()> _
  Public Sub GetAllSettings_2()
        Dim res_setting As String(,)

        res_setting = Interaction.GetAllSettings("Test_APP", "rterr")

        Assert.AreEqual(Nothing, res_setting)
    End Sub

    <Test()> _
    <ExpectedException(GetType(ArgumentException))> _
    Public Sub GetAllSettings_3()
        Dim str_tmp As String(,)
        str_tmp = Interaction.GetAllSettings("", "TEST2")
    End Sub

    <Test()> _
    <ExpectedException(GetType(ArgumentException))> _
    Public Sub GetAllSettings_4()
        Dim str_tmp As String(,)
        str_tmp = Interaction.GetAllSettings("TEST", Nothing)
    End Sub

#End Region
#End If
End Class



