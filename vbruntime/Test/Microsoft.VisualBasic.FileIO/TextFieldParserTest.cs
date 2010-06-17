// TextFieldParserTest.cs - NUnit Test Cases for Microsoft.VisualBasic.FileIO.TextFieldParser
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
using NUnit.Framework;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using System.IO;

namespace MonoTests.Microsoft_VisualBasic.FileIO
{
	[TestFixture]
	public class TextFieldParserTest
	{
		[Test]
		public void CloseTest ()
		{
			TextFieldParser t = new TextFieldParser (new System.IO.MemoryStream());
			t.Close ();
			t.Close ();
		}
		
		[Test]
		public void DelimitedTest1 ()
		{
			string [] delimiters;
			string text;
			
			delimiters = new string [] {";"};
			text = "a;bb;ccc;dddd" + Constants.vbNewLine + "111;22;3";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				Assert.AreEqual ("a:bb:ccc:dddd", Strings.Join (t.ReadFields (), ":"), "#A1");
				Assert.AreEqual ("111:22:3", Strings.Join (t.ReadFields (), ":"), "#A2");
				Assert.AreEqual (null, Strings.Join (t.ReadFields (), ":"), "#A3");
			}

			delimiters = new string [] { ";" };
			text = "a;bb;ccc;dddd" + Constants.vbNewLine + "111;22;3";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("a:bb:ccc:dddd", Strings.Join (t.ReadFields (), ":"), "#B1");
				Assert.AreEqual ("111:22:3", Strings.Join (t.ReadFields (), ":"), "#B2");
				Assert.AreEqual (null, Strings.Join (t.ReadFields (), ":"), "#B3");
			}


