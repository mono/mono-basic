'
' Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.vb
'
' Authors:
'   Miguel de Icaza (miguel@novell.com)
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
'
' Copyright (C) 2006-2007 Novell (http://www.novell.com)
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
Imports System.Threading
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Collections.ObjectModel

Namespace Microsoft.VisualBasic.ApplicationServices

    Partial Public Class WindowsFormsApplicationBase
        Inherits ConsoleApplicationBase

        Private m_IsSingleInstance As Boolean = False
        Private m_EnableVisualStyles As Boolean = False
        Private m_SaveMySettingsOnExit As Boolean = False
        Private m_ShutdownStyle As ShutdownMode
        Private m_AuthenticationMode As AuthenticationMode
        Private m_ApplicationContext As WindowsFormsApplicationContext
        Private m_MinimumSplashScreenTime As Integer
        Private m_SplashScreen As Form

        Public Event NetworkAvailabilityChanged As Devices.NetworkAvailableEventHandler
        Public Event Shutdown As ShutdownEventHandler
        Public Event Startup As StartupEventHandler
        Public Event StartupNextInstance As StartupNextInstanceEventHandler
        Public Event UnhandledException As UnhandledExceptionEventHandler

        Public Sub New()
            Me.New(AuthenticationMode.Windows)
        End Sub

        Public Sub New(ByVal mode As AuthenticationMode)
            MyBase.New()
            m_AuthenticationMode = mode
            m_ApplicationContext = New WindowsFormsApplicationContext(Me)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Shared ReadOnly Property UseCompatibleTextRendering() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Sub Run(ByVal commandLine() As String)
#If TARGET_JVM = False Then 'Not Supported by Grasshopper
            OnRun()
            'Throw New Exception("Visual Basic 2005 applications are not currently supported (try disabling 'Enable Application Framework')")
            'Application.Run()
#Else
                        Throw New NotImplementedException
#End If
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Property IsSingleInstance() As Boolean
            Get
                Return m_IsSingleInstance
            End Get
            Set(ByVal Value As Boolean)
                m_IsSingleInstance = Value
            End Set
        End Property

        Protected Property EnableVisualStyles() As Boolean
            Get
                Return m_EnableVisualStyles
            End Get
            Set(ByVal Value As Boolean)
                m_EnableVisualStyles = Value
            End Set
        End Property

        Public Property SaveMySettingsOnExit() As Boolean
            Get
                Return m_SaveMySettingsOnExit
            End Get
            Set(ByVal Value As Boolean)
                m_SaveMySettingsOnExit = Value
            End Set
        End Property

        Protected Property ShutdownStyle() As ShutdownMode
            Get
                Return m_ShutdownStyle
            End Get
            Set(ByVal Value As ShutdownMode)
                m_ShutdownStyle = Value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property ApplicationContext() As ApplicationContext
            Get
                Return m_ApplicationContext
            End Get
        End Property

        Protected Property MainForm() As Form
            Get
                Return m_ApplicationContext.MainForm
            End Get
            Set(ByVal value As Form)
                m_ApplicationContext.MainForm = value
            End Set
        End Property

        Public Property MinimumSplashScreenDisplayTime() As Integer
            Get
                Return m_MinimumSplashScreenTime
            End Get
            Set(ByVal value As Integer)
                m_MinimumSplashScreenTime = value
            End Set
        End Property

        Public ReadOnly Property OpenForms() As FormCollection
            Get
                Return Application.OpenForms
            End Get
        End Property

        Public Property SplashScreen() As Form
            Get
                Return m_SplashScreen
            End Get
            Set(ByVal value As Form)
                m_SplashScreen = value
            End Set
        End Property

        Public Sub DoEvents()
            Application.DoEvents()
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Sub HideSplashScreen()
            Throw New NotImplementedException
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub OnCreateMainForm()
            Throw New NotImplementedException
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub OnCreateSplashScreen()
            Throw New NotImplementedException
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced), STAThread()> _
        Protected Overridable Function OnInitialize(ByVal commandLineArgs As ReadOnlyCollection(Of String)) As Boolean
            Throw New NotImplementedException
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub OnRun()

            If MainForm Is Nothing Then
                Me.OnCreateMainForm()
                If MainForm Is Nothing Then
                    Throw New NoStartupFormException()
                End If
            End If

            Application.Run(MainForm)
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub OnShutdown()
            Throw New NotImplementedException
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Function OnStartup(ByVal eventArgs As StartupEventArgs) As Boolean
            Throw New NotImplementedException
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Sub OnStartupNextInstance(ByVal eventArgs As StartupNextInstanceEventArgs)
            Throw New NotImplementedException
        End Sub

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Overridable Function OnUnhandledException(ByVal e As UnhandledExceptionEventArgs) As Boolean
            Throw New NotImplementedException
        End Function

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Sub ShowSplashScreen()
            Throw New NotImplementedException
        End Sub
    End Class
End Namespace

#End If

