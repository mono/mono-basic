'
' ApplicationBase.vb
'
' Authors:
'   Mizrahi Rafael (rafim@mainsoft.com)
'

'
' Copyright (c) 2002-2006 Mainsoft Corporation.
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


' We have a problem here
' The VB compiler refuses to compile this class because it conflicts with an assembly name
' while it tries to compile its internal code (MyInternalTemplate.vb or something like that)
'
#If NET_2_0 Then
Imports System
Imports System.Globalization
Imports System.Threading

Namespace Microsoft.VisualBasic.ApplicationServices


    Public Class ApplicationBase

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

    End Class

End Namespace
#End If