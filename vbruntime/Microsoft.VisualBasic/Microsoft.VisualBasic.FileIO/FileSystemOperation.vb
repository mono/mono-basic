'
' FileSystemOperation.vb
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
Imports System.Collections.Generic

Namespace Microsoft.VisualBasic.FileIO
    Friend Class FileSystemOperation
        Private m_Source As String
        Private m_Destination As String
        Private m_Overwrite As Boolean
        Private m_DirectoryNotEmpty As DeleteDirectoryOption
        Private m_ShowUI As Boolean
        Private m_ShowUIOption As UIOption
        Private m_UICancelOption As UICancelOption
        Private m_Recycle As RecycleOption
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
        Private m_UI As FileSystemOperationUI
#End If
        Private m_Sources As New Generic.List(Of Info)
        Private m_TotalSize As Long
        Private m_Cancelled As Boolean
        Private m_Errors As New Generic.Dictionary(Of String, String)

        Private Class Info
            Public Name As String
            Public Size As Long
            Public IsDir As Boolean
        End Class

#Region "Constructors"
        Sub New(ByVal Source As String, ByVal Destination As String, ByVal ShowUIOption As UIOption, ByVal UICancelOption As UICancelOption, ByVal ShowUI As Boolean, ByVal overwrite As Boolean)
            Me.m_Source = Source
            Me.m_Destination = Destination
            Me.m_ShowUI = ShowUI
            Me.m_ShowUIOption = ShowUIOption
            Me.m_UICancelOption = UICancelOption
            Me.m_Overwrite = overwrite
        End Sub

        Sub New(ByVal Source As String, ByVal Destination As String, ByVal ShowUIOption As UIOption, ByVal UICancelOption As UICancelOption)
            Me.m_Source = Source
            Me.m_Destination = Destination
            Me.m_ShowUI = True
            Me.m_ShowUIOption = ShowUIOption
            Me.m_UICancelOption = UICancelOption
            Me.m_Overwrite = False
        End Sub

        Sub New(ByVal Source As String, ByVal Destination As String, ByVal Overwrite As Boolean)
            Me.m_Source = Source
            Me.m_Destination = Destination
            Me.m_ShowUI = False
            Me.m_Overwrite = Overwrite
        End Sub

        Sub New(ByVal Directory As String, ByVal ShowUIOption As UIOption, ByVal Recycle As RecycleOption, ByVal UICancelOption As UICancelOption, ByVal onDirectoryNotEmpty As DeleteDirectoryOption, ByVal ShowUI As Boolean)
            Me.m_Source = Directory
            Me.m_ShowUI = ShowUI
            Me.m_ShowUIOption = ShowUIOption
            Me.m_UICancelOption = UICancelOption
            Me.m_Recycle = Recycle
            Me.m_DirectoryNotEmpty = onDirectoryNotEmpty
            Me.m_Overwrite = False
        End Sub

        Sub New(ByVal Directory As String, ByVal ShowUIOption As UIOption, ByVal Recycle As RecycleOption, ByVal UICancelOption As UICancelOption, ByVal ShowUI As Boolean)
            Me.m_Source = Directory
            Me.m_ShowUI = ShowUI
            Me.m_ShowUIOption = ShowUIOption
            Me.m_UICancelOption = UICancelOption
            Me.m_Recycle = Recycle
            Me.m_Overwrite = False
        End Sub
#End Region
#Region "Public Methods"
        Sub Cancel()
            m_Cancelled = True
        End Sub

        Sub ExecuteFileCopy()
            Try
                Init("Copy")
                LoadSources(False, True)
                Copy()
            Finally
                CleanUp()
            End Try
        End Sub

        Sub ExecuteFileMove()
            Try
                Init("Move")
                LoadSources(False, True)
                Move()
            Finally
                CleanUp()
            End Try
        End Sub

        Sub ExecuteDirCopy()
            Try
                Init("Copy")
                LoadSources(True, True)
                Copy()
            Finally
                CleanUp()
            End Try
        End Sub

        Sub ExecuteDirMove()
            Try
                Init("Move")

                If IO.Directory.Exists(m_Source) = False Then
                    Throw New IOException(String.Format("Could not find directory '{0}'.", m_Source))
                End If

                LoadSources(True, False)
                Move()
            Finally
                CleanUp()
            End Try
        End Sub

        Sub ExecuteDirDelete()
            Try
                Init("Delete")
                If IO.Directory.Exists(m_Source) = False Then
                    Return
                End If

                LoadSources(m_Source, m_DirectoryNotEmpty = DeleteDirectoryOption.DeleteAllContents, False)
                If m_DirectoryNotEmpty = DeleteDirectoryOption.ThrowIfDirectoryNonEmpty AndAlso m_Sources.Count > 1 Then
                    Throw New IOException("Directory not empty")
                End If
                Delete()
            Finally
                CleanUp()
            End Try
        End Sub

        Sub ExecuteFileDelete()
            Try
                Init("Delete")
                LoadSources(m_Source, False, False)
                Delete()
            Finally
                CleanUp()
            End Try
        End Sub
