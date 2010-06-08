Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices

<Assembly: AssemblyVersion("3.2.1.0")> 

Module Test
    Function Main() As Integer
        Dim result As Integer
        Dim FileVersion As Diagnostics.FileVersionInfo = Nothing
        FileVersion = Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
        If FileVersion.ProductVersion.ToString() <> "3.2.1.0" Then
            System.Console.WriteLine("Expected ProductVersion No. 3.2.1.0 got {0}", FileVersion.ProductVersion.ToString())
            result += 1
        End If
        If FileVersion.FileVersion.ToString() <> "3.2.1.0" Then
            System.Console.WriteLine("Expected ProductVersion No. 3.2.1.0 got {0}", FileVersion.FileVersion.ToString())
            result += 1
        End If
        Return result
    End Function
End Module
