'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'this is when first term is False

Imports System
Imports Microsoft.VisualBasic
Module Test

    Function f1(ByVal i As Integer, ByVal j As Integer, ByVal k As Integer) As Boolean
        Dim E As Boolean = (k - i) > (j - i)
        Return E
    End Function

    Function f2(ByVal l As Integer, ByVal m As Integer) As Boolean
        Dim F As Boolean = l < m
        If l = 10 Then
            Throw New Exception("Second Term is not supposed to be evaluated as the First Term is False")
        End If
        Return F
    End Function

    Function Main() As Integer
        Dim MyCheck As Boolean
        MyCheck = f1(10, 40, 30) AndAlso f2(10, 20)
        If Mycheck = True Then
            Throw New Exception(" Unexpected Result for the Expression  ")
        End If
    End Function
End Module

