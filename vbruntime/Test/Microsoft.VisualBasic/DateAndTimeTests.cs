// DateAndTimeTest.cs - NUnit Test Cases for Microsoft.VisualBasic.DateAndTime
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

			Console.WriteLine("{0} {1}",date,time);
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			DateAndTime.DateString = date;
			DateAndTime.TimeString = time;
			Console.WriteLine("{0} {1}",date,time);
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
			DateAndTime.DateAdd("d", 0, "125/03");
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
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff("ww", DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.System,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Sunday,FirstWeekOfYear.System));
			Assert.AreEqual(104,DateAndTime.DateDiff(DateInterval.WeekOfYear, DateTime.Parse("12/5/03"),DateTime.Parse("12/1/05"),FirstDayOfWeek.Monday,FirstWeekOfYear.System));
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
			DateAndTime.DateDiff("d", DateTime.Parse("12/5/03"),"125/03", (FirstDayOfWeek)8 ,FirstWeekOfYear.System );
		}

		#endregion

		#region DatePart Tests

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
			DateAndTime.DatePart("d", "125/03", (FirstDayOfWeek)8 ,FirstWeekOfYear.System );
		}

		#endregion

		#region DateSerial Tests

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
		public void DateString_1()
		{
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
		public void DateValue_1()
		{
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

		[Test]		
		public void MonthName_5()
		{
			// LAMESPEC: MSDN states that in 12-month calendar the 
			// 13 month should return empty string, but exception is thrown instead
			Assert.AreEqual(String.Empty,DateAndTime.MonthName(13,false));
		}

		#endregion

		#region Now Tests

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
		public void Second()
		{
			Assert.AreEqual(23,DateAndTime.Second(DateTime.Parse("2/2/03 12:11:23 AM")));
			Assert.AreEqual(0,DateAndTime.Second(DateTime.Parse("2/2/03 03:11:00 PM")));
			Assert.AreEqual(59,DateAndTime.Second(DateTime.Parse("2/2/03 17:11:59")));
			Assert.AreEqual(6,DateAndTime.Second(DateTime.Parse("2/2/03 03:11:06 pm")));
		}

		#endregion

		#region TimeOfDay Tests

		[Test]
		public void TimeOfDay_1()
		{
			DateAndTime.TimeOfDay = DateTime.Parse("12/2/03 23:34:45");

			Assert.AreEqual(DateTime.Parse("1/1/0001 23:34:45"),DateAndTime.TimeOfDay);
		}

		#endregion

		#region TimeSerial Tests

		[Test]
		public void TimeSerial_1()
		{
			Assert.AreEqual(DateTime.Parse("1/1/0001 02:03:04 2:3:4"),DateAndTime.TimeSerial(2,3,4));
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
		public void TimeString_1()
		{
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
		public void Today_1()
		{
			Assert.AreEqual(new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0),DateAndTime.Today);

			DateAndTime.Today = DateTime.Parse("12/12/03 12:33:34");
			Assert.AreEqual(DateTime.Parse("12/12/03 00:00:00"),DateAndTime.Today);
		}

		#endregion

		#region Weekday Tests

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
		public void Year_1()
		{
			Assert.AreEqual(2003,DateAndTime.Year(DateTime.Parse("1/1/03 12:11:23 AM")));
			Assert.AreEqual(1003,DateAndTime.Year(DateTime.Parse("2/27/1003 03:00:23 PM")));
			Assert.AreEqual(9999,DateAndTime.Year(DateTime.Parse("12/12/9999 17:59:23")));
			Assert.AreEqual(1,DateAndTime.Year(DateTime.Parse("5/5/0001 03:16:23 pm")));
		}

		#endregion


	}
}
