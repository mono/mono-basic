
Imports System

Module M1

    Function Main() As Integer
        Console.WriteLine(f1())
    End Function

    Function f1() As Integer

        Dim a As Long = 0
        Dim arr(17) As Boolean

        If a < System.Int64.MaxValue Then
            arr(0) = True
        End If

        If a <= System.Int64.MaxValue Then
            arr(1) = True
        End If

        If System.Int64.MaxValue > a Then
            arr(2) = True
        End If

        If System.Int64.MaxValue >= a Then
            arr(3) = True
        End If

        If a <> System.Int64.MaxValue Then
            arr(4) = True
        End If

        If a = System.Int64.MaxValue Then arr(5) = False Else arr(5) = True

        Dim b As Double = 0.0F

        If b < System.Double.MaxValue Then
            arr(6) = True
        End If

        If b <= System.Double.MaxValue Then
            arr(7) = True
        End If

        If System.Double.MaxValue > b Then
            arr(8) = True
        End If

        If System.Double.MaxValue >= b Then
            arr(9) = True
        End If

        If b <> System.Double.MaxValue Then
            arr(10) = True
        End If

        If b = System.Double.MaxValue Then arr(11) = False Else arr(11) = True

        Dim c As Decimal = 0D

        If c < System.Decimal.MaxValue Then
            arr(12) = True
        End If

        If c <= System.Decimal.MaxValue Then
            arr(13) = True
        End If

        If System.Decimal.MaxValue > c Then
            arr(14) = True
        End If

        If System.Decimal.MaxValue >= c Then
            arr(15) = True
        End If

        If c <> System.Decimal.MaxValue Then
            arr(16) = True
        End If

        If c = System.Decimal.MaxValue Then arr(17) = False Else arr(17) = True

        If arr.GetUpperBound(0) <> 17 Then
            System.Console.WriteLine("#A UpperBound wrong") : Return 1
        End If
        Dim j As Integer = 0
        Dim s As String
        For i As Integer = 0 To arr.GetUpperBound(0)
            If i <> j Then
                s = "#A" + j.toString() + "Relational Operator Not working"
                System.Console.WriteLine() : Return 1
            End If
            j = j + 1
        Next

        For Each bval As Boolean In arr
            If Not bval Then
                Return 1
            End If
        Next

        Return 0

    End Function


End Module
