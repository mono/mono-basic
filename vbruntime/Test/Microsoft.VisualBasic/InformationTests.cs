// InformationTests.cs - NUnit Test Cases for Microsoft.VisualBasic.Information 
//
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
using System.Collections;
using Microsoft.VisualBasic;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class InformationTests
	{
		public InformationTests()
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
	
		#region IsError tests

		[Test]
		public void IsError_1()
		{

			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 22; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = null; 
			bool oDT10 = true; 
			char oDT11 = 'c'; 
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");

			Assert.AreEqual(false,Information.IsError(oDT1));
			Assert.AreEqual(false,Information.IsError(oDT2));
			Assert.AreEqual(false,Information.IsError(oDT3));
			Assert.AreEqual(false,Information.IsError(oDT4));
			Assert.AreEqual(false,Information.IsError(oDT5));
			Assert.AreEqual(false,Information.IsError(oDT6));
			Assert.AreEqual(false,Information.IsError(oDT7));
			Assert.AreEqual(false,Information.IsError(oDT8));
			Assert.AreEqual(false,Information.IsError(oDT9));
			Assert.AreEqual(false,Information.IsError(oDT10));
			Assert.AreEqual(false,Information.IsError(oDT11));
			Assert.AreEqual(false,Information.IsError(oDT12));


		}

		[Test]
		public void IsError_2()
		{
			
			string BadArg = "BAD ARGS"; 
			object ReturnVal = new System.ArgumentOutOfRangeException(BadArg); 
			object o1;
			o1 = new ArgumentOutOfRangeException();
			Assert.AreEqual(true,Information.IsError(o1));
			Assert.AreEqual(true,Information.IsError(ReturnVal));

		}
		#endregion

		#region IsNothing tests

		[Test]
		public void IsNothing_1()
		{
			object o1;
			
			o1 = "test";
			Assert.AreEqual(false,Information.IsNothing(o1));

			o1 = null;
			Assert.AreEqual(true,Information.IsNothing(o1));
		}

		[Test]
		public void IsNothing_2()
		{
			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 22; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = null; 
			bool oDT10 = true; 
			char oDT11 = 'c'; 
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");

			Assert.AreEqual(false,Information.IsNothing(oDT1));
			Assert.AreEqual(false,Information.IsNothing(oDT2));
			Assert.AreEqual(false,Information.IsNothing(oDT3));
			Assert.AreEqual(false,Information.IsNothing(oDT4));
			Assert.AreEqual(false,Information.IsNothing(oDT5));
			Assert.AreEqual(false,Information.IsNothing(oDT6));
			Assert.AreEqual(false,Information.IsNothing(oDT7));
			Assert.AreEqual(false,Information.IsNothing(oDT8));
			Assert.AreEqual(true,Information.IsNothing(oDT9));
			Assert.AreEqual(false,Information.IsNothing(oDT10));
			Assert.AreEqual(false,Information.IsNothing(oDT11));
			Assert.AreEqual(false,Information.IsNothing(oDT12));
		}


		[Test]
		public void IsNothing_4()
		{
			test_nothing tmp_class = new test_nothing();

			Assert.AreEqual(false,Information.IsNothing(tmp_class));


		}
		internal class test_nothing
		{
		}

		#endregion

		#region RGB tests

		[Test]
		public void RGB_1()
		{
			Assert.AreEqual(0,Information.RGB(0,0,0));
			Assert.AreEqual(6579300,Information.RGB(100,100,100));
			Assert.AreEqual(13158600,Information.RGB(200,200,200));
			Assert.AreEqual(16777215,Information.RGB(255,255,290));

		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void RGB_2()
		{
			int i;
			i = Information.RGB(50,12,-1);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void RGB_3()
		{
			int i;
			i = Information.RGB(50,-1,1);
		}
		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void RGB_4()
		{
			int i;
			i = Information.RGB(-1,122,11);
		}

		[Test]
		public void RGB_5()
		{
			decimal d = 0; 
			for (int i = 0; i <= 255; i += 255) 
			{ 
				for (int j = 0; j <= 255; j += 255) 
				{ 
					for (int k = 0; k <= 255; k += 255) 
					{ 
						d = d + Information.RGB(i, j, k); 
					} 
				} 
			} 
			Assert.AreEqual(67108860,d);
		
		}

		[Test]
		public void RGB_6()
		{
			// greater than 255, 255 is used

			decimal resA1, resA2, resB1, resB2, resC1, resC2;


			resA1 = Information.RGB(255, 0, 0); 
			resA2 = Information.RGB(256, 0, 0); 
			 
			resB1 = Information.RGB(0, 255, 0); 
			resB2 = Information.RGB(0, 256, 0); 
			
			resC1 = Information.RGB(0, 0, 255); 
			resC2 = Information.RGB(0, 0, 256); 
			

			Assert.AreEqual(resA1,resA2);
			Assert.AreEqual(resB1,resB2);
			Assert.AreEqual(resC1,resC2);
			
		}

		#endregion

		#region IsNumeric tests

		[Test]
		public void IsNumeric_1()
		{
			if (Helper.OnMono)
				Assert.Ignore ("Buggy mono: #81777");
	
			object tmpobj1 = "43";
			object tmpobj2 = "343 TEST";
			Assert.AreEqual(false,Information.IsNumeric("20 werwer"));
			Assert.AreEqual(true,Information.IsNumeric("2020"));
			Assert.AreEqual(true,Information.IsNumeric("222,34"));
			Assert.AreEqual(true,Information.IsNumeric(".45"));
			Assert.AreEqual(true,Information.IsNumeric(-0.3));
			Assert.AreEqual(false,Information.IsNumeric("14.33.33"));
			Assert.AreEqual(true,Information.IsNumeric(tmpobj1));
			Assert.AreEqual(false,Information.IsNumeric(tmpobj2));
	
		}

		[Test]
		public void IsNumeric_2()
		{

			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 1; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = oDT1; 
			bool oDT10 = true; 
			char oDT11 = 'c';
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");
			string oDT13 = "";

			Assert.AreEqual(true,Information.IsNumeric(oDT1),"oDT1");
			Assert.AreEqual(true,Information.IsNumeric(oDT2),"oDT2");
			Assert.AreEqual(true,Information.IsNumeric(oDT3),"oDT3");
			Assert.AreEqual(true,Information.IsNumeric(oDT4),"oDT4");
			Assert.AreEqual(true,Information.IsNumeric(oDT5),"oDT5");
			Assert.AreEqual(true,Information.IsNumeric(oDT6));
			Assert.AreEqual(true,Information.IsNumeric(oDT7));
			Assert.AreEqual(false,Information.IsNumeric(oDT8));
			Assert.AreEqual(true,Information.IsNumeric(oDT9));
			Assert.AreEqual(true,Information.IsNumeric(oDT10));
			Assert.AreEqual(false,Information.IsNumeric(oDT11));
			Assert.AreEqual(false,Information.IsNumeric(oDT12));
			Assert.AreEqual(false,Information.IsNumeric(oDT13));
	
		}

		[Test]
		public void IsNumeric_3()
		{

			test_cls tmpobj1 = new test_cls();
			Itest_cls Itest = null;
			Assert.AreEqual(false,Information.IsNumeric(tmpobj1));
			Assert.AreEqual(false,Information.IsNumeric(Itest));	
		}
		internal interface Itest_cls
		{
		};


		#endregion

		#region IsDBNull tests

		[Test]
		public void IsDBNull_1()
		{

			object tmpobj = null;
			object tmpDBnullobj = System.DBNull.Value;

			Assert.AreEqual(false,Information.IsDBNull("werwer"));
			Assert.AreEqual(false,Information.IsDBNull(""));
			Assert.AreEqual(false,Information.IsDBNull(tmpobj));
			Assert.AreEqual(true,Information.IsDBNull(tmpDBnullobj));
	
		}

		
		[Test]
		public void IsDBNull_2()
		{

			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 22; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = null; 
			bool oDT10 = true; 
			char oDT11 = 'c'; 
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");


			Assert.AreEqual(false,Information.IsDBNull(oDT1));
			Assert.AreEqual(false,Information.IsDBNull(oDT2));
			Assert.AreEqual(false,Information.IsDBNull(oDT3));
			Assert.AreEqual(false,Information.IsDBNull(oDT4));
			Assert.AreEqual(false,Information.IsDBNull(oDT5));
			Assert.AreEqual(false,Information.IsDBNull(oDT6));
			Assert.AreEqual(false,Information.IsDBNull(oDT7));
			Assert.AreEqual(false,Information.IsDBNull(oDT8));
			Assert.AreEqual(false,Information.IsDBNull(oDT9));
			Assert.AreEqual(false,Information.IsDBNull(oDT10));
			Assert.AreEqual(false,Information.IsDBNull(oDT11));
			Assert.AreEqual(false,Information.IsDBNull(oDT12));

		}


		#endregion

		#region IsReference tests

		[Test]
		public void IsReference_1()
		{
			int i = 3;
			String tempStr = "TEST STRING";
			Object tmpObj = null;
			bool[] bArr = new bool[2];

			Assert.AreEqual(false,Information.IsReference(i));
			Assert.AreEqual(true,Information.IsReference(tempStr));
			Assert.AreEqual(true,Information.IsReference(bArr));
			Assert.AreEqual(true,Information.IsReference(tmpObj)); 
			Assert.AreEqual(false,Information.IsReference(-0.3));
		}
		[Test]
		public void IsReference_2()
		{
			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 1; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = oDT1; 
			bool oDT10 = true; 
			char oDT11 = 'c';
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");
			string oDT13 = "";
			string oDT14 = null;

			Assert.AreEqual(false,Information.IsReference(oDT1));
			Assert.AreEqual(false,Information.IsReference(oDT2));
			Assert.AreEqual(false,Information.IsReference(oDT3));
			Assert.AreEqual(false,Information.IsReference(oDT4));
			Assert.AreEqual(false,Information.IsReference(oDT5));
			Assert.AreEqual(false,Information.IsReference(oDT6));
			Assert.AreEqual(false,Information.IsReference(oDT7));
			Assert.AreEqual(true,Information.IsReference(oDT8));
			Assert.AreEqual(false,Information.IsReference(oDT9));
			Assert.AreEqual(false,Information.IsReference(oDT10));
			Assert.AreEqual(false,Information.IsReference(oDT11));
			Assert.AreEqual(false,Information.IsReference(oDT12));
			Assert.AreEqual(true,Information.IsReference(oDT13));
			Assert.AreEqual(true,Information.IsReference(oDT14));
		}

		[Test]
		public void IsReference_3()
		{
			test_cls tmpobj1 = new test_cls();
			Itest_cls Itest = null;

			Assert.AreEqual(true,Information.IsReference(tmpobj1));
			Assert.AreEqual(true,Information.IsReference(Itest));
			
		}
		#endregion

		#region SystemTypeName tests

		[Test]
		public void SystemTypeName_1()
		{
			Assert.AreEqual("System.Boolean",Information.SystemTypeName("BOolean"));
			Assert.AreEqual("System.Byte",Information.SystemTypeName("bYte"));
			Assert.AreEqual("System.Int16",Information.SystemTypeName("Short"));
			Assert.AreEqual("System.Int32",Information.SystemTypeName("Integer"));
			Assert.AreEqual("System.Char",Information.SystemTypeName("char"));
			Assert.AreEqual("System.Single",Information.SystemTypeName("single"));
			Assert.AreEqual("System.Double",Information.SystemTypeName("double"));
			Assert.AreEqual("System.String",Information.SystemTypeName("StRing"));
			Assert.AreEqual("System.DateTime",Information.SystemTypeName("DATE")); 
			Assert.AreEqual("System.Decimal",Information.SystemTypeName("decimal"));
			Assert.AreEqual("System.Object",Information.SystemTypeName("Object"));
			Assert.AreEqual("System.DateTime",Information.SystemTypeName("date"));
			Assert.AreEqual("System.Int64",Information.SystemTypeName("Long"));
			Assert.AreEqual(null,Information.SystemTypeName("MUKY")); 
		}

		#endregion

		#region IsDate tests

		[Test]
		public void IsDate_1()
		{
			DateTime tmpDate = DateTime.Parse("5/31/1993");
			String tmpStr = "RRRR";
			object tmpObj = null;

			Assert.AreEqual(true,Information.IsDate(tmpDate));
			Assert.AreEqual(false,Information.IsDate(tmpStr));
			Assert.AreEqual(true,Information.IsDate("February 12, 1969"));
			Assert.AreEqual(false,Information.IsDate(30));
			Assert.AreEqual(false,Information.IsDate(tmpObj));

		}

		[Test]
		public void IsDate_2()
		{
			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 1; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = null; 
			bool oDT10 = true; 
			char oDT11 = 'c';

			Assert.AreEqual(false,Information.IsDate(oDT1));
			Assert.AreEqual(false,Information.IsDate(oDT2));
			Assert.AreEqual(false,Information.IsDate(oDT3));
			Assert.AreEqual(false,Information.IsDate(oDT4));
			Assert.AreEqual(false,Information.IsDate(oDT5));
			Assert.AreEqual(false,Information.IsDate(oDT6));
			Assert.AreEqual(false,Information.IsDate(oDT7));
			Assert.AreEqual(false,Information.IsDate(oDT8));
			Assert.AreEqual(false,Information.IsDate(oDT9));
			Assert.AreEqual(false,Information.IsDate(oDT10));
			Assert.AreEqual(false,Information.IsDate(oDT11));
	
		}
		#endregion

		#region IsArray tests

		[Test]
		public void IsArray_1()
		{
			String tmpStr = "RRRR";
			object tmpObj = null;
			int[] tmpArr = new int [2];
            tmpArr[1] = 2;
			byte[] oDT1 = new byte[2]; 
			short[] oDT2 = new short[2]; 
			int[] oDT3 = new int[2]; 
			long[] oDT4 = new long[2]; 
			float[] oDT5 = new float[2]; 
			double[] oDT6 = new double[2]; 
			decimal[] oDT7 = new decimal[2]; 
			string[] oDT8 = new string[2]; 
			object[] oDT9 = new object[2]; 
			bool[] oDT10 = new bool[2]; 
			char[] oDT11 = new char[2]; 
			System.DateTime[] oDT12 = new System.DateTime[2];


			Assert.AreEqual(false,Information.IsArray(tmpStr));
			Assert.AreEqual(false,Information.IsArray("February 12, 1969"));
			Assert.AreEqual(false,Information.IsArray(30));
			Assert.AreEqual(false,Information.IsArray(tmpObj));
			Assert.AreEqual(true,Information.IsArray(tmpArr));
			Assert.AreEqual(true,Information.IsArray(oDT1));
			Assert.AreEqual(true,Information.IsArray(oDT2));
			Assert.AreEqual(true,Information.IsArray(oDT3));
			Assert.AreEqual(true,Information.IsArray(oDT4));
			Assert.AreEqual(true,Information.IsArray(oDT5));
			Assert.AreEqual(true,Information.IsArray(oDT6));
			Assert.AreEqual(true,Information.IsArray(oDT7));
			Assert.AreEqual(true,Information.IsArray(oDT8));
			Assert.AreEqual(true,Information.IsArray(oDT9));
			Assert.AreEqual(true,Information.IsArray(oDT10));
			Assert.AreEqual(true,Information.IsArray(oDT11));
			Assert.AreEqual(true,Information.IsArray(oDT12));

		}

		[Test]
		public void IsArray_2()
		{
			
			byte oDT1 = 1; 
			short oDT2 = 1; 
			int oDT3 = 1; 
			long oDT4 = 1000; 
			float oDT5 = 22; 
			double oDT6 = 2.2; 
			decimal oDT7 = 1000; 
			string oDT8 = "abc"; 
			object oDT9 = null; 
			bool oDT10 = true; 
			char oDT11 = 'c'; 
			System.DateTime oDT12 = System.DateTime.Parse("5/31/1993");

			Assert.AreEqual(false,Information.IsArray(oDT1));
			Assert.AreEqual(false,Information.IsArray(oDT2));
			Assert.AreEqual(false,Information.IsArray(oDT3));
			Assert.AreEqual(false,Information.IsArray(oDT4));
			Assert.AreEqual(false,Information.IsArray(oDT5));
			Assert.AreEqual(false,Information.IsArray(oDT6));
			Assert.AreEqual(false,Information.IsArray(oDT7));
			Assert.AreEqual(false,Information.IsArray(oDT8));
			Assert.AreEqual(false,Information.IsArray(oDT9));
			Assert.AreEqual(false,Information.IsArray(oDT10));
			Assert.AreEqual(false,Information.IsArray(oDT11));
			Assert.AreEqual(false,Information.IsArray(oDT12));

		}


		[Test]
		public void IsArray_3()
		{
			test_cls[] oDT1 = new test_cls[2];
            Assert.AreEqual(true,Information.IsArray(oDT1));

		}
		internal class test_cls
		{
			private int a;
		};

		#endregion

		#region TypeName tests

		[Test]
		public void TypeName_1()
		{
			int b = 12;
			int[] a = new int[3];
			byte vbyte = 1;
			a[0] = 222;
			short shr = 2;
			double dbl = 12222;
			System.Single tmpsngl = 1;
			decimal dcm = 22;
			object obj = null;
			long tmplong = 11111;
            DBNull testBDNull = DBNull.Value;
			string tmpStr = "Test Str";
			System.DateTime tmpDate = System.DateTime.Parse("5/31/1993");
			bool tmpbool = true;
			test_cls tmp_class = new test_cls();

			Assert.AreEqual("String",Information.TypeName(tmpStr));
			Assert.AreEqual("Integer()",Information.TypeName(a));
			Assert.AreEqual("Byte",Information.TypeName(vbyte));
			Assert.AreEqual("Integer",Information.TypeName(b));
			Assert.AreEqual("DBNull",Information.TypeName(testBDNull));
			Assert.AreEqual("Double",Information.TypeName(dbl));
			Assert.AreEqual("Short",Information.TypeName(shr));
			Assert.AreEqual("Single",Information.TypeName(tmpsngl));
			Assert.AreEqual("Char",Information.TypeName('c'));
			Assert.AreEqual("Decimal",Information.TypeName(dcm));
			Assert.AreEqual("Nothing",Information.TypeName(obj)); 
			Assert.AreEqual("Date",Information.TypeName(tmpDate)); 
			Assert.AreEqual("Long",Information.TypeName(tmplong));
			Assert.AreEqual("Boolean",Information.TypeName(tmpbool));
			Assert.AreEqual("test_cls",Information.TypeName(tmp_class));

		}

		#endregion

		#region VbTypeName tests

		[Test]
		public void VbTypeName_1()
		{

			Assert.AreEqual("Integer",Information.VbTypeName("System.Int32"));
			Assert.AreEqual("Long",Information.VbTypeName("Int64"));
			Assert.AreEqual("Integer",Information.VbTypeName("Int32"));
			Assert.AreEqual("Boolean",Information.VbTypeName("boolean"));
			Assert.AreEqual("Short",Information.VbTypeName("Int16"));
			Assert.AreEqual("Double",Information.VbTypeName("double"));
			Assert.AreEqual("Decimal",Information.VbTypeName("decimal"));
			Assert.AreEqual("Decimal",Information.VbTypeName("System.Decimal"));
			Assert.AreEqual("Object",Information.VbTypeName("System.Object"));
			Assert.AreEqual("Byte",Information.VbTypeName("System.Byte"));
			Assert.AreEqual("String",Information.VbTypeName("System.String"));
			Assert.AreEqual("Char",Information.VbTypeName("system.char"));
			Assert.AreEqual("Date",Information.VbTypeName("System.DateTime"));
			Assert.AreEqual("Single",Information.VbTypeName("system.Single"));
			Assert.AreEqual("Object",Information.VbTypeName("objEct")); 
			Assert.AreEqual(null,Information.VbTypeName("BlaBla")); 

		}

		#endregion

		#region VarType tests

		[Test]
		public void VarType_1()
		{
			bool b1 = true;
			long l1 = 1000000;
			VariantType tmpVartype = VariantType.Integer;
			object tmpObj = null;
			DateTime tmpDate = DateTime.Parse("5/31/1993");
			object tmpDBnullobj = System.DBNull.Value;
			int[] tmpIntArr = new int []{1,2,3} ;
			string[] tmpStrArr = new string [2];
			MyStruct tmpStruct = new MyStruct();
			string strstr = "test";
			byte tmpByte = 1;
			short tmpShort = 2;
			int tmpInt = 22;
			Single tmpSingle = 2;
			double tmpDouble = 222.2;
			decimal tmpDec = 222;
			int[,] int_arr = new int[,] { {1, 2}, {3, 4}, {5, 6} };


			Assert.AreEqual(VariantType.String, Information.VarType(strstr));
			Assert.AreEqual(VariantType.UserDefinedType, Information.VarType(tmpStruct));
			Assert.AreEqual(VariantType.Null, Information.VarType(tmpDBnullobj));
			Assert.AreEqual(VariantType.Array | VariantType.Integer, Information.VarType(int_arr),"VariantType.Array | VariantType.Integer");
			// Information.VarType doesn`t return what docs says: int[,] should returns VariantType.Array | VariantType.Object 
			// but returns VariantType.Array | VariantType.Integer SO this is what we implement too.
			Assert.AreEqual(VariantType.Array | VariantType.Integer, Information.VarType(tmpIntArr),"VariantType.Array | VariantType.Integer 2D");
			Assert.AreEqual(VariantType.Array | VariantType.String, Information.VarType(tmpStrArr),"VariantType.Array | VariantType.String");
			Assert.AreEqual(VariantType.Date, Information.VarType(tmpDate));
			Assert.AreEqual(VariantType.Boolean, Information.VarType(b1));
			Assert.AreEqual(VariantType.Long, Information.VarType(l1));
			Assert.AreEqual(VariantType.Byte, Information.VarType(tmpByte));
			Assert.AreEqual(VariantType.Short, Information.VarType(tmpShort));
			Assert.AreEqual(VariantType.Double, Information.VarType(tmpDouble));
			Assert.AreEqual(VariantType.Single, Information.VarType(tmpSingle));
			Assert.AreEqual(VariantType.Decimal, Information.VarType(tmpDec));
			Assert.AreEqual(VariantType.Integer, Information.VarType(tmpInt),"Call VarType(tmpInt)");
			Assert.AreEqual(VariantType.Integer, Information.VarType(tmpVartype),"Call VarType(tmpVartype)");
			Assert.AreEqual(VariantType.Object, Information.VarType(tmpObj));
			Assert.AreEqual(VariantType.Char, Information.VarType('c'));

		}


		[Test]
		public void VarType_2()
		{
			try
			{
				int zero; 
				int result; 
				zero = 0; 
				result = 8 / zero;
			}
			catch(Exception ex)
			{
				Assert.AreEqual(VariantType.Error, Information.VarType(ex));
			}

		}
		internal struct MyStruct 
		{ 
			int Tel; 
		}

		#endregion

		#region QBColor tests

		[Test]
		public void QBColor_1()
		{
			int i; 
			decimal d = 0; 
			for (i = 0; i <= 15; i++) 
			{ 
				d = d + Information.QBColor(i); 
			} 

			Assert.AreEqual(113427132,d);
		}


		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void QBColor_2()
		{
			int i;
			i = Information.QBColor(17);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void QBColor_3()
		{
			int i;
			i = Information.QBColor(-1);
		}

		#endregion

	}
}



