Imports System

Public Class Main

   Public Shared Sub Main()
      Using fStream As IO.FileStream = IO.File.OpenWrite("test.txt"), _
            writer As New IO.StreamWriter(fStream)
         writer.WriteLine("hi")
      End Using
   End Sub

End Class

