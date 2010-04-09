Public Class MultDimArray1

   Public Structure Foo
     Public T As Integer
   End Structure

   Public Shared Sub Main()
     Dim b(,) As Boolean = { {True,False}, {False,False}}
     System.Console.Out.WriteLine(b(0,1).ToString())

     Dim f(2, 2) As Foo
     f(1,2).T = 5

     System.Console.WriteLine(f(1,2).T.ToString())
   End Sub

End Class