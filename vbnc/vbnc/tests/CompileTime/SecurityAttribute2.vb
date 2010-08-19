Imports System.Security
Imports System.Security.Permissions

Public Module SecurityAttribute2

    <HostProtection(SecurityAction.LinkDemand, Resources:=HostProtectionResource.SelfAffectingProcessMgmt)> _
    <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
    Public Sub EndApp()
    End Sub

End Module