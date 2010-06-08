Imports System.Security
Imports System.Security.Permissions

Namespace SecurityAttribute1
    <FileDialogPermissionAttribute(SecurityAction.Assert)> _
    Class A

    End Class
End Namespace