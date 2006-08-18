// ConversionTests.cs - NUnit Test Cases for Microsoft.VisualBasic.Conversion 
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

using NUnit.Framework;
using System;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic
{
	/// <summary>
	/// Summary description for ConversionTests.
	/// </summary>
	[TestFixture]
	public class ConversionTests
	{
		public ConversionTests()
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

		#region ErrorToString Tests

		[Test]
		public void ErrorToString1() 
		{
			Assert.AreEqual("Application-defined or object-defined error.",Conversion.ErrorToString(-1));
			Assert.AreEqual("",Conversion.ErrorToString());
			Assert.AreEqual("",Conversion.ErrorToString(0));
			Assert.AreEqual("Application-defined or object-defined error.",Conversion.ErrorToString(1));
			Assert.AreEqual("This Error number is obsolete and no longer used.",Conversion.ErrorToString(3));
			Assert.AreEqual("Application-defined or object-defined error.",Conversion.ErrorToString(95));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ErrorToString2() 
		{
			Assert.AreEqual(0,Conversion.ErrorToString(65535));
		}


		#endregion


		#region Fix Tests

		[Test]
		public void Fix1() 
		{
			Assert.AreEqual(12,Conversion.Fix(12.2));
			Assert.AreEqual(0,Conversion.Fix(0));
			Assert.AreEqual(0,Conversion.Fix(-0));
			Assert.AreEqual(0,Conversion.Fix(0.5));
			Assert.AreEqual(1,Conversion.Fix(1.5));
			Assert.AreEqual(123,Conversion.Fix(123.321));

			double dbl1 = 123.231;
			Assert.AreEqual(123,Conversion.Fix(dbl1));

			decimal dec1 = 234;
			Assert.AreEqual(234,Conversion.Fix(dec1));

			Assert.AreEqual(-12,Conversion.Fix(-12.4));
			Assert.AreEqual(0,Conversion.Fix(-0.5));


			char c1 = '1';
			Assert.AreEqual(49,Conversion.Fix(c1));
			c1 = '\u0058';
			Assert.AreEqual(88,Conversion.Fix(c1));

			bool b1 = true;
			Assert.AreEqual(1,Conversion.Fix(b1));

		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Fix2() 
		{
			object o1 = null;
			Assert.AreEqual(0,Conversion.Fix(o1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Fix3() 
		{
			A a1 = new A();
			Assert.AreEqual(0,Conversion.Fix(a1));
		}
		internal class A
		{
		}

		#endregion

		#region Int Tests

		[Test]
		public void Int1() 
		{
			float f1 = 12.2F;
			Assert.AreEqual(12,Conversion.Int(f1));
			Assert.AreEqual(123,Conversion.Int(123.321));
			
			double dbl1 = 123.231;
			Assert.AreEqual(123,Conversion.Int(dbl1));

			decimal dec1 = 234;
			Assert.AreEqual(234,Conversion.Int(dec1));

			Assert.AreEqual(-13,Conversion.Int(-12.4));
			Assert.AreEqual(-1,Conversion.Int(-0.5));

			object o1 = null;
			dbl1 = 123.231;
			o1 = dbl1;
			Assert.AreEqual(123,Conversion.Int(o1));

			char c1 = '1';
			Assert.AreEqual(49,Conversion.Int(c1));
			c1 = '\u0058';
			Assert.AreEqual(88,Conversion.Int(c1));

			bool b1 = true;
			Assert.AreEqual(1,Conversion.Int(b1));
		}


		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Int2() 
		{
			object o1 = null;
			Assert.AreEqual(0,Conversion.Int(o1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Int3() 
		{
			A2 a1 = new A2();
			Assert.AreEqual(0,Conversion.Int(a1));
		}
		internal class A2
		{
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Int4() 
		{
			DateTime date1 = new DateTime(1999,1,1);
			Assert.AreEqual(0,Conversion.Int(date1));
		}

		#endregion

		#region Hex Tests

		[Test]
		public void Hex1() 
		{
			byte byte1;
			string s;
			int i;
			long l;
			object o1;
			short short1;
			double dbl1;
			float f;

			byte1 = 11;
			Assert.AreEqual("B",Conversion.Hex(byte1));

			Assert.AreEqual("C",Conversion.Hex(12));

			s = "11";
			Assert.AreEqual("B",Conversion.Hex(s));

			i = 300;
			Assert.AreEqual("12C",Conversion.Hex(i));

			l = 1300;
			Assert.AreEqual("514",Conversion.Hex(l));

			short1 = 10;
			Assert.AreEqual("A",Conversion.Hex(short1));

			o1 = 14;
			Assert.AreEqual("E",Conversion.Hex(o1));

			s = "10";
			Assert.AreEqual("A",Conversion.Hex(s));

			s = "010";
			Assert.AreEqual("A",Conversion.Hex(s));

			s = System.Int32.MaxValue.ToString();
			Assert.AreEqual("7FFFFFFF",Conversion.Hex(s));

			s = System.Int64.MaxValue.ToString();
			Assert.AreEqual("7FFFFFFFFFFFFFFF",Conversion.Hex(s));

			dbl1 = 234.234;
			Assert.AreEqual("EA",Conversion.Hex(dbl1));

			f = 234.234F;
			Assert.AreEqual("EA",Conversion.Hex(f));

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Hex2() 
		{
			bool b = false;
			Assert.AreEqual(0,Conversion.Hex(b));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Hex3() 
		{
			object o1 = null;
			Assert.AreEqual(0,Conversion.Hex(o1));
		}


		[Test]
		public void Hex4() 
		{
			Assert.AreEqual("0",Conversion.Hex(0));

			Assert.AreEqual("211D1AE3",Conversion.Hex(555555555));
			Assert.AreEqual("2540BE400",Conversion.Hex(10000000000));
			Assert.AreEqual("5AF3107A4000",Conversion.Hex(100000000000000));
			Assert.AreEqual("38D7EA4C68000",Conversion.Hex(1000000000000000));
			Assert.AreEqual("2386F26FC10000",Conversion.Hex(10000000000000000));

			// negatives
			Assert.AreEqual("FFFFFFFF",Conversion.Hex(-1));
			Assert.AreEqual("FFFFFFFE",Conversion.Hex(-1.5));
			Assert.AreEqual("FFFFFFF1",Conversion.Hex(-15));
			Assert.AreEqual("FFFFFC18",Conversion.Hex(-1000));
			Assert.AreEqual("FFFF63C0",Conversion.Hex(-40000));
			Assert.AreEqual("FFFEEE90",Conversion.Hex(-70000));
			Assert.AreEqual("FFFE7960",Conversion.Hex(-100000));
			Assert.AreEqual("FFF0BDC0",Conversion.Hex(-1000000));
			Assert.AreEqual("FF676980",Conversion.Hex(-10000000));
			Assert.AreEqual("FA0A1F00",Conversion.Hex(-100000000));
			Assert.AreEqual("C4653600",Conversion.Hex(-1000000000));
			//FIXME:Assert.AreEqual("FFFFFFFDABF41C00",Conversion.Hex(-10000000000));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Hex5() 
		{
			A3 a1 = new A3();
			Assert.AreEqual("2386F26FC10000",Conversion.Hex(a1));
		}
		internal class A3
		{
		}

		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void Hex6() 
		{
			decimal d = System.Int64.MaxValue;
			d = d +1;
			Assert.AreEqual("0",Conversion.Hex(d));
		}

		[Test]
		public void Hex7() 
		{
			string s;
			s = "&o123";
			Assert.AreEqual("53",Conversion.Hex(s));

			s = "&O123";
			Assert.AreEqual("53",Conversion.Hex(s));

			s = "&h123";
			Assert.AreEqual("123",Conversion.Hex(s));

			s = "&H123";
			Assert.AreEqual("123",Conversion.Hex(s));
			
		}
		#endregion

		#region Oct Tests

		[Test]
		public void Oct1() 
		{
			byte byte1;
			string s;
			int i;
			long l;
			object o1;
			short short1;
			double dbl1;
			float f;

			byte1 = 11;
			Assert.AreEqual("13",Conversion.Oct(byte1));

			Assert.AreEqual("14",Conversion.Oct(12));

			s = "11";
			Assert.AreEqual("13",Conversion.Oct(s));

			i = 300;
			Assert.AreEqual("454",Conversion.Oct(i));

			l = 1300;
			Assert.AreEqual("2424",Conversion.Oct(l));

			short1 = 10;
			Assert.AreEqual("12",Conversion.Oct(short1));

			o1 = 14;
			Assert.AreEqual("16",Conversion.Oct(o1));

			s = "10";
			Assert.AreEqual("12",Conversion.Oct(s));

			s = "010";
			Assert.AreEqual("12",Conversion.Oct(s));

			s = System.Int32.MaxValue.ToString();
			Assert.AreEqual("17777777777",Conversion.Oct(s));

			s = System.Int64.MaxValue.ToString();
			Assert.AreEqual("777777777777777777777",Conversion.Oct(s));

			dbl1 = 234.234;
			Assert.AreEqual("352",Conversion.Oct(dbl1));

			f = 234.234F;
			Assert.AreEqual("352",Conversion.Oct(f));

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Oct2() 
		{
			bool b = false;
			Assert.AreEqual(0,Conversion.Oct(b));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Oct3() 
		{
			object o1 = null;
			Assert.AreEqual(0,Conversion.Oct(o1));
		}


		[Test]
		public void Oct4() 
		{
			Assert.AreEqual("0",Conversion.Oct(0));

			Assert.AreEqual("4107215343",Conversion.Oct(555555555));
			Assert.AreEqual("112402762000",Conversion.Oct(10000000000));
			Assert.AreEqual("2657142036440000",Conversion.Oct(100000000000000));
			Assert.AreEqual("34327724461500000",Conversion.Oct(1000000000000000));
			Assert.AreEqual("434157115760200000",Conversion.Oct(10000000000000000));

			// negatives
			Assert.AreEqual("37777777777",Conversion.Oct(-1));
			Assert.AreEqual("37777777776",Conversion.Oct(-1.5));
			Assert.AreEqual("37777777761",Conversion.Oct(-15));
			Assert.AreEqual("37777776030",Conversion.Oct(-1000));
			Assert.AreEqual("37777661700",Conversion.Oct(-40000));
			Assert.AreEqual("37777567220",Conversion.Oct(-70000));
			Assert.AreEqual("37777474540",Conversion.Oct(-100000));
			Assert.AreEqual("37774136700",Conversion.Oct(-1000000));
			Assert.AreEqual("37731664600",Conversion.Oct(-10000000));
			Assert.AreEqual("37202417400",Conversion.Oct(-100000000));
			Assert.AreEqual("30431233000",Conversion.Oct(-1000000000));
			//FIXME:Assert.AreEqual("1777777777665375016000",Conversion.Oct(-10000000000));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Oct5() 
		{
			A4 a1 = new A4();
			Assert.AreEqual("2386F26FC10000",Conversion.Oct(a1));
		}
		internal class A4
		{
		}

		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void Oct6() 
		{
			decimal d = System.Int64.MaxValue;
			d = d +1;
			Assert.AreEqual("0",Conversion.Oct(d));
		}

		#endregion

		#region Str Tests

		[Test]
		public void Str1() 
		{
			byte byte1;
			int i;
			long l;
			object o1;
			short short1;
			double dbl1;
			float f;

			byte1 = 11;
			Assert.AreEqual(" 11",Conversion.Str(byte1));

			Assert.AreEqual(" 12",Conversion.Str(12));

			i = 300;
			Assert.AreEqual(" 300",Conversion.Str(i));

			i = -300;
			Assert.AreEqual("-300",Conversion.Str(i));

			l = 1300;
			Assert.AreEqual(" 1300",Conversion.Str(l));

			short1 = 10;
			Assert.AreEqual(" 10",Conversion.Str(short1));

			o1 = 14;
			Assert.AreEqual(" 14",Conversion.Str(o1));

			o1 = 52654;
			Assert.AreEqual(" 52654",Conversion.Str(o1));

			i = System.Int32.MaxValue;
			Assert.AreEqual(" 2147483647",Conversion.Str(i));

			l = System.Int64.MaxValue;
			Assert.AreEqual(" 9223372036854775807",Conversion.Str(l));

			dbl1 = 234.234;
			Assert.AreEqual(" 234.234",Conversion.Str(dbl1));

			f = 234.234F;
			Assert.AreEqual(" 234.234",Conversion.Str(f));

		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void Str2() 
		{
			//Str(string) throws System.NullReferenceException: Object reference not set to an instance of an object.
			string s;
			s = "11";
			Assert.AreEqual("11",Conversion.Str(s));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Str3() 
		{
			object o1 = null;
			Assert.AreEqual(0,Conversion.Str(o1));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void Str4() 
		{
			object o1 = new object();
			Assert.AreEqual(0,Conversion.Str(o1));
		}

		#endregion

		#region Val Tests

		[Test]
		public void Val1() 
		{
			char c1;

			c1 = 'a';
			Assert.AreEqual(0,Conversion.Val(c1));

			c1 = '1';
			Assert.AreEqual(1,Conversion.Val(c1));
		}

		[Test]
		public void Val2() 
		{
			string s1;

			s1 = null;
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "9";
			Assert.AreEqual(9,Conversion.Val(s1));

			s1 = "0 9";
			Assert.AreEqual(9,Conversion.Val(s1));

			s1 = "9 9";
			Assert.AreEqual(99,Conversion.Val(s1));

			s1 = "123.321";
			Assert.AreEqual(123.321,Conversion.Val(s1));

			s1 = "9 . 9 a";
			Assert.AreEqual(9.9,Conversion.Val(s1));

			s1 = "B9 . 9 a";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "12.215.215.25";
			Assert.AreEqual(12.215,Conversion.Val(s1));

			s1 = "88888888";
			Assert.AreEqual(88888888,Conversion.Val(s1));

			s1 = " &F";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = " & 2";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "1 & 2";
			Assert.AreEqual(1,Conversion.Val(s1));

			s1 = "& HF";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "&HFF";
			Assert.AreEqual(255,Conversion.Val(s1));

			s1 = "&HFFFF";
			Assert.AreEqual(-1,Conversion.Val(s1));

			s1 = "&HFFFE";
			Assert.AreEqual(-2,Conversion.Val(s1));

			s1 = "&HF111";
			Assert.AreEqual(-3823,Conversion.Val(s1));

			s1 = "&HFFFFF";
			Assert.AreEqual(1048575,Conversion.Val(s1));

			s1 = "&HFFFFFF";
			Assert.AreEqual(16777215,Conversion.Val(s1));

			s1 = "&HFFFFFFFF";
			Assert.AreEqual(-1,Conversion.Val(s1));

			s1 = "&HFFFFFDDD";
			Assert.AreEqual(-547,Conversion.Val(s1));

			s1 = "&HFFFFFFFFFFFF";
			Assert.AreEqual(281474976710655,Conversion.Val(s1));

			s1 = "&HFFFFFFFFFFFFFFFD";
			Assert.AreEqual(-3,Conversion.Val(s1));

			s1 = "&HFF.FF";
			Assert.AreEqual(255,Conversion.Val(s1));

			s1 = "1&HFFFF";
			Assert.AreEqual(1,Conversion.Val(s1));

			s1 = "  &H FFF E";
			Assert.AreEqual(-2,Conversion.Val(s1));

			s1 = "  &HFF";
			Assert.AreEqual(255,Conversion.Val(s1));

			s1 = "  &H FFG E";
			Assert.AreEqual(255,Conversion.Val(s1));

			s1 = "&O10";
			Assert.AreEqual(8,Conversion.Val(s1));

			s1 = "&O1 5";
			Assert.AreEqual(13,Conversion.Val(s1));

			s1 = "&O9 5";
			Assert.AreEqual(0,Conversion.Val(s1));

			s1 = "&O7777777";
			Assert.AreEqual(2097151,Conversion.Val(s1));

			s1 = "&O777764536644327";
			Assert.AreEqual(35182853441751,Conversion.Val(s1));

			//
			//
			//FIXME: s1 = "99999999999999999999999999999999999999999";
			//Assert.AreEqual("1E+41",Conversion.Val(s1).ToString());

			//FIXME: s1 = "&HFFFFFFFFFFFFFFFDD";
			//Assert.AreEqual(-35,Conversion.Val(s1));

		}

		[Test]
		public void Val3() 
		{
			string s1;
			object o1;
			bool b1;

			s1 = "123";
			o1 = s1;
			Assert.AreEqual(123, Conversion.Val(o1));
			
			b1 = true;
			Assert.AreEqual(-1, Conversion.Val(b1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Val4() 
		{
			V v1 = new V();
			Assert.AreEqual(0,Conversion.Val(v1));
		}
		internal class V
		{
		}
		#endregion
	}
}
