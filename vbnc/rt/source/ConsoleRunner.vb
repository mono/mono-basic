' 
' Visual Basic.Net Compiler
' Copyright (C) 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Public Class ConsoleRunner
    Private m_Tests As Tests

    Public Shared Function Run(ByVal cmdArgs As String()) As Integer
        Dim c As New ConsoleRunner
        Return c.Run()
    End Function

    Public Function Run() As Integer
        LoadTests()
        RunTests()
    End Function

    Private Sub LoadTests()
        m_Tests = New Tests()
        m_Tests.Load("tests.xml")
        m_Tests.VBNCPath = "../../../class/lib/vbnc/vbnc.exe"
    End Sub

    Private Sub RunTests()
        Console.WriteLine("Running {0} tests...", m_Tests.Count)
        For Each Test As Test In m_Tests.Values
            Console.Write("Running {0}... ", Test.Name)
            'Test.DoTest()
            Console.WriteLine(Test.Result.ToString())
        Next
    End Sub
End Class
