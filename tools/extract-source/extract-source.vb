' extract-source.vb
'
' Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' 
' Copyright (C) 2007 Novell, Inc (http://www.novell.com)
'
' Permission is hereby granted, free of charge, to any person obtaining
' a copy of this software and associated documentation files (the
' "Software"), to deal in the Software without restriction, including
' without limitation the rights to use, copy, modify, merge, publish,
' distribute, sublicense, and/or sell copies of the Software, and to
' permit persons to whom the Software is furnished to do so, subject to
' the following conditions:
' 
' The above copyright notice and this permission notice shall be
' included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
' MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
' NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
' LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
' OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
' WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Module extract_source

    Sub ShowHelp()
        Console.WriteLine("Extracts sources from a vb project file (.vbproj, VS2005 file format)")
        Console.WriteLine(vbTab & "-s[ource]:<vbproj file>")
        Console.WriteLine(vbTab & "-d[estination]:<destination file>")
        Console.WriteLine(vbTab & "-m[ode]:win|windows|linux")
        Console.WriteLine(vbTab & "-x:r (exclude resources)")
        Console.WriteLine(vbTab & "-b[asepath]:<optional base path to append to all paths in project file>")
    End Sub

    Function IsNullOrEmpty(ByVal str As String) As Boolean
        If str Is Nothing Then Return True
        If String.Compare(str, String.Empty) = 0 Then Return True
        Return False
    End Function

    Function Main(ByVal args As String()) As Integer
        Dim source As String = Nothing, destination As String = Nothing, mode As String = Nothing, basepath As String = Nothing
        Dim exclude_resources As Boolean
        Console.WriteLine("Arguments: '" & String.Join("' '", args) & "'")
        For Each arg As String In args
            If arg.StartsWith("-") = False AndAlso arg.StartsWith("/") = False Then
                Console.WriteLine("Didn't understand argument: '" & arg & "'")
                ShowHelp()
                Return 1
            End If
            Dim name, value As String
            Dim idx As Integer
            arg = arg.Substring(1)
            idx = arg.IndexOfAny(New Char() {":"c, "="c})
            If idx = -1 Then
                Console.WriteLine("Didn't understand argument (no ':' nor '='): '" & arg & "'")
                ShowHelp()
                Return 1
            End If
            name = arg.Substring(0, idx)
            value = arg.Substring(idx + 1)
            Select Case name.ToUpperInvariant
                Case "B", "BASEPATH"
                    basepath = value
                Case "S", "SOURCE"
                    source = value
                Case "D", "DESTINATION", "DEST"
                    destination = value
                Case "M", "MODE"
                    Select Case value.ToUpperInvariant
                        Case "WIN", "W", "WINDOWS"
                            mode = "w"
                        Case "L", "LINUX", "LIN"
                            mode = "l"
                        Case Else
                            Console.WriteLine("Invalid mode: " & value)
                            ShowHelp()
                            Return 1
                    End Select
                    mode = value
                Case "H", "HELP", "?"
                    ShowHelp()
                    Return 0
                Case "X"
                    exclude_resources = True
                Case Else
                    Console.WriteLine("Unknown option: '" & name & "' with value: '" & value & "'")
                    ShowHelp()
                    Return 1
            End Select
        Next

        Dim result As Integer
        If IsNullOrEmpty(mode) Then
            Console.WriteLine("The mode is necessary.")
            result = 1
        End If
        If IsNullOrEmpty(source) Then
            Console.WriteLine("The source is necessary.")
            result = 1
        End If
        If IsNullOrEmpty(destination) Then
            Console.WriteLine("The destination is necessary.")
            result = 1
        End If
        If result = 1 Then
            ShowHelp()
            Return result
        End If

        Try
            Extract(source, destination, mode, basepath, exclude_resources)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

    Sub Extract(ByVal VBProjFileName As String, ByVal DestinationFile As String, ByVal mode As String, ByVal basepath As String, ByVal exclude_resource As Boolean)
        Dim sources As String()

        sources = GetSources(VBProjFileName, basepath, exclude_resource)
        Select Case mode.ToUpperInvariant
            Case "W"
                IO.File.WriteAllText(DestinationFile, Join(sources, vbCrLf).Replace ("/"c, "\"c))
            Case "L"
                IO.File.WriteAllText(DestinationFile, Join(sources, vbLf).Replace("\"c, "/"c))
            Case Else
                Throw New Exception("Invalid mode: " & mode)
        End Select

    End Sub

    Function GetSources(ByVal File As String, ByVal BasePath As String, ByVal ExcludeResources As Boolean) As String()
        Dim files As New Generic.List(Of String)

        If BasePath Is Nothing Then BasePath = String.Empty

        Using x As New Xml.XmlTextReader(File)
            Dim i As Integer
            While x.Read()
                Dim filename As String
                Dim prefix As String
                prefix = ""
                filename = ""
                If x.Name = "Compile" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                    x.MoveToAttribute("Include")
                    filename = x.Value
                ElseIf ExcludeResources = False AndAlso x.Name = "EmbeddedResource" AndAlso x.NodeType = Xml.XmlNodeType.Element Then
                    x.MoveToAttribute("Include")
                    filename = x.Value
                    prefix = "-res:"
                End If
                If filename <> "" Then
                    filename = BasePath & x.Value
                    filename = filename.Replace("%29", ")")
                    filename = filename.Replace("%28", "(")
                    filename = prefix & filename
                    If filename.Contains(" "c) Then
                        filename = """" & filename & """"
                    End If
                    files.Add(filename)
                    i += 1
                End If
            End While
        End Using

        Return files.ToArray
    End Function
End Module
