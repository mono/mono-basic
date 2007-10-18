Namespace TypeNameResolution2
    Namespace a
        Class class_a

        End Class
        Namespace b
            Class class_b

            End Class
            Namespace c
                Class class_c
                    Dim v1 As class_b
                    Dim v2 As class_d
                    Dim v3 As a.class_a
                    Dim v4 As b.class_b
                End Class
            End Namespace
        End Namespace
    End Namespace
    Module d
        Class class_d

        End Class
    End Module
End Namespace