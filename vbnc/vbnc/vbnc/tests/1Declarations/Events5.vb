Namespace Events5
    Class testclass1
        Event a()
        Event b(ByVal builtinparam As Integer)
        Event c(ByRef builtinbyrefparam As Integer)
        Event d(ByVal builtinarrayparam() As Integer)
        Event e(ByVal builtinarrayparam As Integer())
        Event f(ByRef builtinbyrefarrayparam() As Integer)
        Event g(ByRef builtinbyrefarrayparam As Integer())

        Event h(ByVal objparam As Object)
        Event i(ByRef objbyrefparam As Object)
        Event j(ByVal objarrayparam() As Object)
        Event k(ByVal objarrayparam As Object())
        Event l(ByRef objbyrefarrayparam() As Object)
        Event m(ByRef objbyrefarrayparam As Object())

        Event n(ByVal classparam As testclass1)
        Event o(ByRef classbyrefparam As testclass1)
        Event p(ByVal classarrayparam() As testclass1)
        Event q(ByVal classarrayparam As testclass1())
        Event r(ByRef classbyrefarrayparam() As testclass1)
        Event s(ByRef classbyrefarrayparam As testclass1())

    End Class
    Class testclass2
        Shared Event a()
        Shared Event b(ByVal builtinparam As Integer)
        Shared Event c(ByRef builtinbyrefparam As Integer)
        Shared Event d(ByVal builtinarrayparam() As Integer)
        Shared Event e(ByVal builtinarrayparam As Integer())
        Shared Event f(ByRef builtinbyrefarrayparam() As Integer)
        Shared Event g(ByRef builtinbyrefarrayparam As Integer())

        Shared Event h(ByVal objparam As Object)
        Shared Event i(ByRef objbyrefparam As Object)
        Shared Event j(ByVal objarrayparam() As Object)
        Shared Event k(ByVal objarrayparam As Object())
        Shared Event l(ByRef objbyrefarrayparam() As Object)
        Shared Event m(ByRef objbyrefarrayparam As Object())

        Shared Event n(ByVal classparam As testclass1)
        Shared Event o(ByRef classbyrefparam As testclass1)
        Shared Event p(ByVal classarrayparam() As testclass1)
        Shared Event q(ByVal classarrayparam As testclass1())
        Shared Event r(ByRef classbyrefarrayparam() As testclass1)
        Shared Event s(ByRef classbyrefarrayparam As testclass1())

    End Class
    Class testclass3
        Event a As testdelegate
        Shared Event b as testdelegate

        Delegate Sub testdelegate()
    End Class
    Class testclass4
        Private Shared Event a()
        Friend Shared Event b(ByVal builtinparam As Integer)
        Public Shared Event c(ByRef builtinbyrefparam As Integer)
        Protected Shared Event d(ByVal builtinarrayparam() As Integer)
        Protected Friend Shared Event e(ByVal builtinarrayparam As Integer())
        
        Private Event h(ByVal objparam As Object)
        Friend Event i(ByRef objbyrefparam As Object)
        Public Event j(ByVal objarrayparam() As Object)
        Protected Event k(ByVal objarrayparam As Object())
        Protected Friend Event l(ByRef objbyrefarrayparam() As Object)
        
    End Class
    Class testclass5
        Private Shared Event a As testdelegate
        Friend Shared Event b As testdelegate
        Public Shared Event c As testdelegate
        Protected Shared Event d As testdelegate
        Protected Friend Shared Event e As testdelegate

        Private Event h As testdelegate
        Friend Event i As testdelegate
        Public Event j As testdelegate
        Protected Event k As testdelegate
        Protected Friend Event l As testdelegate

        Delegate Sub testdelegate()
    End Class

    Structure teststructure1
        Event a1()
        Event b1(ByVal builtinparam As Integer)
        Event c1(ByRef builtinbyrefparam As Integer)
        Event d1(ByVal builtinarrayparam() As Integer)
        Event e1(ByVal builtinarrayparam As Integer())
        Event f1(ByRef builtinbyrefarrayparam() As Integer)
        Event g1(ByRef builtinbyrefarrayparam As Integer())

        Event h1(ByVal objparam As Object)
        Event i1(ByRef objbyrefparam As Object)
        Event j1(ByVal objarrayparam() As Object)
        Event k1(ByVal objarrayparam As Object())
        Event l1(ByRef objbyrefarrayparam() As Object)
        Event m1(ByRef objbyrefarrayparam As Object())

        Event n1(ByVal classparam As testclass1)
        Event o1(ByRef classbyrefparam As testclass1)
        Event p1(ByVal classarrayparam() As testclass1)
        Event q1(ByVal classarrayparam As testclass1())
        Event r1(ByRef classbyrefarrayparam() As testclass1)
        Event s1(ByRef classbyrefarrayparam As testclass1())

    End Structure
    Structure teststructure2
        Dim value As String
        Shared Event a()
        Shared Event b(ByVal builtinparam As Integer)
        Shared Event c(ByRef builtinbyrefparam As Integer)
        Shared Event d(ByVal builtinarrayparam() As Integer)
        Shared Event e(ByVal builtinarrayparam As Integer())
        Shared Event f(ByRef builtinbyrefarrayparam() As Integer)
        Shared Event g(ByRef builtinbyrefarrayparam As Integer())

        Shared Event h(ByVal objparam As Object)
        Shared Event i(ByRef objbyrefparam As Object)
        Shared Event j(ByVal objarrayparam() As Object)
        Shared Event k(ByVal objarrayparam As Object())
        Shared Event l(ByRef objbyrefarrayparam() As Object)
        Shared Event m(ByRef objbyrefarrayparam As Object())

        Shared Event n(ByVal classparam As testclass1)
        Shared Event o(ByRef classbyrefparam As testclass1)
        Shared Event p(ByVal classarrayparam() As testclass1)
        Shared Event q(ByVal classarrayparam As testclass1())
        Shared Event r(ByRef classbyrefarrayparam() As testclass1)
        Shared Event s(ByRef classbyrefarrayparam As testclass1())

    End Structure
    Structure teststructure3
        Event a As testdelegate1
        Shared Event b As testdelegate1

        Delegate Sub testdelegate1()
    End Structure
    Structure teststructure4
        Private Shared Event a3()
        Friend Shared Event b3(ByVal builtinparam As Integer)
        Public Shared Event c3(ByRef builtinbyrefparam As Integer)
        'Protected Shared Event d(ByVal builtinarrayparam() As Integer)
        'Protected Friend Shared Event e(ByVal builtinarrayparam As Integer())

        Private Event h3(ByVal objparam As Object)
        Friend Event i3(ByRef objbyrefparam As Object)
        Public Event j3(ByVal objarrayparam() As Object)
        'Protected Event k(ByVal objarrayparam As Object())
        'Protected Friend Event l(ByRef objbyrefarrayparam() As Object)

    End Structure
    Structure teststructure5
        Private Shared Event a2 As testdelegate2
        Friend Shared Event b2 As testdelegate2
        Public Shared Event c2 As testdelegate2
        'Protected Shared Event d As testdelegate
        'Protected Friend Shared Event e As testdelegate

        Private Event h2 As testdelegate2
        Friend Event i2 As testdelegate2
        Public Event j2 As testdelegate2
        'Protected Event k As testdelegate
        'Protected Friend Event l As testdelegate

        Delegate Sub testdelegate2()
    End Structure

    Module testmodule1
        Event a()
        Event b(ByVal builtinparam As Integer)
        Event c(ByRef builtinbyrefparam As Integer)
        Event d(ByVal builtinarrayparam() As Integer)
        Event e(ByVal builtinarrayparam As Integer())
        Event f(ByRef builtinbyrefarrayparam() As Integer)
        Event g(ByRef builtinbyrefarrayparam As Integer())

        Event h(ByVal objparam As Object)
        Event i(ByRef objbyrefparam As Object)
        Event j(ByVal objarrayparam() As Object)
        Event k(ByVal objarrayparam As Object())
        Event l(ByRef objbyrefarrayparam() As Object)
        Event m(ByRef objbyrefarrayparam As Object())

        Event n(ByVal classparam As testclass1)
        Event o(ByRef classbyrefparam As testclass1)
        Event p(ByVal classarrayparam() As testclass1)
        Event q(ByVal classarrayparam As testclass1())
        Event r(ByRef classbyrefarrayparam() As testclass1)
        Event s(ByRef classbyrefarrayparam As testclass1())

    End Module
    Module testmodule2
        'Dim value As String
        'Shared Event a()
        'Shared Event b(ByVal builtinparam As Integer)
        'Shared Event c(ByRef builtinbyrefparam As Integer)
        'Shared Event d(ByVal builtinarrayparam() As Integer)
        'Shared Event e(ByVal builtinarrayparam As Integer())
        'Shared Event f(ByRef builtinbyrefarrayparam() As Integer)
        'Shared Event g(ByRef builtinbyrefarrayparam As Integer())

        'Shared Event h(ByVal objparam As Object)
        'Shared Event i(ByRef objbyrefparam As Object)
        'Shared Event j(ByVal objarrayparam() As Object)
        'Shared Event k(ByVal objarrayparam As Object())
        'Shared Event l(ByRef objbyrefarrayparam() As Object)
        'Shared Event m(ByRef objbyrefarrayparam As Object())

        'Shared Event n(ByVal classparam As testclass1)
        'Shared Event o(ByRef classbyrefparam As testclass1)
        'Shared Event p(ByVal classarrayparam() As testclass1)
        'Shared Event q(ByVal classarrayparam As testclass1())
        'Shared Event r(ByRef classbyrefarrayparam() As testclass1)
        'Shared Event s(ByRef classbyrefarrayparam As testclass1())

    End Module
    Module testmodule3
        Event a As testdelegate
        ' Shared Event b As testdelegate

        Delegate Sub testdelegate()
    End Module
    Module testmodule4
        'Private Shared Event a()
        'Friend Shared Event b(ByVal builtinparam As Integer)
        'Public Shared Event c(ByRef builtinbyrefparam As Integer)
        'Protected Shared Event d(ByVal builtinarrayparam() As Integer)
        'Protected Friend Shared Event e(ByVal builtinarrayparam As Integer())

        Private Event h(ByVal objparam As Object)
        Friend Event i(ByRef objbyrefparam As Object)
        Public Event j(ByVal objarrayparam() As Object)
        'Protected Event k(ByVal objarrayparam As Object())
        'Protected Friend Event l(ByRef objbyrefarrayparam() As Object)

    End Module
    Module testmodule5
        'Private Shared Event a As testdelegate
        'Friend Shared Event b As testdelegate
        'Public Shared Event c As testdelegate
        'Protected Shared Event d As testdelegate
        'Protected Friend Shared Event e As testdelegate

        Private Event h As testdelegate
        Friend Event i As testdelegate
        Public Event j As testdelegate
        'Protected Event k As testdelegate
        'Protected Friend Event l As testdelegate

        Delegate Sub testdelegate()
    End Module


    Interface testinterface1
        Event a()
        Event b(ByVal builtinparam As Integer)
        Event c(ByRef builtinbyrefparam As Integer)
        Event d(ByVal builtinarrayparam() As Integer)
        Event e(ByVal builtinarrayparam As Integer())
        Event f(ByRef builtinbyrefarrayparam() As Integer)
        Event g(ByRef builtinbyrefarrayparam As Integer())

        Event h(ByVal objparam As Object)
        Event i(ByRef objbyrefparam As Object)
        Event j(ByVal objarrayparam() As Object)
        Event k(ByVal objarrayparam As Object())
        Event l(ByRef objbyrefarrayparam() As Object)
        Event m(ByRef objbyrefarrayparam As Object())

        Event n(ByVal classparam As testclass1)
        Event o(ByRef classbyrefparam As testclass1)
        Event p(ByVal classarrayparam() As testclass1)
        Event q(ByVal classarrayparam As testclass1())
        Event r(ByRef classbyrefarrayparam() As testclass1)
        Event s(ByRef classbyrefarrayparam As testclass1())

    End Interface
    Interface testinterface2
        'Shared Event a()
        'Shared Event b(ByVal builtinparam As Integer)
        'Shared Event c(ByRef builtinbyrefparam As Integer)
        'Shared Event d(ByVal builtinarrayparam() As Integer)
        'Shared Event e(ByVal builtinarrayparam As Integer())
        'Shared Event f(ByRef builtinbyrefarrayparam() As Integer)
        'Shared Event g(ByRef builtinbyrefarrayparam As Integer())

        'Shared Event h(ByVal objparam As Object)
        'Shared Event i(ByRef objbyrefparam As Object)
        'Shared Event j(ByVal objarrayparam() As Object)
        'Shared Event k(ByVal objarrayparam As Object())
        'Shared Event l(ByRef objbyrefarrayparam() As Object)
        'Shared Event m(ByRef objbyrefarrayparam As Object())

        'Shared Event n(ByVal classparam As testclass1)
        'Shared Event o(ByRef classbyrefparam As testclass1)
        'Shared Event p(ByVal classarrayparam() As testclass1)
        'Shared Event q(ByVal classarrayparam As testclass1())
        'Shared Event r(ByRef classbyrefarrayparam() As testclass1)
        'Shared Event s(ByRef classbyrefarrayparam As testclass1())

    End Interface
    Interface testinterface3
        Event a As testdelegate
        'Shared Event b As testdelegate

        Delegate Sub testdelegate()
    End Interface
    Interface testinterface4
        'Private Shared Event a()
        'Friend Shared Event b(ByVal builtinparam As Integer)
        'Public Shared Event c(ByRef builtinbyrefparam As Integer)
        'Protected Shared Event d(ByVal builtinarrayparam() As Integer)
        'Protected Friend Shared Event e(ByVal builtinarrayparam As Integer())

        'Private Event h(ByVal objparam As Object)
        'Friend Event i(ByRef objbyrefparam As Object)
        'Public Event j(ByVal objarrayparam() As Object)
        'Protected Event k(ByVal objarrayparam As Object())
        'Protected Friend Event l(ByRef objbyrefarrayparam() As Object)

    End Interface
    Interface testinterface5
        'Private Shared Event a As testdelegate
        'Friend Shared Event b As testdelegate
        'Public Shared Event c As testdelegate
        'Protected Shared Event d As testdelegate
        'Protected Friend Shared Event e As testdelegate

        'Private Event h As testdelegate
        'Friend Event i As testdelegate
        'Public Event j As testdelegate
        'Protected Event k As testdelegate
        'Protected Friend Event l As testdelegate

        Delegate Sub testdelegate()
    End Interface
End Namespace
