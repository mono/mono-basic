Imports system

Namespace MethodResolution8
    Interface I
        Function S() As Integer
    End Interface
    Structure S
        Dim F As Integer
    End Structure
    Delegate Function D() As Integer
    Class C
        Implements I

        Public Function S() As Integer Implements I.S

        End Function
    End Class
    Module Methods
        Public Function Main() As Integer
            Dim result As Boolean = True
            Dim something As Object = "something"


            result = Check("A1", result, A(Nothing), 1)
            result = Check("A2", result, A("a"), 3)
            result = Check("A3", result, A(New String() {"a", "b"}), 4)
            result = Check("A4", result, A(New String() {}), 2)
            result = Check("A5", result, A(), 2)

            result = Check("B10", result, B(Nothing), 1)
            result = Check("B11", result, B(), 2)
            result = Check("B12", result, B("b"), -1)
            result = Check("B13", result, B("b", 1), -2)
            result = Check("B14", result, B(New String() {"a"}), 3)
            result = Check("B15", result, B(New Object() {"a"}), 3)
            result = Check("B16", result, B(1, 2, 3, 4), 6)
            result = Check("B17", result, B(New Integer() {4, 5, 6}), -1)
            result = Check("B18", result, B(New I() {Nothing}), 3)
            result = Check("B19", result, B(New S() {Nothing}), -1)
            result = Check("B20", result, B(New D() {Nothing}), 3)
            result = Check("B21", result, B(New C() {Nothing}), 3)
            result = Check("B22", result, B(New S() {Nothing}), -1)
            result = Check("B23", result, B(Nothing, Nothing), -2)


            result = Check("C10", result, B(something), -1)
            result = Check("C11", result, B(), 2)
            result = Check("C12", result, B("b"), -1)
            result = Check("C13", result, B("b", 1), -2)
            result = Check("C14", result, B(New String() {"a"}), 3)
            result = Check("C15", result, B(New Object() {"a"}), 3)
            result = Check("C16", result, B(1, 2, 3, 4), 6)
            result = Check("C17", result, B(New Integer() {4, 5, 6}), -1)
            result = Check("C18", result, B(New I() {New C}), 3)
            result = Check("C19", result, B(New S() {New S}), -1)
            Dim d As D = New D(AddressOf C)
            result = Check("C20", result, B(New D() {d}), 3)
            result = Check("C21", result, B(New C() {New C}), 3)
            result = Check("C22", result, B(New S() {New S}), -1)
            result = Check("C23", result, B(something, something), -2)

            If result Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function Check(ByVal index As String, ByVal failed As Boolean, ByVal value As Integer, ByVal correct As Integer, Optional ByVal msg As String = "Test#{2}: Expected {0}, got {1}") As Boolean
            If correct <> value Then
                Console.WriteLine(msg, correct, value, index)
                System.Diagnostics.Debug.WriteLine(String.Format(msg, correct, value, index))
                Return False
            Else
                Return failed
            End If
        End Function

        Function A(ByVal ParamArray s() As String) As Integer
            If s Is Nothing Then Return 1
            Return s.Length + 2
        End Function

        Function B(ByVal ParamArray s() As Object) As Integer
            If s Is Nothing Then Return 1
            Return s.Length + 2
        End Function

        Function B(ByVal s As Object) As Integer
            Return -1
        End Function

        Function B(ByVal s As Object, ByVal s2 As Object) As Integer
            Return -2
        End Function

        Function C() As Integer
            Return -3
        End Function
    End Module
End Namespace