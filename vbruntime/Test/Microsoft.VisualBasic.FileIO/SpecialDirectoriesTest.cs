// SpecialDirectoriesTest.cs - NUnit Test Cases for Microsoft.VisualBasic.FileUI.SpecialDirectories
//
// Rolf Bjarne Kvinge  (RKvinge@novell.com)
//
// 
// Copyright (C) 2007 Novell, Inc (http://www.novell.com)
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
#if NET_2_0
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.VisualBasic.FileIO;

namespace MonoTests.Microsoft_VisualBasic.FileIO
{
	[TestFixture]
	public class SpecialDirectoriesTest
	{
		[Test]
		public void PathTest()
		{
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.AllUsersApplicationData, RemoveBS (System.Windows.Forms.Application.CommonAppDataPath), "AllUserApplicationData");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.CurrentUserApplicationData, RemoveBS (System.Windows.Forms.Application.UserAppDataPath), "CurrentUserApplicationData");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.Desktop)), "Desktop");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)), "MyDocuments");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyMusic, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.MyMusic)), "MyMusic");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.MyPictures)), "MyPictures");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.ProgramFiles, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFiles)), "ProgramFiles");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.Programs, RemoveBS (Environment.GetFolderPath (Environment.SpecialFolder.Programs)), "Programs");
			Assert.AreEqual (Microsoft.VisualBasic.FileIO.SpecialDirectories.Temp, RemoveBS (System.IO.Path.GetTempPath ()), "Temp");
		}
		
		string RemoveBS (string path) 
		{
			if (path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString())) 
				return path.Substring (0, path.Length -1);
			else {
				return path;
			}
		}
	}
}
#endif