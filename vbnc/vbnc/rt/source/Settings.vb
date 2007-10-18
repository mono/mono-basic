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
    Partial Friend NotInheritable Class Settings
        Shared Property IsFirstRun() As Boolean
            Get
                Return CBool(GetSetting("IsFirstRun", False))
            End Get
            Set(ByVal value As Boolean)
                SaveSetting("IsFirstRun", value)
            End Set
        End Property
        Shared Property ContinuousTest() As Boolean
            Get
                Return CBool(GetSetting("ContinuousTest", False))
            End Get
            Set(ByVal value As Boolean)
                SaveSetting("ContinuousTest", value)
            End Set
        End Property
        Shared Property HostedTest() As Boolean
            Get
                Return CBool(GetSetting("HostedTest", False))
            End Get
            Set(ByVal value As Boolean)
                SaveSetting("HostedTest", value)
            End Set
        End Property
        Shared Property DontTestIfNothingHasChanged() As Boolean
            Get
                Return CBool(GetSetting("DontTestIfNothingHasChanged", False))
            End Get
            Set(ByVal value As Boolean)
                SaveSetting("DontTestIfNothingHasChanged", value)
            End Set
        End Property
        Shared Property ModifyRegistry() As String
            Get
                Return GetSetting("ModifyRegistry", "N")
            End Get
            Set(ByVal value As String)
                SaveSetting("ModifyRegistry", value)
            End Set
        End Property
        Shared Property txtVBCCompiler_Text() As String
            Get
                Return GetSetting("txtVBCCompiler_Text", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtVBCCompiler_Text", value)
            End Set
        End Property
        Shared Property txtVBNCCompiler_Text() As String
            Get
                Return GetSetting("txtVBNCCompiler_Text", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtVBNCCompiler_Text", value)
            End Set
        End Property
        Shared Property txtBasePath_Text() As String
            Get
                Return GetSetting("txtBasePath_Text", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtBasePath_Text", value)
            End Set
        End Property
        Shared Property TestsListView_colPath_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colPath_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colPath_Width", value)
            End Set
        End Property
        Shared Property TestsListView_colName_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colName_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colName_Width", value)
            End Set
        End Property
        Shared Property TestsListView_colCompiler_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colCompiler_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colCompiler_Width", value)
            End Set
        End Property
        Shared Property TestsListView_colResult_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colResult_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colResult_Width", value)
            End Set
        End Property
        Shared Property TestsListView_colFailedVerification_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colFailedVerification_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colFailedVerification_Width", value)
            End Set
        End Property
        Shared Property TestsListView_colDate_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colDate_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colDate_Width", value)
            End Set
        End Property
        Shared Sub Upgrade()

        End Sub
        Shared Sub Save()

        End Sub
        Shared Function GetSetting(ByVal Name As String, ByVal DefaultValue As Object) As String
            Return Microsoft.VisualBasic.GetSetting(System.Windows.Forms.Application.ProductName, "Settings", Name, CStr(DefaultValue))
        End Function
        Shared Sub SaveSetting(ByVal Name As String, ByVal Value As Object)
            Microsoft.VisualBasic.SaveSetting(System.Windows.Forms.Application.ProductName, "Settings", Name, CStr(Value))
        End Sub
    End Class

End Namespace
