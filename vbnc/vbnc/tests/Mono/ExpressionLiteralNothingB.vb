'Author: Ritvik Mayank <mritvik@novell.com>
'Copyright (C) 2005 Novell, Inc (http://www.novell.com)
'Nothing keyword represents the default value of any data type

Imports System
Imports Microsoft.VisualBasic

Module ExpressionLiteralsNothing
    Public Structure MyStruct
        Public Name As String
        Public Number As Short
    End Structure
    Function Main() As Integer
        Dim A As MyStruct
        A = Nothing
        If A.Name <> Nothing Then
            System.Console.WriteLine("Unexpected behavior. A.Name Should be Nothing ") : Return 1
        End If
        If A.Number <> 0 Then
            System.Console.WriteLine("Unexpected behavior. A.Number Should be 0 ") : Return 1
        End If
    End Function
End Module
