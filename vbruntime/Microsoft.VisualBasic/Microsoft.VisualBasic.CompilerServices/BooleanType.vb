'
' BooleanType.vb
'
' Authors:
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

Imports System
Imports Microsoft.VisualBasic.CompilerServices
Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class BooleanType
        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromObject(ByVal Value As Object) As Boolean
            If Value Is Nothing Then
                Return False
            End If

            If TypeOf Value Is Boolean Then
                Return DirectCast(Value, Boolean)
            End If

            If TypeOf Value Is Integer Then
                Return DirectCast(Value, Integer) <> 0
            End If

            If TypeOf Value Is Double Then
                Return DirectCast(Value, Double) <> 0.0
            End If

            If TypeOf Value Is Byte Then
                Return DirectCast(Value, Byte) <> 0
            End If

            If TypeOf Value Is SByte Then
                Return DirectCast(Value, SByte) <> 0
            End If

            If TypeOf Value Is UInteger Then
                Return DirectCast(Value, UInteger) <> 0
            End If

            If TypeOf Value Is ULong Then
                Return DirectCast(Value, ULong) <> 0
            End If

            If TypeOf Value Is Long Then
                Return DirectCast(Value, Long) <> 0
            End If

            If TypeOf Value Is Short Then
                Return DirectCast(Value, Short) <> 0
            End If


            If TypeOf Value Is Decimal Then
                Return DirectCast(Value, Decimal) <> 0
            End If

            If TypeOf Value Is String Then
                Return FromString(DirectCast(Value, String))
            End If

            Throw New ArgumentException("Value")

        End Function

        Public Shared Function FromString(ByVal Value As String) As Boolean
            'BooleanType.FromString does not follow the pattern of implicit casting of Nothing returns 0
            '  so the following line should be left marked out.
            '  instead, an null value is converted into an empty string
            '  and passed to the Parse, resulting in FormatException
            '
            'If Value Is Nothing Then Return False
            If Value Is Nothing Then Value = ""

#If TRACE Then
            Console.WriteLine("TRACE:BooleanType.FromString:input:" + Value.ToString())
#End If

            If String.Compare(Value, "0") = 0 Then
                Return False
            End If


            ' if Value is 'True' or 'False'
            If String.Compare(Value.ToLower(), "true") = 0 Then
                Return True
            End If

            If String.Compare(Value.ToLower(), "false") = 0 Then
                Return False
            End If

            Try
                Dim retd As Double
                retd = DoubleType.Parse(Value)
                If (retd <> 0) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As System.FormatException
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), Value, "Boolean"), ex)
            End Try

        End Function
    End Class
End Namespace
