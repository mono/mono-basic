'
' FileSystem.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com>
'
' Copyright (C) 2007 Novell (http://www.novell.com)
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
'
Imports System.IO
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Microsoft.VisualBasic.FileIO
    Public Class FileSystem
        Public Sub New()
            'Empty
        End Sub

        Private Shared Function StripTrailingSlash(ByVal dir As String) As String
            Return dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
        End Function

        Public Shared Function CombinePath(ByVal baseDirectory As String, ByVal relativePath As String) As String
            Return StripTrailingSlash(Path.Combine(Path.GetFullPath(baseDirectory), relativePath))
        End Function

        Public Shared Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String)
            CopyDirectory(sourceDirectoryName, destinationDirectoryName, False)
        End Sub

        Public Shared Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As UIOption)
            CopyDirectory(sourceDirectoryName, destinationDirectoryName, showUI, UICancelOption.ThrowException)
        End Sub

        Public Shared Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal overwrite As Boolean)
            Dim op As New FileSystemOperation(sourceDirectoryName, destinationDirectoryName, overwrite)
            op.ExecuteDirCopy()
        End Sub

        Public Shared Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As UIOption, ByVal onUserCancel As UICancelOption)
            Dim op As New FileSystemOperation(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel)
            op.ExecuteDirCopy()
        End Sub

        Public Shared Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String)
            CopyFile(sourceFileName, destinationFileName, False)
        End Sub

        Public Shared Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As UIOption)
            CopyFile(sourceFileName, destinationFileName, showUI, UICancelOption.ThrowException)
        End Sub

        Public Shared Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal overwrite As Boolean)
            Dim op As New FileSystemOperation(sourceFileName, destinationFileName, overwrite)
            op.ExecuteFileCopy()
        End Sub

        Public Shared Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As UIOption, ByVal onUserCancel As UICancelOption)
            Dim op As New FileSystemOperation(sourceFileName, destinationFileName, showUI, onUserCancel)
            op.ExecuteFileCopy()
        End Sub

        Public Shared Sub CreateDirectory(ByVal directory As String)
            System.IO.Directory.CreateDirectory(directory)
        End Sub

        Public Shared Sub DeleteDirectory(ByVal directory As String, ByVal onDirectoryNotEmpty As DeleteDirectoryOption)
            DeleteDirectory(directory, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException, onDirectoryNotEmpty, False)
        End Sub

        Public Shared Sub DeleteDirectory(ByVal directory As String, ByVal showUI As UIOption, ByVal recycle As RecycleOption)
            DeleteDirectory(directory, showUI, recycle, UICancelOption.ThrowException, DeleteDirectoryOption.DeleteAllContents, True)
        End Sub

        Public Shared Sub DeleteDirectory(ByVal directory As String, ByVal showUI As UIOption, ByVal recycle As RecycleOption, ByVal onUserCancel As UICancelOption)
            DeleteDirectory(directory, showUI, recycle, onUserCancel, DeleteDirectoryOption.DeleteAllContents, True)
        End Sub

        Private Shared Sub DeleteDirectory(ByVal directory As String, ByVal showUIOption As UIOption, ByVal recycle As RecycleOption, ByVal onUserCancel As UICancelOption, ByVal onDirectoryNotEmpty As DeleteDirectoryOption, ByVal showUI As Boolean)
            Dim op As New FileSystemOperation(directory, showUIOption, recycle, onUserCancel, onDirectoryNotEmpty, showUI)
            op.ExecuteDirDelete()
        End Sub

        Public Shared Sub DeleteFile(ByVal file As String)
            DeleteFile(file, UIOption.AllDialogs, RecycleOption.DeletePermanently, UICancelOption.DoNothing, False)
        End Sub

        Public Shared Sub DeleteFile(ByVal file As String, ByVal showUI As UIOption, ByVal recycle As RecycleOption)
            DeleteFile(file, showUI, recycle, UICancelOption.ThrowException, True)
        End Sub

        Public Shared Sub DeleteFile(ByVal file As String, ByVal showUI As UIOption, ByVal recycle As RecycleOption, ByVal onUserCancel As UICancelOption)
            DeleteFile(file, showUI, recycle, onUserCancel, True)
        End Sub

        Private Shared Sub DeleteFile(ByVal file As String, ByVal showUIOption As UIOption, ByVal recycle As RecycleOption, ByVal onUserCancel As UICancelOption, ByVal showUI As Boolean)
            Dim op As New FileSystemOperation(file, showUIOption, recycle, onUserCancel, showUI)
            op.ExecuteFileDelete()
        End Sub

        Public Shared Function DirectoryExists(ByVal directory As String) As Boolean
            Return IO.Directory.Exists(directory)
        End Function

        Public Shared Function FileExists(ByVal file As String) As Boolean
            Return IO.File.Exists(file)
        End Function

        Public Shared Function FindInFiles(ByVal directory As String, ByVal containsText As String, ByVal ignoreCase As Boolean, ByVal searchType As SearchOption) As ReadOnlyCollection(Of String)
            Return FindInFiles(directory, containsText, ignoreCase, searchType, Nothing)
        End Function

        Public Shared Function FindInFiles(ByVal directory As String, ByVal containsText As String, ByVal ignoreCase As Boolean, ByVal searchType As SearchOption, ByVal ParamArray fileWildcards As String()) As ReadOnlyCollection(Of String)
            Dim list As New Generic.List(Of String)
            FindInFiles2(directory, containsText, ignoreCase, searchType, fileWildcards, list)
            Return New ReadOnlyCollection(Of String)(list)
        End Function

        Private Shared Sub FindInFiles2(ByVal directory As String, ByVal containsText As String, ByVal ignoreCase As Boolean, ByVal searchType As SearchOption, ByVal fileWildcards() As String, ByVal result As Generic.List(Of String))
            Dim files As New Generic.List(Of String)

            If fileWildcards Is Nothing OrElse fileWildcards.Length = 0 Then
                files.AddRange(System.IO.Directory.GetFiles(directory))
            Else
                For Each wildcard As String In fileWildcards
                    AddUniqueToList(files, System.IO.Directory.GetFiles(directory, wildcard))
                Next
            End If

            If Not containsText Is Nothing AndAlso containsText.Length <> 0 Then
                For Each file As String In files
                    If FileContainsText(file, containsText, ignoreCase) Then
                        result.Add(file)
                    End If
                Next
            Else
                result.AddRange(files)
            End If

            If searchType = SearchOption.SearchAllSubDirectories Then
                Dim subdirs() As String
                subdirs = System.IO.Directory.GetDirectories(directory)
                For Each subdir As String In subdirs
                    FindInFiles2(subdir, containsText, ignoreCase, searchType, fileWildcards, result)
                Next
            End If
        End Sub

        Private Shared Sub AddUniqueToList(ByVal list As Generic.List(Of String), ByVal items As String())
            For Each item As String In items
                If list.Contains(item) = False Then list.Add(item)
            Next
        End Sub

        Private Shared Function FileContainsText(ByVal file As String, ByVal text As String, ByVal ignoreCase As Boolean) As Boolean
            Dim currentChar As Char
            Dim peekedChar As Generic.Queue(Of Char)

            If text Is Nothing OrElse text.Length = 0 Then Return True

            peekedChar = New Generic.Queue(Of Char)
            If ignoreCase Then text = text.ToUpperInvariant()

