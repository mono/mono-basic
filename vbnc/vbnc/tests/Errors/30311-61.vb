Class X(Of T)
    Public Shared Function F(ByVal t As T) As Long
        Return CLng(t)
    End Function
End Class

'Public Class Test
'    Public Shared Sub Main()
'        Dim s As New String("test")
'        Dim o As Object
'        T.S(o)
'    End Sub
'End Class

'Class T
'    Shared Sub S(ByVal s As T)

'    End Sub
'    Shared Sub S(ByVal s As Test)

'    End Sub
'End Class