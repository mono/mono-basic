// LogTest.cs - NUnit Test Cases for Microsoft.VisualBasic.Logging.Log
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
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.VisualBasic.Logging;

namespace MonoTests.Microsoft_VisualBasic.Logging
{
	[TestFixture]
	public class LoggingTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void TestWriteExNull1 ()
		{
			Log log = new Log ();
			log.WriteException (null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void TestWriteExNull2 ()
		{
			Log log = new Log ();
			log.WriteException (null, System.Diagnostics.TraceEventType.Information, "");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void TestWriteExNull3 ()
		{
			Log log = new Log ();
			log.WriteException (null, System.Diagnostics.TraceEventType.Information, "", 0);
		}

		[Test]
		public void TestWriteEx1 ()
		{
			Log log = new Log ();
			string em = "ExceptionMessage";
			string ai = "AdditionalInformation";
			Exception ex = new Exception (em);
			System.IO.StringWriter writer = new System.IO.StringWriter();
			
			log.TraceSource.Listeners.Clear();
			log.TraceSource.Listeners.Add (new System.Diagnostics.TextWriterTraceListener (writer));
			log.TraceSource.Switch.Level = System.Diagnostics.SourceLevels.All;

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Information, "");
			Assert.AreEqual ("DefaultSource Information: 0 : ExceptionMessage" + System.Environment.NewLine, writer.ToString (), "#01");
			
			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Error, ai);
			Assert.AreEqual (string.Format("DefaultSource Error: 2 : {0} {1}" + System.Environment.NewLine, em, ai), writer.ToString (), "#02");
			
			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Warning, ai, 200);
			Assert.AreEqual (string.Format ("DefaultSource Warning: 200 : {0} {1}" + System.Environment.NewLine, em, ai), writer.ToString (), "#03");
			
			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex);
			Assert.AreEqual (string.Format ("DefaultSource Error: 2 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#04");



			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Critical, null);
			Assert.AreEqual (string.Format ("DefaultSource Critical: 3 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#10");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Error, null);
			Assert.AreEqual (string.Format ("DefaultSource Error: 2 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#11");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Information, null);
			Assert.AreEqual (string.Format ("DefaultSource Information: 0 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#12");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Resume, null);
			Assert.AreEqual (string.Format ("DefaultSource Resume: 7 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#13");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Start, null);
			Assert.AreEqual (string.Format ("DefaultSource Start: 4 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#14");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Stop, null);
			Assert.AreEqual (string.Format ("DefaultSource Stop: 5 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#15");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Suspend, null);
			Assert.AreEqual (string.Format ("DefaultSource Suspend: 6 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#16");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Transfer, null);
			Assert.AreEqual (string.Format ("DefaultSource Transfer: 9 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#17");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Verbose, null);
			Assert.AreEqual (string.Format ("DefaultSource Verbose: 8 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#18");

			writer.GetStringBuilder ().Length = 0;
			log.WriteException (ex, System.Diagnostics.TraceEventType.Warning, null);
			Assert.AreEqual (string.Format ("DefaultSource Warning: 1 : {0}" + System.Environment.NewLine, em, ai), writer.ToString (), "#19");
		}


		[Test]
		public void TestWriteEntry1 ()
		{
			Log log = new Log ();
			string msg = "AdditionalInformation";
			
			System.IO.StringWriter writer = new System.IO.StringWriter ();

			log.TraceSource.Listeners.Clear ();
			log.TraceSource.Listeners.Add (new System.Diagnostics.TextWriterTraceListener (writer));
			log.TraceSource.Switch.Level = System.Diagnostics.SourceLevels.All;

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg);
			Assert.AreEqual ("DefaultSource Information: 0 : AdditionalInformation" + System.Environment.NewLine, writer.ToString (), "#01");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Error);
			Assert.AreEqual (string.Format ("DefaultSource Error: 2 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#02");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Warning, 200);
			Assert.AreEqual (string.Format ("DefaultSource Warning: 200 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#03");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Suspend);
			Assert.AreEqual (string.Format ("DefaultSource Suspend: 6 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#04");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (null);
			Assert.AreEqual (string.Format ("DefaultSource Information: 0 : " + System.Environment.NewLine), writer.ToString (), "#05");	
					

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Critical);
			Assert.AreEqual (string.Format ("DefaultSource Critical: 3 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#10");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Error);
			Assert.AreEqual (string.Format ("DefaultSource Error: 2 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#11");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Information);
			Assert.AreEqual (string.Format ("DefaultSource Information: 0 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#12");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Resume);
			Assert.AreEqual (string.Format ("DefaultSource Resume: 7 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#13");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Start);
			Assert.AreEqual (string.Format ("DefaultSource Start: 4 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#14");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Stop);
			Assert.AreEqual (string.Format ("DefaultSource Stop: 5 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#15");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Suspend);
			Assert.AreEqual (string.Format ("DefaultSource Suspend: 6 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#16");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Transfer);
			Assert.AreEqual (string.Format ("DefaultSource Transfer: 9 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#17");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Verbose);
			Assert.AreEqual (string.Format ("DefaultSource Verbose: 8 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#18");

			writer.GetStringBuilder ().Length = 0;
			log.WriteEntry (msg, System.Diagnostics.TraceEventType.Warning);
			Assert.AreEqual (string.Format ("DefaultSource Warning: 1 : {0}" + System.Environment.NewLine, msg), writer.ToString (), "#19");
		}
		
		[Test]
		public void TestDefaults ()
		{
			Log log = new Log ();
			Assert.AreEqual ("FileLog", log.DefaultFileLogWriter.Name, "#01");
			Assert.AreEqual ("DefaultSource", log.TraceSource.Name, "#02");
			Assert.AreEqual (2, log.TraceSource.Listeners.Count, "#03");
			Assert.AreEqual ("Default", getObjects (log.TraceSource.Listeners) [0].Name, "#04");
			Assert.AreEqual ("FileLog", getObjects (log.TraceSource.Listeners) [1].Name, "#05");
			Assert.AreEqual ("DefaultTraceListener", getObjects (log.TraceSource.Listeners) [0].GetType ().Name, "#06");
			Assert.AreEqual ("FileLogTraceListener", getObjects (log.TraceSource.Listeners) [1].GetType ().Name, "#07");
			Assert.AreEqual (System.Diagnostics.SourceLevels.Information, log.TraceSource.Switch.Level, "#08");
		}
		
		private System.Diagnostics.TraceListener [] getObjects (System.Collections.IEnumerable en)
		{
			return Helper.getObjects < System.Diagnostics.TraceListener > (en);
		}
	}
}
