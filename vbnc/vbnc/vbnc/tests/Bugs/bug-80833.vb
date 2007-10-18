Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.Resources
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

''' -----------------------------------------------------------------------------
''' Project	 : XPCommonControls
''' Class	 : XPCommonControls.CtrlHelper
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' this is a collection of shared methods to help drawing the controls
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Mike]	25.04.2004	Created
''' </history>
''' -----------------------------------------------------------------------------
Friend Class CtrlHelper
    Private Const WM_THEMECHANGED As Integer = &H31A

    'application is insecure if you don't add the link demand to this sub
    'see FxCop for details: http://www.gotdotnet.com/team/fxcop/docs/rules/SecurityRules/VirtualMethodsAndOverrides.html
    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
    Protected Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_THEMECHANGED
        End Select
    End Sub

End Class
