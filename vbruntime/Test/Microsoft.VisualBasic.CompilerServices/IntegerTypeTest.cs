// IntegerTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.IntegerType 
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
	public class IntegerTypeTestCS
	{
		public IntegerTypeTestCS()
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
			int i = 0;

			// test string = null
			i = Microsoft.VisualBasic.CompilerServices.IntegerType.FromString(st);
			Assert.AreEqual (null, st, "FromString#0");
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString2()
		{
			int i;
			object o1 = null;
			string st = "garbage";

			// test string = garbage
			i = Microsoft.VisualBasic.CompilerServices.IntegerType.FromString(st);
		}

		#endregion

		#region FromObject

		[Test]
		public void FromObject1()
		{
			int i1;
			object o1 = null;
			string st = null;
			byte b1;
			bool bool1;

			// test object = null
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(o1);
			Assert.AreEqual (0, i1);

			// test string = null
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(st);
			Assert.AreEqual (0, i1);

			// string 
			st = "2";
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(st);
			Assert.AreEqual (2, i1);

			// byte
			b1 = 1;
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(b1);
			Assert.AreEqual (1, i1);

			// bool
			bool1 = true;
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(bool1);
			Assert.AreEqual (-1, i1);

			bool1 = false;
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(bool1);
			Assert.AreEqual (0, i1);

		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromObject2()
		{
			object o1;
			o1 = 'w';
			Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(o1);
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromObject3()
		{
			int i1;
			string st1 = "2w";
			i1 = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(st1);
		}

		[Test]
		public void FromObjectTest4()
		{
			object ObjI;
			object o1 = true;
			long It = -1;

			// test object = True
			ObjI = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#0");

			// test object = False
			o1 = false;
			It = 0;
			ObjI = Microsoft.VisualBasic.CompilerServices.IntegerType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#1");

			string ResTypeCode = "";
			ResTypeCode = Type.GetTypeCode(ObjI.GetType()).ToString();
			Assert.AreEqual ("Int32",ResTypeCode, "FromObject#2");
		}
		#endregion

	}
}
