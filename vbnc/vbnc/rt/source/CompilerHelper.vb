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

Option Strict On
Option Explicit On

Imports System.Reflection

<Serializable()> _
Friend Class CompilerHelper
    Inherits MarshalByRefObject
    Implements IDisposable

    Private m_Compiler As Object
    Private m_ConsoleOutput As String
    Private m_ConsoleStream As IO.StringWriter

    Private m_Parent As TestList

    Private m_CompilerPath As String
    Private m_CompilerOriginalPath As String
    Private m_CompilerMethod As MethodInfo

    ''' <summary>
    ''' If the current helper is loaded into the default domain, 
    ''' then this field is loaded into the other domain and everything
    ''' is forwarded into this helper.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CompilerHelper As CompilerHelper
    Private m_CompilerDomain As AppDomain

    Private WithEvents m_CompilerWatcher As New IO.FileSystemWatcher

    Private m_Dirty As Boolean = True


    Public Overrides Function InitializeLifetimeService() As Object
        Dim lease As Runtime.Remoting.Lifetime.ILease
        lease = CType(MyBase.InitializeLifetimeService, Runtime.Remoting.Lifetime.ILease)
        lease.InitialLeaseTime = TimeSpan.Zero
        Return lease
    End Function


    Property ConsoleOutput() As String
        Get
            If m_CompilerHelper IsNot Nothing Then
                Try
                    Return m_CompilerHelper.ConsoleOutput
                Catch ex As Runtime.Remoting.RemotingException
                    Return ex.Message
                End Try
            Else
                Return m_ConsoleOutput
            End If
        End Get
        Private Set(ByVal value As String)
            m_ConsoleOutput = value
        End Set
    End Property
    Private Sub m_CompilerWatcher_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles m_CompilerWatcher.Changed
        m_Dirty = True
    End Sub

    Private Sub m_CompilerWatcher_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles m_CompilerWatcher.Created
        m_Dirty = True
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
        m_ConsoleStream = New IO.StringWriter
        Console.SetOut(m_ConsoleStream)
        m_Compiler = Activator.CreateInstance("vbnc", "vbnc.Compiler").Unwrap ' m_CompilerType)
        m_CompilerMethod = m_Compiler.GetType.GetMethod("Compile", New Type() {GetType(String())})
        Dim responsefilename As String = IO.Path.GetFullPath("..\..\vbnc\bin\vbnc.rsp")
        Dim responsefilename2 As String = IO.Path.Combine(IO.Path.GetDirectoryName(m_Compiler.GetType.Assembly.Location), "vbnc.rsp")
        If IO.File.Exists(responsefilename2) = False Then
            IO.File.Copy(responsefilename, responsefilename2)
        End If
    End Sub

    Private Sub SetupDomain()
        If m_CompilerDomain IsNot Nothing Then
            AppDomain.Unload(m_CompilerDomain)
            m_CompilerDomain = Nothing
        End If

        Dim ns As New AppDomainSetup
        ns.ShadowCopyFiles = "true"
        ns.ShadowCopyDirectories = IO.Path.GetFullPath("..\..\vbnc\bin")
        ns.CachePath = IO.Path.GetFullPath("tmpcompiler")
        ns.PrivateBinPath = "rt\bin;vbnc\bin"
        ns.ApplicationBase = IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly.Location)))
        Static counter As Integer
        counter += 1
        m_CompilerDomain = AppDomain.CreateDomain("Compiler #" & counter.ToString, Nothing, ns)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        Debug.WriteLine("Created domain: " & m_CompilerDomain.FriendlyName)
        m_Dirty = False
    End Sub

    Private Sub CreateCompilerHelper()
        If m_CompilerHelper IsNot Nothing Then
            m_CompilerHelper.Dispose()
            m_CompilerHelper = Nothing
        End If
        SetupDomain()
        'm_CompilerHelper = CType(Activator.CreateInstance(m_CompilerDomain, "rt", GetType(CompilerHelper).FullName, True, BindingFlags.Default, Nothing, New Object() {m_Parent, m_CompilerOriginalPath}, Nothing, Nothing, Nothing).Unwrap, CompilerHelper)
    End Sub

    Sub New(ByVal Tests As TestList, ByVal CompilerPath As String)
        Try
            m_Parent = Tests
            m_CompilerOriginalPath = CompilerPath

            If AppDomain.CurrentDomain.IsDefaultAppDomain Then
                m_CompilerWatcher.NotifyFilter = IO.NotifyFilters.CreationTime Or IO.NotifyFilters.FileName Or IO.NotifyFilters.LastWrite
                m_CompilerWatcher.SynchronizingObject = MainModule.frmMain
                m_CompilerWatcher.Path = IO.Path.GetDirectoryName(m_CompilerOriginalPath)
                m_CompilerWatcher.Filter = IO.Path.GetFileName(m_CompilerOriginalPath)
                m_CompilerWatcher.EnableRaisingEvents = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Function Compile(ByVal Args() As String) As Boolean
        Dim result As Boolean

        Try
            If AppDomain.CurrentDomain.IsDefaultAppDomain Then
                If m_Dirty Then CreateCompilerHelper()
                Debug.Assert(m_CompilerHelper IsNot Nothing)
                Return m_CompilerHelper.Compile(Args)
            Else
                ReloadCompiler()
                result = CBool(m_CompilerMethod.Invoke(m_Compiler, New Object() {Args}))
                ConsoleOutput = m_ConsoleStream.ToString

                Debug.WriteLine("Done test in Domain: " & AppDomain.CurrentDomain.FriendlyName & " with executable: " & m_Compiler.GetType.Assembly.Location)
            End If
        Catch ex As Runtime.Remoting.RemotingException
            result = False
            m_ConsoleStream = New IO.StringWriter()
            m_ConsoleStream.WriteLine("Exception while executing test: " & ex.Message)
        Catch ex As Exception
            result = False
            m_ConsoleStream.WriteLine("Exception while executing test: " & ex.Message)
        End Try

        Return result
    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If
            If m_CompilerHelper IsNot Nothing Then
                m_CompilerHelper.Dispose()
                m_CompilerHelper = Nothing
            End If
            If m_CompilerDomain IsNot Nothing Then
                AppDomain.Unload(m_CompilerDomain)
                m_CompilerDomain = Nothing
            End If
            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
