'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Imports System

Module M
    Delegate Function SD(ByVal i As Integer) As Integer
    Function f(ByVal i As Integer) As Integer
        Return 10
    End Function
    Function f(ByVal i As Single) As Integer
        Return 12
    End Function

    Function Main() As Integer
        Dim b As Boolean = False
        Dim i As Integer
        Dim d1 As SD
        d1 = New SD(AddressOf f)
        i = d1.Invoke(10)
        If (i <> 10) Then
            System.Console.WriteLine("Delegate not working Properly") : Return 1
        End If
    End Function
End Module
