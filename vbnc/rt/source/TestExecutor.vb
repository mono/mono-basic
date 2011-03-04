' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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

Friend Class TestExecutor
    Implements IDisposable

    Private m_Threads() As Threading.Thread
    Private m_Queue As New Generic.LinkedList(Of Test)
    Private m_PausedQueue As New Generic.List(Of Test)
    Private m_RunningTest As Test

    ''' <summary>
    ''' Raised when a test is about to be run.
    ''' </summary>
    ''' <param name="Test"></param>
    ''' <remarks></remarks>
    Public Shared Event BeforeExecute As BeforeExecuteDelegate
    Public Delegate Sub BeforeExecuteDelegate(ByVal Test As Test)
    
    ''' <summary>
    ''' Raised when a test has been run.
    ''' </summary>
    ''' <param name="Test"></param>
    ''' <remarks></remarks>
    Public Shared Event AfterExecute As AfterExecuteDelegate
    Public Delegate Sub AfterExecuteDelegate(ByVal Test As Test)
    
    ''' <summary>
    ''' Raised when all pending tests have been run.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Event Finished As FinishedDelegate
    Public Delegate Sub FinishedDelegate()

    ''' <summary>
    ''' Specifies whether tests should be run in-process or in an external process.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_RunTestsHosted As Boolean

    Sub New()
        ReDim m_Threads(3) '4 threads for now
    End Sub

    ReadOnly Property Queue() As Generic.IEnumerable(Of Test)
        Get
            Return m_Queue
        End Get
    End Property

    ''' <summary>
    ''' Returns the number of tests left in the queue.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property QueueCount() As Integer
        Get
            Return m_Queue.Count
        End Get
    End Property

    ''' <summary>
    ''' Specifies whether tests should be run in-process or in an external process.
    ''' </summary>
    ''' <remarks></remarks>
    Property RunTestsHosted() As Boolean
        Get
            Return m_RunTestsHosted
        End Get
        Set(ByVal value As Boolean)
            m_RunTestsHosted = value
            If m_RunTestsHosted Then
                Throw New NotImplementedException("Hosted testing is not implemented yet.")
            End If
        End Set
    End Property

    ''' <summary>
    ''' This sub should be run async.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Runner()
        While disposedValue = False
            Try
                Dim test As Test
                SyncLock m_Queue
                    If m_Queue.Count > 0 Then
                        Dim theFirst As Generic.LinkedListNode(Of Test)
                        theFirst = m_Queue.First
                        test = theFirst.Value
                        m_Queue.RemoveFirst()
                    Else
                        test = Nothing
                    End If
                End SyncLock

                If test IsNot Nothing Then
                    m_RunningTest = test
                    RaiseEvent BeforeExecute(test)
                    Run(test)
                    RaiseEvent AfterExecute(test)
                    m_RunningTest = Nothing

                    If m_Queue.Count = 0 Then
                        RaiseEvent Finished()
                    End If
                Else
                    Threading.Thread.Sleep(100)
                End If
            Catch ex As Exception
                Debug.WriteLine("Exception while executing test: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End While
    End Sub

    Private Sub StartThread()
        SyncLock m_Queue
            For i As Integer = 0 To m_Threads.Length - 1
                If m_Threads(i) IsNot Nothing Then Continue For
                m_Threads(i) = New Threading.Thread(New Threading.ThreadStart(AddressOf Runner))
                m_Threads(i).IsBackground = True
                m_Threads(i).Start()
            Next
        End SyncLock
    End Sub

    ''' <summary>
    ''' Run the specified test, optionally putting it first in the queue.
    ''' If the test is already in the queue and it is given a priority, it
    ''' is moved to the start of the queue.
    ''' </summary>
    ''' <param name="Test"></param>
    ''' <remarks></remarks>
    Sub RunAsync(ByVal Test As Test, Optional ByVal Priority As Boolean = False)
        SyncLock m_Queue
            If m_Queue.Contains(Test) Then
                If Priority Then
                    m_Queue.Remove(Test)
                    m_Queue.AddFirst(Test)
                End If
            Else
                If Priority Then
                    m_Queue.AddFirst(Test)
                Else
                    m_Queue.AddLast(Test)
                End If
            End If
        End SyncLock
        StartThread()
    End Sub

    ''' <summary>
    ''' Runs the specified test.
    ''' </summary>
    ''' <param name="Test"></param>
    ''' <remarks></remarks>
    Sub Run(ByVal Test As Test)
        Test.DoTest()
    End Sub

    ''' <summary>
    ''' Run all tests async
    ''' </summary>
    ''' <remarks></remarks>
    Sub RunAsync(ByVal Tests As Generic.IEnumerable(Of Test), Optional ByVal Priority As Boolean = False)
        SyncLock m_Queue
            For Each Test As Test In Tests
                If m_Queue.Contains(Test) Then
                    If Priority Then
                        m_Queue.Remove(Test)
                        m_Queue.AddFirst(Test)
                    End If
                Else
                    If Priority Then
                        m_Queue.AddFirst(Test)
                    Else
                        m_Queue.AddLast(Test)
                    End If
                End If
            Next
        End SyncLock
        StartThread()
    End Sub

    ''' <summary>
    ''' Run all tests async as well as all contained tests.
    ''' </summary>
    ''' <remarks></remarks>
    Sub RunAsyncTree(ByVal Tests As Tests)
        RunAsync(Tests.Values)
    End Sub
    ''' <summary>
    ''' Returns true if a test is running.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsExecuting() As Boolean
        Get
            Return m_RunningTest IsNot Nothing
        End Get
    End Property

    ''' <summary>
    ''' Resumes all the paused tests.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub [Resume]()
        SyncLock m_Queue
            For Each test As Test In m_PausedQueue
                m_Queue.AddLast(test)
            Next
            m_PausedQueue.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Pauses all the tests (but not the running test if a test is beeing run).
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Pause()
        SyncLock m_Queue
            m_PausedQueue.AddRange(m_Queue)
            m_Queue.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Clears the queue.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        SyncLock m_Queue
            m_Queue.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Clears the queue and blocks until no more tests are running.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub [Stop]()
        Clear()
        For i As Integer = 0 To m_Threads.Length - 1
            Dim m_Thread As Threading.Thread = m_Threads(i)

            If m_Thread Is Nothing Then Continue For
            If Threading.Thread.CurrentThread.Equals(m_Thread) Then Continue For

            Do Until m_Thread.ThreadState <> Threading.ThreadState.Running
                Threading.Thread.Sleep(100)
            Loop
        Next
    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If
            For i As Integer = 0 To m_Threads.Length - 1
                Dim m_Thread As Threading.Thread = m_Threads(i)
                If m_Thread Is Nothing Then Continue For
                Me.disposedValue = True
                If m_Thread.Join(60000) = False Then
                    m_Thread.Abort()
                End If
                m_Threads(i) = Nothing
            Next

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

