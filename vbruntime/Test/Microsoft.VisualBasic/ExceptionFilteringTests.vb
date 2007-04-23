Imports NUnit.Framework

<TestFixture()> _
Public Class ExceptionFilteringTests

    <Test()> _
    Public Sub ExceptionFilter_EmptyFilter()
        Try
            Throw New ArgumentException
            Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
        Catch e1 As ArgumentException
            ' pass
        Catch e2 As Exception
            Assert.Fail("Catched {0} intead of {1}", e2.GetType, Type.GetType("System.ArgumentException"))
        End Try
    End Sub

    <Test()> _
    Public Sub ExceptionFilter_Filter1()
        Dim filter1 As Boolean = True
        Dim filter2 As Boolean = False
        Try
            Throw New ArgumentException
            Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
        Catch e1 As ArgumentException When filter1
            'pass
        Catch e2 As ArgumentException When filter2
            Assert.Fail("Exception catched, but filter is {0}", filter2)
        Catch e2 As ArgumentException
            Assert.Fail("Exception catched, but there is no filter")
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
        End Try
    End Sub

    <Test()> _
    Public Sub ExceptionFilter_Filter2()
        Try
            Throw New ArgumentException
            Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
        Catch e1 As ArgumentException When FilterTrue()
            'pass
        Catch e2 As ArgumentException When FilterFalse()
            Assert.Fail("Exception catched, but filter is {0}", FilterFalse())
        Catch e2 As ArgumentException
            Assert.Fail("Exception catched, but there is no filter")
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
        End Try
    End Sub

    <Test(), Category("TargetJvmNotWorking")> _
    Public Sub ExceptionFilter_FilterThrow()
        Try
            Throw New ArgumentException
            Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
        Catch e1 As ArgumentException When FilterThrow()
            Assert.Fail("Exception catched, inspite of filter exception thrown.")
        Catch e2 As ArgumentException
            ' recatch and pass
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
        End Try
    End Sub

    <Test()> _
    Public Sub ExceptionFilter_Filter3()
        Dim visitedFinally As Boolean = False
        Try
            Throw New ArgumentException
            Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
        Catch e1 As ArgumentException When FilterTrue()
            'pass
        Catch e2 As ArgumentException When FilterFalse()
            Assert.Fail("Exception catched, but filter is {0}", FilterFalse())
        Catch e2 As ArgumentException
            Assert.Fail("Exception catched, but there is no filter")
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
        Finally
            visitedFinally = True
        End Try

        If Not visitedFinally Then
            Assert.Fail("Finally block was not called")
        End If
    End Sub

    <Test()> _
    Public Sub ExceptionFilter_FilterNested1()
        Try

            Dim visitedFinally As Boolean = False
            Try
                Throw New ArgumentException
                Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
            Catch e1 As ArgumentException When FilterTrue()
                'pass
            Catch e2 As ArgumentException When FilterFalse()
                Assert.Fail("Exception catched, but filter is {0}", FilterFalse())
            Catch e2 As ArgumentException
                Assert.Fail("Exception catched, but there is no filter")
            Catch e3 As Exception
                Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
            Finally
                visitedFinally = True
            End Try

            Throw New IndexOutOfRangeException

        Catch e4 As IndexOutOfRangeException When FilterTrue()
            ' pass
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.IndexOutOfRangeException"))
        End Try
    End Sub

    <Test()> _
    Public Sub ExceptionFilter_FilterNested2()
        Try

            Dim visitedFinally As Boolean = False
            Try
                Throw New ArgumentException
                Assert.Fail("{0} was not thrown", Type.GetType("System.ArgumentException"))
            Catch e1 As ArgumentException When FilterTrue()
                Throw
            Catch e3 As Exception
                Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
            Finally
                visitedFinally = True
            End Try

            Assert.Fail("Exception was not rethrown.")

        Catch e4 As ArgumentException When FilterTrue()
            ' catch rethrown exception and pass
        Catch e3 As Exception
            Assert.Fail("Catched {0} intead of {1}", e3.GetType, Type.GetType("System.ArgumentException"))
        End Try
    End Sub

    Public Function FilterTrue()
        Return True
    End Function

    Public Function FilterFalse()
        Return False
    End Function

    Public Function FilterThrow()
        Throw New IndexOutOfRangeException
    End Function

End Class
