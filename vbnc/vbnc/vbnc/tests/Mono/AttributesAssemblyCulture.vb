'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Assembly: AssemblyCulture("")> 

Module Test
    Function Main() As Integer

        Dim asm As System.Reflection.AssemblyName
        asm = System.Reflection.Assembly.GetCallingAssembly().GetName()
        If asm.cultureinfo.Tostring() <> "" Then
            System.Console.WriteLine("Expected to be null") : Return 1
        End If
    End Function
End Module

