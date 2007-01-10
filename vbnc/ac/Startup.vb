' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

Module Startup

    Function Main(ByVal Arguments() As String) As Integer

        Console.WriteLine(Logo)
        Debug.WriteLine(Logo)

        If Arguments.Length <> 2 Then
            Console.WriteLine("2 arguments must be specified.")
            Debug.WriteLine("2 arguments must be specified.")
            Return -1
        End If

#If DEBUG Then
        If Diagnostics.Debugger.IsAttached Then
            Dim path As String = "..\..\vbnc\tests\"
            path &= "CompileTime2\test output\"
            path &= "StaticVariable2"
            Arguments(0) = path
            Arguments(1) = path
            Arguments(0) &= ".exe"
            Arguments(1) &= "_vbc.exe"
        End If
#End If

        Dim ac As AssemblyComparer
        Try
            ac = New AssemblyComparer(Arguments(0), Arguments(1))
            ac.Compare()
            Console.WriteLine(Join(ac.Errors.ToArray, vbNewLine))
            Debug.WriteLine(Join(ac.Errors.ToArray, vbNewLine))
            Console.WriteLine(Join(ac.Messages.ToArray, vbNewLine))
            Debug.WriteLine(Join(ac.Messages.ToArray, vbNewLine))
            If ac.Result = False Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Console.WriteLine("Exception occured: " & ex.Message)
            Debug.WriteLine("Exception occured: " & ex.Message)
            Console.WriteLine(ex.StackTrace)
            Debug.WriteLine(ex.StackTrace)
            Return -1
        End Try
    End Function

    ReadOnly Property Logo() As String
        Get
            Dim result As New System.Text.StringBuilder
            Dim FileVersion As Diagnostics.FileVersionInfo = Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
#If DEBUG Then
            result.AppendLine(FileVersion.ProductName & " version " & FileVersion.FileVersion & " (last write time: " & IO.File.GetLastWriteTime(FileVersion.FileName).ToString("dd/MM/yyyy HH:mm:ss") & ")")
#Else
            result.AppendLine(FileVersion.ProductName & " version " & FileVersion.FileVersion)
#End If
            result.AppendLine(FileVersion.LegalCopyright)
            result.AppendLine()

            Return result.ToString
        End Get
    End Property
End Module
