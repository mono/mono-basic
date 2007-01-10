'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off

Imports System

Module M
    Function F(ByVal a As Long, ByVal b As Integer, ByVal c As Long) As Integer
        Return 1
    End Function
    Function F(ByVal a As String, ByVal b As Date, ByVal c As Integer) As Integer
        Return 2
    End Function
    Function Main() As Integer
        Dim obj As Object = "123"
        If F(obj, 1, 2) <> 1 Then
            Throw New Exception("Overload Resolution failed in latebinding")
        End If
    End Function
End Module
