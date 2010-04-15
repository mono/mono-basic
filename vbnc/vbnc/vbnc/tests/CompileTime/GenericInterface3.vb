Imports System.Collections.Generic
Imports System.Reflection

Public Class GenericInterface3
    Implements Collections.Generic.IEqualityComparer(Of String)

    Public Function Equals1(ByVal x As String, ByVal y As String) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of String).Equals
        Return False
    End Function

    Public Function GetHashCode1(ByVal obj As String) As Integer Implements System.Collections.Generic.IEqualityComparer(Of String).GetHashCode
        Return obj.GetHashCode
    End Function
End Class
