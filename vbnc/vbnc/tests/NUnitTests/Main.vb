Imports System
Imports System.Reflection

Class NUnitTests
    Public Shared Function Main() As Integer
        Dim result As Boolean = True
        Dim ass As Assembly = Assembly.GetExecutingAssembly()

        For Each type As Type In ass.GetTypes()
            If Not type.IsDefined(GetType(TestFixtureAttribute), False) Then
                Continue For
            End If

            Console.WriteLine(type.FullName)
            Dim instance As Object = Activator.CreateInstance(type)
            For Each method As MethodInfo In type.GetMethods()
                Dim testResult As Boolean = True
                Dim exception As Exception = Nothing

                If Not method.IsDefined(GetType(TestAttribute), False) Then
                    Continue For
                End If

                Console.Write(" ")
                Console.Write(method.Name)

                Try
                    method.Invoke(instance, Nothing)
                Catch ex As Exception
                    exception = ex
                    If TypeOf exception Is TargetInvocationException Then
                        exception = exception.InnerException
                    End If
                End Try

                If exception Is Nothing Then
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine(" PASS")
                    Console.ResetColor()
                    testResult = True
                ElseIf TypeOf exception Is AssertException Then
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(" FAIL (Assertion failure):")
                    Console.ResetColor()
                    Console.Write("  ")
                    Console.WriteLine(exception.Message)
                    testResult = False
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(" FAIL (Unhandled exception):")
                    Console.ResetColor()
                    Console.Write("  ")
                    Console.WriteLine(exception.Message)
                    testResult = False
                End If

                If testResult = False Then
                    result = False
                End If
            Next
        Next

        If result Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class

Class TestFixtureAttribute
    Inherits Attribute
End Class

Class TestAttribute
    Inherits Attribute
End Class

Class AssertException
    Inherits Exception

    Public Sub New(ByVal msg As String)
        MyBase.new(msg)
    End Sub
End Class

Class Assert
    Public Shared Sub Fail(ByVal msg As String, ByVal ParamArray args() As Object)
        Throw New AssertException(String.Format(msg, args))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Double, ByVal actual As Double, ByVal delta As Double, ByVal msg As String)
        If Math.Abs(expected - actual) > Math.Abs(delta) Then
            Fail(msg)
        End If
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Double, ByVal actual As Double, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Integer, ByVal actual As Integer, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Long, ByVal actual As Long, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Single, ByVal actual As Single, ByVal msg As String)
        If Math.Abs(expected - actual) > Single.Epsilon Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As ULong, ByVal actual As ULong, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Date, ByVal actual As Date, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Decimal, ByVal actual As Decimal, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub

    Public Shared Sub AreEqual(ByVal expected As String, ByVal actual As String, ByVal msg As String)
        If String.Equals(expected, actual, StringComparison.Ordinal) = False Then
            fail(String.Format("Expected '{0}', got '{1}': {2}", expected, actual, msg))
        End If
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Boolean, ByVal actual As Boolean, ByVal msg As String)
        If expected <> actual Then fail(String.Format("Expected {0}, got {1}: {2}", expected, actual, msg))
    End Sub
End Class