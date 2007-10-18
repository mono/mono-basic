Public Class SingleLineIfWithBlockStatements1
    Event EventTest()
    Private m_Field As SingleLineIfWithBlockStatements1
    Sub Test()
        Dim arrvar() As Integer
        Dim var As Integer
        Dim o As Object

        Dim sep As Integer
        sep = -100
        If True Then Do While True : test() : Loop
        sep = 100
        If True Then Do : test() : Loop Until False
        sep = 200
        If True Then For Each i As Integer In arrvar : o = 0 : Next
        sep = 300
        If True Then For i As Integer = 0 To 1 : o = i : Next
        seP = 400
        If True Then If False Then test() Else test() Else test()
        sep = 500
        If True Then Select Case var : Case 1 : test() : Case Else : test() : End Select
        sep = 600
        If True Then Try : test() : Catch ex As system.exception : test() : Finally : test() : End Try
        sep = 700
        If True Then Using i As idisposable = CType(o, idisposable) : test() : End Using
        sep = 800
        If True Then While True : test() : End While
        sep = 900
        If True Then With o : test() : End With
        sep = 1000
    End Sub
End Class