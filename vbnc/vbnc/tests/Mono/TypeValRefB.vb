'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System

Class Class1
    Public Value As Integer = 0
End Class

Module Test
    Function Main() As Integer
        Dim val1 As Integer = 0
        Dim val2 As Integer = val1
        val2 = 123
        Dim ref1 As Class1 = New Class1()
        Dim ref2 As Class1 = ref1
        ref2.Value = 123
        If val1 <> 0 And val2 <> 123 Then
            Throw New Exception("Expected was val1 = 0 and val2 = 123 .")
        End If
    End Function
End Module