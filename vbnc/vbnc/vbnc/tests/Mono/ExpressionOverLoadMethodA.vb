'Author:
'   Satya Sudha K (ksathyasudha@novell.com)
'
' (C) 2005 Novell, Inc.
'
' Since the argument is of type object, the first method should be invoked irrespective of the 
' runtime type of the object
Option Strict Off

Imports System

Module M
    Function F(ByVal a As Object) As Integer
        Return 1
    End Function
    Function F(ByVal a As String) As Integer
        Return 2
    End Function
    Function Main() As Integer
        Dim obj As Object = "ABC"
        If F(obj) <> 1 Then
            Throw New Exception("Overload Resolution failed in latebinding")
        End If
    End Function
End Module
