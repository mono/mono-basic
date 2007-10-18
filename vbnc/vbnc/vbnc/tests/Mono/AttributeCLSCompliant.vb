'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Module: CLSCompliant(True)> 
<Assembly: CLSCompliant(True)> 

Module Test
    Function Main() As Integer
        Dim asm As System.Reflection.Assembly
        Dim i As Integer
        asm = System.Reflection.Assembly.GetAssembly(GetType(Test))
        Dim att As Object() = asm.GetCustomAttributes(GetType(CLSCompliantAttribute), False)
        For i = 0 To att.Length - 1
            If att(i).GetType.ToString() <> "System.CLSCompliantAttribute" Then
                System.Console.WriteLine("Expected System.CLSCompliantAttribute") : Return 1
            End If
        Next i
    End Function
End Module