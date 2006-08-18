// ShortTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.ShortType 
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
	public class ShortTypeTest 
	{
		public ShortTypeTest()
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
		public void FromStringTest()
		{
			string st = null;
			short s = 0;

			// test string = null
			s = Microsoft.VisualBasic.CompilerServices.ShortType.FromString(st);
			Assert.AreEqual (null, st, "FromString#0");
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString2()
		{
			short s = 0;
			string st = "garbage";

			// test string = garbage
			s = Microsoft.VisualBasic.CompilerServices.ShortType.FromString(st);
		}

		#endregion

		#region FromObject

		[Test]
		public void FromObjectTest()
		{
			short s;
			object o1 = null;
			string st = null;

			// test object = null
			s = Microsoft.VisualBasic.CompilerServices.ShortType.FromObject(o1);
			Assert.AreEqual (0, s, "FromObject#0");

			// test string = null
			s = Microsoft.VisualBasic.CompilerServices.ShortType.FromObject(st);
			Assert.AreEqual (0, s, "FromObject#1");
		}

		#endregion

	}
}
