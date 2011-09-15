// StringTypeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.StringType 
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
	public class StringTypeTestCS
	{
		public StringTypeTestCS()
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

		#region FromObject

		[Test]
		public void FromObject1()
		{
			string s1;
			object o1 = null;

			// test object = null
			s1 = Microsoft.VisualBasic.CompilerServices.StringType.FromObject(o1);
			Assert.AreEqual (null, s1);
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]		
		public void FromObject2()
		{
			string s1;

			ST1 st1 = new ST1();
			s1 = Microsoft.VisualBasic.CompilerServices.StringType.FromObject(st1);
		}

		public class ST1
		{
		}
		
		[Test]
		public void FromObject3()
		{
			string s1;
			Microsoft.VisualBasic.CompareMethod enum1 = Microsoft.VisualBasic.CompareMethod.Binary;

			s1 = Microsoft.VisualBasic.CompilerServices.StringType.FromObject(enum1);
			Assert.AreEqual ("0", s1);
		}
		#endregion

	}
}
