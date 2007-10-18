Imports System

Class ParamArray1

    Shared Function Test(ByVal ParamArray Value() As Type) As Integer
        Return 0
    End Function
    Shared Function Main() As Integer
        Dim T As Type
        Dim value As type() = Type.EmptyTypes
        Return Test(value)
    End Function
End Class