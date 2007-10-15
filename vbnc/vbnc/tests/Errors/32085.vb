Class Foo
    Shared Sub Main()
        Dim x As Object = FooCreate(Of Int32)()
    End Sub

    Shared Function FooCreate(Of T As New)() As T
        Return New T(2)
    End Function
End Class

