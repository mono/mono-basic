'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Class base
    Dim ia As Integer
    Default Public Property Item(ByVal i As Integer) As Integer
        Get
            Return ia
        End Get
        Set(ByVal j As Integer)
            ia = j
        End Set
    End Property
End Class

Module DefaultA
    Function Main() As Integer
        Dim a As base = New base()
        Dim i As Integer
        a(1) = 4
        i = a(10)
        If i <> 4 Then
            System.Console.WriteLine("Default Not Working") : Return 1
        End If
    End Function
End Module
