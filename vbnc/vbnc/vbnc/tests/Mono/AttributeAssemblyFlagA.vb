'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices


Module Test
    Function Main() As Integer

        Dim asm As System.Reflection.AssemblyName
        Dim i As Integer
        asm = System.Reflection.Assembly.GetCallingAssembly().GetName()
        If asm.Flags.Tostring() <> "PublicKey" Then
            System.Console.WriteLine("Expected Assembly Flag to be PublicKey") : Return 1
        End If
    End Function
End Module
