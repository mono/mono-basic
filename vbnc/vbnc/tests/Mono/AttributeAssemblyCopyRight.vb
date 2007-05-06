'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Assembly: AssemblyCopyright("2004, 2005 Novell, Inc.")> 

Module Test
    Function Main() As Integer

        Dim asm As System.Reflection.Assembly
        Dim i As Integer
        asm = System.Reflection.Assembly.GetAssembly(GetType(Test))
        Dim att As Object() = asm.GetCustomAttributes(GetType(System.Reflection.AssemblyCopyrightAttribute), False)
        For i = 0 To att.Length - 1
            If att(i).ToString() <> "System.Reflection.AssemblyCopyrightAttribute" Then
                System.Console.WriteLine("AssemblyCopyright Attribute was not set properly") : Return 1
            End If
        Next i
    End Function
End Module
