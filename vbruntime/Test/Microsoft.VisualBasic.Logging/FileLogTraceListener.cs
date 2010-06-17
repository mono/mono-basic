// FileLogTraceListenerTest.cs - NUnit Test Cases for Microsoft.VisualBasic.Logging.FileLogTraceListener
//
// Rolf Bjarne Kvinge  (RKvinge@novell.com)
//
// 
// Copyright (C) 2007 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#if !TARGET_JVM //TargetJvmNotSupported #8857
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.VisualBasic.Logging;
using System.Globalization;

namespace MonoTests.Microsoft_VisualBasic.Logging
{
	[TestFixture]
	public class FileLogTraceListenerTest
	{
		[Test]
		public void SupportedAttributesTest ()
		{
			Derived derived = new Derived ();
			Assert.AreEqual (Microsoft.VisualBasic.Strings.Join (derived.GetAttribs (), ";"), "append;Append;autoflush;AutoFlush;autoFlush;basefilename;BaseFilename;baseFilename;BaseFileName;baseFileName;customlocation;CustomLocation;customLocation;delimiter;Delimiter;diskspaceexhaustedbehavior;DiskSpaceExhaustedBehavior;diskSpaceExhaustedBehavior;encoding;Encoding;includehostname;IncludeHostName;includeHostName;location;Location;logfilecreationschedule;LogFileCreationSchedule;logFileCreationSchedule;maxfilesize;MaxFileSize;maxFileSize;reservediskspace;ReserveDiskSpace;reserveDiskSpace", "#01");
		}
		
