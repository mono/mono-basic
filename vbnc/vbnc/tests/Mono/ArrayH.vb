'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module m
    Function Main() As Integer
        Dim a1(3) As Integer
        Dim a2(3) As Double
        Dim a3(3) As Byte
        Dim a4(3) As Boolean
        Dim a5(3) As String

        If a1(1) <> 0 Then
            System.Console.WriteLine("Integer array not working") : Return 1
        End If
        If a2(1) <> 0 Then
            System.Console.WriteLine("Double array not working") : Return 1
        End If
        If a3(1) <> 0 Then
            System.Console.WriteLine("Byte array not working") : Return 1
        End If
        If a4(1) <> False Then
            System.Console.WriteLine("Boolean array not working") : Return 1
        End If
        System.Console.WriteLine(a5(1) = Nothing)
        If a5(1) <> "" Then
            System.Console.WriteLine("String array not working") : Return 1
        End If
    End Function
End Module
