'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'MyBase will call through the chain of inherited classes until it finds a callable implementation.
Option Strict Off
Imports System
Class A
    Public i As Integer
    Public Overridable Function X(ByVal i As Object) As Object
    End Function
End Class

Class B
    Inherits A
    Public Overrides Function X(ByVal i As Object) As Object
        If i <> 20 Then
            Throw New Exception("Unexpected Value. R.Y / R.Z should be eual to 20 but got i =" & i)
        End If
    End Function

    Public Function Y(ByVal i)
        MyClass.X(i)
    End Function

End Class

Class C
    Inherits B
    Public Overrides Function X(ByVal i As Object) As Object
        If i <> 10 Then
            Throw New Exception("Unexpected Value R.X should be eual to 10 but got i = " & i)
        End If
    End Function

    Public Function Z(ByVal i)
        MyBase.X(i)
    End Function
End Class

Module Test
    Function Main() As Integer
        Dim R As C = New C()
        R.X(10)
        R.Y(20)
        R.Z(20)
    End Function
End Module
