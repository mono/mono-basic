' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

Partial Public Class Emitter
    Structure DecimalFields
        Public Scale As Byte
        Public Sign As Byte
        Public Hi As UInteger
        Public Lo As UInteger
        Public Mid As UInteger
        Public Value As Decimal

        Function SignAsBit() As Integer
            If Sign = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Sub New(ByVal value As Decimal)
            Dim bits() As Integer = Decimal.GetBits(value)
            Dim args(4) As Object
            Scale = CByte((bits(3) >> 16) And &HFF)
            Sign = CByte((bits(3) >> 31) And 1) << 7
            Hi = BitConverter.ToUInt32(BitConverter.GetBytes(bits(2)), 0)
            Mid = BitConverter.ToUInt32(BitConverter.GetBytes(bits(1)), 0)
            Lo = BitConverter.ToUInt32(BitConverter.GetBytes(bits(0)), 0)

            Me.Value = value
        End Sub

        ReadOnly Property AsByte_Byte_UInt32_UInt32_UInt32() As Object()
            Get
                Dim result(4) As Object

                result(0) = Scale
                result(1) = Sign
                result(2) = Hi
                result(3) = Mid
                result(4) = Lo

#If DEBUG Then
                Dim test As New Runtime.CompilerServices.DecimalConstantAttribute(Scale, Sign, Hi, Mid, Lo)
                Helper.Assert(test.Value = Value)
#End If
                Return result
            End Get
        End Property
    End Structure
End Class