'
' GlobalsTests.vb - NUnit Test Cases for Microsoft.VisualBasic.Global
'
' Rolf Bjarne Kvinge (RKvinge@novell.com)
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

<TestFixture()> _
Public Class GlobalsTests
    <Test()> _
    Public Sub ScriptEngineTest()
        Assert.AreEqual("VB", Microsoft.VisualBasic.Globals.ScriptEngine, "#1")
    End Sub

    <Test()> _
    Public Sub ScriptEngineBuildVersionTest()
#If NET_VER >= 4.0 Then
        Assert.AreEqual(30319, Microsoft.VisualBasic.Globals.ScriptEngineBuildVersion, "#1")
#Else
        Assert.AreEqual(50727, Microsoft.VisualBasic.Globals.ScriptEngineBuildVersion, "#1")
#End If
    End Sub

    <Test()> _
    Public Sub ScriptEngineMajorVersionTest()
#If NET_VER >= 4.0 Then
        Assert.AreEqual(10, Microsoft.VisualBasic.Globals.ScriptEngineMajorVersion, "#1")
#Else
        Assert.AreEqual(8, Microsoft.VisualBasic.Globals.ScriptEngineMajorVersion, "#1")
#End If
    End Sub

    <Test()> _
    Public Sub ScriptEngineMinorVersionTest()
        Assert.AreEqual(0, Microsoft.VisualBasic.Globals.ScriptEngineMinorVersion, "#1")
    End Sub
End Class
