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

Imports System.IO

Class frmMain
    Inherits Windows.Forms.Form

    Private WithEvents m_Tests As New Tests
    Private WithEvents m_TestExecutor As New TestExecutor
    Private m_TestView As New TestView(Me)
    Private WithEvents m_SaveTimer As Timer

    Private m_Indices() As Integer
    Private m_Icons() As Icon
    Private m_Colors() As Brush
    
    Private Delegate Sub UpdateUIDelegate(ByVal test As Test, ByVal UpdateSummary As Boolean)
    Private Delegate Sub UpdateUIDelegate2()

    ReadOnly Property TestExecutor() As TestExecutor
        Get
            Return m_TestExecutor
        End Get
    End Property

    ReadOnly Property Tests() As Tests
        Get
            Return m_Tests
        End Get
    End Property

    Function GetIcon(ByVal Result As Test.Results) As Icon
        Return m_Icons(Result)
    End Function

    Function GetIconIndex(ByVal Result As Test.Results) As Integer
        Return m_Indices(Result)
    End Function

    Private Sub CreateImages()
        Dim images() As Bitmap
        Dim bounds As New Rectangle(0, 0, 16, 16)

        ReDim m_Colors(System.Enum.GetValues(GetType(Test.Results)).Length - 1)

        For i As Integer = 0 To m_Colors.Length - 1
            m_Colors(i) = Brushes.Chocolate
        Next
        m_Colors(Test.Results.Failed) = Brushes.Red
        m_Colors(Test.Results.Running) = Brushes.Blue
        m_Colors(Test.Results.Success) = Brushes.Green
        m_Colors(Test.Results.KnownFailureSucceeded) = Brushes.GreenYellow
        m_Colors(Test.Results.NotRun) = Brushes.Yellow
        m_Colors(Test.Results.Regressed) = Brushes.Indigo
        m_Colors(Test.Results.Skipped) = Brushes.Orange
        m_Colors(Test.Results.KnownFailureFailed) = Brushes.Purple

        ReDim images(m_Colors.Length - 1)
        ReDim m_Indices(m_Colors.Length - 1)
        ReDim m_Icons(m_Colors.Length - 1)

        Me.barProgress.ValueCount = m_Colors.Length

        For i As Integer = 0 To m_Colors.Length - 1
            images(i) = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
            Using gr As Graphics = Graphics.FromImage(images(i))
                gr.FillEllipse(m_Colors(i), bounds)
            End Using
            m_Icons(i) = System.Drawing.Icon.FromHandle(images(i).GetHicon)
            lstImages.Images.Add(images(i))
            m_Indices(i) = lstImages.Images.Count - 1

            Me.barProgress.Value(i).ColorBrush = m_Colors(i)
            Me.barProgress.Value(i).Text = CType(i, Test.Results).ToString() + ": {0} {1:0.00%}"
        Next
    End Sub

    Sub New()
        MyBase.new()

        InitializeComponent()
        CreateImages()

        lstTests.ListViewItemSorter = New ListViewItemComparer(lstTests)

        Dim tmp As String
        tmp = IO.Path.GetFullPath("..\..\vbnc\bin\vbnc.exe")
        If IO.File.Exists(tmp) Then cmbCompiler.Items.Add(tmp)

        tmp = IO.Path.GetFullPath("..\..\vbnc\tests\tests.xml")
        If IO.File.Exists(tmp) Then cmbBasepath.Items.Add(tmp)

        tmp = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "Microsoft.Net\Framework\v2.0.50727\vbc.exe")
        If IO.File.Exists(tmp) Then cmbVBCCompiler.Items.Add(tmp)
        tmp = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "Microsoft.Net\Framework\v3.5\vbc.exe")
        If IO.File.Exists(tmp) Then cmbVBCCompiler.Items.Add(tmp)
        tmp = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "Microsoft.Net\Framework\v4.0.30319\vbc.exe")
        If IO.File.Exists(tmp) Then cmbVBCCompiler.Items.Add(tmp)

        colDate.Width = My.Settings.TestsListView_colDate_Width
        colFailedVerification.Width = My.Settings.TestsListView_colFailedVerification_Width
        colName.Width = My.Settings.TestsListView_colName_Width
        colResult.Width = My.Settings.TestsListView_colResult_Width
        colKnownFailureReason.Width = My.Settings.TestsListView_colKnownFailureReason_Width

        If My.Settings.txtVBCCompiler_Text <> "" Then
            cmbCompiler.Text = My.Settings.txtVBCCompiler_Text
        ElseIf cmbCompiler.Text = "" AndAlso cmbCompiler.Items.Count = 1 Then
            cmbCompiler.SelectedIndex = 0
        End If
        If My.Settings.txtVBNCCompiler_Text <> "" Then
            cmbVBCCompiler.Text = My.Settings.txtVBNCCompiler_Text
        ElseIf cmbVBCCompiler.Text = "" AndAlso cmbVBCCompiler.Items.Count = 1 Then
            cmbVBCCompiler.SelectedIndex = 0
        End If
        If My.Settings.txtBasePath_Text <> "" Then
            cmbBasepath.Text = My.Settings.txtBasePath_Text
        ElseIf cmbBasepath.Text = "" AndAlso cmbBasepath.Items.Count = 1 Then
            cmbBasepath.SelectedIndex = 0
        End If

        LoadTests()
    End Sub

    Public Sub RunTests()
        cmdRun_Click(Nothing, Nothing)
    End Sub

    Private Sub LoadTests()
        Try
            StopWork()

            If Not File.Exists(cmbBasepath.Text) Then
                MsgBox("Invalid path: " & cmbBasepath.Text, MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            m_Tests.Load(cmbBasepath.Text)
            PopulateTestList()
            UpdateSummary()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function LoadTests(ByVal tests As Tests, ByVal nodes As TreeNodeCollection) As TreeNode
        Dim baseNode As TreeNode
        baseNode = nodes.Add(IO.Path.GetFileName(tests.Filename))
        baseNode.Tag = tests
        Return baseNode
    End Function

    Private Sub lstTests_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstTests.SelectedIndexChanged
        Try
            If lstTests.SelectedItems.Count = 1 Then
                SelectTest(Me.GetSelectedTests(0))
            Else
                SelectTest(Nothing)
                gridTestProperties.SelectedObjects = Me.GetSelectedTests().ToArray()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub lstTests_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstTests.DoubleClick
        Try
            AddWork(GetSelectedTests, True)
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub UpdateUI()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New UpdateUIDelegate2(AddressOf UpdateUI))
        Else
            For Each t As Test In m_Tests.Values
                UpdateUI(t, False)
            Next
            UpdateSummary()
        End If
    End Sub

    ''' <summary>
    ''' Thread-safe.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateSummary()
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New CrossAppDomainDelegate(AddressOf UpdateSummary))
                Return
            End If

            Dim counts(CInt(Test.Results.KnownFailureFailed)) As Integer
            Dim total As Integer

            total = m_Tests.Count
            m_Tests.GetTestsCount(counts)

            If counts(Test.Results.Failed) > 0 Then
                Me.Icon = GetIcon(Test.Results.Failed)
            ElseIf counts(Test.Results.NotRun) > 0 OrElse counts(Test.Results.Running) > 0 Then
                Me.Icon = GetIcon(Test.Results.Running)
            ElseIf counts(Test.Results.Regressed) > 0 Then
                Me.Icon = GetIcon(Test.Results.Regressed)
            ElseIf counts(Test.Results.KnownFailureFailed) > 0 Then
                Me.Icon = GetIcon(Test.Results.KnownFailureFailed)
            ElseIf counts(Test.Results.KnownFailureSucceeded) > 0 Then
                Me.Icon = GetIcon(Test.Results.KnownFailureSucceeded)
            Else
                Me.Icon = GetIcon(Test.Results.Success)
            End If

            For i As Integer = 0 To m_Colors.Length - 1
                Me.barProgress.Value(i).Number = m_Tests.GetTestsCount(CType(i, Test.Results), CType(i, Test.Results))
                Me.barProgress.Value(i).PercentDone = Me.barProgress.Value(i).Number / total
            Next
            Me.barProgress.Invalidate()

            Dim r As Integer = counts(Test.Results.Failed) + counts(Test.Results.Regressed)
            Dim g As Integer = counts(Test.Results.Success) + counts(Test.Results.KnownFailureSucceeded)
            Dim y As Integer = counts(Test.Results.KnownFailureFailed)
            Dim runcount As Integer = r + g + y

            Dim COUNTERFORMAT As String = "{0} ({1:0.#%})"
            tblRedTests.Text = "Red tests: " & String.Format(COUNTERFORMAT, r, r / total)
            tblTestsNotRun.Text = "Tests not run: " & String.Format(COUNTERFORMAT, y, y / total)
            tblGreenTests.Text = "Green tests: " & String.Format(COUNTERFORMAT, g, g / total)

            tblTestsInQueue.Text = "Tests in queue: " & m_TestExecutor.QueueCount.ToString
            tblNumberOfTests.Text = "Number of tests: " & total.ToString
            tblTestsRun.Text = "Tests run: " & (r + g).ToString

            Text = String.Format("RT OK: {0} ({5:#0.0}%) / FAILED: {1} ({4:#0.0}%) / NOT RUN: {2}/{3} tests) / IN QUEUE: {6}", g, r, y, total, r * 100 / total, g * 100 / total, m_TestExecutor.QueueCount)
            Dim exectime As TimeSpan = m_Tests.ExecutionTime
            tblExecutionTime.Text = "Execution time: " & String.Format("{0}", FormatTimespan(exectime))
            If total > 0 Then
                tblAverageExecutionTime.Text = "Avg execution time: " & String.Format("{0}", FormatTimespan(New TimeSpan(exectime.Ticks \ CInt(IIf(runcount = 0, 1, runcount)))))
            Else
                tblAverageExecutionTime.Text = "Avg execution time: 0"
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Thread-safe.
    ''' </summary>
    ''' <param name="test"></param>
    ''' <remarks></remarks>
    Private Sub UpdateUI(ByVal test As Test, Optional ByVal UpdateSummary As Boolean = True)
        If test Is Nothing Then UpdateUI()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New UpdateUIDelegate(AddressOf UpdateUI), New Object() {test, UpdateSummary})
        Else
            If Me.Disposing OrElse Me.IsDisposed Then StopIfDebugging() : Return

            Dim item As ListViewItem = TryCast(test.Tag, ListViewItem)
            If item Is Nothing Then
                For Each item In lstTests.Items
                    If item.Tag Is test Then
                        Exit For
                    Else
                        item = Nothing
                    End If
                Next
            End If

            Dim newStateImageIndex As Integer
            newStateImageIndex = GetIconIndex(test.Result)
            If item.StateImageIndex <> newStateImageIndex Then
                '(not implemnted in winforms yet)'item.StateImageIndex = newStateImageIndex
            End If

            If UpdateSummary Then Me.UpdateSummary()

            If lstTests.SelectedItems.Count > 0 AndAlso lstTests.SelectedItems.Contains(item) Then
                lstTests_SelectedIndexChanged(lstTests, Nothing)
            End If
            tblTestsInQueue.Text = "Tests in queue: " & m_TestExecutor.QueueCount.ToString
            If lstTests.ListViewItemSorter IsNot Nothing Then lstTests.Sort()
        End If
    End Sub

    Private Function FormatTimespan(ByVal ts As TimeSpan) As String
        Return ts.Days.ToString("00") & ":" & ts.Hours.ToString("00") & ":" & ts.Minutes.ToString("00") & ":" & ts.Seconds.ToString("00") & ":" & ts.Milliseconds.ToString("000")
        'Return ts.TotalMilliseconds.ToString("#,##") & " milliseconds"
        'Return CInt(ts.TotalSeconds).ToString & ":" & ts.Milliseconds.ToString & " seconds"
    End Function

    ''' <summary>
    ''' Thread-safe.
    ''' </summary>
    ''' <param name="test"></param>
    ''' <remarks></remarks>
    Private Sub UpdateUITestRunning(ByVal test As Test, Optional ByVal UpdateSummary As Boolean = True)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New UpdateUIDelegate(AddressOf UpdateUITestRunning), New Object() {test, UpdateSummary})
        Else
            If Me.IsDisposed Then StopIfDebugging() : Return
            Dim item As ListViewItem = TryCast(test.Tag, ListViewItem)
            If item IsNot Nothing Then
                For Each item In lstTests.Items
                    If item.Tag Is test Then
                        Exit For
                    Else
                        item = Nothing
                    End If
                Next
            End If
            Debug.Assert(item IsNot Nothing)

            item.SubItems(2).Text = ""
            item.SubItems(3).Text = ""
            item.SubItems(4).Text = ""
            '(not implemnted in winforms yet)'item.StateImageIndex = m_BlueIndex
        End If
    End Sub

    Private Sub UpdateState()
        tblTestsInQueue.Text = "Tests in queue: " & m_TestExecutor.QueueCount.ToString
    End Sub

    Private Sub cmdRun_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdRun.Click
        Try
            AddWork(m_Tests.Values, True)
            UpdateState()
        Catch ex As Exception
            MsgBox(String.Format("Error while executing tests: ") & ex.Message)
        End Try
    End Sub

    Private Sub cmdRunFailed_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdRunFailed.Click
        Try
            For Each Test As Test In m_Tests.Values
                If Test.Success Then Continue For
                AddWork(Test, True)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(String.Format("Error while executing tests: ") & ex.Message)
        End Try
    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        Try
            Dim cmd As Button = TryCast(sender, Button)
            If cmd Is Nothing Then cmd = cmdPause

            If cmd.Text = "Pause" Then
                m_TestExecutor.Pause()
                cmd.Text = "Resume"
            ElseIf cmd.Text = "Resume" Then
                m_TestExecutor.Resume()
                cmd.Text = "Pause"
            Else

            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdBasepath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdBasepath.Click
        Try
            dlgBasepath.SelectedPath = cmbBasepath.Text
            If dlgBasepath.ShowDialog = Windows.Forms.DialogResult.OK Then
                cmbBasepath.Text = dlgBasepath.SelectedPath
                LoadTests()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdCompiler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompiler.Click
        Try
            dlgFile.FileName = cmbCompiler.Text
            If dlgFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                cmbCompiler.Text = dlgFile.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmnuDebugTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuDebugTest.Click
        Try
            DebugTest(Me.GetSelectedTests)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmnuViewCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuViewCode.Click, cmnuViewCode2.Click
        Try
            ViewCode(Me.GetSelectedTests)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmnuRunTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuRunTest.Click
        Try
            AddWork(GetSelectedTests, True)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddWork(ByVal Tests As Generic.IEnumerable(Of Test), ByVal Priority As Boolean)
        Try
            If Me.IsDisposed Then StopIfDebugging() : Return
            m_TestExecutor.RunAsync(Tests, Priority)
            tblTestsInQueue.Text = "Tests in queue: " & m_TestExecutor.QueueCount.ToString
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub AddWork(ByVal Test As Test, ByVal Priority As Boolean)
        Try
            If Me.IsDisposed Then StopIfDebugging() : Return
            m_TestExecutor.RunAsync(Test, Priority)
            tblTestsInQueue.Text = "Tests in queue: " & m_TestExecutor.QueueCount.ToString
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub StopWork()
        Try
            m_TestExecutor.Stop()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function GetSelectedTests() As Generic.List(Of Test)
        Dim result As New Generic.List(Of Test)

        For Each item As ListViewItem In lstTests.SelectedItems
            Dim test As Test = TryCast(item.Tag, Test)
            If test IsNot Nothing Then result.Add(test)
        Next

        Return result
    End Function

    Private Function GetSelectedTest() As Test
        If lstTests.SelectedItems.Count = 1 Then
            Return DirectCast(lstTests.SelectedItems(0).Tag, Test)
        Else
            Return Nothing
        End If
    End Function

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            LoadTests()
            lstTests.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            StopWork()

            My.Settings.TestsListView_colDate_Width = colDate.Width
            My.Settings.TestsListView_colFailedVerification_Width = colFailedVerification.Width
            My.Settings.TestsListView_colName_Width = colName.Width
            My.Settings.TestsListView_colResult_Width = colResult.Width
            My.Settings.TestsListView_colKnownFailureReason_Width = colKnownFailureReason.Width

            My.Settings.txtVBCCompiler_Text = cmbCompiler.Text
            My.Settings.txtVBNCCompiler_Text = cmbVBCCompiler.Text
            My.Settings.txtBasePath_Text = cmbBasepath.Text

            m_Tests.Save()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmnuViewFile_Items_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim item As ToolStripMenuItem
            item = TryCast(sender, ToolStripMenuItem)
            If item IsNot Nothing Then
                'Process.Start("notepad.exe", """" & item.Text & """")
                Process.Start("""" & item.Text & """")
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub mnuToolsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsRefresh.Click
        Try
            cmdRefresh_Click(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        Try
            m_TestExecutor.Stop()
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRerunGreen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            For Each t As Test In m_Tests.GetGreenTests.Values
                AddWork(t, False)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRerunRed_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            For Each t As Test In m_Tests.GetRedTests.Values
                AddWork(t, False)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRerunYellow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            For Each t As Test In m_Tests.GetNotRunTests.Values
                AddWork(t, False)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmnuViewCodeAndDebugTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuViewCodeAndDebugTest.Click
        Try
            cmnuViewCode2.PerformClick()
            cmnuDebugTest.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub ViewCode(ByVal Tests As Generic.List(Of Test))
        For Each test As Test In Tests
            For Each File As String In test.Files
                MainModule.ViewFiles(Path.Combine(test.FullWorkingDirectory, File))
            Next
        Next
    End Sub

    Private Sub DebugTest(ByVal Tests As Generic.List(Of Test))
        If Tests.Count <> 1 Then
            MsgBox("Select only one test, please.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Return
        End If

        Dim test As Test = Tests(0)

        Dim strTestFile As String
        strTestFile = "/Debug" & vbNewLine
        For Each str As String In test.GetTestCommandLineArguments(, False)
            strTestFile &= """" & str & """" & vbNewLine
        Next

        For Each str As String In test.Files
            strTestFile &= """" & Path.Combine(test.FullWorkingDirectory, str) & """" & vbNewLine
        Next

        IO.File.WriteAllText(IO.Path.Combine(IO.Path.GetDirectoryName(m_Tests.VBNCPath), "debug.rsp"), strTestFile)

    End Sub

    Private Sub cmdVBCCompiler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVBCCompiler.Click
        Try
            dlgFile.FileName = cmbVBCCompiler.Text
            Dim tmpFilter As String = dlgFile.Filter
            dlgFile.Filter = "vbc.exe|vbc.exe|All files (*.*)|*.*"
            If dlgFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                cmbVBCCompiler.Text = dlgFile.FileName
            End If
            dlgFile.Filter = tmpFilter
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReload.Click
        Try
            LoadTests()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub BothAssembliesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BothAssembliesToolStripMenuItem.Click
        Try
            Dim vbc, vbnc As String
            Dim test As Test = GetSelectedTest()
            If test Is Nothing Then
                MsgBox("Select a test")
                Return
            End If
            vbc = test.OutputVBCAssembly
            vbnc = test.OutputAssembly

            Process.Start(GetReflectorPath, """" & vbc & """ """ & vbnc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function GetReflectorPath() As String
        Dim path As String
        path = GetSetting(Application.ProductName, Me.Name, "Reflector", Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\Reflector\Reflector.exe"))
        If IO.File.Exists(path) = False Then
            path = InputBox("Path of reflector: ", "Reflector", path)
        End If
        If path <> "" Then
            SaveSetting(Application.ProductName, Me.Name, "Reflector", path)
        End If
        Return path
    End Function

    Private Function GetIldasmPath() As String
        Dim path As String
        path = GetSetting(Application.ProductName, Me.Name, "Ildasm", Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\Microsoft SDKs\Windows\v6.0A\bin\Ildasm.exe"))
        If IO.File.Exists(path) = False Then
            path = InputBox("Path of ildasm.exe: ")
        End If
        If path <> "" Then
            SaveSetting(Application.ProductName, Me.Name, "Ildasm", path)
        End If
        Return path
    End Function

    Private Sub VBNCAssemblyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBNCAssemblyToolStripMenuItem.Click
        Try
            Dim vbnc As String
            Dim test As Test = GetSelectedTest()
            If test Is Nothing Then
                MsgBox("Select a test")
                Return
            End If
            vbnc = test.OutputAssembly

            Process.Start(GetReflectorPath, """" & vbnc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub VBCAssemblyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBCAssemblyToolStripMenuItem.Click
        Try
            Dim vbc As String
            Dim test As Test = GetSelectedTest()
            If test Is Nothing Then
                MsgBox("Select a test")
                Return
            End If
            vbc = test.OutputVBCAssembly

            Process.Start(GetReflectorPath, """" & vbc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub PopulateTestList()
        Dim Tests As Generic.IEnumerable(Of Test)

        If String.IsNullOrEmpty(txtFilter.Text) Then
            Tests = m_Tests.Values
        Else
            Dim tmp As Generic.List(Of Test)
            tmp = New Generic.List(Of Test)
            For Each Test As Test In m_Tests.Values
                If Test.Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    tmp.Add(Test)
                End If
            Next
            Tests = tmp
        End If

        lstTests.BeginUpdate()
        lstTests.Items.Clear()
        Dim items As New Generic.List(Of ListViewItem)
        For Each test As Test In Tests
            Dim item As ListViewItem
            item = m_TestView.GetListViewItem(test)
            items.Add(item)
        Next
        lstTests.Items.AddRange(items.ToArray)
        lstTests.EndUpdate()
    End Sub

    Private Sub SelectTest(ByVal Test As Test)
        If Test Is Nothing Then
            txtTestMessage.Text = String.Empty
            gridTestProperties.SelectedObject = Nothing
        Else
            txtTestMessage.Text = Test.FailedVerificationMessage
            If String.IsNullOrEmpty(txtTestMessage.Text) Then
                txtTestMessage.Text = Test.StdOut
            End If
            If String.IsNullOrEmpty(txtTestMessage.Text) Then
                txtTestMessage.Text = Test.Message
            End If
            gridTestProperties.SelectedObject = Test
        End If
    End Sub

    Private Sub AllTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.m_TestExecutor.RunAsync(m_Tests.Values)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub FailedTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.m_TestExecutor.RunAsync(m_Tests.GetRedTests.Values)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub SucceededTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.m_TestExecutor.RunAsync(m_Tests.GetGreenTests.Values)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub NotRunTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.m_TestExecutor.RunAsync(m_Tests.GetNotRunTests.Values)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub RunTestsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.m_TestExecutor.RunAsync(m_Tests.GetRunTests.Values)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub m_TestExecutor_AfterExecute(ByVal Test As Test) Handles m_TestExecutor.AfterExecute
        Try
            If Me.InvokeRequired Then
                Me.BeginInvoke(New TestExecutor.AfterExecuteDelegate(AddressOf m_TestExecutor_AfterExecute), Test)
                Return
            End If
            UpdateSummary()
            If Me.GetSelectedTest Is Test Then
                lstTests_SelectedIndexChanged(lstTests, Nothing)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles worker.DoWork
        Try

        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub tabMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            UpdateSummary()
        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub MakeErrorTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeErrorTestToolStripMenuItem.Click
        Try
            Dim tests As Generic.List(Of Test), test As Test
            tests = GetSelectedTests()

            If tests.Count <> 1 Then
                Throw New ApplicationException("Select one and only one test.")
            End If

            test = tests(0)

            test.DoTest()

            If test.Run = False Then
                Throw New ApplicationException("The test has not been executed!")
            ElseIf test.Files.Count <> 1 Then
                Throw New ApplicationException("The test has more than one file!")
            End If

            If test.VBCVerification Is Nothing Then Throw New ApplicationException("No VBC results")

            Dim errnumber As String
            Dim source As String = IO.Path.Combine(test.FullWorkingDirectory, test.Files(0))
        
            Dim errors As New Generic.List(Of ErrorInfo)
            Dim ei As ErrorInfo
            For Each line As String In test.VBCVerification.Process.StdOut.Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                ei = ErrorInfo.ParseLine(line)
                If ei IsNot Nothing Then errors.Add(ei)
            Next

            If errors.Count = 0 Then Throw New ApplicationException("The test doesn't have any compiler errors")
            errnumber = errors(0).Number.ToString()

            Dim errdir As String
            errdir = IO.Path.Combine(IO.Path.GetDirectoryName(m_Tests.Filename), "Errors")

            If IO.Directory.Exists(errdir) = False Then
                Throw New ApplicationException("Couldn't find an errors directory (tried: " & errdir & ")!")
            End If

            Dim destination As String
            Dim counter As Integer
            Dim name As String

            name = errnumber
            destination = IO.Path.Combine(errdir, name & ".vb")
            Do While IO.File.Exists(destination)
                counter += 1
                name = errnumber & "-" & counter
                destination = IO.Path.Combine(errdir, name & ".vb")
            Loop

            IO.File.Copy(source, destination, False)

            Dim new_test As Test
            new_test = New Test(m_Tests)
            new_test.ExpectedExitCode = 1
            new_test.Arguments = test.Arguments
            new_test.Name = name
            new_test.Errors.AddRange(errors)
            new_test.Files.Add(Path.Combine("Errors", Path.GetFileName(destination)))
            new_test.Target = rt.Test.Targets.Library
            m_Tests.Append(new_test)

            PopulateTestList()
            For Each item As ListViewItem In lstTests.Items
                If item.Tag Is new_test Then
                    lstTests.EnsureVisible(item.Index)
                    lstTests.SelectedItems.Clear()
                    lstTests.SelectedIndices.Add(item.Index)
                    Exit For
                End If
            Next

            MsgBox("Created test " & name, MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)

        Catch ex As ApplicationException
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub CreateKnownFailurestxtToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim failures As New Generic.List(Of String)
            ListKnownFailures(m_Tests, m_Tests, failures)

            Dim tmp As String = IO.Path.GetTempFileName
            IO.File.WriteAllLines(tmp, failures.ToArray)
            Process.Start("notepad.exe", """" & tmp & """")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub ListKnownFailures(ByVal Root As Tests, ByVal Tests As Tests, ByVal failures As Generic.List(Of String))
        For Each t As Test In Tests.Values
            Select Case t.Result
                Case Test.Results.Failed, Test.Results.KnownFailureFailed, Test.Results.Regressed
                    Dim f As String
                    f = t.Name
                    If Root IsNot Tests Then
                        f = IO.Path.Combine(Tests.Filename.Substring(Root.Filename.Length), f)
                        If f.StartsWith(IO.Path.DirectorySeparatorChar) Then
                            f = f.Substring(1)
                        End If
                    End If
                    If t.FailedVerificationMessage <> "" Then
                        f = f & " '" & Split(t.FailedVerificationMessage, vbNewLine)(0)
                    End If
                    failures.Add(f)
                Case Test.Results.Success, Test.Results.KnownFailureSucceeded
                Case Test.Results.Running, Test.Results.Skipped, Test.Results.NotRun
            End Select
        Next
    End Sub

    Private Sub mnuIldasmBoth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIldasmBoth.Click
        Try
            Dim test As Test = GetSelectedTest()
            If test Is Nothing Then
                MsgBox("Select a test")
                Return
            End If

            Dim vbc As String = Path.GetTempFileName()
            Dim vbnc As String = Path.GetTempFileName()

            IO.File.Delete(vbc)
            IO.File.Delete(vbnc)

            vbc = Path.Combine(Path.GetDirectoryName(vbc), "vbc_" & Path.GetFileName(vbc))
            vbnc = Path.Combine(Path.GetDirectoryName(vbnc), "vbnc_" & Path.GetFileName(vbnc))

            IO.File.Copy(test.OutputVBCAssembly, vbc, True)
            IO.File.Copy(test.OutputAssembly, vbnc, True)

            Process.Start(GetIldasmPath, """" & vbc & """")
            Process.Start(GetIldasmPath, """" & vbnc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub mnuIldasmDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIldasmDump.Click
        Try
            Dim test As Test = GetSelectedTest()
            If test Is Nothing Then
                MsgBox("Select a test")
                Return
            End If

            Dim vbc As String = Path.GetTempFileName()
            Dim vbnc As String = Path.GetTempFileName()

            File.Delete(vbc)
            File.Delete(vbnc)

            vbc = Path.Combine(Path.GetDirectoryName(vbc), "vbc_" & Path.GetFileName(vbc))
            vbnc = Path.Combine(Path.GetDirectoryName(vbnc), "vbnc_" & Path.GetFileName(vbnc))

            IO.File.Copy(test.OutputVBCAssembly, vbc, True)
            IO.File.Copy(test.OutputAssembly, vbnc, True)

            Process.Start(GetIldasmPath, String.Format("""{0}"" ""/out={1}"" /tokens /metadata=raw /metadata=heaps /metadata=validate /metadata=mdheader /metadata=schema", test.OutputAssembly, vbnc)).WaitForExit()
            Process.Start(GetIldasmPath, String.Format("""{0}"" ""/out={1}"" /tokens /metadata=raw /metadata=heaps /metadata=validate /metadata=mdheader /metadata=schema", test.OutputVBCAssembly, vbc)).WaitForExit()

            Process.Start("notepad", String.Format("""{0}""", vbnc))
            Process.Start("notepad", String.Format("""{0}""", vbc))

            Threading.ThreadPool.QueueUserWorkItem(AddressOf delete_file, vbc)
            Threading.ThreadPool.QueueUserWorkItem(AddressOf delete_file, vbnc)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub delete_file(ByVal arg As Object)
        Try
            Dim name As String = DirectCast(arg, String)
            'wait for notepad to start
            Threading.Thread.Sleep(2000)
            While File.Exists(name)
                Threading.Thread.Sleep(1000)
                Try
                    File.Delete(name)
                Catch ex As Exception
                    'Ignore
                End Try
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            m_Tests.Save()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdCreateTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateTest.Click
        Try
            Using frmNewTest As frmNewTest = New frmNewTest(Me)
                frmNewTest.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdFindTests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFindTests.Click
        Try
            Dim frmFiles As frmFiles = New frmFiles(Me)
            frmFiles.Show(Me)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbVBCCompiler_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVBCCompiler.TextChanged
        Try
            m_Tests.VBCPath = cmbVBCCompiler.Text
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmbCompiler_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompiler.TextChanged
        Try
            m_Tests.VBNCPath = cmbCompiler.Text
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Public Sub CreateTest(ByVal Name As String, ByVal ParamArray Files() As String)
        Dim test As Test

        If String.IsNullOrEmpty(Name) Then Return

        test = New Test(m_Tests)
        test.Name = Name
        test.Target = rt.Test.Targets.Library
        test.MyType = rt.Test.MyTypes.Empty
        test.Files.AddRange(Files)

        m_Tests.Append(test)
        PopulateTestList()
        For Each item As ListViewItem In lstTests.Items
            If item.Tag Is test Then
                lstTests.EnsureVisible(item.Index)
                lstTests.SelectedItems.Clear()
                lstTests.SelectedIndices.Add(item.Index)
                Exit For
            End If
        Next

        If m_SaveTimer Is Nothing Then
            m_SaveTimer = New Timer()
            m_SaveTimer.Interval = 30000
            m_SaveTimer.Start()
        End If
    End Sub

    Private Sub CreateNewTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewTestToolStripMenuItem.Click
        Try
            Using frm As New frmNewTest(Me)
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub m_SaveTimer_Tick(ByVal sender As Object, ByVal ea As EventArgs) Handles m_SaveTimer.Tick
        Try
            m_Tests.Save(False)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmnuDeleteTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuDeleteTest.Click
        Try
            Dim tests As Generic.List(Of Test) = Me.GetSelectedTests()
            For Each Test As Test In tests
                m_Tests.Remove(Test.ID)
            Next
            PopulateTestList()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cmdRun_Click(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        Try
            PopulateTestList()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub lstTests_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstTests.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Space
                    AddWork(GetSelectedTests, True)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub MakeErrorTestToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeErrorTestToolStripMenuItem1.Click
        MakeErrorTestToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub UpdateErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateErrorsToolStripMenuItem.Click
        Try
            Dim ei As ErrorInfo

            For Each test As Test In GetSelectedTests()
                test.DoTest()

                If test.VBCVerification Is Nothing Then Throw New ApplicationException("No VBC results")

                test.Errors.Clear()
                For Each line As String In test.VBCVerification.Process.StdOut.Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                    ei = ErrorInfo.ParseLine(line)
                    If ei IsNot Nothing Then test.Errors.Add(ei)
                Next
                test.ExpectedExitCode = test.VBCVerification.Process.ExitCode

                test.DoTest()
                'MsgBox(String.Format("Updated {0}", test.Name), MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub SetVbcErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetVbcErrorsToolStripMenuItem.Click
        Try
            For Each Test As Test In GetSelectedTests()
                Test.VBCErrors = New Generic.List(Of ErrorInfo)
                For Each ei As ErrorInfo In Test.Errors
                    Test.VBCErrors.Add(New ErrorInfo(ei.Line, ei.Number, ei.Message))
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub ClearVbcErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearVbcErrorsToolStripMenuItem.Click
        Try
            For Each Test As Test In GetSelectedTests()
                Test.VBCErrors = Nothing
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub UpdateVbncErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateVbncErrorsToolStripMenuItem.Click
        Try
            Dim ei As ErrorInfo

            For Each test As Test In GetSelectedTests()
                test.DoTest()

                If test.vbncVerification Is Nothing Then Throw New ApplicationException("No VBNC results")

                test.Errors.Clear()
                For Each line As String In test.VBNCVerification.Process.StdOut.Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                    ei = ErrorInfo.ParseLine(line)
                    If ei IsNot Nothing Then test.Errors.Add(ei)
                Next
                test.ExpectedExitCode = test.VBNCVerification.Process.ExitCode

                test.DoTest()
                MsgBox(String.Format("Updated {0}", test.Name), MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub JustFixTheErrrsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JustFixTheErrrsToolStripMenuItem.Click
        Try
            Dim ei As ErrorInfo

            For Each test As Test In GetSelectedTests()
                test.Errors.Clear()
                test.VBCErrors = Nothing

                test.DoTest()

                If test.VBNCVerification Is Nothing Then Throw New ApplicationException("No VBNC results")
                If test.VBCVerification Is Nothing Then Throw New ApplicationException("No VBC results")

                For Each line As String In test.VBCVerification.Process.StdOut.Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                    ei = ErrorInfo.ParseLine(line)
                    If ei IsNot Nothing Then test.Errors.Add(ei)
                Next
                test.ExpectedExitCode = test.VBCVerification.Process.ExitCode
                test.ExpectedVBCExitCode = Nothing

                test.DoTest() ' run the test again

                If test.Success OrElse test.Result = rt.Test.Results.KnownFailureSucceeded Then Return

                'No success, set vbnc results
                test.ExpectedVBCExitCode = test.ExpectedExitCode
                test.ExpectedExitCode = test.VBNCVerification.Process.ExitCode

                test.VBCErrors = New Generic.List(Of ErrorInfo)(test.Errors)
                test.Errors.Clear()

                For Each line As String In test.VBNCVerification.Process.StdOut.Split(New String() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                    ei = ErrorInfo.ParseLine(line)
                    If ei IsNot Nothing Then test.Errors.Add(ei)
                Next

                test.DoTest()
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
End Class

