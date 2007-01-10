'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Class A
    Sub fun()
    End Sub
End Class

Class AB
    Inherits A
    Shadows Sub fun()
    End Sub
End Class

Module ShadowE
    Function Main() As Integer
        Dim a As AB = New AB()
        a.fun()
        CType(a, A).fun()
    End Function
End Module
