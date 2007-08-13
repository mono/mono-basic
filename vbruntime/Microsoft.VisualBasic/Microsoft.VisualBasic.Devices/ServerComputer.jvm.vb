'
' ServerComputer.vb
'
' Authors:
'   Boris Kirzner (borisk@mainsoft.com)
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
#If NET_VER >= 2.0 Then
#If TARGET_JVM = True Then
Imports Microsoft.VisualBasic.MyServices

Namespace Microsoft.VisualBasic.Devices
    Public Class ServerComputer

        '<MonoLimitationAttribute("Microsoft.VisualBasic.Devices.Computer is not supported.") _
        Public Sub New()
           ' Throw New NotImplementedException
        End Sub

        Public ReadOnly Property Clock() As Clock
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property FileSystem() As MyServices.FileSystemProxy
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property Info() As ComputerInfo
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property Network() As Network
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property Registry() As MyServices.RegistryProxy
            Get
                Throw New NotImplementedException
            End Get
        End Property
    End Class
End Namespace
#End If
#End If