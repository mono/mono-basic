'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

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
        Dim x As A = New B()
        x.A1()
    End Function
End Module
