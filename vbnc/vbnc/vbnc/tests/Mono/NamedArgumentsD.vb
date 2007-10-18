Option Strict Off
'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.

Class C
    Public Function fun(ByRef i As Integer, ByRef j As Integer)
        i = 9
        j = 10
    End Function
End Class

Module M
    Function Main() As Integer
        Dim o As Object = New C()
        Dim a As Integer = 1
        Dim err As String = ""
        o.fun(a, a)
        If a <> 10 Then
            err += "#A1 binding not proper. Expected '10' but got '" & a & "'" & vbCrLf
        End If
        o.fun(i:=a, j:=a)
        If a <> 10 Then
            err += "#A2 binding not proper. Expected '10' but got '" & a & "'" & vbCrLf
        End If
        o.fun(j:=a, i:=a)
        If a <> 9 Then
            err += "#A3 binding not proper. Expected '9' but got '" & a & "'" & vbCrLf
        End If
        If (err <> "") Then
            Console.WriteLine(err)
            Return 1
        End If

    End Function
End Module
