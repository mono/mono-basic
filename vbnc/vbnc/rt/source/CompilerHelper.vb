' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2008 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Option Strict On
Option Explicit On

Imports System.Reflection
Imports System.Threading

Friend Class Compilation
    Private m_Test As Test
    Private m_CompilerHelper As CompilerHelper
    Private m_Thread As Thread
    Private m_Arguments As String()
    Private m_ExitCode As Integer

    Sub New(ByVal CompilerHelper As CompilerHelper, ByVal Arguments As String(), ByVal Test As Test)
        m_Arguments = Arguments
        m_CompilerHelper = CompilerHelper
        m_Test = Test
    End Sub

    Sub Start()
        'm_Thread = New Thread(AddressOf Compile)
        'm_Thread.Start()
        Compile()
    End Sub

    Private Sub Compile()
        Dim compiler As Assembly = m_CompilerHelper.GetVbncAssembly

        m_ExitCode = CInt(compiler.EntryPoint.Invoke(Nothing, New Object() {m_Arguments}))

        RaiseEvent Finished(Me)
    End Sub

    ReadOnly Property CompilerHelper() As CompilerHelper
        Get
            Return m_CompilerHelper
        End Get
    End Property

    ReadOnly Property ExitCode() As Integer
        Get
            Return m_ExitCode
        End Get
    End Property

    ReadOnly Property Test() As Test
        Get
            Return m_Test
        End Get
    End Property

    ''' <summary>
    ''' This event will be raised on another thread.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Finished(ByVal Compilation As Compilation)
End Class

Friend Class CompilerHelper
    Private m_Assembly As Assembly
    Private m_Cecil As Assembly
    Private m_ConsoleStream As IO.StringWriter

    Private m_CompilerPath As String
    Private m_CompilerOriginalPath As String

    Private WithEvents m_CompilerWatcher As New IO.FileSystemWatcher

    Private m_Dirty As Boolean = True
    Private m_DirtyStamp As DateTime = DateTime.MinValue
    Private m_SyncObject As Object = New Object

    Function GetVbncAssembly() As Assembly
        Dim result As Assembly

        If m_Dirty Then ReloadCompiler()

        SyncLock m_SyncObject
            result = m_Assembly
        End SyncLock

        Return result
    End Function

    ReadOnly Property ConsoleOutput() As String
        Get
            Return m_ConsoleStream.GetStringBuilder.ToString
        End Get
    End Property

    Private Sub m_CompilerWatcher_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles m_CompilerWatcher.Changed
        m_Dirty = True
        m_DirtyStamp = DateTime.Now
    End Sub

    Private Sub m_CompilerWatcher_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles m_CompilerWatcher.Created
        m_Dirty = True
        m_DirtyStamp = DateTime.Now
    End Sub

    ReadOnly Property CompilerPath() As String
        Get
            If m_CompilerPath Is Nothing Then
                Return m_CompilerOriginalPath
            Else
                Return m_CompilerPath
            End If
        End Get
    End Property

    Sub ReloadCompiler()
        Dim result As Assembly

        Console.WriteLine("(Re)loading compiler, changed " & (DateTime.Now - m_DirtyStamp).TotalSeconds & " seconds ago")

        Dim dir As String = IO.Path.GetDirectoryName(m_CompilerOriginalPath)
        Dim destparent As String = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        Dim number As Integer = 0
        Dim dest As String

        Do
            number += 1
            dest = IO.Path.Combine(destparent, "vbnc-" & number.ToString())
        Loop While IO.Directory.Exists(dest)

        IO.Directory.CreateDirectory(dest)
        For Each file As String In IO.Directory.GetFiles(dir)
            IO.File.Copy(file, IO.Path.Combine(dest, IO.Path.GetFileName(file)))
        Next

        m_CompilerPath = IO.Path.Combine(dest, IO.Path.GetFileName(m_CompilerOriginalPath))

        m_ConsoleStream = New IO.StringWriter()
        Console.SetOut(m_ConsoleStream)

        Try
            result = Assembly.LoadFrom(CompilerPath)

            SyncLock m_SyncObject
                m_Assembly = result
            End SyncLock
        Catch ex As Exception
            Console.WriteLine("Exception while trying to load assembly: " & m_CompilerPath)
            Console.WriteLine(ex.ToString)
            Throw ex
        End Try

        m_Dirty = False
    End Sub

    Sub New(ByVal CompilerPath As String)
        Try
            m_CompilerOriginalPath = CompilerPath

            m_CompilerWatcher.NotifyFilter = IO.NotifyFilters.CreationTime Or IO.NotifyFilters.FileName Or IO.NotifyFilters.LastWrite
            m_CompilerWatcher.SynchronizingObject = MainModule.frmMain
            m_CompilerWatcher.Path = IO.Path.GetDirectoryName(m_CompilerOriginalPath)
            m_CompilerWatcher.Filter = IO.Path.GetFileName(m_CompilerOriginalPath)
            m_CompilerWatcher.EnableRaisingEvents = True

            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf AssemblyResolve
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Function AssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
        If args.Name = m_Cecil.FullName Then
            Return m_Cecil
        Else
            Debug.WriteLine("Trying to find " & args.Name)
            Try
                Return Assembly.Load(args.Name)
            Catch ex As Exception
                Throw
            End Try
            Throw New NotSupportedException(args.Name)
        End If
    End Function

    Function Compile(ByVal Args() As String, ByVal Test As Test) As Compilation
        Dim compilation As Compilation

        compilation = New Compilation(Me, Args, Test)
        compilation.Start()

        Return compilation
    End Function
End Class
