'
' RegistryProxy.vb
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

#If TARGET_JVM = False Then 'Win32 Not Supported by Grasshopper
Imports System.ComponentModel
Imports Microsoft.Win32

Namespace Microsoft.VisualBasic.MyServices
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Class RegistryProxy
        Friend Sub New()
            'Empty constructor
        End Sub

        Public Function GetValue(ByVal keyName As String, ByVal valueName As String, ByVal defaultValue As Object) As Object
            Return Global.Microsoft.Win32.Registry.GetValue(keyName, valueName, defaultValue)
        End Function

        Public Sub SetValue(ByVal keyName As String, ByVal valueName As String, ByVal value As Object)
            Global.Microsoft.Win32.Registry.SetValue(keyName, valueName, value)
        End Sub

        Public Sub SetValue(ByVal keyName As String, ByVal valueName As String, ByVal value As Object, ByVal valueKind As RegistryValueKind)
            Global.Microsoft.Win32.Registry.SetValue(keyName, valueName, value, valueKind)
        End Sub

        Public ReadOnly Property ClassesRoot() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.ClassesRoot
            End Get
        End Property

        Public ReadOnly Property CurrentConfig() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.CurrentConfig
            End Get
        End Property

        Public ReadOnly Property CurrentUser() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.CurrentUser
            End Get
        End Property

        Public ReadOnly Property DynData() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.DynData
            End Get
        End Property

        Public ReadOnly Property LocalMachine() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.LocalMachine
            End Get
        End Property

        Public ReadOnly Property PerformanceData() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.PerformanceData
            End Get
        End Property

        Public ReadOnly Property Users() As RegistryKey
            Get
                Return Global.Microsoft.Win32.Registry.Users
            End Get
        End Property
    End Class
End Namespace
#End If
