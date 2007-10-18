Class TypeNameResolution1
    Class a
        Dim v1 As a
        Dim v2 As b
        Dim v3 As c
        Sub a_a()

        End Sub
        Sub a_b()

        End Sub
        Class a
            Dim v1 As a
            Dim v2 As b
            Dim v3 As c
            Sub a_a_a()

            End Sub
            Sub a_a_b()

            End Sub
        End Class
        Class b
            Sub a_b_a()

            End Sub
            Sub a_b_b()

            End Sub
        End Class
        Class c
            Sub a_c_a()

            End Sub
            Sub a_c_b()

            End Sub
        End Class
    End Class
    Class b
        Dim v1 As a
        Dim v2 As b
        Dim v3 As c
        Sub b_a()

        End Sub
        Sub b_b()

        End Sub
        Class a(Of b)
            Dim v1 As a
            Dim v2 As b
            Dim v3 As c
            Sub b_a_a()

            End Sub
            Sub b_a_b()

            End Sub
        End Class
        Class b(Of c)
            Dim v1 As a
            Dim v2 As b
            Dim v3 As c
            Sub b_b_a()

            End Sub
            Sub b_b_b()

            End Sub
        End Class
        Class c(Of a)
            Dim v1 As a
            Dim v2 As b
            Dim v3 As c
            Sub b_c_a()

            End Sub
            Sub b_c_b()

            End Sub
        End Class
    End Class
    Class c
        Sub c_a()

        End Sub
        Sub c_b()

        End Sub
        Class a
            Sub c_a_a()

            End Sub
            Sub c_a_b()

            End Sub
        End Class
        Class b
            Sub c_b_a()

            End Sub
            Sub c_b_b()

            End Sub
        End Class
        Class c
            Sub c_c_a()

            End Sub
            Sub c_c_b()

            End Sub
        End Class
    End Class
End Class