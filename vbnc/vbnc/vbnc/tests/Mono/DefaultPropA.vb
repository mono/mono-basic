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

Module DefaultA
    Function Main() As Integer
        Dim a As base = New base()
        Dim i As Integer
        i = a(10)
        If i <> 10 Then
            System.Console.WriteLine("Default Not Working") : Return 1
        End If
    End Function
End Module
