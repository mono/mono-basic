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

''' <summary>
''' A list of tests.
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public Class TestList
    Inherits Generic.List(Of Test)
    Implements IDisposable

    Private m_Hashed As New Generic.Dictionary(Of String, Test)
    Private m_VBCPath As String
    Private m_VBNCPath As String

    ''' <summary>
    ''' Looks up the test on the name. 
    ''' Returns nothing if no test was found.
    ''' </summary>
    ''' <param name="TestName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Overloads ReadOnly Property Item(ByVal TestName As String) As Test
        Get
            If m_Hashed.ContainsKey(TestName) Then
                Return m_Hashed(TestName)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Shadows Function Contains(ByVal Test As Test) As Boolean
        If Contains(Test.Name) Then
            Return True
        Else
            Return MyBase.Contains(Test)
        End If
    End Function

    Shadows Function Contains(ByVal TestName As String) As Boolean
        Return m_Hashed.ContainsKey(TestName)
    End Function

    Shadows Sub Add(ByVal Test As Test)
        If Contains(Test) = False Then
            MyBase.Add(Test)
            m_Hashed.Add(Test.Name, Test)
        End If
    End Sub

    ReadOnly Property VBCPath() As String
        Get
            Return m_VBCPath
        End Get
    End Property

    ReadOnly Property VBNCPath() As String
        Get
            Return m_VBNCPath
        End Get
    End Property

    Public Sub New(ByVal CompilerPath As String, ByVal VBCPath As String)
        Me.New()
        m_VBCPath = VBCPath
        m_VBNCPath = CompilerPath
    End Sub

    Sub New()

    End Sub

    ''' <summary>
    ''' The count of tests run
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsRun() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me
                If test.Run Then result += 1
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The count of successful tests
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsSucceded() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me
                If test.Run AndAlso test.Success = True Then result += 1
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The total time all tests have been executing.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property ExecutionTime() As TimeSpan
        Get
            Dim result As TimeSpan
            For Each test As Test In Me
                If test.Run Then
                    If test.Statistics Is Nothing Then Continue For
                    result = result + test.TestDuration
                End If
            Next
            Return result
        End Get
    End Property

    ''' <summary>
    ''' The count of failed tests
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property TestsFailed() As Integer
        Get
            Dim result As Integer
            For Each test As Test In Me
                If test.Run AndAlso test.Success = False Then result += 1
            Next
            Return result
        End Get
    End Property

    Function GetTestsCount(ByVal MinResult As Test.Results, ByVal MaxResult As Test.Results) As Integer
        Dim result As Integer
        For Each test As Test In Me
            If test.Result >= MinResult AndAlso test.Result <= MaxResult Then
                result += 1
            ElseIf test.Result = rt.Test.Results.NotRun AndAlso test.OldResult >= MinResult AndAlso test.OldResult <= MaxResult Then
                result += 1
            End If
        Next
        Return result
    End Function

    Function GetTestsMin(Optional ByVal MinResult As Test.Results = Test.Results.NotRun) As TestList
        If MinResult = Test.Results.NotRun Then Return Me
        Dim result As New TestList
        For Each test As Test In Me
            If test.Result >= MinResult Then result.Add(test)
        Next
        Return result
    End Function

    Function GetTestsMax(Optional ByVal MaxResult As Test.Results = Test.Results.NotRun) As TestList
        Dim result As New TestList
        For Each test As Test In Me
            If test.Result <= MaxResult Then result.Add(test)
        Next
        Return result
    End Function

    Function GetTestsRange(ByVal MinResult As Test.Results, ByVal MaxResult As Test.Results) As TestList
        Dim result As New TestList
        For Each test As Test In Me
            If test.Result >= MinResult AndAlso test.Result <= MaxResult Then result.Add(test)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Get all tests that have failed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRedTests() As TestList
        Return GetTestsRange(Test.Results.Failed, Test.Results.Failed)
    End Function

    ''' <summary>
    ''' Get all tests that have reached at least level Results.Success
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGreenTests() As TestList
        Return GetTestsMin(Test.Results.Success)
    End Function

    Function GetNotRunTests() As TestList
        Return GetTestsMax(Test.Results.NotRun)
    End Function

    Function GetRunTests() As TestList
        Return GetTestsRange(Test.Results.Failed, Test.Results.Success)
    End Function

    Private disposed As Boolean = False

    ' IDisposable
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                Try
                    Me.disposed = True
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region
End Class
