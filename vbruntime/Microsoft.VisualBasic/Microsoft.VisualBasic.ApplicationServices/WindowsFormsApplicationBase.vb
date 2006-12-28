'
' Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.cs
'
' Authors:
'   Miguel de Icaza (miguel@novell.com)
'   Mizrahi Rafael (rafim@mainsoft.com)
'
' Copyright (C) 2006 Novell (http://www.novell.com)
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
 
#if NET_2_0
Imports System
Imports System.Threading
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System.Windows.Forms
#End If

Namespace Microsoft.VisualBasic.ApplicationServices

    Public Class WindowsFormsApplicationBase
        Inherits ConsoleApplicationBase
        Public Sub New()
        End Sub

        Public Sub New(ByVal mode As AuthenticationMode)
        End Sub

        Protected Shared ReadOnly Property UseCompatibleTextRendering() As Boolean
            Get
                Return False
            End Get
        End Property

        '<MonoTODO("We ignore the commandLine argument")> _ 
        Public Sub Run(ByVal commandLine() As String)
#If TARGET_JVM = False Then 'Not Supported by Grasshopper
            Throw New Exception("Visual Basic 2005 applications are not currently supported (try disabling 'Enable Application Framework')")
            Application.Run()
#Else
            Throw New NotImplementedException
#End If
        End Sub

        Dim is_single_instance As Boolean = False
        Protected Property IsSingleInstance() As Boolean
            Get
                Return is_single_instance
            End Get
            Set(ByVal Value As Boolean)
                is_single_instance = Value
            End Set
        End Property

        Dim enable_visual_styles As Boolean = False
        Protected Property EnableVisualStyles() As Boolean
            Get
                Return enable_visual_styles
            End Get
            Set(ByVal Value As Boolean)
                enable_visual_styles = Value
            End Set
        End Property

        Dim save_my_settings_on_exit As Boolean = False
        Protected Property SaveMySettingsOnExit() As Boolean
            Get
                Return save_my_settings_on_exit
            End Get
            Set(ByVal Value As Boolean)
                save_my_settings_on_exit = Value
            End Set
        End Property

        Dim shutdown_style As ShutdownMode
        Protected Property ShutdownStyle() As ShutdownMode
            Get
                Return shutdown_style
            End Get
            Set(ByVal Value As ShutdownMode)
                shutdown_style = Value
            End Set
        End Property
    End Class
End Namespace

#End If

