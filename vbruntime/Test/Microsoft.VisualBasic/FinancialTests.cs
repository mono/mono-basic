// FinancialTest.cs - NUnit Test Cases for Microsoft.VisualBasic.Financial
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
	public class FinancialTests
	{
		public FinancialTests()
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

		#region DDB Tests

        
        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void DDBArg1()
        //{
        //    // docs doesn`t say it should throw an exception on 'cost' <= 0 
        //    Financial.DDB (-1, 1, 1, 1, 1);
        //}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDBArg2()
		{
			Financial.DDB (1, -1, 1, 1, 1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDBArg3()
		{
			Financial.DDB (1, 1, 0, 1, 1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDBArg4()
		{
			Financial.DDB (1, 1, 1, 1, 0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDBArg5()
		{
			// Period has to be <= Life
			Financial.DDB (1, 1, 1, 2, 1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDBArg6()
		{
			// Period has to be > 0
			Financial.DDB (1, 1, 1, 0, 1);
		}
	
		[Test]
		public void DDB()
		{
			double ddb = Financial.DDB(1000, 50, 10, 5, 3);
			Assert.AreEqual(72.03, ddb, 1E-10);
			ddb = Financial.DDB (1000, 50, 10, 5, 2);
			Assert.AreEqual(81.92, ddb, 1E-10);
			ddb = Financial.DDB (1000, 50, 10, 1, 0.1);
			Assert.AreEqual(10, ddb, 1E-10);
			ddb = Financial.DDB (1000, 50, 10, 0.3, 0.1);
			Assert.AreEqual(10, ddb, 1E-10);
		}

		[Test]
		public void DDB_1()
		{
			Assert.AreEqual(47.999403820109578, Financial.DDB(1500, 120, 24, 12, 2.0), 1E-10);
			Assert.AreEqual(479.9940382010958, Financial.DDB(15000, 1000, 24, 12, 2.0), 1E-10);

			Assert.AreEqual(391.34749179845591, Financial.DDB(15000, 1000, 48, 12, 2.0), 1E-10);

			Assert.AreEqual(33.646996435384537, Financial.DDB(1500, 120, 24, 12, 4.0), 1E-10);

			Assert.AreEqual(43.160836714378092, Financial.DDB(1500, 100, 48, 12, 6.0), 1E-10);

			Assert.AreEqual(24.789790003786901, Financial.DDB(1500, 100, 48, 12, 1.0), 1E-10);

			Assert.AreEqual(383.10767441791506, Financial.DDB(15000, 1000, 48, 12.5, 2.0), 1E-10);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_2()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500, 120, 12, 24, 2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_3()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500, 120, 48, 24, -1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_4()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500, -2, 48, 24, 2.0);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException))]
		//LAMESPEC: MSDN claims the exception should be thrown in this case
		public void DDB_5()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(-5, 120, 48, 24, 2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_6()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500, 120, 48, -24, 2.0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void DDB_7()
		{
			// Argument 'Factor' is not a valid value.
			double d = Financial.DDB(1500, 120, -2, -5, 2.0);
		}

		#endregion

		#region SLN Tests


				
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestSLNArgs()
		{
			Financial.SLN (0, 0, 0);
		}		
		
		[Test]
		public void SLN()
		{
//			double d = Financial.SLN (0, 0, 1);
//			Assert.AreEqual( 0, d, "#SLN01");
		
			Assert.AreEqual(0.037681159420289857, Financial.SLN (45, 32, 345), 0.00002);
			Assert.AreEqual(0.657894736842105, Financial.SLN (-54, -4, -76), 0.00002);
		}
        	

		[Test]
		public void SLN_1()
		{
			Assert.AreEqual(20.833333333333332, Financial.SLN(1500, 500, 48));
			Assert.AreEqual(10.416666666666666, Financial.SLN(1500, 500, 96));

			Assert.AreEqual(0, Financial.SLN(500, 500, 96));

			Assert.AreEqual(500, Financial.SLN(1500, 500, 2));
			Assert.AreEqual(-500, Financial.SLN(1500, 500, -2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SLN_2()
		{
			//Argument 'Life' cannot be zero.
			Financial.SLN(1500, 500, 0);
		}

		#endregion

		#region SYD Tests


		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYDArgs1()
		{
			Financial.SYD (1, 1, 1, -1);
		}	
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYDArgs2()
		{
			Financial.SYD (1, -1, 1, 1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYDArgs3()
		{
			Financial.SYD (1, 1, 1, 2);
		}
		
		[Test]
		public void SYD()
		{
			double d = Financial.SYD (23, 34, 26, 21);
			Assert.AreEqual( -0.188034188034188, d, 0.0001, "#SYD01");

			d = Financial.SYD (0, 1, 1, 1);
			Assert.AreEqual( -1, d, 0.00001, "#SYD02");
		}
		
		[Test]
		public void SYD_1()
		{
			Assert.AreEqual (44.047619047619044, Financial.SYD (1500, 100, 48, 12), 1E-10);
			
			Assert.AreEqual (1.1904761904761905, Financial.SYD (1500, 100, 48, 48), 1E-10);

			Assert.AreEqual(0, Financial.SYD(100, 100, 48, 48));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_2()
		{
			// Argument 'Salvage' must be greater than or equal to zero.
			Financial.SYD(1500, -100, 48, 12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_3()
		{
			// Argument 'Period' must be less than or equal to argument 'Life'.
			Financial.SYD(1500, 100, 8, 12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_4()
		{
			// Argument 'Period' must be greater than zero.
			Financial.SYD(1500, 100, 48, 0);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void SYD_5()
		{
			// Argument 'Period' must be greater than zero.
			Financial.SYD(1500, 100, 48, -2);
		}

		#endregion

		#region FV Tests

		[Test]
		public void FV()
		{
			double d = Financial.FV (10, 5, 3, 7, DueDate.BegOfPeriod);
			Assert.AreEqual (-1658822, d, "#FV01");
			
			d = Financial.FV (10, 5, 3, 7, DueDate.EndOfPeriod);
			Assert.AreEqual (-1175672, d, "#FV02");
			
			d = Financial.FV (0, 5, 3, 7, DueDate.BegOfPeriod);
			Assert.AreEqual (-22, d, "#FV03");
			
			d = Financial.FV(0, 1, 1, 1, DueDate.BegOfPeriod);
			Assert.AreEqual (-2, d, "#FV04");
			
			d = Financial.FV (0, 0, 0, 0, DueDate.BegOfPeriod);
			Assert.AreEqual (0, d, "#FV05");
			
			d = Financial.FV (-3, -5, -6, -4, DueDate.BegOfPeriod);
			Assert.AreEqual (-4.25, d, "#FV06");
		}

		[Test]
		public void FV_1()
		{
			Assert.AreEqual(-5042.6861628644065, Financial.FV (0.1 / 48, 48, 100, 0, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(5042.6861628644065, Financial.FV(0.1/48, 48, -100, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(6026.9653563801103, Financial.FV(0.45/48, 48, -100, 0, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(30134.826781900552, Financial.FV(0.45/48, 48, -500, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(5053.1917590370413, Financial.FV(0.1/48, 48, -100, 0, DueDate.BegOfPeriod), 1E-8);


			Assert.AreEqual(1727.5182776853812, Financial.FV(0.1/48, 48, -100, 3000, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(8357.8540480434312, Financial.FV(0.1/48, 48, -100, -3000, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(1738.023873858016, Financial.FV(0.1/48, 48, -100, 3000, DueDate.BegOfPeriod), 1E-8);
			Assert.AreEqual(8368.359644216067, Financial.FV(0.1/48, 48, -100, -3000, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-4572.3341785092407, Financial.FV(-0.1/48, 48, 100, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-4599.4962842992118, Financial.FV(-0.1/48, 48.3, 100, 0, DueDate.EndOfPeriod), 1E-8);
		}


		#endregion

		#region Rate Tests

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void RateArgs1()
		{
			double d = Financial.Rate (-1, 1, 1, 1, DueDate.BegOfPeriod, 1);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		public void Rate()
		{
			
			Assert.AreEqual(-1.5000000000001, Financial.Rate (1, 1, 1, 1, DueDate.BegOfPeriod, 0.1), 0.1);
			Assert.AreEqual(-1.5000000000001, Financial.Rate (1, -1, -1, -1, DueDate.BegOfPeriod, 0.1), 0.1);
			Assert.AreEqual(-1.71428571428571, Financial.Rate (1, 2, 12, 10, DueDate.BegOfPeriod, 0.5), 0.1);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		public void Rate_1()
		{
			Assert.AreEqual(-0.067958335561249847, Financial.Rate(48, -120, 50000, 0, DueDate.EndOfPeriod, 0.1));

			Assert.AreEqual(-0.054284323350630818, Financial.Rate(48, -200, 50000, 0, DueDate.EndOfPeriod, 0.1));

			Assert.AreEqual(-0.03391640485393424, Financial.Rate(48, -400, 50000, 0, DueDate.EndOfPeriod, 0.1));

			Assert.AreEqual(0.19996831303445506, Financial.Rate(48, -1000, 5000, 0, DueDate.EndOfPeriod, 0.1));

			Assert.AreEqual(-0.99999999998846933, Financial.Rate(48, -1000, 5000, 0, DueDate.BegOfPeriod, 0.1));

			Assert.AreEqual(-0.055871364867281934, Financial.Rate(48, -200, 50000, 0, DueDate.BegOfPeriod, 0.1));

			Assert.AreEqual(-0.065503055347169575, Financial.Rate(48, -200, 50000, 1000, DueDate.EndOfPeriod, 0.1));

			Assert.AreEqual(-0.058920469572311909, Financial.Rate(48, -200, 50000, 500, DueDate.EndOfPeriod, 0.1));
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		public void Rate_2()
		{
			Assert.AreEqual(-0.067958335561434935, Financial.Rate(48, -120, 50000, 0, DueDate.EndOfPeriod, 0.3));

			Assert.AreEqual(-0.054284323350996831, Financial.Rate(48, -200, 50000, 0, DueDate.EndOfPeriod, 0.3));

			Assert.AreEqual(-0.033916404853936467, Financial.Rate(48, -400, 50000, 0, DueDate.EndOfPeriod, 0.3));

			Assert.AreEqual(0.19996831303445506, Financial.Rate(48, -1000, 5000, 0, DueDate.EndOfPeriod, 0.2));

			Assert.AreEqual(-0.99999999999999079, Financial.Rate(48, -1000, 5000, 0, DueDate.BegOfPeriod, 0.3));

			Assert.AreEqual(-0.055871364867277139, Financial.Rate(48, -200, 50000, 0, DueDate.BegOfPeriod, 0.3));

			Assert.AreEqual(-0.065503055348340639, Financial.Rate(48, -200, 50000, 1000, DueDate.EndOfPeriod, 0.3));

			Assert.AreEqual(-0.058920469572100169, Financial.Rate(48, -200, 50000, 500, DueDate.EndOfPeriod, 0.3));
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_3()
		{
			// Argument 'NPer' must be greater than zero.
			Financial.Rate(0, -120, 50000, 0, DueDate.EndOfPeriod, 0.1);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_4()
		{
			// Argument 'NPer' must be greater than zero.
			Financial.Rate(-10, -120, 50000, 0, DueDate.EndOfPeriod, 0.1);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Rate_5()
		{
			// Cannot calculate rate using the arguments provided.
			Financial.Rate(48, -1000, 5000, 0, DueDate.EndOfPeriod, 0.3);
		}

		#endregion

		#region IRR Tests

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRRArgs1()
		{
			double [] arr = new double [0];
			Financial.IRR (ref arr, 0.1);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRRArgs2()
		{
			double [] arr = new double [] {134};
			Financial.IRR (ref arr, 0.1);
		}
		
//		[Test]
//		[ExpectedException(typeof(ArgumentException))]
//		public void IRRArgs3()
//		{
//			// -0.99 as Guess throws an exception on MS.NET, -0.98 doesn't
//			//double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
//			//double d = Financial.IRR (ref arr, -0.99);
//		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		public void IRR()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.IRR (ref arr, 0.1);
			Assert.AreEqual ( 0.177435884422527, d, 0.00001);
		}
		
		[Category ("NotWorking")]//Not Implemented
		[Test]
		public void IRR_1 ()
		{
			double [] values = new double [] { -50000, 20000, 20000, 20000, 10000 };

			Assert.AreEqual (0.16479098450887533, Financial.IRR (ref values, 0.1));

			Assert.AreEqual (0.16479098450893837, Financial.IRR (ref values, 0.3));

			Assert.AreEqual (0.16479098450893415, Financial.IRR (ref values, 0.5));

			values = new double [] { -100000, 40000, 35000, 30000, 25000 };

			Assert.AreEqual (0.12441449540624081, Financial.IRR (ref values, 0.1));

			Assert.AreEqual (0.12441449541502105, Financial.IRR (ref values, 0.3));

			Assert.AreEqual (0.12441449541025705, Financial.IRR (ref values, 0.5));
		}
		
		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRR_2()
		{
			//Arguments are not valid.
			double[] values = new double[] {-500000, 20, 20, 20, 10000};

			Financial.IRR(ref values, 10000);
		}

		[Category ("NotWorking")]//Not Implemented
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IRR_3()
		{
			//Arguments are not valid.
			double[] values = new double[] {100, 20, 30, 30, 30};

			Financial.IRR(ref values, 0.1);
		}

		#endregion

		#region MIRR Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRRArgs1()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.MIRR(ref arr, -1, 1);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRRArgs2()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.MIRR(ref arr, 1, -1);
		}
		
		[Test]
		public void MIRR()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.MIRR (ref arr, 1, 1);
			Assert.AreEqual(0.509044845533018, d, 0.00001, "#MIRR01");
			
			arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			d = Financial.MIRR (ref arr, 5, 5);
			Assert.AreEqual(2.02366041666348, d, 0.00001, "#MIRR02");
		}

		[Test]
		public void MIRR_1()
		{
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Assert.AreEqual(0.14382317535283296, Financial.MIRR(ref values, 0.1, 0.12), 1E-10);

			Assert.AreEqual(0.32152371134887248, Financial.MIRR(ref values, 0.3, 0.5), 1E-10);

			Assert.AreEqual(0.41431961645993387, Financial.MIRR(ref values, 0.5, 0.7), 1E-10);

			values = new double[] {-100000, 40000, 35000, 30000, 25000};

			Assert.AreEqual(0.12239312521886214, Financial.MIRR(ref values, 0.1, 0.12), 1E-10);

			Assert.AreEqual(0.29787828889780776, Financial.MIRR(ref values, 0.3, 0.5), 1E-10);

			Assert.AreEqual(0.39034333083777972, Financial.MIRR(ref values, 0.5, 0.7), 1E-10);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_2()
		{
			// Argument 'FinanceRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values, -1, 0.12);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_3()
		{
			// Argument 'ReinvestRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values, 0.1, -1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void MIRR_4()
		{
			// Argument 'ReinvestRate' is not a valid value.
			double[] values = new double[] {-50000, 20000, 20000, 20000, 10000};

			Financial.MIRR(ref values, 0.1, -3);
		}

		[Test]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void MIRR_5()
		{
			double[] values = null;

			Financial.MIRR(ref values, 0.1, 0.12);
		}

		[Category ("NotWorking")]
		[Category ("TargetJvmNotWorking")]
		[Test]
		[ExpectedException(typeof(DivideByZeroException))]
		public void MIRR_6()
		{
			double[] values = new double[] {100, 20, 30, 30, 30};

			Financial.MIRR(ref values, 0.1, 0.1);
		}

		#endregion

		#region NPer Tests

		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPerArgs1()
		{
			double d = Financial.NPer (-1, 2, 2, 2, DueDate.BegOfPeriod);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPerArgs2()
		{
			double d = Financial.NPer (0, 0, 2, 2, DueDate.BegOfPeriod);
		}
		
		[Test]
		public void NPer()
		{
			double d = Financial.NPer (3, 4, 6, 2, DueDate.BegOfPeriod);
			Assert.AreEqual(-0.882767373181489, d, 0.0000001,  "#NPer01");
			
			d = Financial.NPer (1, -4, -6, -2, DueDate.EndOfPeriod);
			Assert.AreEqual(-2.32192809488736, d, 0.0000001, "#NPer02");
		}
		
		[Test]
		public void NPer_1()
		{
			Assert.AreEqual(-47.613161535165531, (Financial.NPer(0.1/48, 200, 10000, 0, DueDate.EndOfPeriod)), 0.00000001);
			Assert.AreEqual(-47.518910769564712, Financial.NPer(0.1/48, 200, 10000, 0, DueDate.BegOfPeriod), 0.00000001);

			Assert.AreEqual(-401.12014892843246, Financial.NPer(0.1/48, 200, 10000, 50000, DueDate.EndOfPeriod), 0.0000001);
			Assert.AreEqual(-399.9412969471162, Financial.NPer(0.1/48, 200, 10000, 50000, DueDate.BegOfPeriod), 0.0000001);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_2()
		{
			// Argument 'Rate' is not a valid value.
			Financial.NPer(-1, 200, 10000, 0, DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_3()
		{
			// Argument 'Pmt' is not a valid value.
			Financial.NPer(0, 0, 10000, 0, DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPer_4()
		{
			// Cannot calculate number of periods using the arguments provided.
			Financial.NPer(0.1/48, 0, 10000, 0, DueDate.EndOfPeriod);
		}

		#endregion

		#region IPmt Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmtArgs1()
		{
			Financial.IPmt (3, 6, 4, 2, 2, DueDate.BegOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmtArgs2()
		{
			Financial.IPmt (3, 0, 4, 2, 2, DueDate.BegOfPeriod);
		}

		[Test]
		public void IPmt()
		{
			double d = Financial.IPmt (10, 2, 3, 7, 9, DueDate.BegOfPeriod);
			Assert.AreEqual ( -6.25427204374573, d, 1E-10, "#IPmt01");
			
			d = Financial.IPmt (10, 4, 4, 7, 4, DueDate.EndOfPeriod);
			Assert.AreEqual ( -60.0068306011053, d, 1E-10 , "#IPmt02");
			
			d = Financial.IPmt (0, 5, 7, 7, 2, DueDate.BegOfPeriod);
			Assert.AreEqual( 0, d, "#IPmt03");
			
			d = Financial.IPmt (1000, 1, 7, 7, 2, DueDate.BegOfPeriod);
			Assert.AreEqual( 0, d, "#IPmt03");
			
			d = Financial.IPmt (-5, 5, 7, -7, -2, DueDate.BegOfPeriod);
			Assert.AreEqual(8.92508391821792, d, 1E-10, "#IPmt04");

		}

		[Test]
		public void IPmt_1()
		{
			Assert.AreEqual(-29.246960945707116, Financial.IPmt(0.1/48, 36, 48, 50000, 0, DueDate.EndOfPeriod), 1E-10);

			Assert.AreEqual(-55.550772957533624, Financial.IPmt(0.1/48, 24, 48, 50000, 0, DueDate.EndOfPeriod), 1E-10);

			Assert.AreEqual(-54.4678461379274, Financial.IPmt(0.1/48, 24.5, 48, 50000, 0, DueDate.EndOfPeriod), 1E-10);

			Assert.AreEqual(-2.2779661153231245, Financial.IPmt(0.1/48, 48, 48, 50000, 0, DueDate.EndOfPeriod), 1E-10);

			Assert.AreEqual(42.6652760279442, Financial.IPmt(0.1/48, 24.3, 48.3, 50000, 100000, DueDate.EndOfPeriod), 1E-9);

			Assert.AreEqual(-29.186156453096753, Financial.IPmt(0.1/48, 36, 48, 50000, 0, DueDate.BegOfPeriod), 1E-10);

			Assert.AreEqual(-55.435282785064992, Financial.IPmt(0.1/48, 24, 48, 50000, 0, DueDate.BegOfPeriod), 1E-10);

			Assert.AreEqual(-54.3546073725681, Financial.IPmt(0.1/48, 24.5, 48, 50000, 0, DueDate.BegOfPeriod), 1E-10);

			Assert.AreEqual(-2.2732302190335001, Financial.IPmt(0.1/48, 48, 48, 50000, 0, DueDate.BegOfPeriod), 1E-10);

			Assert.AreEqual(201.49943498736394, Financial.IPmt(0.1/48, 48, 48, 50000, 100000, DueDate.EndOfPeriod), 1E-9);

			Assert.AreEqual(201.08051724310744, Financial.IPmt(0.1/48, 48, 48, 50000, 100000, DueDate.BegOfPeriod), 1E-9);

			Assert.AreEqual(42.5765748303808, Financial.IPmt(0.1/48, 24.3, 48.3, 50000, 100000, DueDate.BegOfPeriod), 1E-9);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmt_2()
		{
			// Argument 'Per' is not a valid value.
			Financial.IPmt(0.1/48, 56, 48, 50000, 0, DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IPmt_3()
		{
			// Argument 'Per' is not a valid value.
			Financial.IPmt(0.1/48, -48, -48, 50000, 0, DueDate.EndOfPeriod);
		}

		#endregion

		#region Pmt Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PmtArgs()
		{
			Financial.Pmt (1, 0, 1, 1, DueDate.BegOfPeriod);
		}

		[Test]
		public void Pmt()
		{
			double d = Financial.Pmt (2, 5, 2, 3, DueDate.BegOfPeriod);
			Assert.AreEqual (-1.3471074380165289, d, 1E-10, "#Pmt01");
			
			d = Financial.Pmt (2, 5, 2, 3, DueDate.EndOfPeriod);
			Assert.AreEqual (-4.0413223140495864, d, 1E-10, "#Pmt02");
			
			d = Financial.Pmt (-3, -5, -3, -4, DueDate.BegOfPeriod);
			Assert.AreEqual (-5.6818181818181817, d, 1E-10, "#Pmt03");
			
			d = Financial.Pmt (-3, -5, -3, -4, DueDate.EndOfPeriod);
			Assert.AreEqual (11.363636363636363, d, 1E-10, "#Pmt04");
			
			d = Financial.Pmt (0, 1, 0, 0, DueDate.BegOfPeriod);
			Assert.AreEqual ( 0, d, 1E-10, "#Pmt05");
			
			d = Financial.Pmt (0, 1, 0, 0, DueDate.EndOfPeriod);
			Assert.AreEqual ( 0, d, 1E-10, "#Pmt06");
		}

		[Test]
		public void Pmt_1()
		{
			Assert.AreEqual (-1095.7017014703874, Financial.Pmt (0.1 / 48, 48, 50000, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-1093.4237353550641, Financial.Pmt(0.1/48, 48, 50000, 0, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-3072.37099816498, Financial.Pmt(0.1/48, 48, 50000, 100000, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-3027.53768194438, Financial.Pmt(0.1/48, 48.7, 50000, 100000, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(3182.938437744494, Financial.Pmt(0.1/48, -48, 50000, 100000, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(3138.01171878177, Financial.Pmt(0.1/48, -48.7, 50000, 100000, DueDate.EndOfPeriod), 1E-8);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Pmt_2()
		{
			// Argument 'NPer' is not a valid value.
			Financial.Pmt(0.1/48, 0, 50000, 0, DueDate.EndOfPeriod);
		}

		#endregion

		#region PPmt Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmtArgs1()
		{
			double d = Financial.PPmt (2, -1, 1, 1, 1, DueDate.EndOfPeriod);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmtArgs2()
		{
			double d = Financial.PPmt (1, 2, 1, 1, 1, DueDate.BegOfPeriod);
		}

		[Test]
		public void PPmt()
		{
			double d = Financial.PPmt (10, 2, 3, 7, 9, DueDate.BegOfPeriod);
			Assert.AreEqual(-0.120300751879702, d, 1E-10, "#PPmt01");
			
			d = Financial.PPmt (10, 4, 4, 7, 4, DueDate.EndOfPeriod);
			Assert.AreEqual(-10.0006830600969, d, 1E-10, "#PPmt02");
			
			d = Financial.PPmt (0, 5, 7, 7, 2, DueDate.BegOfPeriod);
			Assert.AreEqual(-1.28571428571429, d, 1E-10, "#PPmt03");
			
			d = Financial.PPmt (-5, 5, 7, -7, -2, DueDate.BegOfPeriod);
			Assert.AreEqual( -0.175770521818777, d, 1E-10, "#PPmt04");
		}

		[Test]
		public void PPmt_1()
		{
			Assert.AreEqual(-1066.4547405246804, Financial.PPmt(0.1/48, 36, 48, 50000, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-1040.1509285128539, Financial.PPmt(0.1/48, 24, 48, 50000, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-1093.4237353550643, Financial.PPmt(0.1/48, 48, 48, 50000, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-1064.2375789019673, Financial.PPmt(0.1/48, 36, 48, 50000, 0, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-1037.988452569999, Financial.PPmt(0.1/48, 24, 48, 50000, 0, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-1091.1505051360307, Financial.PPmt(0.1/48, 48, 48, 50000, 0, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-3280.2712060651929, Financial.PPmt(0.1/48, 48, 48, 50000, 100000, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-3273.4515154080918, Financial.PPmt(0.1/48, 48, 48, 50000, 100000, DueDate.BegOfPeriod), 1E-8);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmt_2()
		{
			// Argument 'Per' is not a valid value.
			Financial.PPmt(0.1/48, 56, 48, 50000, 0, DueDate.EndOfPeriod);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PPmt_3()
		{
			// Argument 'Per' is not a valid value.
			Financial.PPmt(0.1/48, -48, -48, 50000, 0, DueDate.EndOfPeriod);
		}

		#endregion

		#region NPV Tests

		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPVArgs1()
		{
			double [] arr = null;
			double d = Financial.NPV (0.0625, ref arr);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPVArgs2()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.NPV (-1, ref arr);
		}
			
		[Test]
		public void NPV()
		{
			double [] arr = new double [] {-70000, 22000, 25000, 28000, 31000};
			double d = Financial.NPV (0.0625, ref arr);	
			Assert.AreEqual(19312.5702095352, d, 0.000001);
		}

		[Test]
		public void NPV_1()
		{
			double[] values = new double[] {10000, 20000, 30000, 20000, 10000};

			Assert.AreEqual(68028.761075684073, Financial.NPV(0.1, ref values), 1E-7);

			Assert.AreEqual(42877.457964464716, Financial.NPV(0.3, ref values), 1E-7);

			Assert.AreEqual(22007.920515939288, Financial.NPV(0.7, ref values), 1E-7);

			Assert.AreEqual(15312.5, Financial.NPV(1, ref values), 1E-7);

			Assert.AreEqual(5895.8421600621214, Financial.NPV(2.3, ref values), 1E-7);

			Assert.AreEqual(-5203.7070453792549, Financial.NPV(-2.3, ref values), 1E-7);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPV_2()
		{
			//Argument 'Rate' is not a valid value.
			double[] values = new double[] {10000, 20000, 30000, 20000, 10000};

			Financial.NPV(-1, ref values);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NPV_3()
		{
			// Argument 'ValueArray' is Nothing.
			double[] values = null;

			Financial.NPV(0.1, ref values);
		}

		#endregion

		#region PV Tests

		[Test]
		public void TestPV()
		{
			double d = Financial.PV (1, 1, 1, 1, DueDate.BegOfPeriod);
			Assert.AreEqual (-1.5, d, "#PV01");
			
			d = Financial.PV (1, 1, 1, 1, DueDate.EndOfPeriod);
			Assert.AreEqual (-1, d, "#PV02");
		}

		[Test]
		public void PV_1()
		{
			Assert.AreEqual(-4563.2857859855494, Financial.PV(0.1/48, 48, 100, 0, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(4563.2857859855494, Financial.PV(0.1/48, 48, -100, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(3851.0271688809689, Financial.PV(0.45/48, 48, -100, 0, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(19255.135844404842, Financial.PV(0.45/48, 48, -500, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(4572.7926313730195, Financial.PV(0.1/48, 48, -100, 0, DueDate.BegOfPeriod), 1E-8);


			Assert.AreEqual(1848.4911476096459, Financial.PV(0.1/48, 48, -100, 3000, DueDate.EndOfPeriod), 1E-8);
			Assert.AreEqual(7278.0804243614521, Financial.PV(0.1/48, 48, -100, -3000, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(1857.9979929971164, Financial.PV(0.1/48, 48, -100, 3000, DueDate.BegOfPeriod), 1E-8);
			Assert.AreEqual(7287.5872697489231, Financial.PV(0.1/48, 48, -100, -3000, DueDate.BegOfPeriod), 1E-8);

			Assert.AreEqual(-5053.7378976476075, Financial.PV(-0.1/48, 48, 100, 0, DueDate.EndOfPeriod), 1E-8);

			Assert.AreEqual(-5086.9414579281301, Financial.PV(-0.1/48, 48.3, 100, 0, DueDate.EndOfPeriod), 1E-8);
		}

		#endregion
	}
}
