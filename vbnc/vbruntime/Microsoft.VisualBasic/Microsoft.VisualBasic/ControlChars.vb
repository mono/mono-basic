'
' ControlChars.vb
'
' Author:
'   Chris J Breisch (cjbreisch@altavista.net)
'   Mizrahi Rafael (rafim@mainsoft.com)
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

Imports MS_VB = Microsoft.VisualBasic

Namespace Microsoft.VisualBasic
    Public NotInheritable Class ControlChars
        ' If you're building in Visual Studio and see errors here, you can ignore them
        ' building the project using F5 will succeed.
        Public Const Back As Char = Strings.Chr(8)
        Public Const Cr As Char = MS_VB.Strings.Chr(13)
        Public Const CrLf As String = Cr & Lf
        Public Const FormFeed As Char = MS_VB.Strings.Chr(12)
        Public Const Lf As Char = MS_VB.Strings.Chr(10)
        Public Const NewLine As String = CrLf
        Public Const NullChar As Char = Char.MinValue
        Public Const Tab As Char = MS_VB.Strings.Chr(9)
        Public Const VerticalTab As Char = MS_VB.Strings.Chr(11)
        Public Const Quote As Char = """"c
    End Class

End Namespace
