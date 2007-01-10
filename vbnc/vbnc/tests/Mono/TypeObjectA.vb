'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
Option Strict Off
Imports System

Public Class ValueReference
    Public AgeClass As Integer
End Class

Structure SomeStruct
    Public AgeStruct As Integer
End Structure

Module Test
    Function Main() As Integer
        Dim objVal1 As Object = New SomeStruct()
        objval1.AgeStruct = 50
        Dim objval2 As Object = objval1
        objval2.AgeStruct = 100
        If (objval1.AgeStruct <> 50 Or objval2.AgeStruct <> 100) Then
            Throw New Exception("objval1.AgeStruct should be 50, but got " & objval1.AgeStruct & " and objval2.AgeStruct should be , but got " & objval2.AgeStruct)
        End If

        Dim Objref1 As Object = New ValueReference()
        objref1.AgeClass = 50
        Dim objref2 As Object = objref1
        objref2.AgeClass = 100
        If (objref2.AgeClass <> objref2.AgeClass Or objref2.AgeClass <> 100) Then
            Throw New Exception("objref1.AgeClass should be 100, but got " & objref1.AgeClass & "and objref2.AgeClass should be , but got " & objref2.AgeClass)
        End If
    End Function
End Module
