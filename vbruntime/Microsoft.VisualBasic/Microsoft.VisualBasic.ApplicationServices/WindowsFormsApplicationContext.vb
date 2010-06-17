'
' Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationContext.vb
'
' Authors:
'   Rolf Bjarne Kvinge  (RKvinge@novell.com)
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

#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Collections.ObjectModel

Namespace Microsoft.VisualBasic.ApplicationServices
    Public Class WindowsFormsApplicationBase
        Private Class WindowsFormsApplicationContext
            Inherits ApplicationContext

            Private m_Application As WindowsFormsApplicationBase

            Sub New(ByVal Application As WindowsFormsApplicationBase)
                MyBase.New()
                m_Application = Application
            End Sub

            Protected Overrides Sub OnMainFormClosed(ByVal sender As Object, ByVal e As System.EventArgs)
                Select Case m_Application.ShutdownStyle
                    Case ShutdownMode.AfterMainFormCloses
                        MyBase.OnMainFormClosed(sender, e)
                    Case Else 'ShutdownMode.AfterAllFormsClose
                        If Application.OpenForms.Count = 0 Then
                            MyBase.OnMainFormClosed(sender, e)
                        Else
                            MainForm = Application.OpenForms(0)
                        End If
                End Select
            End Sub
        End Class
    End Class
End Namespace
#End If
