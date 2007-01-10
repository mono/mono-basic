Namespace Delegate5
    'Private Delegate Sub DS1()
    Friend Delegate Sub DS2()
    'Protected Delegate Sub DS3()
    'Protected Friend Delegate Sub DS4()
    Public Delegate Sub DS5()

    'Private Delegate Function DF1() As Integer
    Friend Delegate Function DF2() As String
    'Protected Delegate Function DF3() As Delegate5_Test
    'Protected Friend Delegate Function DF4() As Integer()
    Public Delegate Function DF5() As Delegate5_Test()

    Delegate Sub D1(ByVal builtinparam As Integer)
    Delegate Sub D2(ByRef builtinbyrefparam As Integer)
    Delegate Sub D3(ByRef builtinbyrefarrayparam As Integer())
    Delegate Sub D4(ByRef builtinbyrefarrayparam2() As Integer)
    Delegate Sub D5(ByVal builtinarrayparam As Integer())
    Delegate Sub D6(ByVal builtinarrayparam2() As Integer)

    Delegate Function D7() As Integer()
    Delegate Function D8() As Delegate5_Test()

    Delegate Sub DC1(ByVal compilingclassparam As Delegate5_Test)
    Delegate Sub DC2(ByRef compilingclassbyrefparam As Delegate5_Test)
    Delegate Sub DC3(ByRef compilingclassbyrefarrayparam As Delegate5_Test())
    Delegate Sub DC4(ByRef compilingclassbyrefarrayparam2() As Delegate5_Test)
    Delegate Sub DC5(ByVal compilingclassarrayparam As Delegate5_Test())
    Delegate Sub DC6(ByVal compilingclassarrayparam2() As Delegate5_Test)

    Delegate Sub DA1(ByVal classparam As Object)
    Delegate Sub DA2(ByRef classbyrefparam As Object)
    Delegate Sub DA3(ByRef classbyrefarrayparam As Object())
    Delegate Sub DA4(ByRef classbyrefarrayparam2() As Object)
    Delegate Sub DA5(ByVal classarrayparam As Object())
    Delegate Sub DA6(ByVal classarrayparam2() As Object)

End Namespace

Public Structure Delegate5_Test
    Public value As Integer
End Structure