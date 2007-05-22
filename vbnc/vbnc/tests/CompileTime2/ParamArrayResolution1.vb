Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Windows.Forms

Namespace ParamArrayResolution1
    Class Test
        Shared Function A() As Integer
            Return 1
        End Function
        Shared Function A(ByVal ParamArray p() As String) As Integer
            Return 2
        End Function
        Shared Function A(ByVal p1 As String, ByVal ParamArray p() As String) As Integer
            Return 3
        End Function

        Shared Function B(ByVal p1 As Object, ByVal ParamArray p2() As Object) As Integer
            Return -1
        End Function

        Shared Function B(ByVal p1 As Object) As Integer
            Return -2
        End Function

        Class Test

        End Class
        Private Delegate Sub UpdateUIDelegate(ByVal test As Test, ByVal UpdateSummary As Boolean)
        Shared Sub UpdateUI(ByVal test As Test, ByVal UpdateSummary As Boolean)

        End Sub
        Shared Function C(ByVal p1 As System.Delegate, ByVal ParamArray p() As Object) As Integer
            Return -10
        End Function

        Shared Function C(ByVal p1 As System.Delegate) As Integer
            Return -11
        End Function

        Shared Function Main() As Integer
            Dim result As Integer = 0

            'If b("a", True, False) <> -1 Then
            '    Console.WriteLine("#01")
            '    result += 1
            'End If

            Dim test As Test, UpdateSummary As Boolean
            'If c(New UpdateUIDelegate(AddressOf UpdateUI), test, UpdateSummary) <> -10 Then
            '    Console.WriteLine("#02")
            '    result += 1
            'End If

            Dim frm As New Form
            Dim obj As Object = frm.Handle
            If frm.BeginInvoke(New UpdateUIDelegate(AddressOf UpdateUI), test, UpdateSummary) Is Nothing Then
                'No error defined here.
            End If

            Return result
        End Function
    End Class
End Namespace