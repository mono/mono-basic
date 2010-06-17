'
' FileLogTraceListener.vb
'
' Authors:
'   Rolf Bjarne Kvinge (RKvinge@novell.com>
'
' Copyright (C) 2007 Novell (http://www.novell.com)
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
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Microsoft.VisualBasic.Logging
    <ComVisible(False)> _
    Public Class FileLogTraceListener
        Inherits TraceListener

        Private m_Append As Boolean
        Private m_AutoFlush As Boolean
        Private m_BaseFileName As String
        Private m_CustomLocation As String
        Private m_Delimiter As String
        Private m_DiskSpaceExhaustedBehaviour As DiskSpaceExhaustedOption
        Private m_Encoding As System.Text.Encoding
        Private m_IncludeHostName As Boolean
        Private m_Location As LogFileLocation
        Private m_LogFileCreationSchedule As LogFileCreationScheduleOption
        Private m_MaxFileSize As Long
        Private m_ReserveDiskSpace As Long

        Private m_Stream As System.IO.StreamWriter
        Private m_SupportedAttributes As String()

        Public Sub New()
            Me.New("FileLogTraceListener")
        End Sub

        Public Sub New(ByVal name As String)
            MyBase.New(name)

            m_Append = True
            m_AutoFlush = False
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
            m_BaseFileName = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath)
            m_CustomLocation = Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData
#Else
			m_BaseFileName = System.IO.Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.SetupInformation.ApplicationName)
			m_CustomLocation = AppDomain.CurrentDomain.BaseDirectory			
#End If
            m_Delimiter = Constants.vbTab
            m_DiskSpaceExhaustedBehaviour = DiskSpaceExhaustedOption.DiscardMessages
            m_Encoding = System.Text.Encoding.UTF8
			m_IncludeHostName = False
#If TARGET_JVM Then
			m_Location = LogFileLocation.Custom
#Else
			m_Location = LogFileLocation.LocalUserApplicationDirectory
