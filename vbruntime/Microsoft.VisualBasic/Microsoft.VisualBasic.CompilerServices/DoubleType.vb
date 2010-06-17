'
' DoubleType.vb
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

Imports System
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.CompilerServices
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class DoubleType

        Private Sub New()
        End Sub

        ' FromObject(Value) -> FromObject(Value, Nothing)
        Public Shared Function FromObject(ByVal Value As Object) As Double
            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

            Return DoubleType.FromObject(Value, Nothing)

        End Function
        Public Shared Function FromObject(ByVal Value As Object, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Double
            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If
            If TypeOf Value Is Boolean Then
                Return ((-1D) * (Convert.ToDouble(Value)))
            End If

            If TypeOf Value Is String Then
                Return FromString(DirectCast(Value, String), NumberFormat)
            End If

            Return Convert.ToDouble(Value)

        End Function

        Public Shared Function FromString(ByVal Value As String) As Double
            Return DoubleType.FromString(Value, Nothing)
        End Function

        Public Shared Function FromString(ByVal Value As String, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Double
            Try
                If NumberFormat Is Nothing Then NumberFormat = Threading.Thread.CurrentThread.CurrentCulture.NumberFormat
                Return DoubleType.Parse(Value, NumberFormat)
            Catch ex As Exception
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), Value, "Double"), ex)
            End Try
        End Function

        Public Shared Function Parse(ByVal Value As String) As Double
            Return DoubleType.Parse(Value, Nothing)
        End Function

        Public Shared Function Parse(ByVal Value As String, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Double
            'implicit casting of Nothing to Double returns 0
            If (Value Is Nothing) Then
                Return 0
            End If

            Return Double.Parse(Value, NumberFormat)
        End Function

        Friend Shared Function TryParse(ByVal value As String, <OutAttribute()> ByRef result As Double) As Boolean
            'Grasshopper still does not support Double.TryParse
#If TARGET_JVM = False Then
            Return Double.TryParse(value, result)
#Else
            Try
                result = Double.Parse(value)
                Return True
            Catch ex As Exception
                Return False
            End Try
#End If
        End Function
    End Class

End Namespace
