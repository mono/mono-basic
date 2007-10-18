' Authors:
'   Alexandre Rocha Lima e Marcondes (alexandre@psl-pr.softwarelivre.org)
'   Maverson Eduardo Schulze Rosa (maverson@gmail.com)
'
' GrupoTIC - UFPR - Federal University of Paraná


Module errorstmt

    Public error_number As Integer = 11

    Function Main() As Integer
        Try
            Error error_number

        Catch ex As System.Exception
            If Not (ex.GetType() Is GetType(System.DivideByZeroException)) Then
                System.Console.WriteLine("#A1 Error not working") : Return 1
            End If
        End Try
    End Function
End Module

