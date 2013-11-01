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


Public Class TypeConverter

    '0 = A Empty          A
    '1 = B Object         B
    '2 = C DBNull         C
    '3 = D Boolean        D
    '4 = E Char           E
    '5 = F SByte          F
    '6 = G Byte           G
    '7 = H Int16(Short)   H
    '8 = I UInt16(UShort) I
    '9 = J Int32          J
    '10= K UInt32         K 
    '11= L Int64(Long)    L
    '12= M UInt64(ULong)  M
    '13= N Single         N
    '14= O Double         O
    '15= P Decimal        P
    '16= Q DateTime       Q
    '17= - 17             -
    '18= S String         S

    Public Shared LikeDefinedTypes As String = "S"
    Public Shared LikeResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBBBBBBBBBBBBBBBB-B" & _
            "XBXXXXXXXXXXXXXXX-X" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "XBXDDDDDDDDDDDDDD-D" & _
            "-------------------" & _
            "XBXDDDDDDDDDDDDDD-D"
    Public Shared LikeOperandType As String = "" & _
                "XXXXXXXXXXXXXXXXX-X" & _
                "XBBBBBBBBBBBBBBBB-B" & _
                "XBXXXXXXXXXXXXXXX-X" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "XBXSSSSSSSSSSSSSS-S" & _
                "-------------------" & _
                "XBXSSSSSSSSSSSSSS-S"

    Public Shared ConcatResultType As String = LikeOperandType
    Public Shared ConcatDefinedTypes As String = "S"
    Public Shared ConcatOperandType As String = LikeOperandType

    Public Shared ModResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared ModDefinedTypes As String = "FGHIJKLMNOP"

    Public Shared IntDivResultTypes As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLLLLLX-L" & _
            "XBXHXHGHIJKLMLLLX-L" & _
            "XBXHXHHHJJLLLLLLX-L" & _
            "XBXJXJIJIJKLMLLLX-L" & _
            "XBXJXJJJJJLLLLLLX-L" & _
            "XBXLXLKLKLKLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLMLMLMLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXLXLLLLLLLLLLLX-L"
    Public Shared IntDivDefinedTypes As String = "FGHIJKLM"

    Public Shared RealDivResultTypes As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXOXOOOOOOOONOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared RealDivDefinedTypes As String = "NOP"

    Public Shared AddResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBBBBBBBBBBBBBBBB-B" & _
            "XBXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XBXXSXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XBXXXXXXXXXXXXXXS-S" & _
            "-------------------" & _
            "XBXOSOOOOOOOOOOOS-S"
    Public Shared AddDefinedTypes As String = "FGHIJKLMNOPS"

    Public Shared SubResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared SubDefinedTypes As String = "FGHIJKLMNOP"

    Public Shared MultResultType As String = SubResultType
    Public Shared MultDefinedTypes As String = "FGHIJKLMNOP"

    Public Shared ShortcircuitResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XBXDXDDDDDDDDDDDX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXDXDDDDDDDDDDDX-D"
    Public Shared ShortcircuitDefinedTypes As String = "D"

    Public Shared LogicalOperatorResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJLLLLLLX-D" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLLLLLX-L" & _
            "XBXHXHGHIJKLMLLLX-L" & _
            "XBXHXHHHJJLLLLLLX-L" & _
            "XBXJXJIJIJKLMLLLX-L" & _
            "XBXJXJJJJJLLLLLLX-L" & _
            "XBXLXLKLKLKLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLMLMLMLMLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XBXLXLLLLLLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXDXLLLLLLLLLLLX-L"
    Public Shared LogicalDefinedTypes As String = "DFGHIJKLM"

    Public Shared RelationalOperandTypes As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBBBBBBBBBBBBBBBB-B" & _
            "XBXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJLLPNOPX-D" & _
            "XBXXEXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJPLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJPLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJPLPNOPX-O" & _
            "XBXLXPKPKPKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNONX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPNOPX-O" & _
            "XBXXXXXXXXXXXXXXQ-Q" & _
            "-------------------" & _
            "XBXDSOOOOOOOOOOOQ-S"
    Public Shared RelationalDefinedTypes As String = "DEFGHIJKLMNOPQS"


    Public Shared ExponentResultTypes As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared ExponentDefinedTypes As String = "O"

    Public Shared NotOperatorResultType As String = "XBXDXFGHIJKLMLLLX-L"

    Public Shared UnaryPlusResultType As String = "XBXHXFGHIJKLMNOPX-O"

    Public Shared UnaryMinusResultType As String = "XBXHXFHHJJLLPNOPX-O"

    Public Shared ShiftDefinedTypes As String = "FGHIJKLM"
    Public Shared ShiftResultType As String = _
             "XXXXXXXXXXXXXXXXX-X" & _
             "XBBBXBBBBBBBBBBBX-B" & _
             "XBXXXXXXXXXXXXXXX-X" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXXXXXXXXXXXXXXX-X" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXHXFGHIJKLMLLLX-L" & _
             "XBXXXXXXXXXXXXXXX-X" & _
             "-------------------" & _
             "XBXHXFGHIJKLMLLLX-L"


    ''' <summary>
    ''' X=?
    ''' I=Implicit ok
    ''' 0=Explicit ok
    ''' 1=30311
    ''' 2=32007
    ''' 3=30533
    ''' 4=32006
    ''' 5=30532
    ''' 6=30533
    ''' A=30311, only explicit
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ConversionResultType As String = _
            "XXXXXXXXXXXXXXXXX-X" & _
            "X0X00000000000000-0" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XIXI1000000000001-0" & _
            "XIX1I222222221111-0" & _
            "XIX04I00000000001-0" & _
            "XIX040I0000000001-0" & _
            "XIX0400I000000001-0" & _
            "XIX04000I00000001-0" & _
            "XIX040000I0000001-0" & _
            "XIX0400000I000001-0" & _
            "XIX04000000I00001-0" & _
            "XIX040000000I0001-0" & _
            "XIX0100000000I001-0" & _
            "XIX01000000000I05-0" & _
            "XIX010000000000I1-0" & _
            "XIX1111111111161I-0" & _
            "-------------------" & _
            "XIX00000000000000-I"

    Shared Function GetErrorNumberForBinaryOperation(ByVal op As KS, ByVal left As TypeCode, ByVal right As TypeCode) As Integer
        If op = KS.ShiftLeft OrElse op = KS.ShiftRight Then
            If left = TypeCode.Object AndAlso (right = TypeCode.DateTime OrElse right = TypeCode.Char) Then Return 0
            If left <> TypeCode.DateTime AndAlso left <> TypeCode.Char Then
                If right = TypeCode.DateTime Then Return 30311
                If right = TypeCode.Char Then Return 32006
            End If
        ElseIf (op = KS.Add OrElse op = KS.Minus) AndAlso left = TypeCode.DateTime AndAlso right = TypeCode.DateTime Then
            Return 0
        End If

        Dim resultType As TypeCode
        resultType = TypeConverter.GetBinaryResultType(op, left, right)

        If resultType = TypeCode.Empty Then Return 30452
        Return 0
    End Function

    Shared Function GetErrorNumberForConversion(ByVal tp1 As TypeCode, ByVal tp2 As TypeCode, ByVal Implicit As Boolean) As Integer
        Select Case GetConversionResultType(tp1, tp2)
            Case "X"c
                Throw New NotImplementedException
            Case "I"c
                Return 0
            Case "0"c
                Return 0
            Case "1"c
                Return 30311
            Case "2"c
                Return 32007
            Case "3"c
                Return 30533
            Case "4"c
                Return 32006
            Case "5"c
                Return 30532
            Case "6"c
                Return 30533
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Shared Function GetConversionResultType(ByVal tp1 As TypeCode, ByVal tp2 As TypeCode) As Char
        Return GetArrayChar(tp1, tp2, ConversionResultType)
    End Function

    Shared Function GetExpOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetExpResultType(op1, op2)
    End Function

    Shared Function GetEqualsOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetLTOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetGTOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetLEOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetGEOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetNotEqualsOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RelationalOperandTypes)
    End Function

    Shared Function GetShiftResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ShiftResultType)
    End Function

    Shared Function GetModResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ModResultType)
    End Function

    Shared Function GetLikeResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, LikeResultType)
    End Function

    Shared Function GetLikeOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, LikeOperandType)
    End Function

    Shared Function GetConcatResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ConcatResultType)
    End Function

    Shared Function GetConcatOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ConcatOperandType)
    End Function

    Shared Function GetRealDivResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, RealDivResultTypes)
    End Function

    Shared Function GetIntDivResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, IntDivResultTypes)
    End Function

    Shared Function GetExpResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ExponentResultTypes)
    End Function

    Shared Function GetEqualsResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetEqualsOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetLTResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetLTOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetGTResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetGTOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetLEResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetLEOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetGEResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetGEOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetNotEqualsResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Dim tp As TypeCode = GetNotEqualsOperandType(op1, op2)
        If tp = TypeCode.Empty OrElse tp = TypeCode.Object Then Return tp
        Return TypeCode.Boolean
    End Function

    Shared Function GetUnaryMinusResultType(ByVal op1 As TypeCode) As TypeCode
        Return GetResultType(op1, UnaryMinusResultType)
    End Function

    Shared Function GetUnaryPlusResultType(ByVal op1 As TypeCode) As TypeCode
        Return GetResultType(op1, UnaryPlusResultType)
    End Function

    Shared Function GetAndResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, LogicalOperatorResultType)
    End Function

    Shared Function GetOrResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, LogicalOperatorResultType)
    End Function

    Shared Function GetXorResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, LogicalOperatorResultType)
    End Function

    Shared Function GetUnaryNotResultType(ByVal op1 As TypeCode) As TypeCode
        Return GetResultType(op1, NotOperatorResultType)
    End Function

    Shared Function GetOrElseResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ShortcircuitResultType)
    End Function

    Shared Function GetAndAlsoResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, ShortcircuitResultType)
    End Function

    Shared Function GetMultResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, MultResultType)
    End Function

    Shared Function GetBinaryAddResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, AddResultType)
    End Function

    Shared Function GetBinarySubResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return GetResultType(op1, op2, SubResultType)
    End Function

    Shared Function GetIsIsNotOperandType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return TypeCode.Object
    End Function

    Shared Function GetIsIsNotResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Return TypeCode.Boolean
    End Function

    Shared Function GetUnaryResultType(ByVal op As KS, ByVal op1 As TypeCode) As TypeCode
        Select Case op
            Case KS.Add
                Return GetUnaryPlusResultType(op1)
            Case KS.Minus
                Return GetUnaryMinusResultType(op1)
            Case KS.Not
                Return GetUnaryNotResultType(op1)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Shared Function GetBinaryResultType(ByVal op As KS, ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Select Case op
            Case KS.And
                Return GetAndResultType(op1, op2)
            Case KS.AndAlso
                Return GetAndAlsoResultType(op1, op2)
            Case KS.Or
                Return GetOrResultType(op1, op2)
            Case KS.OrElse
                Return GetOrElseResultType(op1, op2)
            Case KS.Xor
                Return GetXorResultType(op1, op2)
            Case KS.Add
                Return GetBinaryAddResultType(op1, op2)
            Case KS.Minus
                Return GetBinarySubResultType(op1, op2)
            Case KS.Mult
                Return GetMultResultType(op1, op2)
            Case KS.RealDivision
                Return GetRealDivResultType(op1, op2)
            Case KS.IntDivision
                Return GetIntDivResultType(op1, op2)
            Case KS.Power
                Return GetExpOperandType(op1, op2)
            Case KS.Concat
                Return GetConcatResultType(op1, op2)
            Case KS.GE
                Return GetGEResultType(op1, op2)
            Case KS.GT
                Return GetGTResultType(op1, op2)
            Case KS.LE
                Return GetLEResultType(op1, op2)
            Case KS.LT
                Return GetLTResultType(op1, op2)
            Case KS.Equals
                Return GetEqualsResultType(op1, op2)
            Case KS.NotEqual
                Return GetNotEqualsResultType(op1, op2)
            Case KS.ShiftLeft, KS.ShiftRight
                Return GetShiftResultType(op1, op2)
            Case KS.Mod
                Return GetModResultType(op1, op2)
            Case KS.Like
                Return GetLikeResultType(op1, op2)
            Case KS.Is, KS.IsNot
                Return GetIsIsNotResultType(op1, op2)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Shared Function GetUnaryOperandType(ByVal op As KS, ByVal operand As TypeCode) As TypeCode
        Select Case op
            Case KS.Add
                Return GetUnaryPlusResultType(operand)
            Case KS.Minus
                Return GetUnaryMinusResultType(operand)
            Case KS.Not
                Return GetUnaryNotResultType(operand)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Shared Function GetBinaryOperandType(ByVal Compiler As Compiler, ByVal op As KS, ByVal op1 As Mono.Cecil.TypeReference, ByVal op2 As Mono.Cecil.TypeReference) As TypeCode
        Dim result As TypeCode

        result = GetBinaryOperandType(op, Helper.GetTypeCode(Compiler, op1), Helper.GetTypeCode(Compiler, op2))

        If result = TypeCode.Object Then
            Dim isIntrinsic1, isIntrinsic2 As Boolean
            Dim conv1, conv2 As TypeCode()
            Dim defs As String

            isIntrinsic1 = Helper.IsIntrinsicType(Compiler, op1)
            isIntrinsic2 = Helper.IsIntrinsicType(Compiler, op2)

            If isIntrinsic1 = isIntrinsic2 Then Return result

            defs = GetBinaryOperandDefinedTypes(op)

            Dim conversions As New Generic.List(Of TypeCode)
            If isIntrinsic1 = False Then
                conv1 = TypeResolution.GetIntrinsicTypesImplicitlyConvertibleFrom(Compiler, op1)
                If conv1 IsNot Nothing Then
                    For i As Integer = 0 To conv1.Length - 1
                        Dim chr As Char = GetCharOfTypeCode(conv1(i))
                        If defs.IndexOf(chr) >= 0 Then conversions.Add(conv1(i))
                    Next
                    If conversions.Count = 1 Then
                        result = GetBinaryOperandType(op, conversions(0), Helper.GetTypeCode(Compiler, op2))
                    End If
                End If
            End If
            If isIntrinsic2 = False Then
                conv2 = TypeResolution.GetIntrinsicTypesImplicitlyConvertibleFrom(Compiler, op2)
                If conv2 IsNot Nothing Then
                    For i As Integer = 0 To conv2.Length - 1
                        Dim chr As Char = GetCharOfTypeCode(conv2(i))
                        If defs.IndexOf(chr) >= 0 Then conversions.Add(conv2(i))
                    Next
                    If conversions.Count = 1 Then
                        result = GetBinaryOperandType(op, Helper.GetTypeCode(Compiler, op1), conversions(0))
                    End If
                End If
            End If

        End If

        Return result
    End Function

    Shared Function GetBinaryOperandDefinedTypes(ByVal op As KS) As String
        Select Case op
            Case KS.And
                Return LogicalDefinedTypes
            Case KS.AndAlso
                Return ShortcircuitDefinedTypes
            Case KS.Or
                Return LogicalDefinedTypes
            Case KS.OrElse
                Return ShortcircuitDefinedTypes
            Case KS.Xor
                Return LogicalDefinedTypes
            Case KS.Add
                Return AddDefinedTypes
            Case KS.Minus
                Return SubDefinedTypes
            Case KS.Mult
                Return MultDefinedTypes
            Case KS.RealDivision
                Return RealDivDefinedTypes
            Case KS.IntDivision
                Return IntDivDefinedTypes
            Case KS.Power
                Return ExponentDefinedTypes
            Case KS.Concat
                Return ConcatDefinedTypes
            Case KS.GE
                Return RelationalDefinedTypes
            Case KS.GT
                Return RelationalDefinedTypes
            Case KS.LE
                Return RelationalDefinedTypes
            Case KS.LT
                Return RelationalDefinedTypes
            Case KS.Equals
                Return RelationalDefinedTypes
            Case KS.NotEqual
                Return RelationalDefinedTypes
            Case KS.ShiftLeft, KS.ShiftRight
                Return ShiftDefinedTypes
            Case KS.Mod
                Return ModDefinedTypes
            Case KS.Like
                Return LikeDefinedTypes
            Case KS.Is, KS.IsNot
                Return String.Empty
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Private Shared Function GetBinaryOperandType(ByVal op As KS, ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        Select Case op
            Case KS.And
                Return GetAndResultType(op1, op2)
            Case KS.AndAlso
                Return GetAndAlsoResultType(op1, op2)
            Case KS.Or
                Return GetOrResultType(op1, op2)
            Case KS.OrElse
                Return GetOrElseResultType(op1, op2)
            Case KS.Xor
                Return GetXorResultType(op1, op2)
            Case KS.Add
                Return GetBinaryAddResultType(op1, op2)
            Case KS.Minus
                Return GetBinarySubResultType(op1, op2)
            Case KS.Mult
                Return GetMultResultType(op1, op2)
            Case KS.RealDivision
                Return GetRealDivResultType(op1, op2)
            Case KS.IntDivision
                Return GetIntDivResultType(op1, op2)
            Case KS.Power
                Return GetExpOperandType(op1, op2)
            Case KS.Concat
                Return GetConcatOperandType(op1, op2)
            Case KS.GE
                Return GetGEOperandType(op1, op2)
            Case KS.GT
                Return GetGTOperandType(op1, op2)
            Case KS.LE
                Return GetLEOperandType(op1, op2)
            Case KS.LT
                Return GetLTOperandType(op1, op2)
            Case KS.Equals
                Return GetEqualsOperandType(op1, op2)
            Case KS.NotEqual
                Return GetNotEqualsOperandType(op1, op2)
            Case KS.ShiftLeft, KS.ShiftRight
                Return GetShiftResultType(op1, op2)
            Case KS.Mod
                Return GetModResultType(op1, op2)
            Case KS.Like
                Return GetLikeOperandType(op1, op2)
            Case KS.Is, KS.IsNot
                Return GetIsIsNotOperandType(op1, op2)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Private Shared Function GetArrayChar(ByVal op1 As TypeCode, ByVal op2 As TypeCode, ByVal array As String) As Char
        Return array.Chars(op1 + op2 * 19)
    End Function

    Private Shared Function GetResultType(ByVal op1 As TypeCode, ByVal array As String) As TypeCode
        Dim chr As Char
        chr = array.Chars(op1)
        If chr = "X"c Then
            Return Nothing
        Else
            Return GetTypeCodeOfChar(chr)
        End If
    End Function

    Private Shared Function GetTypeCodeOfChar(ByVal chr As Char) As TypeCode
        Return CType(Microsoft.VisualBasic.Asc(chr) - Microsoft.VisualBasic.Asc("A"c), TypeCode)
    End Function

    Private Shared Function GetCharOfTypeCode(ByVal code As TypeCode) As Char
        Return Microsoft.VisualBasic.Chr(code + Microsoft.VisualBasic.Asc("A"c))
    End Function

    Private Shared Function GetResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode, ByVal array As String) As TypeCode
        Dim chr As Char
        chr = array.Chars(op1 + op2 * 19)
        If chr = "X"c Then
            Return Nothing
        Else
            Return CType(Microsoft.VisualBasic.Asc(chr) - Microsoft.VisualBasic.Asc("A"), TypeCode)
        End If
    End Function

    ''' <summary>
    ''' Converts the source to the destination type. Compiletime conversions are the only ones that succeeds.
    ''' Returns nothing if no conversion possible.
    ''' </summary>
    ''' <param name="Source"></param>
    ''' <param name="Destination"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTo(ByVal Context As ParsedObject, ByVal Source As Object, ByVal Destination As Mono.Cecil.TypeReference, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        If Destination Is Nothing OrElse Source Is Nothing Then
            result = Source
            Return True
        End If

        Helper.Assert(Source IsNot Nothing)
        Helper.Assert(Destination IsNot Nothing)

        If TypeOf Destination Is ByReferenceType Then
            Destination = DirectCast(Destination, ByReferenceType).ElementType
        End If

        Dim dtc As TypeCode = Helper.GetTypeCode(Context.Compiler, Destination)
        Dim stc As TypeCode = Helper.GetTypeCode(Context.Compiler, CecilHelper.GetType(Context.Compiler, Source))

        'Console.WriteLine("ConvertTo: from " & stc.ToString() & " to " & dtc.ToString)

        If dtc = stc Then
            result = Source
            Return True
        End If

        Select Case dtc
            Case TypeCode.Boolean
                Return ConvertToBoolean(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Byte
                Return ConvertToByte(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Char
                Return ConvertToChar(Context, Source, stc, result, ShowErrors)
            Case TypeCode.DateTime
                Return ConvertToDateTime(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Decimal
                Return ConvertToDecimal(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Double
                Return ConvertToDouble(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Int16
                Return ConvertToInt16(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Int32
                Return ConvertToInt32(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Int64
                Return ConvertToInt64(Context, Source, stc, result, ShowErrors)
            Case TypeCode.SByte
                Return ConvertToSByte(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Single
                Return ConvertToSingle(Context, Source, stc, result, ShowErrors)
            Case TypeCode.String
                Return ConvertToString(Context, Source, stc, result, ShowErrors)
            Case TypeCode.UInt16
                Return ConvertToUInt16(Context, Source, stc, result, ShowErrors)
            Case TypeCode.UInt32
                Return ConvertToUInt32(Context, Source, stc, result, ShowErrors)
            Case TypeCode.UInt64
                Return ConvertToUInt64(Context, Source, stc, result, ShowErrors)
            Case TypeCode.Object
                result = Source
                Return True
            Case Else
                'This should never happen 
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, CObj(dtc).ToString())
        End Select
    End Function

    Public Shared Function ConvertToBoolean(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                result = Source
                Return True
            Case TypeCode.Byte
                result = CBool(DirectCast(Source, Byte))
                Return True
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Char", "Boolean")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", "Boolean")
            Case TypeCode.Decimal
                result = CBool(DirectCast(Source, Decimal))
                Return True
            Case TypeCode.Double
                result = CBool(DirectCast(Source, Double))
                Return True
            Case TypeCode.Int16
                result = CBool(DirectCast(Source, Short))
                Return True
            Case TypeCode.Int32
                result = CBool(DirectCast(Source, Integer))
                Return True
            Case TypeCode.Int64
                result = CBool(DirectCast(Source, Long))
                Return True
            Case TypeCode.SByte
                result = CBool(DirectCast(Source, SByte))
                Return True
            Case TypeCode.Single
                result = CBool(DirectCast(Source, Single))
                Return True
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Byte", "String")
            Case TypeCode.UInt16
                result = CBool(DirectCast(Source, UShort))
                Return True
            Case TypeCode.UInt32
                result = CBool(DirectCast(Source, UInteger))
                Return True
            Case TypeCode.UInt64
                result = CBool(DirectCast(Source, ULong))
                Return True
            Case TypeCode.DBNull
                result = CBool(Nothing)
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Boolean")
    End Function

    Public Shared Function ConvertToByte(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CByte(i)
                Return True
            Case TypeCode.Byte
                result = Source
                Return True
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, "Byte")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", "Byte")
            Case TypeCode.Decimal
                Dim i As Decimal = DirectCast(Source, Decimal)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.Double
                Dim i As Double = DirectCast(Source, Double)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.Int16
                Dim i As Short = DirectCast(Source, Short)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.Int32
                Dim i As Integer = DirectCast(Source, Integer)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.Int64
                Dim i As Long = DirectCast(Source, Long)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.SByte
                Dim i As SByte = DirectCast(Source, SByte)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.Single
                Dim i As Single = DirectCast(Source, Single)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Byte", "String")
            Case TypeCode.UInt16
                Dim i As UShort = DirectCast(Source, UShort)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.UInt32
                Dim i As UInteger = DirectCast(Source, UInteger)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.UInt64
                Dim i As ULong = DirectCast(Source, ULong)
                If i >= Byte.MinValue AndAlso i <= Byte.MaxValue Then
                    result = CByte(i)
                    Return True
                End If
            Case TypeCode.DBNull
                result = CByte(0)
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Byte")
    End Function

    Public Shared Function ConvertToChar(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Boolean", "Char")
            Case TypeCode.Byte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "Byte")
            Case TypeCode.Char
                result = Source
                Return True
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "DateTime", "Char")
            Case TypeCode.Decimal
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Decimal", "Char")
            Case TypeCode.Double
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Double", "Char")
            Case TypeCode.Int16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "Short")
            Case TypeCode.Int32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "Integer")
            Case TypeCode.Int64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "Long")
            Case TypeCode.SByte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "SByte")
            Case TypeCode.Single
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Single", "Char")
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Dim str As String = DirectCast(Source, String)
                If str.Length >= 1 Then
                    result = str(0)
                Else
                    result = CChar(Nothing)
                End If
                Return True
            Case TypeCode.UInt16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "UShort")
            Case TypeCode.UInt32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "UInteger")
            Case TypeCode.UInt64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32007, Context.Location, "ULong")
            Case TypeCode.DBNull
                result = VB.Chr(0)
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Char")
    End Function

    Public Shared Function ConvertToDateTime(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Boolean", "Date")
            Case TypeCode.Byte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Byte", "Date")
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Char", "Date")
            Case TypeCode.DateTime
                result = Source
                Return True
            Case TypeCode.Decimal
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Decimal", "Date")
            Case TypeCode.Double
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30533, Context.Location)
            Case TypeCode.Int16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Short", "Date")
            Case TypeCode.Int32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Integer", "Date")
            Case TypeCode.Int64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Long", "Date")
            Case TypeCode.SByte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "SByte", "Date")
            Case TypeCode.Single
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Single", "Date")
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "String", "Date")
            Case TypeCode.UInt16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "UShort", "Date")
            Case TypeCode.UInt32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "UInteger", "Date")
            Case TypeCode.UInt64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "ULong", "Date")
            Case TypeCode.DBNull
                If ShowErrors = False Then Return False
                result = New Date()
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Date")
    End Function

    Public Shared Function ConvertToDecimal(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CDec(i)
                Return True
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Char", "Decimal")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", "Decimal")
            Case TypeCode.Decimal
                result = Source
                Return True
            Case TypeCode.Double
                Dim i As Double = DirectCast(Source, Double)
                If i >= Decimal.MinValue AndAlso i <= Decimal.MaxValue Then
                    result = CDec(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                result = CDec(CLng(Source))
                Return True
            Case TypeCode.Single
                Dim i As Single = DirectCast(Source, Single)
                If i >= Decimal.MinValue AndAlso i <= Decimal.MaxValue Then
                    result = CDec(i)
                    Return True
                End If
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "String", "Decimal")
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                result = CDec(CULng(Source))
                Return True
            Case TypeCode.DBNull
                result = 0D
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Decimal")
    End Function

    Public Shared Function ConvertToDouble(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                result = CDbl(DirectCast(Source, Boolean))
                Return True
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Char", "Double")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30532, Context.Location)
            Case TypeCode.Decimal
                result = CDbl(DirectCast(Source, Decimal))
                Return True
            Case TypeCode.Double
                result = Source
                Return True
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                result = CDbl(CLng(Source))
                Return True
            Case TypeCode.Single
                result = CDbl(DirectCast(Source, Single))
                Return True
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "String", "Double")
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                result = CDbl(CULng(Source))
                Return True
            Case TypeCode.DBNull
                result = 0.0R
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Double")
    End Function

    Public Shared Function ConvertToInt16(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "Short"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CShort(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= Short.MinValue AndAlso i <= Short.MaxValue Then
                    result = CShort(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= Short.MinValue AndAlso i <= Short.MaxValue Then
                    result = CShort(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= Short.MinValue AndAlso i <= Short.MaxValue Then
                    result = CShort(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= Short.MinValue AndAlso i <= Short.MaxValue Then
                    result = CShort(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0S
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

    Public Shared Function ConvertToInt32(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "Integer"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CInt(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= Integer.MinValue AndAlso i <= Integer.MaxValue Then
                    result = CInt(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= Integer.MinValue AndAlso i <= Integer.MaxValue Then
                    result = CInt(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= Integer.MinValue AndAlso i <= Integer.MaxValue Then
                    result = CInt(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= Integer.MinValue AndAlso i <= Integer.MaxValue Then
                    result = CInt(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0I
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

    Public Shared Function ConvertToInt64(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "Long"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CLng(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= Long.MinValue AndAlso i <= Long.MaxValue Then
                    result = CLng(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= Long.MinValue AndAlso i <= Long.MaxValue Then
                    result = CLng(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= Long.MinValue AndAlso i <= Long.MaxValue Then
                    result = CLng(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= Long.MinValue AndAlso i <= Long.MaxValue Then
                    result = CLng(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0L
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

    Public Shared Function ConvertToSByte(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CSByte(i)
                Return True
            Case TypeCode.Byte
                Dim i As Byte = DirectCast(Source, Byte)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, "SByte")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", "SByte")
            Case TypeCode.Decimal
                Dim i As Decimal = DirectCast(Source, Decimal)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.Double
                Dim i As Double = DirectCast(Source, Double)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.Int16
                Dim i As Short = DirectCast(Source, Short)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.Int32
                Dim i As Integer = DirectCast(Source, Integer)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.Int64
                Dim i As Long = DirectCast(Source, Long)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.SByte
                result = Source
                Return True
            Case TypeCode.Single
                Dim i As Single = DirectCast(Source, Single)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "SByte", "String")
            Case TypeCode.UInt16
                Dim i As UShort = DirectCast(Source, UShort)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.UInt32
                Dim i As UInteger = DirectCast(Source, UInteger)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.UInt64
                Dim i As ULong = DirectCast(Source, ULong)
                If i >= SByte.MinValue AndAlso i <= SByte.MaxValue Then
                    result = CSByte(i)
                    Return True
                End If
            Case TypeCode.DBNull
                result = CSByte(0)
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "SByte")
    End Function

    Public Shared Function ConvertToSingle(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                result = CSng(DirectCast(Source, Boolean))
                Return True
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Char", "Single")
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", "Single")
            Case TypeCode.Decimal
                result = CSng(DirectCast(Source, Decimal))
                Return True
            Case TypeCode.Double
                Dim i As Double = DirectCast(Source, Double)
                If i >= Single.MinValue AndAlso i <= Single.MaxValue Then
                    result = CSng(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                result = CSng(CLng(Source))
                Return True
            Case TypeCode.Single
                result = Source
                Return True
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "String", "Single")
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                result = CSng(CULng(Source))
                Return True
            Case TypeCode.DBNull
                result = 0.0!
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "Single")
    End Function

    Public Shared Function ConvertToString(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Boolean", "String")
            Case TypeCode.Byte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Byte", "String")
            Case TypeCode.Char
                result = CStr(DirectCast(Source, Char))
                Return True
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Date", "String")
            Case TypeCode.Decimal
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Decimal", "String")
            Case TypeCode.Double
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Double", "String")
            Case TypeCode.Int16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Short", "String")
            Case TypeCode.Int32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Integer", "String")
            Case TypeCode.Int64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Long", "String")
            Case TypeCode.SByte
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "SByte", "String")
            Case TypeCode.Single
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "Single", "String")
            Case TypeCode.String
                result = Source
                Return True
            Case TypeCode.UInt16
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "UShort", "String")
            Case TypeCode.UInt32
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "UInteger", "String")
            Case TypeCode.UInt64
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, "ULong", "String")
            Case TypeCode.DBNull
                result = Nothing
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, "String")
    End Function

    Public Shared Function ConvertToUInt16(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "UShort"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CUShort(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= UShort.MinValue AndAlso i <= UShort.MaxValue Then
                    result = CUShort(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= UShort.MinValue AndAlso i <= UShort.MaxValue Then
                    result = CUShort(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= UShort.MinValue AndAlso i <= UShort.MaxValue Then
                    result = CUShort(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= UShort.MinValue AndAlso i <= UShort.MaxValue Then
                    result = CUShort(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0US
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

    Public Shared Function ConvertToUInt32(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "UInteger"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CUInt(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= UInteger.MinValue AndAlso i <= UInteger.MaxValue Then
                    result = CUInt(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= UInteger.MinValue AndAlso i <= UInteger.MaxValue Then
                    result = CUInt(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= UInteger.MinValue AndAlso i <= UInteger.MaxValue Then
                    result = CUInt(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= UInteger.MinValue AndAlso i <= UInteger.MaxValue Then
                    result = CUInt(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0UI
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

    Public Shared Function ConvertToUInt64(ByVal Context As ParsedObject, ByVal Source As Object, ByVal SourceTypeCode As TypeCode, ByRef result As Object, ByVal ShowErrors As Boolean) As Boolean
        Const DEST As String = "ULong"

        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Dim i As Boolean = DirectCast(Source, Boolean)
                result = CULng(i)
                Return True
            Case TypeCode.Decimal
                Dim i As Decimal = CDec(Source)
                If i >= ULong.MinValue AndAlso i <= ULong.MaxValue Then
                    result = CULng(i)
                    Return True
                End If
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim i As Long = CLng(Source)
                If i >= ULong.MinValue AndAlso i <= ULong.MaxValue Then
                    result = CULng(i)
                    Return True
                End If
            Case TypeCode.Double, TypeCode.Single
                Dim i As Double = CDbl(Source)
                If i >= ULong.MinValue AndAlso i <= ULong.MaxValue Then
                    result = CULng(i)
                    Return True
                End If
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Dim i As ULong = CULng(Source)
                If i >= ULong.MinValue AndAlso i <= ULong.MaxValue Then
                    result = CULng(i)
                    Return True
                End If
            Case TypeCode.Char
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC32006, Context.Location, DEST)
            Case TypeCode.DateTime
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30311, Context.Location, "Date", DEST)
            Case TypeCode.String
                If ShowErrors = False Then Return False
                Return Context.Compiler.Report.ShowMessage(Messages.VBNC30060, Context.Location, DEST, "String")
            Case TypeCode.DBNull
                result = 0UL
                Return True
        End Select
        If ShowErrors = False Then Return False
        Return Context.Compiler.Report.ShowMessage(Messages.VBNC30439, Context.Location, DEST)
    End Function

End Class

