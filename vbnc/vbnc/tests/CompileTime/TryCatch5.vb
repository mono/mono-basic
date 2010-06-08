Imports System
Class TryCatch1
    Public Sub ExceptionFilter_Filter1()
        Dim filter1 As Boolean = True
        Dim filter2 As Boolean = False
        Try
        Catch e1 As ArgumentException When filter1
        Catch e3 As Exception
        End Try
    End Sub
End Class
