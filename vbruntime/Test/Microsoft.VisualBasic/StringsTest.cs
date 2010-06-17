// StringsTest.cs - NUnit Test Cases for Microsoft.VisualBasic.Strings 
//
// Mizrahi Rafael (rafim@mainsoft.com)
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
using System.Globalization;
namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class StringsTest 
	{
		public StringsTest()
		{
		}

		[SetUp]
		public void GetReady()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo ("en-US");
		}

		[TearDown]
		public void Clean() 
		{
		}	

		#region Asc Tests
		[Test]
		[Category("NotWorking")]
		public void Asc1() 
		{
			Assert.AreEqual(97,Strings.Asc("a"));
			Assert.AreEqual(97,Strings.Asc("abc"));
			Assert.AreEqual(230, Strings.Asc ("æ"));
			Assert.AreEqual(65,Strings.Asc("A"));
			Assert.AreEqual(33,Strings.Asc("!"));
			Assert.AreEqual(51,Strings.Asc("3"));
			Assert.AreEqual(43,Strings.Asc("+"));
			
			Assert.AreEqual(63,Strings.Asc("\u1994"));
			Assert.AreEqual(63,Strings.Asc("\u5666"));
			Assert.AreEqual(63,Strings.Asc("耀"));
			Assert.AreEqual(63,Strings.Asc("耂"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Asc2() 
		{
			int i = Strings.Asc(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Asc3() 
		{
			int i = Strings.Asc("");
		}

		#endregion

		#region AscW Tests

		[Test]
		public void AscW1() 
		{
			Assert.AreEqual(51,Strings.AscW("3"));
			Assert.AreEqual(33,Strings.AscW("!"));
			Assert.AreEqual(65,Strings.AscW("A"));
			//Assert.AreEqual(1494,Strings.AscW("×–"));
			Assert.AreEqual(97,Strings.AscW("abc"));
			Assert.AreEqual(97,Strings.AscW("a"));

			Assert.AreEqual(6548,Strings.AscW("\u1994"));
			Assert.AreEqual(22118,Strings.AscW("\u5666"));
			Assert.AreEqual(32768,Strings.AscW("\u8000"));
			Assert.AreEqual(32770,Strings.AscW("\u8002"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void AscW2() 
		{
			int i = Strings.AscW(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void AscW3() 
		{
			int i = Strings.AscW("");
		}

		#endregion

		#region Chr Tests

		[Test]
		public void ChrTest()
		{
			Assert.AreEqual (System.Convert.ToChar(0), Microsoft.VisualBasic.Strings.Chr(0), "Chr#0");
			Assert.AreEqual (System.Convert.ToChar(1), Microsoft.VisualBasic.Strings.Chr(1), "Chr#1");
			Assert.AreEqual (System.Convert.ToChar(127), Microsoft.VisualBasic.Strings.Chr(127), "Chr#2");
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void ChrTestArgumentException1()
		{
			// CharCode < -32768 Or CharCode > 65535
			int MinValue = -32768;
			Char c;
			c = Microsoft.VisualBasic.Strings.Chr(MinValue - 1);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void ChrTestArgumentException2()
		{
			// Negative value
			Char c;
			c = Microsoft.VisualBasic.Strings.Chr(-1);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void ChrTestArgumentException3()
		{
			// higher than 255 value
			Char c;
			c = Microsoft.VisualBasic.Strings.Chr(256);
		}

		#endregion

		#region ChrW Tests

		[Test]
		public void ChrWTest()
		{
			Assert.AreEqual (System.Convert.ToChar(0), Microsoft.VisualBasic.Strings.ChrW(0), "ChrW#0");
			Assert.AreEqual (System.Convert.ToChar(1), Microsoft.VisualBasic.Strings.ChrW(1), "ChrW#1");
			Assert.AreEqual ('A', Microsoft.VisualBasic.Strings.ChrW(65), "ChrW#Number1");
			Assert.AreEqual (Microsoft.VisualBasic.Strings.ChrW(-1000), Microsoft.VisualBasic.Strings.ChrW(-1000 + 65536), "ChrW#Unicode1");
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void ChrWTestArgumentException1()
		{
			// CharCode < -32768 Or CharCode > 65535
			int MinValue = -32768;
			Char c;
			c = Microsoft.VisualBasic.Strings.ChrW(MinValue - 1);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void ChrWTestArgumentException2()
		{
			// CharCode < -32768 Or CharCode > 65535
			int MaxValue = 65535;
			Char c;
			c = Microsoft.VisualBasic.Strings.ChrW(MaxValue + 1);
		}
		//
		//		[Test]
		//		[ExpectedException (typeof(InvalidCastException))]
		//		public void ChrWTestInvalidCastException1()
		//		{
		//			string s = "test";
		//			Char c;
		//			c = Microsoft.VisualBasic.Strings.ChrW(s);
		//		}

		#endregion

		#region Filter Tests

		[Test]
		public void Filter1()
		{
			string[] arr = new string[] {"Test","ss","aaaaaaaaaaa"};
			string[] res = Strings.Filter(arr,"s",false,CompareMethod.Text);
			Assert.AreEqual("aaaaaaaaaaa",res[0]);
		}

		[Test]
		public void Filter2()
		{
			string[] arr = new string[] {"Dado","Ben","David"};
			string[] res = Strings.Filter(arr,"Da",true,CompareMethod.Binary);
			Assert.AreEqual("Dado",res[0]);
			Assert.AreEqual("David",res[1]);
		}

		[Test]
		public void Filter3()
		{
			string[] arr = new string[] {"AAA","BBB","CCC"};
			string[] res = Strings.Filter(arr,"b",true,CompareMethod.Binary);
			Assert.AreEqual(0,res.Length);
		}

		[Test]
			//[ExpectedException (typeof(ArgumentException))]
		[ExpectedException (typeof(NullReferenceException))]
		public void Filter4()
		{
			string[] res = Strings.Filter(null,"abc",true,CompareMethod.Text);
		}

		#endregion

		#region Format Tests		

		[Test]
		public void Format_Numeric_GeneralNumber()
		{
			Assert.AreEqual("1234.4321",Strings.Format(1234.4321, "General Number"));
			Assert.AreEqual("1234.4321",Strings.Format(1234.4321, "G"));
			Assert.AreEqual("1234.4321",Strings.Format(1234.4321, "g"));

			Assert.AreEqual("GeneralNumber",Strings.Format(1234.4321, "GeneralNumber"));
			Assert.AreEqual("G ",Strings.Format(1234.4321, "G "));
			Assert.AreEqual("GGG",Strings.Format(1234.4321, "GGG"));
		}

		[Test]
		public void Format_Numeric_Currency()
		{
			Assert.AreEqual("$1,234.43",Strings.Format(1234.4321, "Currency"));
			Assert.AreEqual("$1,234.43",Strings.Format(1234.4321, "C"));
			Assert.AreEqual("$1,234.43",Strings.Format(1234.4321, "c"));

			Assert.AreEqual("C ",Strings.Format(1234.4321, "C "));
			Assert.AreEqual("ccc",Strings.Format(1234.4321, "ccc"));
		}

		[Test]
		public void Format_Numeric_Fixed()
		{
			Assert.AreEqual("1234.43",Strings.Format(1234.4321, "Fixed"));
			Assert.AreEqual("1234.43",Strings.Format(1234.4321, "F"));
			Assert.AreEqual("1234.43",Strings.Format(1234.4321, "f"));

			Assert.AreEqual("Fixed " ,Strings.Format(1234.4321, "Fixed "));
			Assert.AreEqual("F ",Strings.Format(1234.4321, "F "));
			Assert.AreEqual("f ",Strings.Format(1234.4321, "f "));
		}

		[Test]
		public void Format_Numeric_Standard()
		{
			Assert.AreEqual("1,234.43",Strings.Format(1234.4321, "Standard"));
			Assert.AreEqual("1,234.43",Strings.Format(1234.4321, "N"));
			Assert.AreEqual("1,234.43",Strings.Format(1234.4321, "n"));

			Assert.AreEqual("n ",Strings.Format(1234.4321, "n "));
			Assert.AreEqual("N ",Strings.Format(1234.4321, "N "));
		}

		[Test]
		public void Format_Numeric_Percent()
		{
			Assert.AreEqual("123443.21%",Strings.Format(1234.4321, "Percent"));
			Assert.AreEqual("123443.21%",Strings.Format(1234.4321, "percent"));
			Assert.AreEqual("123,443.21 %",Strings.Format(1234.4321, "P"));
			Assert.AreEqual("123,443.21 %",Strings.Format(1234.4321, "p"));
			Assert.AreEqual("%",Strings.Format(1234.4321, "%"));
		}

		[Test]
		public void Format_Numeric_Scientific()
		{
			Assert.AreEqual("1.23E+03",Strings.Format(1234.4321, "Scientific"));
			Assert.AreEqual("1.234432E+003",Strings.Format(1234.4321, "E"));
			Assert.AreEqual("1.234432e+003",Strings.Format(1234.4321, "e"));
			Assert.AreEqual("e ",Strings.Format(1234.4321, "e "));
			Assert.AreEqual("E ",Strings.Format(1234.4321, "E "));
		}

		[Test]
		public void Format_Numeric_Hexadecimal()
		{
			Assert.AreEqual("4D2",Strings.Format(1234, "X"));
			Assert.AreEqual("4d2",Strings.Format(1234, "x"));
			Assert.AreEqual("x ",Strings.Format(1234.4321, "x "));
			Assert.AreEqual("X ",Strings.Format(1234.4321, "X "));
		}

		[Test]
		public void Format_Numeric_YesNo()
		{
			Assert.AreEqual("Yes",Strings.Format(1234.4321, "Yes/No"));
			Assert.AreEqual("No",Strings.Format(0, "Yes/No"));
			Assert.AreEqual("Yes",Strings.Format(-12, "Yes/No"));
			Assert.AreEqual("Yes",Strings.Format(123, "yes/no"));
			Assert.AreEqual("No/Yes",Strings.Format(0, "No/Yes"));
			Assert.AreEqual("-no",Strings.Format(-12, "no"));
		}

		[Test]
		public void Format_Numeric_TrueFalse()
		{
			Assert.AreEqual("True",Strings.Format(1, "True/False"));
			Assert.AreEqual("False",Strings.Format(0, "True/False"));
			Assert.AreEqual("True",Strings.Format(-1, "True/False"));
		}

		[Test]
		public void Format_Numeric_OnOff()
		{
			Assert.AreEqual("On",Strings.Format(1234.4321, "On/Off"));			
			Assert.AreEqual("On",Strings.Format(-12, "ON/OFF"));
			Assert.AreEqual("ON/OF",Strings.Format(0, "ON/OF"));
		}

		[Test]
		public void Format_Numeric_UserDefined1()
		{
			Assert.AreEqual("5",Strings.Format(5, ""));			
			Assert.AreEqual("-5",Strings.Format(-5, ""));
			Assert.AreEqual("0.5",Strings.Format(0.5, ""));
			
			Assert.AreEqual("5",Strings.Format(5, "0"));			
			Assert.AreEqual("-5",Strings.Format(-5, "0"));
			Assert.AreEqual("1",Strings.Format(0.5, "0"));

			Assert.AreEqual("5.00",Strings.Format(5, "0.00"));			
			Assert.AreEqual("-5.00",Strings.Format(-5, "0.00"));
			Assert.AreEqual("0.50",Strings.Format(0.5, "0.00"));

			Assert.AreEqual("5",Strings.Format(5, "#,##0"));			
			Assert.AreEqual("-5",Strings.Format(-5, "#,##0"));
			Assert.AreEqual("1",Strings.Format(0.5, "#,##0"));
		}

		[Test]
		public void Format_Numeric_UserDefined2()
		{
			Assert.AreEqual("$5",Strings.Format(5, "$#,##0;($#,##0)"));			
			Assert.AreEqual("($5)",Strings.Format(-5, "$#,##0;($#,##0)"));
			Assert.AreEqual("$1",Strings.Format(0.5, "$#,##0;($#,##0)"));

			Assert.AreEqual("$5.00",Strings.Format(5, "$#,##0.00;($#,##0.00)"));			
			Assert.AreEqual("($5.00)",Strings.Format(-5, "$#,##0.00;($#,##0.00)"));
			Assert.AreEqual("$0.50",Strings.Format(0.5, "$#,##0.00;($#,##0.00)"));

			Assert.AreEqual("500%",Strings.Format(5, "0%"));			
			Assert.AreEqual("-500%",Strings.Format(-5, "0%"));
			Assert.AreEqual("50%",Strings.Format(0.5, "0%"));

			Assert.AreEqual("500.00%",Strings.Format(5, "0.00%"));			
			Assert.AreEqual("-500.00%",Strings.Format(-5, "0.00%"));
			Assert.AreEqual("50.00%",Strings.Format(0.5, "0.00%"));

			Assert.AreEqual("5.00E+00",Strings.Format(5, "0.00E+00"));			
			Assert.AreEqual("-5.00E+00",Strings.Format(-5, "0.00E+00"));
			Assert.AreEqual("5.00E-01",Strings.Format(0.5, "0.00E+00"));
			
			Assert.AreEqual("5.00E00",Strings.Format(5, "0.00E-00"));			
			Assert.AreEqual("-5.00E00",Strings.Format(-5, "0.00E-00"));
			Assert.AreEqual("5.00E-01",Strings.Format(0.5, "0.00E-00"));
		}

		[Test]
		public void Format_Numeric_UserDefined3()
		{
			Assert.AreEqual("5",Strings.Format(5, null));		
			Assert.AreEqual("5",Strings.Format(5, String.Empty));
		}

		[Test]
		public void Format_Date1()
		{
			DateTime d = new DateTime (2006, 6, 19, 14, 22, 35, 78);

			Assert.AreEqual("6/19/2006 2:22:35 PM",Strings.Format(d, "General Date"));	
			Assert.AreEqual("6/19/2006 2:22:35 PM",Strings.Format(d, "G"));	

			Assert.AreEqual("Monday, June 19, 2006",Strings.Format(d, "Long Date"));
			Assert.AreEqual("Monday, June 19, 2006",Strings.Format(d, "D"));	
			
			Assert.AreEqual("Monday, June 19, 2006",Strings.Format(d, "Medium Date"));

			Assert.AreEqual("6/19/2006",Strings.Format(d, "Short Date"));
			Assert.AreEqual("6/19/2006",Strings.Format(d, "d"));

			Assert.AreEqual("2:22:35 PM",Strings.Format(d, "Long Time"));	
			Assert.AreEqual("2:22:35 PM",Strings.Format(d, "T"));

			Assert.AreEqual("2:22:35 PM",Strings.Format(d, "Medium Time"));	

			Assert.AreEqual("2:22 PM",Strings.Format(d, "Short Time"));	
			Assert.AreEqual("2:22 PM",Strings.Format(d, "t"));	

			Assert.AreEqual("Monday, June 19, 2006 2:22:35 PM",Strings.Format(d, "F"));
			Assert.AreEqual("Monday, June 19, 2006 2:22 PM",Strings.Format(d, "f"));

			Assert.AreEqual("6/19/2006 2:22 PM",Strings.Format(d, "g"));

			Assert.AreEqual("June 19",Strings.Format(d, "M"));
			Assert.AreEqual("June 19",Strings.Format(d, "m"));

			Assert.AreEqual("Mon, 19 Jun 2006 14:22:35 GMT",Strings.Format(d, "R"));
			Assert.AreEqual("Mon, 19 Jun 2006 14:22:35 GMT",Strings.Format(d, "r"));

			Assert.AreEqual("2006-06-19T14:22:35",Strings.Format(d, "s"));

#if FIXME // FIXME - these scenario is timezone specific
			if (d.IsDaylightSavingTime ()) {
				Assert.AreEqual("Monday, June 19, 2006 11:22:35 AM", Strings.Format (d, "U"));	
			} else {
				Assert.AreEqual("Monday, June 19, 2006 12:22:35 PM",Strings.Format(d, "U"));
			}
#endif

			Assert.AreEqual("2006-06-19 14:22:35Z",Strings.Format(d, "u"));

			Assert.AreEqual("June, 2006",Strings.Format(d, "Y"));
			Assert.AreEqual("June, 2006",Strings.Format(d, "y"));
				

			Assert.IsTrue(Strings.Format(DateTime.Now, "D") == Strings.Format(DateTime.Now, "Long Date"));
		}

		[Test]
		public void Format_Date_UserDefined1()
		{
			DateTime d = new DateTime (2001,1,27,5,4,23,67);
			
			Assert.AreEqual("5:4:23",Strings.Format(d, "h:m:s"));		
			Assert.AreEqual("05:04:23 AM",Strings.Format(d, "hh:mm:ss tt"));	
			Assert.AreEqual("05:04:23",Strings.Format(d, "HH:mm:ss"));	
			Assert.AreEqual("Saturday, January, 27, 2001",Strings.Format(d, "dddd, MMMM, d, yyyy"));	
			Assert.AreEqual("Saturday, Jan, 27, 2001",Strings.Format(d, "dddd, MMM, d, yyyy"));
			Assert.AreEqual("Saturday, 01, 27, 2001",Strings.Format(d, "dddd, MM, d, yyyy"));
		}

		[Test]
		[Category("TargetJvmNotWorking")]
		public void Format_Date_UserDefined2()
		{
			DateTime d = new DateTime (2006, 6, 19, 14, 22, 35, 78);
			
			Assert.AreEqual("GeneralDaPe",Strings.Format(d, "GeneralDate"));			
			Assert.AreEqual("LonA.D.DaPe",Strings.Format(d, "LongDate"));			
			Assert.AreEqual("6e19iu22DaPe",Strings.Format(d, "MediumDate"));
			Assert.AreEqual("S2orPDaPe",Strings.Format(d, "ShortDate"));
			Assert.AreEqual("LonA.D.Ti22e",Strings.Format(d, "LongTime"));	
			Assert.AreEqual("6e19iu22Ti22e",Strings.Format(d, "MediumTime"));
			Assert.AreEqual("S2orPTi22e",Strings.Format(d, "ShortTime"));	
		}

		[Test]
		public void Format_Date_UserDefined3()
		{
			DateTime d = new DateTime (2006, 6, 19, 14, 22, 35, 78);
			
			Assert.AreEqual("-1",Strings.Format(d, "-1"));
			Assert.AreEqual("6/19/2006 2:22:35 PM",Strings.Format(d, null));
			Assert.AreEqual("6/19/2006 2:22:35 PM",Strings.Format(d, String.Empty));
		}

		[Test]
		public void Format_Null()
		{			
			Assert.AreEqual(String.Empty,Strings.Format(null, "General Number"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "G"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "g"));
			
			Assert.AreEqual(String.Empty,Strings.Format(null, "Currency"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "C"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "c"));
			
			Assert.AreEqual(String.Empty,Strings.Format(null, "Fixed"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "F"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "f"));
			
			Assert.AreEqual(String.Empty,Strings.Format(null, "Standard"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "N"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "n"));
			
			Assert.AreEqual(String.Empty,Strings.Format(null, "Percent"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "percent"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "P"));			
			Assert.AreEqual(String.Empty,Strings.Format(null, "P"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "%"));

			Assert.AreEqual(String.Empty,Strings.Format(null, "Scientific"));			
			Assert.AreEqual(String.Empty,Strings.Format(null, "E"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "e"));

			Assert.AreEqual(String.Empty,Strings.Format(null, "X"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "x"));

			Assert.AreEqual(String.Empty,Strings.Format(null, "Yes/No"));
						
			Assert.AreEqual(String.Empty,Strings.Format(null, "True/False"));

			Assert.AreEqual(String.Empty,Strings.Format(null, "On/Off"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "ON/OFF"));

			Assert.AreEqual(String.Empty,Strings.Format(null, "General Date"));
			Assert.AreEqual(String.Empty,Strings.Format(null, "GeneralDate"));
		}

		#endregion

		#region FormatCurrency Tests

		//FIXME : test default parameter values
		[Test]
		public void FormatCurrency1()
		{
			Assert.AreEqual("($2,344.33)",Strings.FormatCurrency(-2344.33,-1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("$234.430",Strings.FormatCurrency(234.43,3,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("$234.4",Strings.FormatCurrency(234.43,1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatCurrency_TriState1()
		{
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.True,TriState.UseDefault,TriState.UseDefault));
		}
		
		[Test]
		public void FormatCurrency_TriState2()
		{
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-$123.42",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.False,TriState.UseDefault));
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.True,TriState.UseDefault));
		}
		
		[Test]
		public void FormatCurrency_TriState3()
		{
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.UseDefault,TriState.False));
			Assert.AreEqual("($123.42)",Strings.FormatCurrency(-123.42,2,TriState.UseDefault,TriState.UseDefault,TriState.True));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void FormatCurrency2()
		{
			string s = Strings.FormatCurrency(-123.42,100,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault);
		}

		[Test]
		public void FormatCurrency3()
		{
			Assert.AreEqual("$234.430000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",Strings.FormatCurrency(234.43,99,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FormatCurrency4()
		{
			string s = Strings.FormatCurrency("abc",99,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault);
		}


		#endregion

		#region FormatDateTime Tests

		[Test]
		public void FormatDateTime_GeneralDate()
		{
            DateTime tmptime = new DateTime(1, 1, 1,1,1,1);
            DateTime tmptime1 = new DateTime(2000, 12, 5, 3, 23, 45, 5);
            //DateTime tmptime2 = new DateTime(DateTime.MaxValue);

			Assert.AreEqual("12/30/1215",Strings.FormatDateTime(DateTime.Parse("12/30/1215"),DateFormat.GeneralDate));
            Assert.AreEqual("9/11/2038",Strings.FormatDateTime(DateTime.Parse("9/11/2038"),DateFormat.GeneralDate));
			Assert.AreEqual("10/9/1001",Strings.FormatDateTime(DateTime.Parse("10/9/1001"),DateFormat.GeneralDate));
			Assert.AreEqual("9/24/1918",Strings.FormatDateTime(DateTime.Parse("9/24/1918"),DateFormat.GeneralDate));
			Assert.AreEqual("2/11/1946",Strings.FormatDateTime(DateTime.Parse("2/11/1946"),DateFormat.GeneralDate));
			Assert.AreEqual("5/1/1980",Strings.FormatDateTime(DateTime.Parse("5/1/1980"),DateFormat.GeneralDate));
			Assert.AreEqual("2/28/2001",Strings.FormatDateTime(DateTime.Parse("2/28/2001"),DateFormat.GeneralDate));
			Assert.AreEqual("3/3/2003",Strings.FormatDateTime(DateTime.Parse("3/3/2003"),DateFormat.GeneralDate));
			Assert.AreEqual("9/10/1972",Strings.FormatDateTime(DateTime.Parse("9/10/1972"),DateFormat.GeneralDate));
			Assert.AreEqual("1/12/1487",Strings.FormatDateTime(DateTime.Parse("1/12/1487"),DateFormat.GeneralDate));
			Assert.AreEqual("7/7/0100",Strings.FormatDateTime(DateTime.Parse("7/7/0100"),DateFormat.GeneralDate));
			Assert.AreEqual("2/1/2022",Strings.FormatDateTime(DateTime.Parse("2/1/22"),DateFormat.GeneralDate));
			Assert.AreEqual("6/6/0666",Strings.FormatDateTime(DateTime.Parse("6/6/0666"),DateFormat.GeneralDate));
			Assert.AreEqual("1/1/2000",Strings.FormatDateTime(DateTime.Parse("1/1/2000"),DateFormat.GeneralDate));
			Assert.AreEqual("12/31/2000",Strings.FormatDateTime(DateTime.Parse("12/31/2000"),DateFormat.GeneralDate));
			Assert.AreEqual("5/5/1000",Strings.FormatDateTime(DateTime.Parse("5/5/1000"),DateFormat.GeneralDate));
			Assert.AreEqual("1/1/1970",Strings.FormatDateTime(DateTime.Parse("1/1/1970"),DateFormat.GeneralDate));
			Assert.AreEqual("2/2/2002",Strings.FormatDateTime(DateTime.Parse("2/2/2002"),DateFormat.GeneralDate));
            
			Assert.AreEqual(tmptime.ToLongTimeString(),Strings.FormatDateTime(new DateTime(1,1,1,1,1,1,1),DateFormat.GeneralDate),"DT11");
            //  Assert.AreEqual(tmptime.ToLongTimeString(), Strings.FormatDateTime(DateTime.MinValue, DateFormat.GeneralDate));
            //  Assert.AreEqual(tmptime.ToLongDateString() + tmptime.ToLongTimeString(), Strings.FormatDateTime(DateTime.MaxValue, DateFormat.GeneralDate));
		    //Assert.AreEqual(tmptime.ToLongDateString() + tmptime.ToLongTimeString(),Strings.FormatDateTime(new DateTime(2000,12,5,3,23,45,5),DateFormat.GeneralDate));
		}

		[Test]
		public void FormatDateTime_LongDate()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81535");
				
			Assert.AreEqual("Wednesday, December 30, 1215",Strings.FormatDateTime(DateTime.Parse("12/30/1215"),DateFormat.LongDate));
			Assert.AreEqual("Saturday, September 11, 2038",Strings.FormatDateTime(DateTime.Parse("9/11/2038"),DateFormat.LongDate));
			Assert.AreEqual("Friday, October 09, 1001",Strings.FormatDateTime(DateTime.Parse("10/9/1001"),DateFormat.LongDate));
			Assert.AreEqual("Tuesday, September 24, 1918",Strings.FormatDateTime(DateTime.Parse("9/24/1918"),DateFormat.LongDate));
			Assert.AreEqual("Monday, February 11, 1946",Strings.FormatDateTime(DateTime.Parse("2/11/1946"),DateFormat.LongDate));
			Assert.AreEqual("Thursday, May 01, 1980",Strings.FormatDateTime(DateTime.Parse("5/1/1980"),DateFormat.LongDate));
			Assert.AreEqual("Wednesday, February 28, 2001",Strings.FormatDateTime(DateTime.Parse("2/28/2001"),DateFormat.LongDate));
			Assert.AreEqual("Monday, March 03, 2003",Strings.FormatDateTime(DateTime.Parse("3/3/2003"),DateFormat.LongDate));
			Assert.AreEqual("Sunday, September 10, 1972",Strings.FormatDateTime(DateTime.Parse("9/10/1972"),DateFormat.LongDate));
			Assert.AreEqual("Wednesday, January 12, 1487",Strings.FormatDateTime(DateTime.Parse("1/12/1487"),DateFormat.LongDate));
			Assert.AreEqual("Wednesday, July 07, 0100",Strings.FormatDateTime(DateTime.Parse("7/7/100"),DateFormat.LongDate));
			Assert.AreEqual("Tuesday, February 01, 2022",Strings.FormatDateTime(DateTime.Parse("2/1/22"),DateFormat.LongDate));
			Assert.AreEqual("Wednesday, June 06, 0666",Strings.FormatDateTime(DateTime.Parse("6/6/666"),DateFormat.LongDate));
			Assert.AreEqual("Saturday, January 01, 2000",Strings.FormatDateTime(DateTime.Parse("1/1/2000"),DateFormat.LongDate));
			Assert.AreEqual("Sunday, December 31, 2000",Strings.FormatDateTime(DateTime.Parse("12/31/2000"),DateFormat.LongDate));
			Assert.AreEqual("Monday, May 05, 1000",Strings.FormatDateTime(DateTime.Parse("5/5/1000"),DateFormat.LongDate));
			Assert.AreEqual("Thursday, January 01, 1970",Strings.FormatDateTime(DateTime.Parse("1/1/1970"),DateFormat.LongDate));
			Assert.AreEqual("Saturday, February 02, 2002",Strings.FormatDateTime(DateTime.Parse("2/2/2002"),DateFormat.LongDate));
			
			Assert.AreEqual("Monday, January 01, 0001",Strings.FormatDateTime(new DateTime(1,1,1,1,1,1,1),DateFormat.LongDate));
			Assert.AreEqual("Tuesday, December 05, 2000",Strings.FormatDateTime(new DateTime(2000,12,5,3,23,45,5),DateFormat.LongDate));
		}

		[Test]
		public void FormatDateTime_ShortDate()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81535");
				
			Assert.AreEqual("12/30/1215",Strings.FormatDateTime(DateTime.Parse("12/30/1215"),DateFormat.ShortDate));
			Assert.AreEqual("9/11/2038",Strings.FormatDateTime(DateTime.Parse("9/11/2038"),DateFormat.ShortDate));
			Assert.AreEqual("10/9/1001",Strings.FormatDateTime(DateTime.Parse("10/9/1001"),DateFormat.ShortDate));
			Assert.AreEqual("9/24/1918",Strings.FormatDateTime(DateTime.Parse("9/24/1918"),DateFormat.ShortDate));
			Assert.AreEqual("2/11/1946",Strings.FormatDateTime(DateTime.Parse("2/11/1946"),DateFormat.ShortDate));
			Assert.AreEqual("5/1/1980",Strings.FormatDateTime(DateTime.Parse("5/1/1980"),DateFormat.ShortDate));
			Assert.AreEqual("2/28/2001",Strings.FormatDateTime(DateTime.Parse("2/28/2001"),DateFormat.ShortDate));
			Assert.AreEqual("3/3/2003",Strings.FormatDateTime(DateTime.Parse("3/3/2003"),DateFormat.ShortDate));
			Assert.AreEqual("9/10/1972",Strings.FormatDateTime(DateTime.Parse("9/10/1972"),DateFormat.ShortDate));
			Assert.AreEqual("1/12/1487",Strings.FormatDateTime(DateTime.Parse("1/12/1487"),DateFormat.ShortDate));
			Assert.AreEqual("7/7/0100",Strings.FormatDateTime(DateTime.Parse("7/7/100"),DateFormat.ShortDate));
			Assert.AreEqual("2/1/2022",Strings.FormatDateTime(DateTime.Parse("2/1/22"),DateFormat.ShortDate));
			Assert.AreEqual("6/6/0666",Strings.FormatDateTime(DateTime.Parse("6/6/666"),DateFormat.ShortDate));
			Assert.AreEqual("1/1/2000",Strings.FormatDateTime(DateTime.Parse("1/1/2000"),DateFormat.ShortDate));
			Assert.AreEqual("12/31/2000",Strings.FormatDateTime(DateTime.Parse("12/31/2000"),DateFormat.ShortDate));
			Assert.AreEqual("5/5/1000",Strings.FormatDateTime(DateTime.Parse("5/5/1000"),DateFormat.ShortDate));
			Assert.AreEqual("1/1/1970",Strings.FormatDateTime(DateTime.Parse("1/1/1970"),DateFormat.ShortDate));
			Assert.AreEqual("2/2/2002",Strings.FormatDateTime(DateTime.Parse("2/2/2002"),DateFormat.GeneralDate));
			
			Assert.AreEqual("1/1/0001",Strings.FormatDateTime(new DateTime(1,1,1,1,1,1,1),DateFormat.ShortDate));
			Assert.AreEqual("12/5/2000",Strings.FormatDateTime(new DateTime(2000,12,5,3,23,45,5),DateFormat.ShortDate));
		}

		[Test]
		public void FormatDateTime_LongTime()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81535");
				
            DateTime tmptime = new DateTime(1, 10, 1);
            DateTime tmptime1 = new DateTime(1, 10, 1,1,1,1);
            DateTime tmptime2 = new DateTime(1, 10, 1,3,23,45);
            string strTime = tmptime.ToLongTimeString();


            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("12/30/1215"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("9/11/2038"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("10/9/1001"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("9/24/1918"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("2/11/1946"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("5/1/1980"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("2/28/2001"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("3/3/2003"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("9/10/1972"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("1/12/1487"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("7/7/100"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("2/1/22"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("6/6/666"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("1/1/2000"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("12/31/2000"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("5/5/1000"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("1/1/1970"), DateFormat.LongTime));
            Assert.AreEqual(strTime, Strings.FormatDateTime(DateTime.Parse("2/2/2002"), DateFormat.LongTime));

            Assert.AreEqual(tmptime1.ToLongTimeString(), Strings.FormatDateTime(new DateTime(1, 1, 1, 1, 1, 1, 1), DateFormat.LongTime));
            Assert.AreEqual(tmptime2.ToLongTimeString(), Strings.FormatDateTime(new DateTime(2000, 12, 5, 3, 23, 45, 5), DateFormat.LongTime));
		}

		[Test]
		public void FormatDateTime_ShortTime()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81535");
		
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("12/30/1215"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("9/11/2038"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("10/9/1001"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("9/24/1918"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("2/11/1946"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("5/1/1980"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("2/28/2001"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("3/3/2003"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("9/10/1972"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("1/12/1487"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("7/7/100"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("2/1/22"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("6/6/666"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("1/1/2000"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("12/31/2000"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("5/5/1000"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("1/1/1970"),DateFormat.ShortTime));
			Assert.AreEqual("00:00",Strings.FormatDateTime(DateTime.Parse("2/2/2002"),DateFormat.ShortTime));
			
			Assert.AreEqual("01:01",Strings.FormatDateTime(new DateTime(1,1,1,1,1,1,1),DateFormat.ShortTime));
			Assert.AreEqual("03:23",Strings.FormatDateTime(new DateTime(2000,12,5,3,23,45,5),DateFormat.ShortTime));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void FormatDateTime1()
		{
			Strings.FormatDateTime(new DateTime(2000,12,5,3,23,45,5),(DateFormat) 100);
		}

		#endregion

		#region FormatNumber tests

		[Test]
		public void FormatNumber1()
		{
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,-1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.000",Strings.FormatNumber(-123,3,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.0",Strings.FormatNumber(-123,1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,-1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.000",Strings.FormatNumber(4500,3,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.0",Strings.FormatNumber(4500,1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatNumber_TriState1()
		{
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("-0.12",Strings.FormatNumber(-0.123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-.12",Strings.FormatNumber(-0.123,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-0.12",Strings.FormatNumber(-0.123,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("0.45",Strings.FormatNumber(0.4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual(".45",Strings.FormatNumber(0.4500,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("0.45",Strings.FormatNumber(0.4500,2,TriState.True,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatNumber_TriState2()
		{
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.False,TriState.UseDefault));
			Assert.AreEqual("(123.00)",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.True,TriState.UseDefault));

			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.False,TriState.UseDefault));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.True,TriState.UseDefault));
		}

		[Test]
		public void FormatNumber_TriState3()
		{
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.False));
			Assert.AreEqual("-123.00",Strings.FormatNumber(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.True));

			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("4500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.False));
			Assert.AreEqual("4,500.00",Strings.FormatNumber(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.True));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FormatNumber2()
		{
			object o = "abcd";
			Strings.FormatNumber(o,2,TriState.UseDefault,TriState.UseDefault,TriState.True);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void FormatNumber3()
		{
			string s = Strings.FormatNumber(-123.42,100,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault);
		}

		[Test]
		public void FormatNumber4()
		{
			Assert.AreEqual("234.430000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",Strings.FormatNumber(234.43,99,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}


		#endregion

		#region FormatPercent tests

		[Test]
		public void FormatPercent1()
		{
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,-1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.000%",Strings.FormatPercent(-123,3,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.0%",Strings.FormatPercent(-123,1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,-1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.000%",Strings.FormatPercent(4500,3,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.0%",Strings.FormatPercent(4500,1,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatPercent_TriState1()
		{
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("-12.30%",Strings.FormatPercent(-0.123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12.30%",Strings.FormatPercent(-0.123,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12.30%",Strings.FormatPercent(-0.123,2,TriState.True,TriState.UseDefault,TriState.UseDefault));

			Assert.AreEqual("45.00%",Strings.FormatPercent(0.4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("45.00%",Strings.FormatPercent(0.4500,2,TriState.False,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("45.00%",Strings.FormatPercent(0.4500,2,TriState.True,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatPercent_TriState2()
		{
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.False,TriState.UseDefault));
			Assert.AreEqual("(12,300.00%)",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.True,TriState.UseDefault));

			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.False,TriState.UseDefault));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.True,TriState.UseDefault));
		}

		[Test]
		public void FormatPercent_TriState3()
		{
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("-12300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.False));
			Assert.AreEqual("-12,300.00%",Strings.FormatPercent(-123,2,TriState.UseDefault,TriState.UseDefault,TriState.True));

			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
			Assert.AreEqual("450000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.False));
			Assert.AreEqual("450,000.00%",Strings.FormatPercent(4500,2,TriState.UseDefault,TriState.UseDefault,TriState.True));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void FormatPercent2()
		{
			object o = "abcd";
			Strings.FormatPercent(o,2,TriState.UseDefault,TriState.UseDefault,TriState.True);
		}

		[Test]
		public void FormatPercent3()
		{
			Assert.AreEqual("23,443.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000%",Strings.FormatPercent(234.43,110,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}

		[Test]
		public void FormatPercent4()
		{
			Assert.AreEqual("23,443.000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000%",Strings.FormatPercent(234.43,99,TriState.UseDefault,TriState.UseDefault,TriState.UseDefault));
		}


		#endregion

		#region GetChar Tests

		[Test]
		public void GetChar_1()
		{
			Assert.AreEqual('a',Strings.GetChar("abCd",1));
			Assert.AreEqual('b',Strings.GetChar("abCd",2));
			Assert.AreEqual('C',Strings.GetChar("abCd",3));
			Assert.AreEqual('d',Strings.GetChar("abCd",4));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void GetChar_2()
		{
			Strings.GetChar(null,1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void GetChar_3()
		{
			Strings.GetChar("abc",-1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void GetChar_4()
		{
			Strings.GetChar("abc",0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void GetChar_5()
		{
			Strings.GetChar("abc",4);
		}

		#endregion

		#region InStr Tests

		[Test]
		public void InStr_1()
		{
			Assert.AreEqual(2,Strings.InStr("abcd","b",CompareMethod.Text));
			Assert.AreEqual(2,Strings.InStr(2,"abcd","b",CompareMethod.Text));
			Assert.AreEqual(0,Strings.InStr(3,"abcd","b",CompareMethod.Text));

			Assert.AreEqual(0,Strings.InStr("abcd","B",CompareMethod.Binary));
			Assert.AreEqual(2,Strings.InStr("abcd","B",CompareMethod.Text));
		}

		[Test]
		public void InStr_2()
		{
			Assert.AreEqual(0,Strings.InStr(2,"","B",CompareMethod.Text));
			Assert.AreEqual(0,Strings.InStr(2,null,"B",CompareMethod.Text));

			Assert.AreEqual(2,Strings.InStr(2,"abcd",null,CompareMethod.Text));
			Assert.AreEqual(2,Strings.InStr(2,"abcd","",CompareMethod.Text));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void InStr_3()
		{
			Strings.InStr(0,"abcd","b",CompareMethod.Text);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void InStr_4()
		{
			Strings.InStr(-1,"abcd","b",CompareMethod.Text);
		}

		#endregion

		#region InStrRev Tests

		[Test]
		public void InStrRev_1()
		{
			Assert.AreEqual(2,Strings.InStrRev("abcd","b",-1,CompareMethod.Text));
			Assert.AreEqual(2,Strings.InStrRev("abcd","b",2,CompareMethod.Text));
			Assert.AreEqual(0,Strings.InStrRev("abcd","b",1,CompareMethod.Text));

			Assert.AreEqual(5,Strings.InStrRev("abcdb","b",-1,CompareMethod.Text));
			Assert.AreEqual(2,Strings.InStrRev("abcdb","b",3,CompareMethod.Text));
			Assert.AreEqual(0,Strings.InStrRev("abcdb","b",1,CompareMethod.Text));

			Assert.AreEqual(0,Strings.InStrRev("abcd","B",-1,CompareMethod.Binary));
			Assert.AreEqual(2,Strings.InStrRev("abcd","B",-1,CompareMethod.Text));
		}

		[Test]
		public void InStrRev_2()
		{
			Assert.AreEqual(0,Strings.InStrRev("","B",2,CompareMethod.Text));
			Assert.AreEqual(0,Strings.InStrRev(null,"B",2,CompareMethod.Text));

			Assert.AreEqual(2,Strings.InStrRev("abcd",null,2,CompareMethod.Text));
			Assert.AreEqual(2,Strings.InStrRev("abcd","",2,CompareMethod.Text));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IInStrRev_3()
		{
			Strings.InStrRev("abcd","b",0,CompareMethod.Text);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void InStrRev_4()
		{
			Strings.InStrRev("abcd","b",-2,CompareMethod.Text);
		}

		#endregion

		#region Join Tests

		[Test]
		public void Join_1()
		{
			string[] sarr = new string[] {"abc", "def", "ghi"};

			Assert.AreEqual("abc def ghi",Strings.Join(sarr, " "));
			Assert.AreEqual("abc, def, ghi",Strings.Join(sarr, ", "));
			Assert.AreEqual("abc Ok def Ok ghi",Strings.Join(sarr, " Ok "));
		}

		[Test]
		public void Join_2()
		{
			object[] sarr = new object[] {"abc", "def", "ghi"};

			Assert.AreEqual("abc def ghi",Strings.Join(sarr, " "));
			Assert.AreEqual("abc, def, ghi",Strings.Join(sarr, ", "));
			Assert.AreEqual("abc Ok def Ok ghi",Strings.Join(sarr, " Ok "));
		}

		[Test]
		public void Join_3()
		{
			object[] sarr = new object[] {0x20a, 0x57a, 0x566a};

			Assert.AreEqual("522 1402 22122",Strings.Join(sarr, " "));
			Assert.AreEqual("522, 1402, 22122",Strings.Join(sarr, ", "));
			Assert.AreEqual("522 Ok 1402 Ok 22122",Strings.Join(sarr, " Ok "));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Join_4()
		{
			object[][] sarr = new object[][] {new object[] {"a"}, new object[] {"b"}};

			Strings.Join(sarr, " ");
		}

		[Test]
		public void Join_5()
		{
			object[] sarr = new object[] {"abc", "def", "ghi"};

			Assert.AreEqual(null,Strings.Join(null, " "));
			Assert.AreEqual("abcdefghi",Strings.Join(sarr, null));
		}

		#endregion

		#region LCase Tests

		[Test]
		public void LCase_1()
		{
			Assert.AreEqual("aaaaaaaa11111111bbbbb2222ccccc3333",Strings.LCase("AAAAAAAA11111111BBBBB2222CCCCC3333"));
			Assert.AreEqual("a",Strings.LCase("A"));
			Assert.AreEqual("z",Strings.LCase("Z"));
			Assert.AreEqual("@#$$@#",Strings.LCase("@#$$@#"));
			Assert.AreEqual("\u2345 \u5678",Strings.LCase("\u2345 \u5678"));
		}

		[Test]
		public void LCase_2()
		{			
			Assert.AreEqual('a',Strings.LCase('A'));
			Assert.AreEqual('z',Strings.LCase('Z'));
			Assert.AreEqual('@',Strings.LCase('@'));
			Assert.AreEqual('\u5678',Strings.LCase('\u5678'));
		}

		[Test]
		public void LCase_3()
		{
			Assert.AreEqual(null,Strings.LCase(null));
		}

		#endregion

		#region Left Tests

		[Test]
		public void Left_1()
		{
			Assert.AreEqual("Ab",Strings.Left("AbCdEf",2));
			Assert.AreEqual("AbCd",Strings.Left("AbCdEf",4));
			Assert.AreEqual(String.Empty,Strings.Left("AbCdEf",0));
			Assert.AreEqual("AbCdEf",Strings.Left("AbCdEf",15));
		}

		[Test]
		public void Left_2()
		{
			Assert.AreEqual(String.Empty,Strings.Left(null,2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Left_3()
		{
			Strings.Left("abc",-1);
		}

		#endregion

		#region Len Tests

		[Test]
		public void Len_1()
		{
			Assert.AreEqual(2,Strings.Len(new Boolean()),"Boolean");
			Assert.AreEqual(1,Strings.Len(new Byte()),"Byte");
			Assert.AreEqual(2,Strings.Len(new Char()),"Char");
			Assert.AreEqual(8,Strings.Len(new Double()),"Double");
			Assert.AreEqual(2,Strings.Len(new Int16()),"Int16");
			Assert.AreEqual(4,Strings.Len(new Int32()),"Int32");
			Assert.AreEqual(8,Strings.Len(new Int64()),"Int64");
			Assert.AreEqual(1,Strings.Len(new SByte()),"SByte");
			Assert.AreEqual(4,Strings.Len(new Single()),"Single");
			Assert.AreEqual(8,Strings.Len(new DateTime()),"DateTime");
			Assert.AreEqual(8,Strings.Len(new Decimal()),"Decimal");
			Assert.AreEqual(2,Strings.Len(new UInt16()),"UInt16");
			Assert.AreEqual(4,Strings.Len(new UInt32()),"UInt32");
			decimal d = new UInt64();
			Assert.AreEqual(8,Strings.Len(d));

			object o = new Int32();
			string s = String.Empty;
			Assert.AreEqual(4,Strings.Len(o));
			Assert.AreEqual(0,Strings.Len(s));
			Assert.AreEqual(9,Strings.Len("abcdefghi"));
			Assert.AreEqual(0,Strings.Len(null),"null");

		}

		class A 
		{
			int i;
			long j;
			double k;

			public A() 
			{
				i = 1;
				j = 2;
				k = 5.4;
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void Len_2()
		{
			object o = new A();
			Assert.AreEqual(4,Strings.Len(o));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void Len_3()
		{
			Strings.Len(new Object());
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void Len_4()
		{
			Strings.Len(DBNull.Value);
		}

		#endregion

		#region LSet Tests

		[Test]
		public void LSet_1()
		{
			Assert.AreEqual("abc       ",Strings.LSet("abc",10));
			Assert.AreEqual("abc       ",Strings.LSet("abc   ",10));
			Assert.AreEqual("ab",Strings.LSet("abc",2));
			Assert.AreEqual(String.Empty,Strings.LSet("abc",0));
			Assert.AreEqual("  ",Strings.LSet(null,2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void LSet_2()
		{
			Strings.LSet("abc",-1);
		}

		#endregion

		#region LTrim tests

		[Test]
		public void LTrim_1()
		{
			Assert.AreEqual("abc   ",Strings.LTrim("   abc   "));
			Assert.AreEqual("abc   ",Strings.LTrim("abc   "));
			Assert.AreEqual("\tabc   ",Strings.LTrim("\tabc   "));
			Assert.AreEqual("!!!!abc   ",Strings.LTrim("!!!!abc   "));
			Assert.AreEqual(String.Empty,Strings.LTrim(String.Empty));
			Assert.AreEqual(String.Empty,Strings.LTrim(null));
		}

		#endregion

		#region Mid Tests

		[Test]
		public void Mid_1()
		{
			Assert.AreEqual("bcdefg",Strings.Mid("abcdefg",2));
			Assert.AreEqual("bc",Strings.Mid("abcdefg",2,2));
			Assert.AreEqual("bcdefg",Strings.Mid("abcdefg",2,10));
			Assert.AreEqual("g",Strings.Mid("abcdefg",7,10));

			Assert.AreEqual(String.Empty,Strings.Mid("abcdefg",8,10));
			Assert.AreEqual(String.Empty,Strings.Mid("abcdefg",1,0));

			Assert.AreEqual(String.Empty,Strings.Mid(String.Empty,1,0));
			Assert.AreEqual(String.Empty,Strings.Mid(null,1,2));
			Assert.AreEqual(null,Strings.Mid(null,1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Mid_2()
		{
			Strings.Mid("abcdefg",0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Mid_3()
		{
			Strings.Mid("abcdefg",-1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Mid_4()
		{
			Strings.Mid("abcdefg",1,-1);
		}

		#endregion

		#region Replace Tests

		[Test]
		public void Replace_1()
		{
			Assert.AreEqual("aZZdZZlkjhZZkjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,-1,CompareMethod.Binary));
			Assert.AreEqual("cdZZlkjhZZkjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",3,-1,CompareMethod.Binary));
			Assert.AreEqual("kjhZZkjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",8,-1,CompareMethod.Binary));
			Assert.AreEqual("h",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",15,-1,CompareMethod.Binary));

			Assert.AreEqual("aZZdbclkjhbckjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,1,CompareMethod.Binary));
			Assert.AreEqual("aZZdZZlkjhbckjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,2,CompareMethod.Binary));		
		}

		[Test]
		public void Replace_2()
		{
			Assert.AreEqual("abcdbclkjhbckjh",Strings.Replace("abcdbclkjhbckjh","BC", "ZZ",1,-1,CompareMethod.Binary));
			Assert.AreEqual("aZZdZZlkjhZZkjh",Strings.Replace("abcdbclkjhbckjh","BC", "ZZ",1,-1,CompareMethod.Text));
			Assert.AreEqual("aZZdZZlkjhZZkjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,-1,CompareMethod.Text));
		}

		[Test]
		public void Replace_3()
		{
			Assert.AreEqual(null,Strings.Replace(String.Empty,"BC", "ZZ",1,-1,CompareMethod.Binary));
			Assert.AreEqual(null,Strings.Replace(null,"BC", "ZZ",1,-1,CompareMethod.Binary));

			Assert.AreEqual("abcdbclkjhbckjh",Strings.Replace("abcdbclkjhbckjh",String.Empty, "ZZ",1,-1,CompareMethod.Text));
			Assert.AreEqual("abcdbclkjhbckjh",Strings.Replace("abcdbclkjhbckjh",null, "ZZ",1,-1,CompareMethod.Text));

			Assert.AreEqual("adlkjhkjh",Strings.Replace("abcdbclkjhbckjh","bc", String.Empty,1,-1,CompareMethod.Text));
			Assert.AreEqual("adlkjhkjh",Strings.Replace("abcdbclkjhbckjh","bc", null,1,-1,CompareMethod.Text));			

			Assert.AreEqual(null,Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",16,-1,CompareMethod.Text));
			Assert.AreEqual("abcdbclkjhbckjh",Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,0,CompareMethod.Text));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Replace_5()
		{
			Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",0,1,CompareMethod.Text);		
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Replace_6()
		{
			Strings.Replace("abcdbclkjhbckjh","bc", "ZZ",1,-2,CompareMethod.Text);		
		}

		#endregion

		#region Right Tests

		[Test]
		public void Right_1()
		{
			Assert.AreEqual(String.Empty,Strings.Right("Abcd",0));
			Assert.AreEqual("d",Strings.Right("Abcd",1));
			Assert.AreEqual("cd",Strings.Right("Abcd",2));
			Assert.AreEqual("bcd",Strings.Right("Abcd",3));
			Assert.AreEqual("Abcd",Strings.Right("Abcd",4));
			Assert.AreEqual("Abcd",Strings.Right("Abcd",5));
		}

		[Test]
		public void Right_2()
		{
			Assert.AreEqual(String.Empty,Strings.Right(String.Empty,0));
			Assert.AreEqual(String.Empty,Strings.Right(String.Empty,1));

			Assert.AreEqual(String.Empty,Strings.Right(null,0));
			Assert.AreEqual(String.Empty,Strings.Right(null,1));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Right_3()
		{
			Strings.Right("Abcd",-1);
		}

		#endregion

		#region RSet Tests

		[Test]
		public void RSet_1()
		{
			Assert.AreEqual("       abc",Strings.RSet("abc",10));
			Assert.AreEqual("    abc   ",Strings.RSet("   abc   ",10));
			Assert.AreEqual("ab",Strings.RSet("abc",2));
			Assert.AreEqual(String.Empty,Strings.RSet("abc",0));
			Assert.AreEqual("  ",Strings.RSet(null,2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void RSet_2()
		{
			Strings.RSet("abc",-1);
		}

		#endregion

		#region RTrim tests

		[Test]
		public void RTrim_1()
		{
			Assert.AreEqual("   abc",Strings.RTrim("   abc   "));
			Assert.AreEqual("abc",Strings.RTrim("abc   "));
			Assert.AreEqual("   abc\t",Strings.RTrim("   abc\t"));
			Assert.AreEqual("   !!!!abc!!!!",Strings.RTrim("   !!!!abc!!!!"));
			Assert.AreEqual(String.Empty,Strings.RTrim(String.Empty));
			Assert.AreEqual(String.Empty,Strings.RTrim(null));
		}

		#endregion

		#region Space Tests

		[Test]
		public void Space_1()
		{
			Assert.AreEqual(String.Empty,Strings.Space(0));
			Assert.AreEqual(" ",Strings.Space(1));
			Assert.AreEqual("  ",Strings.Space(2));
			Assert.AreEqual("   ",Strings.Space(3));
			Assert.AreEqual("    ",Strings.Space(4));
			Assert.AreEqual("     ",Strings.Space(5));
			Assert.AreEqual("      ",Strings.Space(6));
			Assert.AreEqual("       ",Strings.Space(7));
			Assert.AreEqual("        ",Strings.Space(8));
			Assert.AreEqual("         ",Strings.Space(9));
			Assert.AreEqual("          ",Strings.Space(10));
			Assert.AreEqual("                         ",Strings.Space(25));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Space_2()
		{
			Strings.Space(-1);
		}

		#endregion

		#region Split Tests

		[Test]
		public void Split_1()
		{
			string[] res;
			
			res= Strings.Split("aa aa aa aa aa aa", " ",-1,CompareMethod.Binary);
			Assert.AreEqual(6,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aa",res[3]);
			Assert.AreEqual("aa",res[4]);
			Assert.AreEqual("aa",res[5]);

			res = Strings.Split("aa,aa,aa,aa,aa,aa", ",",-1,CompareMethod.Binary);
			Assert.AreEqual(6,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aa",res[3]);
			Assert.AreEqual("aa",res[4]);
			Assert.AreEqual("aa",res[5]);

			res = Strings.Split("aabaabaabaabaabaa", "b",-1,CompareMethod.Binary);
			Assert.AreEqual(6,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aa",res[3]);
			Assert.AreEqual("aa",res[4]);
			Assert.AreEqual("aa",res[5]);

			res = Strings.Split("aa, bb, cc,dd ,ee", ",",-1,CompareMethod.Binary);
			Assert.AreEqual(5,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual(" bb",res[1]);
			Assert.AreEqual(" cc",res[2]);
			Assert.AreEqual("dd ",res[3]);
			Assert.AreEqual("ee",res[4]);

			res = Strings.Split("aaebbecceddeee", "e",-1,CompareMethod.Binary);
			Assert.AreEqual(7,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("bb",res[1]);
			Assert.AreEqual("cc",res[2]);
			Assert.AreEqual("dd",res[3]);
			Assert.AreEqual(String.Empty,res[4]);
			Assert.AreEqual(String.Empty,res[5]);
			Assert.AreEqual(String.Empty,res[6]);
		}

		[Test]
		public void Split_2()
		{
			string[] res;

			res = Strings.Split("aaebbecceddeee", "E",-1,CompareMethod.Text);
			Assert.AreEqual(7,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("bb",res[1]);
			Assert.AreEqual("cc",res[2]);
			Assert.AreEqual("dd",res[3]);
			Assert.AreEqual(String.Empty,res[4]);
			Assert.AreEqual(String.Empty,res[5]);
			Assert.AreEqual(String.Empty,res[6]);

			res = Strings.Split("aaebbecceddeee", "E",-1,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual("aaebbecceddeee",res[0]);
		}

		[Test]
		public void Split_3()
		{
			string[] res;
			
			res= Strings.Split("aa aa aa aa aa aa", " ",20,CompareMethod.Binary);
			Assert.AreEqual(6,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aa",res[3]);
			Assert.AreEqual("aa",res[4]);
			Assert.AreEqual("aa",res[5]);

			res = Strings.Split("aa,aa,aa,aa,aa,aa", ",",5,CompareMethod.Binary);
			Assert.AreEqual(5,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aa",res[3]);
			Assert.AreEqual("aa,aa",res[4]);

			res = Strings.Split("aabaabaabaabaabaa", "b",4,CompareMethod.Binary);
			Assert.AreEqual(4,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("aa",res[1]);
			Assert.AreEqual("aa",res[2]);
			Assert.AreEqual("aabaabaa",res[3]);

			res = Strings.Split("aa, bb, cc,dd ,ee", ",",3,CompareMethod.Binary);
			Assert.AreEqual(3,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual(" bb",res[1]);
			Assert.AreEqual(" cc,dd ,ee",res[2]);

			res = Strings.Split("aaebbecceddeee", "e",1,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual("aaebbecceddeee",res[0]);
		}

		[Test]
		public void Split_4()
		{
			string[] res;
			
			res = Strings.Split(String.Empty, " ",5,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual(String.Empty,res[0]);

			res = Strings.Split(null, " ",5,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual(String.Empty,res[0]);

			res = Strings.Split("aa,aa,aa", String.Empty,5,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual("aa,aa,aa",res[0]);

			res = Strings.Split("aa,aa,aa", null,5,CompareMethod.Binary);
			Assert.AreEqual(1,res.Length);
			Assert.AreEqual("aa,aa,aa",res[0]);
		}

		[Test]
		public void Split_5()
		{
			string[] res;

			res = Strings.Split("aabaabaabaabaabaa", "ba",-1,CompareMethod.Binary);
			Assert.AreEqual(6,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("a",res[1]);
			Assert.AreEqual("a",res[2]);
			Assert.AreEqual("a",res[3]);			
			Assert.AreEqual("a",res[4]);			
			Assert.AreEqual("a",res[5]);			

			res = Strings.Split("aabaabaabaabaabaa", "ba",3,CompareMethod.Binary);
			Assert.AreEqual(3,res.Length);
			Assert.AreEqual("aa",res[0]);
			Assert.AreEqual("a",res[1]);
			Assert.AreEqual("abaabaabaa",res[2]);
		}

		[Test]
#if TARGET_JVM
		[ExpectedException(typeof(Exception))]
#else
		[ExpectedException(typeof(IndexOutOfRangeException))]
#endif
		public void Split_6()
		{
			Strings.Split("aa aa aa aa aa aa", " ",0,CompareMethod.Binary);
		}

		[Test]
		[Category("NotWorking")]
		[ExpectedException(typeof(OverflowException))]
		public void Split_7()
		{
			Strings.Split("aa aa aa aa aa aa", " ",-5,CompareMethod.Binary);
		}

		#endregion

		#region StrComp Tests

		[Test]
		public void StrComp_1()
		{
			Assert.AreEqual(0,Strings.StrComp("abcd","abcd",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd","ABCD",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd","abCD",CompareMethod.Binary));
			Assert.AreEqual(-1,Strings.StrComp("abCD","abcd",CompareMethod.Binary));

			Assert.AreEqual(0,Strings.StrComp("abcd","abcd",CompareMethod.Text));
			Assert.AreEqual(0,Strings.StrComp("abcd","ABCD",CompareMethod.Text));
			Assert.AreEqual(0,Strings.StrComp("abcd","abCD",CompareMethod.Text));

			Assert.AreEqual(-1,Strings.StrComp("abcd","zzzz",CompareMethod.Binary));
			Assert.AreEqual(-1,Strings.StrComp("abcd","zzzz",CompareMethod.Text));
		}

		[Test]
		public void StrComp_2()
		{
			Assert.AreEqual(-1,Strings.StrComp(String.Empty,"abcd",CompareMethod.Binary));
			Assert.AreEqual(-1,Strings.StrComp(null,"abcd",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd",String.Empty,CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd",null,CompareMethod.Binary));

			Assert.AreEqual(-1,Strings.StrComp(String.Empty,"abcd",CompareMethod.Text));
			Assert.AreEqual(-1,Strings.StrComp(null,"abcd",CompareMethod.Text));
			Assert.AreEqual(1,Strings.StrComp("abcd",String.Empty,CompareMethod.Text));
			Assert.AreEqual(1,Strings.StrComp("abcd",null,CompareMethod.Text));

			Assert.AreEqual(0,Strings.StrComp(null,null,CompareMethod.Binary));
			Assert.AreEqual(0,Strings.StrComp(String.Empty,String.Empty,CompareMethod.Binary));

			Assert.AreEqual(0,Strings.StrComp(String.Empty,null,CompareMethod.Binary));
			Assert.AreEqual(0,Strings.StrComp(null,String.Empty,CompareMethod.Binary));
		}

		[Test]
		public void StrComp_3()
		{
			Assert.AreEqual(1,Strings.StrComp("abcd","ab'cd",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd","ab'bd",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd","ab-cd",CompareMethod.Binary));
			Assert.AreEqual(1,Strings.StrComp("abcd","ab-bd",CompareMethod.Binary));

			Assert.AreEqual(-1,Strings.StrComp("abcd","ab'cd",CompareMethod.Text));
			Assert.AreEqual(1,Strings.StrComp("abcd","ab'bd",CompareMethod.Text));
			Assert.AreEqual(-1,Strings.StrComp("abcd","ab-cd",CompareMethod.Text));
			Assert.AreEqual(1,Strings.StrComp("abcd","ab-bd",CompareMethod.Text));
		}

		#endregion

		#region StrConv Tests

		[Test]
		public void StrConv_1()
		{
            Assert.AreEqual("abcd abcd",Strings.StrConv("ABCD abcd", VbStrConv.Lowercase,0));
            Assert.AreEqual("ABCD ABCD", Strings.StrConv("ABCD abcd", VbStrConv.Uppercase, 0));
            Assert.AreEqual("ASD1234 SDF234 DXC234", Strings.StrConv("asd1234 sdf234 dxc234", VbStrConv.Uppercase, 0));
            Assert.AreEqual("Abcd Abcd",Strings.StrConv("ABCD abcd", VbStrConv.ProperCase,0));
			Assert.AreEqual("Abcd Ascd Ffff",Strings.StrConv("abcd ascd ffff", VbStrConv.ProperCase,0));
			Assert.AreEqual("Asd1234 Sdf234 Dxc234",Strings.StrConv("asd1234 sdf234 dxc234", VbStrConv.ProperCase,0));
		}

		[Test]
		[Category("NotWorking")]
		public void StrConv_2()
		{
            Assert.AreEqual("abcd abcd", Strings.StrConv("ABCD abcd", VbStrConv.LinguisticCasing | VbStrConv.Lowercase, 0));
            Assert.AreEqual("abcd ascd ffff", Strings.StrConv("abcd ascd ffff", VbStrConv.LinguisticCasing | VbStrConv.Lowercase, 0));
            Assert.AreEqual("asd1234 sdf234 dxc234", Strings.StrConv("asd1234 sdf234 dxc234", VbStrConv.LinguisticCasing | VbStrConv.Lowercase, 0));

            Assert.AreEqual("ABCD ABCD", Strings.StrConv("ABCD abcd", VbStrConv.LinguisticCasing | VbStrConv.Uppercase, 0));
            Assert.AreEqual("ABCD ASCD FFFF", Strings.StrConv("abcd ascd ffff", VbStrConv.LinguisticCasing | VbStrConv.Uppercase, 0));
            Assert.AreEqual("ASD1234 SDF234 DXC234", Strings.StrConv("asd1234 sdf234 dxc234", VbStrConv.LinguisticCasing | VbStrConv.Uppercase, 0));
        }

		[Test]
		public void StrConv_ProperCase()
		{
			Assert.AreEqual ("Abcd8abcd", Strings.StrConv ("abcd8abcd", VbStrConv.ProperCase, 0));

			Assert.AreEqual("Abcd,Efgh;Ffff.Kkk!Qqqq@Eeee#Wwww$Llll",Strings.StrConv("abcd,efgh;ffff.kkk!qqqq@eeee#wwww$llll", VbStrConv.ProperCase,0));
			Assert.AreEqual("Aa%Bb^Cc&Dd*Ee(Ff)Gg-Hh_Ee",Strings.StrConv("aa%bb^cc&dd*ee(ff)gg-hh_ee", VbStrConv.ProperCase,0));
			Assert.AreEqual("Ee=Ff+Gg'ii\"Jj:Kk\\Ll|Mm<Nn",Strings.StrConv("ee=ff+gg'ii\"jj:kk\\ll|mm<nn", VbStrConv.ProperCase,0));
			Assert.AreEqual("Ee>Ff~Gg`Ii?Jj/Kkllmmnn",Strings.StrConv("ee>ff~gg`ii?jj/kkllmmnn", VbStrConv.ProperCase,0));
			
			Assert.AreEqual("Aa Bb\tCc\t\tDdd",Strings.StrConv("aa bb\tcc\t\tddd", VbStrConv.ProperCase,0));
			Assert.AreEqual("Mcloud",Strings.StrConv("mcloud", VbStrConv.ProperCase,0));
		}

		[Test]
		public void StrConv_None()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.None,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.None,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.None,0));
		}
        /*
		[Test]
		[Category("NotWorking")]
		public void StrConv_Wide()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.Wide,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.Wide,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.Wide,0));
		}

		[Test]
		[Category("NotWorking")]
		public void StrConv_Narrow()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.Narrow,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.Narrow,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.Narrow,0));
		}

		[Test]
		[Category("NotWorking")]
		public void StrConv_Katakana()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.Katakana,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.Katakana,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.Katakana,0));
		}
        
		[Test]
		[Category("NotWorking")]
		public void StrConv_Hiragana()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.Hiragana,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.Hiragana,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.Hiragana,0));
		}
        */
		[Test]
		[Category("NotWorking")]
		public void StrConv_SimplifiedChinese()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.SimplifiedChinese,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.SimplifiedChinese,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.SimplifiedChinese,0));
		}

		[Test]
		[Category("NotWorking")]
		public void StrConv_TraditionalChinese()
		{
			Assert.AreEqual("ABCD abcd",Strings.StrConv("ABCD abcd", VbStrConv.TraditionalChinese,0));
			Assert.AreEqual("abcd ABCD ffff",Strings.StrConv("abcd ABCD ffff", VbStrConv.TraditionalChinese,0));
			Assert.AreEqual("asd1234 ABCD dxc234",Strings.StrConv("asd1234 ABCD dxc234", VbStrConv.TraditionalChinese,0));
		}

		[Test]
		public void StrConv_3()
		{
			Assert.AreEqual(String.Empty,Strings.StrConv(String.Empty, VbStrConv.Uppercase,0));
        }

		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void StrConv_4()
		{
			Strings.StrConv("ABCD abcd", VbStrConv.LinguisticCasing,0);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void StrConv_5()
		{
			//  Value cannot be null.
			Strings.StrConv(null, VbStrConv.Uppercase,0);
        }


		#endregion

		#region StrDup Tests

		[Test]
		public void StrDup_1()
		{
			Assert.AreEqual("AAAAAAAAAAAA",Strings.StrDup(12,"A"));
			Assert.AreEqual("ccc",Strings.StrDup(3,'c'));
			Assert.AreEqual("aaaaa",Strings.StrDup(5,"abcd"));
		}

		[Test]
		public void StrDup_2()
		{
			object o = "dado";
			Assert.AreEqual("ddddd",Strings.StrDup(5,o));
		}

		[Test]
		public void StrDup_3()
		{
			Assert.AreEqual(String.Empty,Strings.StrDup(0,"abcd"));						
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StrDup_4()
		{
			object o = 5;
			Strings.StrDup(5,o);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StrDup_5()
		{ 
			Strings.StrDup(-1,"a");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StrDup_6()
		{
			Strings.StrDup(5,String.Empty);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void StrDup_7()
		{
			Strings.StrDup(5,null);
		}

		#endregion

		#region StrReverse Tests

		[Test]
		public void StrReverse_1()
		{
			Assert.AreEqual("dcba",Strings.StrReverse("abcd"));
			Assert.AreEqual("d:c,ba",Strings.StrReverse("ab,c:d"));

			Assert.AreEqual(String.Empty,Strings.StrReverse(String.Empty));
			Assert.AreEqual(String.Empty,Strings.StrReverse(null));
		}

		#endregion

		#region Trim Tests

		[Test]
		public void Trim_1()
		{
			Assert.AreEqual("abc",Strings.Trim("   abc   "));
			Assert.AreEqual("abc",Strings.Trim("abc   "));
			Assert.AreEqual("abc\t",Strings.Trim("   abc\t"));
			Assert.AreEqual("!!!!abc!!!!",Strings.Trim("   !!!!abc!!!!"));
			Assert.AreEqual(String.Empty,Strings.Trim(String.Empty));
			Assert.AreEqual(String.Empty,Strings.Trim(null));
		}

		#endregion

		#region UCase tests

		[Test]
		public void UCase_1()
		{
			Assert.AreEqual("AAAAAAAA11111111BBBBB2222CCCCC3333",Strings.UCase("aaaaaaaa11111111bbbbb2222ccccc3333"));
			Assert.AreEqual("A",Strings.UCase("a"));
			Assert.AreEqual("Z",Strings.UCase("z"));
			Assert.AreEqual("@#$$@#",Strings.UCase("@#$$@#"));
			Assert.AreEqual("\u2345 \u5678",Strings.UCase("\u2345 \u5678"));
		}

		[Test]
		public void UCase_2()
		{			
			Assert.AreEqual('A',Strings.UCase('a'));
			Assert.AreEqual('Z',Strings.UCase('z'));
			Assert.AreEqual('@',Strings.UCase('@'));
			Assert.AreEqual('\u5678',Strings.UCase('\u5678'));
		}

		[Test]
		public void UCase_3()
		{
			Assert.AreEqual(String.Empty,Strings.UCase(String.Empty));
			Assert.AreEqual(String.Empty,Strings.UCase(null));
		}

		#endregion


	}
}
