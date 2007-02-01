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

<System.ComponentModel.TypeConverter(GetType(System.ComponentModel.ExpandableObjectConverter))> _
<Serializable()> _
Public Class TestStatistics
    Private m_HandleCount As Integer
    Public Modules As Integer
    Public NonpagedSystemMemorySize64 As Long
    Public PagedMemorySize64 As Long
    Public PeakPagedMemorySize64 As Long
    Public PeakVirtualMemorySize64 As Long
    Public PeakWorkingSet64 As Long
    Public PrivateMemorySize64 As Long
    Public PrivilegedProcessorTime As TimeSpan
    Public Threads As Integer
    Public TotalProcessorTime As TimeSpan
    Public UserProcessorTime As TimeSpan
    Public VirtualMemorySize64 As Long
    Public WorkingSet64 As Long

    Private m_Started As DateTime

    ReadOnly Property HandleCount() As Integer
        Get
            Return m_HandleCount
        End Get
    End Property

    Sub WriteToXML(ByVal xml As Xml.XmlWriter)
        xml.WriteStartElement(Me.GetType.ToString)
        xml.WriteStartElement("HandleCount", HandleCount.ToString)
        xml.WriteStartElement("Modules", Modules.ToString)
        xml.WriteStartElement("NonpagedSystemMemorySize64", NonpagedSystemMemorySize64.ToString)
        xml.WriteStartElement("PagedMemorySize64", PagedMemorySize64.ToString)
        xml.WriteStartElement("PeakPagedMemorySize64", PeakPagedMemorySize64.ToString)
        xml.WriteStartElement("PeakVirtualMemorySize64", PeakVirtualMemorySize64.ToString)
        xml.WriteStartElement("PeakWorkingSet64", PeakWorkingSet64.ToString)
        xml.WriteStartElement("PrivateMemorySize64", PrivateMemorySize64.ToString)
        xml.WriteStartElement("PrivilegedProcessorTime", PrivateMemorySize64.ToString)
        xml.WriteStartElement("Threads", Threads.ToString)
        xml.WriteStartElement("TotalProcessorTime", TotalProcessorTime.ToString)
        xml.WriteStartElement("UserProcessorTime", UserProcessorTime.ToString)
        xml.WriteStartElement("VirtualMemorySize64", VirtualMemorySize64.ToString)
        xml.WriteStartElement("WorkingSet64", WorkingSet64.ToString)
        xml.WriteEndElement()
    End Sub

    Sub StartTimer()
        m_Started = Now
    End Sub

    Sub StopTimer()
        TotalProcessorTime = Now - m_Started
        UserProcessorTime = TotalProcessorTime
        m_Started = Nothing
    End Sub

    Sub New()
        '
    End Sub

    Sub New(ByVal Process As Process)
        If Process.HasExited = False Then
            m_HandleCount = Process.HandleCount
            Me.Modules = Process.Modules.Count
            Me.NonpagedSystemMemorySize64 = Process.NonpagedSystemMemorySize64
            Me.PagedMemorySize64 = Process.PagedMemorySize64
            Me.PeakPagedMemorySize64 = Process.PeakPagedMemorySize64
            Me.PeakVirtualMemorySize64 = Process.PeakVirtualMemorySize64
            Me.PeakWorkingSet64 = Process.PeakWorkingSet64
            'Me.PrivateMemorySize64 = Process.PrivateMemorySize64
            Me.Threads = Process.Threads.Count
            Me.VirtualMemorySize64 = Process.VirtualMemorySize64
            Me.WorkingSet64 = Process.WorkingSet64
        End If
        Me.PrivilegedProcessorTime = Process.PrivilegedProcessorTime
        Me.TotalProcessorTime = Process.TotalProcessorTime
        Me.UserProcessorTime = Process.UserProcessorTime
    End Sub
End Class
