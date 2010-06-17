// SpecialDirectoriesProxyTest.cs - NUnit Test Cases for Microsoft.VisualBasic.MyServices.SpecialDirectoriesProxy
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
using Microsoft.VisualBasic.MyServices;

namespace MonoTests.Microsoft_VisualBasic.MyServices
{
	[TestFixture]
	public class SpecialDirectoriesProxyTest
	{
#if !TARGET_JVM
		[Category ("TargetJvmNotSupported")]//System.Windows.Forms.Application, ServerComputer.FileSystem property
		[Test]
		public void PathTest()
		{
			Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
			Microsoft.VisualBasic.MyServices.SpecialDirectoriesProxy sd = pc.FileSystem.SpecialDirectories;
			Assert.AreEqual (FixPath(System.Windows.Forms.Application.CommonAppDataPath), sd.AllUsersApplicationData, "AllUserApplicationData");
			Assert.AreEqual (FixPath (System.Windows.Forms.Application.UserAppDataPath), sd.CurrentUserApplicationData, "CurrentUserApplicationData");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.Desktop)), sd.Desktop, "Desktop");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)), sd.MyDocuments, "MyDocuments");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyMusic)), sd.MyMusic, "MyMusic");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.MyPictures)), sd.MyPictures, "MyPictures");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFiles)), sd.ProgramFiles, "ProgramFiles");
			Assert.AreEqual (FixPath (Environment.GetFolderPath (Environment.SpecialFolder.Programs)), sd.Programs, "Programs");
			Assert.AreEqual (FixPath (System.IO.Path.GetTempPath ()), sd.Temp, "Temp");
		}
#endif
		
		string FixPath (string path) 
		{
			return path.Replace (@"\\", @"\").TrimEnd (Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}
	}
}
