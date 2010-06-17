' ShortTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.ShortType
'
' Rolf Bjarne Kvinge (RKvinge@novell.com)
'
' 
' Copyright (C) 2008 Novell, Inc (http:www.novell.com)
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
Imports Microsoft.VisualBasic


Namespace CompilerServices
    <TestFixture()> _
    Public Class DateTypeTest
        Sub New()
            Helper.SetThreadCulture()
        End Sub
        <Test()> _
        Public Sub FromString()
            Try
                Microsoft.VisualBasic.CompilerServices.DateType.FromString("#ERROR")
                Assert.Fail("Expected InvalidCastException", "#A1")
            Catch ex As InvalidCastException
                Assert.AreEqual("Conversion from string ""#ERROR"" to type 'Date' is not valid.", ex.Message, "#A2")
                Assert.IsNull(ex.InnerException, "#A3")
            End Try

            Try
                Microsoft.VisualBasic.CompilerServices.DateType.FromString(448)
                Assert.Fail("Expected InvalidCastException", "#B1")
            Catch ex As InvalidCastException
                Assert.AreEqual("Conversion from string ""448"" to type 'Date' is not valid.", ex.Message, "#B2")
                Assert.IsNull(ex.InnerException, "#B3")
            End Try
        End Sub

        <Test()> _
        Public Sub FromObject()
            Try
                Microsoft.VisualBasic.CompilerServices.DateType.FromObject("#ERROR")
                Assert.Fail("Expected InvalidCastException", "#A1")
            Catch ex As InvalidCastException
                Assert.AreEqual("Conversion from string ""#ERROR"" to type 'Date' is not valid.", ex.Message, "#A2")
                Assert.IsNull(ex.InnerException, "#A3")
            End Try

            Try
                Microsoft.VisualBasic.CompilerServices.DateType.FromObject(448)
                Assert.Fail("Expected InvalidCastException", "#B1")
            Catch ex As InvalidCastException
                Assert.AreEqual("Conversion from type 'Integer' to type 'Date' is not valid.", ex.Message, "#B2")
                Assert.IsNull(ex.InnerException, "#B3")
            End Try

            Try
                Microsoft.VisualBasic.CompilerServices.DateType.FromObject(New DateTypeTest())
                Assert.Fail("Expected InvalidCastException", "#C1")
            Catch ex As InvalidCastException
                Assert.AreEqual("Conversion from type 'DateTypeTest' to type 'Date' is not valid.", ex.Message, "#C2")
                Assert.IsNull(ex.InnerException, "#C3")
            End Try
        End Sub
    End Class
End Namespace