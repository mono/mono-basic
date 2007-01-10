'Test structures
structure a
    Public i As Integer
    Sub New(ByVal a As String)

    End Sub
End Structure
structure b
    public j as integer
    sub New(a as string)

    End Sub
    shared sub New()

    End Sub
    sub c
        Dim aa As a
        'aa.i = 2
    End Sub
End Structure
