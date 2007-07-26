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

Class frmMain
    Inherits Windows.Forms.Form

    Private WithEvents m_Tests As Tests
    Private WithEvents m_TestExecutor As New TestExecutor
    Private m_TestView As New TestView(Me)

    Private m_TransparentIndex, m_RedIndex, m_YellowIndex, m_GreenIndex, m_BlueIndex, m_IndigoIndex As Integer
    Private m_TransparentIcon, m_RedIcon, m_YellowIcon, m_GreenIcon, m_BlueIcon, m_IndigoIcon As Icon

    Private Delegate Sub UpdateUIDelegate(ByVal test As Test, ByVal UpdateSummary As Boolean)
    Private Delegate Sub UpdateUIDelegate2()

    ReadOnly Property TransparentIconIndex() As Integer
        Get
            Return m_TransparentIndex
        End Get
    End Property

    ReadOnly Property RedIconIndex() As Integer
        Get
            Return m_RedIndex
        End Get
    End Property

    ReadOnly Property YellowIconIndex() As Integer
        Get
            Return m_YellowIndex
        End Get
    End Property

    ReadOnly Property GreenIconIndex() As Integer
        Get
            Return m_GreenIndex
        End Get
    End Property

    ReadOnly Property IndigoIconIndex() As Integer
        Get
            Return m_IndigoIndex
        End Get
    End Property

    ReadOnly Property BlueIconIndex() As Integer
        Get
            Return m_BlueIndex
        End Get
    End Property

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

    Private Sub CreateImages()
        Dim imgTransparent, imgRed, imgYellow, imgGreen, imgBlue, imgIndigo As Bitmap
        imgTransparent = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
        imgRed = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
        imgYellow = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
        imgGreen = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
        imgBlue = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
        imgIndigo = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)

        Dim bounds As New Rectangle(0, 0, 16, 16)

        Using gr As Graphics = Graphics.FromImage(imgTransparent)
            gr.FillEllipse(Brushes.Transparent, bounds)
        End Using
        Using gr As Graphics = Graphics.FromImage(imgRed)
            gr.FillEllipse(Brushes.Red, bounds)
        End Using
        Using gr As Graphics = Graphics.FromImage(imgGreen)
            gr.FillEllipse(Brushes.Green, bounds)
        End Using
        Using gr As Graphics = Graphics.FromImage(imgYellow)
            gr.FillEllipse(Brushes.Yellow, bounds)
        End Using
        Using gr As Graphics = Graphics.FromImage(imgBlue)
            gr.FillEllipse(Brushes.Blue, bounds)
        End Using
        Using gr As Graphics = Graphics.FromImage(imgIndigo)
            gr.FillEllipse(Brushes.Indigo, bounds)
        End Using

        m_TransparentIcon = System.Drawing.Icon.FromHandle(imgTransparent.GetHicon)
        m_RedIcon = System.Drawing.Icon.FromHandle(imgRed.GetHicon)
        m_YellowIcon = System.Drawing.Icon.FromHandle(imgYellow.GetHicon)
        m_GreenIcon = System.Drawing.Icon.FromHandle(imgGreen.GetHicon)
        m_BlueIcon = System.Drawing.Icon.FromHandle(imgBlue.GetHicon)
        m_IndigoIcon = System.Drawing.Icon.FromHandle(imgIndigo.GetHicon)

        lstImages.Images.Add(imgTransparent)
        m_TransparentIndex = lstImages.Images.Count - 1
        lstImages.Images.Add(imgRed)
        m_RedIndex = lstImages.Images.Count - 1
        lstImages.Images.Add(imgYellow)
        m_YellowIndex = lstImages.Images.Count - 1
        lstImages.Images.Add(imgGreen)
        m_GreenIndex = lstImages.Images.Count - 1
        lstImages.Images.Add(imgBlue)
        m_BlueIndex = lstImages.Images.Count - 1
        lstImages.Images.Add(imgIndigo)
        m_IndigoIndex = lstImages.Images.Count - 1
    End Sub

    Sub New()
        MyBase.new()

        InitializeComponent()
        CreateImages()

        lstTests.ListViewItemSorter = New ListViewItemComparer(lstTests)

        Dim tmp As String
        tmp = IO.Path.GetFullPath("..\..\vbnc\bin\vbnc.exe")
        If IO.File.Exists(tmp) Then cmbCompiler.Items.Add(tmp)

        tmp = IO.Path.GetFullPath("..\..\vbnc\tests")
        If IO.Directory.Exists(tmp) Then cmbBasepath.Items.Add(tmp)

        tmp = IO.Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), "Microsoft.Net\Framework\v2.0.50727\vbc.exe")
        If IO.File.Exists(tmp) Then cmbVBCCompiler.Items.Add(tmp)


        colCompiler.Width = My.Settings.TestsListView_colCompiler_Width
        colDate.Width = My.Settings.TestsListView_colDate_Width
        colFailedVerification.Width = My.Settings.TestsListView_colFailedVerification_Width
        colName.Width = My.Settings.TestsListView_colName_Width
        colResult.Width = My.Settings.TestsListView_colResult_Width
        colPath.Width = My.Settings.TestsListView_colPath_Width

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

        Me.EnhancedProgressBar1.Value(0).Color = Color.Red
        Me.EnhancedProgressBar1.Value(1).Color = Color.Yellow
        Me.EnhancedProgressBar1.Value(2).Color = Color.Green

        LoadTests()

        chkDontTestIfNothingHasChanged_CheckedChanged(Nothing, Nothing)
    End Sub

    Public Sub RunTests()
        cmdRun_Click(Nothing, Nothing)
    End Sub

    Private Sub RefreshTests()
        Try
            Dim index As Integer = -1
            If lstTests.SelectedIndices.Count > 0 Then
                index = lstTests.SelectedIndices(0)
            End If

            If m_Tests IsNot Nothing Then
                m_Tests.Dispose()
                m_Tests = Nothing
            End If

            m_Tests = New Tests(cmbBasepath.Text, cmbCompiler.Text, cmbVBCCompiler.Text)
            For Each test As Test In m_Tests
                Dim item As ListViewItem
                item = lstTests.Items.Add(test.Name)
                item.SubItems.Add(test.IsMultiFile.ToString)
                item.Tag = test
            Next
            If index >= 0 Then
                lstTests.SelectedIndices.Add(index)
                lstTests.EnsureVisible(index)
            End If
            txtNumberOfTests.Text = lstTests.Items.Count.ToString
            txtAverageExecutionTime.Text = "0"
            txtExecutionTime.Text = "0"
            txtGreenTests.Text = "0"
            txtMessage.Text = ""
            txtRedTests.Text = "0"
            txtTestsRun.Text = "0"
            txtYellowTests.Text = "0"
            txtQueue.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub LoadTests(Optional ByVal CheckForNewTestsOnly As Boolean = False)
        Try
            StopWork()

            If cmbBasepath.Text = "" OrElse cmbCompiler.Text = "" OrElse cmbVBCCompiler.Text = "" Then
                MsgBox("Invalid paths.")
                Exit Sub
            End If

            If m_Tests IsNot Nothing AndAlso CheckForNewTestsOnly Then
                m_Tests.Update()
            Else
                m_Tests = New Tests(cmbBasepath.Text, cmbCompiler.Text, cmbVBCCompiler.Text)
                'm_Tests.WriteLinuxScript()
            End If

            Dim selectednodetext As String = Nothing
            If treeTests.SelectedNode IsNot Nothing Then selectednodetext = treeTests.SelectedNode.Text
            treeTests.Nodes.Clear()
            LoadTests(m_Tests, treeTests.Nodes)
            treeTests.Nodes(0).Expand()
            If selectednodetext IsNot Nothing Then
                For Each node As TreeNode In treeTests.Nodes(0).Nodes
                    If node.Text = selectednodetext Then
                        treeTests.SelectedNode = node
                        Exit For
                    End If
                Next
            End If

            LoadOldResults()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function LoadTests(ByVal tests As Tests, ByVal nodes As TreeNodeCollection) As TreeNode
        Dim baseNode As TreeNode
        baseNode = nodes.Add(IO.Path.GetFileName(tests.Path))
        baseNode.Tag = tests
        For Each subtests As Tests In tests.ContainedTests
            LoadTests(subtests, baseNode.Nodes)
        Next
        Return baseNode
    End Function

    Private Sub lstTests_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstTests.SelectedIndexChanged
        Try
            For Each item As Test In Me.GetSelectedTests
                item.LoadOldResults()
            Next
            If lstTests.SelectedItems.Count = 1 Then
                SelectTest(Me.GetSelectedTests(0))
            Else
                SelectTest(Nothing)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub lstTests_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstTests.DoubleClick
        Try
            Me.tabMain.SelectedTab = pageTestResult
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Thread-safe.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoTestOf(ByVal Test As Test)
        UpdateUITestRunning(Test)
        Test.DoTest()
        UpdateUI(Test)
    End Sub

    Private Sub UpdateUI()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New UpdateUIDelegate2(AddressOf UpdateUI))
        Else
            For Each t As Test In m_Tests
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

            Dim r, y, g, total, failed As Integer
            Dim runcount, notruncount As Integer
            Dim alltests As Tests
            alltests = Me.GetSelectedTestList
            If alltests Is Nothing Then alltests = m_Tests
            total = alltests.RecursiveCount
            r = alltests.GetRedRecursiveCount
            g = alltests.GetGreenRecursiveCount
            y = total - r - g
            failed = r
            runcount = r + g
            notruncount = y
            If r > 0 Then
                Me.Icon = m_RedIcon
            ElseIf g > 0 AndAlso y > 0 Then
                Me.Icon = m_BlueIcon
            ElseIf g = total Then
                Me.Icon = m_GreenIcon
            Else
                Me.Icon = m_YellowIcon
            End If
            Me.EnhancedProgressBar1.Value(0).PercentDone = r / total
            Me.EnhancedProgressBar1.Value(1).PercentDone = y / total
            Me.EnhancedProgressBar1.Value(2).PercentDone = g / total
            Me.EnhancedProgressBar1.Invalidate()

            If tabMain.SelectedTab Is pageSummary Then
                Dim COUNTERFORMAT As String = "{0} ({1:0.#%})"
                txtRedTests.Text = String.Format(COUNTERFORMAT, r, r / total)
                txtYellowTests.Text = String.Format(COUNTERFORMAT, y, y / total)
                txtGreenTests.Text = String.Format(COUNTERFORMAT, g, g / total)

                txtQueue.Text = m_TestExecutor.QueueCount.ToString
                txtNumberOfTests.Text = total.ToString
                txtTestsRun.Text = (r + g).ToString

                Text = String.Format("RT OK: {0} ({5:#0.0}%) / FAILED: {1} ({4:#0.0}%) / NOT RUN: {2}/{3} tests) / IN QUEUE: {6}", g, r, y, total, r * 100 / total, g * 100 / total, m_TestExecutor.QueueCount)
                Dim exectime As TimeSpan = alltests.ExecutionTimeRecursive
                txtExecutionTime.Text = String.Format("{0}", FormatTimespan(exectime))
                If total > 0 Then
                    txtAverageExecutionTime.Text = String.Format("{0}", FormatTimespan(New TimeSpan(exectime.Ticks \ CInt(IIf(runcount = 0, 1, runcount)))))
                Else
                    txtAverageExecutionTime.Text = "0"
                End If
            End If
            UpdateTreeIcons()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub UpdateTreeIcons()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New CrossAppDomainDelegate(AddressOf UpdateTreeIcons))
            Return
        End If
        For Each subnode As TreeNode In treeTests.Nodes
            UpdateTreeIcons(subnode)
        Next
    End Sub

    Private Sub UpdateTreeIcons(ByVal Node As TreeNode)
        Dim tests As Tests = TryCast(Node.Tag, Tests)

        If tests IsNot Nothing Then
            Dim g, r, t, i As Integer

            t = tests.RecursiveCount
            g = tests.GetGreenRecursiveCount
            r = tests.GetRedRecursiveCount
            i = tests.GetTestsCount(Test.Results.Regressed, Test.Results.Regressed)

            If g + r + i = 0 Then
                'no tests run
                Node.ImageIndex = Me.YellowIconIndex
            ElseIf r > 0 Then
                'at least one red test.
                Node.ImageIndex = Me.RedIconIndex
            ElseIf g = t Then
                'only green tests (of the run tests).
                Node.ImageIndex = Me.GreenIconIndex
            ElseIf i > 0 Then
                'at least one regressed test
                Node.ImageIndex = Me.IndigoIconIndex
            Else 'working, but no red tests yet.
                Node.ImageIndex = Me.BlueIconIndex
            End If
            Node.SelectedImageIndex = Node.ImageIndex
            '(not implemnted in winforms yet)'Node.StateImageIndex = Node.ImageIndex
        End If

        For Each subnode As TreeNode In Node.Nodes
            UpdateTreeIcons(subnode)
        Next
    End Sub

    ''' <summary>
    ''' Thread-safe.
    ''' </summary>
    ''' <param name="test"></param>
    ''' <remarks></remarks>
    Private Sub UpdateUI(ByVal test As Test, Optional ByVal UpdateSummary As Boolean = True)
        If test Is Nothing Then UpdateUI()
        If Me.InvokeRequired Then
            Me.BeginInvoke(New UpdateUIDelegate(AddressOf UpdateUI), New Object () {test, UpdateSummary})
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
            If test.Result = rt.Test.Results.NotRun Then
                newStateImageIndex = m_YellowIndex
            ElseIf test.Result >= rt.Test.Results.Success Then
                newStateImageIndex = m_GreenIndex
            Else
                newStateImageIndex = m_RedIndex
            End If
            If item.StateImageIndex <> newStateImageIndex Then
                '(not implemnted in winforms yet)'item.StateImageIndex = newStateImageIndex
            End If

            If UpdateSummary Then Me.UpdateSummary()

            If lstTests.SelectedItems.Count > 0 AndAlso lstTests.SelectedItems.Contains(item) Then
                lstTests_SelectedIndexChanged(lstTests, Nothing)
            End If
            txtQueue.Text = m_TestExecutor.QueueCount.ToString
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
            Me.BeginInvoke(New UpdateUIDelegate(AddressOf UpdateUITestRunning), new Object() {test, UpdateSummary})
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
            item.SubItems(5).Text = ""
            '(not implemnted in winforms yet)'item.StateImageIndex = m_BlueIndex
        End If
    End Sub

    Private Sub UpdateState()
        txtQueue.Text = m_TestExecutor.QueueCount.ToString
    End Sub

    Private Sub cmdRun_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdRun.Click
        Try
            'm_Tests.RunAsync()
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

    Private Sub mnuToolsChangeOutputToVerified_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuToolsChangeOutputToVerified.Click
        Try
            Dim result As MsgBoxResult
            result = MsgBox("Overwrite existing files?", MsgBoxStyle.YesNoCancel)
            If result = MsgBoxResult.Yes Then
                MainModule.ChangeOutputToVerified(cmbBasepath.Text, True, True)
            ElseIf result = MsgBoxResult.No Then
                MainModule.ChangeOutputToVerified(cmbBasepath.Text, False, True)
            Else
                Exit Sub
            End If
            MsgBox("Output xml files has sucessfully been changed to verified xml files.", MsgBoxStyle.Information)
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
            txtQueue.Text = m_TestExecutor.QueueCount.ToString
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub AddWork(ByVal Test As Test, ByVal Priority As Boolean)
        Try
            If Me.IsDisposed Then StopIfDebugging() : Return
            m_TestExecutor.RunAsync(Test, Priority)
            txtQueue.Text = m_TestExecutor.QueueCount.ToString
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

    Private Sub cmnuOutputToVerified_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuOutputToVerified.Click
        Try
            Dim count As Integer
            For Each test As Test In GetSelectedTests()
                count += MainModule.ChangeOutputToVerified(test, True)
            Next
            MsgBox(String.Format("{0} output xml files has sucessfully been changed to verified xml files.", count), MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            LoadTests()
            lstTests.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If m_Tests IsNot Nothing Then
            m_Tests.Dispose()
            m_Tests = Nothing
        End If
        If m_TestExecutor IsNot Nothing Then
            m_TestExecutor.Dispose()
            m_TestExecutor = Nothing
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            StopWork()
            If m_Tests IsNot Nothing Then
                m_Tests.Dispose()
                m_Tests = Nothing
            End If
            My.Settings.TestsListView_colCompiler_Width = colCompiler.Width
            My.Settings.TestsListView_colDate_Width = colDate.Width
            My.Settings.TestsListView_colFailedVerification_Width = colFailedVerification.Width
            My.Settings.TestsListView_colName_Width = colName.Width
            My.Settings.TestsListView_colResult_Width = colResult.Width
            My.Settings.TestsListView_colPath_Width = colPath.Width

            My.Settings.txtVBCCompiler_Text = cmbCompiler.Text
            My.Settings.txtVBNCCompiler_Text = cmbVBCCompiler.Text
            My.Settings.txtBasePath_Text = cmbBasepath.Text
            My.Settings.ContinuousTest = chkContinuous.Checked
            My.Settings.Save()

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
            For Each t As Test In m_Tests.GetGreenTests
                AddWork(t, False)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRerunRed_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            For Each t As Test In m_Tests.GetRedTests
                AddWork(t, False)
            Next
            UpdateState()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub cmdRerunYellow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            For Each t As Test In m_Tests.GetNotRunTests
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
            MainModule.ViewFiles(test.Files.ToArray)
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
        For Each str As String In test.GetTestCommandLineArguments
            strTestFile &= """" & str & """" & vbNewLine
        Next

        IO.File.WriteAllText("..\..\vbnc\bin\debug.rsp", strTestFile)

    End Sub

    Private Sub cmdCopySummaryToClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopySummaryToClipboard.Click
        Try
            Dim str As New System.Text.StringBuilder
            Dim t, r, g As Integer
            t = Tests.RecursiveCount
            r = Tests.GetRedRecursiveCount
            g = Tests.GetGreenRecursiveCount
            str.AppendLine("# of Tests: " & t.ToString)
            str.AppendLine("# of Tests (Successful): " & g.ToString & " = " & (g / t).ToString("0.0%"))
            str.AppendLine("# of Tests (Failed): " & r.ToString & " = " & (r / t).ToString("0.0%"))
            str.AppendLine("# of Tests (NotRun): " & (t - r - g).ToString & " = " & ((t - r - g) / t).ToString("0.0%"))

            Clipboard.SetText(str.ToString)
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
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

    Private Sub CreateNewTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewTestToolStripMenuItem.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            Using frmEditor As New frmTestEditor
                frmEditor.txtFolder.Text = tests.Path
                If frmEditor.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    LoadTests(True)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateNewTestCopyingThisTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewTestCopyingThisTestToolStripMenuItem.Click
        Try
            Dim t As Test = Me.GetSelectedTest()
            If t Is Nothing Then
                MsgBox("No selected test!")
            ElseIf t.IsMultiFile Then
                MsgBox("This is a multifile test!")
            Else
                Using frmEditor As New frmTestEditor
                    frmEditor.txtFolder.Text = t.BasePath
                    Dim newName As String
                    Dim base As String = IO.Path.Combine(t.BasePath, IO.Path.GetFileNameWithoutExtension(t.Files(0)))
                    Dim i As Integer = 1
                    Do While IsNumeric(base.Chars(base.Length - 1))
                        base = base.Substring(0, base.Length - 1)
                    Loop
                    newName = base & i.ToString & ".vb"
                    Do While IO.File.Exists(newName)
                        i += 1
                        newName = base & i.ToString & ".vb"
                    Loop
                    frmEditor.txtCode.Text = IO.File.ReadAllText(t.Files(0))
                    frmEditor.txtFile.Text = IO.Path.GetFileName(newName)
                    If frmEditor.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        LoadTests(True)
                    End If
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub EditThisTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditThisTestToolStripMenuItem.Click
        Try
            Dim t As Test = Me.GetSelectedTest()
            If t Is Nothing Then
                MsgBox("No selected test!")
            ElseIf t.IsMultiFile Then
                MsgBox("This is a multifile test!")
            Else
                Using frmEditor As New frmTestEditor
                    frmEditor.txtFolder.Text = cmbBasepath.Text
                    frmEditor.txtFile.Text = IO.Path.GetFileName(t.Files(0))
                    frmEditor.txtCode.Text = IO.File.ReadAllText(t.Files(0))
                    If frmEditor.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        LoadTests()
                    End If
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub chkHosted_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHosted.CheckedChanged
        Try
            If m_Tests IsNot Nothing Then
                m_TestExecutor.RunTestsHosted = chkHosted.Checked
            End If
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
            vbc = test.GetOutputVBCAssembly
            vbnc = test.GetOutputAssembly

            Process.Start(GetReflectorPath, """" & vbc & """ """ & vbnc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function GetReflectorPath() As String
        Dim path As String
        path = GetSetting(Application.ProductName, Me.Name, "Reflector", Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\Reflector\Reflector.exe"))
        If IO.File.Exists(path) = False Then
            path = InputBox("Path of reflector: ")
        End If
        If path <> "" Then
            SaveSetting(Application.ProductName, Me.Name, "Reflector", path)
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
            vbnc = test.GetOutputAssembly

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
            vbc = test.GetOutputVBCAssembly

            Process.Start(GetReflectorPath, """" & vbc & """")

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub tmrContinuous_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrContinuous.Tick
        Try
            If chkContinuous.Checked Then
                If m_TestExecutor IsNot Nothing AndAlso m_TestExecutor.QueueCount = 0 AndAlso m_Tests IsNot Nothing Then
                    m_TestExecutor.RunAsyncTree(m_Tests)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub NewTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewTestToolStripMenuItem.Click
        Try
            Dim test As Test = Me.GetSelectedTest
            Dim list As Tests = Me.GetSelectedTestList
            Using frmNew As New frmNewTest
                Dim result As DialogResult
                If test IsNot Nothing Then
                    result = frmNew.ShowDialog(Me, list.Path, test.Name)
                Else
                    result = frmNew.ShowDialog(Me, test.BasePath, "")
                End If
                If result = Windows.Forms.DialogResult.OK Then
                    LoadTests(True)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub CreateNewTestUsingThisTestAsBaseNameToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateNewTestUsingThisTestAsBaseNameToolStripMenuItem.Click
        NewTestToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub treeTests_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeTests.AfterSelect
        Try
            Dim tests As Tests = GetSelectedTestList()
            If tests IsNot Nothing Then
                SelectTestList(tests.GetAllTestsInTree)
            End If
            UpdateSummary()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub treeTests_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeTests.DoubleClick
        Try
            Me.tabMain.SelectedTab = pageTests
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub SelectTestList(ByVal Tests As Generic.IEnumerable(Of Test))
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
        While Me.tabMain.TabPages.Count > 4
            Me.tabMain.TabPages.Remove(Me.tabMain.TabPages(4))
        End While

        If Test Is Nothing Then
            txtTestResult.Text = ""
            txtMessage.Text = ""
        Else
            Test.Initialize()
            If Test.Run Then
                If Test.Success Then
                    txtTestResult.Text = "Success"
                Else
                    txtTestResult.Text = "Failed"
                End If
            Else
                txtTestResult.Text = "NotRun"
            End If
            txtMessage.Text = Test.FailedVerificationMessage
            tabMain.Visible = False
            For Each file As String In Test.Files
                tabMain.TabPages.Add(New FileTabPage(file))
            Next
            If Test.ResponseFile <> "" Then tabMain.TabPages.Add(New FileTabPage(Test.ResponseFile))
            If Test.RspFile <> "" Then tabMain.TabPages.Add(New FileTabPage(Test.RspFile))
            tabMain.Visible = True

            pageOldResults.Tag = Test
        End If
        gridTestProperties.SelectedObject = Test
    End Sub

    Private Function GetSelectedTestList() As Tests
        Dim result As Tests = Nothing

        If Me.treeTests.SelectedNode IsNot Nothing Then
            result = TryCast(Me.treeTests.SelectedNode.Tag, Tests)
        End If

        Return result
    End Function

    Private Sub ViewQueuedTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewQueuedTestsToolStripMenuItem.Click
        Try
            Me.SelectTestList(m_TestExecutor.Queue)
            Me.tabMain.SelectedTab = pageTests
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub AllTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllTestsToolStripMenuItem.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            If tests IsNot Nothing Then
                Me.m_TestExecutor.RunAsync(tests)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub FailedTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FailedTestsToolStripMenuItem.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            If tests IsNot Nothing Then
                Me.m_TestExecutor.RunAsync(tests.GetRedTests)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub SucceededTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SucceededTestsToolStripMenuItem.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            If tests IsNot Nothing Then
                Me.m_TestExecutor.RunAsync(tests.GetGreenTests)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub NotRunTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotRunTestsToolStripMenuItem.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            If tests IsNot Nothing Then
                Me.m_TestExecutor.RunAsync(tests.GetNotRunTests)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub RunTestsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunTestsToolStripMenuItem1.Click
        Try
            Dim tests As Tests = Me.GetSelectedTestList
            If tests IsNot Nothing Then
                Me.m_TestExecutor.RunAsync(tests.GetRunTests)
            End If
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

    Private Sub CreateNewTestInThisFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewTestInThisFolderToolStripMenuItem.Click
        NewTestToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub chkDontTestIfNothingHasChanged_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDontTestIfNothingHasChanged.CheckedChanged
        Try
            If m_Tests IsNot Nothing Then m_Tests.SkipCleanTests = chkDontTestIfNothingHasChanged.Checked
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub OnlyRefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyRefreshToolStripMenuItem.Click
        Try
            Dim tests As Tests
            tests = Me.GetSelectedTestList()
            If tests IsNot Nothing Then
                tests.Update()
            End If
            treeTests_AfterSelect(treeTests, New TreeViewEventArgs(treeTests.SelectedNode))
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub lstOldResults_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstOldResults.SelectedIndexChanged
        Try
            If lstOldResults.SelectedItems.Count = 1 Then
                Dim result As OldResult = TryCast(lstOldResults.SelectedItems(0).Tag, OldResult)
                txtOldResult.Text = result.Text
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub LoadOldResults()
        Static thread As Threading.Thread
        Static sync As New Object

        Return

        SyncLock sync
            If thread Is Nothing Then
                thread = New Threading.Thread(New Threading.ThreadStart(AddressOf LoadOldResults))
                thread.Start()
                Exit Sub
            End If
        End SyncLock

        Try
            Dim tests As Tests = m_Tests
            Dim stack As New Generic.Queue(Of Tests)

            stack.Enqueue(tests)
            Do Until stack.Count = 0
                tests = stack.Dequeue
                For Each subtests As Tests In tests.ContainedTests
                    stack.Enqueue(subtests)
                Next
                For Each test As Test In tests
                    If Me.IsDisposed Then Exit Do
                    Try
                        Me.Invoke(New CrossAppDomainDelegate(AddressOf test.LoadOldResults))
                    Catch ex As Exception
                        Continue For
                    End Try
                    Threading.Thread.Sleep(0)
                Next
            Loop
            thread = Nothing
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub cmdSelfTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelfTest.Click
        Try
            Static selfTests As Generic.List(Of Test)

            If selfTests Is Nothing Then
                selfTests = New Generic.List(Of Test)
                For Each ts As Tests In m_Tests.ContainedTests
                    If ts.Path.Contains("SelfTest") Then
                        selfTests.AddRange(ts)
                    End If
                Next
            End If

            Me.ViewCode(selfTests)
            Me.DebugTest(selfTests)
            AddWork(selfTests, True)
        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles worker.DoWork
        Try

        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub tabMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMain.SelectedIndexChanged

        Try
            If tabMain.SelectedTab Is pageSummary Then
                UpdateSummary()
            ElseIf tabMain.SelectedTab Is pageOldResults Then
                LoadOldTests()
            End If
        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub LoadOldTests()
        Dim oldResults As Generic.List(Of OldResult)
        Dim oldResultsItem As New Generic.List(Of ListViewItem)
        Dim Test As Test

        lstOldResults.Items.Clear()
        lstOldResults.Columns(1).Width = 600

        Test = TryCast(pageOldResults.Tag, Test)
        If Test Is Nothing Then Return

        oldResults = Test.GetOldResults
        For Each result As OldResult In oldResults
            Dim newItem As New ListViewItem(result.Result.ToString)
            newItem.SubItems.Add(result.Compiler)
            newItem.Tag = result
            Select Case result.Result
                Case rt.Test.Results.Failed
                    newItem.ImageIndex = Me.RedIconIndex
                Case rt.Test.Results.NotRun
                    newItem.ImageIndex = Me.YellowIconIndex
                Case rt.Test.Results.Success
                    newItem.ImageIndex = Me.GreenIconIndex
            End Select
            oldResultsItem.Add(newItem)
        Next
        oldResultsItem.Reverse()
        lstOldResults.Items.AddRange(oldResultsItem.ToArray)
        txtOldResult.Text = ""
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            chkContinuous.Checked = My.Settings.ContinuousTest
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub
End Class
