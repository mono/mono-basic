'
' Computer.vb
'
' Authors:
'   Miguel de Icaza (miguel@novell.com)
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Rolf Bjarne Kvinge (RKvinge@novell.com)
'
'
' Copyright (C) 2002-2006 Mainsoft Corporation.
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
 
#If TARGET_JVM = False Then 'System.IO.Ports, Windows.Forms Not Supported by Grasshopper
Imports System
Imports Microsoft.VisualBasic.MyServices
Imports System.Windows.Forms

Namespace Microsoft.VisualBasic.Devices
    Public Class Computer
        Inherits ServerComputer

        Public Sub New()
            'Empty
        End Sub

        Public ReadOnly Property Audio() As Audio
            Get
                Return New Audio
            End Get
        End Property

        Public ReadOnly Property Clipboard() As MyServices.ClipboardProxy
            Get
                Return New MyServices.ClipboardProxy
            End Get
        End Property

        Public ReadOnly Property Keyboard() As Keyboard
            Get
                Return New Keyboard
            End Get
        End Property

        Public ReadOnly Property Mouse() As Mouse
            Get
                Return New Mouse
            End Get
        End Property

        Public ReadOnly Property Ports() As Ports
            Get
                Return New Ports
            End Get
        End Property

        Public ReadOnly Property Screen() As Screen
            Get
                Return System.Windows.Forms.Screen.PrimaryScreen
            End Get
        End Property
    End Class
End Namespace
#End If
