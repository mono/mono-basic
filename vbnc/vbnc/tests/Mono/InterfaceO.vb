'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.
Option Strict Off
Imports System

Interface A
    Function A1() As Object
End Interface

Class B
    Implements A
    Public Function A1() As Object Implements A.A1
    End Function
End Class

Module A2
    Function Main() As Integer
        Dim x As A
        Dim x1 As B = New B()
        x1 = x
    End Function
End Module
