Imports system.collections
Public Class NameResolution1_ListBox
    Sub Test()
    End Sub
    Public Class NestedListBox
    End Class
End Class
Public Module NameResolution1_Test002
    Sub Test()
        Dim box As NameResolution1_listbox 'ok
        Dim nbox As NameResolution1_listbox.NestedListBox 'ok
        'dim obox as ListBox.ObjectCollection 'does not compile in v1.1 (ref: system.windows.forms.listbox.objectcollection)
    End Sub
End Module