'
' FileSystem.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Guy Cohen (guyc@mainsoft.com)

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
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
Imports System
Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Collections

Namespace Microsoft.VisualBasic
    Public Module FileSystem

        ' Dir private members
        Private m_Index As Integer
        Private m_FileSystemInfos As FileSystemInfo()
        'Private m_files As FileInfo()
        'Private m_dirs As DirectoryInfo()
        'Private m_IsFile As Boolean = True
        'Private ResStr As String = ""
        'Private m_len As Integer = 0
        'Private m_IsLastElem As Boolean = Nothing

        Private m_OpenFiles As Hashtable

        Public Sub ChDir(ByVal Path As String)
            If ((Path = "") Or (Path Is Nothing)) Then Throw New System.ArgumentException("Argument 'Path' is Nothing or empty.")

            Dim fileinfo As New FileInfo(Path)
            If (fileinfo.Exists) Then Throw New System.IO.IOException("The Directory name is invalid.")

            Dim dirinfo As New DirectoryInfo(Path)
            If (dirinfo.Exists) Then
                Directory.SetCurrentDirectory(Path)
            Else
                Throw New System.IO.FileNotFoundException("Path " + "'" + Path + "'" + " not found.")
            End If

        End Sub
        Public Sub ChDrive(ByVal Drive As Char)
            If (Drive = "") Then Return
            If Not Char.IsLetter(Drive) Then Throw New System.ArgumentException("Argument 'Drive' is not a valid value.")

            Try
                Directory.SetCurrentDirectory(Drive + Path.VolumeSeparatorChar)
            Catch ex As DirectoryNotFoundException
                Throw New System.IO.IOException("Drive " + "'" + Drive + "'" + " not found.")

            End Try

        End Sub
        Public Sub ChDrive(ByVal Drive As String)
            If (Drive Is Nothing) Or (Drive = "") Then Return
            Dim ch As Char = CChar(Drive.Substring(0, 1))
            FileSystem.ChDrive(ch)

        End Sub
        Public Function CurDir() As String
            Return Directory.GetCurrentDirectory()
        End Function
        Public Function CurDir(ByVal Drive As Char) As String
            If Not Char.IsLetter(Drive) Then Throw New System.ArgumentException("Argument 'Drive' is not a valid value.")
            Try
                Directory.SetCurrentDirectory(Drive + Path.VolumeSeparatorChar)
            Catch ex As DirectoryNotFoundException
                Throw New System.IO.IOException("Drive " + "'" + Drive + "'" + " not found.")
            End Try

            Return Path.GetFullPath(Convert.ToString(Drive))
        End Function
        Public Function Dir() As String
            If m_FileSystemInfos Is Nothing Then
                Throw New System.ArgumentException("'Dir' function must first be called with a 'Pathname' argument.")
            ElseIf m_FileSystemInfos.Length < m_Index Then
                Throw New System.ArgumentException("'Dir' function must first be called with a 'Pathname' argument.")
            ElseIf m_FileSystemInfos.Length = m_Index Then
                m_Index += 1
                Return Nothing
            Else
                m_Index += 1
                Return m_FileSystemInfos(m_Index - 1).Name
            End If
        End Function

        Public Function Dir(ByVal Pathname As String, Optional ByVal Attributes As Microsoft.VisualBasic.FileAttribute = 0) As String
            Dim str_parent_dir As String = Nothing
            Dim str_pattern As String = Nothing
            Dim di As DirectoryInfo
            Dim dirs As DirectoryInfo() = nothing
            Dim files As FileInfo()
            Dim result As String
            Dim length As Integer

            m_FileSystemInfos = Nothing
            m_Index = 0

            If Pathname <> String.Empty Then
                str_parent_dir = IO.Path.GetDirectoryName(Pathname)
            End If

            If str_parent_dir = String.Empty Then
                str_parent_dir = Directory.GetCurrentDirectory
            End If
            str_pattern = IO.Path.GetFileName(Pathname)

            '' dir() doesn`t throw any exception just return ""
            di = New DirectoryInfo(str_parent_dir)
            If di.Exists = False Then Return String.Empty

            If (Attributes And FileAttributes.Directory) <> 0 Then
                If (str_pattern = String.Empty) Then
                    dirs = di.GetDirectories()
                Else
                    dirs = di.GetDirectories(str_pattern)
                End If
            End If

            If str_pattern = String.Empty Then
                files = di.GetFiles()
            Else
                files = di.GetFiles(str_pattern)
            End If

            length = files.Length
            If Not dirs Is Nothing Then length += dirs.Length

            ReDim m_FileSystemInfos(length - 1)

            If Not dirs Is Nothing Then
                Array.Copy(dirs, m_FileSystemInfos, dirs.Length)
                Array.Copy(files, 0, m_FileSystemInfos, dirs.Length, files.Length)
            Else
                Array.Copy(files, m_FileSystemInfos, files.Length)
            End If

            If m_FileSystemInfos.Length > 0 Then
                result = m_FileSystemInfos(0).Name
                m_Index += 1
            Else
                result = String.Empty
            End If

            Return result
        End Function

        Private Function FindFileData(ByVal FileNumber As Integer) As FileData
            If m_OpenFiles Is Nothing OrElse m_OpenFiles.ContainsKey(FileNumber) = False Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR52_Bad_file_name_or_number)
            End If

            Return DirectCast(m_OpenFiles(FileNumber), FileData)
        End Function

        Public Function EOF(ByVal FileNumber As Integer) As Boolean
            Return FindFileData(FileNumber).EOF
        End Function

        Public Function FileAttr(ByVal FileNumber As Integer) As Microsoft.VisualBasic.OpenMode
            Return FindFileData(FileNumber).Mode
        End Function

        Public Sub FileClose(ByVal ParamArray FileNumbers() As Integer)
            If m_OpenFiles Is Nothing OrElse m_OpenFiles.Count = 0 Then Return

            If FileNumbers Is Nothing OrElse FileNumbers.Length = 0 Then
                For Each fd As FileData In m_OpenFiles.Values
                    fd.Close()
                Next
                m_OpenFiles = Nothing
                Return
            End If

            For i As Integer = 0 To FileNumbers.Length - 1
                Dim number As Integer = FileNumbers(i)
                Dim fd As FileData

                If m_OpenFiles.ContainsKey(number) = False Then
                    Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR52_Bad_file_name_or_number)
                End If
                fd = DirectCast(m_OpenFiles(number), FileData)
                fd.Close()
                m_OpenFiles.Remove(FileNumbers(i))
            Next
        End Sub

        Public Sub FileCopy(ByVal Source As String, ByVal Destination As String)
            '' seems File.Copy throw the same exceptions VB requires 
            File.Copy(Source, Destination, True)
        End Sub
        Public Function FileDateTime(ByVal PathName As String) As Date
            If (PathName = "") Then Throw New System.IO.FileNotFoundException("File " + "'" + "'" + " not found.")

            Dim InvalidChars() As Char
#If NET_VER >= 2.0 Then
            InvalidChars = Path.GetInvalidPathChars()
#Else
            InvalidChars = Path.InvalidPathChars
#End If
            If Not (PathName.IndexOfAny(InvalidChars) = -1) Then
                Throw New System.ArgumentException("Argument 'PathName' is not a valid value.")
            End If

            Dim fi As New FileInfo(PathName)
            If (fi.Exists) Then
                Return fi.LastWriteTime
            Else
                Throw New System.IO.FileNotFoundException("File " + "'" + PathName + "'" + " not found.")
            End If
        End Function

        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Byte, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Char, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Date, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Decimal, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Double, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Integer, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Long, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Short, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As Single, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As System.Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGet(ByVal FileNumber As Integer, ByRef Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGet(Value, RecordNumber)
        End Sub
        Public Sub FileGetObject(ByVal FileNumber As Integer, ByRef Value As Object, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FileGetObject(Value, RecordNumber)
        End Sub
        Public Function FileLen(ByVal PathName As String) As Long
            If ((PathName = "") Or (PathName Is Nothing)) Then Throw New System.IO.FileNotFoundException("File " + "'" + PathName + "'" + " not found.")
            Dim fi As New FileInfo(PathName)
            If (fi.Exists) Then
                Return fi.Length
            Else
                Throw New System.IO.FileNotFoundException("File " + "'" + PathName + "'" + " not found.")
            End If

        End Function

        Public Sub FileOpen(ByVal FileNumber As Integer, ByVal FileName As String, ByVal Mode As Microsoft.VisualBasic.OpenMode, Optional ByVal Access As Microsoft.VisualBasic.OpenAccess = Microsoft.VisualBasic.OpenAccess.[Default], Optional ByVal Share As Microsoft.VisualBasic.OpenShare = Microsoft.VisualBasic.OpenShare.[Default], Optional ByVal RecordLength As Integer = -1)

            If FileNumber <= 0 OrElse FileNumber > 255 OrElse (Not m_OpenFiles Is Nothing AndAlso m_OpenFiles.ContainsKey(FileNumber)) Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR52_Bad_file_name_or_number)
            End If

            If m_OpenFiles Is Nothing Then
                m_OpenFiles = New Hashtable
            End If

            If Mode <> OpenMode.Append AndAlso Mode <> OpenMode.Binary AndAlso Mode <> OpenMode.Input AndAlso Mode <> OpenMode.Output AndAlso Mode <> OpenMode.Random Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End If

            If Access <> OpenAccess.Default AndAlso Access <> OpenAccess.Read AndAlso Access <> OpenAccess.ReadWrite AndAlso Access <> OpenAccess.Write Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End If

            If Share <> OpenShare.Default AndAlso Share <> OpenShare.LockRead AndAlso Share <> OpenShare.LockReadWrite AndAlso Share <> OpenShare.LockWrite AndAlso Share <> OpenShare.Shared Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End If

            If (Access = OpenAccess.Write OrElse Access = OpenAccess.ReadWrite) AndAlso Mode = OpenMode.Input Then
                Throw New ArgumentException("Argument 'Access' is not valid. Valid values for Input mode are 'OpenAccess.Read' and 'OpenAccess.Default'.")
            End If

            If (Access = OpenAccess.Read OrElse Access = OpenAccess.ReadWrite) AndAlso Mode = OpenMode.Output Then
                Throw New ArgumentException("Argument 'Access' is not valid. Valid values for Output mode are 'OpenAccess.Write' and 'OpenAccess.Default'.")
            End If

            If Access = OpenAccess.Read AndAlso Mode = OpenMode.Append Then
                Throw New ArgumentException("Argument 'Access' is not valid. Valid values for Append mode are 'OpenAccess.Write' and 'OpenAccess.Default'.")
            End If

            If RecordLength < -1 Then
                Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR5_Invalid_procedure_call)
            End If

            Dim data As New FileData(FileNumber, FileName, Mode, Access, Share, RecordLength)

            m_OpenFiles.Add(FileNumber, data)

            'data.CreateStream()
        End Sub

        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Boolean, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Byte, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Char, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Date, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Decimal, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Double, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Integer, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Long, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Short, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As Single, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As String, Optional ByVal RecordNumber As Long = -1, Optional ByVal StringIsFixedLength As Boolean = False)
            FindFileData(FileNumber).FilePut(Value, RecordNumber, StringIsFixedLength)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As System.Array, Optional ByVal RecordNumber As Long = -1, Optional ByVal ArrayIsDynamic As Boolean = False, Optional ByVal StringIsFixedLength As Boolean = False)
            FindFileData(FileNumber).FilePut(Value, RecordNumber, ArrayIsDynamic, StringIsFixedLength)
        End Sub
        Public Sub FilePut(ByVal FileNumber As Integer, ByVal Value As System.ValueType, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePut(Value, RecordNumber)
        End Sub
#If NET_VER >= 2.0 Then
        <Obsolete("This member has been deprectated. Try FilePutObject.")> _
        Public Sub FilePut(ByVal FileNumber As Object, ByVal Value As Object, Optional ByVal RecordNumber As Object = -1)
#Else
        Public Sub FilePut(ByVal FileNumber As Object, ByVal Value As Object, Optional ByVal RecordNumber As Object = -1)
#End If
            Throw New ArgumentException("Use 'FilePutObject' instead of 'FilePut' when using argument of type 'Object'.")
        End Sub
        Public Sub FilePutObject(ByVal FileNumber As Integer, ByVal Value As Object, Optional ByVal RecordNumber As Long = -1)
            FindFileData(FileNumber).FilePutObject(Value, RecordNumber)
        End Sub
        Public Sub FileWidth(ByVal FileNumber As Integer, ByVal RecordWidth As Integer)
            FindFileData(FileNumber).FileWidth(RecordWidth)
        End Sub

        Public Function FreeFile() As Integer
            If m_OpenFiles Is Nothing Then Return 1

            For i As Integer = 1 To 255
                If m_OpenFiles.ContainsKey(i) = False Then
                    Return i
                End If
            Next

            Throw Microsoft.VisualBasic.CompilerServices.ExceptionUtils.GetVBException(VBErrors.ERR67_Too_many_files)
        End Function

        Public Function GetAttr(ByVal PathName As String) As Microsoft.VisualBasic.FileAttribute

            If ((PathName = "") Or (PathName Is Nothing)) Then Throw New System.ArgumentException("The path is not of a legal form.")

            Dim InvalidChars() As Char
#If NET_VER >= 2.0 Then
            InvalidChars = Path.GetInvalidPathChars()
#Else
            InvalidChars = Path.InvalidPathChars
#End If
            If Not (PathName.IndexOfAny(InvalidChars) = -1) Then
                Throw New System.ArgumentException("Argument 'PathName' is not a valid value.")
            End If

            Dim fi As New FileInfo(PathName)
            Dim di As New DirectoryInfo(PathName)
            Dim attr As System.IO.FileAttributes

            If (fi.Exists Or di.Exists) Then
                attr = File.GetAttributes(PathName)
            Else
                Throw New System.IO.FileNotFoundException("File " + "'" + PathName + "'" + " not found.")
            End If

            Return CType(attr, Microsoft.VisualBasic.FileAttribute)
        End Function
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Boolean)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Byte)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Char)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Date)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Decimal)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Double)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Integer)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Long)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Object)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Short)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As Single)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Sub Input(ByVal FileNumber As Integer, ByRef Value As String)
            FindFileData(FileNumber).Input(Value)
        End Sub
        Public Function InputString(ByVal FileNumber As Integer, ByVal CharCount As Integer) As String
            Return FindFileData(FileNumber).InputString(CharCount)
        End Function
        Public Sub Kill(ByVal PathName As String)
            If ((PathName = "") Or (PathName Is Nothing)) Then Throw New System.ArgumentException("The path is not of a legal form.")
            Dim str_parent_dir, str_file_to_delete As String
            Dim last_ch, i As Integer
            Dim di As DirectoryInfo
            Dim tmpFile As FileInfo()

            last_ch = PathName.LastIndexOf(Path.DirectorySeparatorChar)
            If (last_ch = -1) Then
                str_parent_dir = Directory.GetCurrentDirectory()
            Else
                str_parent_dir = PathName.Substring(0, last_ch)
            End If

            str_file_to_delete = PathName.Substring(last_ch + 1, PathName.Length - last_ch - 1)

            di = New DirectoryInfo(str_parent_dir)
            tmpFile = di.GetFiles(str_file_to_delete)
            If Not (tmpFile.Length = 0) Then
                For i = 0 To tmpFile.Length - 1
                    File.Delete(str_parent_dir + Path.DirectorySeparatorChar + tmpFile(i).Name)
                Next
            Else
                Throw New System.IO.FileNotFoundException(" No files found matching: " + PathName)
            End If

        End Sub
        Public Function LineInput(ByVal FileNumber As Integer) As String
            Return FindFileData(FileNumber).LineInput
        End Function
        Public Function Loc(ByVal FileNumber As Integer) As Long
            Return FindFileData(FileNumber).Loc()
        End Function
        Public Sub Lock(ByVal FileNumber As Integer)
            FindFileData(FileNumber).Lock()
        End Sub
        Public Sub Lock(ByVal FileNumber As Integer, ByVal Record As Long)
            FindFileData(FileNumber).Lock(Record)
        End Sub
        Public Sub Lock(ByVal FileNumber As Integer, ByVal FromRecord As Long, ByVal ToRecord As Long)
            FindFileData(FileNumber).Lock(FromRecord, ToRecord)
        End Sub
        Public Function LOF(ByVal FileNumber As Integer) As Long
            FindFileData(FileNumber).LOF()
        End Function
        Public Sub MkDir(ByVal Path As String)
            If ((Path = "") Or (Path Is Nothing)) Then Throw New System.ArgumentException("Argument 'Path' is Nothing or empty.")
            Dim di As New DirectoryInfo(Path)
            If (di.Exists) Then
                Throw New System.IO.IOException("Path/File access error.")
            Else
                Directory.CreateDirectory(Path)
            End If

        End Sub
        Public Sub Print(ByVal FileNumber As Integer, ByVal ParamArray Output() As Object)
            FindFileData(FileNumber).Print(Output)
        End Sub
        Public Sub PrintLine(ByVal FileNumber As Integer, ByVal ParamArray Output() As Object)
            FindFileData(FileNumber).PrintLine(Output)
        End Sub
        Public Sub Rename(ByVal OldPath As String, ByVal NewPath As String)
            If ((OldPath = "") Or (OldPath Is Nothing) Or (NewPath = "") Or (NewPath Is Nothing)) Then Throw New System.ArgumentException("The path is not of a legal form.")

            Dim fiNew As New FileInfo(NewPath)
            Dim fiOld As New FileInfo(OldPath)
            Dim diNew As New DirectoryInfo(NewPath)
            Dim diOld As New DirectoryInfo(OldPath)

            Try

            Catch ex As DirectoryNotFoundException
                Throw New System.ArgumentException("Procedure call or argument is not valid.")
            End Try
            If Not (fiOld.Exists) And Not (diOld.Exists) Then
                Throw New System.ArgumentException("Procedure call or argument is not valid.")
            ElseIf (fiOld.Exists) And Not (diOld.Exists) Then
                '' MSDN says IOException on this scenario(as File.Move throw), 
                '' but FileSystem.Rename actually returns ArgumentException
                If fiNew.Exists Then Throw New System.ArgumentException("Procedure call or argument is not valid.")
                Try
                    File.Move(OldPath, NewPath)
                Catch ex As DirectoryNotFoundException
                    Throw New System.ArgumentException("Procedure call or argument is not valid.")
                End Try
            ElseIf Not (fiOld.Exists) And (diOld.Exists) Then
                '' MSDN says IOException on this scenario(as Directory.Move throw), 
                '' but FileSystem.Rename actually returns ArgumentException
                If diNew.Exists Then
#If NET_VER >= 2.0 Then
                    Throw New System.IO.IOException("File already exists.")
#Else
                    Throw New System.IO.FileNotFoundException("File not found.")
#End If
                End If
                Try
                    Directory.Move(OldPath, NewPath)
                Catch ex As DirectoryNotFoundException
                    Throw New System.ArgumentException("Procedure call or argument is not valid.")
                End Try
            End If
        End Sub
        Public Sub Reset()
            FileClose()
        End Sub
        Public Sub RmDir(ByVal Path As String)
            Dim fi As FileInfo()

            If ((Path = "") Or (Path Is Nothing)) Then Throw New System.ArgumentException("Argument 'Path' is Nothing or empty.")
            Dim di As New DirectoryInfo(Path)
            fi = di.GetFiles
            If Not (fi.Length = 0) Then
                Throw New System.IO.IOException("The directory is not empty.")
            Else
                Directory.Delete(Path)
            End If

        End Sub
        Public Function Seek(ByVal FileNumber As Integer) As Long
            Return FindFileData(FileNumber).Seek
        End Function
        Public Sub Seek(ByVal FileNumber As Integer, ByVal Position As Long)
            FindFileData(FileNumber).Seek(Position)
        End Sub
        Public Sub SetAttr(ByVal PathName As String, ByVal Attributes As Microsoft.VisualBasic.FileAttribute)

            If ((PathName = "") Or (PathName Is Nothing)) Then Throw New System.ArgumentException("The path is not of a legal form.")

            Dim InvalidChars() As Char
#If NET_VER >= 2.0 Then
            InvalidChars = Path.GetInvalidPathChars()
#Else
            InvalidChars = Path.InvalidPathChars
#End If
            If Not (PathName.IndexOfAny(InvalidChars) = -1) Then
                Throw New System.ArgumentException("Argument 'PathName' is not a valid value.")
            End If

            Dim fi As New FileInfo(PathName)
            Dim di As New DirectoryInfo(PathName)

            If (fi.Exists Or di.Exists) Then
                Dim attr As System.IO.FileAttributes
                attr = CType(Attributes, System.IO.FileAttributes)
                File.SetAttributes(PathName, attr)
            Else
                Throw New System.IO.FileNotFoundException("File " + "'" + PathName + "'" + " not found.")
            End If

        End Sub
        Public Function SPC(ByVal Count As Short) As Microsoft.VisualBasic.SpcInfo
            Dim info As Microsoft.VisualBasic.SpcInfo
            info.Count = Count
            Return info
        End Function
        Public Function TAB() As Microsoft.VisualBasic.TabInfo
            Dim info As TabInfo
            info.Column = -1 'TODO: Add test
            Return info
        End Function
        Public Function TAB(ByVal Column As Short) As Microsoft.VisualBasic.TabInfo
            Dim info As TabInfo
            info.Column = Column
            Return info
        End Function
        Public Sub Unlock(ByVal FileNumber As Integer)
            FindFileData(FileNumber).Unlock()
        End Sub
        Public Sub Unlock(ByVal FileNumber As Integer, ByVal Record As Long)
            FindFileData(FileNumber).Unlock(Record)
        End Sub
        Public Sub Unlock(ByVal FileNumber As Integer, ByVal FromRecord As Long, ByVal ToRecord As Long)
            FindFileData(FileNumber).Unlock(FromRecord, ToRecord)
        End Sub
        Public Sub Write(ByVal FileNumber As Integer, ByVal ParamArray Output() As Object)
            FindFileData(FileNumber).Write(Output)
        End Sub
        Public Sub WriteLine(ByVal FileNumber As Integer, ByVal ParamArray Output() As Object)
            FindFileData(FileNumber).WriteLine(Output)
        End Sub

  
    End Module
End Namespace
