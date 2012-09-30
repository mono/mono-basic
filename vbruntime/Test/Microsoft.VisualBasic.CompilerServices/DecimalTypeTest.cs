// DecimalTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.DecimalType 
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
	public class DecimalTypeTestCS
	{
		public DecimalTypeTestCS()
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
			decimal d = 0;

			// test string = null
			d = Microsoft.VisualBasic.CompilerServices.DecimalType.FromString(st);
			Assert.AreEqual (null, st, "FromString#0");
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromString2()
		{
			decimal d = 0;
			string st = "garbage";

			// test string = garbage
			d = Microsoft.VisualBasic.CompilerServices.DecimalType.FromString(st);
		}

		#endregion

		#region FromObject

		[Test]
		public void FromObject1()
		{
			decimal d1;
			object o1 = null;
			string st = null;
			byte b1;
			bool bool1;

			// test object = null
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(o1);
			Assert.AreEqual (0, d1);

			// test string = null
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(st);
			Assert.AreEqual (0, d1);

			// test string
			st = "2";
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(st);
			Assert.AreEqual (2, d1);

			// byte
			b1 = 1;
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(b1);
			Assert.AreEqual (1, d1);

			// bool
			bool1 = true;
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(bool1);
			Assert.AreEqual (-1, d1);

			bool1 = false;
			d1 = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(bool1);
			Assert.AreEqual (0, d1);
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FromObject3()
		{
			object o1;
			o1 = 'w';
			Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(o1);
		}

		[Test]
		public void FromObjectTest_2()
		{
			object ObjI;
			object o1 = true;
			decimal It = -1;

			// test object = True
			ObjI = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#0");

			// test object = False
			o1 = false;
			It = 0;
			ObjI = Microsoft.VisualBasic.CompilerServices.DecimalType.FromObject(o1);
			Assert.AreEqual (It, ObjI, "FromObject#1");

			string ResTypeCode = "";
			ResTypeCode = Type.GetTypeCode(ObjI.GetType()).ToString();
			Assert.AreEqual ("Decimal",ResTypeCode, "FromObject#2");
		}
		#endregion

	}
}
