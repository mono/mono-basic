'
' ApplicationBase.vb
'
' Authors:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
'
' Copyright (c) 2002-2007 Mainsoft Corporation.
' Copyright (C) 2004-2007 Novell, Inc (http://www.novell.com)
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


' We have a problem here
' The VB compiler refuses to compile this class because it conflicts with an assembly name
' while it tries to compile its internal code (MyInternalTemplate.vb or something like that)
'
Imports System
Imports System.Globalization
Imports System.Threading

Namespace Microsoft.VisualBasic.ApplicationServices
    Public Class ApplicationBase
        Private m_AssemblyInfo As AssemblyInfo
        Private m_Log As Logging.Log

        Public Sub New()

        End Sub

        Public Sub ChangeCulture(ByVal cultureName As String)
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName)
        End Sub

        Public Sub ChangeUICulture(ByVal cultureName As String)
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName)
        End Sub

        Public Function GetEnvironmentVariable(ByVal name As String) As String
            Return Environment.GetEnvironmentVariable(name)
        End Function

        Public ReadOnly Property Info() As AssemblyInfo
            Get
                If m_AssemblyInfo Is Nothing Then
                    m_AssemblyInfo = New AssemblyInfo(System.Reflection.Assembly.GetEntryAssembly)
                End If

                Return m_AssemblyInfo
            End Get
        End Property

        Public ReadOnly Property Culture() As CultureInfo
            Get
                Return Thread.CurrentThread.CurrentCulture
            End Get
        End Property

        Public ReadOnly Property Log() As Logging.Log
            Get
                If m_Log Is Nothing Then m_Log = New Logging.Log()
                Return m_Log
            End Get
        End Property

        Public ReadOnly Property UICulture() As CultureInfo
            Get
                Return Thread.CurrentThread.CurrentUICulture
            End Get
        End Property
    End Class

End Namespace
