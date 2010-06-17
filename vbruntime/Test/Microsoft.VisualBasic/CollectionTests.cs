// CollectionTests.cs - NUnit Test Cases for Microsoft.VisualBasic.Collection 
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
using System.Collections;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class CollectionTests
	{
		public CollectionTests()
		{
		}

		[SetUp]
		public void GetReady() 
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
		}

		[TearDown]
		public void Clean() 
		{
		}
	

		// Test Constructor
		[Test]
		public void New ()
		{
			Collection c;

			c = new Collection ();

			Assert.IsNotNull (c, "#N01");
			Assert.AreEqual (0, c.Count, "#N02");
		}

		#region AddTests

		[Test]
		public void Add_1()
		{
			Collection col = new Collection();

			string s = "abc";
			col.Add(s,null,null,null);
			col.Add(s,null,null,null);
			col.Add(s,null,null,null);

			Assert.AreEqual(3,col.Count);
			Assert.AreEqual("abc",col[1]);
			Assert.AreEqual("abc",col[2]);
			Assert.AreEqual("abc",col[3]);
		}

		[Test]
		public void Add_2()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;
			Single o5 = 1.1F;
			Double o6 = 2.2;
			Decimal o7 = 1000;
			String o8 = "abc";
			Object o9 = null;
			bool o10 = true;
			Char o11 = 'c';
			DateTime o12 = DateTime.Parse("5/31/1993");

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);
			col.Add(o4,null,null,null);
			col.Add(o5,null,null,null);
			col.Add(o6,null,null,null);
			col.Add(o7,null,null,null);
			col.Add(o8,null,null,null);
			col.Add(o9,null,null,null);
			col.Add(o10,null,null,null);
			col.Add(o11,null,null,null);
			col.Add(o12,null,null,null);

			Assert.AreEqual(12,col.Count);
			Assert.AreEqual(o1,col[1]);
			Assert.AreEqual(o2,col[2]);
			Assert.AreEqual(o3,col[3]);
			Assert.AreEqual(o4,col[4]);
			Assert.AreEqual(o5,col[5]);
			Assert.AreEqual(o6,col[6]);
			Assert.AreEqual(o7,col[7]);
			Assert.AreEqual(o8,col[8]);
			Assert.AreEqual(o9,col[9]);
			Assert.AreEqual(o10,col[10]);
			Assert.AreEqual(o11,col[11]);
			Assert.AreEqual(o12,col[12]);
		}

		[Test]
		public void Add_Key_1()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"key2",null,null);
			col.Add(s1,"key3",null,null);			
			col.Add(s3,"key1",null,null);

			Assert.AreEqual(3,col.Count);
			Assert.AreEqual(s2,col[1]);
			Assert.AreEqual(s1,col[2]);
			Assert.AreEqual(s3,col[3]);

			Assert.AreEqual(s1,col["key3"]);
			Assert.AreEqual(s2,col["key2"]);
			Assert.AreEqual(s3,col["key1"]);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Add_Key_2()
		{
			// Add failed. Duplicate key value supplied.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s2,"key",null,null);
			col.Add(s1,"key",null,null);
		}

		[Test]
		public void Add_Before_1()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,null,null,null);
			col.Add(s2,null,1,null);
			col.Add(s3,null,2,null);
			col.Add(s4,null,2,null);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s2,col[1]);
			Assert.AreEqual(s4,col[2]);
			Assert.AreEqual(s3,col[3]);
			Assert.AreEqual(s1,col[4]);

		}

		[Test]
		public void Add_Before_2()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,"key4",null,null);
			col.Add(s2,"key3","key4",null);
			col.Add(s4,null,"key3",null);
			col.Add(s3,null,"key4",null);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s4,col[1]);
			Assert.AreEqual(s2,col[2]);
			Assert.AreEqual(s3,col[3]);
			Assert.AreEqual(s1,col[4]);
		}

		[Test]
		public void Add_Before_3()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,"key4",null,null);
			col.Add(s2,"key3",1,null);
			col.Add(s4,null,"key3",null);
			col.Add(s3,null,1,null);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s3,col[1]);
			Assert.AreEqual(s4,col[2]);
			Assert.AreEqual(s2,col[3]);
			Assert.AreEqual(s1,col[4]);
		}

		[Test]
		public void Add_After_1()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,null,null,null);
			col.Add(s2,null,null,1);
			col.Add(s3,null,null,2);
			col.Add(s4,null,null,1);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s4,col[2]);
			Assert.AreEqual(s2,col[3]);
			Assert.AreEqual(s3,col[4]);

		}

		[Test]
		public void Add_After_2()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,"key4",null,null);
			col.Add(s2,"key3",null,"key4");
			col.Add(s4,null,null,"key4");
			col.Add(s3,null,null,"key3");

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s4,col[2]);
			Assert.AreEqual(s2,col[3]);
			Assert.AreEqual(s3,col[4]);
		}

		[Test]
		public void Add_After_3()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,"key4",null,null);
			col.Add(s2,"key3",null,1);
			col.Add(s4,null,null,1);
			col.Add(s3,null,null,2);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s4,col[2]);
			Assert.AreEqual(s3,col[3]);
			Assert.AreEqual(s2,col[4]);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Add_3()
		{
			// 'Before' and 'After' arguments cannot be combined.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";
			string s4 = "d";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);
			col.Add(s3,"key3",null,null);
			col.Add(s4,"key4",3,2);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Add_4()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",3,null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Add_5()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",0,null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Add_6()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,3);
		}

		[Test]
		public void Add_After_4()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,0);

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual(s2,col[1]);
			Assert.AreEqual(s1,col[2]);
		}

		[Test]
		public void Add_Before_4()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",2,null);

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s2,col[2]);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Add_7()
		{
			// Specified argument was out of the range of valid values.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,-1);
		}

		[Test]
		public void Add_8()
		{
			Collection col = new Collection();

			Object o1 = null;
			Object o2 = null;
			Object o3 = null;
			Object o4 = null;


			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);
			col.Add(o4,null,null,null);
			
			Assert.AreEqual(4,col.Count);
			Assert.AreEqual(o1,col[1]);
			Assert.AreEqual(o2,col[2]);
			Assert.AreEqual(o3,col[3]);
			Assert.AreEqual(o4,col[4]);			
		}

		// Test Add method with Key == null
		[Test]
		public void AddNoKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add (typeof (int), null, null, null);
			c.Add (typeof (double), null, null, null);
			c.Add (typeof (string), null, null, null);

			Assert.AreEqual (3, c.Count, "#ANK01");

			// Collection class is 1-based
			Assert.AreEqual (typeof (string), c[3], "#ANK02");
		}

		// Test Add method with Key specified
		[Test]
		public void AddKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Football", "Foot", null, null);
			c.Add ("Basketball", "Basket", null, null);
			c.Add ("Volleyball", "Volley", null, null);

			Assert.AreEqual (4, c.Count, "#AK01");

			// Collection class is 1-based
			Assert.AreEqual ("Baseball", c[1], "#AK02");
			Assert.AreEqual ("Volleyball", c["Volley"], "#AK03");
		}

		// Test Add method with Before specified and Key == null
		[Test]
		public void AddBeforeNoKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add (typeof (int), null, null, null);
			c.Add (typeof (double), null, 1, null);
			c.Add (typeof (string), null, 2, null);
			c.Add (typeof (object), null, 2, null);

			Assert.AreEqual (4, c.Count, "#ABNK01");

			// Collection class is 1-based
			Assert.AreEqual (typeof (int), c[4], "#ABNK02");
			Assert.AreEqual (typeof (double), c[1], "#ABNK03");
			Assert.AreEqual (typeof (object), c[2], "#ABNK04");
		}

		// Test Add method with Before and Key
		[Test]
		public void AddBeforeKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Football", "Foot", 1, null);
			c.Add ("Basketball", "Basket", 1, null);
			c.Add ("Volleyball", "Volley", 3, null);

			Assert.AreEqual (4, c.Count, "#ABK01");
			Assert.AreEqual ("Basketball", c[1], "#ABK02");
			Assert.AreEqual ("Baseball", c[4], "#ABK03");
			Assert.AreEqual ("Volleyball", c["Volley"], "#ABK04");
			Assert.AreEqual ("Football", c["Foot"], "#ABK05");
		}

		// Test Add method with After specified and Key == null
		[Test]
		public void AddAfterNoKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add (typeof (int), null, null, 0);
			c.Add (typeof (double), null, null, 1);
			c.Add (typeof (string), null, null, 1);
			c.Add (typeof (object), null, null, 3);

			Assert.AreEqual (4, c.Count, "#AANK01");
			Assert.AreEqual (typeof (object), c[4], "#AANK02");
			Assert.AreEqual (typeof (int), c[1], "#AANK03");
			Assert.AreEqual (typeof (string), c[2], "#AANK04");
		}

		// Test Add method with After and Key
		[Test]
		public void AddAfterKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add ("Baseball", "Base", null, 0);
			c.Add ("Football", "Foot", null, 1);
			c.Add ("Basketball", "Basket", null, 1);
			c.Add ("Volleyball", "Volley", null, 2);

			Assert.AreEqual (4, c.Count, "#AAK01");
			Assert.AreEqual ("Baseball", c[1], "#AAK02");
			Assert.AreEqual ("Football", c[4], "#AAK03");
			Assert.AreEqual ("Basketball", c["Basket"], "#AAK04");
			Assert.AreEqual ("Volleyball", c["Volley"], "#AAK05");
		}

		#endregion

		#region Item Tests

		[Test]
		public void Index_1()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s2,col[2]);

			Assert.AreEqual(s1,col["key1"]);
			Assert.AreEqual(s2,col["key2"]);
		}

		[Test]
		public void Index_2()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			object key1 = "key1";
			object key2 = "key2";

			col.Add(s1,(string)key1,null,null);
			col.Add(s2,(string)key2,null,null);

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s2,col[2]);

			Assert.AreEqual(s1,col[key1]);
			Assert.AreEqual(s2,col[key2]);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Index_3()
		{
			// Argument 'Index' is not a valid value.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			object o = col["key3"];
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Index_4()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			object o = col[0];
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Index_5()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			object o = col[-1];
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Index_6()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			object o = col[3];
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Index_7()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			object o = col[null];
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Index_8()
		{
			Collection col = new Collection();

			((System.Collections.IList)col)[1] = "a";
		}

		[Test]
		public void Index_9()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";

			col.Add(s1,"key1",null,null);
			col.Add(s2,"key2",null,null);

			System.Collections.IList il = (System.Collections.IList)col;

			il[0] = "q";
			il[1] = "r";

			object o = il[1];
		}

		#endregion

		#region Remove Tests

		[Test]
		public void Remove_1()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,null,null,null);
			col.Add(s1,null,null,null);			
			col.Add(s3,null,null,null);
			col.Add(s2 + s1,null,null,null);
			col.Add(s1 + s1,null,null,null);			
			col.Add(s3 + s1,null,null,null);

			col.Remove(1);

			Assert.AreEqual(5,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("ba",col[3]);
			Assert.AreEqual("aa",col[4]);
			Assert.AreEqual("ca",col[5]);

			col.Remove(3);

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("aa",col[3]);
			Assert.AreEqual("ca",col[4]);

			col.Remove(4);

			Assert.AreEqual(3,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("aa",col[3]);

			col.Remove(2);

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("aa",col[2]);

			col.Remove(1);

			Assert.AreEqual(1,col.Count);
			Assert.AreEqual("aa",col[1]);

			col.Remove(1);

			Assert.AreEqual(0,col.Count);
		}

		[Test]
		public void Remove_2()
		{
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);
			col.Add(s2 + s1,"keyd",null,null);
			col.Add(s1 + s1,"keye",null,null);			
			col.Add(s3 + s1,"keyf",null,null);

			col.Remove("keya");

			Assert.AreEqual(5,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("ba",col[3]);
			Assert.AreEqual("aa",col[4]);
			Assert.AreEqual("ca",col[5]);

			col.Remove("keyd");

			Assert.AreEqual(4,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("aa",col[3]);
			Assert.AreEqual("ca",col[4]);

			col.Remove("keyf");

			Assert.AreEqual(3,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("c",col[2]);
			Assert.AreEqual("aa",col[3]);

			col.Remove("keyc");

			Assert.AreEqual(2,col.Count);
			Assert.AreEqual("a",col[1]);
			Assert.AreEqual("aa",col[2]);

			col.Remove("keyb");

			Assert.AreEqual(1,col.Count);
			Assert.AreEqual("aa",col[1]);

			col.Remove("keye");

			Assert.AreEqual(0,col.Count);
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Remove_3()
		{
			// Collection index must be in the range 1 to the size of the collection.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);

			col.Remove(-1);
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Remove_4()
		{
			// Collection index must be in the range 1 to the size of the collection.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);

			col.Remove(0);
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void Remove_5()
		{
			// Collection index must be in the range 1 to the size of the collection.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);

			col.Remove(4);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Remove_6()
		{
			// Argument 'Key' is not a valid value.
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);

			col.Remove("keyd");
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Remove_7()
		{
			// Key cannot be null
			Collection col = new Collection();

			string s1 = "a";
			string s2 = "b";
			string s3 = "c";

			col.Add(s2,"keya",null,null);
			col.Add(s1,"keyb",null,null);			
			col.Add(s3,"keyc",null,null);

			col.Remove(null);
		}
		// Test Remove method with Index
		[Test]
		public void RemoveNoKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add (typeof (int), null, null, null);
			c.Add (typeof (double), null, null, null);
			c.Add (typeof (string), null, null, null);
			c.Add (typeof (object), null, null, null);

			Assert.AreEqual (4, c.Count, "#RNK01");

			c.Remove (3);

			Assert.AreEqual (3, c.Count, "#RNK02");

			// Collection class is 1-based
			Assert.AreEqual (typeof (object), c[3], "#RNK03");

			c.Remove (1);

			Assert.AreEqual (2, c.Count, "#RNK04");
			Assert.AreEqual (typeof (double), c[1], "#RNK05");
			Assert.AreEqual (typeof (object), c[2], "#RNK06");

			c.Remove (2);

			Assert.AreEqual (1, c.Count, "#RNK07");
			Assert.AreEqual (typeof (double), c[1], "#RNK08");

			c.Remove (1);

			Assert.AreEqual (0, c.Count, "#RNK09");
		}

		// Test Remove method with Key
		[Test]
		public void RemoveKey ()
		{
			Collection c;

			c = new Collection ();

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Football", "Foot", null, null);
			c.Add ("Basketball", "Basket", null, null);
			c.Add ("Volleyball", "Volley", null, null);

			Assert.AreEqual (4, c.Count, "#RK01");

			c.Remove ("Foot");

			Assert.AreEqual (3, c.Count, "#RK02");
			Assert.AreEqual ("Basketball", c["Basket"], "#RK03");

			// Collection class is 1-based
			Assert.AreEqual ("Volleyball", c[3], "#RK04");

			c.Remove ("Base");

			Assert.AreEqual (2, c.Count, "#RK05");
			Assert.AreEqual ("Basketball", c[1], "#RK06");
			Assert.AreEqual ("Volleyball", c["Volley"], "#RK07");

			c.Remove (2);

			Assert.AreEqual (1, c.Count, "#RK08");
			Assert.AreEqual ("Basketball", c[1], "#RK09");
			Assert.AreEqual ("Basketball", c["Basket"], "#RK10");

			c.Remove (1);

			Assert.AreEqual (0, c.Count, "#RK11");
		}

		#endregion

		#region GetEnumerator Tests

		[Test]
		public void GetEnumerator_1()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;
			Single o5 = 1.1F;
			Double o6 = 2.2;
			Decimal o7 = 1000;
			String o8 = "abc";
			Object o9 = null;
			bool o10 = true;
			Char o11 = 'c';
			DateTime o12 = DateTime.Parse("5/31/1993");

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);
			col.Add(o4,null,null,null);
			col.Add(o5,null,null,null);
			col.Add(o6,null,null,null);
			col.Add(o7,null,null,null);
			col.Add(o8,null,null,null);
			col.Add(o9,null,null,null);
			col.Add(o10,null,null,null);
			col.Add(o11,null,null,null);
			col.Add(o12,null,null,null);

			IEnumerator en = col.GetEnumerator();

			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o3,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o4,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o5,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o6,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o7,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o8,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o9,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o10,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o11,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o12,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);	
		}

		[Test]
		public void GetEnumerator_2()
		{
			// Collection index must be in the range 1 to the size of the collection.
			Collection col = new Collection();

			col.Add("a",null,null,null);

			IEnumerator en = col.GetEnumerator();
			
			object o = en.Current;
		}

		[Test]
		public void GetEnumerator_3()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;
			Single o5 = 1.1F;
			Double o6 = 2.2;
			Decimal o7 = 1000;
			String o8 = "abc";
			Object o9 = null;
			bool o10 = true;
			Char o11 = 'c';
			DateTime o12 = DateTime.Parse("5/31/1993");

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);
			col.Add(o4,null,null,null);
			col.Add(o5,null,null,null);
			col.Add(o6,null,null,null);
			col.Add(o7,null,null,null);
			col.Add(o8,null,null,null);
			col.Add(o9,null,null,null);
			col.Add(o10,null,null,null);
			col.Add(o11,null,null,null);
			col.Add(o12,null,null,null);

			IEnumerator en = col.GetEnumerator();

			while(en.MoveNext());

			Assert.AreEqual(null,en.Current);

			en.Reset();
	
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o3,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o4,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o5,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o6,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o7,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o8,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o9,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o10,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o11,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o12,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);
		}

		[Test]
		public void GetEnumerator_4()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			en.MoveNext();

			col.Add(o4,null,null,null);

			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o3,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o4,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_5()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			en.MoveNext();

			col.Add(o4,null,1,null);

			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o3,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);

			col.Add(o4,null,null,null);
			Assert.AreEqual(null,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_10()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;
			long o4 = 1000;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			col.Add(o4,null,1,null);

			en.MoveNext();

			Assert.AreEqual(o4,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o3,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);

			col.Add(o4,null,null,null);

			Assert.AreEqual(null,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_11()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o1,en.Current);

			col.Remove(1);
			
			object o = en.Current;
			Assert.AreEqual (o1, o, "#01");
		}

		[Test]
		public void GetEnumerator_12()
		{
			Collection col = new Collection();

			string s1 = "e";
			string s2 = "g";
			string s3 = "a";
			string s4 = "f";

			col.Add(s1,null,null,null);
			col.Add(s2,null,null,null);
			col.Add(s3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(s1,en.Current);

			col.Remove(1);
			
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(s2,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(s3,en.Current);
			Assert.IsFalse(en.MoveNext());

			col.Add(s4,null,null,null);

			Assert.AreEqual(null,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_13()
		{
			Collection col = new Collection();

			string s1 = "e";
			string s2 = "g";
			string s3 = "a";
			string s4 = "f";
			string s5 = "q";

			col.Add(s1,null,null,null);
			col.Add(s2,null,null,null);
			

			IEnumerator en = col.GetEnumerator();

			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(s1,en.Current);		
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(s2,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);

			col.Add(s3,null,null,null);

			Assert.AreEqual(null,en.Current);

			col.Add(s4,null,null,null);
			col.Add(s5,null,null,null);
			
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_6()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			en.MoveNext();

			col.Remove(3);

			Assert.AreEqual(o1,en.Current);
			Assert.IsTrue(en.MoveNext());
			Assert.AreEqual(o2,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_7()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			en.MoveNext();

			col.Remove(1);

			object o = en.Current;
		}

		[Test]
		public void GetEnumerator_8()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			en.MoveNext();

			col.Remove(3);
			col.Remove(2);

			Assert.AreEqual(o1,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		public void GetEnumerator_9()
		{
			Collection col = new Collection();

			Byte o1 = 1;
			short o2 = 1;
			int o3 = 1;

			col.Add(o1,null,null,null);
			col.Add(o2,null,null,null);
			col.Add(o3,null,null,null);

			IEnumerator en = col.GetEnumerator();

			Assert.IsTrue(en.MoveNext());
			Assert.IsTrue(en.MoveNext());
			Assert.IsTrue(en.MoveNext());
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);
			Assert.IsFalse(en.MoveNext());
			Assert.AreEqual(null,en.Current);
			Assert.IsFalse(en.MoveNext());						
		}
		// Test GetEnumerator method
		[Test]
		public void GetEnumerator ()
		{
			Collection c;
			IEnumerator e;
			object[] o = new object[4] {typeof(int), 
										   typeof(double), typeof(string), typeof(object)};
			int i = 0;

			c = new Collection ();

			c.Add (typeof (int), null, null, null);
			c.Add (typeof (double), null, null, null);
			c.Add (typeof (string), null, null, null);
			c.Add (typeof (object), null, null, null);

			e = c.GetEnumerator ();

			Assert.IsNotNull (e, "#GE01");

			while (e.MoveNext ()) 
			{
				Assert.AreEqual (o[i], e.Current, "#GE02." + i.ToString ());
				i++;
			}

			e.Reset ();
			e.MoveNext ();

			Assert.AreEqual (o[0], e.Current, "#GE03");
		}

		// Test GetEnumerator method again, this time using foreach
		[Test]
		public void Foreach ()
		{
			Collection c;
			object[] o = new object[4] {typeof(int), 
										   typeof(double), typeof(string), typeof(object)};
			int i = 0;

			c = new Collection ();

			c.Add (typeof (int), null, null, null);
			c.Add (typeof (double), null, null, null);
			c.Add (typeof (string), null, null, null);
			c.Add (typeof (object), null, null, null);


			foreach (object item in c) 
			{
				Assert.AreEqual (o[i], item, "#fe01." + i.ToString ());
				i++;
			}

		}

		#endregion

		#region Count Tests

		[Test]
		public void Count_1()
		{
			Collection col = new Collection();

			Assert.AreEqual(0,col.Count);

			col.Add("a",null,null,null);
			Assert.AreEqual(1,col.Count);

			col.Add("b",null,null,null);
			Assert.AreEqual(2,col.Count);

			col.Add("c",null,null,null);
			Assert.AreEqual(3,col.Count);

			col.Add("d",null,null,null);
			Assert.AreEqual(4,col.Count);

			col.Remove(1);
			col.Remove(1);

			Assert.AreEqual(2,col.Count);

			col.Remove(1);
			col.Remove(1);

			Assert.AreEqual(0,col.Count);
		}

		#endregion

		#region IList Tests

		[Test]
		public void IList_1()
		{
			Collection col = new Collection();

			string s = "abc";
			col.Add(s,null,null,null);

			Assert.AreEqual(false,((IList)col).IsFixedSize);
			Assert.AreEqual(false,((IList)col).IsReadOnly);
			Assert.AreEqual(false,((IList)col).IsSynchronized);
		}

		[Test]
		public void IList_Add()
		{
			Collection col = new Collection();

			string s = "abc";

			((IList)col).Add(s);
			((IList)col).Add(s);

			Assert.AreEqual(s,col[1]);
			Assert.AreEqual(s,col[2]);
		}

		[Test]
		public void IList_Contains()
		{
			Collection col = new Collection();

			string s = "abc";

			((IList)col).Add(s);
			((IList)col).Add(s);

			Assert.IsTrue(((IList)col).Contains(s));
		}

		[Test]
		public void IList_CopyTo()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s3);
			((IList)col).Add(s2);
			((IList)col).Add(s1);

			object[] oarr = new object[6];

			((IList)col).CopyTo(oarr,0);

			Assert.AreEqual(s1,oarr[0]);
			Assert.AreEqual(s2,oarr[1]);
			Assert.AreEqual(s3,oarr[2]);
			Assert.AreEqual(s3,oarr[3]);
			Assert.AreEqual(s2,oarr[4]);
			Assert.AreEqual(s1,oarr[5]);
		}

		[Test]
		public void IList_Count()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s3);
			((IList)col).Add(s2);
			((IList)col).Add(s1);

			Assert.AreEqual(6,((IList)col).Count);
		}

		[Test]
		public void IList_IndexOf()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);

			Assert.AreEqual(0,((IList)col).IndexOf(s1));
			Assert.AreEqual(1,((IList)col).IndexOf(s2));
			Assert.AreEqual(2,((IList)col).IndexOf(s3));
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void IList_Insert_1()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);

			((IList)col).Insert(-1,s1+s1);
			((IList)col).Insert(2,s2+s2);
			((IList)col).Insert(4,s3+s3);

			Assert.AreEqual(9,col.Count);
			Assert.AreEqual(s1+s1,col[1]);
			Assert.AreEqual(s1,col[2]);
			Assert.AreEqual(s2,col[3]);
			Assert.AreEqual(s2+s2,col[4]);
			Assert.AreEqual(s3,col[5]);
			Assert.AreEqual(s3+s3,col[6]);
			Assert.AreEqual(s1,col[7]);
			Assert.AreEqual(s2,col[8]);
			Assert.AreEqual(s3,col[9]);

			((IList)col).Insert(8,s3 + s3 + s3);

			Assert.AreEqual(10,col.Count);
			Assert.AreEqual(s3 + s3 + s3,col[10]);
		}

		[Test]
		public void IList_Insert_2()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);

			((IList)col).Insert(6,s1+s1);

			Assert.AreEqual ("abc3", col [6], "#01");
			Assert.AreEqual ("abc2", col [5], "#02");
			Assert.AreEqual ("abc1abc1", col [7], "#03");
		}

		[Test]
		public void IList_Item()
		{
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);

			Assert.AreEqual(s1,((IList)col)[0]);
			Assert.AreEqual(s2,((IList)col)[1]);
			Assert.AreEqual(s3,((IList)col)[2]);
			Assert.AreEqual(s1,((IList)col)[3]);
			Assert.AreEqual(s2,((IList)col)[4]);
			Assert.AreEqual(s3,((IList)col)[5]);
		}

		[Test]
		[Category("NotWorking")] // Unstable behaviour in .Net
		public void IList_Remove()
		{
			/*
			Collection col = new Collection();

			string s1 = "abc1";
			string s2 = "abc2";
			string s3 = "abc3";

			((IList)col).Add(s1);
			((IList)col).Add(s2);
			((IList)col).Add(s3);
			((IList)col).Add(s3);
			((IList)col).Add(s2);
			((IList)col).Add(s1);

			((IList)col).Remove(s2);

			Assert.AreEqual(5,col.Count);

			Assert.AreEqual(s1,col[1]);
			Assert.AreEqual(s3,col[2]);
			Assert.AreEqual(s3,col[3]);
			Assert.AreEqual(s2,col[4]);
			Assert.AreEqual(s1,col[5]);
			
			((IList)col).Remove(s1);

			Assert.AreEqual(4,col.Count);

			Assert.AreEqual(s3,col[1]);
			Assert.AreEqual(s3,col[2]);
			Assert.AreEqual(s2,col[3]);
			Assert.AreEqual(s1,col[4]);

			((IList)col).Remove(s1);

			Assert.AreEqual(3,col.Count);

			Assert.AreEqual(s3,col[1]);
			Assert.AreEqual(s3,col[2]);
			Assert.AreEqual(s2,col[3]);
			*/

		}

		#endregion

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Dirty_Read()
		{
			Collection col = new Collection();

			string s = "abc";

			col.Add(s,null,null,null);
			col.Add(s,null,null,null);
			col.Add(s,null,null,null);
			col.Add(s,null,null,null);

			object o = col["abc"];
		}




		
		// Test all the Exceptions we're supposed to throw
		[Test]
		public void Exception ()
		{
			Collection c = new Collection ();

			try 
			{
				// nothing in Collection yet
				object o = c[0];
				Assert.Fail ("#E02");
			} 
			catch (IndexOutOfRangeException) 
			{
			}

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Football", "Foot", null, null);
			c.Add ("Basketball", "Basket", null, null);
			c.Add ("Volleyball", "Volley", null, null);

			try 
			{
				// only 4 elements
				object o = c[5];
				Assert.Fail ("#E04");
			} 
			catch (IndexOutOfRangeException) 
			{
			}

			try 
			{
				// Collection class is 1-based
				object o = c[0];
				Assert.Fail ("#E06");
			} 
			catch (IndexOutOfRangeException) 
			{
			}

			try 
			{
				// no member with Key == "Kick"
				object o = c["Kick"];
				Assert.Fail ("#E08");
			} 
			catch (ArgumentException) 
			{
				// FIXME
				// VB Language Reference says IndexOutOfRangeException 
				// here, but MS throws ArgumentException
			}

			try 
			{
				// Even though Indexer is an object, really it's a string
				object o = c[typeof (int)];
				Assert.Fail ("#E10");
			} 
			catch (ArgumentException) 
			{
			}

			try 
			{
				// can't specify both Before and After
				c.Add ("Kickball", "Kick", "Volley", "Foot");
				Assert.Fail ("#E12");
			} 
			catch (ArgumentException) 
			{
			}

			try 
			{
				// Key "Foot" already exists
				c.Add ("Kickball", "Foot", null, null);
				Assert.Fail ("#E14");
			} 
			catch (ArgumentException) 
			{
			}

			try 
			{
				// Even though Before is object, it's really a string
				c.Add ("Dodgeball", "Dodge", typeof (int), null);
				Assert.Fail ("#E16");
			} 
			catch (InvalidCastException) 
			{
			}

			try 
			{
				// Even though After is object, it's really a string
				c.Add ("Wallyball", "Wally", null, typeof (int));
				Assert.Fail ("#E18");
			} 
			catch (InvalidCastException) 
			{
			}

			try 
			{
				// have to pass a legitimate value to remove
				c.Remove (null);
				Assert.Fail ("#E20");
			} 
			catch (ArgumentNullException) 
			{
			}

			try 
			{
				// no Key "Golf" exists
				c.Remove ("Golf");
				Assert.Fail ("#E22");
			} 
			catch (ArgumentException) 
			{
			}

			try 
			{
				// no Index 10 exists
				c.Remove (10);
				Assert.Fail ("#E24");
			} 
			catch (IndexOutOfRangeException) 
			{
			}

			try 
			{
				IEnumerator e = c.GetEnumerator ();

				// Must MoveNext before Current
				object item = e.Current;
				Assert.IsNull (item, "#E25");
			} 
			catch (IndexOutOfRangeException) 
			{
				Assert.Fail ("#E27");
			}

			try 
			{
				IEnumerator e = c.GetEnumerator ();
				e.MoveNext ();

				c.Add ("Paintball", "Paint", null, null);

				// Can't MoveNext if Collection has been modified
				e.MoveNext ();

				// FIXME
				// On-line help says this should throw an error. MS doesn't.
			} 
			catch (Exception) 
			{
				Assert.Fail ("#E28");
			}

			try 
			{
				IEnumerator e = c.GetEnumerator ();
				e.MoveNext ();

				c.Add ("Racketball", "Racket", null, null);

				// Can't Reset if Collection has been modified
				e.Reset ();

				// FIXME
				// On-line help says this should throw an error. MS doesn't.
			} 
			catch (InvalidOperationException) 
			{
				Assert.Fail ("#E30");
			}
		}

		[Test]
		public void IList_Remove_2 ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			list.Remove (null);

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);

			Assert.AreEqual (2, c.Count, "#1");

			try 
			{
				list.Contains (null);
				Assert.Fail ("#2");
			} 
			catch (NullReferenceException) 
			{
			}

			Assert.AreEqual (2, c.Count, "#3");

			list.Remove (c.GetType ());
			Assert.AreEqual (2, c.Count, "#4");

			list.Remove (1);
			Assert.AreEqual (2, c.Count, "#5");

			list.Remove ("Something");
			Assert.AreEqual (2, c.Count, "#6");

			list.Remove ("Baseball");
			Assert.AreEqual (1, c.Count, "#7");
			Assert.AreEqual ("Paintball", c[1], "#8");
		}

		[Test]
		public void IList_RemoveAt ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			try 
			{
				list.RemoveAt (0);
				Assert.Fail ("#1");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			try 
			{
				list.RemoveAt (-1);
				Assert.Fail ("#2");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);

			Assert.AreEqual (2, c.Count, "#3");
			Assert.AreEqual ("Baseball", list[0], "#4");

			list.RemoveAt (0);
			Assert.AreEqual (1, c.Count, "#5");
			Assert.AreEqual ("Paintball", list[0], "#6");

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Basketball", "Basket", null, null);

			Assert.AreEqual ("Paintball", list[0], "#7");
			Assert.AreEqual ("Baseball", list[1], "#8");
			Assert.AreEqual ("Basketball", list[2], "#9");

			try 
			{
				list.RemoveAt (3);
				Assert.Fail ("#10");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			list.RemoveAt (-1);
			Assert.AreEqual (2, c.Count, "#11");
			Assert.AreEqual ("Baseball", list[0], "#12");
			Assert.AreEqual ("Basketball", list[1], "#13");
		}

		[Test]
		public void IList_IndexOf_2 ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			Assert.AreEqual (-1, list.IndexOf (null), "#1");

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);
			c.Add (5, "6", null, null);

			try 
			{
				list.IndexOf (null);
				Assert.Fail ("#2");
			} 
			catch (NullReferenceException) 
			{
			}

			Assert.AreEqual (0, list.IndexOf ("Baseball"), "#3");
			Assert.AreEqual (-1, list.IndexOf ("Base"), "#4");

			Assert.AreEqual (1, list.IndexOf ("Paintball"), "#5");
			Assert.AreEqual (-1, list.IndexOf ("Pain"), "#6");

			Assert.AreEqual (2, list.IndexOf (5), "#7");
			Assert.AreEqual (-1, list.IndexOf (6), "#8");

			Assert.AreEqual (-1, list.IndexOf ("Something"), "#9");
		}

		[Test]
		public void IList_Contains_2 ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			Assert.IsFalse (list.Contains (null), "#1");

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);
			c.Add (5, "6", null, null);

			try 
			{
				list.Contains (null);
				Assert.Fail ("#2");
			} 
			catch (NullReferenceException) 
			{
			}

			Assert.AreEqual (true, list.Contains ("Baseball"), "#3");
			Assert.AreEqual (false, list.Contains ("Base"), "#4");

			Assert.AreEqual (true, list.Contains ("Paintball"), "#5");
			Assert.AreEqual (false, list.Contains ("Paint"), "#6");

			Assert.AreEqual (true, list.Contains (5), "#7");
			Assert.AreEqual (false, list.Contains (6), "#8");

			Assert.AreEqual (false, list.Contains ("Something"), "#9");
		}

		[Test]
		public void IList_Indexer_Get ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			try 
			{
				object value = list[0];
				Assert.Fail ("#1");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			try 
			{
				object value = list[-1];
				Assert.Fail ("#2");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);
			c.Add (5, "6", null, null);

			Assert.AreEqual ("Baseball", list[0], "#3");
			Assert.AreEqual ("Paintball", list[1], "#4");
			Assert.AreEqual (5, list[2], "#5");

			try 
			{
				object value = list[3];
				Assert.Fail ("#6");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			object val = list[-1];
			Assert.AreEqual ("Baseball", val, "#6");
			Assert.AreEqual ("Baseball", list [-2], "#7");
			Assert.AreEqual ("Baseball", list [int.MinValue], "#8");
		}

		[Test]
		public void IList_Indexer_Set ()
		{
			Collection c = new Collection ();
			IList list = (IList) c;

			try 
			{
				list[0] = "Baseball";
				Assert.Fail ("#1");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			try 
			{
				list[-1] = "Baseball";
				Assert.Fail ("#2");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			c.Add ("Baseball", "Base", null, null);
			c.Add ("Paintball", "Paint", null, null);
			c.Add (5, "6", null, null);

			Assert.AreEqual (3, c.Count, "#3");

			list[0] = "Basketball";
			list[2] = "Six";

			Assert.AreEqual (3, c.Count, "#4");
			Assert.AreEqual ("Basketball", list[0], "#5");
			Assert.AreEqual ("Paintball", list[1], "#6");
			Assert.AreEqual ("Six", list[2], "#7");

			try 
			{
				list[3] = "Baseball";
				Assert.Fail ("#8");
			} 
			catch (ArgumentOutOfRangeException) 
			{
			}

			list[-1] = "Whatever";
			Assert.AreEqual (3, c.Count, "#8");
			Assert.AreEqual ("Whatever", list[0], "#9");
			Assert.AreEqual ("Paintball", list[1], "#10");
			Assert.AreEqual ("Six", list[2], "#11");
		}
	

	}
}
