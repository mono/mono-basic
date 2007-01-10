'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Class base
    Default Public ReadOnly Property Item(ByVal i As Integer) As Integer
        Get
            Return i
        End Get
    End Property
End Class

Class derive
    Inherits base
    Public Shadows ReadOnly Property Item(ByVal i As Integer) As Integer
        Get
            Return 2 * i
        End Get
    End Property
End Class

Class derive1
    Inherits derive
    Default Public Shadows ReadOnly Property Item1(ByVal i As Integer) As Integer
        Get
            Return 3 * i
        End Get
    End Property
End Class

Module DefaultA
    Function Main() As Integer
        Dim a As derive1 = New derive1()
        Dim b As derive = a
        Dim i, j, k As Integer
        i = a(10)
        j = a.Item(10)
        k = b(10)
        If i <> 30 Then
            System.Console.WriteLine("Default Not Working properly in Derive1") : Return 1
        End If
        If j <> 20 Then
            System.Console.WriteLine("Default Not Working properly in Derive") : Return 1
        End If
        If k <> 10 Then
            System.Console.WriteLine("Default Not Working properly in Base") : Return 1
        End If
    End Function
End Module
