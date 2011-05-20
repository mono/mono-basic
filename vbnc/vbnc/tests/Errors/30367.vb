Option Strict Off

Imports System.Data
Class R
    Shared Function Main() As Integer
        Dim result As Integer
        Dim a As New a

        If Not a!foo Then
            Console.WriteLine("a failed")
            result += 1
        End If


        Return result
    End Function
End Class

Class A
    ReadOnly Property I(ByVal a As String) As Boolean
        Get
            Return False
        End Get
    End Property
End Class
