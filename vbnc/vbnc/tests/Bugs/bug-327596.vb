Module Lab3
    Dim Age, AvAge, TotalAge As Double
    Dim NumAge As Integer

    ''' <summary>
    ''' Helper function that reads a floating point number
    ''' from the console, enforcing correctness.
    ''' </summary>
    ''' <returns>Number as double.</returns>
    Function InputDouble() As Double
        Dim result As Double
        Dim str As String
        Dim okay As Boolean = False
        Do
            str = "0" 'Console.ReadLine()
            okay = Double.TryParse(str, result)
            If Not okay Then
                Console.WriteLine("You did not enter a proper number. Try again.")
                result = 0
            End If
        Loop Until okay
        Return result
    End Function

    Sub Main()
        Console.ForegroundColor = ConsoleColor.DarkMagenta
        Console.WriteLine("Average Age Calculator")
        Console.ResetColor()
        Console.WriteLine()
        Console.WriteLine("This program caluclates the average age for the ")
        Console.WriteLine("total number of ages entered by the user.")

        '' Initialize variables

        Age = 0
        AvAge = 0
        NumAge = 0
        TotalAge = 0

        PerformCalc()

        Console.WriteLine()
        Console.WriteLine("Goodbye. Press any key to exit.")

        '' Pause console output
        'Console.ReadKey()
    End Sub


    Sub ReadAge()
        Console.WriteLine()
        Console.WriteLine("Please enter an age. Please enter 0 to exit the program and read result.")
        Age = InputDouble()
    End Sub


    Sub PerformCalc()
        Do
            ReadAge()
            If Age > 0 Then
                NumAge = NumAge + 1
                TotalAge = TotalAge + Age
            ElseIf Age < 0 Then
                Console.WriteLine("Ages entered must be positive.")
            End If
        Loop Until Age = 0

        If NumAge <> 0 Then
            AvAge = TotalAge / NumAge
            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("The average age of " + NumAge.ToString() + " individual(s) is " + AvAge.ToString() + ".")
            Console.ResetColor()
        End If
    End Sub

End Module

