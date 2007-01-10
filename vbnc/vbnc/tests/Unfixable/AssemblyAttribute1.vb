Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

<Assembly: AssemblyVersion("1.2.3.4")> 

Namespace AssemblyAttribute1
    Class Test
        Shared Function Main() As Integer
            Dim a As String
            Dim b As String = "1.2.3.4"
            a = reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString
            If a <> b Then
                Console.WriteLine("Expected '" & b & "' but got '" & a & "'")
                Return 1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace