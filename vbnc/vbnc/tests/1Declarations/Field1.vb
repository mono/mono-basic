Namespace Field1
    Class ClassA
        Dim obj As String
        Dim builtin As Integer
        Dim builtinarray As Integer()
        Dim builtinarray2() As Integer
        Dim cl As classa
        Dim str As structurea
        Dim intf As interfacea
        Dim del As delegatea
        Dim e As enuma

        Public var1 As Object
        Private var2 As Object
        Protected var3 As Object
        Protected Friend var4 As Object
        Friend var5 As Object
    End Class

    Structure structureA
        Dim obj As String
        Dim builtin As Integer
        Dim builtinarray As Integer()
        Dim builtinarray2() As Integer
        Dim cl As classa
        Dim str As structureb
        Dim intf As interfacea
        Dim del As delegatea
        Dim e As enuma

        Public var1 As Object
        Private var2 As Object
        'Protected var3 As Object
        'Protected Friend var4 As Object
        Friend var5 As Object
    End Structure

    Structure structureB
        Dim value As Integer
    End Structure

    Module moduleA
        Dim obj As String
        Dim builtin As Integer
        Dim builtinarray As Integer()
        Dim builtinarray2() As Integer
        Dim cl As classa
        Dim str As structurea
        Dim intf As interfacea
        Dim del As delegatea
        Dim e As enuma

        Public var1 As Object
        Private var2 As Object
        'Protected var3 As Object
        'Protected Friend var4 As Object
        Friend var5 As Object
    End Module

    Interface interfacea

    End Interface

    Delegate Sub delegatea()

    Enum enuma
        value
    End Enum
End Namespace
