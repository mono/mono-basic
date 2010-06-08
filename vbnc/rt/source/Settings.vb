' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
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
                Return GetSetting("txtVBCCompiler_Text2", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtVBCCompiler_Text2", value)
            End Set
        End Property
        Shared Property txtVBNCCompiler_Text() As String
            Get
                Return GetSetting("txtVBNCCompiler_Text2", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtVBNCCompiler_Text2", value)
            End Set
        End Property
        Shared Property txtBasePath_Text() As String
            Get
                Return GetSetting("txtBasePath_Text2", "")
            End Get
            Set(ByVal value As String)
                SaveSetting("txtBasePath_Text2", value)
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
        Shared Property TestsListView_colKnownFailureReason_Width() As Integer
            Get
                Return CInt(GetSetting("TestsListView_colKnownFailureReason_Width", 80))
            End Get
            Set(ByVal value As Integer)
                SaveSetting("TestsListView_colKnownFailureReason_Width", value)
            End Set
        End Property

        Shared Function GetSetting(ByVal Name As String, ByVal DefaultValue As Object) As String
            Return Microsoft.VisualBasic.GetSetting(System.Windows.Forms.Application.ProductName, "Settings", Name, CStr(DefaultValue))
        End Function
        Shared Sub SaveSetting(ByVal Name As String, ByVal Value As Object)
            Microsoft.VisualBasic.SaveSetting(System.Windows.Forms.Application.ProductName, "Settings", Name, CStr(Value))
        End Sub
    End Class

End Namespace
