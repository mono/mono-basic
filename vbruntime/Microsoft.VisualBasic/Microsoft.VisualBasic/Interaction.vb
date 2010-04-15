'
' Interaction.vb
'
' Author:
'   Mizrahi Rafael (rafim@mainsoft.com)
'   Guy Cohen (guyc@mainsoft.com)
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
Imports Microsoft.VisualBasic.CompilerServices
#If TARGET_JVM = False Then 'Win32,Windows.Forms Not Supported by Grasshopper
Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Drawing
#End If

Namespace Microsoft.VisualBasic
    <StandardModule()> _
    Public NotInheritable Class Interaction

#If Not MOONLIGHT Then
        Public Shared Sub AppActivate(ByVal ProcessId As Integer)
            'TODO: OS Specific
            Throw New NotImplementedException
        End Sub
        Public Shared Sub AppActivate(ByVal Title As String)
            'TODO: OS Specific
            Throw New NotImplementedException
        End Sub
        Public Shared Sub Beep()
            'TODO: OS Specific
            ' Removed Throw exception, as it does not really harm that the beep does not work.
        End Sub

#End If
        <MonoLimitation("CallType.Let options is not supported.")> _
        Public Shared Function CallByName(ByVal ObjectRef As Object, ByVal ProcName As String, ByVal UseCallType As Microsoft.VisualBasic.CallType, ByVal ParamArray Args() As Object) As Object
            Select Case UseCallType
                Case CallType.Get
                    Return LateBinding.LateGet(ObjectRef, ObjectRef.GetType(), ProcName, Args, Nothing, Nothing)
                Case CallType.Let
                    Throw New NotImplementedException("Microsoft.VisualBasic.Interaction.CallByName Case CallType.Let")
                Case CallType.Method
                    LateBinding.LateCall(ObjectRef, ObjectRef.GetType(), ProcName, Args, Nothing, Nothing)
                Case CallType.Set
                    LateBinding.LateSet(ObjectRef, ObjectRef.GetType(), ProcName, Args, Nothing)
            End Select
            Return Nothing
        End Function

        Public Shared Function Choose(ByVal Index As Double, ByVal ParamArray Choice() As Object) As Object

            If (Choice.Rank <> 1) Then
                Throw New ArgumentException
            End If

            'FIXME: why Index is Double, while an Index of an Array is Integer ?
            Dim IntIndex As Integer
            IntIndex = Convert.ToInt32(Index)
            Dim ChoiceIndex As Integer = IntIndex - 1

            If ((IntIndex >= 0) And (ChoiceIndex <= Information.UBound(Choice))) Then
                Return Choice(ChoiceIndex)
            Else
                Return Nothing
            End If
        End Function
#If Not MOONLIGHT Then
        Public Shared Function Command() As String
            'TODO: OS Specific
            Return String.Join(" ", Environment.GetCommandLineArgs)
        End Function
        Public Shared Function CreateObject(ByVal ProgId As String, Optional ByVal ServerName As String = "") As Object
            'TODO: COM
            Throw New NotImplementedException
        End Function
        Public Shared Sub DeleteSetting(ByVal AppName As String, Optional ByVal Section As String = Nothing, Optional ByVal Key As String = Nothing)

#If TARGET_JVM = False Then

            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            If Section Is Nothing Then
                rkey.DeleteSubKeyTree(AppName)
            Else
                If Key Is Nothing Then
                    rkey.DeleteSubKeyTree(Section)
                Else
                    rkey = rkey.OpenSubKey(Section)
                    rkey.DeleteValue(Key)
                End If
            End If

            'Closes the key and flushes it to disk if the contents have been modified.
            rkey.Close()
#Else
            Throw New NotImplementedException
#End If
        End Sub
        Public Shared Function Environ(ByVal Expression As Integer) As String
            Throw New NotImplementedException
        End Function
        Public Shared Function Environ(ByVal Expression As String) As String
            Return Environment.GetEnvironmentVariable(Expression)
        End Function

        <MonoLimitation("If this function is used the assembly have to be recompiled when you switch platforms.")> _
        Public Shared Function GetAllSettings(ByVal AppName As String, ByVal Section As String) As String(,)

