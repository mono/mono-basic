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
using Microsoft.Win32;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class InteractionTests
	{
		public InteractionTests()
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
	
		#region Partition tests

		[Test]
		public void Partition_1()
		{

	        String str1;
	        str1 = Interaction.Partition(1, 0, 9, 5);
			str1 = str1 + Interaction.Partition(1, 0, 9, 5);
	        str1 = str1 + Interaction.Partition(1, 20, 199, 10);
	        str1 = str1 + Interaction.Partition(1, 100, 1010, 20);

			Assert.AreEqual(" 0: 4 0: 4   : 19    :  99" ,str1);

		}
		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void Partition_2()
		{
			string str_tmp;
			str_tmp = Interaction.Partition(12,5,3,2);
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void Partition_3()
		{
			string str_tmp;
			str_tmp = Interaction.Partition(12,5,7,0);
		}

		[Test]
		public void Partition_4()
		{

			String str1 = "";
			str1 = str1 + Interaction.Partition(267, 100, 24469, 1);

			Assert.AreEqual("  267:  267" ,str1);

		}

		#endregion

#if !TARGET_JVM			
		#region GetAllSettings tests

		[Test]
		public void GetAllSettings_1()
		{

		string[,] res_setting;
        int index, elm_count;
        string tmp_str;
        RegistryKey regk;
		string[] arr_str;


		regk = Registry.CurrentUser;
		regk = regk.CreateSubKey( "Test_APP");
		regk = regk.OpenSubKey("GetAllSettings_1");

        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go1", "Val_Go1");
        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go2", "Val_Go2");
        Interaction.SaveSetting("Test_APP", "GetAllSettings_1", "Go3", "Val_Go3");

        res_setting = Interaction.GetAllSettings("Test_APP", "GetAllSettings_1");
        
        Assert.AreEqual("Go2",res_setting[1,0]);
		Assert.AreEqual("Val_Go2",res_setting[1,1]);
		}
		[Test]
		public void GetAllSettings_2()
		{

			string[,] res_setting;

			res_setting = Interaction.GetAllSettings("Test_APP", "rterr");
        
			Assert.AreEqual(null,res_setting);
			
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void GetAllSettings_3()
		{
			string[,] str_tmp;
			str_tmp = Interaction.GetAllSettings("","TEST2");
		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void GetAllSettings_4()
		{
			string[,] str_tmp;
			str_tmp = Interaction.GetAllSettings("TEST",null);
		}

		#endregion
#endif

	}
}