#End If
            m_LogFileCreationSchedule = LogFileCreationScheduleOption.None
            m_MaxFileSize = 5000000
            m_ReserveDiskSpace = 10000000
        End Sub

        Public Overrides Sub Close()
            MyBase.Close()
            m_Stream.Close()
            m_Stream = Nothing
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If m_Stream IsNot Nothing Then
                m_Stream.Dispose()
                m_Stream = Nothing
            End If
        End Sub

        Public Overrides Sub Flush()
            m_Stream.Flush()
            MyBase.Flush()
        End Sub

        Protected Overrides Function GetSupportedAttributes() As String()
            If m_SupportedAttributes Is Nothing Then
                m_SupportedAttributes = New String() {"append", "Append", "autoflush", "AutoFlush", "autoFlush", "basefilename", "BaseFilename", "baseFilename", "BaseFileName", "baseFileName", "customlocation", "CustomLocation", "customLocation", "delimiter", "Delimiter", "diskspaceexhaustedbehavior", "DiskSpaceExhaustedBehavior", "diskSpaceExhaustedBehavior", "encoding", "Encoding", "includehostname", "IncludeHostName", "includeHostName", "location", "Location", "logfilecreationschedule", "LogFileCreationSchedule", "logFileCreationSchedule", "maxfilesize", "MaxFileSize", "maxFileSize", "reservediskspace", "ReserveDiskSpace", "reserveDiskSpace"}
            End If
            Return m_SupportedAttributes
        End Function

        Public Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal data As Object)
            If data Is Nothing Then
                TraceEvent(eventCache, source, eventType, id, String.Empty)
            Else
                TraceEvent(eventCache, source, eventType, id, data.ToString)
            End If
        End Sub

        Public Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal ParamArray data As Object())
            If data Is Nothing Then
                TraceEvent(eventCache, source, eventType, id, String.Empty)
            Else
                TraceEvent(eventCache, source, eventType, id, Microsoft.VisualBasic.Strings.Join(data, m_Delimiter))
            End If
        End Sub


        Public Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal message As String)

            If Me.Filter IsNot Nothing AndAlso Me.Filter.ShouldTrace(eventCache, source, eventType, id, message, Nothing, Nothing, Nothing) = False Then Return

            Dim builder As New System.Text.StringBuilder()
            builder.Append(source)
            builder.Append(m_Delimiter)
            builder.Append(eventType.ToString())
            builder.Append(m_Delimiter)
            builder.Append(id.ToString)
            builder.Append(m_Delimiter)
            builder.Append(message)
            If CBool(Me.TraceOutputOptions And TraceOptions.Callstack) Then
                builder.Append(m_Delimiter)
                builder.Append(eventCache.Callstack)
            End If
            If CBool(Me.TraceOutputOptions And TraceOptions.LogicalOperationStack) Then
                builder.Append(m_Delimiter)
                builder.Append("""")
                For Each item As Object In eventCache.LogicalOperationStack
                    builder.Append(eventCache.ToString)
                Next
                builder.Append("""")
            End If
            If CBool(Me.TraceOutputOptions And TraceOptions.DateTime) Then
                builder.Append(m_Delimiter)
                builder.Append(eventCache.DateTime.ToString("u", System.Globalization.CultureInfo.InvariantCulture))
            End If
            If CBool(Me.TraceOutputOptions And TraceOptions.ProcessId) Then
                builder.Append(m_Delimiter)
                builder.Append(eventCache.ProcessId)
            End If
            If CBool(Me.TraceOutputOptions And TraceOptions.ThreadId) Then
                builder.Append(m_Delimiter)
                builder.Append(eventCache.ThreadId)
            End If
            If CBool(Me.TraceOutputOptions And TraceOptions.Timestamp) Then
                builder.Append(m_Delimiter)
                builder.Append(eventCache.Timestamp)
            End If
            If m_IncludeHostName Then
                builder.Append(m_Delimiter)
                builder.Append(Environment.MachineName)
            End If
            WriteLine(builder.ToString)
        End Sub


        Public Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal format As String, ByVal ParamArray args As Object())
            If args Is Nothing Then
                TraceEvent(eventCache, source, eventType, id, format)
            Else
                TraceEvent(eventCache, source, eventType, id, String.Format(format, args))
            End If
        End Sub


        Public Overloads Overrides Sub Write(ByVal message As String)
            Dim stream As System.IO.StreamWriter
            stream = GetOpenStream()

            If CheckSpace(message.Length) = False Then Return

            stream.Write(message)

            If m_AutoFlush Then stream.Flush()
        End Sub

        Public Overloads Overrides Sub WriteLine(ByVal message As String)
            Dim stream As System.IO.StreamWriter
            stream = GetOpenStream()

            If CheckSpace(message.Length + System.Environment.NewLine.Length) = False Then Return

            stream.WriteLine(message)

            If m_AutoFlush Then stream.Flush()
        End Sub

        Private Function CheckSpace(ByVal msgSize As Integer) As Boolean
            If m_Stream.BaseStream.Length + msgSize > m_MaxFileSize Then
                If m_DiskSpaceExhaustedBehaviour = DiskSpaceExhaustedOption.ThrowException Then
                    Throw New InvalidOperationException("Log file size exceeded maximum size")
                Else
                    Return False
                End If
            End If
#if Not TARGET_JVM
            If New System.IO.DriveInfo(Me.FullLogFileName(0).ToString()).TotalFreeSpace - msgSize < m_ReserveDiskSpace Then
                If m_DiskSpaceExhaustedBehaviour = DiskSpaceExhaustedOption.ThrowException Then
                    Throw New InvalidOperationException("No more disk space for log file")
                Else
                    Return False
                End If
            End If
#End If

            Return True
        End Function

        Private Function GetOpenStream() As System.IO.StreamWriter
            If m_Stream Is Nothing Then
                m_Stream = New System.IO.StreamWriter(FullLogFileName, m_Append, m_Encoding)
            End If
            Return m_Stream
        End Function

