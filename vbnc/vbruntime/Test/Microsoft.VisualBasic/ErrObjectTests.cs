// ErrObjectTests.cs - NUnit Test Cases for Microsoft.VisualBasic.ErrObject 
//
// Mizrahi Rafael (rafim@mainsoft.com)
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
	public class ErrObjectTests
	{
		public ErrObjectTests()
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
	
		#region Raise tests

		[Test]
		[ExpectedException (typeof(Exception))]
		public void Raise1()
		{
			//'o1 = new ArgumentOutOfRangeException();
			ErrObject err;
			err = Information.Err();
			err.Raise(1,"source","description","",0);
		}

		[Test]
		[ExpectedException (typeof(Exception))]
		public void Raise2()
		{
			ErrObject err;
			err = Information.Err();
			err.Raise(2,"source","description","",0);
		}

		[Test]
		public void Raise3()
		{
			ErrObject err;
			err = Information.Err();

			try{
				err.Raise(3,"source","description","",0);
			}catch (InvalidOperationException ex){
				Assert.AreEqual(3,err.Number);  
			}
			
			try{
				err.Raise(20,"source","description","",0);
			}catch (InvalidOperationException ex){
				Assert.AreEqual(20,err.Number);  
			}

			try{
				err.Raise(94,"source","description","",0);
			}catch (InvalidOperationException ex){
				Assert.AreEqual(94,err.Number);  
			}

			try{
				err.Raise(14,"source","description","",0);
			}catch (OutOfMemoryException ex){
				Assert.AreEqual(14,err.Number);  
			}

			try{
				err.Raise(5,"source","description","",0);
			}catch (ArgumentException ex){
				Assert.AreEqual(5,err.Number);  
			}

			try{
				err.Raise(6,"source","description","",0);
			}catch (OverflowException ex){
				Assert.AreEqual(6,err.Number);  
			}

			try{
				err.Raise(7,"source","description","",0);
			}catch (OutOfMemoryException ex){
				Assert.AreEqual("System.OutOfMemoryException",ex.GetType().FullName);  
				Assert.AreEqual(7,err.Number);  
			}

			try{
				err.Raise(9,"source","description","",0);
			}catch (IndexOutOfRangeException ex){
				Assert.AreEqual("System.IndexOutOfRangeException",ex.GetType().FullName);  
				Assert.AreEqual(9,err.Number);  
			}
		
			try{
				err.Raise(11,"source","description","",0);
			}catch (DivideByZeroException ex){
				Assert.AreEqual("System.DivideByZeroException",ex.GetType().FullName);  
				Assert.AreEqual(11,err.Number);  
			}

			try{
				err.Raise(13,"source","description","",0);
			}catch (InvalidCastException ex){
				Assert.AreEqual("System.InvalidCastException",ex.GetType().FullName);  
				Assert.AreEqual(13,err.Number);  
			}	
	 
			try{
				err.Raise(28,"source","description","",0);
			}catch (StackOverflowException ex){
				Assert.AreEqual("System.StackOverflowException",ex.GetType().FullName);  
				Assert.AreEqual(28,err.Number);  
			}		
		
			try{
				err.Raise(48,"source","description","",0);
			}catch (TypeLoadException ex){
				Assert.AreEqual("System.TypeLoadException",ex.GetType().FullName);  
				Assert.AreEqual(48,err.Number);  
			}	

			try{
				err.Raise(52,"source","description","",0);
			}catch (IOException ex){
				Assert.AreEqual("System.IO.IOException",ex.GetType().FullName);  
				Assert.AreEqual(52,err.Number);  
			}
	
			try{
				err.Raise(53,"source","description","",0);
			}catch (System.IO.FileNotFoundException ex){
				Assert.AreEqual("System.IO.FileNotFoundException",ex.GetType().FullName);  
				Assert.AreEqual(53,err.Number);  
			}	
	
			try{
				err.Raise(54,"source","description","",0);
			}catch (System.IO.IOException ex){
				Assert.AreEqual("System.IO.IOException",ex.GetType().FullName);  
				Assert.AreEqual(54,err.Number);  
			}	
	
			try{
				err.Raise(55,"source","description","",0);
			}catch (System.IO.IOException ex){
				Assert.AreEqual("System.IO.IOException",ex.GetType().FullName);  
				Assert.AreEqual(55,err.Number);  
			}	
			
			try{
				err.Raise(57,"source","description","",0);
			}catch (System.IO.IOException ex){
				Assert.AreEqual("System.IO.IOException",ex.GetType().FullName);  
				Assert.AreEqual(57,err.Number);  
			}	

		}

		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void Raise4()
		{
			ErrObject err;
			err = Information.Err();
			err.Raise(65536,"source","description","",0);
		}
		
		[Test]
		[ExpectedException (typeof(ArgumentException))]
		public void Raise5()
		{
			ErrObject err;
			err = Information.Err();
			err.Raise(0,"source","description","",0);
		}

		#endregion

		
	}
}

