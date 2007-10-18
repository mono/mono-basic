Imports System

Module LikeOperator
    Function Main() As Integer

        Dim a As Boolean

        a = "HELLO" Like "HELLO"
        If a <> True Then
            System.Console.WriteLine("#A1-LikeOperator:Failed") : Return 1
        End If

        a = "HELLO" Like "HEllO"
        If a <> False Then
            System.Console.WriteLine("#A2-LikeOperator:Failed") : Return 1
        End If

        a = "HELLO" Like "H*O"
        If a <> True Then
            System.Console.WriteLine("#A3-LikeOperator:Failed") : Return 1
        End If

        a = "HELLO" Like "H[A-Z][!M-P][!A-K]O"
        If a <> True Then
            System.Console.WriteLine("#A4-LikeOperator:Failed") : Return 1
        End If

        a = "HE12O" Like "H?##[L-P]"
        If a <> True Then
            System.Console.WriteLine("#A5-LikeOperator:Failed") : Return 1
        End If

        a = "HELLO123WORLD" Like "H?*#*"
        If a <> True Then
            System.Console.WriteLine("#A6-LikeOperator:Failed") : Return 1
        End If

        a = "HELLOworld" Like "B*O*d"
        If a <> False Then
            System.Console.WriteLine("#A7-LikeOperator:Failed") : Return 1
        End If

        a = "" Like ""
        If a <> True Then
            System.Console.WriteLine("#A8-LikeOperator:Failed") : Return 1
        End If

        a = "A" Like ""
        If a <> False Then
            System.Console.WriteLine("#A9-LikeOperator:Failed") : Return 1
        End If

        a = "" Like "A"
        If a <> False Then
            System.Console.WriteLine("#A10-LikeOperator:Failed") : Return 1
        End If

        a = "HELLO" Like "HELLO" & Nothing
        If a <> True Then
            System.Console.WriteLine("#A11-LikeOperator:Failed") : Return 1
        End If


    End Function

End Module
