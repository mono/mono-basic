'
' ProjectData.vb
'
' Authors:
'   Mizrahi Rafael (rafim@mainsoft.com)
'

'
' Copyright (C) 2002-2006 Mainsoft Corporation.
' Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
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
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace Microsoft.VisualBasic.CompilerServices
    ' ProjectData class is used in VB exception handling.
    ' it holds an ErrObject which stores the current error status of the whole running program.
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public NotInheritable Class ProjectData

        ' ProjectData singelton
        Private Shared Inst As ProjectData = Nothing
#If TARGET_JVM Then
        private Shared m_projectErrorSlot as LocalDataStoreSlot = System.Threading.Thread.AllocateDataSlot()
#Else
        '
        ' ThreadStatic Indicates that the value of a static field is unique for each thread.
        ' Although there is one ProjectData, every thread which set get VB errors must have its own ErrObject
        <ThreadStatic()> _
        Friend Shared m_projectError As ErrObject
#End If

        Friend ReadOnly Property ProjectError() As ErrObject
            Get
#If TARGET_JVM Then
                                dim errObj as Object = System.Threading.Thread.GetData(m_projectErrorSlot)
                                if (errObj is Nothing)
                                    errObj = New ErrObject
                                    System.Threading.Thread.SetData(m_projectErrorSlot, errObj)
                                End if
                                return CType( errObj, ErrObject)
#Else
                Return m_projectError
#End If
            End Get
        End Property
        Private Sub New()
            'Nobody should see constructor
            '#If TRACE Then
            '            Console.WriteLine("TRACE:ProjectData.New:called")
            '#End If
        End Sub
        'singelton, override New in order to use only one instance of ProjectData
        Friend Shared Function Instance() As ProjectData
            If Inst Is Nothing Then
                Inst = New ProjectData
            End If
#If Not TARGET_JVM Then
            If m_projectError Is Nothing Then
                m_projectError = New ErrObject
            End If
#End If
            Return Inst
        End Function
        'ClearProjectError is called by the statement "On Error Resume Next"
        <System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.Success)> _
        Public Shared Sub ClearProjectError()
            'FIXME: "On Error Resume Next" cause to stop throwing exceptions. 
            'might be some friend variable of ErrObject which store that flag .
            Dim pd As ProjectData = Instance()
            pd.ProjectError.Clear()
        End Sub
        Public Shared Function CreateProjectError(ByVal hr As Integer) As Exception
            'FIXME: hr might be a Windows HResult number, and not a VB error number.
            'FIXME: Add a test and verify that. 
#If TRACE Then
            Console.WriteLine("TRACE:ProjectData.CreateProjectError:hr:" + hr.ToString())
#End If
            Dim pd As ProjectData = Instance()

            Dim description As String
            description = Conversion.ErrorToString(hr)

            pd.ProjectError.SetExceptionFromNumber(hr, description)

            Return pd.ProjectError.GetException

        End Function

        <System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.Success)> _
        Public Overloads Shared Sub SetProjectError(ByVal ex As Exception)
            Dim pd As ProjectData = Instance()
            pd.ProjectError.SetException(ex)
        End Sub

        <System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, System.Runtime.ConstrainedExecution.Cer.Success)> _
        Public Overloads Shared Sub SetProjectError(ByVal ex As Exception, ByVal lErl As Integer)
            Throw New NotImplementedException("implement me: Erl")
            'FIXME: projectError.SetException(ex)
            'FIXME: projectError.Erl = lErl
            'FIXME: projectError.Erl  is readonly. suggested solution, add an overload of SetException(Exception,Integer)
        End Sub

#If Moonlight = False Then
        Public Shared Sub EndApp()
            Environment.Exit(Environment.ExitCode)
        End Sub
#End If
    End Class
End Namespace
