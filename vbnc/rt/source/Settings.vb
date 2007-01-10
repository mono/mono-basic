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

Namespace My
    
    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings

        Private Sub MySettings_SettingsLoaded(ByVal sender As Object, ByVal e As System.Configuration.SettingsLoadedEventArgs) Handles Me.SettingsLoaded
            Debug.WriteLine("******************* Settings loaded *******************************")
            Dim s As MySettings = TryCast(sender, MySettings)
            For Each value As Configuration.SettingsPropertyValue In s.PropertyValues
                Debug.WriteLine(value.Name & "=" & value.PropertyValue.ToString)
            Next
            Debug.WriteLine("*******************************************************************")
        End Sub

        Private Sub MySettings_SettingsSaving(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.SettingsSaving
            Debug.WriteLine("******************* Settings saved ********************************")
            Dim s As MySettings = TryCast(sender, MySettings)
            For Each value As Configuration.SettingsPropertyValue In s.PropertyValues
                Debug.WriteLine(value.Name & "=" & value.PropertyValue.ToString)
            Next
            Debug.WriteLine("*******************************************************************")
        End Sub
    End Class
End Namespace
