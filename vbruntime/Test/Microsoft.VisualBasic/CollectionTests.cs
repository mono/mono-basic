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
		}

		[TearDown]
		public void Clean() 
		{
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
		[ExpectedException(typeof(InvalidCastException))]
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
		[ExpectedException(typeof(IndexOutOfRangeException))]
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

			Assert.AreEqual(o4,en.Current);
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

			Assert.AreEqual(o4,en.Current);
			Assert.IsFalse(en.MoveNext());
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
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

			Assert.AreEqual(s4,en.Current);
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

			Assert.AreEqual(s3,en.Current);

			col.Add(s4,null,null,null);
			col.Add(s5,null,null,null);
			
			Assert.IsTrue(en.MoveNext());
			Assert.IsTrue(en.MoveNext());

			Assert.AreEqual(s5,en.Current);
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
		[ExpectedException(typeof(IndexOutOfRangeException))]
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
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
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

			Console.WriteLine(col["abc"]);
		}
	}
}