#End Region

        Private Function GetDestination(ByVal Source As String) As String
            Dim result As String

            result = Source.Replace(m_Source, m_Destination)

            Return result
        End Function

        Private Sub Move()
            Dim counter As Integer
            Dim size As Long

            size = 0

            If OnSameVolume Then
                If IsDirectory(m_Source) Then
                    UpdateUI(Path.GetDirectoryName(m_Source), m_Destination, Path.GetFileName(m_Source), 0, 0)
                    System.IO.Directory.Move(m_Source, m_Destination)
                Else
                    If IO.File.Exists(m_Destination) Then
                        If DoOverwrite(m_Source, m_Destination) Then
                            System.IO.File.Delete(m_Destination)
                        Else
                            m_Cancelled = True
                            Return
                        End If
                    End If
                    System.IO.File.Move(m_Source, m_Destination)
                End If
            Else
                For i As Integer = m_Sources.Count - 1 To 0 Step -1
                    Dim info As Info = m_Sources(i)
                    Dim item As String = info.Name
                    Dim destination As String

                    destination = GetDestination(item)

                    CopyItem(info, destination, counter, size)
                    If m_Cancelled Then Return
                    DeleteItem(info, counter, False)
                    counter += 1


                    If m_Cancelled Then Return
                Next
            End If

            UpdateUI(Nothing, Nothing, Nothing, m_Sources.Count, 0)
        End Sub

        Private Sub Copy()
            Dim counter As Integer
            Dim size As Long

            size = 0
            For i As Integer = 0 To m_Sources.Count - 1
                Dim info As Info = m_Sources(i)
                Dim item As String = info.Name
                Dim destination As String

                destination = GetDestination(item)

                CopyItem(info, destination, counter, size)
                counter += 1

                If m_Cancelled Then Return
            Next
            UpdateUI(Nothing, Nothing, Nothing, m_Sources.Count, size)
        End Sub

        Private Sub CopyItem(ByVal Info As Info, ByVal Destination As String, ByVal Counter As Integer, ByRef Size As Long)
            Dim Source As String = Info.Name
            If Info.IsDir Then
                UpdateUI(Source, Nothing, Nothing, Counter, 0)
                If IO.Directory.Exists(Destination) = False Then
                    IO.Directory.CreateDirectory(Destination)
                End If
            Else
                UpdateUI(Path.GetDirectoryName(Source), Path.GetDirectoryName(Destination), Path.GetFileName(Source), Counter, Size)
                CopyFile(Source, Destination, Size)
            End If
        End Sub

        Private Sub DeleteItem(ByVal Info As Info, ByVal Counter As Integer, ByVal DoUpdate As Boolean)
            Dim Item As String = Info.Name
            If Info.IsDir Then
                If DoUpdate Then UpdateUI(Item, Nothing, Nothing, Counter, 0)
                System.IO.Directory.Delete(Item, False)
            Else
                If DoUpdate Then UpdateUI(Path.GetDirectoryName(Item), Nothing, Path.GetFileName(Item), Counter, 0)
                System.IO.File.Delete(Item)
            End If
        End Sub

        Private Function IsDirectory(ByVal Path As String) As Boolean
            Return CBool(System.IO.File.GetAttributes(Path) And FileAttributes.Directory)
        End Function

        Private Sub CopyFile(ByVal Source As String, ByVal Destination As String, ByRef Size As Long)
            Dim newFileMode As FileMode

            If IO.File.Exists(Destination) Then
                If DoOverwrite(Source, Destination) = False Then Return
                newFileMode = FileMode.Create
            Else
                newFileMode = FileMode.CreateNew
            End If

            Try
#If TARGET_JVM = False Then 'FileStream ctor with FileOptions Not Supported by Grasshopper
                Using reader As New IO.FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.SequentialScan)
                    Using writer As New IO.FileStream(Destination, newFileMode, FileAccess.Write, FileShare.Read, 1024, FileOptions.SequentialScan)
