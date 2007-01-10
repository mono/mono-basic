Class AddHandler2
    Class Test1
        Public Event TestEvent()
        Sub Test()
        End Sub
    End Class
    Class Test2
        Dim value As Test1
        Sub Test()
            AddHandler Value.TestEvent, AddressOf Test
        End Sub
        Sub Test2()
            AddHandler Value.TestEvent, AddressOf Value.Test
        End Sub
    End Class
End Class