#If TARGET_JVM = False Then

            If AppName Is Nothing OrElse AppName.Length = 0 Then Throw New System.ArgumentException(" Argument 'AppName' is Nothing or empty.")
            If Section Is Nothing OrElse Section.Length = 0 Then Throw New System.ArgumentException(" Argument 'Section' is Nothing or empty.")

            Dim res_setting(,) As String
            Dim index, elm_count As Integer
            Dim regk As RegistryKey
            Dim arr_str() As String

            regk = Registry.CurrentUser
            Try
                ''TODO: original dll set/get settings from this path
                regk = regk.OpenSubKey("Software\VB and VBA Program Settings\" + AppName)
                regk = regk.OpenSubKey(Section)
            Catch ex As Exception
                Return Nothing
            End Try
            If (regk Is Nothing) Then
                Return Nothing
            Else
                elm_count = regk.ValueCount
                If elm_count = 0 Then Return Nothing
            End If

            ReDim arr_str(elm_count)
            ReDim res_setting(elm_count - 1, 1)
            arr_str = regk.GetValueNames()
            For index = 0 To elm_count - 1
                res_setting(index, 0) = arr_str(index)
                res_setting(index, 1) = Interaction.GetSetting(AppName, Section, arr_str(index))
            Next
            Return res_setting

#Else
            Throw New NotImplementedException
#End If
        End Function
        Public Shared Function GetObject(Optional ByVal PathName As String = Nothing, Optional ByVal [Class] As String = Nothing) As Object
            'TODO: COM
            Throw New NotImplementedException
        End Function
        Public Shared Function GetSetting(ByVal AppName As String, ByVal Section As String, ByVal Key As String, Optional ByVal [Default] As String = "") As String
#If TARGET_JVM = False Then
            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            rkey = rkey.OpenSubKey("Software\VB and VBA Program Settings\" + AppName)
            rkey = rkey.OpenSubKey(Section)
            Return rkey.GetValue(Key, CObj([Default])).ToString
#Else
            Throw New NotImplementedException
#End If
        End Function
#End If
        Public Shared Function IIf(ByVal Expression As Boolean, ByVal TruePart As Object, ByVal FalsePart As Object) As Object
            If Expression Then
                Return TruePart
            Else
                Return FalsePart
            End If
        End Function

#If Not MOONLIGHT Then
#If TARGET_JVM = False Then
        Class InputForm
            Inherits Form
            Dim bok As Button
            Dim bcancel As Button
            Dim entry As TextBox
            Dim result As String

            Public Sub New(ByVal Prompt As String, Optional ByVal Title As String = "", Optional ByVal DefaultResponse As String = "", Optional ByVal XPos As Integer = -1, Optional ByVal YPos As Integer = -1)
                SuspendLayout()

                Text = Title
                ClientSize = New Size(400, 120)

                bok = New Button()
                bok.Text = "Ok"

                bcancel = New Button()
                bcancel.Text = "Cancel"

                entry = New TextBox()
                entry.Text = DefaultResponse
                result = DefaultResponse

                AddHandler bok.Click, AddressOf ok_Click
                AddHandler bcancel.Click, AddressOf cancel_Click

                bok.Location = New Point(ClientSize.Width - bok.ClientSize.Width - 8, 8)
                bcancel.Location = New Point(bok.Location.X, 8 + bok.ClientSize.Height + 8)
                entry.Location = New Point(8, 80)
                entry.ClientSize = New Size(ClientSize.Width - 28, entry.ClientSize.Height)

                Controls.Add(bok)
                Controls.Add(bcancel)
                Controls.Add(entry)
                ResumeLayout(False)
            End Sub

            Public Function Run() As String
                If Me.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Return result
                Else
                    Return String.Empty
                End If
            End Function

            Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
                result = entry.Text
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End Sub

            Private Sub cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End Sub
        End Class
#End If

        Public Shared Function InputBox(ByVal Prompt As String, Optional ByVal Title As String = "", Optional ByVal DefaultResponse As String = "", Optional ByVal XPos As Integer = -1, Optional ByVal YPos As Integer = -1) As String
#If TARGET_JVM = False Then
            Dim f As InputForm
            f = New InputForm(Prompt, Title, DefaultResponse, XPos, YPos)
            Return f.Run()
#Else
            Throw New NotImplementedException
#End If
        End Function
#End If
        Public Shared Function Partition(ByVal Number As Long, ByVal Start As Long, ByVal [Stop] As Long, ByVal Interval As Long) As String

            Dim strEnd As String = ""
            Dim strStart As String = ""
            Dim strStop As String = ""


            Dim lEnd, lStart As Long
            Dim nSpaces As Integer

            If Start < 0 Then Throw New System.ArgumentException("Argument 'Start' is not a valid value.")
            If [Stop] <= Start Then Throw New System.ArgumentException("Argument 'Stop' is not a valid value.")
            If Interval < 1 Then Throw New System.ArgumentException("Argument 'Start' is not a valid value.")

            If Number > [Stop] Then
                strEnd = "Out Of Range"
                lStart = [Stop] - 1
            ElseIf Number < Start Then
                strStart = "Out Of Range"
                lEnd = Start - 1
            ElseIf (Number = Start) Then
                lStart = Number
                If (lEnd < (Number + Interval)) Then
                    lEnd = Number + Interval - 1
                Else
                    lEnd = [Stop]
                End If
            ElseIf (Number = [Stop]) Then
                lEnd = [Stop]
                If (lStart > (Number - Interval)) Then
                    lStart = Number
                Else
                    lStart = Number - Interval + 1
                End If
            ElseIf Interval = 1 Then
                lStart = Number
                lEnd = Number
            Else
                lStart = Start
                While (lStart < Number)
                    lStart += Interval
                End While
                lStart = lStart - Interval
                lEnd = lStart + Interval - 1
            End If

            If String.Equals(strEnd, "Out Of Range") Then
                strEnd = ""
            Else
                strEnd = Conversions.ToString(lEnd)
            End If

            If String.Equals(strStart, "Out Of Range") Then
                strStart = ""
            Else
                strStart = Conversions.ToString(lStart)
            End If

            strStop = Conversions.ToString([Stop])

            If (strEnd.Length > strStop.Length) Then
                nSpaces = strEnd.Length
            Else
                nSpaces = strStop.Length
            End If

            If (nSpaces = 1) Then nSpaces = nSpaces + 1

            Return strStart.PadLeft(nSpaces) + ":" + strEnd.PadLeft(nSpaces)

        End Function
#If Not MOONLIGHT Then
        Public Shared Sub SaveSetting(ByVal AppName As String, ByVal Section As String, ByVal Key As String, ByVal Setting As String)

#If TARGET_JVM = False Then

            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            rkey = rkey.CreateSubKey("Software\VB and VBA Program Settings\" + AppName)
            rkey = rkey.CreateSubKey(Section)
            rkey.SetValue(Key, Setting)
            'Closes the key and flushes it to disk if the contents have been modified.
            rkey.Close()
#Else
            Throw New NotImplementedException
#End If
        End Sub
        Public Shared Function Shell(ByVal Pathname As String, Optional ByVal Style As Microsoft.VisualBasic.AppWinStyle = Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, Optional ByVal Wait As Boolean = False, Optional ByVal Timeout As Integer = -1) As Integer
            'TODO: OS Specific
            Throw New NotImplementedException
        End Function
#End If
        Public Shared Function Switch(ByVal ParamArray VarExpr() As Object) As Object
            Dim i As Integer
            If VarExpr Is Nothing Then
                Return Nothing
            End If

            If Not (VarExpr.Length Mod 2 = 0) Then
                Throw New System.ArgumentException("Argument 'VarExpr' is not a valid value.")
            End If
            For i = 0 To VarExpr.Length Step 2
                If Conversions.ToBoolean(VarExpr(i)) Then Return VarExpr(i + 1)
            Next
            Return Nothing
        End Function

#If Not MOONLIGHT Then
        Public Shared Function MsgBox(ByVal Prompt As Object, Optional ByVal Button As MsgBoxStyle = MsgBoxStyle.OkOnly, _
         Optional ByVal Title As Object = Nothing) As MsgBoxResult
#If TARGET_JVM = False Then

            Dim wf_buttons As MessageBoxButtons
            Dim wf_icon As MessageBoxIcon
            Dim wf_default As MessageBoxDefaultButton
            Dim wf_options As MessageBoxOptions

            If Title Is Nothing Then
                Title = ""
            End If
            wf_icon = MessageBoxIcon.None
            wf_options = 0

            Select Case Button And 7
                Case 0
                    wf_buttons = MessageBoxButtons.OK
                Case 1
                    wf_buttons = MessageBoxButtons.OKCancel
                Case 2
                    wf_buttons = MessageBoxButtons.AbortRetryIgnore
                Case 3
                    wf_buttons = MessageBoxButtons.YesNoCancel
                Case 4
                    wf_buttons = MessageBoxButtons.YesNo
                Case 5
                    wf_buttons = MessageBoxButtons.RetryCancel
            End Select

            If (Button And 16) = 16 Then
                wf_icon = MessageBoxIcon.Error
            ElseIf (Button And 32) = 32 Then
                wf_icon = MessageBoxIcon.Question
            ElseIf (Button And 64) = 64 Then
                wf_icon = MessageBoxIcon.Information
            End If

            If (Button And 256) = 256 Then
                wf_default = MessageBoxDefaultButton.Button2
            ElseIf (Button And 512) = 512 Then
                wf_default = MessageBoxDefaultButton.Button3
            Else
                wf_default = MessageBoxDefaultButton.Button1
            End If

            If (Button And 4096) = 4096 Then
                ' Ignore, we do not support SystemModal dialog boxes, or I cant find how to do this
            End If

            If (Button And 524288) = 524288 Then
                wf_options = MessageBoxOptions.RightAlign
            End If

            If (Button And 1048576) = 1048576 Then
                wf_options = wf_options Or MessageBoxOptions.RtlReading
            End If

            Return CType(MessageBox.Show(Prompt.ToString, Title.ToString(), wf_buttons, wf_icon, wf_default, wf_options), MsgBoxResult)
#Else
            Throw New NotImplementedException
#End If

        End Function
#End If
    End Class
End Namespace
