// UtilsTest.cs - NUnit Test Cases for Microsoft.VisualBasic.CompilerServices.Utils 
//
// Boris Kirzner <borisk@mainsoft.com>
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
using Microsoft.VisualBasic.CompilerServices;

namespace MonoTests.Microsoft_VisualBasic.CompilerServices
{
	[TestFixture]
	public class UtilsTestsCS
	{
		public UtilsTestsCS()
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

		[Test]
		public void ReDimPreserve_SingleDimension() 
		{
			string[] a = new string[6];

			a[1] = "a";
			a[2] = "b";
			a[3] = "c";

			string[] b = (string[]) Microsoft.VisualBasic.CompilerServices.Utils.CopyArray((Array) a, new string[4]);

			// Assert.AreEqual(b[0],null);
			Assert.AreEqual(b[1],"a");
			Assert.AreEqual(b[2],"b");
			Assert.AreEqual(b[3],"c");
		}

		[Test]
		public void ReDimPreserve_MultipleDimensions() 
		{
			int [,,,] source = new int [,,,] {{{{1, 2}, {3, 4}, {5, 6}}, {{7, 8}, {9, 10}, {11, 12}}}};
			int [,,,] destination = new int [1, 2, 3, 4];
			
			Utils.CopyArray (source, destination);

			Assert.AreEqual (destination [0, 0, 0, 0], 1, "#01");
			Assert.AreEqual (destination [0, 0, 0, 1], 2, "#02");
			Assert.AreEqual (destination [0, 0, 0, 2], 0, "#03");
			Assert.AreEqual (destination [0, 0, 0, 3], 0, "#04");
			Assert.AreEqual (destination [0, 0, 1, 0], 3, "#05");
			Assert.AreEqual (destination [0, 0, 1, 1], 4, "#06");
			Assert.AreEqual (destination [0, 0, 1, 2], 0, "#07");
			Assert.AreEqual (destination [0, 0, 1, 3], 0, "#08");
			Assert.AreEqual (destination [0, 0, 2, 0], 5, "#09");
			Assert.AreEqual (destination [0, 0, 2, 1], 6, "#10");
			Assert.AreEqual (destination [0, 0, 2, 2], 0, "#11");
			Assert.AreEqual (destination [0, 0, 2, 3], 0, "#12");

			Assert.AreEqual (destination [0, 1, 0, 0], 7, "#21");
			Assert.AreEqual (destination [0, 1, 0, 1], 8, "#22");
			Assert.AreEqual (destination [0, 1, 0, 2], 0, "#23");
			Assert.AreEqual (destination [0, 1, 0, 3], 0, "#24");
			Assert.AreEqual (destination [0, 1, 1, 0], 9, "#25");
			Assert.AreEqual (destination [0, 1, 1, 1], 10, "#26");
			Assert.AreEqual (destination [0, 1, 1, 2], 0, "#27");
			Assert.AreEqual (destination [0, 1, 1, 3], 0, "#28");
			Assert.AreEqual (destination [0, 1, 2, 0], 11, "#29");
			Assert.AreEqual (destination [0, 1, 2, 1], 12, "#30");
			Assert.AreEqual (destination [0, 1, 2, 2], 0, "#31");
			Assert.AreEqual (destination [0, 1, 2, 3], 0, "#32");


			destination = new int [1, 2, 3, 1];

			Utils.CopyArray (source, destination);

			Assert.AreEqual (destination [0, 0, 0, 0], 1, "#A1");
			Assert.AreEqual (destination [0, 0, 1, 0], 3, "#A2");
			Assert.AreEqual (destination [0, 0, 2, 0], 5, "#A3");
			Assert.AreEqual (destination [0, 1, 0, 0], 7, "#A4");
			Assert.AreEqual (destination [0, 1, 1, 0], 9, "#A5");
			Assert.AreEqual (destination [0, 1, 2, 0], 11, "#A6");
		}
			
		[Test]
		public void TestCopyArrayOneDimensionalShrinking() 
		{
			string[] source = new string[] { "First", "Second", "Third" };
			string[] destination = new string[2];
			string[] result = (string[])Utils.CopyArray(source, destination);
			Assert.AreSame (destination, result, "ResultIsDestination");
			Assert.AreEqual (source[0], destination[0], "First");
			Assert.AreEqual (source[1], destination[1], "Second");
		}

		[Test]
		public void TestCopyArrayOneDimensionalExpanding() 
		{
			string[] source = new string[] { "First", "Second" };
			string[] destination = new string[3];
			string[] result = (string[])Utils.CopyArray(source, destination);
			Assert.AreSame (destination, result, "ResultIsDestination");
			Assert.AreEqual (source[0], destination[0], "First");
			Assert.AreEqual (source[1], destination[1], "Second");
			Assert.IsNull (destination[2], "EmptyThird");
		}

		[Test]
		public void TestCopyArrayBiDimensionalShrinking() 
		{
			string[,] source = new string[2,2];
			source[0,0] = "First";
			source[0,1] = "Second";
			source[1,0] = "Third";
			source[1,1] = "Fourth";
			string[,] destination = new string[2,1];
			string[,] result = (string[,])Utils.CopyArray(source, destination);
			Assert.AreSame (destination, result, "ResultIsDestination");
			Assert.AreEqual (source[0,0], destination[0,0], "First");
			Assert.AreEqual (source[1,0], destination[1,0], "Third");
		}

		[Test]
		public void TestCopyArrayBiDimensionalExpanding() 
		{
			string[,] source = new string[2,2];
			source[0,0] = "First";
			source[0,1] = "Second";
			source[1,0] = "Third";
			source[1,1] = "Fourth";
			string[,] destination = new string[2,3];
			string[,] result = (string[,])Utils.CopyArray(source, destination);
			Assert.AreSame (destination, result, "ResultIsDestination");
			Assert.AreEqual (source[0,0], destination[0,0], "First");
			Assert.AreEqual (source[0,1], destination[0,1], "Second");
			Assert.AreEqual (source[1,0], destination[1,0], "Third");
			Assert.AreEqual (source[1,1], destination[1,1], "Fourth");
			Assert.IsNull (destination[0,2], "EmptyFifth");
			Assert.IsNull (destination[1,2], "EmptySixth");
		}


		[Test]
		[ExpectedException (typeof (InvalidCastException), "'ReDim' cannot change the number of dimensions.")]
		public void TestCopyArrayDifferentDimensions ()
		{
			string [] source = new string [2];
			string [,] destination = new string [2, 2];
			object result = Utils.CopyArray (source, destination);
		}

		[Test]
		public void TestCopyArrayNullSource ()
		{
			string [,] destination = new string [2, 2];
			object result = Utils.CopyArray (null, destination);
			
			Assert.AreSame (result, destination, "#01");
		}


		[Test]
		[ExpectedException (typeof (NullReferenceException))]
		public void TestCopyArrayNullDestination ()
		{
			string [] source = new string [2];
			object result = Utils.CopyArray (source, null);
		}

		[Test]
		public void TestCopyArrayReturnValue ()
		{
			string [] source = new string [2];
			string [] destination = new string [2];
			object result = Utils.CopyArray (source, destination);
			
			Assert.AreSame (result, destination, "#01");
		}
		
	}
}
