Option Strict Off
Imports System
Imports Microsoft.VisualBasic

Module ExceptionHandlingC
    Dim failed As Boolean
    Function f1() As Object
        On Error GoTo ErrorHandler
        Dim i As Integer = 0
        i = 1 / i
        Console.WriteLine(i)
        Exit Function
ErrorHandler:
        System.Console.WriteLine("AA") : failed = True : Return 1
        Resume Next
    End Function

    Function f2() As Object
        On Error GoTo ErrorHandler
        f1()
        Exit Function
ErrorHandler:
        If Err.Description <> "AA" Then
            System.Console.WriteLine("#EHC1 - Error statement failed") : failed = True : Return 1
        End If
        Resume Next
    End Function

    Function f3() As Object
        On Error GoTo ErrorHandler
        Throw New DivideByZeroException()
        Exit Function
ErrorHandler:
        If Not TypeOf Err.GetException Is DivideByZeroException Then
            System.Console.WriteLine("#EHC2 - Error statement failed") : failed = True : Return 1
        End If
        Resume Next
    End Function

    Function f4() As Object
        On Error GoTo ErrorHandler
        Dim i As Integer = 0
        i = 5 / i
        On Error GoTo 0
        If i <> 1 Then
            System.Console.WriteLine("#EHC3 - Error Statement failed") : failed = True : Return 1
        End If
        Exit Function
ErrorHandler:
        i = 5
        Resume   ' Execution resumes with the statement that caused the error
    End Function

    Function f5() As Object
        On Error GoTo ErrorHandler
        Error 6    ' Overflow Exception
        Exit Function
ErrorHandler:
        If Err.Number <> 6 Then
            System.Console.WriteLine("#EHC4 - Error Statement failed") : failed = True : Return 1
        End If
        Resume Next
    End Function

    Function f6() As Object
        On Error GoTo ErrorHandler
        Dim i As Integer = 0, j As Integer
        i = 1 / i

        On Error GoTo 0  ' Disable error handler
        On Error Resume Next

        i = 0
        i = 1 / i ' create error 
        If Err.Number = 6 Then  ' handle error
            Err.Clear()
        Else
            System.Console.WriteLine("#EHC5 - Error Statement failed") : failed = True : Return 1
        End If

        i = 1 / i
        On Error GoTo -1
        If Err.Number <> 0 Then
            System.Console.WriteLine("#EHC6 - Error Statement failed") : failed = True : Return 1
        End If

        Exit Function
ErrorHandler:
        Select Case Err.Number
            Case 6
                i = 1
            Case Else
                System.Console.WriteLine("#EHC7 - Error Statement failed") : failed = True : Return 1
        End Select
        Resume
    End Function

    Function Main() As Integer
        f2() : f3() : f4() : f5() : f6()
        If failed Then Return 1
    End Function

End Module

