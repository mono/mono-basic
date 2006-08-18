'
' IntegerType.vb
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
Imports System.Runtime.InteropServices
Namespace Microsoft.VisualBasic.CompilerServices
    Public Class IntegerType
        Private Sub New()
            'Nobody should see constructor
        End Sub

        Public Shared Function FromObject(ByVal Value As Object) As Integer

            'implicit casting of Nothing returns 0
            If Value Is Nothing Then
                Return 0
            End If

            Dim type1 As Type = Value.GetType()
            Select Case Type.GetTypeCode(type1)
                Case TypeCode.Boolean
                    Return (-1) * Convert.ToInt32(CBool(Value))
                Case TypeCode.Byte
                    Return CByte(Value)
                Case TypeCode.Double
                    Return Convert.ToInt32(CDbl(Value))
                Case TypeCode.Decimal
                    Return Convert.ToInt32((CDec(Value)))
                Case TypeCode.Int32
                    Return Convert.ToInt32(Value)
                Case TypeCode.Int16
                    Return CShort(Value)
                Case TypeCode.Int64
                    Return Convert.ToInt32(CLng(Value))
                Case TypeCode.Single
                    Return Convert.ToInt32(CSng(Value))
                Case TypeCode.String
                    Return IntegerType.FromString(Value.ToString())
                Case Else
                    Throw New InvalidCastException
            End Select

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
                Throw New InvalidCastException("Cast from string """ + Value + """ to type 'Integer' is not valid.", ex)
            End Try

        End Function
    End Class
End Namespace