#Else
                Using reader As New IO.FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.Read, 1024)
                    Using writer As New IO.FileStream(Destination, newFileMode, FileAccess.Write, FileShare.Read)
#End If
                        Dim read As Integer
                        Dim buffer(1023) As Byte

                        Do
                            read = reader.Read(buffer, 0, 1024)
                            writer.Write(buffer, 0, read)
                            Size = Size + CLng(read)
                            UpdateUI(Size)
                        Loop Until read = 0 OrElse m_Cancelled
                    End Using
                End Using
            Catch ex As IOException
                m_Errors.Add(Source, ex.Message)
            End Try
        End Sub

        Private Sub Delete()
            Dim counter As Integer

            If m_Recycle = RecycleOption.SendToRecycleBin Then
                Throw New NotImplementedException
            Else
                For i As Integer = m_Sources.Count - 1 To 0 Step -1
                    Dim info As Info = m_Sources(i)
                    Dim item As String = info.Name

                    DeleteItem(info, counter, True)
                    counter += 1

                    If m_Cancelled Then Return
                Next
            End If

            UpdateUI(Nothing, Nothing, Nothing, m_Sources.Count, 0)
        End Sub

        Private Sub Recycle(ByVal Source As String)

        End Sub

        Private Sub LoadSources(ByVal Recursive As Boolean, ByVal SizeMatters As Boolean)
            m_TotalSize = 0

            LoadSources(m_Source, Recursive, SizeMatters, 0)

        End Sub

        Private Sub LoadSources(ByVal Source As String, ByVal Recursive As Boolean, ByVal SizeMatters As Boolean, Optional ByRef DirSize As Long = 0)
            Dim subdirs() As String
            Dim files() As String

            Dim info2 As Info = Nothing
            Dim subsize As Long
            If CBool(System.IO.File.GetAttributes(Source) And FileAttributes.Directory) Then
                info2 = New Info
                info2.Name = Source
                info2.IsDir = True
                m_Sources.Add(info2)
                Debug.WriteLine(info2.Name)


                files = System.IO.Directory.GetFiles(Source)
                For Each file As String In files
                    Dim info As New Info
                    info.Name = file
                    info.IsDir = False
                    If SizeMatters Then
                        info.Size = (New FileInfo(file)).Length
                        m_TotalSize += info.Size
                        DirSize += info.Size
                    End If
                    m_Sources.Add(info)
                    Debug.WriteLine(info.Name)
                Next

                If Recursive Then
                    subdirs = System.IO.Directory.GetDirectories(Source)
                    For Each subdir As String In subdirs
                        LoadSources(subdir, Recursive, SizeMatters, subsize)
                    Next
                End If

                If info2 IsNot Nothing Then
                    info2.Size = subsize
                    DirSize += subsize
                End If
            Else
                Dim info As New Info
                info.Name = Source
                m_Sources.Add(info)
                If SizeMatters Then
                    info.Size = (New FileInfo(Source)).Length
                    m_TotalSize = info.Size
                End If
            End If

        End Sub

        Private Sub Init(ByVal Title As String)
            m_Source = Path.GetFullPath(m_Source)
            If Not m_Destination Is Nothing AndAlso m_Destination.Length <> 0 Then
                m_Destination = Path.GetFullPath(m_Destination)
            End If

            If m_ShowUI = False Then Return
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
            m_UI = New FileSystemOperationUI(Me)
            m_UI.Text = Title
            m_UI.lblDirs.Text = "Calculating time..."
            m_UI.lblFile.Text = String.Empty
            m_UI.lblTimeLeft.Text = "..."
            m_UI.barProgress.Value = 0
            m_UI.Show()
#End If
        End Sub

        Private Sub UpdateUI(ByVal SizeDone As Long)
            If m_ShowUI = False Then Return

            Dim PercentDone As Double
            If SizeDone > 0 AndAlso m_TotalSize > 0 Then
                PercentDone = SizeDone / m_TotalSize * 100
            Else
                PercentDone = 0
            End If
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
            m_UI.UpdateInfo(PercentDone)
#End If
        End Sub

        Private Sub UpdateUI(ByVal SourceDirectory As String, ByVal DestinationDirectory As String, ByVal File As String, ByVal ItemsDone As Integer, ByVal SizeDone As Long)
            If m_ShowUI = False Then Return

            Dim PercentDone As Double
            If SizeDone > 0 AndAlso m_TotalSize > 0 Then
                PercentDone = SizeDone / m_TotalSize * 100
            ElseIf ItemsDone > 0 AndAlso m_Sources.Count > 0 Then
                PercentDone = ItemsDone / m_Sources.Count
            Else
                PercentDone = 0
            End If
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
            m_UI.UpdateInfo(SourceDirectory, DestinationDirectory, File, PercentDone)
