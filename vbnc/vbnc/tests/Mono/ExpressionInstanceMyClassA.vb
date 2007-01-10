'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'MyClass behaves like an object variable referring to the current instance of a class as originally implemented.
Option Strict Off
Imports System

Class BaseClass
    Public i As Integer
    Public Overridable Function MyMethod(ByVal i)
        If i <> 100 Then
            Throw New Exception("Unexpected Behavior Expected 100 but got i = " & i)
        End If
    End Function
    Public Function UseMyClass(ByVal i)
        MyClass.MyMethod(i)
    End Function
End Class

Class DerivedClass : Inherits BaseClass
    Public Overrides Function MyMethod(ByVal i)
        i = 50
        Throw New Exception("Unexpected Behavior.MyMethod should always call member of BaseClass. It should never call the DerivedClass")
    End Function
End Class

Module Test
    Function Main() As Integer
        Dim TestObj As DerivedClass = New DerivedClass()
        TestObj.UseMyClass(100)
    End Function
End Module

