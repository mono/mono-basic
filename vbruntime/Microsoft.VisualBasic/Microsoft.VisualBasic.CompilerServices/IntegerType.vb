'
' IntegerType.vb
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
    Public NotInheritable Class IntegerType
        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromObject(ByVal Value As Object) As Integer
            Try
                'implicit casting of Nothing returns 0
                If Value Is Nothing Then
                    Return 0
                End If

                Dim type1 As Type = Value.GetType()

                Select Case Type.GetTypeCode(type1)
                    Case TypeCode.Boolean
                        Return (-1) * Convert.ToInt32(DirectCast(Value, Boolean))
                    Case TypeCode.Byte
                        Return DirectCast(Value, Byte)
                    Case TypeCode.Double
                        Return Convert.ToInt32(DirectCast(Value, Double))
                    Case TypeCode.Decimal
                        Return Convert.ToInt32((DirectCast(Value, Decimal)))
                    Case TypeCode.Int32
                        Return DirectCast(Value, Integer)
                    Case TypeCode.Int16
                        Return DirectCast(Value, Short)
                    Case TypeCode.Int64
                        Return Convert.ToInt32(DirectCast(Value, Long))
                    Case TypeCode.Single
                        Return Convert.ToInt32(DirectCast(Value, Single))
                    Case TypeCode.String
                        Return IntegerType.FromString(DirectCast(Value, String))
                    Case TypeCode.SByte
                        Return DirectCast(Value, SByte)
                    Case TypeCode.UInt16
                        Return DirectCast(Value, UShort)
                    Case TypeCode.UInt32
                        Return Convert.ToInt32(DirectCast(Value, UInteger))
                    Case TypeCode.UInt64
                        Return Convert.ToInt32(DirectCast(Value, ULong))
                End Select
            Catch ex As Exception
            End Try
            Throw New InvalidCastException("Cast from '" + Value.GetType().Name + "' to type 'Integer' is not valid.")
        End Function
        Public Shared Function FromString(ByVal Value As String) As Integer

            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

#If TRACE Then
            System.Console.WriteLine("TRACE:IntegerType.FromString:Value:" + Value.ToString())
#End If

            Try
#If TRACE Then
                System.Console.WriteLine("TRACE:IntegerType.FromString:Value:" + Int32.Parse(Value).ToString())
#End If
                Return Int32.Parse(Value)
            Catch ex As Exception
                Throw New InvalidCastException(String.Format(Utils.GetResourceString("CastFromStringToType"), Value, "Integer"), ex)
            End Try

        End Function
    End Class
End Namespace
