Imports FooSpace

Namespace MySpace
    Public Class SomeClass
        Inherits FooBase

        Dim i As FooInternal ' Erorr's here

    End Class
End Namespace

Namespace FooSpace
    Public Class FooBase
        Public Class FooInternal
            '
        End Class
    End Class
End Namespace

