'
' FileSystemProxy.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com)
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

Imports Microsoft.VisualBasic.FileIO
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Text

Namespace Microsoft.VisualBasic.MyServices
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Class FileSystemProxy
        Friend Sub New()
            'Empty constructor
        End Sub

        Public Function CombinePath(ByVal baseDirectory As String, ByVal relativePath As String) As String
            Return System.IO.Path.Combine(baseDirectory, relativePath)
        End Function

        Public Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String)
            FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName)
        End Sub

        Public Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As FileIO.UIOption)
            FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, showUI)
        End Sub

        Public Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal overwrite As Boolean)
            FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, overwrite)
        End Sub

        Public Sub CopyDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As FileIO.UIOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel)
        End Sub

        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String)
            FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName)
        End Sub

        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As FileIO.UIOption)
            FileIO.FileSystem.CopyDirectory(sourceFileName, destinationFileName, showUI)
        End Sub

        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal overwrite As Boolean)
            FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName, overwrite)
        End Sub

        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As FileIO.UIOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.CopyDirectory(sourceFileName, destinationFileName, showUI, onUserCancel)
        End Sub

        Public Sub CreateDirectory(ByVal directory As String)
            FileIO.FileSystem.CreateDirectory(directory)
        End Sub

        Public Sub DeleteDirectory(ByVal directory As String, ByVal onDirectoryNotEmpty As FileIO.DeleteDirectoryOption)
            FileIO.FileSystem.DeleteDirectory(directory, onDirectoryNotEmpty)
        End Sub

        Public Sub DeleteDirectory(ByVal directory As String, ByVal showUI As FileIO.UIOption, ByVal recycle As FileIO.RecycleOption)
            FileIO.FileSystem.DeleteDirectory(directory, showUI, recycle)
        End Sub

        Public Sub DeleteDirectory(ByVal directory As String, ByVal showUI As FileIO.UIOption, ByVal recycle As FileIO.RecycleOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.DeleteDirectory(directory, showUI, recycle, onUserCancel)
        End Sub

        Public Sub DeleteFile(ByVal file As String)
            FileIO.FileSystem.DeleteFile(file)
        End Sub

        Public Sub DeleteFile(ByVal file As String, ByVal showUI As FileIO.UIOption, ByVal recycle As FileIO.RecycleOption)
            FileIO.FileSystem.DeleteDirectory(file, showUI, recycle)
        End Sub

        Public Sub DeleteFile(ByVal file As String, ByVal showUI As FileIO.UIOption, ByVal recycle As FileIO.RecycleOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.DeleteFile(file, showUI, recycle, onUserCancel)
        End Sub

        Public Function DirectoryExists(ByVal directory As String) As Boolean
            Return FileIO.FileSystem.DirectoryExists(directory)
        End Function

        Public Function FileExists(ByVal file As String) As Boolean
            Return FileIO.FileSystem.FileExists(file)
        End Function

        Public Function FindInFiles(ByVal directory As String, ByVal containsText As String, ByVal ignoreCase As Boolean, ByVal searchType As FileIO.SearchOption) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.FindInFiles(directory, containsText, ignoreCase, searchType)
            Throw New NotImplementedException
        End Function

        Public Function FindInFiles(ByVal directory As String, ByVal containsText As String, ByVal ignoreCase As Boolean, ByVal searchType As SearchOption, ByVal ParamArray fileWildcards As String()) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.FindInFiles(directory, containsText, ignoreCase, FileIO.SearchOption.SearchTopLevelOnly, fileWildcards)
        End Function

        Public Function GetDirectories(ByVal directory As String) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.GetDirectories(directory)
        End Function

        Public Function GetDirectories(ByVal directory As String, ByVal searchType As FileIO.SearchOption, ByVal ParamArray wildcards As String()) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.GetDirectories(directory, searchType, wildcards)
        End Function

        Public Function GetDirectoryInfo(ByVal directory As String) As System.IO.DirectoryInfo
            Return FileIO.FileSystem.GetDirectoryInfo(directory)
        End Function

        Public Function GetDriveInfo(ByVal drive As String) As System.IO.DriveInfo
            Return FileIO.FileSystem.GetDriveInfo(drive)
        End Function

        Public Function GetFileInfo(ByVal file As String) As System.IO.FileInfo
            Return FileIO.FileSystem.GetFileInfo(file)
        End Function

        Public Function GetFiles(ByVal directory As String) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.GetFiles(directory)
        End Function

        Public Function GetFiles(ByVal directory As String, ByVal searchType As FileIO.SearchOption, ByVal ParamArray wildcards As String()) As ReadOnlyCollection(Of String)
            Return FileIO.FileSystem.GetFiles(directory, searchType, wildcards)
        End Function

        Public Function GetName(ByVal path As String) As String
            Return FileIO.FileSystem.GetName(path)
        End Function

        Public Function GetParentPath(ByVal path As String) As String
            Return FileIO.FileSystem.GetParentPath(path)
        End Function

        Public Function GetTempFileName() As String
            Return FileIO.FileSystem.GetTempFileName()
        End Function

        Public Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String)
            FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName)
        End Sub

        Public Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As FileIO.UIOption)
            FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI)
        End Sub

        Public Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal overwrite As Boolean)
            FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, overwrite)
        End Sub

        Public Sub MoveDirectory(ByVal sourceDirectoryName As String, ByVal destinationDirectoryName As String, ByVal showUI As FileIO.UIOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel)
        End Sub

        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String)
            FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName)
        End Sub

        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As FileIO.UIOption)
            FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI)
        End Sub

        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal overwrite As Boolean)
            FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, overwrite)
        End Sub

        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As FileIO.UIOption, ByVal onUserCancel As FileIO.UICancelOption)
            FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI, onUserCancel)
        End Sub

        Public Function OpenTextFieldParser(ByVal file As String) As FileIO.TextFieldParser
            Return FileIO.FileSystem.OpenTextFieldParser(file)
        End Function

        Public Function OpenTextFieldParser(ByVal file As String, ByVal ParamArray fieldWidths As Integer()) As FileIO.TextFieldParser
            Return FileIO.FileSystem.OpenTextFieldParser(file, fieldWidths)
        End Function

        Public Function OpenTextFieldParser(ByVal file As String, ByVal ParamArray delimiters As String()) As FileIO.TextFieldParser
            Return FileIO.FileSystem.OpenTextFieldParser(file, delimiters)
        End Function

        Public Function OpenTextFileReader(ByVal file As String) As System.IO.StreamReader
            Return FileIO.FileSystem.OpenTextFileReader(file)
        End Function

        Public Function OpenTextFileReader(ByVal file As String, ByVal encoding As Encoding) As System.IO.StreamReader
            Return FileIO.FileSystem.OpenTextFileReader(file, encoding)
        End Function

        Public Function OpenTextFileWriter(ByVal file As String, ByVal append As Boolean) As System.IO.StreamWriter
            Return FileIO.FileSystem.OpenTextFileWriter(file, append)
        End Function

        Public Function OpenTextFileWriter(ByVal file As String, ByVal append As Boolean, ByVal encoding As Encoding) As System.IO.StreamWriter
            Return FileIO.FileSystem.OpenTextFileWriter(file, append, encoding)
        End Function

        Public Function ReadAllBytes(ByVal file As String) As Byte()
            Return FileIO.FileSystem.ReadAllBytes(file)
        End Function

        Public Function ReadAllText(ByVal file As String) As String
            Return FileIO.FileSystem.ReadAllText(file)
        End Function

        Public Function ReadAllText(ByVal file As String, ByVal encoding As Encoding) As String
            Return FileIO.FileSystem.ReadAllText(file, encoding)
        End Function

        Public Sub RenameDirectory(ByVal directory As String, ByVal newName As String)
            FileIO.FileSystem.RenameDirectory(directory, newName)
        End Sub

        Public Sub RenameFile(ByVal file As String, ByVal newName As String)
            FileIO.FileSystem.RenameFile(file, newName)
        End Sub

        Public Sub WriteAllBytes(ByVal file As String, ByVal data As Byte(), ByVal append As Boolean)
            FileIO.FileSystem.WriteAllBytes(file, data, append)
        End Sub

        Public Sub WriteAllText(ByVal file As String, ByVal text As String, ByVal append As Boolean)
            FileIO.FileSystem.WriteAllText(file, text, append)
        End Sub

        Public Sub WriteAllText(ByVal file As String, ByVal text As String, ByVal append As Boolean, ByVal encoding As Encoding)
            FileIO.FileSystem.WriteAllText(file, text, append, encoding)
        End Sub

        Public Property CurrentDirectory() As String
            Get
                Return FileIO.FileSystem.CurrentDirectory
            End Get
            Set(ByVal value As String)
                FileIO.FileSystem.CurrentDirectory = value
            End Set
        End Property

        Public ReadOnly Property Drives() As ReadOnlyCollection(Of System.IO.DriveInfo)
            Get
                Return FileIO.FileSystem.Drives
            End Get
        End Property
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
        Public ReadOnly Property SpecialDirectories() As SpecialDirectoriesProxy
            Get
                Return New SpecialDirectoriesProxy()
            End Get
        End Property
#End If
    End Class
End Namespace
