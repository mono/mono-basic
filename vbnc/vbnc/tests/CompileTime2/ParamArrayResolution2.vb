Imports System
Imports System.Collections
Imports System.Reflection

Namespace ParamArrayResolution2
    Class Test
        Shared Function A(ByVal p1 As String, ByVal ParamArray p() As Object) As Integer
            Return 1
        End Function
        Shared Function Main() As Integer
            Dim result As Integer = 0
            'If a("") <> 1 Then
            '    'Console.WriteLine("#01")
            '    result += 1
            'End If

            Dim test As String = String.format("")
            Return result
        End Function
    End Class
End Namespace