#End If
        End Sub

        Private Sub CleanUp()
            If m_ShowUI Then
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
                If m_UI IsNot Nothing Then m_UI.Dispose()
                m_UI = Nothing
#End If

                If m_Cancelled AndAlso m_UICancelOption = UICancelOption.ThrowException Then
                    Throw New OperationCanceledException("The operation was canceled.")
                End If
            End If

            If m_Errors.Count > 0 Then
                If CBool(File.GetAttributes(m_Source) And FileAttributes.Directory) = False Then
                    Throw New IOException(m_Errors(m_Source))
                Else
                    Dim ex As New IOException("Could not complete operation on some files and directories. See the Data property of the exception for more details.")
                    For Each entry As KeyValuePair(Of String, String) In m_Errors
                        ex.Data.Add(entry.Key, entry.Value)
                    Next
                    Throw ex
                End If
            End If
        End Sub

        Private Sub CopyDir(ByVal SourceDir As String, ByVal DestinationDir As String)
            If IO.Directory.Exists(DestinationDir) = False Then
                IO.Directory.CreateDirectory(DestinationDir)
            End If

            Dim files() As String = IO.Directory.GetFiles(SourceDir)
            Dim subdirs() As String = IO.Directory.GetDirectories(SourceDir)

            For Each file As String In files
                System.IO.File.Copy(file, Path.Combine(DestinationDir, Path.GetFileName(file)))
            Next

            For Each subdir As String In subdirs
                Dim name As String = Path.GetFileName(subdir)
                CopyDir(Path.Combine(SourceDir, name), Path.Combine(DestinationDir, name))
            Next
        End Sub

        Private Function DoOverwrite(ByVal Source As String, ByVal Destination As String) As Boolean
            If m_ShowUI Then
                Static overWriteAll As Boolean
                Static overWriteNone As Boolean

                If overWriteAll Then Return True
                If overWriteNone Then Return False

                If m_ShowUIOption = UIOption.OnlyErrorDialogs Then Return True
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
                Using frm As New FileSystemOperationUIQuestion
                    Dim result As FileSystemOperationUIQuestion.Answer
                    Dim infoA As FileInfo = New FileInfo(Source)
                    Dim infoB As FileInfo = New FileInfo(Destination)

                    frm.Text = "Confirm file overwrite"
                    frm.lblTitle.Text = String.Format("This folder already has a file called '{0}'.", Path.GetFileName(Source))
                    frm.lblText1.Text = "Do you want to replace the existing file"
                    frm.lblText2.Text = "with this other file?"
                    frm.lblSizeA.Text = String.Format("{0} bytes", infoA.Length)
                    frm.lblSizeB.Text = String.Format("{0} bytes", infoB.Length)
                    frm.lblDateA.Text = String.Format("modified: {0}", infoA.LastWriteTime)
                    frm.lblDateB.Text = String.Format("modified: {0}", infoB.LastWriteTime)
                    frm.iconA.Image = System.Drawing.Icon.ExtractAssociatedIcon(Source).ToBitmap
                    frm.iconB.Image = System.Drawing.Icon.ExtractAssociatedIcon(Destination).ToBitmap

                    result = frm.ShowDialog()

                    Select Case result
                        Case FileSystemOperationUIQuestion.Answer.Cancel
                            m_Cancelled = True
                            Return False
                        Case FileSystemOperationUIQuestion.Answer.No
                            Return False
                        Case FileSystemOperationUIQuestion.Answer.NoToAll
                            overWriteNone = True
                            Return False
                        Case FileSystemOperationUIQuestion.Answer.Yes
                            Return True
                        Case FileSystemOperationUIQuestion.Answer.YesToAll
                            overWriteAll = True
                            Return True
                        Case Else
                            Return False
                    End Select
                End Using
#End If
            Else
                If m_Overwrite = False Then
                    m_Errors.Add(Source, String.Format("The file '{0}' already exists.", Destination))
                End If
                Return m_Overwrite
            End If
        End Function

        Private ReadOnly Property OnSameVolume() As Boolean
            Get
                Return IsOnSameVolume(m_Source, m_Destination)
            End Get
        End Property

        Private Shared Function IsOnSameVolume(ByVal Source As String, ByVal Destination As String) As Boolean
            Return Char.ToUpperInvariant(Source(0)) = Char.ToUpperInvariant(Destination(0))
        End Function
    End Class

End Namespace
