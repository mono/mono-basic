' Author:
'   Maverson Eduardo Schulze Rosa (maverson@gmail.com)
'
' GrupoTIC - UFPR - Federal University of Paraná


Imports System
Imports System.Collections

Module ForEachC

    Class C1
        Public ReadOnly index As String = ""

        Sub New()
            Dim arr As New ArrayList
            arr.Add("a")
            arr.Add("b")
            arr.Add("c")

            For Each index In arr
            Next
        End Sub

    End Class

    Function Main() As Integer
        Dim c As New C1()
        If Not c.index.Equals("c") Then
            System.Console.WriteLine("#FEC1") : Return 1
        End If
    End Function

End Module