#If TARGET_JVM = False Then 'FileStream ctor with FileOptions Not Supported by Grasshopper
            Using reader As New IO.StreamReader(New IO.FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024, FileOptions.SequentialScan))
#Else
                            Using reader As New IO.StreamReader(New IO.FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024))
#End If
                currentChar = Microsoft.VisualBasic.Strings.ChrW(reader.Read)
                Do Until currentChar = Char.MinValue

                    If (ignoreCase AndAlso Char.ToUpperInvariant(currentChar) = text(0)) OrElse (currentChar = text(0)) Then
                        If text.Length = 1 Then Return True

                        Dim noMatch As Boolean
                        Dim tmp As Char() = peekedChar.ToArray
                        noMatch = False
                        For i As Integer = 0 To tmp.Length - 1
                            If text.Length = i + 2 Then Return True
                            If text(i + 1) <> tmp(i) Then
                                noMatch = True
                                Exit For
                            End If
                        Next

                        If noMatch = False Then
                            If text.Length = tmp.Length + 1 Then Return True

                            For i As Integer = tmp.Length To text.Length - 2
                                If reader.EndOfStream Then Return False

                                Dim tmpChar As Char = Microsoft.VisualBasic.Strings.ChrW(reader.Read)

                                If tmpChar = Char.MinValue Then Return False
                                peekedChar.Enqueue(tmpChar)
                                If Not ((ignoreCase AndAlso Char.ToUpperInvariant(tmpChar) = text(i + 1)) OrElse (tmpChar = text(i + 1))) Then
                                    noMatch = True
                                    Exit For
                                End If
                            Next
                            If noMatch = False Then Return True
                        End If
                    End If
                    If reader.EndOfStream Then Return False

                    If peekedChar.Count > 0 Then
                        currentChar = peekedChar.Dequeue
                    Else
                        currentChar = Microsoft.VisualBasic.Strings.ChrW(reader.Read)
                    End If
                Loop
            End Using

            Return False
        End Function

        Public Shared Function GetDirectories(ByVal directory As String) As ReadOnlyCollection(Of String)
            Return GetDirectories(directory, SearchOption.SearchTopLevelOnly, Nothing)
        End Function

        Public Shared Function GetDirectories(ByVal directory As String, ByVal searchType As SearchOption, ByVal ParamArray wildcards As String()) As ReadOnlyCollection(Of String)
            Dim list As New Generic.List(Of String)
            FindInDirectory2(directory, searchType, wildcards, list)
            Return New ReadOnlyCollection(Of String)(list)
        End Function

        Private Shared Sub FindInDirectory2(ByVal directory As String, ByVal searchType As SearchOption, ByVal fileWildcards() As String, ByVal result As Generic.List(Of String))

            If fileWildcards Is Nothing OrElse fileWildcards.Length = 0 Then
                result.AddRange(System.IO.Directory.GetDirectories(directory))
            Else
                For Each wildcard As String In fileWildcards
                    AddUniqueToList(result, System.IO.Directory.GetDirectories(directory, wildcard))
                Next
            End If

            If searchType = SearchOption.SearchAllSubDirectories Then
                Dim subdirs() As String
                subdirs = System.IO.Directory.GetDirectories(directory)
                For Each subdir As String In subdirs
                    FindInDirectory2(subdir, searchType, fileWildcards, result)
                Next
            End If
        End Sub

        Public Shared Function GetDirectoryInfo(ByVal directory As String) As DirectoryInfo
            Return New DirectoryInfo(directory)
        End Function

        Public Shared Function GetDriveInfo(ByVal drive As String) As DriveInfo
            Return New DriveInfo(drive)
        End Function

        Public Shared Function GetFileInfo(ByVal file As String) As FileInfo
            Return New FileInfo(file)
        End Function

        Public Shared Function GetFiles(ByVal directory As String) As ReadOnlyCollection(Of String)
            Return GetFiles(directory, SearchOption.SearchTopLevelOnly, Nothing)
        End Function

        Public Shared Function GetFiles(ByVal directory As String, ByVal searchType As SearchOption, ByVal ParamArray wildcards As String()) As ReadOnlyCollection(Of String)
            Return FindInFiles(directory, Nothing, False, searchType, wildcards)
        End Function

        Public Shared Function GetName(ByVal path As String) As String
            Return IO.Path.GetFileName(path)
        End Function

        Public Shared Function GetParentPath(ByVal path As String) As String
            Return IO.Path.GetDirectoryName(path)
        End Function

        Public Shared Function GetTempFileName() As String
            Return Path.GetTempFileName
        End Function

        Public Shared Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String)
            MoveDirectory(sourceDirectoryName, destinationDirectoryName, False)
        End Sub

        Public Shared Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As UIOption)
            MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI, UICancelOption.ThrowException)
        End Sub

        Public Shared Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal overwrite As Boolean)
            MoveDirectory(sourceDirectoryName, destinationDirectoryName, UIOption.AllDialogs, UICancelOption.DoNothing, False, overwrite)
        End Sub

        Public Shared Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As UIOption, ByVal onUserCancel As UICancelOption)
            MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel, True, False)
        End Sub

        Private Shared Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUIOption As UIOption, ByVal onUserCancel As UICancelOption, ByVal showUI As Boolean, ByVal overwrite As Boolean)
            Dim op As New FileSystemOperation(sourceDirectoryName, destinationDirectoryName, showUIOption, onUserCancel, showUI, overwrite)
            op.ExecuteDirMove()
        End Sub

        Public Shared Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String)
            MoveFile(sourceFileName, destinationFileName, False)
        End Sub

        Public Shared Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As UIOption)
            MoveFile(sourceFileName, destinationFileName, showUI, UICancelOption.ThrowException)
        End Sub

        Public Shared Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal overwrite As Boolean)
            MoveFile(sourceFileName, destinationFileName, UIOption.AllDialogs, UICancelOption.DoNothing, False, overwrite)
        End Sub

        Public Shared Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As UIOption, ByVal onUserCancel As UICancelOption)
            MoveFile(sourceFileName, destinationFileName, showUI, onUserCancel, True, False)
        End Sub

        Private Shared Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUIOption As UIOption, ByVal onUserCancel As UICancelOption, ByVal showUI As Boolean, ByVal overwrite As Boolean)
            Dim op As New FileSystemOperation(sourceFileName, destinationFileName, showUIOption, onUserCancel, showUI, overwrite)
            op.ExecuteFileMove()
        End Sub

        Public Shared Function OpenTextFieldParser(ByVal file As String) As TextFieldParser
            Return New TextFieldParser(file)
        End Function

        Public Shared Function OpenTextFieldParser(ByVal file As String, ByVal ParamArray fieldWidths As Integer()) As TextFieldParser
            Dim result As New TextFieldParser(file)
            result.FieldWidths = fieldWidths
            result.TextFieldType = FieldType.FixedWidth
            Return result
        End Function

        Public Shared Function OpenTextFieldParser(ByVal file As String, ByVal ParamArray delimiters As String()) As TextFieldParser
            Dim result As New TextFieldParser(file)
            result.Delimiters = delimiters
            result.TextFieldType = FieldType.Delimited
            Return result
        End Function

        Public Shared Function OpenTextFileReader(ByVal file As String) As StreamReader
            Return New StreamReader(file)
        End Function

        Public Shared Function OpenTextFileReader(ByVal file As String, ByVal encoding As Encoding) As StreamReader
            Return New StreamReader(file, encoding)
        End Function

        Public Shared Function OpenTextFileWriter(ByVal file As String, ByVal append As Boolean) As StreamWriter
            Return New StreamWriter(file, append)
        End Function

        Public Shared Function OpenTextFileWriter(ByVal file As String, ByVal append As Boolean, ByVal encoding As Encoding) As StreamWriter
            Return New StreamWriter(file, append, encoding)
        End Function

        Public Shared Function ReadAllBytes(ByVal file As String) As Byte()
            Return System.IO.File.ReadAllBytes(file)
        End Function

        Public Shared Function ReadAllText(ByVal file As String) As String
            Return System.IO.File.ReadAllText(file)
        End Function

        Public Shared Function ReadAllText(ByVal file As String, ByVal encoding As Encoding) As String
            Return System.IO.File.ReadAllText(file, encoding)
        End Function

        Public Shared Sub RenameDirectory(ByVal directory As String, ByVal newName As String)
            IO.Directory.Move(directory, Path.Combine(Path.GetDirectoryName(directory), newName))
        End Sub

        Public Shared Sub RenameFile(ByVal file As String, ByVal newName As String)
            IO.File.Move(file, Path.Combine(Path.GetDirectoryName(file), newName))
        End Sub

        Public Shared Sub WriteAllBytes(ByVal file As String, ByVal data As Byte(), ByVal append As Boolean)
            Dim mode As FileMode

            If append Then
                mode = FileMode.Append
            Else
                mode = FileMode.Create
            End If

            Using reader As New System.IO.FileStream(file, mode, FileAccess.Write, FileShare.Read)
                If data IsNot Nothing Then
                    reader.Write(data, 0, data.Length)
                End If
            End Using
        End Sub

        Public Shared Sub WriteAllText(ByVal file As String, ByVal text As String, ByVal append As Boolean)
            WriteAllText(file, text, append, System.Text.Encoding.UTF8)
        End Sub

        Public Shared Sub WriteAllText(ByVal file As String, ByVal text As String, ByVal append As Boolean, ByVal encoding As Encoding)
            Dim mode As FileMode

            If append Then
                mode = FileMode.Append
            Else
                mode = FileMode.Create
            End If

            Using reader As New System.IO.FileStream(file, mode, FileAccess.Write, FileShare.Read)
                Using reader2 As New System.IO.StreamWriter(reader, encoding)
                    reader2.Write(text)
                End Using
            End Using
        End Sub

        Public Shared Property CurrentDirectory() As String
            Get
                Return Environment.CurrentDirectory
            End Get
            Set(ByVal value As String)
                Environment.CurrentDirectory = value
            End Set
        End Property

        Public Shared ReadOnly Property Drives() As ReadOnlyCollection(Of DriveInfo)
            Get
                Return New ReadOnlyCollection(Of DriveInfo)(System.IO.DriveInfo.GetDrives())
            End Get
        End Property
    End Class
End Namespace
