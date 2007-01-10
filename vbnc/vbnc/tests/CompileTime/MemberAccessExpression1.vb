'MemberAccessExpression: E.I
'If E is omitted then the expresion from the immediately containing "With" statement
'is substituted for "E" and the member access is performed.
Class MemberAccessExpression1
    Sub Test()
        Dim o As Object
        With o
            o = .Tostring
        End With
    End Sub
End Class