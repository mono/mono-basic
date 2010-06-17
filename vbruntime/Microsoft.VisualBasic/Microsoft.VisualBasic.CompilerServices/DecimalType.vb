'
' DecimalType.vb
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
    Public NotInheritable Class DecimalType
        '
        ' core logic is at Parse
        '
        Private Sub New()
            'Nobody should see constructor
        End Sub
        Public Shared Function FromBoolean(ByVal Value As Boolean) As Decimal
            If Value Then
                Return -1
            Else
                Return 0
            End If
        End Function
        ' FromObject(Value) -> FromObject(Value, Nothing)
        Public Shared Function FromObject(ByVal Value As Object) As Decimal
            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

#If TRACE Then
            Console.WriteLine("TRACE:DecimalType.FromObject:Value:" + Value.ToString)
#End If

            Return DecimalType.FromObject(Value, Nothing)

        End Function
        Public Shared Function FromObject(ByVal Value As Object, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Decimal
            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

            Dim type1 As Type = Value.GetType()
            Select Case Type.GetTypeCode(type1)
                Case TypeCode.Boolean
                    Return (-1) * Convert.ToDecimal(DirectCast(Value, Boolean))
                Case TypeCode.Byte
                    Return Convert.ToDecimal(DirectCast(Value, Byte))
                Case TypeCode.Double
                    Return Convert.ToDecimal(DirectCast(Value, Double))
                Case TypeCode.Decimal
                    Return DirectCast(Value, Decimal)
                Case TypeCode.Int32
                    Return Convert.ToDecimal(DirectCast(Value, Integer))
                Case TypeCode.Int16
                    Return Convert.ToDecimal(DirectCast(Value, Short))
                Case TypeCode.Int64
                    Return Convert.ToDecimal(DirectCast(Value, Long))
                Case TypeCode.Single
                    Return Convert.ToDecimal(DirectCast(Value, Single))
                Case TypeCode.String
                    Return Parse(DirectCast(Value, String), NumberFormat)
                Case TypeCode.SByte
                    Return Convert.ToDecimal(DirectCast(Value, SByte))
                Case TypeCode.UInt16
                    Return Convert.ToDecimal(DirectCast(Value, UShort))
                Case TypeCode.UInt32
                    Return Convert.ToDecimal(DirectCast(Value, UInteger))
                Case TypeCode.UInt64
                    Return Convert.ToDecimal(DirectCast(Value, ULong))
                Case Else
                    Throw New InvalidCastException
            End Select

        End Function
        ' FromString(Value) -> FromString(Value, Nothing)
        ' FromString(Value, Nothing) -> Parse(Value, NumberFormat)
        Public Shared Function FromString(ByVal Value As String) As Decimal

            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

            Try
                Return FromString(Value, Nothing)
            Catch ex As Exception
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), Value, "Decimal"))
            End Try
        End Function
        Public Shared Function FromString(ByVal Value As String, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Decimal
            Return Parse(Value, NumberFormat)
        End Function
        Public Shared Function Parse(ByVal Value As String, ByVal NumberFormat As System.Globalization.NumberFormatInfo) As Decimal
            Return Decimal.Parse(Value, NumberFormat)
        End Function
    End Class

End Namespace
