Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Check the working of the Non-Inheritable Class.
'Does Not apply for the Function classes

Class A
    NotInheritable Class B
        Function G()
        End Function
    End Class
    Function F()
    End Function
End Class

Class C
    Inherits A
    Public c As A.B = New A.B()
End Class

Module InheritanceN
    Function Main() As Integer
        Dim a As C = New C()
        Dim b As A.B = New A.B()
        a.c.G()
        b.G()
    End Function
End Module

