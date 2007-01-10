Option Compare Text

Imports System

Module LikeOperatorA
    Function Main() As Integer

        Dim a As Boolean

        a = "HEllo" Like "H[A-Z][!M-P][!A-K]O"
        If a <> True Then
            System.Console.WriteLine("#A1-LikeOperator:Failed") : Return 1
        End If

        a = "he12O" Like "H?##[L-P]"
        If a <> True Then
            System.Console.WriteLine("#A2-LikeOperator:Failed") : Return 1
        End If

        a = "He[ll?o*" Like "He[[]*[?]o[*]"
        If a <> True Then
            System.Console.WriteLine("#A3-LikeOperator:Failed") : Return 1
        End If

        a = "Hell[]o*" Like "Hell[[][]]o[*]"
        If a <> True Then
            System.Console.WriteLine("#A4-LikeOperator:Failed") : Return 1
        End If

        a = "Hell[]o*" Like "Hell[[]][[]]o[*]"
        If a <> False Then
            System.Console.WriteLine("#A5-LikeOperator:Failed") : Return 1
        End If

        a = "Hello*" Like "Hell[]o[*]"
        If a <> True Then
            System.Console.WriteLine("#A6-LikeOperator:Failed") : Return 1
        End If

    End Function

End Module
