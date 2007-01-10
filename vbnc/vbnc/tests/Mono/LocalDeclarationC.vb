' Author:
'   Maverson Eduardo Schulze Rosa (maverson@gmail.com)
'
' GrupoTIC - UFPR - Federal University of Paraná

Imports System

Module LocalDeclarationC
    Function Main() As Integer
        Dim i As Integer = 0

        'Declaring a Local Static Variable in a Method Child Block.
        If i = 0 Then
            'this is declared as ".field  private static  specialname".
            Static Dim stVarModule As Integer

            'this uses stsfld.
            stVarModule = 10

            'this uses ldsfld.
            If stVarModule <> 10 Then
                System.Console.WriteLine("#LDC1 - Load  Local Static Variable Failed") : Return 1
            End If
        End If
    End Function
End Module
