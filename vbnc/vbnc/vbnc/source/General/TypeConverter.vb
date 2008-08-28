' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
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
                "XBXBBBBBBBBBBBBBB-B" & _
                "XXXXXXXXXXXXXXXXX-X" & _
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
            "XBXFXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPPOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared ModDefinedTypes As String = "FGHIJKLMNOP"

    Public Shared IntDivResultTypes As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXHHJJJLLLLLLX-L" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXHXGHJJJLLLLLLX-L" & _
            "XBXHXHHIIJKLMLLLX-L" & _
            "XBXJXJIIJJLLLLLLX-L" & _
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
            "XBXOXOOOOOOOONOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXOXOOOOOOOONOOX-O" & _
            "XBXNXNNNNNNNNNOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "-------------------" & _
            "XBXOXOOOOOOOOOOOX-O"
    Public Shared RealDivDefinedTypes As String = "NOP"

    Public Shared AddResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXXSXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPPOPX-O" & _
            "XBXXXXXXXXXXXXXXX-S" & _
            "-------------------" & _
            "XBXOSOOOOOOOOOOOS-S"
    Public Shared AddDefinedTypes As String = "FGHIJKLMNOPS"

    Public Shared SubResultType As String = "" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXBXBBBBBBBBBBBX-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXFXFHHJJLLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJLLPNOPX-O" & _
            "XBXJXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJLLPNOPX-O" & _
            "XBXLXLKLKLKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOOX-O" & _
            "XBXPXPPPPPPPPPOPX-O" & _
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
            "XBXBBBBBBBBBBBBBB-B" & _
            "XXXXXXXXXXXXXXXXX-X" & _
            "XBXDXFHHJJPLPNOPX-D" & _
            "XBXXEXXXXXXXXXXXX-S" & _
            "XBXFXFHHJJPLPNOPX-O" & _
            "XBXHXHGHIJKLMNOPX-O" & _
            "XBXHXHHHJJPLPNOPX-O" & _
            "XBXIXJIJIJKLMNOPX-O" & _
            "XBXJXJJJJJPLPNOPX-O" & _
            "XBXPXPKPKPKLMNOPX-O" & _
            "XBXLXLLLLLLLPNOPX-O" & _
            "XBXPXPMPMPMPMNOPX-O" & _
            "XBXNXNNNNNNNNNOPX-O" & _
            "XBXOXOOOOOOOOOOPX-O" & _
            "XBXPXPPPPPPPPPPPX-O" & _
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
    Public Shared NotDefinedTypes As String = "DFGHIJKLM"

    Public Shared UnaryPlusResultType As String = "XBXFXFGHIJKLMNOPX-O"
    Public Shared UnaryPlusDefinedTypes As String = "FGHIJKLMNOP"

    Public Shared UnaryMinusResultType As String = "XBXFXFHHJJLLPNOPX-O"
    Public Shared UnaryMinusDefinedTypes As String = "FHJLNOP"

    Public Shared ShiftDefinedTypes As String = "FGHIJKLM"
    Public Shared ShiftResultType As String = _
 "XXXXXXXXXXXXXXXXX-X" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XXXXXXXXXXXXXXXXX-X" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XXXXXXXXXXXXXXXXX-X" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XBXFXFGHIJKLMLLLX-L" & _
 "XXXXXXXXXXXXXXXXX-X" & _
 "-------------------" & _
 "XBXFXFGHIJKLMLLLX-L"


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
        If GetEqualsOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
        Return TypeCode.Boolean
    End Function

    Shared Function GetLTResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        If GetLTOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
        Return TypeCode.Boolean
    End Function

    Shared Function GetGTResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        If GetGTOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
        Return TypeCode.Boolean
    End Function

    Shared Function GetLEResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        If GetLEOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
        Return TypeCode.Boolean
    End Function

    Shared Function GetGEResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        If GetGEOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
        Return TypeCode.Boolean
    End Function

    Shared Function GetNotEqualsResultType(ByVal op1 As TypeCode, ByVal op2 As TypeCode) As TypeCode
        If GetNotEqualsOperandType(op1, op2) = TypeCode.Empty Then Return TypeCode.Empty
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

#If GENERATOR = False OrElse DEVGENERATOR Then
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
#End If

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

#If DEVGENERATOR OrElse GENERATOR = False Then
    ''' <summary>
    ''' Converts the source to the destination type. Compiletime conversions are the only ones that succeeds.
    ''' Returns nothing if no conversion possible.
    ''' </summary>
    ''' <param name="Source"></param>
    ''' <param name="Destination"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTo(ByVal Compiler As Compiler, ByVal Source As Object, ByVal Destination As Mono.Cecil.TypeReference) As Object
        Dim result As Object

        If Destination Is Nothing Then Return Source

        Helper.Assert(Source IsNot Nothing)
        Helper.Assert(Destination IsNot Nothing)

        Dim dtc As TypeCode = Helper.GetTypeCode(Compiler, Destination)
        Dim stc As TypeCode = Helper.GetTypeCode(Compiler, CecilHelper.GetType(Compiler, Source))

        'Console.WriteLine("ConvertTo: from " & stc.ToString() & " to " & dtc.ToString)

        If dtc = stc Then Return Source

        Select Case dtc
            Case TypeCode.Boolean
                result = ConvertToBoolean(Source, stc)
            Case TypeCode.Byte
                result = ConvertToByte(Source, stc)
            Case TypeCode.Char
                result = ConvertToChar(Source, stc)
            Case TypeCode.DateTime
                result = ConvertToDateTime(Source, stc)
            Case TypeCode.DBNull
                result = ConvertToDBNull(Source, stc)
            Case TypeCode.Decimal
                result = ConvertToDecimal(Source, stc)
            Case TypeCode.Double
                result = ConvertToDouble(Source, stc)
            Case TypeCode.Empty
                result = ConvertToEmpty(Source, stc)
            Case TypeCode.Int16
                result = ConvertToInt16(Source, stc)
            Case TypeCode.Int32
                result = ConvertToInt32(Source, stc)
            Case TypeCode.Int64
                result = ConvertToInt64(Source, stc)
            Case TypeCode.Object
                result = ConvertToObject(Source, stc)
            Case TypeCode.SByte
                result = ConvertToSByte(Source, stc)
            Case TypeCode.Single
                result = ConvertToSingle(Source, stc)
            Case TypeCode.String
                result = ConvertToString(Source, stc)
            Case TypeCode.UInt16
                result = ConvertToUInt16(Source, stc)
            Case TypeCode.UInt32
                result = ConvertToUInt32(Source, stc)
            Case TypeCode.UInt64
                result = ConvertToUInt64(Source, stc)
            Case Else
                Throw New NotImplementedException()
        End Select

        Return result
    End Function
