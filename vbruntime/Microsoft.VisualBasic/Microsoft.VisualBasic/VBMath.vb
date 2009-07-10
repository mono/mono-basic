'
' VBMath.vb
'
' Author:
'   Chris J Breisch (cjbreisch@altavista.net) 
'   Francesco Delfino (pluto@tipic.com)
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
'

Imports System
Imports Microsoft.VisualBasic.CompilerServices

Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class VBMath

        ' Declarations
        ' Constructors
        ' Properties
        Private Shared m_rnd As Random = New Random
        Private Shared m_last As Single = CType(m_rnd.NextDouble(), Single)
        ' Methods
        Public Shared Function Rnd() As Single
            m_last = CType(m_rnd.NextDouble(), Single)
            Return m_last
        End Function
        Public Shared Function Rnd(ByVal Number As Single) As Single
            If Number = 0.0 Then
                Return m_last
            ElseIf Number < 0.0 Then
                'fd: What does this mean?
                'fd: ms-help://MS.VSCC/MS.MSDNVS/script56/html/vsstmRandomize
                'fd: ms-help://MS.VSCC/MS.MSDNVS/script56/html/vsfctrnd.htm
                Randomize(Number)
                Return Rnd()
            End If
            Return Rnd()
        End Function
        Public Shared Sub Randomize()
            m_rnd = New Random
        End Sub
        'TODO: Rethink the double => int conversion
        Public Shared Sub Randomize(ByVal Number As Double)
            m_rnd = New Random(CType(Number, Integer))
        End Sub
        ' Events
    End Class
End Namespace
