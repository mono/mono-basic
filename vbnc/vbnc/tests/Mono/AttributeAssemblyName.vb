'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)

Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Assembly: AssemblyCulture(""), Assembly: AssemblyVersion("1.2.3.4")> 

Module Test
    Function Main() As Integer

        Dim asm As System.Reflection.AssemblyName
        Dim i As Integer
        asm = System.Reflection.Assembly.GetExecutingAssembly().GetName()
        If asm.toString() <> "AttributeAssemblyName, Version=1.2.3.4, Culture=neutral, PublicKeyToken=null" AndAlso _
           asm.toString() <> "AttributeAssemblyName_vbc, Version=1.2.3.4, Culture=neutral, PublicKeyToken=null" Then
            System.Console.WriteLine("#A1 Atributes not working, got '" & asm.ToString()) : Return 1
        End If
    End Function
End Module