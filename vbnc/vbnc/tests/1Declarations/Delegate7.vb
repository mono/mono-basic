Interface Delegate7
    Delegate Sub DS1()

    Delegate Function DF1() As Integer
    Delegate Function DF2() As String
    Delegate Function DF3() As Delegate7
    Delegate Function DF4() As Integer()
    Delegate Function DF5() As Delegate7()
End interface