			delimiters = new string [] { ";" };
			text = "a;bb;ccc;dddd" + Constants.vbNewLine + "\"111;22\";\"3\"";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("a:bb:ccc:dddd", Strings.Join (t.ReadFields (), ":"), "#C1");
				Assert.AreEqual ("111;22:3", Strings.Join (t.ReadFields (), ":"), "#C2");
				Assert.AreEqual (null, Strings.Join (t.ReadFields (), ":"), "#C3");
			}


			delimiters = new string [] { ";" };
			text = "\"";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				try {
					t.ReadFields ();
					Assert.Fail ("#Dx1 - Expected MalformedLineException");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 1 cannot be parsed using the current Delimiters.", ex.Message, "#Dx2");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#Dx3 - Expected MalformedLineException");
				}
			}


			delimiters = new string [] { "a", "bb"};
			text = "a;bb;ccc;dddd" + Constants.vbNewLine + "\"111;22\";\"3\"";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("?;?;ccc;dddd", Strings.Join (t.ReadFields (), "?"), "#E1");
				try {
					t.ReadFields ();
					Assert.Fail ("#Ex1 - Expected MalformedLineException");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 2 cannot be parsed using the current Delimiters.", ex.Message, "#Ex2");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#Ex3 - Expected MalformedLineException");
				}
			}


			delimiters = new string [] { ";" };
			text = "a\"b\"c";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("a\"b\"c", Strings.Join (t.ReadFields (), "?"), "#F1");
			}

			delimiters = new string [] { "a", "aa", "aaa" };
			text = "a";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				Assert.AreEqual ("?", Strings.Join (t.ReadFields (), "?"), "#G1");
			}

			delimiters = new string [] { "aa", "a", "aaa" };
			text = "aaaaaaaaa";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				Assert.AreEqual ("?????", Strings.Join (t.ReadFields (), "?"), "#H1");
			}

			delimiters = new string [] { ";" };
			text = "\"";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				try {
					t.ReadFields ();
					Assert.Fail ("#Ix1 - Expected MalformedLineException");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 1 cannot be parsed using the current Delimiters.", ex.Message, "#Ix2");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#Ix3 - Expected MalformedLineException");
				}
			}


			delimiters = new string [] { ";" };
			text = "\"a";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				try {
					t.ReadFields ();
					Assert.Fail ("#Jx1 - Expected MalformedLineException");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 1 cannot be parsed using the current Delimiters.", ex.Message, "#Jx2");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#Jx3 - Expected MalformedLineException");
				}
			}

			delimiters = new string [] { "aa", "a", "aaa" };
			text = "\"aaaaaaaaa\"";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("aaaaaaaaa", Strings.Join (t.ReadFields (), "?"), "#K1");
			}

			delimiters = new string [] { "aa", "a", "aaa" };
			text = "aba";
			using (StringReader reader = new StringReader (text))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetDelimiters (delimiters);
				t.TextFieldType = FieldType.Delimited;
				t.HasFieldsEnclosedInQuotes = true;
				Assert.AreEqual ("?b?", Strings.Join (t.ReadFields (), "?"), "#L1");
			}
		}
		
		[Test]
		public void FixedTest1 ()
		{
			using (StringReader reader = new StringReader ("abcdef" + Constants.vbNewLine + "1234" + Constants.vbNewLine + "ghijklmno" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetFieldWidths (new int [] {1, 3, 2});
				t.TextFieldType = FieldType.FixedWidth;
				Assert.AreEqual ("a;bcd;ef", Strings.Join (t.ReadFields (), ";"), "#01");
				try {
					Assert.AreEqual ("1;234", Strings.Join (t.ReadFields (), ";"), "#02");
					Assert.Fail ("#E3 - Expected 'MalformedLineException'");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 2 cannot be parsed using the current FieldWidths.", ex.Message, "#E1");
					Assert.AreEqual (2, ex.LineNumber, "#E2");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#E4 - Expected 'MalformedLineException'");
				}	
				Assert.AreEqual ("g;hij;kl", Strings.Join (t.ReadFields (), ";"), "#03");
			}


			using (StringReader reader = new StringReader ("abcdef" + Constants.vbNewLine + "1234" + Constants.vbNewLine + "ghijklmno" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetFieldWidths (new int [] {});
				t.TextFieldType = FieldType.FixedWidth;
				try {
					Assert.AreEqual ("a;bcd;ef", Strings.Join (t.ReadFields (), ";"), "#11");
					Assert.Fail ("#E12 - Expected 'InvalidOperationException'");
				} catch (InvalidOperationException ex) {
					Assert.AreEqual ("Unable to read fixed width fields because FieldWidths is Nothing or empty.", ex.Message, "#E11");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#E13 - Expected 'InvalidOperationException'");
				}
			}


			using (StringReader reader = new StringReader (" bcdef" + Constants.vbNewLine + "1 234" + Constants.vbNewLine + "gh klmno" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.SetFieldWidths (new int [] { 1, 3, 2 });
				t.TextFieldType = FieldType.FixedWidth;
				Assert.AreEqual (";bcd;ef", Strings.Join (t.ReadFields (), ";"), "#21");
				try {
					Assert.AreEqual ("1;234", Strings.Join (t.ReadFields (), ";"), "#22");
					Assert.Fail ("#E23 - Expected 'MalformedLineException'");
				} catch (MalformedLineException ex) {
					Assert.AreEqual ("Line 2 cannot be parsed using the current FieldWidths.", ex.Message, "#E21");
					Assert.AreEqual (2, ex.LineNumber, "#E22");
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#E24 - Expected 'MalformedLineException'");
				}
				Assert.AreEqual ("g;h k;lm", Strings.Join (t.ReadFields (), ";"), "#23");
			}

		}
		
		[Test]
		public void SetFieldWidhtsTest ()
		{
			using (StringReader reader = new StringReader ("abcd" + Constants.vbNewLine + "efgh" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.TextFieldType = FieldType.Delimited;
				t.SetFieldWidths (new int [] { 1, 3, 2 });
				Assert.AreEqual ("1;3;2", Helper.Join (t.FieldWidths, ";"), "#01");
				Assert.AreEqual (FieldType.Delimited, t.TextFieldType, "#02");
			}
		}

		[Test]
		public void SetDelimitersTest ()
		{
			using (StringReader reader = new StringReader ("abcd" + Constants.vbNewLine + "efgh" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				t.TextFieldType = FieldType.FixedWidth;
				t.SetDelimiters (";", ":");
				Assert.AreEqual (";?:", Helper.Join (t.Delimiters, "?"), "#01");
				Assert.AreEqual (FieldType.FixedWidth, t.TextFieldType, "#02");
			}
		}
		
		[Test]
		public void PeekTest ()
		{

			using (StringReader reader = new StringReader ("abcd" + Constants.vbNewLine + "efgh" + Constants.vbNewLine + "'comment" + Constants.vbNewLine + "after comment" + Constants.vbNewLine))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				Assert.AreEqual ("a", t.PeekChars (1), "#01");
				Assert.AreEqual ("a", t.PeekChars (1), "#02");
				Assert.AreEqual ("ab", t.PeekChars (2), "#03");
				Assert.AreEqual ("abcd", t.PeekChars (10), "#04");
				Assert.AreEqual ("abcd", t.ReadLine (), "#05");
				Assert.AreEqual ("ef", t.PeekChars (2), "#06");
				
				try {
					t.PeekChars (0);
				} catch (ArgumentException ex){
					Helper.RemoveWarning (ex);
				} catch (Exception ex) {
					Helper.RemoveWarning (ex); 
					Assert.Fail ("#07 - Expected 'ArgumentException'");
				}

				try {
					t.PeekChars (-1);
				} catch (ArgumentException ex) {
					Helper.RemoveWarning (ex);
				} catch (Exception ex) {
					Helper.RemoveWarning (ex);
					Assert.Fail ("#08 - Expected 'ArgumentException'");
				}


				Assert.AreEqual ("efgh", t.PeekChars (10), "#09");
				Assert.AreEqual ("efgh", t.ReadLine (), "#10");
				t.CommentTokens = new string [] {"'"};
				Assert.AreEqual ("afte", t.PeekChars (4), "#11");
				Assert.AreEqual ("'comment", t.ReadLine (), "#12");
				Assert.AreEqual ("af", t.PeekChars (2), "#13");
				Assert.AreEqual ("after comment", t.ReadLine (), "#14");
			}
		}
		
		[Test]
		public void DefaultPropertiesTest ()
		{
			using (StringReader reader = new StringReader (String.Empty))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				Assert.IsNotNull (t.CommentTokens, "#C1");
				Assert.AreEqual (null, Strings.Join(t.CommentTokens, ";"), "#C2");
				Assert.IsNull (t.Delimiters, "#D1");
				Assert.IsTrue (t.EndOfData, "#E1");
				Assert.AreEqual ("", t.ErrorLine, "#E2");
				Assert.AreEqual (-1, t.ErrorLineNumber, "#E3");
				Assert.IsNull (t.FieldWidths, "#F1");
				Assert.AreEqual (true, t.HasFieldsEnclosedInQuotes, "#H1");
				Assert.AreEqual (-1, t.LineNumber, "#L1");
				Assert.AreEqual (FieldType.Delimited, t.TextFieldType, "#T1");
				Assert.AreEqual (true, t.TrimWhiteSpace, "#T2");
				
			}
		}
		
		[Test]
		public void CtorTest1 ()
		{
			using (StringReader reader = new StringReader (String.Empty)) 
			using (TextFieldParser t = new TextFieldParser (reader)) 
			{
					Assert.AreEqual (string.Empty, t.ReadToEnd (), "#A1");
			}
			
			using (StringReader reader = new StringReader ("abc")) 
			using (TextFieldParser t = new TextFieldParser (reader)) 
			{
					Assert.AreEqual ("abc", t.ReadToEnd (), "#A2");
			}

			using (MemoryStream reader = new System.IO.MemoryStream (Encoding.ASCII.GetBytes("abc")))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.ASCII)) 
			{
				Assert.AreEqual ("abc", t.ReadToEnd (), "#A3");
			}

			using (MemoryStream reader = new System.IO.MemoryStream (Encoding.ASCII.GetBytes ("abc")))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.Unicode)) 
			{
				Assert.IsTrue ("abc" != t.ReadToEnd (), "#A4");
			}


			using (MemoryStream reader = new System.IO.MemoryStream (Encoding.ASCII.GetBytes ("abc")))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.Unicode, true)) 
			{
				Assert.IsTrue ("abc" != t.ReadToEnd (), "#A5");
			}

			using (MemoryStream reader = new System.IO.MemoryStream (Encoding.Unicode.GetBytes ("abc")))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.ASCII, true)) 
			{
				Assert.IsTrue ("abc" != t.ReadToEnd (), "#A6");
			}

			// Unicode string with bom
			using (MemoryStream reader = new System.IO.MemoryStream (new byte [] {0xFF, 0xFE, 0x61, 0, 0x62, 0, 0x63, 0}))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.ASCII, true)) 
			{
				Assert.AreEqual ("abc", t.ReadToEnd (), "#A7");
			}

			// UTF8 string with bom
			using (MemoryStream reader = new System.IO.MemoryStream (new byte [] { 0xEF, 0xBB, 0xBF, 0x61, 0x62, 0x63 }))
			using (TextFieldParser t = new TextFieldParser (reader, Encoding.ASCII, true)) 
			{
				Assert.AreEqual ("abc", t.ReadToEnd (), "#A8");
			}


			try {
				using (StringReader reader = new StringReader ("abc")) {
					using (TextFieldParser t = new TextFieldParser (reader)) {
						Assert.AreEqual ("abc", t.ReadToEnd (), "#A9");
					}
					reader.ReadToEnd ();
				}
				Assert.Fail ("Excepted 'ObjectDisposedException'");
			} catch (ObjectDisposedException ex) {
				Helper.RemoveWarning (ex);
			} catch (Exception ex) {
				Helper.RemoveWarning (ex);
				Assert.Fail("Excepted 'ObjectDisposedException'");
			}

			using (MemoryStream reader = new System.IO.MemoryStream (new byte [] { 0xEF, 0xBB, 0xBF, 0x61, 0x62, 0x63 })) {
				using (TextFieldParser t = new TextFieldParser (reader, Encoding.UTF8, true, true)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#A10");
				}
				reader.ReadByte ();
			}


			using (MemoryStream reader = new System.IO.MemoryStream (Encoding.UTF8.GetBytes ("abc")))
			using (TextFieldParser t = new TextFieldParser (reader)) {
				Assert.AreEqual ("abc", t.ReadToEnd (), "#A11");
			}
			
			
			string tmpfile;
			
			tmpfile = System.IO.Path.GetTempFileName ();
			try {
				Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText (tmpfile, "abc", false);

				using (TextFieldParser t = new TextFieldParser (tmpfile)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#B01");
				}
			} finally {
				System.IO.File.Delete (tmpfile);
			}
			
			tmpfile = System.IO.Path.GetTempFileName ();
			try {
				Microsoft.VisualBasic.FileIO.FileSystem.WriteAllText (tmpfile, "abc", false);

				using (TextFieldParser t = new TextFieldParser (tmpfile, Encoding.ASCII)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#B02");
				}
			} finally {
				System.IO.File.Delete (tmpfile);
			}

			tmpfile = System.IO.Path.GetTempFileName ();
			try {
				Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes (tmpfile, new byte [] {0xFF, 0xFE, 0x61, 0, 0x62, 0, 0x63, 0}, false);

				using (TextFieldParser t = new TextFieldParser (tmpfile, Encoding.Unicode)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#B03");
				}
			} finally {
				System.IO.File.Delete (tmpfile);
			}


			tmpfile = System.IO.Path.GetTempFileName ();
			try {
				Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes (tmpfile, new byte [] { 0xFF, 0xFE, 0x61, 0, 0x62, 0, 0x63, 0 }, false);

				using (TextFieldParser t = new TextFieldParser (tmpfile, Encoding.UTF8, true)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#B04");
				}
			} finally {
				System.IO.File.Delete (tmpfile);
			}


			tmpfile = System.IO.Path.GetTempFileName ();
			try {
				Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes (tmpfile, new byte [] { 0x61, 0x62, 0x63}, false);

				using (TextFieldParser t = new TextFieldParser (tmpfile, Encoding.UTF8, false)) {
					Assert.AreEqual ("abc", t.ReadToEnd (), "#B04");
				}
			} finally {
				System.IO.File.Delete (tmpfile);
			}
			
			
			
			
			
			
		}
	}
}
