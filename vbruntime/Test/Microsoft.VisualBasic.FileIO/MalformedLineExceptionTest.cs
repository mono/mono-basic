// MalformedLineExceptionTest.cs - NUnit Test Cases for Microsoft.VisualBasic.FileIO.MalformedLineException
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
using Microsoft.VisualBasic.FileIO;

namespace MonoTests.Microsoft_VisualBasic.FileIO
{
	[TestFixture]
	public class MalformedLineExceptionTest
	{
		[Test]
		public void Test ()
		{
			MalformedLineException ex;
			
			ex = new MalformedLineException ();
			Assert.AreEqual (0, ex.LineNumber, "A1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: Exception of type 'Microsoft.VisualBasic.FileIO.MalformedLineException' was thrown. Line Number:0", ex.ToString (), "A2");
#endif

			ex = new MalformedLineException ("msg");
			Assert.AreEqual (0, ex.LineNumber, "B1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: msg Line Number:0", ex.ToString (), "B2");
#endif

			ex = new MalformedLineException ("msg", new Exception ("InnerException"));
			Assert.AreEqual (0, ex.LineNumber, "C1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: msg ---> System.Exception: InnerException" + System.Environment.NewLine + "   --- End of inner exception stack trace --- Line Number:0", ex.ToString (), "C2");
#endif

			ex = new MalformedLineException ("msg", 52);
			Assert.AreEqual (52, ex.LineNumber, "D1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: msg Line Number:52", ex.ToString (), "D2");
#endif

			ex = new MalformedLineException ("msg", 53, new Exception("InnerException"));
			Assert.AreEqual (53, ex.LineNumber, "E1");
			#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: msg ---> System.Exception: InnerException" + System.Environment.NewLine + "   --- End of inner exception stack trace --- Line Number:53", ex.ToString (), "E2");
#endif

			ex = new MalformedLineException ();
			Assert.AreEqual (0, ex.LineNumber, "F1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: Exception of type 'Microsoft.VisualBasic.FileIO.MalformedLineException' was thrown. Line Number:0", ex.ToString (), "F2");
#endif

			ex.LineNumber = -345;
			Assert.AreEqual (-345, ex.LineNumber, "G1");
#if !TARGET_JVM
			Assert.AreEqual ("Microsoft.VisualBasic.FileIO.MalformedLineException: Exception of type 'Microsoft.VisualBasic.FileIO.MalformedLineException' was thrown. Line Number:-345", ex.ToString (), "G2");
#endif
		}
	}
}
