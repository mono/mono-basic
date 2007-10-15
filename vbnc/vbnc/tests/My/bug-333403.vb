'
' My = WindowsForms, with My.MyApplicationOnCreateForm creating an instance of WrongForm.
' -main commandline arguments sets RightForm to the initial form.

Option Strict On
Option Explicit On

Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

Partial Class WrongForm
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Environment.Exit(1)
    End Sub
End Class

Partial Class RightForm
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Environment.Exit(0)
    End Sub
End Class

Namespace My

    Partial Friend Class MyApplication

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Public Sub New()
            MyBase.New(Global.Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
            Me.IsSingleInstance = False
            Me.EnableVisualStyles = True
            Me.SaveMySettingsOnExit = True
            Me.ShutDownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = Global.WindowsApplication1.WrongForm
        End Sub
    End Class
End Namespace
