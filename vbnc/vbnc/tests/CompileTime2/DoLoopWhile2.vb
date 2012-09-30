Imports System
Imports System.Web.UI.WebControls

Public Class D
    Shared counter As Integer
    Shared looped As Integer

    Public Shared Function Main() As Integer
        console.writeline("main")
        counter += 1
        If counter > 1 Then
            console.writeline("failed")
            Return 1
        End If
        Console.WriteLine("test")

        Do
            looped += 1
            If looped > counter Then
                console.writeline("done")
                Return 0
            End If
            Continue Do
        Loop
    End Function
End Class