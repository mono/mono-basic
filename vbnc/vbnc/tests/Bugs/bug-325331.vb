Class HashDefine

    Sub Two()
#If FOO Then
        this is any old nonsense here
#Else
        Console.WriteLine("aaaa")
#End If
    End Sub

    Sub Three()
#If FOO Then
        myObj.Bar("this is any old nonsense here");
        myObj2.Foo("broken by semicolons, and question marks")?
#Else
        myObj.Foo("aaaa")
#End If
    End Sub

    Private myObj, myObj2

    Shared Sub Main()
        Console.WriteLine("before")
#If FOO Then
        Console.WriteLine("is")
#Else
        Console.WriteLine("not")
#End If
        Console.WriteLine("after")
    End Sub

End Class