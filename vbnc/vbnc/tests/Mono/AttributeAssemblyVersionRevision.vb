'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Assembly: AssemblyVersion("3.2.1.0")> 

Module Test
    Function Main() As Integer

        Dim asm As System.Reflection.AssemblyName
        Dim i As Integer
        asm = System.Reflection.Assembly.GetCallingAssembly().GetName()
        If asm.Version.Revision.ToString() <> "0" Then
            System.Console.WriteLine("Expected Revision Version No. 0") : Return 1
        End If

    End Function
End Module

