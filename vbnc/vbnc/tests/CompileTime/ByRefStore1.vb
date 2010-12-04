
Public Structure Vertex
    Dim X As Double
    Dim y As Double
    Dim z As Double
End Structure

Public Class C
    Public Function M(ByRef F As Single) As Vertex
        M.X = F
    End Function
End Class