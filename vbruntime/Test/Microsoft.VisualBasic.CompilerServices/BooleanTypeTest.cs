// BooleanTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.BooleanType 
//
// Mizrahi Rafael (rafim@mainsoft.com)
//
// 

// Copyright (c) 2002-2006 Mainsoft Corporation.
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
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
using NUnit.Framework;
using System;
using System.IO;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic.CompilerServices
{
	[TestFixture]
	public class BooleanTypeTestCS
	{
		public BooleanTypeTestCS()
		{
		}

		[SetUp]
		public void GetReady() 
		{
		}

		[TearDown]
		public void Clean() 
		{
		}	

		#region FromString

		[Test]
		public void FromString1()
		{
			string st = null;
			bool b = false;

			// test string = "False"
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString("False");
			Assert.AreEqual (false.ToString(), b.ToString(), "FromString1#0");

			// test string = "fAlse"
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString("fAlse");
			Assert.AreEqual (false.ToString(), b.ToString(), "FromString1#1");

			// test string = "TRUe"
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString("TRUe");
			Assert.AreEqual (true.ToString(), b.ToString(), "FromString1#2");

			// test string = "0"
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString("0");
			Assert.AreEqual (false.ToString(), b.ToString(), "FromString1#3");

		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString2()
		{
			bool b;
			object o1 = null;
			string st = null;

			// test string = null
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString(st);
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString3()
		{
			bool b;
			string st = "";

			// test string = ""
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromString(st);
		}

		#endregion

		#region FromObject

		[Test]
		public void FromObject1()
		{
			bool b;
			object o1 = true;

			// test object = true
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromObject(o1);
			Assert.AreEqual (true, b, "FromObject1#0");
		}

		[Test]
		public void FromObject2()
		{
			bool b;
			object o1 = null;

			// test object = null
			b = Microsoft.VisualBasic.CompilerServices.BooleanType.FromObject(o1);
			Assert.AreEqual (false, b, "FromObject2#0");
		}
		#endregion

	}
}
