using System;
using System.Text;
using NUnit.Framework;

namespace MonoTests.Microsoft_VisualBasic
{
	[TestFixture]
	public class Helper
	{
		[Test]
		public void PrintRuntimePath ()
		{
			Console.WriteLine ("\r\nUsing runtime in: " + typeof (Microsoft.VisualBasic.Strings).Assembly.Location);
		}
		
		public static bool OnMono {
			get {
				return Type.GetType ("Mono.Runtime") != null;
			}
		}
		
		public static bool OnMS {
			get {
				return !OnMono;
			}
		}

		public static T [] getObjects <T> (System.Collections.IEnumerable en)
		{
			System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T> ();
			foreach (T obj in en) {
				list.Add (obj);
			}
			return list.ToArray ();
		}
		
		public static void RemoveWarning  (object obj)
		{
		}
		
		public static string Join <T> (T [] array, string delimiter) 
		{
			if (array == null)
				return Microsoft.VisualBasic.Strings.Join (null, delimiter);
			object [] obj = new object [array.Length];
			Array.Copy (array, obj, array.Length);
			return Microsoft.VisualBasic.Strings.Join (obj, delimiter);
		}
	}
}