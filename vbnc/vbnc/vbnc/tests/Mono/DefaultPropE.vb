'Author:
'   V. Sudharsan (vsudharsan@novell.com)
'
' (C) 2005 Novell, Inc.

REM LineNo: 19
REM ExpectedWarning: BC40007
REM WarningMessage: Default property 'Item1' conflicts with default property 'Item' in the base class 'base' and so should be declared 'Shadows'.

Imports System

Class base
    Default Public ReadOnly Property Item(ByVal i As Integer) As Integer
        Get
            Return i
        End Get
    End Property
End Class

Class derive
    Inherits base
    Default Public Overloads ReadOnly Property Item1(ByVal i As Integer) As Integer
        Get
            Return 2 * i
        End Get
    End Property
End Class

Module DefaultA
    Function Main() As Integer
        Dim a As derive = New derive()
        Dim i As Integer
        i = a(10)
        If i <> 20 Then
            System.Console.WriteLine("Default Not Working") : Return 1
        End If
    End Function
End Module
