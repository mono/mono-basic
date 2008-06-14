'
' ByteType.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Guy Cohen      (guyc@mainsoft.com)

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

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class ByteType
        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromString(ByVal value As String) As Byte
            Try
                If value Is Nothing Then Return 0

                Return Byte.Parse(value)
            Catch ex As FormatException
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), value, "Byte"), ex)
            End Try
        End Function

        Public Shared Function FromObject(ByVal Value As Object) As Byte
            If Value Is Nothing Then
                Return 0
            End If
            If TypeOf Value Is Boolean Then
                Return ((Convert.ToByte(-1)) * (Convert.ToByte(Value)))
            End If

            Return Convert.ToByte(Value)
        End Function
    End Class
End Namespace