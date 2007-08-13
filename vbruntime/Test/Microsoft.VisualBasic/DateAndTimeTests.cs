// DateAndTimeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.DateAndTime
//
// Boris Kirzner <borisk@mainsoft.com>
// Chris J. Breisch (cjbreisch@altavista.net)
// Martin Willemoes Hansen (mwh@sysrq.dk)
// Guy Cohen (guyc@mainsoft.com)

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
using System.Globalization;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class DateAndTimeTests
	{
		String date;
		String time;

		public DateAndTimeTests()
		{
		}

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			date = DateAndTime.DateString;
			time = DateAndTime.TimeString;
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			DateAndTime.DateString = date;
			DateAndTime.TimeString = time;
		}


		[SetUp]
		public void GetReady() 
		{			
		}

		[TearDown]
		public void Clean() 
		{			
		}	

		#region DateAdd Tests

		[Test]
		public void DateAdd() 
		{
		
			DateTime dtNow = DateTime.Now;

			Assert.AreEqual( dtNow.AddYears(1), DateAndTime.DateAdd(DateInterval.Year, 1, dtNow), "#DA01");
			Assert.AreEqual( dtNow.AddYears(-1), DateAndTime.DateAdd("yyyy", -1, dtNow),"#DA02");


			bool caughtException = false;
			
			try 
			{
				DateAndTime.DateAdd("foo", 1, dtNow);
			} 
			catch (Exception e) 
			{
				Assert.AreEqual(e.GetType(), typeof(ArgumentException),"#DA03");
				caughtException = true;
			}

			Assert.AreEqual(true, caughtException, "#DA04");

			Assert.AreEqual(dtNow.AddMonths(6), DateAndTime.DateAdd(DateInterval.Quarter, 2, dtNow),"#DA05");
			Assert.AreEqual( dtNow.AddMonths(-6), DateAndTime.DateAdd("q", -2, dtNow),"#DA06");

			Assert.AreEqual(dtNow.AddMonths(3), DateAndTime.DateAdd(DateInterval.Month, 3, dtNow),"#DA07");
			Assert.AreEqual(dtNow.AddMonths(-3), DateAndTime.DateAdd("m", -3, dtNow),"#DA08");

			Assert.AreEqual(dtNow.AddDays(28), DateAndTime.DateAdd(DateInterval.WeekOfYear, 4, dtNow),"#DA09");
			Assert.AreEqual(dtNow.AddDays(-28), DateAndTime.DateAdd("ww", -4, dtNow),"#DA10");

			Assert.AreEqual(dtNow.AddDays(5), DateAndTime.DateAdd(DateInterval.Weekday, 5, dtNow),"#DA11");
			Assert.AreEqual(dtNow.AddDays(-5), DateAndTime.DateAdd("w", -5, dtNow),"#DA12");

			Assert.AreEqual(dtNow.AddDays(6), DateAndTime.DateAdd(DateInterval.DayOfYear, 6, dtNow),"#DA13");
			Assert.AreEqual(dtNow.AddDays(-6), DateAndTime.DateAdd("y", -6, dtNow),"#DA14");

			Assert.AreEqual(dtNow.AddDays(7), DateAndTime.DateAdd(DateInterval.Day, 7, dtNow),"#DA15");
			Assert.AreEqual(dtNow.AddDays(-7), DateAndTime.DateAdd("d", -7, dtNow),"#DA16");

			Assert.AreEqual(dtNow.AddHours(8), DateAndTime.DateAdd(DateInterval.Hour, 8, dtNow),"#DA17");
			Assert.AreEqual(dtNow.AddHours(-8), DateAndTime.DateAdd(DateInterval.Hour, -8, dtNow),"#DA18");

			Assert.AreEqual(dtNow.AddMinutes(9), DateAndTime.DateAdd(DateInterval.Minute, 9, dtNow),"#DA19");
			Assert.AreEqual(dtNow.AddMinutes(-9), DateAndTime.DateAdd("n", -9, dtNow),"#DA20");

			Assert.AreEqual(dtNow.AddSeconds(10), DateAndTime.DateAdd(DateInterval.Second, 10, dtNow),"#DA21");
			Assert.AreEqual(dtNow.AddSeconds(-10), DateAndTime.DateAdd("s", -10, dtNow),"#DA22");

			try 
			{
				DateAndTime.DateAdd(DateInterval.Year, int.MinValue, dtNow);
			}
			catch (Exception e) 
			{
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), e.GetType(), "#DA23");
			}
		}

		
		[Test]
		public void DateAdd_DateInterval_1()
		{
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Day, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.DayOfYear, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 02:00:00"),DateAndTime.DateAdd(DateInterval.Hour, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:02:00"),DateAndTime.DateAdd(DateInterval.Minute, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("2/5/2004 00:00:00"),DateAndTime.DateAdd(DateInterval.Month, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("6/5/2004 00:00:00"),DateAndTime.DateAdd(DateInterval.Quarter, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:02"),DateAndTime.DateAdd(DateInterval.Second, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Weekday, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/19/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.WeekOfYear, 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2005 00:00:00"),DateAndTime.DateAdd(DateInterval.Year, 2, DateTime.Parse("12/5/03")));
		}

		[Test]
		public void DateAdd_String_1()
		{
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd("d", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd("y", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 02:00:00"),DateAndTime.DateAdd("h", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:02:00"),DateAndTime.DateAdd("n", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("2/5/2004 00:00:00"),DateAndTime.DateAdd("m", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("6/5/2004 00:00:00"),DateAndTime.DateAdd("q", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:02"),DateAndTime.DateAdd("s", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/7/2003 00:00:00"),DateAndTime.DateAdd("w", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/19/2003 00:00:00"),DateAndTime.DateAdd("ww", 2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2005 00:00:00"),DateAndTime.DateAdd("yyyy", 2, DateTime.Parse("12/5/03")));
		}

		[Test]
		public void DateAdd_DateInterval_2()
		{
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Day, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.DayOfYear, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 22:00:00"),DateAndTime.DateAdd(DateInterval.Hour, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 23:58:00"),DateAndTime.DateAdd(DateInterval.Minute, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("10/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Month, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("6/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Quarter, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 23:59:58"),DateAndTime.DateAdd(DateInterval.Second, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Weekday, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("11/21/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.WeekOfYear, -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2001 00:00:00"),DateAndTime.DateAdd(DateInterval.Year, -2, DateTime.Parse("12/5/03")));
		}

		[Test]
		public void DateAdd_String_2()
		{
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd("d", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd("y", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 22:00:00"),DateAndTime.DateAdd("h", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 23:58:00"),DateAndTime.DateAdd("n", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("10/5/2003 00:00:00"),DateAndTime.DateAdd("m", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("6/5/2003 00:00:00"),DateAndTime.DateAdd("q", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/4/2003 23:59:58"),DateAndTime.DateAdd("s", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/3/2003 00:00:00"),DateAndTime.DateAdd("w", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("11/21/2003 00:00:00"),DateAndTime.DateAdd("ww", -2, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2001 00:00:00"),DateAndTime.DateAdd("yyyy", -2, DateTime.Parse("12/5/03")));
		}
		
		[Test]
		public void DateAdd_DateInterval_3()
		{
			Assert.AreEqual(DateTime.Parse("12/5/03 00:00:00"),DateAndTime.DateAdd(DateInterval.Day, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.DayOfYear, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Hour, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Minute, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Month, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Quarter, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Second, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Weekday, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.WeekOfYear, 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd(DateInterval.Year, 0, DateTime.Parse("12/5/03")));
		}

		[Test]
		public void DateAdd_String_3()
		{
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("d", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("y", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("h", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("n", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("m", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("q", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("s", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("w", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("ww", 0, DateTime.Parse("12/5/03")));
			Assert.AreEqual(DateTime.Parse("12/5/2003 00:00:00"),DateAndTime.DateAdd("yyyy", 0, DateTime.Parse("12/5/03")));
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void DateAdd_1()
		{
			// Argument 'DateValue' cannot be converted to type 'Date'.
			DateAndTime.DateAdd("d", 0, "12test5/03");
		}

		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void DateAdd_2()
		{
			DateAndTime.DateAdd("yyyy", 9999999999999999999L, DateTime.Parse("12/5/03"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void DateAdd_3()
		{
			DateAndTime.DateAdd("yyyy", -5, new DateTime());
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DateAdd_5()
		{
			DateAndTime.DateAdd("yyy", 5, DateTime.Parse("12/5/03"));
		}

		#endregion

		#region DateDiff Tests

		[Test]
		public void DateDiff () 
		{
			DateTime dtNow = DateTime.Now;
			DateTime dtOld = dtNow.AddYears(-1);

			// TODO: Test this better
			long diff = DateAndTime.DateDiff(DateInterval.Year, dtOld, dtNow, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual(dtNow, dtOld.AddYears((int)diff),"#DD01");

			DateTime dtJan1 = new DateTime(2002, 1, 1);
			DateTime dtDec31 = new DateTime(2001, 12, 31);

			diff = DateAndTime.DateDiff(DateInterval.Year, dtDec31, dtJan1, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual(1L, diff,"#DD02");

			diff = DateAndTime.DateDiff(DateInterval.Quarter, dtDec31, dtJan1, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual(1L, diff,"#DD03");

			diff = DateAndTime.DateDiff(DateInterval.Month, dtDec31, dtJan1, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual(1L, diff,"#DD04");

			DateTime dtJan4 = new DateTime(2001, 1, 4);	// This is a Thursday
			DateTime dtJan9 = new DateTime(2001, 1, 9);	// This is the next Tuesday
			
			
			long WD = DateAndTime.DateDiff(DateInterval.Weekday, dtJan4, dtJan9, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual (0L, WD,"#DD05");

			long WY = DateAndTime.DateDiff(DateInterval.WeekOfYear, dtJan4, dtJan9, FirstDayOfWeek.System, FirstWeekOfYear.System);

			Assert.AreEqual (1L, WY, "#DD06");
		}

		[Test]
		public void DateDiff_DateInterval_1()
		{
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
		}

		[Test]
		public void DateDiff_String_1()
		{
			Assert.AreEqual(727,DateAndTime.DateDiff("d", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff("y", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff("h", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff("n", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff("m", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff("q", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff("s", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff("w", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff("yyyy", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
		}

		[Test]
		public void DateDiff_FirstDayOfWeek_1()
		{
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
		}
		
		[Test]
		public void DateDiff_FirstWeekOfYear_1()
		{
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays ));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));

			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.Day, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(727,DateAndTime.DateDiff(DateInterval.DayOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek  ));
			Assert.AreEqual(17448,DateAndTime.DateDiff(DateInterval.Hour, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(1046880,DateAndTime.DateDiff(DateInterval.Minute, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(24,DateAndTime.DateDiff(DateInterval.Month, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(8,DateAndTime.DateDiff(DateInterval.Quarter, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(62812800,DateAndTime.DateDiff(DateInterval.Second, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.Weekday, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));			
			Assert.AreEqual(2,DateAndTime.DateDiff(DateInterval.Year, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
		}

		[Test]
		public void DateDiff_DateInterval_WeekOfYear()
		{
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.System),"1");
			Assert.AreEqual(104,DateAndTime.DateDiff("ww", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.System),"2");
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday,FirstWeekOfYear.System),"3");
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday,FirstWeekOfYear.System),"4");
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Tuesday,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Wednesday,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Thursday,FirstWeekOfYear.System));
			Assert.AreEqual(103,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Friday,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Saturday,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.FirstFullWeek   ));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DateDiff_1()
		{
			// Argument 'Interval' is not a valid value.
			DateAndTime.DateDiff("k", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System ,FirstWeekOfYear.System );
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void DateDiff_3()
		{
			// Argument 'Date2' cannot be converted to type 'Date'.
			DateAndTime.DateDiff("d", DateTime.Parse("12/5/03"),"12test5/03", (FirstDayOfWeek)8 ,FirstWeekOfYear.System );
		}

		#endregion

		#region DatePart Tests


		[Test]
		public void DatePart () 
		{
			DateTime dtJan4 = new DateTime(2001, 1, 4);

			// TODO: Test this better

			Assert.AreEqual(2001, DateAndTime.DatePart(DateInterval.Year, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.System),"#DP01");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.Quarter, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.System),"#DP02");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.Month, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.System),"#DP03");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.WeekOfYear, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.FirstFourDays),"#DP04");
			Assert.AreEqual(53, DateAndTime.DatePart(DateInterval.WeekOfYear, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.FirstFullWeek),"#DP05");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.WeekOfYear, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.Jan1),"#DP06");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.WeekOfYear, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.System),"#DP07");
			Assert.AreEqual(7, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Friday, FirstWeekOfYear.FirstFourDays),"#DP08");
			Assert.AreEqual(6, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Saturday, FirstWeekOfYear.FirstFourDays),"#DP09");
			Assert.AreEqual(5, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Sunday, FirstWeekOfYear.FirstFourDays),"#DP10");
			Assert.AreEqual(4, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays),"#DP11");
			Assert.AreEqual(3, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Tuesday, FirstWeekOfYear.FirstFourDays),"#DP12");
			Assert.AreEqual(2, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Wednesday, FirstWeekOfYear.FirstFourDays),"#DP13");
			Assert.AreEqual(1, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.Thursday, FirstWeekOfYear.FirstFourDays),"#DP14");
			Assert.AreEqual(5, DateAndTime.DatePart(DateInterval.Weekday, dtJan4, FirstDayOfWeek.System, FirstWeekOfYear.FirstFourDays),"#DP15");


		}


		[Test]
		public void DatePart_DateInterval_1()
		{
			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(6,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(49,DateAndTime.DatePart(DateInterval.WeekOfYear, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
		}

		[Test]
		public void DatePart_String_1()
		{
			Assert.AreEqual(5,DateAndTime.DatePart("d", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart("y", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart("h", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart("n", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart("m", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart("q", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart("s", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(6,DateAndTime.DatePart("w", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(49,DateAndTime.DatePart("ww", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
			Assert.AreEqual(2003,DateAndTime.DatePart("yyyy", DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.System));
		}

		[Test]
		public void DatePart_FirstDayOfWeek_1()
		{
			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));
			Assert.AreEqual(6,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Sunday  ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));
			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Monday   ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Tuesday    ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));
			Assert.AreEqual(3,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Wednesday     ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));
			Assert.AreEqual(2,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Thursday   ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));
			Assert.AreEqual(1,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Friday    ,FirstWeekOfYear.System));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear , DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
			Assert.AreEqual(7,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.Saturday     ,FirstWeekOfYear.System));
		}

		[Test]
		public void DatePart_FirstWeekOfYear_1()
		{
			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));
			Assert.AreEqual(6,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.Jan1 ));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays ));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));
			Assert.AreEqual(6,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFourDays  ));

			Assert.AreEqual(5,DateAndTime.DatePart(DateInterval.Day, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(339,DateAndTime.DatePart(DateInterval.DayOfYear, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek  ));
			Assert.AreEqual(15,DateAndTime.DatePart(DateInterval.Hour, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(22,DateAndTime.DatePart(DateInterval.Minute, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(12,DateAndTime.DatePart(DateInterval.Month, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(4,DateAndTime.DatePart(DateInterval.Quarter, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(33,DateAndTime.DatePart(DateInterval.Second, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
			Assert.AreEqual(6,DateAndTime.DatePart(DateInterval.Weekday, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));			
			Assert.AreEqual(2003,DateAndTime.DatePart(DateInterval.Year, DateTime.Parse("12/5/03 15:22:33"),FirstDayOfWeek.System ,FirstWeekOfYear.FirstFullWeek   ));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DatePart_1()
		{
			// Argument 'Interval' is not a valid value.
			DateAndTime.DatePart("k", DateTime.Parse("12/5/03"),FirstDayOfWeek.System ,FirstWeekOfYear.System );
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void DatePart_3()
		{
			// Argument 'Date2' cannot be converted to type 'Date'.
			DateAndTime.DatePart("d", "12test5/03", (FirstDayOfWeek)8 ,FirstWeekOfYear.System );
		}

		#endregion

		#region DateSerial Tests

		
		[Test]
		public void DateSerial () 
		{
			DateTime dtJan4 = new DateTime(2001, 1, 4);
			DateTime dtSerial = DateAndTime.DateSerial(2001, 1, 4);

			Assert.AreEqual( dtJan4, dtSerial);
		}

		[Test]
		public void DateSerial_1()
		{
			Assert.AreEqual(DateTime.Parse("12/1/2003"),DateAndTime.DateSerial(2003,12,1));

			Assert.AreEqual(new DateTime(DateTime.Now.Year - 6,5,24),DateAndTime.DateSerial(-5,-6,-7));

			Assert.AreEqual(DateTime.Parse("12/1/9999"),DateAndTime.DateSerial(9999,12,1));
			Assert.AreEqual(DateTime.Parse("12/1/2003"),DateAndTime.DateSerial(03,12,1));
			Assert.AreEqual(DateTime.Parse("12/1/2025"),DateAndTime.DateSerial(25,12,1));

			Assert.AreEqual(DateTime.Parse("12/1/2000"),DateAndTime.DateSerial(0,12,1));
			Assert.AreEqual(new DateTime(DateTime.Now.Year - 1,12,1),DateAndTime.DateSerial(-1,12,1));
			Assert.AreEqual(new DateTime(DateTime.Now.Year - 99,12,1),DateAndTime.DateSerial(-99,12,1));
		}

		[Test]
		public void DateSerial_2()
		{
			Assert.AreEqual(DateTime.Parse("1/1/2003"),DateAndTime.DateSerial(2003,1,1));
			Assert.AreEqual(DateTime.Parse("12/1/2002"),DateAndTime.DateSerial(2003,0,1));
			Assert.AreEqual(DateTime.Parse("11/1/2002"),DateAndTime.DateSerial(2003,-1,1));
			Assert.AreEqual(DateTime.Parse("1/1/2004"),DateAndTime.DateSerial(2003,13,1));

			Assert.AreEqual(DateTime.Parse("2/1/2001"),DateAndTime.DateSerial(2003,-22,1));
		}

		[Test]
		public void DateSerial_3()
		{
			Assert.AreEqual(DateTime.Parse("5/1/2003"),DateAndTime.DateSerial(2003,5,1));
			Assert.AreEqual(DateTime.Parse("4/30/2003"),DateAndTime.DateSerial(2003,5,0));
			Assert.AreEqual(DateTime.Parse("4/29/2003"),DateAndTime.DateSerial(2003,5,-1));
			Assert.AreEqual(DateTime.Parse("6/24/2003"),DateAndTime.DateSerial(2003,5,55));
		}

		#endregion

		#region DateString Tests

		[Test]
		public void DateString() 
		{
			string s = DateAndTime.DateString;
			DateTime dtNow = DateTime.Today;
			Assert.AreEqual(dtNow.ToShortDateString(), DateTime.Parse(s).ToShortDateString());

			// TODO: Add a test for setting the date string too
		}

        //DateAndTime.DateString property is read-only under TARGET_JVM
        [Category("TargetJvmNotWorking")]
		[Test]
		public void DateString_1()
		{
			DateTime now = DateTime.Now;
			try {
				Assert.AreEqual(Strings.Format(DateTime.Now,"MM-dd-yyyy"),DateAndTime.DateString);

				DateAndTime.DateString = "9-5-2003";
				Assert.AreEqual("09-05-2003",DateAndTime.DateString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"MM-dd-yyyy"),DateAndTime.DateString);

				DateAndTime.DateString = "9-5-03";
				Assert.AreEqual("09-05-2003",DateAndTime.DateString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"MM-dd-yyyy"),DateAndTime.DateString);

				DateAndTime.DateString = "9/5/2003";
				Assert.AreEqual("09-05-2003",DateAndTime.DateString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"MM-dd-yyyy"),DateAndTime.DateString);

				DateAndTime.DateString = "9/5/03";
				Assert.AreEqual("09-05-2003",DateAndTime.DateString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"MM-dd-yyyy"),DateAndTime.DateString);
			} catch (System.UnauthorizedAccessException exception) {
				Assert.Ignore (exception.Message);			
			} finally {
				try {
					DateAndTime.Today = now;
					DateAndTime.TimeOfDay = now;
				} catch {
				}
			}			
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void DateString_2()
		{
			// Cast from string "9-5-003" to type 'Date' is not valid.
			DateAndTime.DateString = "9-5-003";
		}

		#endregion

		#region DateValue Tests

		[Test]
		public void DateValue () 
		{
			try 
			{
				DateAndTime.DateValue("This is not a date.");
			}
			catch (InvalidCastException) 
			{
				/* do nothing.  this is what we expect */
			}
			catch (Exception e) 
			{
				Assert.Fail ("Unexpected exception:" + e);
			}
			Assert.AreEqual(( new DateTime(1969, 2, 12)), DateAndTime.DateValue("02/12/1969"),"#DV03");
			Assert.AreEqual((new DateTime(1969, 2, 12)), DateAndTime.DateValue("February 12, 1969"),"#DV04");
		}

		[Test]
		public void DateValue_1()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81535");
				
			Assert.AreEqual(DateTime.Parse("12/30/1991"),DateAndTime.DateValue("12/30/1991"));
			Assert.AreEqual(DateTime.Parse("12/30/1991"),DateAndTime.DateValue("12/30/91"));
			Assert.AreEqual(DateTime.Parse("12/30/1991"),DateAndTime.DateValue("December 30, 1991"));
			Assert.AreEqual(DateTime.Parse("12/30/1991"),DateAndTime.DateValue("Dec 30, 1991"));
			Assert.AreEqual(DateTime.Parse("12/30/1991"),DateAndTime.DateValue("12/30/91 12:13:14"));
		}
		
		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void DateValue_2()
		{
			DateAndTime.DateValue("26:33");
		}
		
		[Test]
		public void DateValue_3()
		{
			 Assert.AreEqual(DateTime.Parse("1/1/0001 00:00:00"),DateAndTime.DateValue("22:33"));
		}

		#endregion

		#region Day Tests

		[Test]
		public void Day () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Day, DateAndTime.Day(jan1),"#D01");
		}

		[Test]
		public void Day_1()
		{
			Assert.AreEqual(3,DateAndTime.Day(DateTime.Parse("2/3/2003")));
			Assert.AreEqual(28,DateAndTime.Day(DateTime.Parse("2/28/2003")));

			Assert.AreEqual(1,DateAndTime.Day(DateTime.Parse("1/1/0001")));
			Assert.AreEqual(31,DateAndTime.Day(DateTime.Parse("12/31/9999")));
		}

		#endregion

		#region Hour Tests

		[Test]
		public void Hour () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Hour, DateAndTime.Hour(jan1),"#H01");
		}

		[Test]
		public void Hour_1()
		{
			Assert.AreEqual(0,DateAndTime.Hour(DateTime.Parse("2/2/03 12:11:23 AM")));
			Assert.AreEqual(15,DateAndTime.Hour(DateTime.Parse("2/2/03 03:11:23 PM")));
			Assert.AreEqual(17,DateAndTime.Hour(DateTime.Parse("2/2/03 17:11:23")));
			Assert.AreEqual(15,DateAndTime.Hour(DateTime.Parse("2/2/03 03:11:23 pm")));
		}

		#endregion

		#region Minute Tests

		[Test]
		public void Minute () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Minute, DateAndTime.Minute(jan1),"#MI01");
		}


		[Test]
		public void Minute_1()
		{
			Assert.AreEqual(11,DateAndTime.Minute(DateTime.Parse("2/2/03 12:11:23 AM")));
			Assert.AreEqual(0,DateAndTime.Minute(DateTime.Parse("2/2/03 03:00:23 PM")));
			Assert.AreEqual(59,DateAndTime.Minute(DateTime.Parse("2/2/03 17:59:23")));
			Assert.AreEqual(16,DateAndTime.Minute(DateTime.Parse("2/2/03 03:16:23 pm")));
		}

		#endregion

		#region Month Tests

		[Test]
		public void Month () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Month, DateAndTime.Month(jan1),"#MO01");
		}
		

		[Test]
		public void Month_1()
		{
			Assert.AreEqual(1,DateAndTime.Month(DateTime.Parse("1/1/03 12:11:23 AM")));
			Assert.AreEqual(2,DateAndTime.Month(DateTime.Parse("2/27/03 03:00:23 PM")));
			Assert.AreEqual(12,DateAndTime.Month(DateTime.Parse("12/12/03 17:59:23")));
			Assert.AreEqual(5,DateAndTime.Month(DateTime.Parse("5/5/03 03:16:23 pm")));
		}

		#endregion

		#region MonthName Tests

		[Test]
		public void MonthName () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(jan1.Month),
				DateAndTime.MonthName(jan1.Month, true),"#MN01");
			Assert.AreEqual(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(jan1.Month),
				DateAndTime.MonthName(jan1.Month, false),"#MN02");

			bool caughtException = false;

			try 
			{
				DateAndTime.MonthName(0, false);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#MN03");
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException,"#MN04");

			caughtException = false;
			
			try 
			{
				DateAndTime.MonthName(14, false);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#MN05");
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException,"#MN06");

			//Assert.AreEqual("#MN07", "", DateAndTime.MonthName(13, false));
		}

		[Test]
		public void MonthName_1()
		{
			Assert.AreEqual("January",DateAndTime.MonthName(1,false));
			Assert.AreEqual("February",DateAndTime.MonthName(2,false));
			Assert.AreEqual("March",DateAndTime.MonthName(3,false));
			Assert.AreEqual("April",DateAndTime.MonthName(4,false));
			Assert.AreEqual("May",DateAndTime.MonthName(5,false));
			Assert.AreEqual("June",DateAndTime.MonthName(6,false));
			Assert.AreEqual("July",DateAndTime.MonthName(7,false));
			Assert.AreEqual("August",DateAndTime.MonthName(8,false));
			Assert.AreEqual("September",DateAndTime.MonthName(9,false));
			Assert.AreEqual("October",DateAndTime.MonthName(10,false));
			Assert.AreEqual("November",DateAndTime.MonthName(11,false));
			Assert.AreEqual("December",DateAndTime.MonthName(12,false));
		}

		[Test]
		public void MonthName_2()
		{
			Assert.AreEqual("Jan",DateAndTime.MonthName(1,true));
			Assert.AreEqual("Feb",DateAndTime.MonthName(2,true));
			Assert.AreEqual("Mar",DateAndTime.MonthName(3,true));
			Assert.AreEqual("Apr",DateAndTime.MonthName(4,true));
			Assert.AreEqual("May",DateAndTime.MonthName(5,true));
			Assert.AreEqual("Jun",DateAndTime.MonthName(6,true));
			Assert.AreEqual("Jul",DateAndTime.MonthName(7,true));
			Assert.AreEqual("Aug",DateAndTime.MonthName(8,true));
			Assert.AreEqual("Sep",DateAndTime.MonthName(9,true));
			Assert.AreEqual("Oct",DateAndTime.MonthName(10,true));
			Assert.AreEqual("Nov",DateAndTime.MonthName(11,true));
			Assert.AreEqual("Dec",DateAndTime.MonthName(12,true));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MonthName_3()
		{
			DateAndTime.MonthName(0,false);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MonthName_4()
		{
			DateAndTime.MonthName(-1,false);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MonthName_6()
		{
			DateAndTime.MonthName(15,false);
		}

        //[Test]		
        //public void MonthName_5()
        //{
        //    // LAMESPEC: MSDN states that in 12-month calendar the 
        //    // 13 month should return empty string, but exception is thrown instead
        //    Assert.AreEqual(String.Empty,DateAndTime.MonthName(13,false));
        //}

		#endregion

		#region Now Tests

		
		[Test]
		public void Now() 
		{
			DateTime dtNow = DateTime.Now;
			DateTime dtTest = DateAndTime.Now;
			DateTime dtNow2 = DateTime.Now;

			Assert.AreEqual(true, dtTest >= dtNow, "#N01");
			Assert.AreEqual(true, dtNow2 >= dtTest,"#N02");
		}

		[Test]
		public void Now_1()
		{
			Assert.AreEqual(DateTime.Now.Year, DateAndTime.Now.Year);
			Assert.AreEqual(DateTime.Now.Month, DateAndTime.Now.Month);
			Assert.AreEqual(DateTime.Now.Day, DateAndTime.Now.Day);
			Assert.AreEqual(DateTime.Now.Hour, DateAndTime.Now.Hour);
			Assert.AreEqual(DateTime.Now.Minute, DateAndTime.Now.Minute);
		}

		#endregion

		#region Second Tests

		[Test]
		public void Second () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Second, DateAndTime.Second(jan1));
		}


		[Test]
		public void Second_1()
		{
			Assert.AreEqual(23,DateAndTime.Second(DateTime.Parse("2/2/03 12:11:23 AM")));
			Assert.AreEqual(0,DateAndTime.Second(DateTime.Parse("2/2/03 03:11:00 PM")));
			Assert.AreEqual(59,DateAndTime.Second(DateTime.Parse("2/2/03 17:11:59")));
			Assert.AreEqual(6,DateAndTime.Second(DateTime.Parse("2/2/03 03:11:06 pm")));
		}

		#endregion

		#region TimeOfDay Tests

		
		[Test]
		public void TimeOfDay() 
		{
			DateTime dtNow = DateTime.Now;
			TimeSpan tsNow = new TimeSpan(dtNow.Hour, dtNow.Minute, dtNow.Second);
			DateTime dtTest = DateAndTime.TimeOfDay;
			TimeSpan tsTest = new TimeSpan(dtTest.Hour, dtTest.Minute, dtTest.Second);
			DateTime dtNow2 = DateTime.Now;
			TimeSpan tsNow2 = new TimeSpan(dtNow2.Hour, dtNow2.Minute, dtNow2.Second);
			
			Assert.AreEqual(true, tsTest.Ticks >= tsNow.Ticks,"#TOD01");
			Assert.AreEqual(true, tsNow2.Ticks >= tsTest.Ticks,"#TOD02");

			// TODO: add a test case for setting time of day
		}
		
        //DateAndTime.TimeOfDay property is read-only under TARGET_JVM
        [Category("TargetJvmNotWorking")]
		[Test]
		public void TimeOfDay_1()
		{
			DateTime dt = DateAndTime.TimeOfDay;
			try {
				DateAndTime.TimeOfDay = DateTime.Parse("12/2/03 23:34:45");

				DateTime d1 = DateTime.Parse ("1/1/0001 23:34:45");
				DateTime d2 = DateAndTime.TimeOfDay;
				Assert.IsTrue(Math.Abs((d1 - d2).TotalMilliseconds) < 50);
			} catch (System.UnauthorizedAccessException exception) {
				Assert.Ignore (exception.Message);
			} finally {
				try {
					DateAndTime.TimeOfDay = dt;
				}
				catch {
				}
			}
		}

		#endregion

		#region TimeSerial Tests


		[Test]
		public void TimeSerial () 
		{
			bool caughtException = false;

			try 
			{
				DateAndTime.TimeSerial(0, -1440, -1);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentOutOfRangeException), e.GetType());
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException);

			Assert.AreEqual((new DateTime(1, 1, 1, 1, 1, 1)), DateAndTime.TimeSerial(1, 1, 1));
				
		}

		[Test]
		public void TimeSerial_1()
		{
			Assert.AreEqual(DateTime.Parse("1/1/0001 02:03:04"),DateAndTime.TimeSerial(2,3,4));
			Assert.AreEqual(DateTime.Parse("1/1/0001 21:56:56"),DateAndTime.TimeSerial(-2,-3,-4));
			Assert.AreEqual(DateTime.Parse("1/1/0001 22:33:34"),DateAndTime.TimeSerial(22,33,34));
			Assert.AreEqual(DateTime.Parse("1/1/0001 01:26:16"),DateAndTime.TimeSerial(-22,-33,-44));
			Assert.AreEqual(DateTime.Parse("1/2/0001 05:30:34"),DateAndTime.TimeSerial(28,89,94));
			Assert.AreEqual(DateTime.Parse("1/1/0001 00:00:00"),DateAndTime.TimeSerial(-24,0,0));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TimeSerial_2()
		{
			DateAndTime.TimeSerial(-24,0,-1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TimeSerial_3()
		{
			DateAndTime.TimeSerial(-28,-89,-94);
		}

		#endregion

		#region TimeString Tests

		[Test]
		public void TimeString() 
		{
			DateTime dtNow = DateTime.Now;
			TimeSpan tsNow = new TimeSpan(dtNow.Hour, dtNow.Minute, dtNow.Second);
			string s = DateAndTime.TimeString;
			DateTime dtTest = DateTime.Parse(s);
			TimeSpan tsTest = new TimeSpan(dtTest.Hour, dtTest.Minute, dtTest.Second);
			DateTime dtNow2 = DateTime.Now;
			TimeSpan tsNow2 = new TimeSpan(dtNow2.Hour, dtNow2.Minute, dtNow2.Second);
			
			Assert.AreEqual(true, tsTest.Ticks >= tsNow.Ticks, "#TS01");
			Assert.AreEqual(true, tsNow2.Ticks >= tsTest.Ticks, "#TS02");

			// TODO: add a test case for setting TimeString
		}

        //DateAndTime.TimeString property is read-only under TARGET_JVM
        [Category("TargetJvmNotWorking")]
		[Test]
		public void TimeString_1()
		{
			string dt = DateAndTime.TimeString;
			try {
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "11:23:44";
				Assert.AreEqual("11:23:44",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "15:23:44";
				Assert.AreEqual("15:23:44",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "1:23:44";
				Assert.AreEqual("01:23:44",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "11:23:44 am";
				Assert.AreEqual("11:23:44",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "11:23:44 pm";
				Assert.AreEqual("23:23:44",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "11:23";
				Assert.AreEqual("11:23:00",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);

				DateAndTime.TimeString = "1:3";
				Assert.AreEqual("01:03:00",DateAndTime.TimeString);
				Assert.AreEqual(Strings.Format(DateTime.Now,"HH:mm:ss"),DateAndTime.TimeString);
			} catch (UnauthorizedAccessException exception) {
				Assert.Ignore (exception.Message);
			} finally {
				try {
					DateAndTime.TimeString = dt;
				} catch {
				}
			}			
		}

		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TimeString_2()
		{
			DateAndTime.TimeString = "25:56";
		}

		#endregion

		#region TimeValue Tests


		[Test]
		public void TimeValue () 
		{
			try 
			{
				DateAndTime.TimeValue("This is not a time.");
			}
			catch (InvalidCastException) 
			{
				/* do nothing.  this is what we expect */
			}
			catch (Exception e) 
			{
				Assert.Fail ("Unexpected exception:" + e);
			}
			Assert.AreEqual((new DateTime(1, 1, 1, 16, 35, 17)), DateAndTime.TimeValue("16:35:17"),"#TV03");
			Assert.AreEqual((new DateTime(1, 1, 1, 16, 35, 17)), DateAndTime.TimeValue("4:35:17 PM"),"#TV04");
		//	Thread.CurrentThread.CurrentCulture = new CultureInfo ("en-US");
			Assert.AreEqual((new DateTime(1, 1, 1, 16, 35, 17)), DateAndTime.TimeValue("4:35:17 PM"),"#TV05");
		}


		[Test]
		public void TimeValue_1()
		{
			Assert.AreEqual(DateTime.Parse("1/1/0001 11:23:44"),DateAndTime.TimeValue("11:23:44"));
			Assert.AreEqual(DateTime.Parse("1/1/0001 23:23:44"),DateAndTime.TimeValue("11:23:44 PM"));
			Assert.AreEqual(DateTime.Parse("1/1/0001 17:02:34"),DateAndTime.TimeValue("5:2:34 pm"));
			Assert.AreEqual(DateTime.Parse("1/1/0001 10:11:00"),DateAndTime.TimeValue("10:11 am"));

			Assert.AreEqual(DateTime.Parse("1/1/0001 10:11:00"),DateAndTime.TimeValue("1/2/03 10:11 am"));
		}
		
		[Test]
		[ExpectedException(typeof(InvalidCastException))]
		public void TimeValue_2()
		{
			DateAndTime.TimeValue("26:33");
		}

		#endregion

		#region Today Tests

		[Test]
		public void Today() 
		{
			Assert.AreEqual(DateTime.Today, DateAndTime.Today);

			// TODO: Add a test for setting Today
		}

		[Test]
		[Category("NotWorking")]
		public void Today_1()
		{
			Assert.AreEqual(new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0),DateAndTime.Today);

			DateAndTime.Today = DateTime.Parse("12/12/03 12:33:34");
			Assert.AreEqual(DateTime.Parse("12/12/03 00:00:00"),DateAndTime.Today);
		}

		#endregion

		#region Weekday Tests

		[Test]
		public void Weekday () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual((int)jan1.DayOfWeek + 1, DateAndTime.Weekday(jan1, FirstDayOfWeek.System));
		}

		[Test]
		public void Weekday_1()
		{
			Assert.AreEqual(6,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.System));
			Assert.AreEqual(6,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Sunday));
			Assert.AreEqual(5,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Monday));
			Assert.AreEqual(4,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Tuesday));
			Assert.AreEqual(3,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Wednesday));
			Assert.AreEqual(2,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Thursday));
			Assert.AreEqual(1,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Friday));
			Assert.AreEqual(7,DateAndTime.Weekday(DateTime.Parse("12/12/03"),FirstDayOfWeek.Saturday));
		}

		#endregion

		#region WeekdayName Tests

		[Test]
		public void WeekdayName () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual("Tue",
				DateAndTime.WeekdayName((int)jan1.DayOfWeek + 1, true, FirstDayOfWeek.Monday));
			Assert.AreEqual("Tuesday",
				DateAndTime.WeekdayName((int)jan1.DayOfWeek + 1, false, FirstDayOfWeek.Monday));

			bool caughtException = false;

			try 
			{
				DateAndTime.WeekdayName(0, false, FirstDayOfWeek.Monday);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#WN03");
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException,"#WN04");

			caughtException = false;
			
			try 
			{
				DateAndTime.WeekdayName(8, false, FirstDayOfWeek.Monday);
			}
			catch (Exception e) 
			{
				Assert.AreEqual(typeof(ArgumentException), e.GetType(),"#WN05");
				caughtException = true;
			}
			Assert.AreEqual(true, caughtException,"#WN06");

			Assert.AreEqual("Tuesday", DateAndTime.WeekdayName((int)jan1.DayOfWeek + 1, false, FirstDayOfWeek.Monday),"#WN07");
		}
		[Test]
		public void WeekdayName_1()
		{
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.System));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.System));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.System));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.System));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.System));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.System));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.System));

			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Sunday));

			Assert.AreEqual("Monday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Monday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Monday));

			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Tuesday));

			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Wednesday));

			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Thursday));

			Assert.AreEqual("Friday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Friday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Friday));

			Assert.AreEqual("Saturday",DateAndTime.WeekdayName(1,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Sunday",DateAndTime.WeekdayName(2,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Monday",DateAndTime.WeekdayName(3,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Tuesday",DateAndTime.WeekdayName(4,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Wednesday",DateAndTime.WeekdayName(5,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Thursday",DateAndTime.WeekdayName(6,false,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Friday",DateAndTime.WeekdayName(7,false,FirstDayOfWeek.Saturday));
		}

		[Test]
		public void WeekdayName_2()
		{
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.System));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.System));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.System));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.System));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.System));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.System));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.System));

			Assert.AreEqual("Sun",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Sunday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Sunday));

			Assert.AreEqual("Mon",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Monday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Monday));

			Assert.AreEqual("Tue",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Tuesday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Tuesday));

			Assert.AreEqual("Wed",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Wednesday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Wednesday));

			Assert.AreEqual("Thu",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Thursday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Thursday));

			Assert.AreEqual("Fri",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Sat",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Friday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Friday));

			Assert.AreEqual("Sat",DateAndTime.WeekdayName(1,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Sun",DateAndTime.WeekdayName(2,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Mon",DateAndTime.WeekdayName(3,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Tue",DateAndTime.WeekdayName(4,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Wed",DateAndTime.WeekdayName(5,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Thu",DateAndTime.WeekdayName(6,true,FirstDayOfWeek.Saturday));
			Assert.AreEqual("Fri",DateAndTime.WeekdayName(7,true,FirstDayOfWeek.Saturday));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void WeekdayName_3()
		{
			DateAndTime.WeekdayName(0,true,FirstDayOfWeek.System);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void WeekdayName_4()
		{
			DateAndTime.WeekdayName(-1,true,FirstDayOfWeek.System);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void WeekdayName_5()
		{
			DateAndTime.WeekdayName(8,true,FirstDayOfWeek.System);
		}

		#endregion

		#region Year Tests

		[Test]
		public void Year () 
		{
			DateTime jan1 = new DateTime(2001, 1, 1, 1, 1, 1);
			Assert.AreEqual(jan1.Year, DateAndTime.Year(jan1),"#Y01");
		}

		[Test]
		public void Year_1()
		{
			Assert.AreEqual(2003,DateAndTime.Year(DateTime.Parse("1/1/03 12:11:23 AM")));
			Assert.AreEqual(1003,DateAndTime.Year(DateTime.Parse("2/27/1003 03:00:23 PM")));
			Assert.AreEqual(9999,DateAndTime.Year(DateTime.Parse("12/12/9999 17:59:23")));
			Assert.AreEqual(1,DateAndTime.Year(DateTime.Parse("5/5/0001 03:16:23 pm")));
		}

		#endregion

		#region Timer Tests

		[Test]
		[Category ("Slow")]
		public void Timer() 
		{
			double secTimer = DateAndTime.Timer;
			DateTime dtNow = DateTime.Now;
			double secNow = dtNow.Hour * 3600 + dtNow.Minute * 60 + dtNow.Second + (dtNow.Millisecond + 1) / 1000D;
			double secTimer2 = DateAndTime.Timer + .002D; // before was .001; but we need to allow for rounding differences
			
			// waste a little time
			for (int i = 0; i < int.MaxValue; i++);

			// get another timer
			double secTimer3 = DateAndTime.Timer;
			
			// should be same time within a reasonable tolerance
			Assert.AreEqual(true, secNow >= secTimer,"#TI01");
			Assert.AreEqual(true, secTimer2 >= secNow,"#TI02: slacked SecTimer2=" + secTimer2 + " secNow=" + secNow);

			// third timer should be greater than the first
			Assert.AreEqual(true, secTimer3 > secTimer,"#TI03");
		}

		#endregion



	}
}
