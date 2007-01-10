'MemberAccessExpression: E.I
'If "E" is the keyword "Global", and "I" is the name of an 
'accessible type in the global namespace, then the result is that type.
Class MemberAccessExpression2
    Sub Test()
        Dim o As Global.System.Object
    End Sub
End Class