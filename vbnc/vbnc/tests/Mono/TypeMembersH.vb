'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Class C
    WriteOnly Property fun(ByVal a1 As Integer) As Integer
        Set(ByVal a As Integer)
            If a <> 30 Then
                System.Console.WriteLine("#A1 - Latebinding not working. a = " & a) : m.failed = True
            End If
        End Set
    End Property
    WriteOnly Property fun(ByVal a As Long) As Long
        Set(ByVal a1 As Long)
            If a1 <> 20 Then
                System.Console.WriteLine("#A1 - Latebinding not working. a1 = " & a) : m.failed = True
            End If
        End Set
    End Property
End Class

Module M
    Public failed As Boolean
    Function Main() As Integer
        Dim a As Integer = 30
        Dim a1 As Long = 20
        Dim o As Object = New C()
        o.fun(a) = a
        o.fun(a1) = a1
        If failed Then Return 1
    End Function
End Module