#Region "Properties"
        Public Property Append() As Boolean
            Get
                Return m_Append
            End Get
            Set(ByVal value As Boolean)
                m_Append = value
            End Set
        End Property

        Public Property AutoFlush() As Boolean
            Get
                Return m_AutoFlush
            End Get
            Set(ByVal value As Boolean)
                m_AutoFlush = value
            End Set
        End Property

        Public Property BaseFileName() As String
            Get
                Return m_BaseFileName
            End Get
            Set(ByVal value As String)
                m_BaseFileName = value
            End Set
        End Property

        Public Property CustomLocation() As String
            Get
                Return m_CustomLocation
            End Get
            Set(ByVal value As String)
                m_CustomLocation = value
            End Set
        End Property

        Public Property Delimiter() As String
            Get
                Return m_Delimiter
            End Get
            Set(ByVal value As String)
                m_Delimiter = value
            End Set
        End Property

        Public Property DiskSpaceExhaustedBehavior() As DiskSpaceExhaustedOption
            Get
                Return m_DiskSpaceExhaustedBehaviour
            End Get
            Set(ByVal value As DiskSpaceExhaustedOption)
                m_DiskSpaceExhaustedBehaviour = value
            End Set
        End Property

        Public Property Encoding() As Encoding
            Get
                Return m_Encoding
            End Get
            Set(ByVal value As Encoding)
                m_Encoding = value
            End Set
        End Property

        Public ReadOnly Property FullLogFileName() As String
            Get
                Dim path As String
                Dim file As String
#If TARGET_JVM = False Then 'Windows.Forms Not Supported by Grasshopper
                Select Case m_Location
                    Case LogFileLocation.CommonApplicationDirectory
                        path = System.Windows.Forms.Application.CommonAppDataPath
                    Case LogFileLocation.Custom
                        path = m_CustomLocation
                    Case LogFileLocation.ExecutableDirectory
                        path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath)
                    Case LogFileLocation.LocalUserApplicationDirectory
                        path = Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData
                    Case LogFileLocation.TempDirectory
                        path = Microsoft.VisualBasic.FileIO.SpecialDirectories.Temp
                    Case Else
                        path = Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData
                End Select
#Else
				Select Case m_Location                   
                    Case LogFileLocation.Custom
                        path = m_CustomLocation
                    Case LogFileLocation.ExecutableDirectory
                        path = System.AppDomain.CurrentDomain.BaseDirectory
                    Case Else
                        throw new NotSupportedException("LogFileLocation must be one of: Custom, ExecutableDirectory")
                End Select
#End If

                file = m_BaseFileName
                Select Case m_LogFileCreationSchedule
                    Case LogFileCreationScheduleOption.Daily
                        file = file & "-" & Date.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    Case LogFileCreationScheduleOption.None
                        'nothing here
                    Case LogFileCreationScheduleOption.Weekly
                        Dim today As Date = Date.Today
                        'This is not culture-dependant, here Sunday is always the first day of the week
                        file = file & "-" & today.AddDays(-today.DayOfWeek).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    Case Else
                        'nothing here
                End Select

                Return System.IO.Path.Combine(path, file) & ".log"
            End Get
        End Property

        Public Property IncludeHostName() As Boolean
            Get
                Return m_IncludeHostName
            End Get
            Set(ByVal value As Boolean)
                m_IncludeHostName = value
            End Set
        End Property

        Public Property Location() As LogFileLocation
            Get
                Return m_Location
            End Get
			Set(ByVal value As LogFileLocation)
#If TARGET_JVM Then
				if (value <> LogFileLocation.Custom And value <> LogFileLocation.ExecutableDirectory) Then
					Throw New NotSupportedException("LogFileLocation must be one of: Custom, ExecutableDirectory")
				End If
#End If
				m_Location = value
			End Set
		End Property

        Public Property LogFileCreationSchedule() As LogFileCreationScheduleOption
            Get
                Return m_LogFileCreationSchedule
            End Get
            Set(ByVal value As LogFileCreationScheduleOption)
                m_LogFileCreationSchedule = value
            End Set
        End Property

        Public Property MaxFileSize() As Long
            Get
                Return m_MaxFileSize
            End Get
            Set(ByVal value As Long)
                If value < 1000 Then
                    Throw New ArgumentException("MaxFileSize has to be greater or equal to 1000")
                End If

                m_MaxFileSize = value
            End Set
        End Property

        Public Property ReserveDiskSpace() As Long
            Get
                Return m_ReserveDiskSpace
            End Get
            Set(ByVal value As Long)
                m_ReserveDiskSpace = value
            End Set
        End Property
#End Region
    End Class

End Namespace
