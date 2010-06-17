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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;
using Microsoft.VisualBasic.FileIO;

namespace MonoTests.Microsoft_VisualBasic.FileIO
{
	[TestFixture]
	public class SpecialDirectoriesTest
	{
#if !TARGET_JVM
		[Category ("TargetJvmNotSupported")]//System.Windows.Forms.Application, ServerComputer.FileSystem property
		[Test]
		public void PathTest()
		{
			Assert.AreEqual (FixPath (System.Windows.Forms.Application.CommonAppDataPath), SpecialDirectories.AllUsersApplicationData, "AllUserApplicationData");
			Assert.AreEqual (FixPath (System.Windows.Forms.Application.UserAppDataPath), SpecialDirectories.CurrentUserApplicationData, "CurrentUserApplicationData");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.Desktop)), SpecialDirectories.Desktop, "Desktop");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)), SpecialDirectories.MyDocuments, "MyDocuments");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyMusic)), SpecialDirectories.MyMusic, "MyMusic");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyPictures)), SpecialDirectories.MyPictures, "MyPictures");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFiles)), SpecialDirectories.ProgramFiles, "ProgramFiles");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.Programs)), SpecialDirectories.Programs, "Programs");
			Assert.AreEqual (FixPath (System.IO.Path.GetTempPath ()), SpecialDirectories.Temp, "Temp");
		}
#endif
		
		string FixPath (string path) 
		{	// For some reason VB may return paths with \\ in them instead of just \.
			// So fix them so that the tests run correctly on MS runtime.
			return path.Replace (@"\\", @"\").TrimEnd (Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}
	}
}
