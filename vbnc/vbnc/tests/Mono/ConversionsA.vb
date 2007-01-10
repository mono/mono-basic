
Imports System
Imports Microsoft.VisualBasic

Module ConversionsA

    Function f1(ByRef a As Object) As Object
    End Function

    Function f2(ByVal array() As Object, ByVal index As Integer, ByVal count As Integer, ByVal value As Object) As Object
        Dim i As Integer
        For i = index To (index + count) - 1
            array(i) = value
        Next i
    End Function


    Function Main() As Integer
        On Error GoTo ErrorHandler

        Dim a(10) As Object
        Dim b() As Object = New String(10) {}
        f1(a(0))
        f1(b(1)) ' ArrayTypeMismatchException

        Dim str(100) As String
        f2(str, 0, 101, "Undefined")
        f2(str, 0, 10, Nothing)
        f2(str, 91, 10, 0) ' ArrayTypeMismatchException
        Exit Function

ErrorHandler:
        If Err.Number <> 5 Then   ' System.ArrayTypeMismatchException
            System.Console.WriteLine("#CA1 - Conversion Statement failed") : Return 1
        End If
        Resume Next

    End Function

End Module