		[Test]
		public void DefaultPropertiesTest ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				Assert.AreEqual (true, log.Append, "A1");
				Assert.AreEqual (false, log.AutoFlush, "A2");
				Assert.AreEqual (System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath), log.BaseFileName, "B1");
				Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.CustomLocation, "C1");
				Assert.AreEqual ("\t", log.Delimiter, "D1");
				Assert.AreEqual (DiskSpaceExhaustedOption.DiscardMessages, log.DiskSpaceExhaustedBehavior, "D2");
				Assert.AreEqual (System.Text.Encoding.UTF8.EncodingName, log.Encoding.EncodingName, "E1");
				Assert.AreEqual (System.IO.Path.Combine(Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + ".log", log.FullLogFileName, "F1");
				Assert.IsNull	(log.Filter, "F2");
				Assert.AreEqual (false, log.IncludeHostName, "I1");
				Assert.AreEqual (LogFileLocation.LocalUserApplicationDirectory, log.Location, "L1");
				Assert.AreEqual (LogFileCreationScheduleOption.None, log.LogFileCreationSchedule, "L2");
				Assert.AreEqual (5000000, log.MaxFileSize, "M1");
				Assert.AreEqual ("FileLogTraceListener", log.Name, "N1");
				Assert.AreEqual (10000000, log.ReserveDiskSpace, "R1");
			}
		}
		
		[Test]
		public void FilenameTest ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.LogFileCreationSchedule = LogFileCreationScheduleOption.None;
				log.Location = LogFileLocation.CommonApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine(Microsoft.VisualBasic.FileIO.SpecialDirectories.AllUsersApplicationData, log.BaseFileName) + ".log", log.FullLogFileName, "#A1");

				log.Location = LogFileLocation.ExecutableDirectory;
				Assert.AreEqual (System.IO.Path.Combine (System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), log.BaseFileName) + ".log", log.FullLogFileName, "#A2");

				log.Location = LogFileLocation.LocalUserApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + ".log", log.FullLogFileName, "#A3");

				log.Location = LogFileLocation.TempDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.Temp, log.BaseFileName) + ".log", log.FullLogFileName, "#A4");

				log.Location = LogFileLocation.Custom;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + ".log", log.FullLogFileName, "#A5");
				
				log.CustomLocation = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments, log.BaseFileName) + ".log", log.FullLogFileName, "#A6");
			}

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.LogFileCreationSchedule = LogFileCreationScheduleOption.Daily;
				log.Location = LogFileLocation.CommonApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.AllUsersApplicationData, log.BaseFileName) + DateTime.Now.ToString("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B1");

				log.Location = LogFileLocation.ExecutableDirectory;
				Assert.AreEqual (System.IO.Path.Combine (System.IO.Path.GetDirectoryName (System.Windows.Forms.Application.ExecutablePath), log.BaseFileName) + DateTime.Now.ToString ("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B2");

				log.Location = LogFileLocation.LocalUserApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + DateTime.Now.ToString ("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B3");

				log.Location = LogFileLocation.TempDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.Temp, log.BaseFileName) + DateTime.Now.ToString ("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B4");

				log.Location = LogFileLocation.Custom;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + DateTime.Now.ToString ("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B5");

				log.CustomLocation = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments, log.BaseFileName) + DateTime.Now.ToString ("-yyyy-MM-dd") + ".log", log.FullLogFileName, "#B6");
			}

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				DateTime dt = DateTime.Today.AddDays (-(int)DateTime.Today.DayOfWeek);
				string format = dt.ToString ("-yyyy-MM-dd");
				log.LogFileCreationSchedule = LogFileCreationScheduleOption.Weekly;
				log.Location = LogFileLocation.CommonApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.AllUsersApplicationData, log.BaseFileName) + format + ".log", log.FullLogFileName, "#C1");

				log.Location = LogFileLocation.ExecutableDirectory;
				Assert.AreEqual (System.IO.Path.Combine (System.IO.Path.GetDirectoryName (System.Windows.Forms.Application.ExecutablePath), log.BaseFileName) + format + ".log", log.FullLogFileName, "#C2");

				log.Location = LogFileLocation.LocalUserApplicationDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + format + ".log", log.FullLogFileName, "#C3");

				log.Location = LogFileLocation.TempDirectory;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.Temp, log.BaseFileName) + format + ".log", log.FullLogFileName, "#C4");

				log.Location = LogFileLocation.Custom;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, log.BaseFileName) + format + ".log", log.FullLogFileName, "#C5");

				log.CustomLocation = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments;
				Assert.AreEqual (System.IO.Path.Combine (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments, log.BaseFileName) + format + ".log", log.FullLogFileName, "#C6");
			}
		}
		
		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void DiskSpaceTest1 ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Location = LogFileLocation.TempDirectory;
				log.ReserveDiskSpace = new System.IO.DriveInfo (log.FullLogFileName [0].ToString ()).TotalFreeSpace;
				log.DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.ThrowException;
				log.WriteLine ("TestLine");	
			}
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void DiskSpaceTest2 ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.MaxFileSize = 0;
			}
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void DiskSpaceTest3 ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Location = LogFileLocation.TempDirectory;
				log.MaxFileSize = 1000;
				log.DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.ThrowException;
				log.Write (new string('z', 1001));
			}
		}
		
		[Test]
		public void WriteTest ()
		{
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();
				
				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);
				
				Assert.AreEqual ("nunit\tCritical\t0\t\r\n", data, "#01");
			}


			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, "data");
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);

				Assert.AreEqual ("nunit\tCritical\t0\tdata\r\n", data, "#02");
			}

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, "data", "data2");
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);

				Assert.AreEqual ("nunit\tCritical\t0\tdata\tdata2\r\n", data, "#03");
			}

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceEvent (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, "msg");
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);

				Assert.AreEqual ("nunit\tCritical\t0\tmsg\r\n", data, "#04");
			}


			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceEvent (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, "msg:{0}", "arg1");
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);

				Assert.AreEqual ("nunit\tCritical\t0\tmsg:arg1\r\n", data, "#05");
			}
			
			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();
				
				log.TraceOutputOptions = System.Diagnostics.TraceOptions.DateTime | System.Diagnostics.TraceOptions.LogicalOperationStack | System.Diagnostics.TraceOptions.ProcessId | System.Diagnostics.TraceOptions.ThreadId | System.Diagnostics.TraceOptions.Timestamp;
				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);
				
				Assert.AreEqual ("nunit\tCritical\t0\t\t\"\"\t" + cache.DateTime.ToString ("u", CultureInfo.InvariantCulture) + "\t" + cache.ProcessId + "\t" + cache.ThreadId + "\t" + cache.Timestamp + System.Environment.NewLine, data, "#06");
			}


			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				log.IncludeHostName = true;
				
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceOutputOptions = System.Diagnostics.TraceOptions.DateTime | System.Diagnostics.TraceOptions.LogicalOperationStack | System.Diagnostics.TraceOptions.ProcessId | System.Diagnostics.TraceOptions.ThreadId | System.Diagnostics.TraceOptions.Timestamp;
				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);

				Assert.AreEqual ("nunit\tCritical\t0\t\t\"\"\t" + cache.DateTime.ToString ("u", CultureInfo.InvariantCulture) + "\t" + cache.ProcessId + "\t" + cache.ThreadId + "\t" + cache.Timestamp + "\t" + Environment.MachineName + System.Environment.NewLine, data, "#07");
			}
		}
		
		[Test]
		public void AppendTest ()
		{

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				log.Append = false;
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);

				Assert.AreEqual ("nunit\tCritical\t0\t\r\n", data, "#01");	
			}


			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				log.Append = true;
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);

				Assert.AreEqual ("nunit\tCritical\t0\t\r\n" + "nunit\tCritical\t0\t\r\n", data, "#02");
			}

			using (FileLogTraceListener log = new FileLogTraceListener ()) {
				log.Filter = new System.Diagnostics.EventTypeFilter (System.Diagnostics.SourceLevels.All);
				log.Append = false;
				string filename = log.FullLogFileName;
				string data;
				System.Diagnostics.TraceEventCache cache = new System.Diagnostics.TraceEventCache ();

				log.TraceData (cache, "nunit", System.Diagnostics.TraceEventType.Critical, 0, null);
				log.Close ();
				data = Microsoft.VisualBasic.FileIO.FileSystem.ReadAllText (filename);

				Assert.AreEqual ("nunit\tCritical\t0\t\r\n", data, "#03");
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (filename);
			}
			
			
		}
		
	}
	
	class Derived : FileLogTraceListener
	{
		protected override string [] GetSupportedAttributes ()
		{
			return base.GetSupportedAttributes ();
		}
		
		public string [] GetAttribs ()
		{
			return GetSupportedAttributes ();
		}
	}
}
#endif