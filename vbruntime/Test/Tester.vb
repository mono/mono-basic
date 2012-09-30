Imports System.Reflection
Imports NUnit.Framework

Module Tester

    Function Main() As Integer
        System.Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.GetCultureInfo("en-US")
        System.Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.GetCultureInfo("en-US")

        Dim success As Boolean = True
        Dim objects As New ArrayList
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.BooleanTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.ByteTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.ShortTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.IntegerTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.LongTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.SingleTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.DoubleTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.DateTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.CompilerServices.DecimalTypeTest)
        objects.Add(New MonoTests.Microsoft_VisualBasic.OperatorsTests)
        objects.Add(New MonoTests.Microsoft_VisualBasic.FileSystemTests2)

        Dim ms As New Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute
        If True OrElse ms.GetType.Assembly.Location.Contains("assembly\GAC") = False Then
            objects.Add(New MonoTests.Microsoft_VisualBasic.FileSystemTestGenerated)
        End If

        Dim counter As Integer = 0
        For Each obj As Object In objects
            For Each method As MethodInfo In obj.GetType.GetMethods()
                counter += 1
                Dim failed As Boolean = False
                Dim ex2 As Exception = Nothing
                Dim exp As ExpectedExceptionAttribute = Nothing

                If method.IsDefined(GetType(TestAttribute), True) = False Then Continue For

                Dim exps() As Object = method.GetCustomAttributes(GetType(ExpectedExceptionAttribute), True)
                If exps IsNot Nothing AndAlso exps.Length = 1 Then
                    exp = DirectCast(exps(0), ExpectedExceptionAttribute)
                End If

                Try
                    method.Invoke(obj, New Object() {})
                Catch ex As TargetInvocationException
                    ex2 = ex.InnerException
                Catch ex As Exception
                    ex2 = ex
                End Try

                Dim msg As String = Nothing
                If ex2 Is Nothing AndAlso exp IsNot Nothing Then
                    msg = String.Format("Expected: " & exp.ExceptionType.FullName & " (" & exp.ExpectedMessage & ")")
                ElseIf ex2 IsNot Nothing AndAlso exp Is Nothing Then
                    msg = String.Format("Didn't expect: " & ex2.GetType.FullName & " (" & ex2.Message & ")")
                ElseIf ex2 IsNot Nothing AndAlso exp IsNot Nothing AndAlso (ex2.GetType Is exp.ExceptionType = False OrElse (ex2.Message <> exp.ExpectedMessage AndAlso exp.ExpectedMessage <> "")) Then
                    msg = String.Format("Expected: " & exp.ExceptionType.FullName & " (" & exp.ExpectedMessage & "), got: " & ex2.GetType.FullName & " (" & ex2.Message & ")")
                End If

                If msg <> "" Then
                    failed = True
                    success = False
                    msg = ": " & msg
                End If
                Dim line As String = (Not failed).ToString()(0).ToString & " " & method.Name & msg
                Console.WriteLine(line)
                Debug.WriteLine(counter.ToString() & " " & line)
                If failed AndAlso System.Diagnostics.Debugger.IsAttached Then
                    Stop
                End If

                success = success AndAlso failed = False
            Next
        Next

        If Not success Then
            Console.WriteLine("Find the 'ANY' key on the keyboard and press it to exit.")
            Console.Read()
        Else
            Debug.WriteLine("Succeded!")
        End If
    End Function

End Module
