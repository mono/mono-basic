Namespace Field2
    Class ClassA
        Const obj As String = ""
        Const builtin As Integer = 1
        'Const cl As classa = Nothing
        'Const str As structurea = Nothing
        'Const intf As interfacea = Nothing
        'Const del As delegatea = Nothing
        Const e As enuma = enuma.value

        Public Const var1 As Object = Nothing
        Private Const var2 As Object = Nothing
        Protected Const var3 As Object = Nothing
        Protected Friend Const var4 As Object = Nothing
        Friend Const var5 As Object = Nothing
    End Class

    Structure structureA
        Dim value As Integer
        Const obj As String = Nothing
        Const builtin As Integer = 2
        'Const cl As classa = Nothing
        'Const str As structureb = Nothing
        'Const intf As interfacea = Nothing
        'Const del As delegatea = Nothing
        Const e As enuma = enuma.value

        Public Const var1 As Object = Nothing
        Private Const var2 As Object = Nothing
        'Protected var3 As Object= nothing
        'Protected Friend var4 As Object= nothing
        Friend Const var5 As Object = Nothing
    End Structure

    Structure structureB
        Dim value As Integer
    End Structure

    Module moduleA
        Const obj As String = Nothing
        Const builtin As Integer = 5
        'Const cl As classa = Nothing
        'Const str As structurea = Nothing
        'Const intf As interfacea = Nothing
        'Const del As delegatea = Nothing
        Const e As enuma = enuma.value

        Public Const var1 As Object = Nothing
        Private Const var2 As Object = Nothing
        'Protected var3 As Object= nothing
        'Protected Friend var4 As Object= nothing
        Friend Const var5 As Object = Nothing
    End Module

    Interface interfacea

    End Interface

    Delegate Sub delegatea()

    Enum enuma
        value
    End Enum
End Namespace
