// DoubleTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.DoubleType 
//
// Mizrahi Rafael (rafim@mainsoft.com)
// Guy Cohen	  (guyc@mainsoft.com)
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
	public class DoubleTypeTestCS
	{
		public DoubleTypeTestCS()
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
			double dbl1 = 0;

			// test string = null
			dbl1 = Microsoft.VisualBasic.CompilerServices.DoubleType.FromString(st);
			Assert.AreEqual (null, st, "FromString#0");
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString2()
		{
			double dbl1 = 0;
			string st = "garbage";

			// test string = garbage
			dbl1 = Microsoft.VisualBasic.CompilerServices.DoubleType.FromString(st);
		}

		#endregion

		#region FromObject

		[Test]
		public void FromObjectTest_1()
		{
			double dbl1;
			object o1 = null;
			string st = null;

			// test object = null
			dbl1 = Microsoft.VisualBasic.CompilerServices.DoubleType.FromObject(o1);
			Assert.AreEqual (0, dbl1, "FromObject#0");

			// test string = null
			dbl1 = Microsoft.VisualBasic.CompilerServices.DoubleType.FromObject(st);
			Assert.AreEqual (0, dbl1, "FromObject#1");
		}

		[Test]
		public void FromObjectTest_2()
		{
			object ObjI;
			object o1 = true;
			double It = -1;

			// test object = True
			ObjI = Microsoft.VisualBasic.CompilerServices.DoubleType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#0");

			// test object = False
			o1 = false;
			It = 0;
			ObjI = Microsoft.VisualBasic.CompilerServices.DoubleType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#1");

			string ResTypeCode = "";
			ResTypeCode = Type.GetTypeCode(ObjI.GetType()).ToString();
			Assert.AreEqual ("Double",ResTypeCode, "FromObject#2");
		}
		#endregion

	}
}
