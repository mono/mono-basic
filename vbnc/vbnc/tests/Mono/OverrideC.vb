'Checks if Default is working or not with Overrides...It works
Option Strict Off
Imports System

Class base
    Default Public Overridable ReadOnly Property Item(ByVal i As Integer) As Integer
        Get
            Return i
        End Get
    End Property
End Class

Class derive
    Inherits base
    Default Public Overrides ReadOnly Property Item(ByVal i As Integer) As Integer
        Get
            Return 2 * i
        End Get
    End Property
End Class

Module DefaultA
    Function Main() As Integer
        Dim a As Object = New derive()
        Dim i As Integer
        i = a(10)
        If i <> 20 Then
            System.Console.WriteLine("Default Not Working") : Return 1
        End If
    End Function
End Module
