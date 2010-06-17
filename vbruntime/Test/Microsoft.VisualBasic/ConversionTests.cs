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
            Information.Err().Clear();
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

		// Test the Fix function
		[Test]
		public void Fix() 
		{
			System.Single Sng;
			System.Double Dbl;
			System.Decimal Dec;
			System.String S;
			System.Object O;
			
			Assert.AreEqual(System.Int16.MaxValue, Conversion.Fix(System.Int16.MaxValue),"#F01");
			Assert.AreEqual(System.Int16.MinValue, Conversion.Fix(System.Int16.MinValue),"#F02");
			Assert.AreEqual(System.Int32.MaxValue, Conversion.Fix(System.Int32.MaxValue),"#F03");
			Assert.AreEqual(System.Int32.MinValue, Conversion.Fix(System.Int32.MinValue),"#F04");
			Assert.AreEqual(System.Int64.MaxValue, Conversion.Fix(System.Int64.MaxValue),"#F05");
			Assert.AreEqual(System.Int64.MinValue, Conversion.Fix(System.Int64.MinValue),"#F06");
			Assert.AreEqual((System.Single)Math.Floor(System.Single.MaxValue), Conversion.Fix(System.Single.MaxValue),"#F07");
			Assert.AreEqual( -1 * (System.Single)Math.Floor(-1 * System.Single.MinValue), Conversion.Fix(System.Single.MinValue),"#F08");
			Assert.AreEqual(Math.Floor(System.Double.MaxValue), Conversion.Fix(System.Double.MaxValue),"#F09");
			Assert.AreEqual(-1 * Math.Floor(-1 * System.Double.MinValue), Conversion.Fix(System.Double.MinValue),"#F10");
			Assert.AreEqual( Decimal.Floor(System.Decimal.MaxValue), Conversion.Fix(System.Decimal.MaxValue),"#F11");
			Assert.AreEqual(-1 * Decimal.Floor(-1 * System.Decimal.MinValue), Conversion.Fix(System.Decimal.MinValue),"#F12");

			Sng = 99.1F;

			Assert.AreEqual(99F, Conversion.Fix(Sng),"#F13");

			Sng = 99.6F;

			Assert.AreEqual( 99F, Conversion.Fix(Sng),"#F14");

			Sng = -99.1F;

			Assert.AreEqual( -99F, Conversion.Fix(Sng),"#F15");

			Sng = -99.6F;

			Assert.AreEqual(-99F, Conversion.Fix(Sng),"#F16");

			Dbl = 99.1;

			Assert.AreEqual(99D, Conversion.Fix(Dbl),"#F17");

			Dbl = 99.6;

			Assert.AreEqual(99D, Conversion.Fix(Dbl),"#F18");

			Dbl = -99.1;

			Assert.AreEqual( -99D, Conversion.Fix(Dbl),"#F19");

			Dbl = -99.6;

			Assert.AreEqual(-99D, Conversion.Fix(Dbl),"#F20");

			Dec = 99.1M;

			Assert.AreEqual( 99M, Conversion.Fix(Dec),"#F21");

			Dec = 99.6M;

			Assert.AreEqual(99M, Conversion.Fix(Dec),"#F22");

			Dec = -99.1M;

			Assert.AreEqual(-99M, Conversion.Fix(Dec),"#F23");

			Dec = -99.6M;

			Assert.AreEqual(-99M, Conversion.Fix(Dec),"#F24");

			Dbl = 99.1;
			S = Dbl.ToString();

			Assert.AreEqual(99D, Conversion.Fix(S),"#F25");

			Dbl = 99.6;
			S = Dbl.ToString();

			Assert.AreEqual(99D, Conversion.Fix(S),"#F26");

			Dbl = -99.1;
			S = Dbl.ToString();

			Assert.AreEqual(-99D, Conversion.Fix(S),"#F27");

			Dbl = -99.6;
			S = Dbl.ToString();

			Assert.AreEqual( -99D, Conversion.Fix(S),"#F28");

			Dbl = 99.1;
			O = Dbl;

			Assert.AreEqual(99D, Conversion.Fix(O),"#F29");

			Sng = 99.6F;
			O = Sng;

			Assert.AreEqual((System.Object)99F, Conversion.Fix(O),"#F30");

			Dbl = -99.1;
			O = Dbl;

			Assert.AreEqual(-99D, Conversion.Fix(O),"#F31");

			Dec = -99.6M;
			O = Dec;

			Assert.AreEqual((System.Object)(-99M), Conversion.Fix(O),"#F32");

			O = typeof(int);

			// test for Exceptions
			bool caughtException = false;
			try 
			{
				Conversion.Fix(O);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#F33");
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException,"#F34");

			caughtException = false;
			try 
			{
				Conversion.Fix(null);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentNullException), e.GetType(),"#F35");
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException,"#F36");

		}

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

					
		// Test the Int function
		[Test]
		public void Int() 
		{
			System.Single Sng;
			System.Double Dbl;
			System.Decimal Dec;
			System.String S;
			System.Object O;

			Assert.AreEqual(System.Int16.MaxValue, Conversion.Int(System.Int16.MaxValue),"#I01");
			Assert.AreEqual(System.Int16.MinValue, Conversion.Int(System.Int16.MinValue),"#I02");
			Assert.AreEqual(System.Int32.MaxValue, Conversion.Int(System.Int32.MaxValue),"#I03");
			Assert.AreEqual(System.Int32.MinValue, Conversion.Int(System.Int32.MinValue),"#I04");
			Assert.AreEqual(System.Int64.MaxValue, Conversion.Int(System.Int64.MaxValue),"#I05");
			Assert.AreEqual(System.Int64.MinValue, Conversion.Int(System.Int64.MinValue),"#I06");
			Assert.AreEqual((System.Single)Math.Floor(System.Single.MaxValue), Conversion.Int(System.Single.MaxValue),"#I07");
			Assert.AreEqual((System.Single)Math.Floor(System.Single.MinValue), Conversion.Int(System.Single.MinValue),"#I08");
			Assert.AreEqual(Math.Floor(System.Double.MaxValue), Conversion.Int(System.Double.MaxValue),"#I09");
			Assert.AreEqual(Math.Floor(System.Double.MinValue), Conversion.Int(System.Double.MinValue),"#I10");
			Assert.AreEqual(Decimal.Floor(System.Decimal.MaxValue), Conversion.Int(System.Decimal.MaxValue),"#I11");
			Assert.AreEqual(Decimal.Floor(System.Decimal.MinValue), Conversion.Int(System.Decimal.MinValue),"#I12");

			Sng = 99.1F;

			Assert.AreEqual(99F, Conversion.Int(Sng),"#I13");

			Sng = 99.6F;

			Assert.AreEqual(99F, Conversion.Int(Sng),"#I14");

			Sng = -99.1F;

			Assert.AreEqual(-100F, Conversion.Int(Sng),"#I15");

			Sng = -99.6F;

			Assert.AreEqual(-100F, Conversion.Int(Sng),"#I16");

			Dbl = 99.1;

			Assert.AreEqual(99D, Conversion.Int(Dbl),"#I17");

			Dbl = 99.6;

			Assert.AreEqual(99D, Conversion.Int(Dbl),"#I18");

			Dbl = -99.1;

			Assert.AreEqual(-100D, Conversion.Int(Dbl),"#I19");

			Dbl = -99.6;

			Assert.AreEqual(-100D, Conversion.Int(Dbl),"#I20");

			Dec = 99.1M;

			Assert.AreEqual(99M, Conversion.Int(Dec),"#I21");

			Dec = 99.6M;

			Assert.AreEqual(99M, Conversion.Int(Dec),"#I22");

			Dec = -99.1M;

			Assert.AreEqual(-100M, Conversion.Int(Dec),"#I23");

			Dec = -99.6M;

			Assert.AreEqual(-100M, Conversion.Int(Dec),"#I24");

			Dbl = 99.1;
			S = Dbl.ToString();

			Assert.AreEqual(99D, Conversion.Int(S),"#I25");

			Dbl = 99.6;
			S = Dbl.ToString();

			Assert.AreEqual(99D, Conversion.Int(S),"#I26");

			Dbl = -99.1;
			S = Dbl.ToString();

			Assert.AreEqual(-100D, Conversion.Int(S),"#I27");

			Dbl = -99.6;
			S = Dbl.ToString();

			Assert.AreEqual(-100D, Conversion.Int(S),"#I28");

			Dbl = 99.1;
			O = Dbl;

			Assert.AreEqual(99D, Conversion.Int(O),"#I29");

			Sng = 99.6F;
			O = Sng;

			Assert.AreEqual(99F, Conversion.Int(O),"#I30");

			Dbl = -99.1;
			O = Dbl;

			Assert.AreEqual(-100D, Conversion.Int(O),"#I31");

			Dec = -99.6M;
			O = Dec;

			Assert.AreEqual(-100M, Conversion.Int(O),"#I32");

			// test the exceptions it's supposed to throw

			O = typeof(int);
			bool caughtException = false;

			try 
			{
				Conversion.Fix(O);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#I33");
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException,"#I34");

			caughtException = false;
			try 
			{
				Conversion.Int(null);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentNullException), e.GetType(),"#I35");
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException,"#I36");


		}	

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


		// test the Hex function
		[Test]
		public void Hex() 
		{
			Assert.AreEqual("FF", Conversion.Hex(System.Byte.MaxValue));
			Assert.AreEqual("0", Conversion.Hex(System.Byte.MinValue));
			Assert.AreEqual("7FFF", Conversion.Hex(System.Int16.MaxValue));
			Assert.AreEqual("8000", Conversion.Hex(System.Int16.MinValue));
			Assert.AreEqual("7FFFFFFF", Conversion.Hex(System.Int32.MaxValue));
			Assert.AreEqual("80000000", Conversion.Hex(System.Int32.MinValue));
			Assert.AreEqual("7FFFFFFFFFFFFFFF", Conversion.Hex(System.Int64.MaxValue));
			Assert.AreEqual("8000000000000000", Conversion.Hex(System.Int64.MinValue));

			System.Byte UI8;
			System.Int16 I16;
			System.Int32 I32;
			System.Int64 I64;
			System.Object O;
			System.String S;

			UI8 = 15;
			Assert.AreEqual("F", Conversion.Hex(UI8));
			
			I16 = System.Byte.MaxValue;
			Assert.AreEqual("FF", Conversion.Hex(I16));

			I16 = (System.Int16)((I16 + 1) * -1);
			Assert.AreEqual("FF00", Conversion.Hex(I16));

			I16 = -2;
			Assert.AreEqual("FFFE", Conversion.Hex(I16));

			I32 = System.UInt16.MaxValue;
			Assert.AreEqual("FFFF", Conversion.Hex(I32));

			I32 = (I32 + 1) * -1;
			Assert.AreEqual("FFFF0000", Conversion.Hex(I32));

			I32 = -2;
			Assert.AreEqual("FFFFFFFE", Conversion.Hex(I32));

			I64 = System.UInt32.MaxValue;
			Assert.AreEqual("FFFFFFFF", Conversion.Hex(I64));

			I64 = (I64 + 1) * -1;
			Assert.AreEqual("FFFFFFFF00000000", Conversion.Hex(I64));
			
			I64 = -2;
			Assert.AreEqual("FFFFFFFFFFFFFFFE", Conversion.Hex(I64));
			
			I16 = System.Byte.MaxValue;
			S = I16.ToString();
			Assert.AreEqual("FF", Conversion.Hex(S));

			I16 = (System.Int16)((I16 + 1) * -1);
			S = I16.ToString();
			Assert.AreEqual("FFFFFF00", Conversion.Hex(S));

			I16 = -1;
			S = I16.ToString();
			Assert.AreEqual("FFFFFFFF", Conversion.Hex(S));

			I32 = System.UInt16.MaxValue;
			S = I32.ToString();
			Assert.AreEqual("FFFF", Conversion.Hex(S));

			I32 = (I32 + 1) * -1;
			S = I32.ToString();
			Assert.AreEqual("FFFF0000", Conversion.Hex(S));

			I32 = -2;
			S = I32.ToString();
			Assert.AreEqual("FFFFFFFE", Conversion.Hex(S));

			I64 = System.UInt32.MaxValue;
			S = I64.ToString();
			Assert.AreEqual("FFFFFFFF", Conversion.Hex(S));

			I64 = (I64 + 1) * -1;
			S = I64.ToString();
			Assert.AreEqual("FFFFFFFF00000000", Conversion.Hex(S));
			
			UI8 = System.Byte.MaxValue;
			O = UI8;
			Assert.AreEqual("FF", Conversion.Hex(O));

			I16 = System.Byte.MaxValue;
			O = I16;
			Assert.AreEqual("FF", Conversion.Hex(O));

			I16 = (System.Int16)((I16 + 1) * -1);
			O = I16;
			Assert.AreEqual("FF00", Conversion.Hex(O));

			I16 = -2;
			O = I16;
			Assert.AreEqual("FFFE", Conversion.Hex(O));

			I32 = System.UInt16.MaxValue;
			O = I32;
			Assert.AreEqual("FFFF", Conversion.Hex(O));

			I32 = (I32 + 1) * -1;
			O = I32;
			Assert.AreEqual("FFFF0000", Conversion.Hex(O));

			I32 = -2;
			O = I32;
			Assert.AreEqual("FFFFFFFE", Conversion.Hex(O));

			I64 = System.UInt32.MaxValue;
			O = I64;
			Assert.AreEqual("FFFFFFFF", Conversion.Hex(O));

			I64 = (I64 + 1) * -1;
			O = I64;
			Assert.AreEqual("FFFFFFFF00000000", Conversion.Hex(O));

			I64 = -2;
			O = I64;
			// FIXME : MS doesn't pass this test
			//Assert.AreEqual("FFFFFFFFFFFFFFFE", Conversion.Hex(O));

			O = typeof(int);

			bool caughtException = false;
			try 
			{
				Conversion.Hex(O);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException);

			caughtException = false;

			try 
			{
				Conversion.Hex(null);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentNullException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException);
		}
		
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

		// test the Oct function
		[Test]
		public void Oct() 
		{
			Assert.AreEqual( "377", Conversion.Oct(System.Byte.MaxValue));
			Assert.AreEqual( "0", Conversion.Oct(System.Byte.MinValue));
			Assert.AreEqual( "77777", Conversion.Oct(System.Int16.MaxValue));
			Assert.AreEqual( "100000", Conversion.Oct(System.Int16.MinValue));
			Assert.AreEqual( "17777777777", Conversion.Oct(System.Int32.MaxValue));
			Assert.AreEqual( "20000000000", Conversion.Oct(System.Int32.MinValue));
			Assert.AreEqual( "777777777777777777777", Conversion.Oct(System.Int64.MaxValue));
			//Assert.AreEqual( "1000000000000000000000", Conversion.Oct(System.Int64.MinValue));

			System.Byte UI8;
			System.Int16 I16;
			System.Int32 I32;
			System.Int64 I64;
			System.Object O;
			System.String S;

			UI8 = 15;
			Assert.AreEqual( "17", Conversion.Oct(UI8));
			
			I16 = System.Byte.MaxValue;
			Assert.AreEqual( "377", Conversion.Oct(I16));

			I16 = (System.Int16)((I16 + 1) * -1);
			Assert.AreEqual( "177400", Conversion.Oct(I16));

			I16 = -2;
			Assert.AreEqual( "177776", Conversion.Oct(I16));

			I32 = System.UInt16.MaxValue;
			Assert.AreEqual( "177777", Conversion.Oct(I32));

			I32 = (I32 + 1) * -1;
			Assert.AreEqual( "37777600000", Conversion.Oct(I32));

			I32 = -2;
			Assert.AreEqual( "37777777776", Conversion.Oct(I32));

			I64 = System.UInt32.MaxValue;
			Assert.AreEqual( "37777777777", Conversion.Oct(I64));

			I64 = (I64 + 1) * -1;
			Assert.AreEqual( "1777777777740000000000", Conversion.Oct(I64));
			
			I64 = -2;
			Assert.AreEqual( "1777777777777777777776", Conversion.Oct(I64));
			
			I16 = System.Byte.MaxValue;
			S = I16.ToString();
			Assert.AreEqual( "377", Conversion.Oct(S));

			I16 = (System.Int16)((I16 + 1) * -1);
			S = I16.ToString();
			Assert.AreEqual( "37777777400", Conversion.Oct(S));

			I16 = -2;
			S = I16.ToString();
			Assert.AreEqual( "37777777776", Conversion.Oct(S));

			I32 = System.UInt16.MaxValue;
			S = I32.ToString();
			Assert.AreEqual( "177777", Conversion.Oct(S));

			I32 = (I32 + 1) * -1;
			S = I32.ToString();
			Assert.AreEqual( "37777600000", Conversion.Oct(S));

			I32 = -2;
			S = I32.ToString();
			Assert.AreEqual( "37777777776", Conversion.Oct(S));

			I64 = System.UInt32.MaxValue;
			S = I64.ToString();
			Assert.AreEqual( "37777777777", Conversion.Oct(S));

			I64 = (I64 + 1) * -1;
			S = I64.ToString();
			Assert.AreEqual( "1777777777740000000000", Conversion.Oct(S));
			
			UI8 = System.Byte.MaxValue;
			O = UI8;
			Assert.AreEqual( "377", Conversion.Oct(O));

			I16 = System.Byte.MaxValue;
			O = I16;
			Assert.AreEqual( "377", Conversion.Oct(O));

			I16 = (System.Int16)((I16 + 1) * -1);
			O = I16;
			Assert.AreEqual( "177400", Conversion.Oct(O));

			I16 = -2;
			O = I16;
			Assert.AreEqual( "177776", Conversion.Oct(O));

			I32 = System.UInt16.MaxValue;
			O = I32;
			Assert.AreEqual( "177777", Conversion.Oct(O));

			I32 = (I32 + 1) * -1;
			O = I32;
			Assert.AreEqual( "37777600000", Conversion.Oct(O));

			I32 = -2;
			O = I32;
			Assert.AreEqual( "37777777776", Conversion.Oct(O));

			I64 = System.UInt32.MaxValue;
			O = I64;
			Assert.AreEqual( "37777777777", Conversion.Oct(O));

			I64 = (I64 + 1) * -1;
			O = I64;
			Assert.AreEqual( "1777777777740000000000", Conversion.Oct(O));

			I64 = -2;
			O = I64;

			// FIXME: MS doesn't pass this test
			// Assert.AreEqual( "1777777777777777777776", Conversion.Oct(O));
		
			O = typeof(int);

			bool caughtException = false;
			try 
			{
				Conversion.Oct(O);
			}
			catch (Exception e) 
			{
				Assert.AreEqual( typeof(ArgumentException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual( true, caughtException);
			
			caughtException = false;

			try 
			{
				Conversion.Oct(null);
			}
			catch (Exception e) 
			{
				Assert.AreEqual( typeof(ArgumentNullException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual( true, caughtException);
		}

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


		// test the Str function
		[Test]
		public void Str() 
		{
			Assert.AreEqual("-1", Conversion.Str(-1));
			Assert.AreEqual(" 1", Conversion.Str(1));

			bool caughtException = false;
			Object O = typeof(int);

			try 
			{
				Conversion.Str(O);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(InvalidCastException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException);

			caughtException = false;

			try 
			{
				Conversion.Str(null);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentNullException), e.GetType());
				caughtException = true;
			}
		}


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

		// Test the Val function
		[Test]
		public void Val() 
		{
			Assert.AreEqual(4, Conversion.Val('4'));
			Assert.AreEqual(-3542.76, Conversion.Val("    -   3       5   .4   2  7   6E+    0 0 2    "));
			Assert.AreEqual(255D, Conversion.Val("&HFF"));
			Assert.AreEqual(255D, Conversion.Val("&o377"));

			System.Object O = "    -   3       5   .4     7   6E+    0 0 3";

			Assert.AreEqual(-35476D, Conversion.Val(O));

			bool caughtException;

			caughtException = false;

			try 
			{
				Conversion.Val("3E+9999999");
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(OverflowException), e.GetType());
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException);

			caughtException = false;

			try 
			{
				Conversion.Val(typeof(int));
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType());
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException);

			Assert.AreEqual(0, Conversion.Val (null));
			Assert.AreEqual(0, Conversion.Val (String.Empty));
		}


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
