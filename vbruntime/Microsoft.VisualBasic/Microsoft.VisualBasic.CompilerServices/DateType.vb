'
' DateType.vb
'
' Author:
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

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class DateType

        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromObject(ByVal Value As Object) As Date

#If TRACE Then
            Console.WriteLine("TRACE:DateType.FromObject:input:" + Value.ToString())
#End If

            If Value Is Nothing Then
                Dim d As DateTime
                Return d
            End If

            If TypeOf Value Is String Then
                Return FromString(DirectCast(Value, String))
            End If

            If TypeOf Value Is Date Then
                Return DirectCast(Value, Date)
            End If
            Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromTypeToType"), Information.VBName(Value.GetType), "Date"))
        End Function

        Public Shared Function FromString(ByVal Value As String, ByVal culture As System.Globalization.CultureInfo) As Date
            Return DateTime.Parse(Value, culture)
        End Function

        Public Shared Function FromString(ByVal Value As String) As DateTime

#If TRACE Then
            Console.WriteLine("TRACE:DateType.FromString:input:" + Value)
            Console.WriteLine("TRACE:DateType.FromString:output:" + DateTime.Parse(Value).ToString())
#End If
            Try
                Return DateTime.Parse(Value)
            Catch ex As FormatException
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), Value, "Date"))
            End Try
        End Function
    End Class
End Namespace

