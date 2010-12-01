Option Infer Off

Enum B As Byte
    A
    B
End Enum
Enum SB As SByte
    A
    B
End Enum
Enum S As Short
    A
    B
End Enum
Enum US As UShort
    A
    B
End Enum
Enum I As Integer
    A
    B
End Enum
Enum UI As UInteger
    A
    B
End Enum
Enum L As Long
    A
    B
End Enum
Enum UL As ULong
    A
    B
End Enum

Class InferFor1
    Private Shared Errors As Integer

    Public Shared Function Main() As Integer
        'same type
        For i = CByte(1) To CByte(2)
        Next

        Return Errors
    End Function

    Shared Sub VerifyType(ByVal Actual As Type, ByVal Expected As Type, ByVal Message As String)
        If Actual Is Expected Then Return
        Errors += 1
        Console.WriteLine("Expected '{0}' Got '{1}': {2}", Expected.FullName, Actual.FullName, Message)
    End Sub

End Class