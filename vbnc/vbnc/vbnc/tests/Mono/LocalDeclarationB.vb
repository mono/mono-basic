' Author:
'   Maverson Eduardo Schulze Rosa (maverson@gmail.com)
'
' GrupoTIC - UFPR - Federal University of Paran

Imports System

Module LocalDeclarationB

    Function Main() As Integer
        'this is declared as ".field  private static  specialname".
        Static stVarModule As Integer

        'this uses stsfld.
        stVarModule = 10

        'this uses ldsfld.
        If stVarModule <> 10 Then
            System.Console.WriteLine("#LDB1 - Load  Local Static Variable Failed") : Return 1
        End If

        Dim C As NonStaticField = New NonStaticField()
        c.test_class()
    End Function

    Class NonStaticField

        Function test_class() As Object
            'this is declared as ".field  private specialname" without static.
            Static stVarClass As Integer

            'this uses stfld.
            stVarClass = 10

            'this uses ldfld.
            If stVarClass <> 10 Then
                System.Console.WriteLine("#LDB2 - Load  Local Static Variable Failed") : Return 1
            End If
        End Function
    End Class
End Module
