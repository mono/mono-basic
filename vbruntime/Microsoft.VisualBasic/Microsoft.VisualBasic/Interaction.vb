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
Imports Microsoft.Win32
Imports System.Windows.Forms

Namespace Microsoft.VisualBasic
    Public Module Interaction

        Public Sub AppActivate(ByVal ProcessId As Integer)
            'TODO: OS Specific
            Throw New NotImplementedException
        End Sub
        Public Sub AppActivate(ByVal Title As String)
            'TODO: OS Specific
            Throw New NotImplementedException
        End Sub
        Public Sub Beep()
            'TODO: OS Specific
            ' Removed Throw exception, as it does not really harm that the beep does not work.
        End Sub
        Public Function CallByName(ByVal ObjectRef As Object, ByVal ProcName As String, ByVal UseCallType As Microsoft.VisualBasic.CallType, ByVal ParamArray Args() As Object) As Object
            'TODO: Call LateBinding
            Throw New NotImplementedException
        End Function
        Public Function Choose(ByVal Index As Double, ByVal ParamArray Choice() As Object) As Object

            'FIXME: why Index is Double, while an Index of an Array is Integer ?
            Dim IntIndex As Integer
            IntIndex = Convert.ToInt32(Index)

            'TODO: add the exception message.
            If (Choice.Rank <> 1) Then
                Throw New ArgumentException
            End If


            If ((IntIndex >= 0) And (IntIndex <= Information.UBound(Choice))) Then
                Return Choice(IntIndex)
            Else
                'TODO: add the exception message.
                Throw New ArgumentOutOfRangeException
            End If
        End Function
        Public Function Command() As String
            'TODO: OS Specific
            Return String.Join(" ", Environment.GetCommandLineArgs)
        End Function
        Public Function CreateObject(ByVal ProgId As String, Optional ByVal ServerName As String = "") As Object
            'TODO: COM
            Throw New NotImplementedException
        End Function
        Public Sub DeleteSetting(ByVal AppName As String, Optional ByVal Section As String = Nothing, Optional ByVal Key As String = Nothing)
            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            If Section = Nothing Then
                rkey.DeleteSubKeyTree(AppName)
            Else
                If Key = Nothing Then
                    rkey.DeleteSubKeyTree(Section)
                Else
                    rkey = rkey.OpenSubKey(Section)
                    rkey.DeleteValue(Key)
                End If
            End If

            'Closes the key and flushes it to disk if the contents have been modified.
            rkey.Close()

        End Sub
        Public Function Environ(ByVal Expression As Integer) As String
            Throw New NotImplementedException
        End Function
        Public Function Environ(ByVal Expression As String) As String
            Return Environment.GetEnvironmentVariable(Expression)
        End Function
        Public Function GetAllSettings(ByVal AppName As String, ByVal Section As String) As String(,)
            'TODO: Registry
            Throw New NotImplementedException
        End Function
        Public Function GetObject(Optional ByVal PathName As String = Nothing, Optional ByVal [Class] As String = Nothing) As Object
            'TODO: COM
            Throw New NotImplementedException
        End Function
        Public Function GetSetting(ByVal AppName As String, ByVal Section As String, ByVal Key As String, Optional ByVal [Default] As String = "") As String
            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            rkey = rkey.OpenSubKey(AppName)
            rkey = rkey.OpenSubKey(Section)
            Return rkey.GetValue(Key, CObj([Default])).ToString
        End Function
        Public Function IIf(ByVal Expression As Boolean, ByVal TruePart As Object, ByVal FalsePart As Object) As Object
            If Expression Then
                Return TruePart
            Else
                Return FalsePart
            End If
        End Function
        Public Function InputBox(ByVal Prompt As String, Optional ByVal Title As String = "", Optional ByVal DefaultResponse As String = "", Optional ByVal XPos As Integer = -1, Optional ByVal YPos As Integer = -1) As String
            Dim form As Form

            form.Text = Title
            Return ""
        End Function
        Public Function Partition(ByVal Number As Long, ByVal Start As Long, ByVal [Stop] As Long, ByVal Interval As Long) As String
            'TODO: pure algorithm, Not OS specific.
            Throw New NotImplementedException
        End Function
        Public Sub SaveSetting(ByVal AppName As String, ByVal Section As String, ByVal Key As String, ByVal Setting As String)
            Dim rkey As RegistryKey
            rkey = Registry.CurrentUser
            rkey = rkey.OpenSubKey(AppName)
            rkey = rkey.OpenSubKey(Section)
            rkey.SetValue(Key, Setting)
            'Closes the key and flushes it to disk if the contents have been modified.
            rkey.Close()
        End Sub
        Public Function Shell(ByVal Pathname As String, Optional ByVal Style As Microsoft.VisualBasic.AppWinStyle = Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, Optional ByVal Wait As Boolean = False, Optional ByVal Timeout As Integer = -1) As Integer
            'TODO: OS Specific
            Throw New NotImplementedException
        End Function
        Public Function Switch(ByVal ParamArray VarExpr() As Object) As Object
            Dim i As Integer
            If VarExpr Is Nothing Then
                Return Nothing
            End If

            If Not (VarExpr.Length Mod 2 = 0) Then
                Throw New System.ArgumentException("Argument 'VarExpr' is not a valid value.")
            End If
            For i = 0 To VarExpr.Length Step 2
                If CBool(VarExpr(i)) Then Return VarExpr(i + 1)
            Next
            Return Nothing
        End Function
    End Module
End Namespace
