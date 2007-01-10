'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

Module C
    Private Event E()
    Function S() As Object
        RaiseEvent E()
    End Function
End Module

Module A
    Function Main() As Integer
    End Function
End Module
