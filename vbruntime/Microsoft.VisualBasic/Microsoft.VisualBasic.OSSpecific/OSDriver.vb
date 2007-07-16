'
' ContextValue.vb
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

Imports System

Namespace Microsoft.VisualBasic.OSSpecific
#If TARGET_JVM = False Then
    Friend MustInherit Class OSDriver
#Else
    Friend Class OSDriver
#End If
    
        Private Shared m_Driver As OSDriver

        Shared ReadOnly Property Driver() As OSDriver
            Get
#If TARGET_JVM = False Then
		If m_Driver Is Nothing Then
                    Select Case CInt(System.Environment.OSVersion.Platform)
                        Case PlatformID.Win32NT, PlatformID.Win32S, PlatformID.Win32Windows, PlatformID.WinCE
                            m_Driver = New Win32Driver()
                        Case 128, 4 'PlatformID.Unix = 4
                            m_Driver = New LinuxDriver()
                        Case Else
                            Throw New ApplicationException(String.Format("The OS '{0}' is not supported.", System.Environment.OSVersion.ToString()))
                    End Select
                End If
#Else
		m_Driver = New OSDriver()
#End If		
                Return m_Driver
            End Get
        End Property

        Overridable Sub SetDate(ByVal newDate As Date)
            ThrowNotImplemented("SetDate")
        End Sub

        Overridable Sub SetTime(ByVal newTime As Date)
            ThrowNotImplemented("SetTime")
        End Sub

        Sub ThrowNotImplemented(ByVal Method As String)
            Throw New NotImplementedException(String.Format("'{0}' is not implemented for your operating system.", Method))
        End Sub

        Sub New()

        End Sub
    End Class
End Namespace
