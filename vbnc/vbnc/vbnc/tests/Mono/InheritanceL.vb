Option Strict Off
'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

'Checking for NotInheritable class....
'This insists that NonInheritable belongs only to the specific class.. not branches to its sub-classes

NotInheritable Class A
    Class B
        Function G()
        End Function
    End Class
End Class

Class C
    Inherits A.B
End Class

Module InheritanceN
    Function Main() As Integer
        Dim a As C = New C()
        a.G()
    End Function
End Module

