' Positive Test
' Test labels in functions
' the prduction for LabelName is
' LabelName ::= Identifier | IntLiteral
' vide vbls71 section 10.1 Blocks
Imports System

Module labelA


    Function Abs(ByVal x As Integer) As Integer

        If x >= 0 Then
            GoTo 1234234
        End If

        x = -x

1234234:
        Return x

    End Function


    Function Main() As Integer

        Dim x As Integer, y As Integer
        x = Abs(-1)

        y = Abs(1)

        If x <> 1 Then
            System.Console.WriteLine("#Lbl1") : Return 1
        End If
        If y <> 1 Then
            System.Console.WriteLine("#Lbl2") : Return 1
        End If

    End Function


End Module