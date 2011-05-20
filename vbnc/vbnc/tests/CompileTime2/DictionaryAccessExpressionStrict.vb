Option Strict On
Imports System.Data
Class R
    Shared Function Main() As Integer
        Dim result As Integer
        Dim a As New a
        Dim b As New b
        Dim c As New c

        If Not a!foo Then
            Console.WriteLine("a failed")
            result += 1
        End If

        If Not b!foo Then
            Console.WriteLine("b failed")
            result += 1
        End If

        If Not c!foo Then
            Console.WriteLine("c failed")
            result += 1
        End If

        Return result
    End Function
End Class

Class A
    Default ReadOnly Property I(ByVal a As Integer) As Boolean
        Get
            Return False
        End Get
    End Property
    Default ReadOnly Property I(ByVal a As String) As Boolean
        Get
            Return True
        End Get
    End Property
End Class

Class B
    Default ReadOnly Property I(ByVal a As Object) As Boolean
        Get
            Return True
        End Get
    End Property
End Class

Class C
    Default ReadOnly Property I(ByVal a As Object) As Boolean
        Get
            Return False
        End Get
    End Property
    Default ReadOnly Property I(ByVal a As String) As Boolean
        Get
            Return True
        End Get
    End Property
End Class