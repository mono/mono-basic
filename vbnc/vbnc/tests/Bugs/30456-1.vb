Option Strict Off
Class Test
    Shared Sub Main()
        Dim myAssembly As System.Reflection.Assembly = Reflection.Assembly.LoadFile(System.Assembly.CurrentExecutingAssembly.Location)
        Dim myAssemblyMainClass As Object = myAssembly.CreateInstance("myAssembly.Main", True, Reflection.BindingFlags.CreateInstance, Nothing, Nothing, Nothing, Nothing)
        Dim tmpResult As Boolean = myAssemblyMainClass.Main()
    End Sub
End Class