#End If

    Public Shared Function ConvertToBoolean(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Boolean
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToByte(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Byte
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Return CByte(DirectCast(Source, Integer))
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToChar(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Char
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Dim str As String = DirectCast(Source, String)
                If str.Length = 1 Then Return str(0)
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToDateTime(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As DateTime
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToDBNull(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As DBNull
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToDecimal(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Object
        Dim result As Decimal
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                result = CLng(Source)
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                result = CULng(Source)
            Case Else
                Throw New NotImplementedException()
        End Select
        Return result
    End Function

    Public Shared Function ConvertToDouble(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Double
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Return CDbl(Source)
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToEmpty(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Object
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToInt16(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Int16
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Return CShort(DirectCast(Source, Integer))
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToInt32(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Int32
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Return 0
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToInt64(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Int64
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Return CLng(Source)
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Return CLng(Source)
            Case TypeCode.Int32
                Return CLng(Source)
            Case TypeCode.Int64
                Return CLng(Source)
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Return CLng(Source)
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Return CLng(Source)
            Case TypeCode.UInt32
                Return CLng(Source)
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToObject(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Object
        Return Source
    End Function

    Public Shared Function ConvertToSByte(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As SByte
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToSingle(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Single
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Return DirectCast(Source, Byte)
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Return DirectCast(Source, Short)
            Case TypeCode.Int32
                Return DirectCast(Source, Integer)
            Case TypeCode.Int64
                Return DirectCast(Source, Long)
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Return DirectCast(Source, SByte)
            Case TypeCode.Single
                Return DirectCast(Source, Single)
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Return DirectCast(Source, UShort)
            Case TypeCode.UInt32
                Return DirectCast(Source, UInteger)
            Case TypeCode.UInt64
                Return DirectCast(Source, ULong)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToString(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As String
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Return CStr(Source)
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Return Nothing 'Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToUInt16(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As UInt16
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToUInt32(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As UInt32
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                Throw New NotImplementedException
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Throw New NotImplementedException()
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.Int16
                Throw New NotImplementedException
            Case TypeCode.Int32
                Throw New NotImplementedException
            Case TypeCode.Int64
                Throw New NotImplementedException
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.SByte
                Throw New NotImplementedException
            Case TypeCode.Single
                Throw New NotImplementedException
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16
                Throw New NotImplementedException
            Case TypeCode.UInt32
                Throw New NotImplementedException
            Case TypeCode.UInt64
                Throw New NotImplementedException
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Public Shared Function ConvertToUInt64(ByVal Source As Object, ByVal SourceTypeCode As TypeCode) As Object
        Dim result As ULong
        Select Case SourceTypeCode
            Case TypeCode.Boolean
                Throw New NotImplementedException
            Case TypeCode.Byte
                result = CByte(Source)
            Case TypeCode.Char
                Throw New NotImplementedException
            Case TypeCode.DateTime
                Throw New NotImplementedException
            Case TypeCode.DBNull
                Throw New NotImplementedException
            Case TypeCode.Decimal
                Throw New NotImplementedException
            Case TypeCode.Double
                Return Nothing
            Case TypeCode.Empty
                Throw New NotImplementedException
            Case TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                Dim tmp As Long = CLng(Source)
                If tmp >= 0 Then
                    result = CULng(tmp)
                Else
                    Return Nothing
                End If
            Case TypeCode.Object
                Throw New NotImplementedException
            Case TypeCode.Single
                Return Nothing
            Case TypeCode.String
                Throw New NotImplementedException
            Case TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
                Return CULng(Source)
            Case Else
                Throw New NotImplementedException()
        End Select
        Return result
    End Function

End Class



#If GENERATOR And DEVGENERATOR = False Then
''' <summary>
''' All the keywords.
''' </summary>
''' <remarks></remarks>
Public Enum KS
    [AndAlso]
    [And]
    [Is]
    [IsNot]
    [Like]
    [Mod]
    [Not]
    [Or]
    [OrElse]
    [Xor]
    LT
    GT
    Equals
    NotEqual
    LE
    GE
    Concat
    Mult
    Add
    Minus
    Power
    RealDivision
    IntDivision
    ShiftRight
    ShiftLeft
    ConcatAssign '		L"&="
    AddAssign 'L"+="
    MinusAssign 'L"-="
    RealDivAssign 'L"/="
    IntDivAssign 'L"\="
    PowerAssign 'L"^="
    MultAssign 'L"*="
    ShiftLeftAssign '<<=
    ShiftRightAssign '>>=
End Enum
#End If